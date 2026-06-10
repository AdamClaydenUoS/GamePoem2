using UnityEngine;
public class HeadBob : MonoBehaviour
{
    public float bobSpeed = 0.18f;
    public float bobAmount = 0.2f;
    private float defaultYPos = 0;
    private float timer = 0;
    void Start()
    {
        defaultYPos = transform.localPosition.y;
    }
    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange *= totalAxes;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultYPos + translateChange, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, defaultYPos, transform.localPosition.z);
        }
    }
}