using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://www.youtube.com/watch?v=JRnNqQ2wbOU
/// </summary>
public class OptionsMenu : MonoBehaviour
{
  List<int> widths = new List<int> { 640, 800, 1024 };
  List<int> heights = new List<int> { 480, 600, 768 };


  public void SetVolume(float value)
  {
    //TODO AudioMixer or something
  }

  public void SetSCreenSize (int index)
  {
    bool fullscreen = false;// Screen.fullScreen; //Don't care right now
    int width = widths[index];
    int height = heights[index];
    Screen.SetResolution(width, height, fullscreen);
  }

  //Currently unused; could be used later
  public void SetFullscreen(bool _fullscreen)
  {
    Screen.fullScreen = _fullscreen;
  }

}
