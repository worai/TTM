using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{

  [SerializeField] CreatureData data;

  public UnityEvent playerDeathEvent;

  private void Start()
  {
    if (playerDeathEvent == null) playerDeathEvent = new UnityEvent();
  }

  private void Update()
  {
    //if (data.IsDead) playerDeathEvent.Invoke();
  }

}
