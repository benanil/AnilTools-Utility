using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

    /// <summary>
    /// Finds custom property drawer for a given type.
    /// </summary>
    public static class PropertyDrawerFinder
    {
        private static Dictionary<Type, PropertyDrawer> customDrawers = new Dictionary<Type, PropertyDrawer>();

        /// <summary>
        /// Searches for custom property drawer for given property, or returns null if no custom property drawer was found.
        /// </summary>
        public static PropertyDrawer Find(SerializedProperty property)
        {
            Type pType = PropertyDrawerFinder.GetPropertyType(property);
            if (!customDrawers.ContainsKey(pType))
            {
                customDrawers.Add(pType, PropertyDrawerFinder.Find(pType));
            }


            return customDrawers[pType];
        }


        /// <summary>
        /// Gets type of a serialized property.
        /// </summary>
        public static Type GetPropertyType(SerializedProperty property)
        {
            Type parentType = property.serializedObject.targetObject.GetType();
            string[] fullPath = property.propertyPath.Split('.');
            FieldInfo fi = parentType.GetField(fullPath[0]);
            for (int i = 1; i < fullPath.Length; i++)
            {
                fi = fi.FieldType.GetField(fullPath[i]);
            }
            return fi.FieldType;
        }


        /// <summary>
        /// Returns custom property drawer for type if one could be found, or null if
        /// no custom property drawer could be found. Does not use cached values, so it's resource intensive.
        /// </summary>
        public static PropertyDrawer Find(Type propertyType)
        {
            foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type candidate in assem.GetTypes())
                {
                    FieldInfo typeField = typeof(CustomPropertyDrawer).GetField("m_Type", BindingFlags.NonPublic | BindingFlags.Instance);
                    FieldInfo childField = typeof(CustomPropertyDrawer).GetField("m_UseForChildren", BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (Attribute a in candidate.GetCustomAttributes(typeof(CustomPropertyDrawer)))
                    {
                        if (a.GetType().IsSubclassOf(typeof(CustomPropertyDrawer)) || a.GetType() == typeof(CustomPropertyDrawer))
                        {
                            CustomPropertyDrawer drawerAttribute = (CustomPropertyDrawer)a;
                            Type drawerType = (Type)typeField.GetValue(drawerAttribute);
                            if (drawerType == propertyType ||
                                ((bool)childField.GetValue(drawerAttribute) && propertyType.IsSubclassOf(drawerType)) ||
                                ((bool)childField.GetValue(drawerAttribute) && IsGenericSubclass(drawerType, propertyType)))
                            {
                                if (candidate.IsSubclassOf(typeof(PropertyDrawer)))
                                {
                                    return (PropertyDrawer)Activator.CreateInstance(candidate);
                                }
                            }

                        }
                    }


                }
            }



            return null;
        }

        /// <summary>
        /// Returns true if the parent type is generic and the child type implements it.
        /// </summary>
        private static bool IsGenericSubclass(Type parent, Type child)
        {
            if (!parent.IsGenericType)
            {
                return false;
            }

            Type currentType = child;
            bool isAccessor = false;
            while (!isAccessor && currentType != null)
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == parent.GetGenericTypeDefinition())
                {
                    isAccessor = true;
                    break;
                }
                currentType = currentType.BaseType;
            }
            return isAccessor;
        }

    }