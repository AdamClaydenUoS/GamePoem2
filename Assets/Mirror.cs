using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform playerCamera;
    public Camera mirrorCamera;

    void LateUpdate()
    {
        Vector3 localPos =
            transform.InverseTransformPoint(playerCamera.position);

        localPos.x *= -1;

        mirrorCamera.transform.position =
            transform.TransformPoint(localPos);

        Vector3 localForward =
            transform.InverseTransformDirection(playerCamera.forward);

        localForward.x *= -1;

        mirrorCamera.transform.rotation =
            Quaternion.LookRotation(
                transform.TransformDirection(localForward),
                Vector3.up
            );
    }
}