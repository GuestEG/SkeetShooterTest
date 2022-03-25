namespace Controllers
{
	using Actors;
	using UnityEngine;
	using UnityEngine.UI;

	public class LaunchersController : MonoBehaviour
	{
		private static int ClayFlight = Animator.StringToHash("ClayFlight");
		
		[SerializeField] private Launcher[] _launchers;
		[SerializeField] private Clay _clayPrefab;
		[SerializeField] private Button _launchButton;

		[SerializeField] private float _flyTime = 5f;

		public Clay ClayObject { get; private set; }

		private int _numLaunch = 0;

		private bool IsFlying => Time.time <= _endFlight;

		private float _endFlight = 0f;

		public void Launch()
		{
			if (ClayObject == null)
			{
				ClayObject = Instantiate(_clayPrefab);
			}

			var launcher = _launchers[_numLaunch];
			_numLaunch++;
			_numLaunch %= _launchers.Length;
			ClayObject.Clear();
			ClayObject.transform.SetParent(launcher.ClayContainer, false);
			launcher.Animator.Play(ClayFlight);

			_endFlight = Time.time + _flyTime;
		}

		private void Start()
		{
			_launchButton.onClick.AddListener(() =>
			{
				if (!IsFlying)
				{
					Launch();
				}
			});
		}

		private void Update()
		{
			_launchButton.gameObject.SetActive(!IsFlying);
		}

		private void OnDestroy()
		{
			_launchButton.onClick.RemoveAllListeners();
		}
	}
}