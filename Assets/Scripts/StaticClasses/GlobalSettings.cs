using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GlobalSettings
{
  private static GlobalSettings _instance;
  public static GlobalSettings Instance
  {
    get
    {
      if (_instance == null)
        _instance = new GlobalSettings();
      return _instance;
    }
  }


  public const float gravity = -10;
  public float Gravity => gravity;


}
