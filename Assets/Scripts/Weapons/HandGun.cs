using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : AWeapon1, IWeapon
{
  private bool verboseMessages = true;

  [SerializeField] private float reloadDuration = 1f;
  [SerializeField] private float lowerBound = 25f;
  [SerializeField] private float upperBound = 75f;
  [SerializeField] private GameObject effect;
  [SerializeField] private Sprite iconSprite;
  [SerializeField] private Sprite inGameSprite;


  private RaycastHit2D hit;


  public bool Ready { get; private set ; }
  public Sprite IconSprite { get => iconSprite; }
  public Sprite InGameSprite => inGameSprite;


  public bool Fire(Transform firePoint)
  {
    if(!Ready)
    {
      //TODO sound fx for not ready?
      if (verboseMessages) Debug.Log("Can't fire...");
      return false;
    }

    //https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

    ////LAYER 8 won't be seen. Layer 8 is the "Hit" layer...
    //int layerMask = 1 << 8; //LayerMask.GetMask( (new string[] { "HitCreature"});

    ////only layer 8 is seen
    //layerMask = ~layerMask;


    //Only these guys are hit!
    int layerMask = LayerMask.GetMask(new string[] { "Hit" });

    if (verboseMessages) Debug.Log("Handgun fired");
    //hit = Physics2D.Raycast(firePoint.position, firePoint.right);
    hit = Physics2D.Raycast(firePoint.position, firePoint.right, distance:1000f, layerMask: layerMask);
    
    Debug.DrawLine(  firePoint.position, firePoint.right * 100f, Color.white);

    //TODO use some interface like ITakeDamage?
    if(hit && hit.transform.TryGetComponent<CreatureData>(out CreatureData data))
    {
      if(verboseMessages) Debug.Log("hit thing " + hit.transform.name);
      data.TakeDamage(Random.Range(lowerBound, upperBound));
    }

    soundManager.Play("Fire");

    Ready = false;

    return true;

  }

  public void MakeEffect()
  {
    if (!hit || hit.transform == null) 
      return;
    else
    {
      GameObject newGO = Instantiate(effect);
      newGO.SetActive(true);
      newGO.transform.position = hit.point; // hit.transform.position;
    }
  }

  public void MakeReadyStarted()
  {
    soundManager.Play("Ready");
  }

  public void MakeReadyCancelled()
  {
    soundManager.Stop("Ready");
  }

  public bool MakeReady(float duration)
  {
    if (verboseMessages) Debug.Log("Trying to make handgun ready");
    if (!Ready && duration > reloadDuration)
    {
      //TODO some more effect or sth like it... sound?
      if (verboseMessages) Debug.Log("handgun is now ready");
      Ready = true;
    }
    return Ready;
  }

  public override string ToString()
  {
    return "Handgun";
  }

  


}
