using System.Collections;
using UnityEngine;

public class HoleEventHandler : MonoBehaviour
{

  //TODO should this be a global setting perhaps??
  [SerializeField] private float precariousTimeLimit = 2f;

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


  private PlayerController GetControllerInParentOrNull(Collider2D collision)
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


  private void HandlePlayerFallingDetectorEnteringHole(Collider2D collision)
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
      //controller.Falling = true;
      controller.Fall();
    }
  }


  /// <summary>
  /// //TODO remove
  /// isn't this guy superfluous?
  /// </summary>
  /// <param name="collision"></param>
  [System.Obsolete("Not needed, surely!")]
  private void HandleFallingDetectorLeavingHoleQuestionmark(Collider2D collision)
  {
    PlayerController controller = GetControllerInParentOrNull(collision);
    if (collision.tag == "FallingDetector")
    {
      Debug.Log("Player detector out of hole");
      //controller.Falling = false;
    }
  }

  /// <summary>
  /// TODO make other things capable of falling?
  /// </summary>
  /// <param name=""></param>
  /// <returns></returns>
  private bool runningFallTimer = false;
  private IEnumerator FallTimerCoroutine;

  private IEnumerator FallTimer(PlayerController controller, float timeGiven)
  {
    runningFallTimer = true;
    yield return new WaitForSeconds(timeGiven);
    //controller.Falling = true;
    controller.Fall();
    runningFallTimer = false;
  }

  private  void HandlePrecariousStateEnteringHole(Collider2D collision)
  {
    Debug.Log("Precarious detector detected");
    if (collision.CompareTag("PrecariousDetector") && 
      collision.transform.parent.TryGetComponent(out PlayerController controller))
    {
      controller.Precarious = true;
      FallTimerCoroutine = FallTimer(controller, precariousTimeLimit);
      if(!runningFallTimer)
      {
        StartCoroutine(FallTimerCoroutine);
      }
    }
  }



  private void HandleEndOfPrecariousState(Collider2D collision)
  {
    if (collision.CompareTag("PrecariousDetector") && 
      collision.transform.parent.TryGetComponent(out PlayerController controller))
    {
      Debug.Log("End of precarious state from hole");
      StopCoroutine(FallTimerCoroutine);
      controller.Precarious = false;
    }
  }

  #endregion

}
