using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]  public Rigidbody rb { get;private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerHealthSystem health;

    [Header("Movement")]
    [SerializeField]private float movementSpeed = 5f;  
    [SerializeField] private float jumpForce = 5f;
    [SerializeField]  private float rotationSpeed = 10f;


    [SerializeField] private float stunDuration = 0.2f;
    [SerializeField] private float teleportOffset = 1f;

    private Coroutine stunRoutine;

    private GameManager gm;
    private Vector2 input;
    private Vector3 moveDirection;
    public Vector3 lastGroundedPos { get; private set; }

    private void OnValidate()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();


    }
    private void Start()
    {
        gm = GameManager.Instance;
        StartCoroutine(KnockbackRoutine(Vector3.zero, 1f));
    }

    private void Update()
    {
        ReadInput();
        HandleJump();
        SaveGroundedPosition();
    }

    private void FixedUpdate()
    {
        if (gm.IsStunned)
        {
            rb.angularVelocity = Vector3.zero;
        }

        HandleRotation();
        HandleAnimition();
        HandleMovement();
    }

    private void ReadInput()
    {
        if (gm.IsStunned)
            return;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * input.y + right * input.x).normalized;

     
    }

    private void HandleMovement()
    {
        if (gm.IsStunned)
        {
            return;
        }


        Vector3 targetVelocity = moveDirection * movementSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void HandleRotation()
    {
        if (moveDirection.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(smoothedRotation);

    }
    private void HandleJump()
    {


        if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }



    private void HandleAnimition()
    {
        animator.SetFloat("horizonalVelocity", Mathf.Abs(moveDirection.magnitude));
        animator.SetFloat("verticalVelocity", rb.linearVelocity.y);
        animator.SetBool("isGrounded", groundCheck.IsGrounded);

    }
    public void SaveGroundedPosition()
    {
        if (groundCheck.IsGrounded && groundCheck.savePosition)
        {
            lastGroundedPos = rb.position - transform.forward * teleportOffset;
        }
    }

    public void ResetToLastGroundedPosition()
    {
        rb.position = lastGroundedPos;
        rb.linearVelocity = Vector3.zero;
    }

    public void ApplyKnockback(Vector3 force)
    {
        StopCoroutine(nameof(KnockbackRoutine));
        StartCoroutine(KnockbackRoutine(force, stunDuration));
    }

    private IEnumerator KnockbackRoutine(Vector3 force,float stunDuration)
    {
        gm.IsStunned = true;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(force, ForceMode.VelocityChange);

        yield return new WaitForSeconds(stunDuration);

        gm.IsStunned = false;


    }

}
