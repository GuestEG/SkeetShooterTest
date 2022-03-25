namespace Composition
{
    using Configs;
    using Controllers;
    using Input;
    using UnityEngine;
    
    public sealed class GameRoot : MonoBehaviour
    {
        [SerializeField] private DragDetect _dragDetect;
        [SerializeField] private CameraController _cameraController;
        // [SerializeField] private LaunchersController _launchersController;
        
        [Header("Configs")] 
        [SerializeField] private CameraConfig _cameraConfig;
        
        private void Start()
        {
            _cameraController.SetConfig(_cameraConfig);
            _dragDetect.DragEventDelta += _cameraController.OnDeltaDrag;
        }
    }
}