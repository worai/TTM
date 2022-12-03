using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRemote : MonoBehaviour
{
  private RectTransform myRect;

  private void Awake()
  {
    myRect = GetComponent<RectTransform>();
    if (myRect == null) Debug.LogError("No rect found");
  }
}
