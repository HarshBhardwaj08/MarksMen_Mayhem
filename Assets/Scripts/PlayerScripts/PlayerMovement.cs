using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;

    [SerializeField] private CharacterController characterController;

    public Vector2 movementInput { get; private set; }
    private bool isRunning;
    private float originalWalkSpeed;

    private Animator animator; 

    private Dave playerControls;
    private MovementController movementController;
    private AimController aimController;
    private void Start()
    {
        playerControls = GetComponent<PLayer>().playerControls;

        animator = GetComponentInChildren<Animator>();
        originalWalkSpeed = walkSpeed;
        SetupInput();
        movementController = new MovementController(characterController);

    }
    private void SetupInput()
    {
        playerControls.Character.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        playerControls.Character.Movement.canceled += ctx => movementInput = Vector2.zero;

        playerControls.Character.Sprint.performed += ctx => { walkSpeed *= 1.5f; isRunning = true; };
        playerControls.Character.Sprint.canceled += ctx => { walkSpeed = originalWalkSpeed; isRunning = false; };
    }

    private void Update()
    {
        movementController.Move(new Vector3(movementInput.x, 0, movementInput.y), walkSpeed);
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("xVelocity", movementInput.x, 0.1f, Time.deltaTime);
        animator.SetFloat("zVelocity", movementInput.y, 0.1f, Time.deltaTime);
        animator.SetBool("Run", isRunning && movementInput.magnitude > 0);
    }
}
