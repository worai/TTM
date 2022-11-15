using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class WeaponProvider : MonoBehaviour
{
  [SerializeField] private AWeapon weapon;
  [SerializeField] private WeaponPickupWeaponEvent pickupEvent;
  [SerializeField] private UnityEvent surpriseEvent;
  [SerializeField] private bool verbose = false;

  private bool playerInTrigger = false;
  private bool weaponPickedUp = false;
  // TODO move this to another class that can be reused by other guys as well
  private bool surpriseEventInvoked = false;

  private void Start()
  {
    if (pickupEvent == null) pickupEvent = new WeaponPickupWeaponEvent();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    string[] maskNames = { "EventDetector" };
    LayerMask mask = LayerMask.GetMask(maskNames);
    int collisionObjectLayer = 1 << collision.gameObject.layer; 
    //if(collision.TryGetComponent(out PlayerController controller))

    if (collisionObjectLayer == mask)
    {
      if(verbose) Debug.Log($"player should notice provider of {weapon}");
      playerInTrigger = true;
      if(!surpriseEventInvoked)
      {
        surpriseEvent.Invoke();
        surpriseEventInvoked = true;
      }
    }
  }


  //private void OnTriggerStay2D(Collider2D collision)
  //{
  //  //Should the 
  //  if (collision.TryGetComponent(out PlayerController controller))
  //    Debug.Log($"player should be able to obtain weapon of type {weapon.ToString()}");
  //}


  private void OnTriggerExit2D(Collider2D collision)
  {
    string[] maskNames = { "EventDetector" };
    LayerMask mask = LayerMask.GetMask(maskNames);
    int collisionObjectLayer = 1 << collision.gameObject.layer;
    //if (collision.TryGetComponent(out PlayerController controller))

    if (collisionObjectLayer == mask)
    {
      playerInTrigger = false;
      if(verbose) Debug.Log("Player exited area.");
    }
  }

  public void TryGetWeapon()
  {
    if (playerInTrigger && !weaponPickedUp)
    {
      //TODO play animation of whatever corpse crumbling or something like that.
      //  or something more unexpected, like holy light?
      pickupEvent.Invoke(weapon);
      Debug.Log("persumably picked up weapon " + weapon);
      weaponPickedUp = true;
    }
    else
      Debug.Log("Player was unable to pick up weapon of type " + weapon);
  }

}
