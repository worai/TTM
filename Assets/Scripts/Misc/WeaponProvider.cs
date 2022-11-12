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

  private bool playerInTrigger = false;
  private bool weaponPickedUp = false;
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
      Debug.Log($"player should notice provider of {weapon}");
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
      Debug.Log("Player exited area.");
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
