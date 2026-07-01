using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class NewPlayerMovement : MonoBehaviour
{
    GameObject mainCamera;
    public GameObject lagPosition, lookAtPosition;
    public GameObject outsideBedroom, insideBedroom;
    public float lagSpeed = 2f;
    bool isCatchingUp;
    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float fastMoveSpeed;
    public float slowMoveSpeed = 1f;
    public float rotSpeed;
    float _forward;
    float _rotation;

    private CharacterController controller;
    public Animator animator;
    private Vector3 velocity;
    Vector3 moveVec;
    Vector3 rotVec;
    Vector3 moveDir;
    bool isWalking, isMovingBackwards;

    void Start()
    {
        fastMoveSpeed = moveSpeed;
        mainCamera = Camera.main.gameObject;
        isWalking = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log(transform.position);
        Vector3 direction = transform.forward * _forward;
        moveVec = direction * moveSpeed * Time.deltaTime;

        controller.Move(moveVec);

        transform.Rotate(Vector3.up, _rotation * rotSpeed * Time.deltaTime);

        moveDir = moveVec.normalized;
        float dot = Vector3.Dot(transform.forward, moveDir);
        isMovingBackwards = dot < 0f;
        if (isMovingBackwards)
        {
            moveSpeed = slowMoveSpeed;
        }
        else
        {
            moveSpeed = fastMoveSpeed;
        }
    }

    public void OnMoveThroughDoor(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("hello?");
        if(callbackContext.started && Constants.isAtDoor)
        {
            Debug.Log(Constants.doorNum);
            if(Constants.doorNum == 0)
            {
                controller.enabled = false;
                transform.position = insideBedroom.transform.position;
                controller.enabled = true;
            }
            else if(Constants.doorNum == 1)
            {
                controller.enabled = false;
                transform.position = outsideBedroom.transform.position;
                controller.enabled = true;
            }
            Constants.doorNum = -1;
        }
    }

    public void OnMovement(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            isWalking = true;
           
            //animator.SetBool("IsWalking", isWalking);
        }
        if (callbackContext.canceled)
        {
            isWalking = false;
            
        }
        if (isWalking)
        {
            
        }
        _forward = callbackContext.ReadValue<float>();
    }

    public void OnRotate(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            isWalking = true;
            //animator.SetBool("IsWalking", isWalking);

        }
        if (callbackContext.canceled)
        {
            isWalking = false;

        }
        if (isWalking)
        {

        }
        _rotation = callbackContext.ReadValue<float>();
    }
}