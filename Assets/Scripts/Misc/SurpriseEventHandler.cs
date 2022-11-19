using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurpriseEventHandler : MonoBehaviour
{
  [SerializeField] protected UnityEvent surpriseEvent;

  private bool surpriseEventInvoked = false;


  private void OnTriggerEnter2D(Collider2D collision)
  {
    string[] maskNames = { "EventDetector" };
    LayerMask mask = LayerMask.GetMask(maskNames);
    int collisionObjectLayer = 1 << collision.gameObject.layer;
    Debug.Log("Player entered the possibility of surprise");
    if (collisionObjectLayer == mask)
    {
      if (!surpriseEventInvoked)
      {
        surpriseEvent.Invoke();
        surpriseEventInvoked = true;
      }
    }
  }

  protected void _onTriggerEnter2D(Collider2D collision)
  {
    string[] maskNames = { "EventDetector" };
    LayerMask mask = LayerMask.GetMask(maskNames);
    int collisionObjectLayer = 1 << collision.gameObject.layer;
    Debug.Log("Player entered the possibility of surprise");
    if (collisionObjectLayer == mask)
    {
      if (!surpriseEventInvoked)
      {
        surpriseEvent.Invoke();
        surpriseEventInvoked = true;
      }
    }
  }

}
