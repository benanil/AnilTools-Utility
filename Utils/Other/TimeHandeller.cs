using UnityEngine;

namespace AnilTools
{
    public class TimeHandeller
    {
        public readonly WaitForSecondsRealtime _60fps = new WaitForSecondsRealtime(0.01666f);
        public readonly WaitForSecondsRealtime _30fps = new WaitForSecondsRealtime(0.03333f);
        public readonly WaitForSecondsRealtime _24fps = new WaitForSecondsRealtime(0.04166f);
        public readonly WaitForSecondsRealtime OneSecond = new WaitForSecondsRealtime(1);
    }
}