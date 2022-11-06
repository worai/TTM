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

  private void Start()
  {
    int currentLevel = LevelInfos.Level-1 ?? 0;
    transitions[currentLevel].SetActive(true);
  }

}
