using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
  public static bool IsLessThanSeparation(Vector3 p1, Vector3 p2, float separation)
  {
    if (   Mathf.Abs(p1.x - p2.x) < separation
        && Mathf.Abs(p1.y - p2.y) < separation
        && Mathf.Abs(p1.z - p2.z) < separation)
      return true;
    else if ((p1 - p2).sqrMagnitude < separation * separation)
      return true;

    return false;
  }

  internal static Vector3 GetRandomDirection()
  {
    Vector3 result;

    float x = UnityEngine.Random.Range(-1f, 1f);
    float y = UnityEngine.Random.Range(-1f, 1f);

    result = (new Vector3(x, y)).normalized;

    return result;
  }

  /// <summary>
  /// Answers the question "What should the vertical jump speed be,
  /// given that we know the gravitational acc, distance jumped and horizontal speed.
  /// </summary>
  /// <param name="distance">Absolutized</param>
  /// <param name="gravity">Absolutized</param>
  /// <param name="horizontalSpeed">Absolutized</param>
  /// <returns></returns>
  public static float InitialJumpSpeed(float distance, float gravity, float horizontalSpeed)
  {
    float result = 1;
    if (horizontalSpeed != 0f)
    {
      result = Mathf.Abs(distance * gravity / (horizontalSpeed));
    }
    return result;
  }


}
