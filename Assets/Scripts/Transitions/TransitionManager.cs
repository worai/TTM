using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{

  //public enum Transition
  //{
  //  From1to2,
  //  From2to3,
  //  From3to4,
  //  StartNewGame,
  //  //FinishGame,
  //  //GiveUpGame, //??
  //}

  //[SerializeField] private Transition currentTransition;

  [SerializeField] private GameObject[] transitions;

  [SerializeField] private TransitionType testTransition = TransitionType.None;

  private void Start()
  {
    switch (testTransition)
    {
      case TransitionType.T1to2:
        LevelInfos.Level = 1;
        break;
      case TransitionType.T2to3:
        LevelInfos.Level = 2;
        break;
      case TransitionType.T3to4:
        LevelInfos.Level = 3;
        break;
      default:
        //Do nothing
        break;
    }
    //if(testTransition == TransitionType.T1to2)
    int currentLevel = LevelInfos.Level-1 ?? 0;
    transitions[currentLevel].SetActive(true);
  }

  public enum TransitionType
  {
    None,
    T1to2,
    T2to3,
    T3to4,
  }

}
