using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
// current bug: sometimes the white moving box doesn't reset position properly and starts too high up. Replicate by spamming debug button
public class TwitchMechanic : MonoBehaviour
{
    Vector3 startPosition, targetPos;
    bool lockInteraction;
    public RectTransform spawnPos, targetObj, endPosition;
    public Image target, movingRect;
    public RawImage whiteCircle, circleBorder, ghostHands;
    float maxScale = 30f, thresholdScale = 10f; // 5 is min
    float duration = 5f, distanceThreshold = 20f;
    Vector3 borderScale, defaultScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockInteraction = false;
        defaultScale = borderScale = circleBorder.rectTransform.localScale;
        startPosition = spawnPos.localPosition;
        targetPos = targetObj.localPosition;
        movingRect.rectTransform.localPosition = startPosition;
        movingRect.gameObject.SetActive(false);
        circleBorder.gameObject.SetActive(false);
        //Invoke("BeginTest",3f);
    }

    void BeginTest()
    {
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MakeHandsAppear()
    {
        ghostHands.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ghostHands.gameObject.SetActive(false);
    }

    public void OnMechanicInteract(InputAction.CallbackContext callbackContext)
    {
        // rectangle approach
        /*
        if (callbackContext.started)
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
        */
        // circle approach
        if (callbackContext.started)
        {
            StopCoroutine(MoveToTarget());
            if(circleBorder.rectTransform.localScale.x <= thresholdScale)
            {
                StartCoroutine(MakeHandsAppear());
                Debug.Log("WOOOOOOOOOOOOOOOOOOOOOOOOOOO");
                ResetMovingRect();
            }else
            {
                Debug.Log("NOOOOOOO!!!!!!!");
                ResetMovingRect();
            }
            lockInteraction = false;
        }
    }

    IEnumerator MoveToTarget()
    {
        // rectangle approach
        /*
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
        */
        // circle approach
        circleBorder.gameObject.SetActive(true);
        float timer = 0f;
        float scaleAdjustor = maxScale;
        while(timer<duration)
        {
            //circleBorder.rectTransform.localScale = Mathf.Lerp(maxScale, thresholdScale, timer / duration);
            scaleAdjustor = Mathf.Lerp(maxScale, 5f, timer / duration);
            borderScale.x = scaleAdjustor;
            borderScale.y = scaleAdjustor;
            borderScale.z = scaleAdjustor;
            circleBorder.rectTransform.localScale = borderScale;
            timer += Time.deltaTime;
            yield return null;
        }
        ResetMovingRect();
    }

    void ResetMovingRect()
    {
        // rectangle approach
        /*
        StopCoroutine(MoveToTarget());
        movingRect.rectTransform.localPosition = startPosition;
        movingRect.gameObject.SetActive(false);
        //Invoke("BeginTest", 3f);
        */
        // circle approach
        StopCoroutine(MoveToTarget());
        circleBorder.rectTransform.localScale = defaultScale;
        circleBorder.gameObject.SetActive(false);
        StartCoroutine(MoveToTarget());
    }

    public void OnDebugPressed(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            if (!lockInteraction)
            {
                lockInteraction = true;
                ResetMovingRect();
                //StartCoroutine(MoveToTarget());
            }
        }
    }
}
