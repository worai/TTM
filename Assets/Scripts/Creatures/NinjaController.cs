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
    if (playerGo != null) playerGo.GetComponentInChildren<PlayerWeapon>().onWeaponShot.AddListener(HandleBeingShotAt);
  }



  private void FixedUpdate()
  {
    if (data.IsDead || !GetComponentInChildren<CreatureAutoSpawner>().AlreadySpawned) return;
    if (playerGo == null) playerGo = GameObject.FindGameObjectWithTag("Player");
    if (playerGo == null) CurrentState = CreatureActionState.Idle;

    if (!UpdateStateNCoroutine())
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

  private bool _runningJumpPursueCoroutine = false;
  private IEnumerator JumpPursueCoroutine()
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

  private bool _runningJumpEvadeCoroutine = false;
  private IEnumerator JumpEvadeCoroutine()
  {
    _runningJumpEvadeCoroutine = true;
    //Vector3 final = 
    float vertJumpSpeed = this.jumpSpeed;
    Vector3 playerDirection = (playerGo.transform.position - transform.position).normalized;
    Vector3 rightDirection = GetRightRandomDirection(playerDirection);
    int mask = LayerMask.GetMask("Structure");
    RaycastHit2D hit = Physics2D.Raycast(transform.position, rightDirection, maxJumpDistance, mask);
    if (hit)
    {
      rightDirection = -rightDirection;
      //if hit other direction
      //  stop the coroutine... make ref to this coroutine, for this clause to be able to reach it.
    }
    Vector3 jumpDestination = transform.position + rightDirection * maxJumpDistance;
    //bool canAttack = TryFindPositionCloserToPlayer(max: maxJumpDistance, out jumpDestination);
    float jumpDistance = maxJumpDistance; // (jumpDestination - transform.position).magnitude;
    currentVertSpeed = Utility.InitialJumpSpeed(jumpDistance, gravity, vertJumpSpeed);
    currentHorzVelocity = rightDirection * vertJumpSpeed;
    while (currentVertSpeed > 0f ? true : currentJumpHeight != 0f)
    {
      HandleJumping();
      //yield return new WaitForEndOfFrame();
      yield return new WaitForSeconds(Time.deltaTime);
    }

    animator.SetFloat("Height", currentJumpHeight);
    CurrentState = CreatureActionState.Ready;
    _runningJumpEvadeCoroutine = false;
  }

  private Vector3 GetRightRandomDirection(Vector3 playerDirection)
  {
    return (new Vector3(playerDirection.y, -playerDirection.x )) * (UnityEngine.Random.Range(0,2) * 2 - 1);
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


  private bool UpdateStateNCoroutine()
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
      if (!_runningJumpPursueCoroutine)
        StartCoroutine(JumpPursueCoroutine());
    }
    else if(CurrentState == CreatureActionState.Evading)
    {
      if (!_runningJumpEvadeCoroutine)
        StartCoroutine(JumpEvadeCoroutine());
    }
    else if (CurrentState == CreatureActionState.Staggered)
    {
      Debug.LogError("Staggered state is not handled yet.");
    }
    return true;
  }

  public void HandleBeingShotAt(bool hit)
  {
    if (hit)
      CurrentState = CreatureActionState.Staggered;
    else
      CurrentState = CreatureActionState.Evading;
  }

  [Obsolete("Use HandleBeingShotAt instead")]
  public void SetEvadingState()
  {
    CurrentState = CreatureActionState.Evading;
  }

  private void UpdateLookingDirection()
  {
    myRenderer.flipX = playerGo.transform.position.x < transform.position.x;
  }

}
