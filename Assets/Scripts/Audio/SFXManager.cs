using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

  public Sound[] sounds;


  private void Awake()
  {
    foreach (Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
    }

  }

  public void Play(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if(s == null)
    {
      Debug.LogError("No such sound to play : " + name);
      return;
    }
    s.source.Play();
  }

  public void Stop(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
      Debug.LogError("No such sound to stop : s" + name);
      return;
    }
    s.source.Stop();
  }

}
