using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
  [SerializeField] private Transform playerTrans;
  [SerializeField] private float startSpeed = 2f;
  [SerializeField] private float acceleration = 30f;
  [SerializeField] private float maxDistanceFromPlayer = 15;

  private float _currentSpeed = 0f;
  private Transform _trans;
  private Vector3? _direction = null;

  // Start is called before the first frame update
  void Awake()
  {
    _direction = Utility.GetRandomDirection();
    _trans = transform;
  }

  private void Update()
  {
    _trans.position += _direction.Value * _currentSpeed * Time.deltaTime;
    _currentSpeed += acceleration * Time.deltaTime;
    if (Mathf.Abs(playerTrans.position.x - _trans.position.x) > maxDistanceFromPlayer ||
       Mathf.Abs(playerTrans.position.y - _trans.position.y) > maxDistanceFromPlayer)
      Destroy(this.gameObject);
  }

  internal void SetDirection(Vector3 direction)
  {
    _direction = direction;
  }
}
