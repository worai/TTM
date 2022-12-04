using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{

  private UIInput interfaceInputs;
  private PlayerWeaponInput weaponInputs;

  private GameObject[] children;

  void Start()
  {
    interfaceInputs = new UIInput();
    interfaceInputs.Enable();
    weaponInputs = new PlayerWeaponInput();
    weaponInputs.Enable();

    children = new GameObject[transform.childCount];
    for (int i = 0; i < transform.childCount; i++)
    {
      children[i] = transform.GetChild(i).gameObject;
      children[i].SetActive(false);
    }

    interfaceInputs.User.Hints.started += Hints_started;
    interfaceInputs.User.Hints.canceled += Hints_canceled;

  }


  private void Hints_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    foreach(GameObject child in children)
    {
      child.SetActive(true);
    }
  }

  private void Hints_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    foreach (GameObject child in children)
    {
      child.SetActive(false);
    }
  }

}
