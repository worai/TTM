using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Director : MonoBehaviour
{


  //[SerializeField] private float lullPeriodSec = 60f;
  //[SerializeField] private float risingTensionPeriodSec = 60f;
  //[SerializeField] private float actionPeriodSec = 120f;

  [SerializeField] private float risingTensionValue = 50f;
  [SerializeField] private float actionTensionValue = 75f;

  [SerializeField] private Transform playerTrans;
  [Space(5)]
  [SerializeField] private float tensionYFactor = 5f;
  [SerializeField] private float tensionTimeFactor = 0.1f;
  [SerializeField] private float rageDecreaseTimeFactor = 1f;


  private float tension = 0f;
  private float rage = 0f;
  private float climaxTension = 0f;

  private float resetTime = 0f;

  /// <summary>
  /// Automatically shifts between its state.
  /// </summary>
  public DirectorMode CurrentMode { get; private set; }

  private void Update()
  {
    tension = Mathf.Max(playerTrans.position.y*tensionYFactor % 100, tension);
    tension += tensionTimeFactor * Time.deltaTime;
    tension %= 100;

    if (tension <= risingTensionValue && climaxTension <= 0f)
    {
      CurrentMode = DirectorMode.Lull;

    }
    else if (tension > risingTensionValue && tension <= actionTensionValue && climaxTension <= 0f)
    {
      CurrentMode = DirectorMode.RisingTension;
    }
    else if(climaxTension <= 0f)
    {
      climaxTension = 100f;
    }
    else
    {
      CurrentMode = DirectorMode.Action;
      float rageDecreaseDiff = rage - rageDecreaseTimeFactor * Time.deltaTime;
      rage = rageDecreaseDiff < 0f ? 0f : rageDecreaseDiff;
      climaxTension -= Time.deltaTime;

    }
    
    //if (Time.time - resetTime < lullPeriodSec)
    //  CurrentMode = DirectorMode.Lull;
    //else if (Time.time - resetTime < lullPeriodSec + risingTensionPeriodSec)
    //  CurrentMode = DirectorMode.RisingTension;
    //else if (Time.time - resetTime < lullPeriodSec + risingTensionPeriodSec + actionPeriodSec)
    //  CurrentMode = DirectorMode.Action;
    //else if (Time.time - resetTime > lullPeriodSec + risingTensionPeriodSec + actionPeriodSec)
    //  resetTime = Time.time;
  }

  //TODO implement rage mechanic
  public enum DirectorMode
  {
    Lull,
    RisingTension,
    Action,
    Rage
  }


  /// <summary>
  /// 
  /// </summary>
  /// <param name="time">0 to inf (Absolutised)</param>
  public void MoveToCalm(float time)
  {
    if (CurrentMode == DirectorMode.Lull || CurrentMode == DirectorMode.RisingTension)
      resetTime += Mathf.Abs( time);
    else resetTime -= time;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="time">0 to inf (Absolutised)</param>
  public void MoveToAction(float time)
  {
    if (CurrentMode == DirectorMode.Lull || CurrentMode == DirectorMode.RisingTension)
      resetTime -= Mathf.Abs(time);
    else resetTime += time;
  }

  public void IncreaseRage(float value)
  {
    rage += value;
  }

  public void DecreaseActionTension(float value)
  {
    if (CurrentMode == DirectorMode.Action)
      climaxTension -= value;
  }


}
