using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
  public RowNum Row { get; set; }
  public ColNum Col { get; set; }



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
  internal bool Instantiate(byte state, byte level, ColNum col, RowNum row)
  {
    if(!instantiated)
    {
      instantiated = true;
      this.State = state;
      this.Health = state;
      this.Level = level;
      this.Col = col;
      this.Row = row;
      return true;
    }
    return false;
  }

  public override string ToString()
  {
    StringBuilder sb = new StringBuilder();
    
    sb.AppendLine($"-[State: {State}")
      .AppendLine($"  Health: {Health}")
      .AppendLine($"  Level: {Level}")
      .AppendLine($"  Col: {Col}")
      .AppendLine($"  Row: {Row}]");
    return base.ToString();
  }

}
