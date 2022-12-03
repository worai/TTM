using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom made for the very first "cut-scene" where the player is allowed to interact with the scene
/// </summary>
public class TVCutscene : MonoBehaviour
{
  [SerializeField] private SceneBitNTime[] bitntime;


  IEnumerator Start()
  {

    yield return StartCoroutine(RunBit(0));




    yield return StartCoroutine(RunBit(2));

  }


  IEnumerator RunBit(int i)
  {
    bitntime[i].bit.SetActive(true);
    yield return new WaitForSeconds(bitntime[i].time);
  }

}
