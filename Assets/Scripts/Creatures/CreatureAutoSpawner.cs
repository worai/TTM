using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAutoSpawner : MonoBehaviour
{
  [SerializeField] private AnimationClip spawnAnimation;
  [Space(10)]
  [SerializeField] private BoxCollider2D collider;
  [SerializeField] private CreatureData data;
  [SerializeField] private Animator myAnimator;
  [SerializeField] private SpriteRenderer myRenderer;

  [SerializeField] private bool alreadySpawned = false;
  public bool AlreadySpawned => alreadySpawned;
  private bool spawnInitiated = false;
  public bool SpawnInitiated => spawnInitiated;

  [SerializeField] private bool showMessages = false;


  private void Start()
  {
    if (!spawnInitiated && !alreadySpawned)
    {
      //myRenderer.enabled = false;
      myRenderer.gameObject.SetActive(false);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.gameObject.TryGetComponent(out PlayerController _))
    {
      myRenderer.gameObject.SetActive(true);
      spawnInitiated = true;
      myAnimator.SetBool("Spawning", true);
    }
  }


  private void Update()
  {
    string _name =  spawnAnimation.name;
    if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName(_name))
    {
      if(showMessages) Debug.Log(_name + " spawning, " + myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

      if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName(_name ))
      { 
        if (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) 
        {
          alreadySpawned = true;
          collider.enabled = false;
          myAnimator.SetBool("Spawning", false);
        }
      }

    }
  }

  //private IEnumerator Spawn()
  //{
  //  yield return new wai
  //}


}
