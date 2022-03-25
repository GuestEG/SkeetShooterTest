namespace Inputs
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.InputSystem.OnScreen;
    
	public class OnScreenDeltaDrag : OnScreenControl, IDragHandler, IInitializePotentialDragHandler
	{
		private string _controlPath;

		[SerializeField] private Vector2 _scaleVal = new Vector2(16f, 10f);
        [SerializeField] private bool _scale = true;

        [SerializeField] private bool _normalizeByDPI = true;

        private bool _hasDrag;
        private Vector2 _delta;

        protected override string controlPathInternal
		{
			get => _controlPath;
			set => _controlPath = value;
		}

        private void LateUpdate()
		{
			if (_hasDrag)
			{
				SendValueToControl(_delta);
				_hasDrag = false;
			}
			else
			{
				SendValueToControl(Vector2.zero);
			}
		}

        void IDragHandler.OnDrag(PointerEventData eventData)
		{
			//TODO: OnDrag event has a significant threshold before firing and then produces large delta up to 10+ units. Need a way to eliminate this
			//API does not provide anything meaningful for this
            //Debug.Log($"Drag delta value = {eventData.delta}, thres = {EventSystem.current.pixelDragThreshold}, mult = {FindObjectOfType<InputSystemUIInputModule>().trackedDeviceDragThresholdMultiplier}");
			_delta = eventData.delta;
			
            if (_normalizeByDPI)
            {
				_delta /= Screen.dpi;
            }

			if (_scale)
			{
				_delta.Scale(_scaleVal);
			}

			_hasDrag = true;
		}

		void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
		{
			//should prevent drag threshold, but does not do that on touch display
			eventData.useDragThreshold = false;
        }
	}
}