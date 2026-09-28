using UnityEngine;

public class StartMechanic : MonoBehaviour
{
    public GameObject cueObj, mechanicObj;

    public void RevealMechanicCue()
    {
        if (cueObj.activeSelf)
        {
            mechanicObj.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            cueObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cueObj.SetActive(false);
        }
    }
}
