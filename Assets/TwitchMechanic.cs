using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
public class TwitchMechanic : MonoBehaviour
{
    Vector3 startPosition, targetPos;
    public RectTransform spawnPos, targetObj, endPosition;
    public Image target, movingRect;
    float duration = 1f, distanceThreshold = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = spawnPos.localPosition;
        targetPos = targetObj.localPosition;
        movingRect.rectTransform.localPosition = startPosition;
        movingRect.gameObject.SetActive(false);
        //Invoke("BeginTest",3f);
    }

    void BeginTest()
    {
        StartCoroutine(MoveToTarget());
    }

    public void OnMechanicInteract(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            StopCoroutine(MoveToTarget());
            print(Vector3.Distance(movingRect.rectTransform.localPosition, targetPos));
            if (Vector3.Distance(movingRect.rectTransform.localPosition, targetPos) < distanceThreshold)
            {
                Debug.Log("WOOOOOOOOOOOOOOOOOOOOOOOOOOO");
                ResetMovingRect();
            }
            else
            {
                Debug.Log("NOOOOOOO!!!!!!!");
                ResetMovingRect();
            }
        }
    }

    IEnumerator MoveToTarget()
    {
        movingRect.gameObject.SetActive(true);
        float timer = 0f;
        while(timer<duration)
        {
            movingRect.rectTransform.localPosition = Vector3.Lerp(startPosition, endPosition.localPosition, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        //movingRect.rectTransform.localPosition = endPosition.localPosition;
        ResetMovingRect();
    }

    void ResetMovingRect()
    {
        StopCoroutine(MoveToTarget());
        movingRect.rectTransform.localPosition = startPosition;
        movingRect.gameObject.SetActive(false);
        //Invoke("BeginTest", 3f);
    }

    public void OnDebugPressed(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            ResetMovingRect();
            StartCoroutine(MoveToTarget());
        }
    }
}
