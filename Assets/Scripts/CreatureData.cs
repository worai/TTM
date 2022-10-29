using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class DamageFloatEvent : UnityEvent<float> { }


public class CreatureData : MonoBehaviour
{
  [SerializeField] private float MaxHP;
  [SerializeField] private float HP;
  [Tooltip("Damage reduction")] [SerializeField] private float RD;
  [SerializeField] private bool obstructsWhenDead;

  [SerializeField] Animator animator;
  //[SerializeField] UnityEvent damagedEvent;
  [SerializeField] DamageFloatEvent damageFloatEvent;
  [SerializeField] UnityEvent deathEvent;

  private bool isDeaded = false;

  [SerializeField] private bool showMessages = false;

  private void Start()
  {
    if (animator == null) animator = GetComponent<Animator>();
    if (deathEvent == null) deathEvent = new UnityEvent();
  }

  private void Update()
  {
    if (IsDead && !isDeaded)
    {
      deathEvent.Invoke();
      animator.SetBool("Dead", true);
      if (showMessages) Debug.Log("Is now dead");
      isDeaded = true;
      if (!obstructsWhenDead)
      {
        //TODO change so that collider filter changes, instead of this shit.
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null) collider.enabled = false;
      }
    }

    if (IsDestroyed) gameObject.SetActive(false); // Destroy(gameObject); 

  }


  #region SERIALIZABLE
  [Serializable]
  public class CreatureDataS
  {
    public float MaxHP;
    public float HP;

    public CreatureDataS(float maxHP, float hP)
    {
      MaxHP = maxHP;
      HP = hP;
    }
  }
  #endregion



  internal void TakeDamage(float damage)
  {
    HP -= RD > damage ? 0 : damage - RD;
    //damagedEvent.Invoke();
    damageFloatEvent.Invoke(damage - RD);
    if (showMessages) Debug.Log("current hp " + HP);
  }

  public void Respawn()
  {
    HP = MaxHP;
    isDeaded = false;
    animator.SetBool("Dead", false);
    BoxCollider2D collider = GetComponent<BoxCollider2D>();
    if (collider != null) collider.enabled = true;
  }

  internal bool IsDead => HP < 0f;
  internal bool IsDestroyed => HP < -MaxHP;

  //public float CurrentHP => HP;
  public float CurrentHP => HP;

}
