using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.1f, 0);
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded { get; private set; }
    public bool savePosition { get; private set; }

    private readonly Collider[] groundHits = new Collider[4];
    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }


    private void CheckGround()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position + groundCheckOffset,groundCheckRadius,groundHits,groundLayer);


        IsGrounded = hitCount > 0;
        savePosition = false;

        if (!IsGrounded)
            return;

        for (int i = 0; i < hitCount; i++)
        {
            if (groundHits[i].CompareTag("Ground"))
            {
                savePosition = true;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }
    
    }
