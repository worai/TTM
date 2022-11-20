using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

  public enum Scene
  {
    TransitionScene,
    SampleScene,
    StartMenu,
  }

  // TODO set scene infos in here?
  public static void Load(Scene scene)
  {
    SceneManager.LoadScene(scene.ToString());
  }
}
