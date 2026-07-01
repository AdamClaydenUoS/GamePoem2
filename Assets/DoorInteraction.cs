using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public int doorNum;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Constants.isAtDoor = true;
            Constants.doorNum = doorNum;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Constants.isAtDoor = false;
        }
    }
}
