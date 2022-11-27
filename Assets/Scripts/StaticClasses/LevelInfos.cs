using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelInfos
{

  
  /// <summary>
  /// 1-based
  /// </summary>
  public static int? Level { get; set; }
  public static bool? Fell { get; set; }
  public static bool StartingIndors { get; set; }
  public static float MapWidth { get; set; }
  public static float MapHeight { get; set; }

  //TODO time infos in here?

}
