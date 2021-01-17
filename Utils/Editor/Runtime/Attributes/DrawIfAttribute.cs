using UnityEngine;
using System;

namespace AnilTools.AnilEditor
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class DrawIfAttribute : PropertyAttribute
    {

        public string comparedPropertyName { get; private set; }
        public object comparedValue { get; private set; }
        public object comparedValue2 { get; private set; }
        public ComparisonType comparisonType { get; private set; }
        public DisablingType disablingType { get; private set; }

        public DrawIfAttribute(string comparedPropertyName, object comparedValue, ComparisonType comparisonType = ComparisonType.Equals, DisablingType disablingType = DisablingType.DontDraw)
        {
            this.comparedPropertyName = comparedPropertyName;
            this.comparedValue = comparedValue;
            this.comparisonType = comparisonType;
            this.disablingType = disablingType;
            comparedValue2 = comparedValue2;
        }
    }
}