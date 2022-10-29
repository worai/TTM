using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class W1ContainerController : MonoBehaviour
{
  [SerializeField] private Image image;

  private void Start()
  {
    if (image.sprite == null) image.color = Color.clear;
  }

  public void UpdateImage(AWeapon weapon)
  {
    Debug.Log("Update image1");
    //this.image.sprite = ((IWeapon)weapon).IconSprite;
    IWeapon interfaceWeapon = weapon as IWeapon;
    image.sprite = interfaceWeapon.IconSprite;
    image.color = Color.white;
    Debug.Log("Update image2");
  }

}
