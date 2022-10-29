using Poets.MathUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACreatureMono : MonoBehaviour
{

  /// <summary>
  /// can't be reached in the editor!
  /// how to handle this kinda thing?
  /// </summary>
  public float abstractFloat;

  protected GameObject playerGo;



  public enum CreatureActionState
  {
    Idle = 0,
    Pursuing = 1,
    Attacking,
    Raging
  }
}
