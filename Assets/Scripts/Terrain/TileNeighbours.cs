using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNeighbours : MonoBehaviour
{


  /// <summary>
  /// directions, same as on a regular keyboard numpad, minus 1
  /// 
  /// 678
  /// 345
  /// 012
  /// </summary>
  public bool[] neighbours = new bool[9];


  public bool[] opening = new bool[9];

  /// <summary>
  /// needed to check if corners and crannies cause tiles to become disallowed, where they should be allowed
  /// </summary>
  /// <param name="direction"></param>
  /// <returns></returns>
  public bool AllowedDiagonal(int direction)
  {
    if (neighbours[direction]) return true;
    //if not a diagonal...
    if (direction % 2 == 0) return false;

    //check each diagonall: 0, 2, 6 & 8
    int checkDirection;
    for (int i = 0; i < 5; i++)
    {
      checkDirection = i * 2;
      if (checkDirection == 4) continue;
      if (neighbours[checkDirection]) return true;
    }
    return false;
  }


  public void Reset()
  {

    for (int i = 0; i < opening.Length; i++)
    {
      opening[i] = false;
      neighbours[i] = false;
    }
  }

  public bool IsWall
  {
    get
    {
      int direction;
      for (int i = 0; i < 4; i++)
      {
        direction = i * 2;
        if (!neighbours[direction]) return true;
      }
      return false;
    }
  }

}
