//using Poets.MathUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : ACreatureMono
{

  #region fields
  [SerializeField] private bool verbose = false;
  [Space(10)]
  [SerializeField] private CreatureData data;
  [SerializeField] private Animator animator;
  [SerializeField] private SpriteRenderer myRenderer;
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
  [Space(10)]
  [SerializeField] private GameObject spriteGraphicGO;
  [SerializeField] private float jumpSpeed = 10f;
  [SerializeField] private float maxJumpDistance = 6f;

  private float gravity = -10f;
  private float currentJumpHeight = 0f;
  private float currentVertSpeed = 0f;
  private float instantaneousSeparation = 10f;
  private Vector3 currentHorzVelocity;
  private bool touchesGround = true;
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
    gravity = GlobalSettings.Instance.Gravity;
    _currentReadyRunPeriod = currentReadyRunPeriod;
    playerGo = GameObject.FindGameObjectWithTag("Player");
  }



  private void FixedUpdate()
  {
    if (data.IsDead || !GetComponentInChildren<CreatureAutoSpawner>().AlreadySpawned) return;
    if (playerGo == null) playerGo = GameObject.FindGameObjectWithTag("Player");
    if (playerGo == null) CurrentState = CreatureActionState.Idle;

    if (!UpdateState())
    {
      // ??
    }

    if (CurrentState == CreatureActionState.Idle)
      animator.SetBool("Idle", true);

    UpdateLookingDirection();
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
    CurrentState = CreatureActionState.Ready;
  }

  private bool _runningReadyCoroutine = false;
  private IEnumerator ReadyCoroutine()
  {
    _runningReadyCoroutine = true;
    animator.SetBool("Ready", true);
    yield return new WaitForSeconds(currentReadyRunPeriod);
    animator.SetBool("Ready", false);
    CurrentState = CreatureActionState.Pursuing;
    _runningReadyCoroutine = false;
  }

  private bool _runningJumpCoroutine = false;
  /// <summary>
  /// This basically tries to jump closer to player for an attack
  /// </summary>
  /// <returns></returns>
  private IEnumerator JumpCoroutine()
  {
    _runningReadyCoroutine = true;
    //Vector3 final = 
    float jumpSpeed = this.jumpSpeed;
    bool canAttack = TryFindPositionCloserToPlayer(max: maxJumpDistance, out Vector3 jumpDestination);
    float jumpDistance = (jumpDestination - transform.position).magnitude;
    currentVertSpeed = Utility.InitialJumpSpeed(jumpDistance, gravity, jumpSpeed);
    currentHorzVelocity = (jumpDestination - transform.position).normalized * jumpSpeed;
    while (currentVertSpeed > 0f ? true : currentJumpHeight != 0f)
    {
      HandleJumping();
      //yield return new WaitForEndOfFrame();
      yield return new WaitForSeconds(Time.deltaTime);
    }

    animator.SetFloat("Height", currentJumpHeight);
    CurrentState = CreatureActionState.Ready;
    _runningReadyCoroutine = false;
  }

  private bool TryFindPositionCloserToPlayer(float max, out Vector3 jumpDestination)
  {
    if (Utility.IsLessThanSeparation(transform.position, playerGo.transform.position, max))
    {
      jumpDestination = playerGo.transform.position;
    }
    else
    {
      float distance = (transform.position - playerGo.transform.position).magnitude;
      instantaneousSeparation = distance;
      jumpDestination = Mathf.Lerp(0, distance, max) * playerGo.transform.position + transform.position;
    }
    return true;
  }

  private void HandleJumping()
  {
    transform.position = transform.position + currentHorzVelocity * Time.deltaTime; 
    if (currentVertSpeed > 0f ? true : currentJumpHeight > 0f)
    {
      touchesGround = false;
      currentJumpHeight += currentVertSpeed * Time.fixedDeltaTime;
      currentVertSpeed += gravity * Time.fixedDeltaTime;
      animator.SetFloat("Height", currentJumpHeight);
      UpdateHeight();
    }
    else if (currentVertSpeed < 0f && currentJumpHeight < 0f)
    {
      touchesGround = true;
      currentJumpHeight = 0f;
      currentVertSpeed = 0f;
      animator.SetFloat("Height", currentJumpHeight);
      UpdateHeight();
    }
    if (verbose) Debug.Log(" - height " + currentJumpHeight + "\t current vert speed" + currentVertSpeed);
  }

  private void UpdateHeight()
  {
    spriteGraphicGO.transform.position = transform.position + new Vector3() { y = currentJumpHeight };
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
    else if(CurrentState == CreatureActionState.Pursuing)
    {
      if (!_runningJumpCoroutine)
        StartCoroutine(JumpCoroutine());
    }
    return true;
  }

  private void UpdateLookingDirection()
  {
    myRenderer.flipX = playerGo.transform.position.x < transform.position.x;
  }

}
