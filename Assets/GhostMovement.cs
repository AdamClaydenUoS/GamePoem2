using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class GhostMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f; // 10
    public float verticalSpeed = 6f;
    public float acceleration = 4f; //3
    public float deceleration = 2f; //1

    [Header("Look")]
    public float mouseSensitivity = 2f;
    public Transform cameraHolder;

    private CharacterController controller;
    private Vector3 currentVelocity;
    private float pitch;

    float _forward;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        Vector3 direction = transform.forward * _forward;
        Vector3 velocity = direction.normalized * moveSpeed;

        float smoothing =
            direction.magnitude > 0
            ? acceleration
            : deceleration;

        currentVelocity = Vector3.Lerp(
            currentVelocity,
            velocity,
            smoothing * Time.deltaTime
        );

        controller.Move(currentVelocity * Time.deltaTime);
        //Look();
        //Move();
    }

    void Look()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        cameraHolder.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    public void MoveCharacter(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            _forward = callbackContext.ReadValue<float>();
        }
    }

    /*void Move()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float z = Input.GetAxisRaw("Vertical");

        Vector3 desiredDirection =
            transform.right * x +
            transform.forward * z;

        desiredDirection.Normalize();

        Vector3 targetVelocity = desiredDirection * moveSpeed;

        float smoothing =
            desiredDirection.magnitude > 0
            ? acceleration
            : deceleration;

        currentVelocity = Vector3.Lerp(
            currentVelocity,
            targetVelocity,
            smoothing * Time.deltaTime
        );

        // Vertical ghost movement
        float vertical = 0f;

        if (Input.GetKey(KeyCode.Space))
            vertical += verticalSpeed;

        if (Input.GetKey(KeyCode.LeftControl))
            vertical -= verticalSpeed;

        Vector3 finalVelocity =
            currentVelocity +
            Vector3.up * vertical;

        controller.Move(finalVelocity * Time.deltaTime);
    }*/
}