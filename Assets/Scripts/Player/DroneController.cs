using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
  [SerializeField] private Transform playerTrans;
  [SerializeField] private float startSpeed = 2f;
  [SerializeField] private float acceleration = 30f;
  [SerializeField] private float maxDistanceFromPlayer = 15f;
  [SerializeField] private float despawnRangeProximityToPlayer = 1f;

  private float _currentSpeed = 0f;
  private Transform _trans;
  private Vector3? _direction = null;
  private DroneMode _currentMode;

  // Start is called before the first frame update
  void Awake()
  {
    _currentSpeed = startSpeed;
    _direction = Utility.GetRandomDirection();
    _trans = transform;
    _currentMode = DroneMode.Spawning;
  }

  private void Update()
  {
    if (_currentMode == DroneMode.Spawning)
    {
      _trans.position += _direction.Value * _currentSpeed * Time.deltaTime;
      _currentSpeed += acceleration * Time.deltaTime;
      if (Mathf.Abs(playerTrans.position.x - _trans.position.x) > maxDistanceFromPlayer ||
         Mathf.Abs(playerTrans.position.y - _trans.position.y) > maxDistanceFromPlayer)
        Destroy(this.gameObject);
    }
    else if (_currentMode == DroneMode.Despawning)
    {
      _direction = (playerTrans.position - _trans.position).normalized;
      _trans.position += _direction.Value * _currentSpeed * Time.deltaTime;
      _currentSpeed += acceleration * Time.deltaTime;
      if (Mathf.Abs(playerTrans.position.x - _trans.position.x) < despawnRangeProximityToPlayer ||
         Mathf.Abs(playerTrans.position.y - _trans.position.y) < despawnRangeProximityToPlayer)
        Destroy(this.gameObject);
    }
  }

  internal void SetDirection(Vector3 direction)
  {
    _direction = direction;
  }

  internal void Instantiate(DroneMode mode = DroneMode.Spawning)
  {
    _currentMode = mode;
    if(_currentMode == DroneMode.Spawning)
    {
      //no need to do anything, the local position is set to 0,0 by default anyway.
    }
    else if(_currentMode == DroneMode.Despawning)
    {
      float h = 2f * Camera.main.orthographicSize;
      float w = h * Camera.main.aspect;
      //float w = Screen.width; float h = Screen.height;
      int side1 = UnityEngine.Random.Range(0, 2);
      side1 = 0;
      int side2 = UnityEngine.Random.Range(0, 2) - 1;
      float rHalf = UnityEngine.Random.Range(-0.5f, 0.5f);
      float x = side1 % 2 == 0 ?  rHalf * w : side2 * w / 2;
      float y = side1 % 2 != 0 ? rHalf * h : side2 * h / 2;

      Vector3 spawnPosition = new Vector3(x, y);
      _trans.position = spawnPosition;
    }
  }

  public enum DroneMode
  {
    Spawning,
    Despawning,
  }

}
