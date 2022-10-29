using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeThresholdManager : MonoBehaviour
{

  public byte[] Thresholds { get; private set; }
  

  [Tooltip("Currently 3: accomodations, factories, highways")] [SerializeField] private byte numLevels = 3;
  [Tooltip("5 thresholds per level: Dam1, Dam2, Des1, Des2, Node destruction")] [SerializeField] private byte numThresholdsPerLevel = 5;


  private void Start()
  {
    CalculateThresholds();
  }

  private void CalculateThresholds()
  {
    TryInstantiate();

    /*TODO adjust thresholds*/
    //for (int level = 0; level < numLevels; level++)
    //{
    //  for (int thresh = 0; thresh < numThresholdsPerLevel; thresh++)
    //  {
    //    Thresholds
    //  }
    //}

    for (int i = 0; i < Thresholds.Length; i++)
    {
      //Thresholds[Thresholds.Length - i - 1] = (byte) ( (i + 1) * byte.MaxValue / (numLevels * numThresholdsPerLevel) );
      Thresholds[i] = (byte) ( byte.MaxValue * (Thresholds.Length - i - 1) / (Thresholds.Length) );
    }

  }


  /// <summary>
  /// thresholds for level given. Zerobased level. 
  /// </summary>
  /// <param name="level">zero based, zero to num levels of game</param>
  /// <returns></returns>
  public byte[] GetThresholdsForLevel(byte level)
  {
    TryInstantiate();

    byte _level = (byte) Mathf.Max(level, 0);
    _level = (byte) Mathf.Min(_level, numLevels-1); //zero-based, so need to subtract 1

    byte[] result = new byte[numThresholdsPerLevel];
    for (int threshThisLvl = 0; threshThisLvl < numThresholdsPerLevel; threshThisLvl++)
    {
      result[threshThisLvl] = Thresholds[numThresholdsPerLevel * _level + threshThisLvl];
    }
    return result;
  }

  private void TryInstantiate()
  {
    if (Thresholds != null) return;
    Thresholds = new byte[numLevels * numThresholdsPerLevel];
  }

}
