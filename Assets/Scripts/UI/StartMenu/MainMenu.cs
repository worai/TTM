using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  public Button button;


  public void StartButton()
  {
    SceneLoader.Load(SceneLoader.Scene.TransitionScene);
  }




  public void ExitToDesktopButton()
  {
    Debug.Log("Exiting to desktop");
    Application.Quit();
  }

}
