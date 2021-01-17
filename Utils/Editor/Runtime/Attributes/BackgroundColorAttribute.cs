using UnityEngine;
using UrFairy;

namespace AnilTools.AnilEditor
{
    public enum BGColors
    { 
        white , black , blue , red , yellow , orange , pink , green
    }

    public class BackgroundColorAttribute : PropertyAttribute
    {
        private BGColors bgColor;

        public BackgroundColorAttribute(BGColors bGColor)
        {
            this.bgColor = bGColor;
        }

        public BackgroundColorAttribute()
        {
            bgColor = BGColors.white;
        }

        public Color color
        {
            get
            {
                switch (bgColor)
                {
                    case BGColors.white:
                        return Color.white;
                    case BGColors.black:
                        return Color.black.A(.3f);
                    case BGColors.blue:
                        return Color.blue.A(.3f);
                    case BGColors.red:
                        return Color.red.A(.3f);
                    case BGColors.yellow:
                        return Color.yellow;
                    case BGColors.orange:
                        return new Color(150, 180, 0).A(.3f);
                    case BGColors.pink:
                        return Color.magenta;
                    case BGColors.green:
                        return Color.green.A(.3f);
                    default:
                        return Color.white;
                }
            }
        }
    }
}