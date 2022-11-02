using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private bool printMessages = false;


  [Tooltip("-10")]
  [SerializeField] private float gravity = -10f;

  [Tooltip("2 (Can' jump very far at all with just 2. Maybe better use a value of 3?")]
  [SerializeField] private float initJumpSpeed = 2f;
  [Tooltip("Might wanna start off as 3")]
  [SerializeField] private float speed = 5f;
  [Space(10)]
  [SerializeField] private Animator animator;
  [SerializeField] private SpriteRenderer myRenderer;
  [SerializeField] private GameObject spriteGraphicGO;


  private CreatureData data;

  private float currentJumpHeight = 0f;
  private float currentVertSpeed = 0f;
  private bool touchesGround = true;

  private const string FALLING_ANIMATION = "PlayerFalling";

  PlayerMovementInput pmi;

  private Vector3 _velocity;
  public Vector3 Velocity { get => _velocity; }
  public bool Falling { get; internal set; }
  public bool Precarious { get; set; }
  public bool Bracing { get; private set; }
  public bool Balancing { get; private set; }

  private void Start()
  {
    data = GetComponent<CreatureData>();
    pmi = new PlayerMovementInput();
    pmi.Enable();
    pmi.Player.Brace.started += Brace_started;
    pmi.Player.Brace.canceled += Brace_canceled;
    pmi.Player.Balance.started += Balance_started;
    pmi.Player.Balance.canceled += Balance_canceled;
  }

  private void FixedUpdate()
  {
    //TODO check if dead
    HandleMovement();

    HandleBracingAnimator();

    HandleJumping();

    HandlePrecariousAnimationState();

  }

  private void HandlePrecariousAnimationState()
  {
    if (Precarious && currentJumpHeight < 0.1f)
      animator.SetBool("Precarious", true);
    else
      animator.SetBool("Precarious", false);
  }

  /// <summary>
  /// Initialises a coroutine for falling 
  /// </summary>
  internal void Fall()
  {
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
  }


  private void HandleBracingAnimator()
  {
    if (Precarious) Bracing = false;
    animator.SetBool("Bracing", Bracing);
  }

  private void Brace_started(InputAction.CallbackContext context)
  {
    Debug.Log("Bracing started");
    if (Precarious) Bracing = false;
    Bracing = true;
  }


  private void Brace_canceled(InputAction.CallbackContext obj)
  {
    Bracing = false;
  }


  private void Balance_started(InputAction.CallbackContext context)
  {
    Balancing = true;
    animator.SetBool("Balancing", true);
  }

  private void Balance_canceled(InputAction.CallbackContext obj)
  {
    Balancing = false;
    animator.SetBool("Balancing", false);
  }


  private void HandleMovement()
  {
    if (data.IsDead || Falling) return;
    float _speed = speed * (!Balancing && Precarious && currentJumpHeight < 0.1f ? 0.2f : 1f);

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


}
