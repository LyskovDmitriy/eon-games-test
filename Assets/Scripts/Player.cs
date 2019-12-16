using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string SpeedXName = "SpeedX";
    private const string SpeedZName = "SpeedZ";

    [SerializeField] private Animator animator = default;
    [SerializeField] private float acceleration = default;
    [SerializeField] private float minSpeed = default;
    [SerializeField] private float maxSpeed = default;

    private float currentSpeed;

    private void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementZ = Input.GetAxisRaw("Vertical");

        if (!Mathf.Approximately(movementX, 0.0f) || !Mathf.Approximately(movementZ, 0.0f))
        {
            currentSpeed = Mathf.Max(currentSpeed, minSpeed);
            currentSpeed = Mathf.Min(maxSpeed, currentSpeed + acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0.0f;
        }

        animator.SetFloat(SpeedZName, Input.GetAxisRaw("Vertical") * currentSpeed);
        animator.SetFloat(SpeedXName, Input.GetAxisRaw("Horizontal") * currentSpeed);
    }
}
