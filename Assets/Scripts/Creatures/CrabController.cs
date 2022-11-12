//using Poets.MathUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : ACreatureMono
{

  private bool messagesDebug = false;


  [Tooltip("The maximum number of directions, over the span of secons, that the players velocity directions are remembered.")]
  [SerializeField] private int maxSavedPlayerDirections = 4;

  [SerializeField] private float aggroRange = 5;
  [SerializeField] private float pursuitRange = 25;
  [SerializeField] private float attackRange = 20f;
  [SerializeField] private float moveSpeed = 2f;

  [SerializeField] private float attackWindUp = 5f;
  [SerializeField] private float attackWindDown = 0.533f;

  [SerializeField] private float minDamage = 100f;
  [SerializeField] private float maxDamage = 200f;

  [SerializeField] private Animator animator;
  [SerializeField] private Animation myAnimation;
  [SerializeField] private CreatureData data;
  [SerializeField] private RotorController rotor;
  [SerializeField] private CreatureAutoSpawner spawner;

  public CreatureActionState CurrentActionState { get; private set; }
  public SimultaneousActionState CurrentSimeltaneousActionState { get; private set; }

  protected bool CanAttack
  {
    get
    {
      if (playerGo == null) return false;
      if (playerGo.GetComponent<CreatureData>().IsDead) return false;
      return Utility.IsLessThanSeparation(playerGo.transform.position, transform.position, attackRange);
    }
  }
  protected bool CanPursue
  {
    get
    {
      if (playerGo == null) return false;
      if (playerGo.GetComponent<CreatureData>().IsDead) return false;
      return Utility.IsLessThanSeparation(playerGo.transform.position, transform.position, pursuitRange);
    }
  }
  protected bool CanAggro
  {
    get
    {
      if (playerGo == null) return false;
      if (playerGo.GetComponent<CreatureData>().IsDead) return false;
      return Utility.IsLessThanSeparation(playerGo.transform.position, transform.position, aggroRange);
    }
  }

  public Vector3 LastObservedPlayerDirectionNormal { get; private set; }
  public List<Vector3> PlayerDirections { get; private set; }
  public Vector3 ChaseDirection { get; private set; }
  public bool RaycastHittingSomething { get; private set; }
  public bool TargetOccluded { get; private set; }

  private RaycastHit2D hit;

  private void Start()
  {
    PlayerDirections = new List<Vector3>();
  }

  private void Update()
  {
    if (data.IsDead || !spawner.AlreadySpawned) return;
    if (playerGo == null) playerGo = GameObject.FindGameObjectWithTag("Player");

    if (playerGo != null && !runningUpdatePlayersLastDirection) StartCoroutine( UpdatePlayersLastDirections());

    if (!UpdateState())
      Debug.LogError("Could not update state! object name: " + gameObject.name);
    HandleWeapon();
    UpdatePosition();
  }


  private bool runningUpdatePlayersLastDirection = false;
  private IEnumerator UpdatePlayersLastDirections()
  {
    runningUpdatePlayersLastDirection = true;
    yield return new WaitForSeconds(1f);
    PlayerDirections.Add(playerGo.GetComponent<PlayerController>().Velocity.normalized); //added to end of list
    if (PlayerDirections.Count > maxSavedPlayerDirections) PlayerDirections.RemoveAt(0);
    runningUpdatePlayersLastDirection = false;
  }

  private bool UpdateState()
  {
    if (playerGo == null) CurrentActionState = CreatureActionState.Idle;

    if (CanAggro)
    {
      CurrentActionState = CreatureActionState.Pursuing;
    }
    else if (CanPursue && CurrentActionState == CreatureActionState.Pursuing)
    {
      //keep same state if cannot attack
      if (CanAttack || attackCoroutineStarted)
      {
        CurrentSimeltaneousActionState = SimultaneousActionState.WindUp;

      }
    }
    else if (!CanAttack && attackCoroutineStarted)
    {
      StopCoroutine(Attack());
      attackCoroutineStarted = false;
      CurrentSimeltaneousActionState = SimultaneousActionState.WindDown;
    }
    else
      CurrentActionState = CreatureActionState.Idle;


    Transform trans = transform;
    if (CanPursue) //null-check done in this guy
    {
      //Only these guys are hit!
      int layerMask = LayerMask.GetMask(new string[] { "Structure", "Player" });

      hit = Physics2D.Raycast(
        origin: trans.position,
        direction: playerGo.transform.position - trans.position,
        distance: (playerGo.transform.position - trans.position).magnitude,
        layerMask);
      Debug.DrawLine(trans.position, playerGo.transform.position, Color.red);
      if (hit.collider) RaycastHittingSomething = true;
    }
    else
    {
      RaycastHittingSomething = false;
    }

    Vector3 crabHitVector = new Vector3(hit.point.x, hit.point.y) - trans.position;
    Vector3 crabPlayerVector = playerGo.transform.position - trans.position;

    CheckWhetherToChase(crabHitVector, crabPlayerVector);
    CheckWhetherShotIsClear(crabHitVector, crabPlayerVector);

    return true;
  }

  private void CheckWhetherToChase(Vector3 crabHitVector, Vector3 crabPlayerVector)
  {

    if (CurrentActionState == CreatureActionState.Pursuing) return;

    if (hit.collider &&
      RaycastHittingSomething &&
      hit.collider.tag == "Terrain")
    {
      if (crabPlayerVector.magnitude > crabHitVector.magnitude)
      {
        CurrentActionState = CreatureActionState.Pursuing;
        TargetOccluded = true;
        LastObservedPlayerDirectionNormal = playerGo.GetComponent<PlayerController>().Velocity.normalized;
        bool CCW = Vector3.Cross(crabPlayerVector, LastObservedPlayerDirectionNormal).z > 0f;
        ChaseDirection = CCW ?
          new Vector3(-crabPlayerVector.y, crabPlayerVector.x) :
          new Vector3(crabPlayerVector.y, -crabPlayerVector.x);
        if (messagesDebug) Debug.Log("Hit a building");
      }
    }
  }


  private void CheckWhetherShotIsClear(Vector3 crabHitVector, Vector3 crabPlayerVector)
  {
    if (hit.collider && RaycastHittingSomething)
    {
      if (crabPlayerVector.magnitude > crabHitVector.magnitude)
      {
        CurrentActionState = CreatureActionState.Attacking;
        TargetOccluded = false;
      }
    }
  }

  private void HandleWeapon()
  {
    if(CanAttack)
    {
      WeaponWindUp();
    }
    else
    {
      WeaponWindDown();
    }
  }


  private void UpdatePosition()
  {
    if (CurrentActionState == CreatureActionState.Idle)
    {
      animator.SetFloat("Speed", 0f);
      //TODO crab idle animation
      //something with a coroutine, surely
    }

    else if (CurrentActionState == CreatureActionState.Pursuing && TargetOccluded)
    {
      transform.position += -ChaseDirection.normalized * moveSpeed * Time.deltaTime;
      animator.SetFloat("Speed", moveSpeed);
    }
  }

  private bool attackCoroutineStarted = false;

  private IEnumerator Attack()
  {
    attackCoroutineStarted = true;
    yield return null;
    attackCoroutineStarted = false;
  }

  private void WeaponWindUp()
  {
    float liftSpeed = 0.5f;
    float maxSpeed = 10f;
    float acceleration = 2f;

    if (rotor.liftParam > 0f)
      rotor.liftParam -= Time.deltaTime * liftSpeed;
    else if (rotor.liftParam < 0f)
      rotor.liftParam = 0f;
    else
    {
      if (rotor.speedParam < maxSpeed)
        rotor.speedParam += Time.deltaTime * acceleration;
    }
  }

  private void WeaponWindDown()
  {
    float liftSpeed = 0.5f;
    float acceleration = 2f;

    if (rotor.speedParam > 0f)
      rotor.speedParam -= Time.deltaTime * acceleration;
    else if (rotor.speedParam < 0f)
      rotor.speedParam = 0f;
    else
    {
      if (rotor.liftParam < 1f)
        rotor.liftParam += Time.deltaTime * liftSpeed;
    }
  }


  /// <summary>
  /// Enums specific to crab (as far as I know)
  /// </summary>
  public enum SimultaneousActionState
  {
    WindUp = 0,
    WindDown = 1,
    Loaded
  }

}
