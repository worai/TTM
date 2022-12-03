using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// this guy has too much stuff in it atm.
/// This guy will only show stuff, and never allow the player to interract with the scene.
/// </summary>
public class CutsceneManager : MonoBehaviour
{
  [SerializeField] private SceneBitNTime[] bitntime;

  private bool runningCoroutine = false;
  private bool timerFinished;
  private float startTime;

  IEnumerator Start()
  {
    for (int i = 0; i < bitntime.Length; i++)
    {
      if (bitntime[i].time < 0)
      {
        yield return StartCoroutine(RunBit(i));
        Debug.Log(i);
      }
      else
      {
        float timerTime = Mathf.Abs(bitntime[i].time);
        StartCoroutine(Timer(timerTime));
        yield return StartCoroutine(RunUntilNotReset(i));
      }
    }
    Debug.Log("Done with cutscene");
  }

  IEnumerator RunUntilNotReset(int i)
  {
    while(!timerFinished)
    {
      yield return null;
    }
  }

  IEnumerator RunBit(int i)
  {
    bitntime[i].bit.SetActive(true);
    yield return new WaitForSeconds(bitntime[i].time);
  }


  IEnumerator Timer(float time)
  {
    timerFinished = false;
    startTime = Time.time;
    while(startTime + time > Time.time)
    {
      yield return null;
    }
    timerFinished = true;
  }

  public void PostponeTimer(float seconds)
  {
    startTime += seconds;
  }

}
