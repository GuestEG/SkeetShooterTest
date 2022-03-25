namespace Actors
{
    using UnityEngine;

    public class Clay : MonoBehaviour
    {
        [SerializeField] private GameObject _clayBody;
        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private TrailRenderer _trail;

        public void Clear()
        {
            _clayBody.SetActive(true);
            _trail.Clear();
            _explosion.Stop(true);
            _explosion.Clear(true);
        }
        
        public void Explode()
        {
            _clayBody.SetActive(false);
            _explosion.Play(true);
        }
    }
}