using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slugger : AWeapon2, IWeapon
{

  [SerializeField] private float lowerBound = 25f;
  [SerializeField] private float upperBound = 75f;
  [SerializeField] private GameObject effect;
  [SerializeField] private Sprite iconSprite;
  [SerializeField] private Sprite inGameSprite;

  private RaycastHit2D hit;

  public bool Ready { get => throw new System.NotImplementedException(); private set => throw new System.NotImplementedException(); }

  public Sprite IconSprite { get => iconSprite; }

  public Sprite InGameSprite => inGameSprite;

  public bool Fire(Transform firePoint)
  {
    if (!Ready) return false;
    int layerMask = LayerMask.GetMask(new string[] { "Hit" });


    //hit = Physics2D.Raycast(firePoint.position, firePoint.right);
    hit = Physics2D.Raycast(firePoint.position, firePoint.right, distance: 1000f, layerMask: layerMask);

    Debug.DrawLine(firePoint.position, firePoint.right * 100f, Color.white);

    //TODO use some interface like ITakeDamage?
    if (hit && hit.transform.TryGetComponent<CreatureData>(out CreatureData data))
    {
      data.TakeDamage(Random.Range(lowerBound, upperBound));
    }

    return true;
  }

  public void MakeEffect()
  {
    if (!hit || hit.transform == null)
      return;
  }

  public void MakeReady(float duration)
  {
    throw new System.NotImplementedException();
  }


  public override string ToString()
  {
    return "Slugger";
  }

}
