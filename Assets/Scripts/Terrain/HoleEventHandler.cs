using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleEventHandler : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("Collision name in hole: " + collision.name);
    PlayerController controller = GetControllerOrNull(collision);
    if (collision.tag == "FallingDetector")
    {
      if (controller == null)
      {
        Debug.LogError("didn't find controller");
        return;
      }
      Debug.Log("Player detector in hole");
      //collision.TryGetComponent(out PlayerController controller)
      controller.Falling = true;
    }
  }


  private void OnTriggerExit2D(Collider2D collision)
  {
    PlayerController controller = GetControllerOrNull(collision);
    if (collision.tag == "FallingDetector")
    {
      Debug.Log("Player detector out of hole");
      controller.Falling = false;
    }
  }



  private static PlayerController GetControllerOrNull(Collider2D collision)
  {
    PlayerController controller = null;
    try
    {
      controller = collision.transform.parent.GetComponent<PlayerController>();
    }
    catch (System.Exception)
    {
      //nothing can be found on this this baby, so just leave it.
    }

    return controller;
  }



}
