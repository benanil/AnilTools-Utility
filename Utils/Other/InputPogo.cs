using UnityEngine;

namespace AnilTools
{
    public readonly struct InputPogo
    {
        private const string MouseXstr = "Mouse X";
        private const string MouseYstr = "Mouse Y";
        
        private const string HorizontalStr = "Horizontal";
        private const string VerticalStr = "Vertical";

        private const float Tolerance = .3f;

        public static float MouseX
        {
            get
            {
                return Input.GetAxis(MouseXstr);
            }
        }
        
        public static float MouseY
        {
            get
            {
                return Input.GetAxis(MouseYstr);
            }
        }

        private static Vector2 mouseVelocity;
        public static Vector2 MouseVelocity
        {
            get
            {
                mouseVelocity.x = MouseX;
                mouseVelocity.y = MouseY;
                return mouseVelocity;
            }
        }

        public static bool MouseDown
        {
            get
            {
                return Input.GetMouseButtonDown(0);
            }
        }

        public static bool MouseUp
        {
            get
            {
                return Input.GetMouseButtonUp(0);
            }
        }

        public static bool MouseHold
        {
            get
            {
                return Input.GetMouseButton(0);
            }
        }

        public static bool SwipingRight
        {
            get
            {
                return MouseX > Tolerance && mouseVelocity.y < .3f;
            }
        }

        public static bool SwipingLeft
        {
            get
            {
                return MouseX < Tolerance && mouseVelocity.y < .3f;
            }
        }

        public static bool SwipingUp
        {
            get
            {
                return MouseY < Tolerance && mouseVelocity.x < .3f;
            }
        }

        public static bool SwipingDown
        {
            get
            {
                return MouseY < Tolerance && mouseVelocity.x < .3f;
            }
        }

#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // windows fln
        public static float KeyHorizontal
        {
            get
            {
                return Input.GetAxis(HorizontalStr);
            }
        }
        public static float KeyVertical
        {
            get
            {
                return Input.GetAxis(VerticalStr);
            }
        }
        private static Vector2 keyVelocity;
        public static Vector2 KeyVelocity
        {
            get
            {
                keyVelocity.x = KeyHorizontal;
                keyVelocity.y = KeyVertical;
                return keyVelocity;
            }
        }
#endif
    }
}
