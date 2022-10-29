using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAccBuilder : MonoBehaviour
{
  [Tooltip("Acc, Hor, Vrt, Crs, Bld")]
  [SerializeField] private GameObject[] mapElementTemplates;
  [Tooltip("e.g. from 220 to 230")] [Range(0, 10f)] [SerializeField] private float randomStateRange = 10f;
  [SerializeField] private Transform mapContainerTrans;


  [SerializeField] bool deactivateTemplates = true;

  private void Start()
  {
    //set any active templates inactive
    if (deactivateTemplates)
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        transform.GetChild(i).gameObject.SetActive(false);
      }
    }
  }


  internal void BuildNode(int col, int row, float mapElementSideSize, byte state)
  {
    List<GameObject> mapElementList = new List<GameObject>();
    foreach (var mapElem in mapElementTemplates)
    {
      GameObject newGO = GameObject.Instantiate(mapElem);

      mapElementList.Add(newGO);
      newGO.SetActive(true);
      if (!newGO.TryGetComponent<MapNodeState>(out var _))
      {
        MapNodeState mapNodeState = newGO.AddComponent<MapNodeState>();
        //TODO providing level here is probably not correct?
        if (mapNodeState.Instantiate(state: state, level: 0))
        {
          //GetComponent<StateSpriteController>().Instantiate(mapNodeState);
          //try doing nothing
          //the state sprite controller will attempt setting the MapNodeState in itself by itself
        }
        else
          throw new NotImplementedException("Don't know yet how to handle a MapNodeState that has already been instantiated!");
      }
      //mapElem.transform.parent = this.gameObject.transform; //Incorrect way of setting parent, seemingly
      newGO.transform.SetParent(mapContainerTrans);
    }
    mapElementList[0].transform.position = new Vector3(
      mapElementSideSize * 2 * col,
      mapElementSideSize * 2 * row
      );
    mapElementList[1].transform.position = new Vector3(
      mapElementSideSize * 2 * col,
      mapElementSideSize * 2 * row - mapElementSideSize
      );
    mapElementList[2].transform.position = new Vector3(
      mapElementSideSize * 2 * col - mapElementSideSize,
      mapElementSideSize * 2 * row
      );
    mapElementList[3].transform.position = new Vector3(
      mapElementSideSize * 2 * col - mapElementSideSize,
      mapElementSideSize * 2 * row - mapElementSideSize
      );
    mapElementList[4].transform.position = new Vector3(
      mapElementSideSize * 2 * col,
      mapElementSideSize * 2 * row + +mapElementSideSize / 2
      );

  }
}
