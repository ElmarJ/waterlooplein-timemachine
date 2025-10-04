// by @torahhorse
// modified by @igaryhe

// Instructions:
// Place on player. OnBelowLevel will get called if the player ever falls below

using UnityEngine;
using UnityEngine.SceneManagement;

public class FallOff : MonoBehaviour
{
	public float resetBelowThisY = -30f;
	
	private Vector3 startingPosition;

	private void Awake()
	{
		startingPosition = transform.position;
	}

	private void Update ()
	{
		if (transform.position.y >= resetBelowThisY) return;
		transform.position = startingPosition;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
