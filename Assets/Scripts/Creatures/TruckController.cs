using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// see the AssetTests project for an exploration into render/shader graph for pixellation of the guy.
/// </summary>
public class TruckController : MonoBehaviour
{
  [Tooltip("30?")] [SerializeField] private float speed = 20;

  private Transform trans;

  private float _turningRadius;
  private float _radialVelocity;
  private float _turningAngle;
  private Vector3 _oldCoords;

  public float TurningRadiusForRadVelocity { 
    get
    {
      return _turningRadius;
    } 
    set
    {
      _turningRadius = value;
      _radialVelocity = speed != 0f ? speed / _turningRadius : 0f;
      _turningAngle = 0f;
    }
  }

  public Vector3 OldCoords { get => _oldCoords; set => _oldCoords = value; }
  public Vector3 TurningPoint { get; set; }
  

  //Dictionary<Vector3, PathNode[]> pathNodesByPosition = new Dictionary<Vector3, PathNode[]>();


  public enum TruckState
  {
    Up,
    Down, //might implement this at some other time... no, I want to implement this! One thing at a time
    TurningRight,
    TurningLeft,
    Right,
    Left
  }


  public TruckState CurrentTruckState 
  { 
    get; 
    private set; 
  }

  public TruckState PreviousTruckState { get; private set; }


  private void Start()
  {
    CurrentTruckState = TruckState.Up;
    trans = transform;
  }

  public bool TryUpdateState(TruckState state)
  {
    if (CurrentTruckState == state) return false;
    PreviousTruckState = CurrentTruckState;
    CurrentTruckState = state;

    return true;
  }

  private void FixedUpdate()
  {
    UpdateMovement();
  }

  private void UpdateMovement()
  {
    switch (CurrentTruckState)
    {
      case TruckState.Up:
        trans.position += new Vector3(0f, speed) * Time.deltaTime;
        break;
      case TruckState.TurningRight:
        if (_turningRadius == 0f) CurrentTruckState = PreviousTruckState;
        _turningAngle += _radialVelocity * Time.deltaTime;
        trans.position = TurningPoint + new Vector3(
          - Mathf.Cos(_turningAngle), 
          Mathf.Sin(_turningAngle)
          ) * _turningRadius;
        trans.rotation = Quaternion.Euler(0f, 0f, 90f - Mathf.Rad2Deg * _turningAngle); 
        break;
      case TruckState.Right:
        trans.position += new Vector3(speed, 0f) * Time.deltaTime;
        break;
      default:
        Debug.Log("Don't know what to do with truck default state");
        break;
    }
  }

  //internal void QueuePathNodes(PathNode[] pathNodes, Transform mapElementTrans)
  //{
  //  pathNodesByPosition.Add(mapElementTrans.position, pathNodes);
  //}

  /// <summary>
  /// seems too complicated!
  /// I just need something that works for now. I don't need something that will break my back or mind.
  /// Another alternative would be using boids,
  /// but that could give me even less control than using path nodes!
  /// </summary>
  /// <param name="pathNodes"></param>
  /// <param name="trans"></param>
  /// <returns></returns>
  //internal bool TryRemove(PathNode[] pathNodes, Transform trans)
  //{
  //  if (pathNodesByPosition.ContainsKey(trans.position))
  //  {
  //    pathNodesByPosition.Remove(trans.position);
  //    return true;
  //  }
  //  else return false;
  //}



  public void Respawn(Vector3 position)
  {
    transform.position = position;
    gameObject.SetActive(true);
  }

}
