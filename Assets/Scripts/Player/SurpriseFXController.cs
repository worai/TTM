using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseFXController : MonoBehaviour
{
  [SerializeField] private SpriteRenderer myRenderer;
  [SerializeField] private float timePeriod = 0.1f;
  [SerializeField] private int repeats = 2;

  public void PlaySurpriseAnimation()
  {
    if(!runningSurpriseCoroutine)
      StartCoroutine(SurpriseCoroutine(timePeriod));
  }

  private bool runningSurpriseCoroutine = false;
  private IEnumerator SurpriseCoroutine(float reallyHalfPeriod)
  {
    runningSurpriseCoroutine = true;
    for (int i = 0; i < repeats * 2; i++)
    {
      myRenderer.enabled = !myRenderer.enabled;
      yield return new WaitForSeconds(timePeriod);
    }
    runningSurpriseCoroutine = false;
  }

}
