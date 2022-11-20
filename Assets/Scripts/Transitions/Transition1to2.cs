using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition1to2 : MonoBehaviour
{
  [SerializeField] private Animator myAnimator;
  [SerializeField] private Image myImage;
  [SerializeField] private string animationName = "FallingCharacter1to2";

  [SerializeField] private float waitFadeIn = 1f;
  [SerializeField] private float waitTime1 = 2f;
  [SerializeField] private float waitTime2 = 2f;
  [SerializeField] private float waitFadeOut = 1f;


  /// <summary>
  /// Level moving from one to another is handled in the transition...
  /// </summary>
  /// <returns></returns>
  IEnumerator Start()
  {
    myImage = GetComponent<Image>();

    //https://answers.unity.com/questions/441116/two-coroutines-in-start-must-exec-consecutively.html
    yield return StartCoroutine(FadeIn());
    yield return StartCoroutine(Animate());
    yield return StartCoroutine(FadeOut()); //needed, not just nice to have
    if (LevelInfos.Level.HasValue)
      LevelInfos.Level++;
    else
      LevelInfos.Level = 2;

    SceneLoader.Load(SceneLoader.Scene.SampleScene); //TODO well, some other scene needs to be loaded
  }

  private IEnumerator FadeIn()
  {
    float startTime = Time.time;
    float t = 0;
    while (Time.time - startTime < waitFadeIn)
    {
      yield return new WaitForEndOfFrame();
      t = (Time.time - startTime) / waitFadeIn;
      myImage.color = Color.Lerp(Color.black, Color.white, t);
    }

  }

  private IEnumerator FadeOut()
  {
    float startTime = Time.time;
    float t = 0;
    while (Time.time - startTime < waitFadeOut)
    {
      yield return new WaitForEndOfFrame();
      t = (Time.time - startTime) / waitFadeOut;
      myImage.color = Color.Lerp(Color.black, Color.white, 1 - t);
    }
  }


  private IEnumerator Animate()
  {
    //wait before start
    yield return new WaitForSeconds(waitTime1);

    //run animation n wait
    myAnimator.gameObject.SetActive(true);
    myAnimator.Play(animationName);
    float animationLength = myAnimator.GetCurrentAnimatorStateInfo(0).length;
    yield return new WaitForSeconds(animationLength);

    //stop animating, switching off the animator GO and wait some time;
    myAnimator.gameObject.SetActive(false);
    yield return new WaitForSeconds(waitTime2);

  }
}
