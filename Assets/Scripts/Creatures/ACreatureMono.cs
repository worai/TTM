using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACreatureMono : MonoBehaviour
{
  protected GameObject playerGo;

  public enum CreatureActionState
  {
    Idle = 0,
    Pursuing = 1,
    Attacking,
    Raging,
    Ready,
    Evading,
    Staggered
  }
}
