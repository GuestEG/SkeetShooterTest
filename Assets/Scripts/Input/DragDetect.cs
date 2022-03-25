namespace Input
{
	using System;
	using UnityEngine;
	using UnityEngine.EventSystems;

	public class DragDetect : MonoBehaviour, IDragHandler
	{
		public event Action<Vector2> DragEventDelta;

		public void OnDrag(PointerEventData data)
		{
			DragEventDelta?.Invoke(data.delta);
		}
	}
}