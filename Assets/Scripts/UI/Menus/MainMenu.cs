using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  public void StartButton()
  {
    SceneLoader.Load(SceneLoader.Scene.TransitionScene);
    LevelInfos.Level = 1;
  }

  //OptionsButton is handled directly in the button's event handler.

  public void LoadGameButton()
  {
    Debug.Log("Load game");
  }

  public void ExitToDesktopButton()
  {
    Debug.Log("Exiting to desktop");
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }

}
