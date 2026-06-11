using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    GameObject mainCamera;
    public GameObject lagPosition, lookAtPosition;
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
        isCatchingUp = false;
        mainCamera = Camera.main.gameObject;
        isWalking = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 direction = transform.forward * _forward;
        moveVec = direction * moveSpeed * Time.deltaTime;

        controller.Move(moveVec);
        
        transform.Rotate(Vector3.up, _rotation * rotSpeed * Time.deltaTime);

        moveDir = moveVec.normalized;
        float dot = Vector3.Dot(transform.forward, moveDir);
        isMovingBackwards = dot < 0f;
        if(isMovingBackwards)
        {
            moveSpeed = slowMoveSpeed;
        }
        else
        {
            moveSpeed = fastMoveSpeed;
        }

        if(isCatchingUp)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, lagPosition.transform.position, lagSpeed * Time.deltaTime);

            Vector3 targetDirection = lookAtPosition.transform.position - mainCamera.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(mainCamera.transform.forward, targetDirection, lagSpeed * Time.deltaTime, 0.0f);
            mainCamera.transform.rotation = Quaternion.LookRotation(newDirection);
        }
        if (Vector3.Distance(mainCamera.transform.position, lagPosition.transform.position) <= 0.001f)
        {
            isCatchingUp = false;
        }
    }

    public void OnMovement(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            isWalking = true;
            StartCoroutine(LagCamera());
            //animator.SetBool("IsWalking", isWalking);
        }
        if(callbackContext.canceled)
        {
            isWalking = false;
            animator.SetTrigger("GoIdle");
        }
        if(isWalking)
        {
            animator.SetTrigger("IsNowWalking");
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
            animator.SetTrigger("GoIdle");
        }
        if (isWalking)
        {
            animator.SetTrigger("IsNowWalking");
        }
        _rotation = callbackContext.ReadValue<float>();
    }

    public IEnumerator LagCamera()
    {
        yield return new WaitForSeconds(0.75f);
        isCatchingUp = true;
    }

}