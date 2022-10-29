using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IWeapon cannot be serialised, so this guy is needed to allow weapons 
/// (handgun, slugger...) to be set in the inspector
/// </summary>
public abstract class AWeapon : MonoBehaviour
{

}
