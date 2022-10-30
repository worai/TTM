using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleEventHandler : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("Collision name in hole: " + collision.name);
    HandlePlayerFallingDetectorEnteringHole(collision);
    HandlePrecariousStateEnteringHole(collision);

  }

  private void OnTriggerExit2D(Collider2D collision)
  {

    HandleFallingDetectorLeavingHoleQuestionmark(collision);
    HandleEndOfPrecariousState(collision);
  }




  #region private methods


  private static PlayerController GetControllerInParentOrNull(Collider2D collision)
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


  private static void HandlePlayerFallingDetectorEnteringHole(Collider2D collision)
  {
    PlayerController controller = GetControllerInParentOrNull(collision);
    if (collision.CompareTag("FallingDetector"))
    {
      if (controller == null)
      {
        Debug.LogError("didn't find controller");
        return;
      }
      Debug.Log("Player detector in hole");
      //collision.TryGetComponent(out PlayerController controller)
      controller.Falling = true;
      controller.Fall();
    }
  }
  private static void HandlePrecariousStateEnteringHole(Collider2D collision)
  {
    if (collision.TryGetComponent(out PlayerController controller))
    {
      controller.Precarious = true;
    }
  }


  /// <summary>
  /// isn't this guy superfluous?
  /// </summary>
  /// <param name="collision"></param>
  [System.Obsolete("Not needed, surely!")]
  private static void HandleFallingDetectorLeavingHoleQuestionmark(Collider2D collision)
  {
    PlayerController controller = GetControllerInParentOrNull(collision);
    if (collision.tag == "FallingDetector")
    {
      Debug.Log("Player detector out of hole");
      controller.Falling = false;
    }
  }

  private void HandleEndOfPrecariousState(Collider2D collision)
  {
    if (collision.TryGetComponent(out PlayerController controller))
    {
      Debug.Log("End of precarious state from hole");
      controller.Precarious = false;
    }
  }

  #endregion

}
