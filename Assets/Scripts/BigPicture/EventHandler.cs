using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// I'm afraid this fucker will cause more problems than is worth the effort... don't like this shit...
/// </summary>
public class EventHandler : MonoBehaviour
{
  public UnityEvent removeEnemies;
  public UnityEvent revivePlayer;
  public UnityEvent resetDeathTimerEvent;

  private void Start()
  {
    if (resetDeathTimerEvent == null) resetDeathTimerEvent = new UnityEvent();
  }


  public void OnPlayerDeath()
  {
    StartCoroutine(HandleDeath(4f));
  }


  private IEnumerator HandleDeath(float time)
  {
    yield return new WaitForSeconds(time);
    removeEnemies.Invoke();
    yield return new WaitForSeconds(time);
    revivePlayer.Invoke();
    yield return new WaitForSeconds(2.6f);
    resetDeathTimerEvent.Invoke();

  }



  private bool runningWait_resetTimer = false;
  private IEnumerator Wait(float time)
  {
    runningWait_resetTimer = true;
    yield return new WaitForSeconds(time);
    runningWait_resetTimer = false;

  }


}
