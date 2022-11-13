using System;
using System.Collections;
using UnityEngine;

public class HoleEventHandler : MonoBehaviour
{

  //TODO should this be a global setting perhaps??
  [SerializeField] private float precariousTimeLimit = 2f;
  [SerializeField] private bool verbose = false;

  private MapNodeState nodeInfos;
  private PlayerController controller;
  private bool _inTriggerArea;

  private void Start()
  {
    nodeInfos = transform.parent.transform.parent.GetComponent<MapNodeState>();
    Debug.Log("MapNodeState " + nodeInfos.ToString());

    controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //controller.onStartedBalancingCoords.AddListener(ResetFallTimerCoords);
    controller.onStartedBalancing.AddListener(ResetFallTimer);
    controller.onStoppedBalancing.AddListener(RestartFallTimer);
  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (verbose) Debug.Log("Collision in hole: " + collision.name);
    HandlePlayerFallingDetectorEnteringHole(collision);
    HandlePrecariousStateEnteringHole(collision);
    _inTriggerArea = true;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (verbose) Debug.Log("Collision that left hole: " + collision.name);
    _inTriggerArea = false;
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
  /// TODO make other things capable of falling?
  /// </summary>
  /// <param name=""></param>
  /// <returns></returns>
  private bool runningFallTimer = false;
  private IEnumerator FallTimerCoroutine;

  private IEnumerator FallTimer(PlayerController controller, float timeGiven)
  {
    Debug.Log("FallTimer started");
    runningFallTimer = true;
    yield return new WaitForSeconds(timeGiven);
    //controller.Falling = true;
    controller.Fall();
    runningFallTimer = false;
    Debug.Log("FallTimer Started");
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


  private void ResetFallTimerCoords(ColNum arg0, RowNum arg1)
  {
    throw new NotImplementedException();
  }

  private void ResetFallTimer()
  {
    if (FallTimerCoroutine == null) return;
    if (!_inTriggerArea) return;
    Debug.Log("Reset fall timer; stopping coroutine");
    StopCoroutine(FallTimerCoroutine);
  }

  private void RestartFallTimer()
  {
    if (!_inTriggerArea)
      return;
    else
    {
      Debug.Log("Restart timer");
      FallTimerCoroutine = FallTimer(controller, precariousTimeLimit);
      StartCoroutine(FallTimerCoroutine);
    }
  }

  #endregion

}
