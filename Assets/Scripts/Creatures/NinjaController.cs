using Poets.MathUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : ACreatureMono
{

  #region fields
  [SerializeField] private CreatureData data;
  [SerializeField] private Animator animator;
  [Space(10)]
  [SerializeField] private float attackRange = 1f;
  [SerializeField] private float moveSpeed = 2f;
  [Space(10)]
  [SerializeField] private float attackWindUp = 1f;
  [SerializeField] private float attackWindDown = 0.533f;
  [Space(10)]
  [SerializeField] private float minDamage = 10f;
  [SerializeField] private float maxDamage = 25f;
  [Space(10)]
  [Tooltip("only used the first time the ready animation is run")] [SerializeField] private float currentReadyRunPeriod = 2f;

  private float _currentReadyRunPeriod;
  #endregion

  #region properties

  public CreatureActionState CurrentState { get; set; }
  private bool CanAttack
  {
    get
    {
      if (playerGo == null || playerGo.GetComponent<CreatureData>().IsDead) return false;
      if (playerGo.GetComponent<CreatureData>().IsDead) return false;
      return Utility.IsLessThanSeparation(playerGo.transform.position, transform.position, attackRange);
    }
  }

  #endregion



  private void Start()
  {
    _currentReadyRunPeriod = currentReadyRunPeriod;
    playerGo = GameObject.FindGameObjectWithTag("Player");
  }



  private void Update()
  {
    if (data.IsDead) return;
    if (playerGo == null) playerGo = GameObject.FindGameObjectWithTag("Player");
    if (playerGo == null) CurrentState = CreatureActionState.Idle;

    if (!UpdateState())
    {
      //nothing to report, just don't care, and let the dude idle around or something.
    }
    //UpdatePosition();
    //UpdateAttack();

  }

  internal void Respawn()
  {
    CurrentState = CreatureActionState.Idle;
    data.Respawn();
  }

  private bool _runningAttackCoroutine = false;
  private IEnumerator AttackCoroutine()
  {
    _runningAttackCoroutine = true;
    animator.SetBool("Attack", true);
    yield return new WaitForEndOfFrame();
    float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;
    yield return new WaitForSeconds(waitTime);
    _runningAttackCoroutine = false;
    animator.SetBool("Attack", false);
  }

  private bool _runningReadyCoroutine = false;
  private IEnumerator ReadyCoroutine()
  {
    _runningReadyCoroutine = true;
    animator.SetBool("Ready", true);
    yield return new WaitForSeconds(currentReadyRunPeriod);
    animator.SetBool("Ready", false);
    _runningReadyCoroutine = false;
  }


  private bool UpdateState()
  {
    if (playerGo == null) return false;
    if (playerGo != null && CurrentState == CreatureActionState.Idle) CurrentState = CreatureActionState.Ready;
    if (_runningAttackCoroutine || (CurrentState == CreatureActionState.Ready && CanAttack))
    {
      StartCoroutine(AttackCoroutine());
      CurrentState = CreatureActionState.Attacking;
    }
    else if (_runningReadyCoroutine || CurrentState == CreatureActionState.Ready)
    {
      if(!_runningReadyCoroutine)
        StartCoroutine(ReadyCoroutine());
      CurrentState = CreatureActionState.Ready;
    }
    return true;
  }
}
