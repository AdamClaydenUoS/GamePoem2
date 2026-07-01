using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public GameObject mainCam, cameraTwo;
    public GameObject canvasObj;
    bool startNewGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startNewGame = false;
    }

    public void NewGame()
    {
        if (!startNewGame)
        {
            cameraTwo.SetActive(false);
            mainCam.GetComponent<Camera>().enabled = true;
            canvasObj.SetActive(false);
        }
    }
}
