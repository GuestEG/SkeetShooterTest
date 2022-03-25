namespace Configs
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "ShooterTest/GameConfig")]
    public class GameConfig : ScriptableObject
    {
	    public float BasicAimTime = 1f;
        public AnimationCurve AimTimeMultiplier = AnimationCurve.Linear(0f, 0.25f, 5f, 1f);
	}
}