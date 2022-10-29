using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Poets.MathUtility;
using UnityEngine.Events;

[System.Serializable]
public class AttackAttemptFloatEvent : UnityEvent<float> { }

public class CreatureController : ACreatureMono
{

  ///*[SerializeField]*/ private GameObject playerGo;
  [SerializeField] private CreatureData data;
  [SerializeField] private Animator animator;
  [SerializeField] private SpriteRenderer myRenderer;


  [SerializeField] private float pursuitRange = 10f;
  [SerializeField] private float attackRange = 1f;
  [SerializeField] private float moveSpeed = 2f;
  [Space(5)]
  [SerializeField] private float attackWindUp = 1f;
  [SerializeField] private float attackWindDown = 0.533f;
  [Space(5)]
  [SerializeField] private float minDamage = 10f;
  [SerializeField] private float maxDamage = 25f;
  [SerializeField] private AttackAttemptFloatEvent attackAttemptFloatEvent;

  [Space(5)]
  [SerializeField] private bool hasHeavyAttack = false;

  public CreatureActionState CurrentState { get; set; }

  [HideInInspector] public float heavyAttackWindup = 0.5f;
  [HideInInspector] public float heavyAttackWinddown = 0.5f;
  [HideInInspector] public float heavyAttackMinDamage = 50f;
  [HideInInspector] public float heavyAttackMaxDamage =  75f;
  [HideInInspector] public float heavyAttackLikelyhood = 0.5f;

  public bool HasHeavyAttack { get => hasHeavyAttack; private set => hasHeavyAttack = value; }

  private bool CanAttack
  {
    get
    {
      if (playerGo == null || playerGo.GetComponent<CreatureData>().IsDead) return false;
      if (playerGo.GetComponent<CreatureData>().IsDead) return false;
      return Utility.IsLessThanSeparation(playerGo.transform.position, transform.position, attackRange);
    }
  }

  private bool CanPursue
  {
    get
    {
      if (playerGo == null || playerGo.GetComponent<CreatureData>().IsDead) return false;
      return Utility.IsLessThanSeparation(playerGo.transform.position, transform.position, pursuitRange);
    }
  }

  internal void Respawn()
  {
    CurrentState = CreatureActionState.Idle;
    data.Respawn();
  }


  private void Start()
  {
    data = GetComponent<CreatureData>();
  }

  private void Update()
  {
    if (data.IsDead) return;
    if (playerGo == null) playerGo = GameObject.FindGameObjectWithTag("Player");

    if (!UpdateState())
      Debug.LogError("Could not update state! object name: " + gameObject.name);
    UpdatePosition(); 
    UpdateAttack();
  }

  private void UpdateAttack()
  {
    if (CurrentState == CreatureActionState.Attacking)
    {
      bool heavyAttack = HasHeavyAttack && UnityEngine.Random.Range(0f, 1f) < heavyAttackLikelyhood;

      if(!attackCoroutineStarted)
      {
        attackAttemptFloatEvent.Invoke(1);
        if (heavyAttack)
          StartCoroutine(AttackHeavy());
        else
          StartCoroutine(Attack());
      }
    }
  }


  private bool attackCoroutineStarted = false;
  private IEnumerator AttackHeavy()
  {
    attackCoroutineStarted = true;
    animator.SetBool("AttackHeavy", attackCoroutineStarted);

    yield return new WaitForSeconds(heavyAttackWindup);
    try
    {
      CreatureData data = playerGo.GetComponent<CreatureData>();
      Debug.Log("Tries to attack");
      if (data != null && CanAttack)
      {
        data.TakeDamage(UnityEngine.Random.Range(heavyAttackMinDamage, heavyAttackMaxDamage));
        Debug.Log(data.gameObject.name + " HP: " + data.CurrentHP);
      }
    }
    catch (Exception e)
    {
      //probably needs no handling...
      //Debug.Log("")
      CurrentState = CreatureActionState.Idle;
    }
    yield return new WaitForSeconds(heavyAttackWinddown);
    attackCoroutineStarted = false;
    animator.SetBool("AttackHeavy", attackCoroutineStarted);
  }


  private IEnumerator Attack()
  {

    attackCoroutineStarted = true;
    animator.SetBool("Attack", attackCoroutineStarted);

    yield return new WaitForSeconds(attackWindUp);
    try
    {
      CreatureData data = playerGo.GetComponent<CreatureData>();
      Debug.Log("Tries to attack");
      if(data != null && CanAttack)
      {
        data.TakeDamage(UnityEngine.Random.Range(minDamage, maxDamage));
        Debug.Log(data.gameObject.name + " HP: " + data.CurrentHP); 
      }
    }
    catch(Exception e)
    {
      //probably needs no handling...
      //Debug.Log("")
      CurrentState = CreatureActionState.Idle;
    }
    yield return new WaitForSeconds(attackWindDown);
    attackCoroutineStarted = false;
    animator.SetBool("Attack", attackCoroutineStarted);
  }

  private void UpdatePosition()
  {
    if (CurrentState == CreatureActionState.Pursuing)
    {
      Vector3 direction = (playerGo.transform.position - transform.position).normalized;
      Vector3 velocity = direction * moveSpeed;
      animator.SetFloat("Speed", velocity.magnitude);
      myRenderer.flipX = velocity.x > 0f;
      transform.position = transform.position + velocity * Time.deltaTime;
    }
    else
      animator.SetFloat("Speed", 0f);
  }

  private bool UpdateState()
  {
    if (playerGo == null) CurrentState = CreatureActionState.Idle;  


    if (CanAttack || attackCoroutineStarted)
      CurrentState = CreatureActionState.Attacking;
    else if (CanPursue)
      CurrentState = CreatureActionState.Pursuing;
    else
      CurrentState = CreatureActionState.Idle;

    return true;
  }



}
