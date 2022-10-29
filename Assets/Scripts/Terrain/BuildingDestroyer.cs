using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyer : MonoBehaviour
{
  //ok if exposed, because it exposes the info about this thing variable
  [Tooltip("Automatically assigned")] [SerializeField] private MapNodeState state;
  [Tooltip("Automatically assigned")] [SerializeField] private MapNodeThresholdManager threshManager;

  [Tooltip("Hor, Vrt")] [SerializeField] private GameObject[] RubbleTemplates;

  public bool IsDestroyed { get; private set; }



  private void Start()
  {
    TryUpdateDestruction();
  }

  private void Update()
  {

    TryUpdateDestruction();
  }

  private void TryUpdateDestruction()
  {
    //try to set state, if we can't then don't bother... heh
    if (state == null)
      state = GetComponent<MapNodeState>();
    if (state == null) return;

    //trying to set statemanager, if not set.
    if (threshManager == null)
      threshManager = GetComponentInParent<MapNodeThresholdManager>();
    if (threshManager == null) return;

    byte[] thresholds = threshManager.GetThresholdsForLevel(state.Level);
    if(state.Health < thresholds[thresholds.Length-3])
    {
      //chances of destruction?
      //destroy for certain for now
      MakeDestroyedOrAnimateDestruction();
    }
    else if(state.Health < thresholds[thresholds.Length - 2]) 
    {
      //certain destruction
      MakeDestroyedOrAnimateDestruction();
    }
    // for index
    //    thresholds[thresholds.Length - 2
    // the node should be destroyed entirely


    //byte thing = MapNodeStateManager.Instance.Thresholds[0];
  }

  private void MakeDestroyedOrAnimateDestruction()
  {
    //TODO animate destruction

    int randDir = UnityEngine.Random.Range(0, 4);
    //randDir = 3;

    switch (randDir)
    {
      case 0:
        RightDestruction();
        break;
      case 1:
        UpDestruction();
        break;
      case 2:
        LeftDestruction();
        break;
      case 3:
        DownDestruction();
        break;
      default:
        Destroy(this.gameObject);
        break;
    } 
  }

  private void DownDestruction()
  {
    VerticalDestruction(-15);
  }

  private void UpDestruction()
  {
    VerticalDestruction(5);
  }

  private void RightDestruction()
  {
    HorizontalDestruction(10);
  }

  private void LeftDestruction()
  {
    HorizontalDestruction(-10);
  }

  private void VerticalDestruction(float vrtShift)
  {
    GameObject newGO = Instantiate(RubbleTemplates[1]);
    Transform _trans = transform;
    newGO.transform.position = new Vector3(0, vrtShift) + _trans.position;
    newGO.transform.SetParent(_trans.parent);
    newGO.SetActive(true);
    Destroy(this.gameObject);
  }

  private void HorizontalDestruction(float horShift)
  {
    GameObject newGO = Instantiate(RubbleTemplates[0]);
    Transform _trans = transform;
    newGO.transform.position = new Vector3(horShift, -5f) + _trans.position;
    newGO.transform.SetParent(_trans.parent);
    newGO.SetActive(true);
    Destroy(this.gameObject);
  }
}
