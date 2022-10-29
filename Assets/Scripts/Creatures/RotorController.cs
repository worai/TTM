using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RotorController : MonoBehaviour
{

  [SerializeField] private LineRenderer line1;
  [SerializeField] private LineRenderer line2;

  [SerializeField] private float lineLengths = 1.0f;
  [Tooltip("Larger angle -> further above")] [SerializeField] private float viewAngleDeg = 30f;
  [SerializeField] private float lineSeparationRadius = 0.1f;
  [SerializeField] private float lineYShift = 0.2f;

  [SerializeField] public float speedParam = 0f;
  [SerializeField] public float liftParam = 1f;

  private float viewAngleFactor = 1;

  private float oldRotationAngle;
  private float oldLiftAngle;

  private float rotationAngleDeg;
  private float rotationAngleDeg_Complementary;
  /// <summary>
  /// wanted to use this and the other public guy to control the thingies from animation clips
  /// </summary>
  [SerializeField] private float RotationAngleDeg 
  { 
    get => rotationAngleDeg; 
    set
    {
      float complementaryValue = value + 180f;
      rotationAngleDeg = rotationAngleDeg + value < 0f ? 360 + value % 360f : value % 360f;
      rotationAngleDeg_Complementary = rotationAngleDeg + complementaryValue < 0f ? 360 + complementaryValue % 360f : complementaryValue % 360f;
    }
  }
  private float liftAngleDeg;
  /// <summary>
  /// wanted to use this and the other public guy to control the thingies from animation clips
  /// </summary>
  [SerializeField] private float LiftAngleDeg 
  { 
    get => liftAngleDeg; 
    set
    {
      if (value < 0f) liftAngleDeg = 0f;
      else if(value > 90f) liftAngleDeg = 90f;
      else liftAngleDeg = value;
    }
  }

  private void Start()
  {
    viewAngleFactor = Mathf.Cos(Mathf.Deg2Rad * (90f - viewAngleDeg));
  }

  private void Update() 
  {
    //RotationAngleDeg += Time.deltaTime * 90f;
    //LiftAngleDeg = 60f;

    if(true)
    {
      //oldRotationAngle = RotationAngleDeg;
      //oldLiftAngle = LiftAngleDeg;

      //base of the thingy
      viewAngleFactor = Mathf.Cos(Mathf.Deg2Rad * (90f - viewAngleDeg));
      float v = viewAngleFactor;
      float s = Mathf.Sin(Mathf.Deg2Rad * rotationAngleDeg);
      float c = Mathf.Cos(Mathf.Deg2Rad * rotationAngleDeg);
      Vector3 newBasePosition = new Vector3(lineSeparationRadius * c, lineSeparationRadius * s * v + lineYShift);
      line1.SetPosition(0, newBasePosition);

      Vector3 newBasePosition_comp = -newBasePosition + new Vector3(0f, 2 * lineYShift);
      line2.SetPosition(0, newBasePosition_comp);

      //tip of the thingy
      float sLift = Mathf.Sin(Mathf.Deg2Rad * liftAngleDeg);
      float cLift = Mathf.Cos(Mathf.Deg2Rad * liftAngleDeg);
      float l = lineLengths;
      float lSin = l * sLift;
      float lCos = l * cLift;
      Vector3 newTipPosition = new Vector3(lCos * c + newBasePosition.x, 
        lCos * s * v + lSin + newBasePosition.y); 
      line1.SetPosition(1, newTipPosition);

      //Vector3 newTipPosition_comp = -newTipPosition + new Vector3(-newBasePosition.x, 2 * lSin + 2 * newBasePosition.y);
      Vector3 newTipPosition_comp = new Vector3(-lCos * c + newBasePosition_comp.x, 
        -lCos * s * v + lSin + newBasePosition_comp.y);
      line2.SetPosition(1, newTipPosition_comp);
    }


    if (speedParam < 0f) speedParam = 0f;
    RotationAngleDeg += Time.deltaTime * 360f * speedParam;

    if (liftParam < 0f) liftParam = 0f;
    else if (liftParam > 1f) liftParam = 1f;
    LiftAngleDeg = liftParam * 90f;

  }




}
