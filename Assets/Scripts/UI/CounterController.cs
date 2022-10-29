using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour
{
  [SerializeField] GameObject numberTemplate;
  [SerializeField] private bool createNew = true;
  [SerializeField] private bool startAtStart = true;
  private bool deathCountAtLeastOne = false;

  List<NumberController> numbers;

  private void Start()
  {
    numbers = new List<NumberController>();
    if (!startAtStart) 
      return;
    if(!createNew)
    {
      //load in
    }
    else
    {
      Vector2 newPos = new Vector2(0f, 0f);
      CreateNewNumber(0, selfRunning: true, value: 0);
    }
  }

  internal void IncrementOrCreateNextDigit(byte callingDigit)
  {
    if (callingDigit >= numbers.Count-1)
      CreateNewNumber((byte)numbers.Count, selfRunning: false, value:1);
    else
      numbers[callingDigit + 1].Increment();
  }

  private void CreateNewNumber(byte digitPosition, bool selfRunning, byte value)
  {
    GameObject newGo = Instantiate(numberTemplate);
    newGo.SetActive(true);
    newGo.transform.SetParent(transform);

    //Rect rect = newGo.GetComponent<Rect>(); REMOVE
    RectTransform rectTrans = newGo.GetComponent<RectTransform>();
    Vector2 newPos = new Vector2(-rectTrans.sizeDelta.x/2 * digitPosition, 0);
    rectTrans.anchoredPosition = newPos;

    NumberController number = newGo.GetComponent<NumberController>();
    number.Instantiate(selfRunning, digitPosition, value);
    number.UpdateImage();
    numbers.Add(number);

  }

  public void ResetOrStart()
  {
    if(numbers.Count > 0)
    {
      numbers.Clear();
      CreateNewNumber(0, selfRunning: true, value: 0);
    }
    else
      CreateNewNumber(0, selfRunning: true, value: 0);
  }
}
