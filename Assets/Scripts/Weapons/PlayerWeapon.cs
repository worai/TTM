using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


// TODO put this guys somewhere better
[System.Serializable] public class WeaponChoiceWeaponEvent: UnityEvent<AWeapon>{ }


public class PlayerWeapon : MonoBehaviour
{
  private PlayerWeaponInput pwi;
  private IWeapon weapon1;
  private IWeapon weapon2;
  private IWeapon chosenWeapon;
  
  [SerializeField] private Transform firePoint;
  //[SerializeField] private GameObject smallHitTemplate;

  [SerializeField] private HandGun handGun;
  [SerializeField] private Slugger slugger;
  [Space(5)]
  [SerializeField] private SpriteRenderer weaponSpriteRenderer;
  [SerializeField] private WeaponChoiceWeaponEvent chosenWeaponEvent;
  [SerializeField] private WeaponPickupWeaponEvent receivedWeapon1Event;
  [SerializeField] private WeaponPickupWeaponEvent receivedWeapon2Event;

  [HideInInspector] public ShootEventHit onWeaponShot;


  void Start()
  {
    pwi = new PlayerWeaponInput();
    pwi.Enable();
    if (receivedWeapon1Event == null) receivedWeapon1Event = new WeaponPickupWeaponEvent();
    if (receivedWeapon2Event == null) receivedWeapon2Event = new WeaponPickupWeaponEvent();

    //weapon1 = handGun;

    pwi.Player.MakeReady.started += MakeReady_started;
    pwi.Player.MakeReady.canceled += MakeReady_canceled;
    pwi.Player.Weapon1.started += Weapon1_started;
    pwi.Player.Weapon2.started += Weapon2_started;
  }

  private void Weapon1_started(InputAction.CallbackContext context)
  {
    if (context.started && weapon1 != null)
    {
      chosenWeapon = weapon1;
      chosenWeaponEvent.Invoke(weapon1 as AWeapon);
      weaponSpriteRenderer.sprite = weapon1.InGameSprite;
      //TODO fx for weapon swap, swivel and pop, n some sound too
    }
  }

  private void Weapon2_started(InputAction.CallbackContext context)
  {
    if (context.started && weapon2 != null)
    {
      chosenWeapon = weapon2;
      chosenWeaponEvent.Invoke(weapon2 as AWeapon);
      weaponSpriteRenderer.sprite = weapon2.InGameSprite;
    }
  }

  void Update()
  {

    Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(pwi.Player.MousePosition.ReadValue<Vector2>());
    Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
    transform.right = direction;

    if (direction.x < 0) weaponSpriteRenderer.flipY = true;
    else weaponSpriteRenderer.flipY = false;

  }


  private float makeReadyStartedTime = 0f;
  private void MakeReady_started(InputAction.CallbackContext context)
  {
    Debug.Log("make ready started");
    if (chosenWeapon == null)
    {
      Debug.Log("GUN IS NULL THO!");
      return;
    }
    makeReadyStartedTime = Time.time;
  }

  private void MakeReady_canceled(InputAction.CallbackContext context)
  {
    Debug.Log("Canceled");
    if (chosenWeapon == null) return;
    chosenWeapon.MakeReady(Time.time - makeReadyStartedTime);
  }


  /// <summary>
  /// Is fed into the Player Input cpt attached to the WeaponPivot GO
  /// </summary>
  /// <param name="context"></param>
  public void Fire(InputAction.CallbackContext context)
  {
    if (chosenWeapon == null) return;
    //if( context.phase == InputAction.) 
    if(context.started)
    {
      //Debug.Log("fired phase " + context.phase);
      if(chosenWeapon.Fire(firePoint))
        chosenWeapon.MakeEffect();
    }
  }



  public void PickupWeapon (AWeapon receivedWeapon)
  {
    //if (receivedWeapon.GetType().Equals(typeof(AWeapon1)))
    //  weapon1 = receivedWeapon as IWeapon;
    //else if (receivedWeapon.GetType().Equals(typeof(AWeapon2)))
    //  weapon2 = receivedWeapon as IWeapon;

    

    Debug.Log("Got into method1");
    Debug.Log("Got into method2");

    if (receivedWeapon is AWeapon1)
    {
      weapon1 = receivedWeapon as IWeapon;
      receivedWeapon1Event?.Invoke(receivedWeapon);
      if (chosenWeapon == null) chosenWeapon = weapon1;
    }
    else if (receivedWeapon is AWeapon2)
    {
      weapon2 = receivedWeapon as IWeapon;
      receivedWeapon2Event?.Invoke(receivedWeapon);
      if (chosenWeapon == null) chosenWeapon = weapon2;
    }
    else
      throw new TypeAccessException($"Wrong type of weapon being loaded, type {receivedWeapon.GetType()} is not supported!");
  }


}
