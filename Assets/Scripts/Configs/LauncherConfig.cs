namespace Configs
{
    using UnityEngine;

    public class LauncherConfig : ScriptableObject
    {
        public float HorizontalAngle = 120f;
        public float VerticalAngle = 30f;
        [Space]
        public float HorizontalSensitivity = 1.0f;
        public float VerticalSensitivity = 1.0f;
    }
}