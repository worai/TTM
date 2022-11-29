using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// should this guy keep track of exact method of firing?
///  - handgun
///  - blade (?)
///  - lasergun (?)
///  - grenade launcher
///  - slugger (neutronium cannon)
/// </summary>

//can't be serializable
//[Serializable]
public interface IWeapon
{
  bool Ready { get; }
  Sprite IconSprite { get; }
  Sprite InGameSprite { get; }

  /// <summary>
  /// exact inputmethodology in this guy?
  /// </summary>
  bool Fire(Transform firePoint);
  void MakeEffect();
  bool MakeReady(float duration);
  /// <summary>
  /// Currently only used for controll of sfx
  /// </summary>
  void MakeReadyStarted();
  /// <summary>
  /// Currently only used for controll of sfx
  /// </summary>
  void MakeReadyCancelled();

}
