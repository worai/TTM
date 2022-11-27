using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
  [SerializeField] GameObject[] trailTemplates;
  [SerializeField] float trailSeparation = 5f;
  [SerializeField] float trailLength = 21f;
  //TODO make this value depend upon the level of zoom the player is using
  [SerializeField] float vertStartDistance = 3f;
  [SerializeField] float spread = 2f;

  private Transform playerTrans;

  private void Awake()
  {
    foreach (GameObject item in trailTemplates)
    {
      item.SetActive(false);
    }
    playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    if(playerTrans == null)
    {
      Debug.LogError("Trail MonoBehaviour didn't find GO with tag Player");
      return;
    }
    CreateTrail();
  }

  private void CreateTrail()
  {
    Vector3 startPosition = transform.position;
    Vector3 endPosition = playerTrans.position + new Vector3(0f, vertStartDistance);

    bool endOfTrail = false;
    float distanceFromGoal = 0;
    Vector3 direction = (endPosition - startPosition).normalized;
    while (!endOfTrail)
    {
      distanceFromGoal += trailSeparation;
      Vector3 nextTrailGOPosition = Next(startPosition, direction, ref distanceFromGoal);
      GameObject nextGO = NextGO(distanceFromGoal, trailLength);
      nextGO.transform.position = nextTrailGOPosition;
      if (distanceFromGoal > trailLength) endOfTrail = true;

    }
  }

  private Vector3 Next(Vector3 startPosition, Vector3 direction, ref float distanceFromGoal)
  {
    Vector3 random = Utility.GetRandomDirection() * spread;
    //return startPosition + direction * distanceFromGoal / trailLength + random;
    return startPosition + direction * distanceFromGoal + random;
  }

  private GameObject NextGO(float distanceFromGoal, float trailLength)
  {
    int randIndex = UnityEngine.Random.Range(0, trailTemplates.Length);
    bool flipped = UnityEngine.Random.Range(0, 2) > 0;
    GameObject result = Instantiate(trailTemplates[randIndex]);
    result.GetComponent<SpriteRenderer>().flipX = flipped;
    result.SetActive(true);
    result.transform.SetParent(this.transform);
    return result;
  }

}
