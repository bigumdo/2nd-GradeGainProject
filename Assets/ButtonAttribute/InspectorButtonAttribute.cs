using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButtonAttribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class InspectorButtonAttribute : PropertyAttribute
    {
        public string label;
        public bool isToolTipActive;
        public string toolTip;
        
        public InspectorButtonAttribute(bool isToolTipActive, string tooltip = null, string label = null)
        {
            this.label = label;
            this.isToolTipActive = isToolTipActive;
            this.toolTip = tooltip;
        }
    }
}
