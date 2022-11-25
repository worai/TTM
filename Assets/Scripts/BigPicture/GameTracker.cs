using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// has got a bunch o flags n such.
/// Serializable, s.t. it can be saved...
/// hopefully
/// </summary>
[Serializable]
public class GameTracker
{


  //private Dictionary<FlagType, bool> boolByFlagtype;

  private FlagType[] flagTypes;
  private bool[] bools;


  public GameTracker()
  {
    //boolByFlagtype = new Dictionary<FlagType, bool>();
    flagTypes = new FlagType[Enum.GetValues(typeof(FlagType)).Length];
    bools = new bool[Enum.GetValues(typeof(FlagType)).Length];
    InitialiseFlagtypes();
  }

  private void InitialiseFlagtypes()
  {
    //boolByFlagtype.Clear();
    //foreach(FlagType _type in (FlagType[]) Enum.GetValues(typeof(FlagType)))
    //{
    //  boolByFlagtype.Add(_type, false);

    //}

    for (int i = 0; i < bools.Length; i++)
    {
      bools[i] = false;
    }

    //int index = GetIndexOf()
  }

  public enum FlagType
  {
    PickedUpHandgun,
    DisregardedHandgun,
  }

  public bool GetBoolByFlag(FlagType _type)
  {
    int index = GetIndexOf(_type);

    return bools[index];

    //if (!boolByFlagtype.ContainsKey(_type)) InitialiseFlagtypes();
    //return boolByFlagtype[_type];
  }

  public void SetBoolByFlag(FlagType _type, bool value)
  {
    //if (!boolByFlagtype.ContainsKey(_type)) InitialiseFlagtypes();
    //boolByFlagtype[_type] = value;
    int index = GetIndexOf(_type);
    bools[index] = value;
  }


  private int GetIndexOf(FlagType _type)
  {
    int failedResult = -1;
    for (int i = 0; i < flagTypes.Length; i++)
    {
      if (flagTypes[i] == _type) return i;
    }
    return failedResult;
  }

}


//public class GameTrackerData
//{

//}