namespace Controllers
{
	using Actors;
	using UnityEngine;
	using UnityEngine.UI;

	public sealed class LaunchersController : MonoBehaviour
	{
		private static int ClayFlight = Animator.StringToHash("ClayFlight");

		[SerializeField] private Launcher[] _launchers;
		[SerializeField] private Clay _clayPrefab;
		[SerializeField] private Button _launchButton;

		[SerializeField] private float _flyTime = 5f;

		public Clay ClayObject { get; private set; }

		public bool IsFlying => Time.time <= _endFlight && ClayObject != null && !ClayObject.Exploded;

		public float TimeLeft => _endFlight - Time.time;
		public float TimeInFlight => _flyTime - TimeLeft;

		private float _endFlight = 0f;
		private int _numLaunch = 0;
		private Launcher CurLauncher =>  _launchers[_numLaunch];

		public void Launch()
		{
			if (ClayObject == null)
			{
				ClayObject = Instantiate(_clayPrefab);
			}
			
			ClayObject.Clear();
			ClayObject.transform.SetParent(CurLauncher.ClayContainer, false);
			CurLauncher.Animator.Play(ClayFlight);

			_endFlight = Time.time + _flyTime;

			_numLaunch++;
			_numLaunch %= _launchers.Length;
		}

		public void HitClay()
		{
			ClayObject.Explode();
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