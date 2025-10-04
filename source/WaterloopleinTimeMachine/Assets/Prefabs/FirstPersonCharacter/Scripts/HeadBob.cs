// original by Mr. Animator
// adapted to C# by @torahhorse
// modified by @igaryhe
// http://wiki.unity3d.com/index.php/Headbobber

using UnityEngine;
using UnityEngine.InputSystem;

public class HeadBob : MonoBehaviour
{	
	public float bobbingSpeed = 10f; 
	public float bobbingAmount = 0.05f; 
	public float  midpoint = 0.6f; 
	
	private float timer;

	private float horizontal;
	private float vertical;

	private void Update ()
	{ 
	    var waveslice = 0.0f;

	    if (Mathf.Abs(horizontal) == 0f && Mathf.Abs(vertical) == 0f)
	    { 
	       timer = 0.0f; 
	    } 
	    else
	    { 
	       waveslice = Mathf.Sin(timer); 
	       timer += bobbingSpeed * Time.deltaTime; 
	       if (timer > Mathf.PI * 2f) timer -= Mathf.PI * 2f;
	    }

	    var localPos = transform.localPosition;
	    if (waveslice != 0f)
	    {
		    var translateChange = waveslice * bobbingAmount; 
		    var totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical); 
		    totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f); 
		    translateChange = totalAxes * translateChange;
		    
		    localPos.y = midpoint + translateChange * Time.timeScale;
	    } 
	    else localPos.y = midpoint;
	    transform.localPosition = localPos;
	}
	
	public void OnMove(InputAction.CallbackContext ctx)
	{
		var movement = ctx.ReadValue<Vector2>();
		horizontal = movement.x;
		vertical = movement.y;
	}
}
