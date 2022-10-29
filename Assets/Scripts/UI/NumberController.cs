using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NumberController : MonoBehaviour
{
  [SerializeField] private Image image;
  [SerializeField] private Sprite[] spriteArray;
  [SerializeField] [Range(0, 4)] private byte value;
  [SerializeField] public byte _digitPosition = 0;

  [SerializeField] CounterController counter;

  private bool _selfRunning;

  public byte Value => value;
  public byte DigitPosition => _digitPosition;

  private void Update()
  {
    if (!runningTick && _selfRunning) 
      StartCoroutine(Tick());
  }


  private bool runningTick = false;
  private IEnumerator Tick()
  {
    runningTick = true;
    yield return new WaitForSeconds(1f);
    Increment();
    runningTick = false;
  }


  public void Increment()
  {
    value++;
    value = (byte)(value % 5);
    if (value == 0) counter.IncrementOrCreateNextDigit(DigitPosition);
    UpdateImage();
  }


  public void UpdateImage()
  {
    image.sprite = spriteArray[value];
  }


  public void Instantiate(bool selfRunning, byte digitPosition, byte value)
  {
    this.value = value;
    _selfRunning = selfRunning;
    _digitPosition = digitPosition;
  }

}
