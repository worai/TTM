using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Obsolete("Use regular unity events instead")]
public class BalanceStartedEvent : UnityEvent<ColNum, RowNum> { }
[System.Obsolete("Use regular unity events instead")]
public class BalanceCancelledEvent : UnityEvent<ColNum, RowNum> { }


public struct ColNum
{
  public int value { get; set; }

  public ColNum(int value)
  {
    this.value = value;
  }

  public static implicit operator int(ColNum num) => num.value;
  public static implicit operator ColNum(int intVal) => new ColNum(intVal);
}

public struct RowNum
{
  public int value { get; set; }

  public RowNum(int value)
  {
    this.value = value;
  }

  public static implicit operator int(RowNum num) => num.value;
  public static implicit operator RowNum(int intVal) => new RowNum(intVal);
}