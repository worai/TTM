using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TileMapFac
{

  private List<TileMapCol> tileMapColList;

  public TileMapFac(int numColumns = 1, int numRows = 1)
  {
    tileMapColList = new List<TileMapCol>();
    for (int i = 0; i < numColumns; i++)
    {
      tileMapColList.Add(new TileMapCol(numRows));
    }
  }

  public int Width { get => tileMapColList.Count; }
  /// <summary>
  /// assumes that all the columns have the same height...
  /// </summary>
  public int Height { get => tileMapColList[0].Height; }

  /// <summary>
  /// bounds-safe; returns false if out of bounds
  /// </summary>
  /// <param name="col">0-indexed</param>
  /// <param name="row">0-indexed</param>
  /// <returns></returns>
  public bool Get(int col, int row)
  {
    if (col < 0 || col >= tileMapColList.Count) return false;
    return tileMapColList[col].Get(row);
  }

  /// <summary>
  /// Boundarysafe, returns a value depending on whether the indeces were within bounds.
  /// </summary>
  /// <param name="col">0-based</param>
  /// <param name="row">0-based</param>
  /// <param name="value"></param>
  /// <returns>true if successfully set</returns>
  public bool Set(int col, int row, bool value= true)
  {
    try
    {
      if(col >= 0 && col < Width)
        return tileMapColList[col].Set(row, value);
      return false;
    }
    catch(Exception e)
    {
      Debug.LogError(e);
      Debug.Log("");
      Debug.LogError("Exception thrown, col:" + col + ", row:" + row);

    }
    return false;
  }

  //public override string ToString()
  //{
  //  StringBuilder sb = new StringBuilder();
  //  sb.AppendLine("((");
  //  foreach (TileMapCol mapCol in tileMapColList)
  //  {
  //    sb.AppendLine(mapCol.ToString());
  //  }
  //  sb.AppendLine("))");
  //  return sb.ToString();
  //}


  private class TileMapCol
  {
    private List<bool> tileMapCol;

    public int Height { get => tileMapCol.Count;}

    public TileMapCol(int numRows = 1)
    {
      this.tileMapCol = new List<bool>();
      for (int i = 0; i < numRows; i++)
      {
        tileMapCol.Add(false);
      }
    }

    internal bool Get(int row)
    {
      if (row < 0 || row >= tileMapCol.Count) return false;
      return tileMapCol[row];
    }

    internal bool Set(int row, bool value)
    {
      if(row >= 0 && row < Height)
      {
        tileMapCol[row] = value;
        return true;
      }
      return false;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("{");
      sb.Append(tileMapCol[0]);
      for (int i = 1; i < tileMapCol.Count; i++)
      {
        sb.Append(", ");
        sb.Append(tileMapCol[i]);
      }
      sb.Append("}");
      return sb.ToString();
    }
  }


}
