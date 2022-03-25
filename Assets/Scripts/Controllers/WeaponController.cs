namespace Controllers
{
    using UnityEngine;

    public sealed class WeaponController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Animator _animator;

        private static int Shot = Animator.StringToHash("Shot");

        public void Shoot()
        {
            _animator.Play(Shot);
            _particleSystem.Play(true);
        }
    }
}