using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapContainerController : MonoBehaviour
{
  public void RemoveMapContents()
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      Destroy(transform.GetChild(i));
    }
  }

}
