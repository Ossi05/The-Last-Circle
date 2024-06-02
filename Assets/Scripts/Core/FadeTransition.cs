using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    public static FadeTransition Instance;

    const string FADE_IN = "FadeIn";
    const string FADE_OUT = "FadeOut";

    [SerializeField] Animator animator;
    WaitForSecondsRealtime fadeInTime = new WaitForSecondsRealtime(1f);

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    public void FadeInOnSceneChange()
    {
        FadeIn();
        StartCoroutine(WaitToFadeInOnSceneChange());
    }

    IEnumerator WaitToFadeInOnSceneChange()
    {
        yield return fadeInTime;
        Loader.LoaderCallback();       
    }

    public void FadeOut()
    {   
        animator.SetTrigger(FADE_OUT);
    }

    void FadeIn()
    {
        animator.SetTrigger(FADE_IN);
    }

    public async Task FadeInToOut()
    {
        FadeIn();
        await Task.Delay(1000);
        FadeOut();
    }



}
