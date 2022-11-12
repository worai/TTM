using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  [SerializeField] private GameObject pauseMenuGO;

  private UIInput uii;


  private void Start()
  {
    uii = new UIInput();
    uii.Enable();

    uii.User.Escape.started += Escape_started;
  }




  private void Escape_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    // TODO this pause function is not enough: if we're in the options menu, then this should not be possible
    if(pauseMenuGO != null)
    {
      if (Time.timeScale == 0f)
        Time.timeScale = 1f;
      else
        Time.timeScale = 0f;
      if (Time.timeScale == 0)
        pauseMenuGO.SetActive(true);
      else
        pauseMenuGO.SetActive(false);
    }
    else if(true)
    {
      Debug.LogError("Here, the pause button should affect some part of the start menu.");
    }
    else
    {
      throw new System.Exception("The escape button did not receive any object to affect!");
    }

  }



}
