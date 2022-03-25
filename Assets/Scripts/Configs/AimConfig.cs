namespace Configs
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "ShooterTest/AimConfig")]
	public sealed class AimConfig : ScriptableObject
	{
		public float AimOffAngle = 5f;
		public float BasicAimTime = 1f;
		public AnimationCurve AimTimeMultiplier = AnimationCurve.Linear(0f, 0.25f, 5f, 1f);
		public float DesperateTime = 0.1f;
	}
}