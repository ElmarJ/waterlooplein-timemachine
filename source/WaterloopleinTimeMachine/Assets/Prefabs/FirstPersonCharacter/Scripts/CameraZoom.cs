// by @torahhorse
// modified by @igaryhe

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// allows player to zoom in the FOV when holding a button down
[RequireComponent (typeof (Camera))]
public class CameraZoom : MonoBehaviour
{
	public float zoomFOV = 30.0f;
	public float zoomSpeed = 9f;

	private float baseFOV;
	
	private Camera cam;

	private void Start ()
	{
		cam = GetComponent<Camera>();
		baseFOV = cam.fieldOfView;
	}
	private IEnumerator ZoomTo(float target)
	{
		while (Math.Abs(cam.fieldOfView - target) > 0.0001)
		{
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, target, zoomSpeed * Time.deltaTime);
			yield return null;
		}
	}

	public void OnZoom(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			StopAllCoroutines();
			StartCoroutine(ZoomTo(zoomFOV));
		}

		if (ctx.canceled)
		{
			StopAllCoroutines();
			StartCoroutine(ZoomTo(baseFOV));
		}
	}

	public void SetBaseFOV(float fov)
	{
		StopAllCoroutines();
		baseFOV = fov;
	}
}
