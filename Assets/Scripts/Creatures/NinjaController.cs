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
  [Space(5)]
  [SerializeField] private float attackRange = 1f;
  [SerializeField] private float moveSpeed = 2f;
  [Space(5)]
  [SerializeField] private float attackWindUp = 1f;
  [SerializeField] private float attackWindDown = 0.533f;
  [Space(5)]
  [SerializeField] private float minDamage = 10f;
  [SerializeField] private float maxDamage = 25f;
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

  internal void Respawn()
  {
    CurrentState = CreatureActionState.Idle;
    data.Respawn();
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

  private bool UpdateState()
  {
    if (playerGo == null) return false;

    return true;
  }
}
