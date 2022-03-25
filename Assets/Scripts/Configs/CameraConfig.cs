namespace Configs
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "ShooterTest/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        public float HorizontalAngle = 120f;
        public float VerticalAngle = 30f;
        [Space]
        public float HorizontalSensitivity = -0.1f;
        public float VerticalSensitivity = 0.1f;
    }
}