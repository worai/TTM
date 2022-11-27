using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadyFX : MonoBehaviour
{

  [SerializeField] float displacement = 1.5f;

  private Animator myAnimator;
  private SpriteRenderer myRenderer;
  PlayerWeaponInput input;

  private void Awake()
  {
    input = new PlayerWeaponInput();
    input.Enable();
    myAnimator = GetComponent<Animator>();
    myRenderer = GetComponent<SpriteRenderer>();
    myRenderer.enabled = false;
    myAnimator.enabled = false;
  }

  public void PlayAnimation(InputAction.CallbackContext context)
  {
    Debug.Log("Playing ready animation");
    if(context.started)
    {
      Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(input.Player.MousePosition.ReadValue<Vector2>());
      Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

      myAnimator.enabled = true;
      myRenderer.enabled = true;
      myAnimator.Play("Ready", -1, 0f);

      transform.localPosition = -direction * displacement;
    }
    else if (context.canceled)
    {
      myAnimator.playbackTime = 0f;
      myAnimator.enabled = false;
      myRenderer.enabled = false;
    }

    //can I get away with only one state name?
  }

}
