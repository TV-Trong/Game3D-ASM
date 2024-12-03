using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float cameraLockSpeed = 5f;
    public LockOnTarget lockTarget;
    public vThirdPersonCamera vThirdPersonCamera;
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(2))
        {
            vThirdPersonCamera.lockCamera = lockTarget.lockOn;
        }

        if (lockTarget.currentTarget != null && lockTarget.lockOn)
        {

            vThirdPersonCamera.lockCamera = lockTarget.lockOn;
            // Calculate the direction to the target
            Vector3 directionToTarget = (lockTarget.currentTarget.position - cameraTransform.position).normalized;

            // Calculate the desired camera rotation
            Quaternion cameraRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, directionToTarget.y + .2f, directionToTarget.z));

            // Smoothly rotate the camera
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraRotation, Time.deltaTime * cameraLockSpeed);
        }
    }
    public void SetLockCamera(bool isTrue)
    {
        vThirdPersonCamera.lockCamera = isTrue;
    }
}
