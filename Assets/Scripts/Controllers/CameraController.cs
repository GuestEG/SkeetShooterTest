namespace Controllers
{
    using Configs;
    using UnityEngine;

    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraPivot;

        private CameraConfig _config;

        public Transform CameraPivot => _cameraPivot;
        public void OnDeltaDrag(Vector2 delta)
        {
            if (_config == null)
            {
                return;
            }

            var curCameraAngles = _cameraPivot.localEulerAngles;
            
            //horizontal is around up vector (sensitivity should be negative)
            curCameraAngles.y += delta.x * _config.HorizontalSensitivity;
            //vertical is around right vector
            curCameraAngles.x += delta.y * _config.VerticalSensitivity;
            curCameraAngles = ClampCameraAngles(curCameraAngles, _config);

            _cameraPivot.localEulerAngles = curCameraAngles;
        }

        public void SetConfig(CameraConfig cameraConfig)
        {
            _config = cameraConfig;
        }

        private Vector3 ClampCameraAngles(Vector3 curCameraAngles, CameraConfig config)
        {
            //euler angles snap around 0 to 360;
            if (curCameraAngles.y > config.HorizontalAngle / 2f
                && curCameraAngles.y <= 180)
            {
                curCameraAngles.y = config.HorizontalAngle / 2f;
            }

            if (curCameraAngles.y < 360 - config.HorizontalAngle / 2f
                && curCameraAngles.y > 180)
            {
                curCameraAngles.y = 360 - config.HorizontalAngle / 2f;
            }


            if (curCameraAngles.x > config.VerticalAngle / 2f
                && curCameraAngles.x <= 180)
            {
                curCameraAngles.x = config.VerticalAngle / 2f;
            }

            if (curCameraAngles.x < 360 - config.VerticalAngle / 2f
                && curCameraAngles.x > 180)
            {
                curCameraAngles.x = 360 - config.VerticalAngle / 2f;
            }

            return curCameraAngles;
        }

        
    }
}