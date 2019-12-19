using UnityEngine;

public class Player : MonoBehaviour
{
    private const string SpeedXName = "SpeedX";
    private const string SpeedZName = "SpeedZ";
    private const string GroundedKey = "IsGrounded";

    [SerializeField] private Animator animator = default;
    [SerializeField] private Rigidbody body = default;
    [SerializeField] private Sword sword = default;
    [Header("Movement")]
    [SerializeField] private float acceleration = default;
    [SerializeField] private float minSpeed = default;
    [SerializeField] private float maxSpeed = default;
    [SerializeField] private float rotationSpeed = default;
    [SerializeField] private float stoppingSpeed = default;
    [Space]
    [SerializeField] private float groundCheckRadius = default;
    [SerializeField] private LayerMask groundCheckLayer = default;


    public float CurrentSpeedX { get; private set; }
    public float CurrentSpeedZ { get; private set; }


    private void Awake()
    {
        body.maxAngularVelocity = 0.0f;
    }

    private void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementZ = Input.GetAxisRaw("Vertical");
 
        if (!Mathf.Approximately(movementX, 0.0f))
        {
            float movementSign = Mathf.Sign(movementX);

            if (Mathf.Approximately(movementSign, Mathf.Sign(CurrentSpeedX)))
            {
                CurrentSpeedX = Mathf.Max(Mathf.Abs(CurrentSpeedX), minSpeed) * movementSign;
                CurrentSpeedX = Mathf.Min(maxSpeed, Mathf.Abs(CurrentSpeedX + movementSign * acceleration * Time.deltaTime)) * movementSign;
            }
            else
            {
                CurrentSpeedX += movementSign * stoppingSpeed * Time.deltaTime;
            }
        }
        else
        {
            CurrentSpeedX = Mathf.MoveTowards(CurrentSpeedX, 0.0f, Time.deltaTime * stoppingSpeed);
        }

        if (!Mathf.Approximately(movementZ, 0.0f))
        {
            float movementSign = Mathf.Sign(movementZ);

            if (Mathf.Approximately(movementSign, Mathf.Sign(CurrentSpeedZ)))
            {
                CurrentSpeedZ = Mathf.Max(Mathf.Abs(CurrentSpeedZ), minSpeed) * movementSign;
                CurrentSpeedZ = Mathf.Min(maxSpeed, Mathf.Abs(CurrentSpeedZ + movementSign * acceleration * Time.deltaTime)) * movementSign;
            }
            else
            {
                CurrentSpeedZ += movementSign * stoppingSpeed * Time.deltaTime;
            }
        }
        else
        {
            CurrentSpeedZ = Mathf.MoveTowards(CurrentSpeedZ, 0.0f, Time.deltaTime * stoppingSpeed);
        }

        animator.SetFloat(SpeedZName, CurrentSpeedZ);
        animator.SetFloat(SpeedXName, CurrentSpeedX);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        animator.SetBool(GroundedKey, Physics.OverlapSphere(transform.position, groundCheckRadius, groundCheckLayer).Length != 0);
    }


    public void StartAttack() => sword.SetColliderEnabled(true);

    public void FinishAttack() => sword.SetColliderEnabled(false);
}
