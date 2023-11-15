using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator animator;
    private GameObject Location1;
    private GameObject Location2;

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void ChangeLocation(GameObject CurrentLocation, GameObject LocationToChangeTo)
    {
        Location1 = CurrentLocation;
        Location2 = LocationToChangeTo;
        
        StartCoroutine(FadeOutThenIn());
    }

    public void FadeOutAndInMidScene()
    {
        StartCoroutine(FadeOutThenIn());
    }

    private IEnumerator FadeOutThenIn()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);
        //Allows location visuals to be switched if needed to
        if(Location1 && Location2)
        {
            //Hides current location and makes second location visable
            Location1.SetActive(false);
            Location2.SetActive(true);
            //Empties variables to prevent the if statement from acting again if not needed
            Location1 = null;
            Location2 = null;
        }
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);
    }
}
