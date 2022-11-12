using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// https://www.youtube.com/watch?v=JRnNqQ2wbOU
/// </summary>
public class OptionsMenu : MonoBehaviour
{
  List<int> widths = new List<int> { 640, 800, 1024 };
  List<int> heights = new List<int> { 480, 600, 768 };

  [SerializeField] private AudioMixer mainMixer;

  public void SetMainVolume(float value)
  {
    mainMixer.SetFloat("UserMasterVolume", value);
  }
  public void SetMusicVolume(float value)
  {
    mainMixer.SetFloat("UserMusicVolume", value);
  }
  public void SetEffectsVolume(float value)
  {
    mainMixer.SetFloat("UserEffectsVolume", value);
  }

  public void SetScreenSize (int index)
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
