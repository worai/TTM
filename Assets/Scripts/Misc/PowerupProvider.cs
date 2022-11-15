using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Even this more barebones type o class could possibly be abstracted
/// even further so that WeaponProvider and PowerupProvider got lumped together.
/// Best reason why I wouldn't wanna do that is because the Weapon class family is
/// already pretty ugly, with its chain/web of inheritance.
/// If I wanna come up with some more type of pickup thing, then I might consider it.
/// Tho it would then be best to perhaps rethink the Weapon family.
/// </summary>
public class PowerupProvider : MonoBehaviour
{
  [SerializeField] private APowerup powerup;
  [SerializeField] private PowerupPickupWeaponEvent pickupEvent;
  [SerializeField] private bool verbose = false;

  private bool playerInTrigger = false;
  private bool powerupPickedUp = false;



  private void OnTriggerEnter2D(Collider2D collision)
  {
    string[] maskNames = { "EventDetector" };
    LayerMask mask = LayerMask.GetMask(maskNames);
    int collisionObjectLayer = 1 << collision.gameObject.layer;

    if (collisionObjectLayer == mask)
    {
      if (verbose) Debug.Log($"player should notice provider of {powerup}");
      playerInTrigger = true;
    }
  }



  private void OnTriggerExit2D(Collider2D collision)
  {
    string[] maskNames = { "EventDetector" };
    LayerMask mask = LayerMask.GetMask(maskNames);
    int collisionObjectLayer = 1 << collision.gameObject.layer;

    if (collisionObjectLayer == mask)
    {
      playerInTrigger = false;
      if (verbose) Debug.Log("Player exited area.");
    }
  }

  public void TryGetWeapon()
  {
    if (playerInTrigger && !powerupPickedUp)
    {
      //TODO play animation of whatever corpse crumbling or something like that.
      //  or something more unexpected, like holy light?
      pickupEvent.Invoke(powerup);
      if(verbose) Debug.Log("persumably picked up weapon " + powerup);
      powerupPickedUp = true;
    }
    else
      if (verbose) Debug.Log("Player was unable to pick up weapon of type " + powerup);
  }

}

