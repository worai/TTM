using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Obsolete("Controlled in script in PowerupContainer")]
public class P1ContainerController : MonoBehaviour
{

  [SerializeField] private Image image;


  private void Start()
  {
    if (image.sprite == null) image.color = Color.clear;
  }




  public void UpdateImage(APowerup powerup)
  {
    Debug.Log("Update image1");
    //this.image.sprite = ((IWeapon)weapon).IconSprite;
    //IWeapon interfaceWeapon = weapon as IWeapon;
    //image.sprite = interfaceWeapon.IconSprite;
    //image.color = Color.white;
    Debug.Log("Update image2");
  }

}
