// Based on http://wiki.unity3d.com/index.php/FPSWalkerEnhanced
// by Eric Haines (Eric5h5), @torahhorse, @igaryhe

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonDrifter : MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;

    public bool enableRunning = false;

    public float jumpSpeed = 4.0f;
    public float gravity = 10.0f;

    // If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
    public bool slideWhenOverSlopeLimit = false;

    // If checked and the player is on an object tagged "Slide", he will slide down it regardless of the slope limit
    public bool slideOnTaggedObjects = false;

    public float slideSpeed = 5.0f;

    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;

    // Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
    public int antiBunnyHopFactor = 1;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded;
    private CharacterController controller;
    private Transform myTransform;
    private float speed;
    private RaycastHit hit;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private int jumpTimer;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;

    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        myTransform = transform;
        speed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveAction = InputSystem.actions["Move"];
        sprintAction = InputSystem.actions["Sprint"];
        jumpAction = InputSystem.actions["Jump"];
        sprintAction.Enable();
        jumpAction.Enable();   
    }

    public void Update()
    {
        var moveInput = moveAction.ReadValue<Vector2>();

        if (grounded)
        {
            var sliding = false;

            // See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
            // because that interferes with step climbing amongst other annoyances
            if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
            // However, just raycasting straight down from the center can fail when on steep slopes
            // So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
            else
            {
                Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }

            if (enableRunning)
            {
                speed = sprintAction.IsPressed() ? runSpeed : walkSpeed;
            }

            // If sliding (and it's allowed), or if we're on an object tagged "Slide", get a vector pointing down the slope we're on
            if (sliding && slideWhenOverSlopeLimit || slideOnTaggedObjects && hit.collider.CompareTag("Slide"))
            {
                var hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
            }
            // Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
            else
            {
                moveDirection = new Vector3(moveInput.x, -antiBumpFactor, moveInput.y);
                moveDirection = myTransform.TransformDirection(moveDirection) * speed;
            }

            // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
            if (!jumpAction.triggered) jumpTimer++;
            else if (jumpTimer >= antiBunnyHopFactor)
            {
                moveDirection.y = jumpSpeed;
                jumpTimer = 0;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller, and set grounded true or false depending on whether we're standing on something
        var collision = controller.Move(moveDirection * Time.deltaTime);
        grounded = (collision & CollisionFlags.Below) != 0;
    }

    // Store point that we're in contact with for use in Update if needed
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contactPoint = hit.point;
    }
}