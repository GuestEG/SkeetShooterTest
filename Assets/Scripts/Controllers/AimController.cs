namespace Controllers
{
    using System;
    using Configs;
    using PlasticGui.WorkspaceWindow.Merge;
    using UnityEngine;
    using UnityEngine.UI;

    public class AimController : MonoBehaviour
    {
        [SerializeField] private Image _aimProgressBar;

        private LaunchersController _launchersController;
        private CameraController _cameraController;
        private GameConfig _config;

        private float _progress = 0;
        private bool _complete = false;

        public event Action ProgressComplete;

        public void SetConfig(
            GameConfig gameConfig, 
            LaunchersController launchersController,
            CameraController cameraController)
        {
            _config = gameConfig;
            _launchersController = launchersController;
            _cameraController = cameraController;
        }

        private void Update()
        {
            if (_launchersController == null || _cameraController == null)
            {
                return;
            }

            if (!_launchersController.IsFlying)
            {
                _complete = false;
                _progress = 0;
                _aimProgressBar.fillAmount = _progress;
                return;
            }

            _progress = CalculateProgress(_progress, _launchersController, _cameraController, _config);
            _aimProgressBar.fillAmount = _progress;
            
            //fire once
            if (_progress >= 1.0 && !_complete)
            {
                Debug.Log($"Complete!");
                ProgressComplete?.Invoke();
                _complete = true;
            }
        }

        private float CalculateProgress(float progress, LaunchersController launchersController, CameraController cameraController,  GameConfig config)
        {
            var aimingVector = cameraController.CameraPivot.forward;
            var targetVector = launchersController.ClayObject.transform.position - cameraController.CameraPivot.position;
            var angle = Vector3.Angle(aimingVector, targetVector);
            var inSight = angle <= config.AimOffAngle;
            
            if (!inSight)
            {
                return 0;
            }

            var timeInFlight = launchersController.TimeInFlight;
            var timeMult = config.BasicAimTime * config.AimTimeMultiplier.Evaluate(timeInFlight);
            var progressPerSec = 1 / timeMult;
            var deltaProgress = Time.deltaTime * progressPerSec;
            
            return progress + deltaProgress;
        }
    }
}