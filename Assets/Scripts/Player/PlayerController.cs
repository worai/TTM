using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private bool printMessages = false;

  [Tooltip("-10")]
  [SerializeField] private float gravity = -10f;

  [Tooltip("2 (Can' jump very far at all with just 2. Maybe better use a value of 3?")]
  [SerializeField] private float initJumpSpeed = 2f;
  [Tooltip("Might wanna start off as 3")]
  [SerializeField] private float walkSpeed = 3f;
  [SerializeField] private float runSpeed = 5f;
  [Space(10)]
  [SerializeField] private Animator animator;
  [SerializeField] private SpriteRenderer myRenderer;
  [SerializeField] private GameObject spriteGraphicGO;

  

  // TODO see if BalanceStartedEvent & BalanceCancelledEvent are really necessary
  [HideInInspector] public BalanceStartedEvent onStartedBalancingCoords;
  [HideInInspector] public BalanceCancelledEvent onCancelledBalancingCoords;
  [HideInInspector] public UnityEvent onStartedBalancing;
  [HideInInspector] public UnityEvent onStoppedBalancing;

  private CreatureData data;

  private float currentJumpHeight = 0f;
  private float currentVertSpeed = 0f;
  private bool touchesGround = true;

  private enum PlayerAnimationState
  {
    PlayerFalling,
    PlayerBrace,
    PlayerUnbrace,
    PlayerIdleOrWalkPrecarious,
    PlayerRunBalanced,
    PlayerIdleBalance,
    PlayerIdle,
    PlayerWalk,
    PlayerRun,
  }

  private const string FALLING_ANIMATION = "PlayerFalling";

  private string currentAnimationState;

  PlayerMovementInput pmi;

  private Vector3 _velocity;
  public Vector3 Velocity { get => _velocity; }
  public bool Falling { get; private set; }
  public bool Precarious { get; set; }
  [System.Obsolete("I think this might be superfluous")]
  public bool CantBePrecarious { get; private set; }
  public bool Bracing { get; private set; }
  public bool Balancing { get; private set; }
  public int Strength { get; private set; }
  public int Balance { get; private set; }

  private void Start()
  {
    gravity = GlobalSettings.Instance.Gravity;

    data = GetComponent<CreatureData>();
    pmi = new PlayerMovementInput();
    pmi.Enable();
    pmi.Player.Brace.started += Brace_started;
    pmi.Player.Brace.canceled += Brace_canceled;
    pmi.Player.Balance.started += Balance_started;
    pmi.Player.Balance.canceled += Balance_cancelled;
  }

  private void FixedUpdate()
  {
    //TODO check if dead
    HandleMovement();

    HandleBracingAnimator();

    HandleJumping();

    HandlePrecariousAnimationState();

    HandleAnimationChanges();
  }

  private void HandleAnimationChanges()
  {

    if(Precarious)
    {
      if(!Balancing)
        ChangeAnimationState(PlayerAnimationState.PlayerIdleOrWalkPrecarious.ToString());
      else if(Balancing)
      {
        if(Velocity.magnitude > 0.1f)
          ChangeAnimationState(PlayerAnimationState.PlayerRunBalanced.ToString());
        else if (Velocity.magnitude < 0.1f)
          ChangeAnimationState(PlayerAnimationState.PlayerIdleBalance.ToString());
      }
    }
    else if (!Precarious)
    {
      if(Velocity.magnitude < 0.1f)
      {
        ChangeAnimationState(PlayerAnimationState.PlayerIdle.ToString());
      }
      else if (Velocity.magnitude < walkSpeed * 1.1f)
      {
        ChangeAnimationState(PlayerAnimationState.PlayerWalk.ToString());
      }
      else if (Velocity.magnitude < runSpeed * 1.1f)
      {
        ChangeAnimationState(PlayerAnimationState.PlayerRun.ToString());
      }
    }

    //ChangeAnimationState(PlayerAnimationState.PlayerBrace.ToString());
    //ChangeAnimationState(PlayerAnimationState.PlayerUnbrace.ToString());
  }

  void ChangeAnimationState(string newState)
  {
    if (currentAnimationState == newState) return;

    animator.Play(newState);

    currentAnimationState = newState;
  }

  private void HandlePrecariousAnimationState()
  {
    if (Precarious && currentJumpHeight < 0.1f)
      animator.SetBool("Precarious", true);
    else
      animator.SetBool("Precarious", false);
  }

  /// <summary>
  /// Initialises a coroutine for falling, handling all logic and animation internally
  /// </summary>
  internal void Fall()
  {
    Falling = true;
    LevelInfos.Fell = true;
    if (!runningFallingCoroutine) StartCoroutine(FallingCoroutine());
  }


  bool runningFallingCoroutine = false;

  private IEnumerator FallingCoroutine()
  {
    runningFallingCoroutine = true;
    Falling = true;
    animator.Play(FALLING_ANIMATION);
    float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;
    yield return new WaitForSeconds(waitTime);

    runningFallingCoroutine = false;
    SceneLoader.Load(SceneLoader.Scene.TransitionScene);
  }


  private void HandleBracingAnimator()
  {
    if (Precarious) Bracing = false;
    //animator.SetBool("Bracing", Bracing);
  }

  private void Brace_started(InputAction.CallbackContext context)
  {
    Debug.Log("Bracing started");
    if (Precarious) Bracing = false;
    Bracing = true;
  }


  private void Brace_canceled(InputAction.CallbackContext obj)
  {
    Debug.Log("Bracing stopped");
    Bracing = false;
  }


  private void Balance_started(InputAction.CallbackContext context)
  {
    if (Balance > 0)
    {
      Debug.Log("Balance started");
      Balancing = true;
      onStartedBalancing?.Invoke();
      //animator.SetBool("Balancing", true);
    }
    else
    {
      Debug.Log("Can't balance");
    }

  }

  private void Balance_cancelled(InputAction.CallbackContext obj)
  {
    //Always wanna be able to cancel the balancing thing at least.
    Debug.Log("Balance cancelled");
    Balancing = false;
    onStoppedBalancing?.Invoke();
    animator.SetBool("Balancing", false);
  }


  private void HandleMovement()
  {
    if (data.IsDead || Falling) return;
    float _speed = walkSpeed * (!Balancing && Precarious && currentJumpHeight < 0.1f ? 0.2f : 1f);

    if(!Bracing)
    {
      Vector3 velocity = new Vector3(
            pmi.Player.Horizontal.ReadValue<float>(), // Input.GetAxis("Horizontal"), 
            pmi.Player.Vertical.ReadValue<float>(),
            0f);
      if (velocity.x < 0f) myRenderer.flipX = true;
      else if (velocity.x > 0f) myRenderer.flipX = false;
      animator.SetFloat("Speed", velocity.magnitude);
      Vector2 fakeNormal = FakeNormalise(new Vector2(velocity.x, velocity.y));
      velocity = new Vector3(fakeNormal.x, fakeNormal.y);
      _velocity = velocity * _speed;
    }
    else
    {
      _velocity = new Vector3();
    }
    transform.position += Time.fixedDeltaTime * _velocity;
  }

  private void HandleJumping()
  {
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
    if (printMessages) Debug.Log(" - height " + currentJumpHeight + "\t current vert speed" + currentVertSpeed);
  }

  private void UpdateHeight()
  {
    spriteGraphicGO.transform.position = transform.position + new Vector3() { y = currentJumpHeight };
  }


  //TODO move this to util class.
  /// <summary>
  /// The name is confusing for a reason.
  /// The 'normalization' is not done s.t. the vector gets unit length,
  /// but so that the length of the input vector lands at least somewhat on a circle.
  /// Else diagonal movelements will be sqrt(2) times faster than other movement.
  /// </summary>
  /// <param name="v"></param>
  /// <returns>a vector between lenght v.normalized and v.normalized / sqrt(2), 
  /// depending on whether the vector points parallel to an axis, or if it's diagonal.</returns>
  private Vector2 FakeNormalise(Vector2 v) 
  {
    float length;
    if (v.magnitude == 0f || v.x == 0f || v.y == 0f) return v;
    
    length = Mathf.Abs( (v.x * v.y) * Mathf.Sqrt(  0.5f * (1 / (v.x * v.x)) + (1 / (v.y * v.y))  ) );
    return v.normalized * length;
  }



  public void Jump()
  {
    if(touchesGround)
    {
      currentVertSpeed = initJumpSpeed;
    }
  }

  public void PickedUpStrengthPowerup()
  {
    Strength++;
  }

  public void PickedUpBalancePowerup()
  {
    Balance++;
  }


}
