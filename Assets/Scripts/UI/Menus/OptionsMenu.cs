using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// https://www.youtube.com/watch?v=JRnNqQ2wbOU
/// </summary>
public class OptionsMenu : MonoBehaviour
{

  [SerializeField] private AudioMixer mainMixer;

  private List<int> widths = new List<int> { 640, 800, 1024 };
  private List<int> heights = new List<int> { 480, 600, 768 };
  private RectTransform myRect;

  private void Awake()
  {
    myRect = GetComponent<RectTransform>();
    //myRect.rect.x = 0f; myRect.rect.y = 0f;
    //myRect.rect.width = Screen.currentResolution.width;
    //myRect.rect.height = Screen.currentResolution.height;

  }


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
