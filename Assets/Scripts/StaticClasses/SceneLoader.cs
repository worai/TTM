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
  }

  public static void Load(Scene scene)
  {
    SceneManager.LoadScene(scene.ToString());
  }
}
