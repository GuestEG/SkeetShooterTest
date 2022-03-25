namespace Controllers
{
	using Configs;
	using UnityEngine;
	using UnityEngine.UI;

	public sealed class AimController : MonoBehaviour
	{
		[SerializeField] private Image _aimProgressBar;

		private LaunchersController _launchersController;
		private CameraController _cameraController;
		private WeaponController _weaponController;
		private AimConfig _config;

		private float _progress = 0;
		private bool _hasShot = false;

		public void SetConfig(AimConfig aimConfig,
			LaunchersController launchersController,
			CameraController cameraController, 
			WeaponController weaponController)
		{
			_config = aimConfig;
			_launchersController = launchersController;
			_cameraController = cameraController;
			_weaponController = weaponController;
		}

		private void Update()
		{
			if (_launchersController == null || _cameraController == null)
			{
				return;
			}

			if (!_launchersController.IsFlying)
			{
				_hasShot = false;
				_progress = 0;
				_aimProgressBar.fillAmount = _progress;
				return;
			}

			if(!_hasShot)
			{
				_progress = CalculateProgress(_progress, _launchersController, _cameraController, _config);
				_aimProgressBar.fillAmount = _progress;

				//fire once
				if (_progress >= 1.0 && !_hasShot)
				{
					Shoot(_launchersController, _weaponController);
					_hasShot = true;
				}

				if (_launchersController.TimeLeft <= _config.DesperateTime)
				{
					Shoot(_launchersController, _weaponController, _progress);
					_hasShot = true;
				}
			}
		}

		private float CalculateProgress(float progress, LaunchersController launchersController, CameraController cameraController,  AimConfig config)
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

		private void Shoot(LaunchersController launchersController, WeaponController weaponController, float hitProbability = 1.0f)
		{
			//play shoot animation
			weaponController.Shoot();
			//the higher is progress - the more surface of random it will cover
			var isHit = hitProbability >= UnityEngine.Random.value;
			if (isHit)
			{
				launchersController.HitClay();
			}
		}
	}
}