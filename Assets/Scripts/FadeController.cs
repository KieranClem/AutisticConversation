using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButton(0))
        {
            FadeIn();
        }

        if(Input.GetMouseButton(1))
        {
            FadeOut();
        }*/
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
