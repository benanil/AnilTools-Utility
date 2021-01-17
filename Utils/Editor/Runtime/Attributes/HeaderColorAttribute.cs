using System;
using UnityEngine;

namespace AnilTools.AnilEditor
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class HeaderColorAttribute : PropertyAttribute
    {
        public string Header { get; private set; }

        public HeaderColorAttribute(string Header)
        {
            this.Header = Header;
        }
    }
}