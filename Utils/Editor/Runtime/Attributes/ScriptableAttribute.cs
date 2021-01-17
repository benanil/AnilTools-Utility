using System;
using UnityEngine;

namespace AnilTools.AnilEditor
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ScriptableAttribute : PropertyAttribute
    {
        public bool Openable { get; set; }

        public ScriptableAttribute(bool isArray = false)
        {
            Openable = isArray;
        }
    }
}