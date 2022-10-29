using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpiringObject : MonoBehaviour
{
  [SerializeField] private float expiryTime = 1f;

  private float timeOfCreation;


  private void Start()
  {
     timeOfCreation = Time.time;
  }

  private void Update()
  {
    if (Time.time - timeOfCreation > expiryTime)
    {
      Debug.Log("I expire");
      Destroy(this.gameObject);
    }
  }

}
