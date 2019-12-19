using UnityEngine;

public class Player : MonoBehaviour
{
    private const string SpeedXName = "SpeedX";
    private const string SpeedZName = "SpeedZ";

    [SerializeField] private Animator animator = default;
    [SerializeField] private Rigidbody body = default;
    [SerializeField] private Sword sword = default;
    [Header("Movement")]
    [SerializeField] private float acceleration = default;
    [SerializeField] private float minSpeed = default;
    [SerializeField] private float maxSpeed = default;
    [SerializeField] private float rotationSpeed = default;


    public float CurrentSpeed { get; private set; }


    private void Awake()
    {
        body.maxAngularVelocity = 0.0f;
    }

    private void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementZ = Input.GetAxisRaw("Vertical");

        if (!Mathf.Approximately(movementX, 0.0f) || !Mathf.Approximately(movementZ, 0.0f))
        {
            CurrentSpeed = Mathf.Max(CurrentSpeed, minSpeed);
            CurrentSpeed = Mathf.Min(maxSpeed, CurrentSpeed + acceleration * Time.deltaTime);
        }
        else
        {
            CurrentSpeed = 0.0f;
        }

        animator.SetFloat(SpeedZName, Input.GetAxisRaw("Vertical") * CurrentSpeed);
        animator.SetFloat(SpeedXName, Input.GetAxisRaw("Horizontal") * CurrentSpeed);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
    }


    public void StartAttack() => sword.SetColliderEnabled(true);

    public void FinishAttack() => sword.SetColliderEnabled(false);
}
