using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1ContainerController : MonoBehaviour
{

  [SerializeField] private Image image;


  private void Start()
  {
    if (image.sprite == null) image.color = Color.clear;
  }


}
