using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] private int minEnemiesInLists = 5;
  //[SerializeField] private int enemyIncreaseIncrements = 5;
  [SerializeField] private int mediumMaxEnemiesInLists = 10;
  [SerializeField] private int maxMaxEnemiesInLists = 15;
  private int currentMaxEnemiesInLists = 5;

  [SerializeField] Director director;
  [Space(5)]
  [SerializeField] GameObject zombieTemplate;
  [SerializeField] GameObject workerTemplate;
  [SerializeField] GameObject crabTemplate;
  [SerializeField] GameObject playerGO;
  [Space(5)]
  [SerializeField] private float maxSpawnPeriod = 2f;
  [SerializeField] private float minSpawnPeriod = 0.5f;
  [SerializeField] private float taxiDespawnRange = 20f;
  [Space(5)]
  [SerializeField] private float minSpawnRange = 10f;
  [SerializeField] private float maxSpawnRange = 14f;

  //TODO sort 'frontness' of creatures and set their sprite sorting layer rel to player sprite
  private List<GameObject> enemyList = new List<GameObject>();
  private float lastSpawnTime = 0f;
  private float currentSpawnPeriod = 2f;

  private void Update()
  {
    UpdateNumMaxEnemiesNSpawnTimes();

    DespawnFarEnemies();

    if (lastSpawnTime + currentSpawnPeriod < Time.time)
    {
      //New position
      Vector3 randDirection = Poets.MathUtility.Utility.GetRandomDirection();
      if (playerGO.GetComponent<PlayerController>().Velocity.y >= 0f && randDirection.y < 0f)
        randDirection = new Vector3(randDirection.x, Mathf.Abs(randDirection.y));
      if (playerGO.GetComponent<PlayerController>().Velocity.y < 0f && randDirection.y > 0f)
        randDirection = new Vector3(randDirection.x, -Mathf.Abs(randDirection.y));
      randDirection = new Vector3(randDirection.x, randDirection.y).normalized;
      Vector3 newPosition = playerGO.transform.position + randDirection * UnityEngine.Random.Range(minSpawnRange, maxSpawnRange);

      //TODO extract method and randomise or discriminate when and at which levels different enemies should spawn
      //if zombie
      //// enemyList.Where(x => x.activeInHierarchy).ToArray().Length

      if (enemyList.Count < currentMaxEnemiesInLists || enemyList.Count < 1)
      {
        // creating new guy
        GameObject newGo = GameObject.Instantiate(zombieTemplate);
        newGo.transform.position = newPosition;
        newGo.SetActive(true);
        enemyList.Add(newGo); //adds to end of list
      }
      else
      {
        //reuse disabled guy
        GameObject enemyGO = enemyList[0];
        enemyList.RemoveAt(0);
        if (!enemyGO.activeSelf || enemyGO.GetComponent<CreatureData>().IsDead)
        {
          enemyGO.SetActive(true);
          enemyGO.GetComponent<CreatureController>().Respawn(); //should I rather get the CreatureData??
          enemyGO.transform.position = newPosition;
        }
        enemyList.Add(enemyGO);
      }

      lastSpawnTime = Time.time;
    }

  }



  private void UpdateNumMaxEnemiesNSpawnTimes()
  {
    if (director.CurrentMode == Director.DirectorMode.Lull)
    {
      currentMaxEnemiesInLists = minEnemiesInLists;
      currentSpawnPeriod = maxSpawnPeriod;
    }
    else if (director.CurrentMode == Director.DirectorMode.RisingTension)
    {
      currentMaxEnemiesInLists = mediumMaxEnemiesInLists;
      currentSpawnPeriod = minSpawnPeriod;
    }
    else if (director.CurrentMode == Director.DirectorMode.Action)
    {
      currentMaxEnemiesInLists = maxMaxEnemiesInLists;
    }
  }

  private void DespawnFarEnemies()
  {
    foreach(GameObject enemy in enemyList)
    {
      Transform _playerTrans = playerGO.transform;
      Transform _enemyTrans = enemy.transform;
      if(taxiDespawnRange < Mathf.Abs(_playerTrans.position.x - _enemyTrans.position.x)
        || taxiDespawnRange < Mathf.Abs(_playerTrans.position.y - _enemyTrans.position.y))
      {
        enemy.SetActive(false);
      }
    }
  }

  public void DespawnAllEnemies()
  {
    foreach (GameObject enemy in enemyList)
    {
      enemy.SetActive(false);
    }
  }

}
