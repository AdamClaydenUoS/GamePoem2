using UnityEngine;

public class OpeningSegment : MonoBehaviour
{
    public Animator _blackBarAnimations, woman, man;
    public string[] dialoguePieces = new string[7];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        woman.SetTrigger("Cry");
        man.SetTrigger("Cry");
        Invoke("AnimateBars", 2.0f);
    }

    void AnimateBars()
    {
        _blackBarAnimations.SetTrigger("MoveBars");
    }
}
