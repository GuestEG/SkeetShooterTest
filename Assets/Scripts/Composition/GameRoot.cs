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
        [SerializeField] private LaunchersController _launchersController;
        [SerializeField] private AimController _aimController;
        [SerializeField] private WeaponController _weaponController;
        
        [Header("Configs")] 
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private AimConfig _aimConfig;
        
        
        private void Start()
        {
            _cameraController.SetConfig(_cameraConfig);
            _dragDetect.DragEventDelta += _cameraController.OnDeltaDrag;
            _aimController.SetConfig(_aimConfig, _launchersController, _cameraController, _weaponController);
        }
    }
}