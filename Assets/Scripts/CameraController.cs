using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offsetFromPivot = default;
    [SerializeField] private Vector3 targetPivotOffset = default;
    [SerializeField] private Vector3 defaultEulerAngles = default;
    [SerializeField] private float rotationSpeed = default;
    [SerializeField] private float minRotationX = default;
    [SerializeField] private float maxRotationX = default;

    private Transform target;

    private Vector3 relativeRotation;
    private Quaternion defaultRotation;

    private void Awake()
    {
        defaultRotation = Quaternion.Euler(defaultEulerAngles);
    }


    private void Update()
    {
        relativeRotation += Vector3.right * Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        relativeRotation.x = Mathf.Clamp(relativeRotation.x, minRotationX, maxRotationX);
    }


    private void LateUpdate()
    {
        Matrix4x4 transformMatrix = Matrix4x4.TRS(target.position + targetPivotOffset, target.rotation * Quaternion.Euler(relativeRotation), target.lossyScale);

        transform.position = transformMatrix.MultiplyPoint3x4(offsetFromPivot);
        transform.rotation = target.transform.rotation * Quaternion.Euler(relativeRotation) * defaultRotation;
    }

    public void StartFollowing(Transform target)
    {
        this.target = target;
    }
}
