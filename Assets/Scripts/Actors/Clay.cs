namespace Actors
{
    using UnityEngine;

    public class Clay : MonoBehaviour
    {
        [SerializeField] private GameObject _clayBody;
        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private TrailRenderer _trail;

        public bool Exploded { get; private set; }

        public void Clear()
        {
            Exploded = false;
            _clayBody.SetActive(true);
            _trail.Clear();
            _explosion.Stop(true);
            _explosion.Clear(true);
        }
        
        public void Explode()
        {
            Exploded = true;
            _clayBody.SetActive(false);
            _explosion.Play(true);
        }
    }
}