using UnityEngine;
using UnityEngine.InputSystem;

/**
 *  This component allows the player to add force to its object using the arrow keys.
 *  Works with a 3D RigidBody.
 */
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TouchDetector))]
public class Mover : MonoBehaviour
{
    [Tooltip("The horizontal force that the player's feet use for walking, in newtons.")]
    [SerializeField] float walkForce = 5f;
    [SerializeField] InputAction moveHorizontal;

    [Tooltip("The vertical force that the player's feet use for jumping, in newtons.")]
    [SerializeField] float jumpImpulse = 5f;
    [SerializeField] InputAction jump;
    [SerializeField] float jumpForce = 5f;

    [Range(0, 1f)]
    [SerializeField] float slowDownAtJump = 0.5f;
    [SerializeField] public bool isGrounded;
    [SerializeField] float maxVelocityX = 0.5f;

    [SerializeField] float flightForce = 5f;
    void OnValidate()
    {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (jump == null)
            jump = new InputAction(type: InputActionType.Button);
        if (jump.bindings.Count == 0)
            jump.AddBinding("<Keyboard>/space");

        if (moveHorizontal == null)
            moveHorizontal = new InputAction(type: InputActionType.Button);
        if (moveHorizontal.bindings.Count == 0)
            moveHorizontal.AddCompositeBinding("1DAxis")
                .With("Positive", "<Keyboard>/rightArrow")
                .With("Negative", "<Keyboard>/leftArrow");
    }

    private Rigidbody2D rb;
    private TouchDetector td;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        td = GetComponent<TouchDetector>();
    }

    private ForceMode2D walkForceMode = ForceMode2D.Force;
    private ForceMode2D jumpForceMode = ForceMode2D.Impulse;
    private ForceMode2D jumpContForceMode = ForceMode2D.Force;
    private bool playerWantsToJump = false;

    private void Update()
    {
        // Keyboard events are checked each frame, so we should check them in Update.
        if (jump.WasPressedThisFrame())
            playerWantsToJump = true;
    }
    private void OnEnable()
    {
        moveHorizontal.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        moveHorizontal.Disable();
        jump.Disable();
    }

    /*
     * Note that updates related to the physics engine should be done in FixedUpdate and not in Update!
     */
    private void FixedUpdate()
    {
        if (isGrounded)
        {  // allow to walk and jump 
            float horizontal = moveHorizontal.ReadValue<float>();
            if(rb.velocity.x < maxVelocityX)
            {
                rb.AddForce(new Vector2(horizontal * walkForce, 0), walkForceMode);
            }
            if (playerWantsToJump)
            {            // Since it is active only once per frame, and FixedUpdate may not run in that frame!
                rb.velocity = new Vector2(rb.velocity.x * slowDownAtJump, rb.velocity.y);
                rb.AddForce(new Vector2(0, jumpImpulse), jumpForceMode);
                playerWantsToJump = false;
            }
        }
        else
        {
            float horizontal = moveHorizontal.ReadValue<float>();
            rb.AddForce(new Vector2(horizontal * flightForce, 0), walkForceMode);
            if (jump.IsPressed())
            {
                rb.AddForce(new Vector2(0, jumpForce), jumpContForceMode);
            }
        }
    }
}