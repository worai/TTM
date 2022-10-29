using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// can't be bothered adding this guy manually to each game object
/// </summary>
public class MapNodeState : MonoBehaviour
{
  private bool instantiated;

  public byte State {get; private set;}
  public byte Health { get; private set; }
  public byte Level { get; private set; }

  private void Start()
  {
    instantiated = false;
  }

  /// <summary>
  /// Instantiates map node state. State is not it's health, but health cannot be greater than the state
  /// Level is zero based, and is the number of levels the game has.
  /// </summary>
  /// <param name="state"></param>
  /// <param name="level">zero based</param>
  /// <returns>true if successful, otherwise false</returns>
  internal bool Instantiate(byte state, byte level)
  {
    if(!instantiated)
    {
      instantiated = true;
      State = state;
      Health = state;
      Level = level;
      return true;
    }
    return false;
  }
}
