using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent (typeof (AudioSource), typeof(CharacterController))]
public class Footstep : MonoBehaviour
{
    public AudioClip[] footsteps;
    [Range(.1f, 3f)]
    public float pitch = 1;
    public float minSpeed = 0.5f;
    private CharacterController controller;
    private AudioSource audioSource;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (controller.velocity.magnitude < minSpeed || !controller.isGrounded || audioSource.isPlaying) return;
        audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
        audioSource.volume = Random.Range(0.8f, 1);
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
