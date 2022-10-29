using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used for map node element prefabs
/// </summary>
public class StateSpriteController : MonoBehaviour
{
  [Tooltip("Make sure there are only 5 states (clean, + 4 damaged) that this object can take")] [SerializeField] private Sprite[] sprites;
  [Range(0, 2)]
  [SerializeField] private byte level = 0;
  [Space(10)]
  [SerializeField] [Tooltip("Each layer is overlayed the old one")] private bool overlay = false;
  [SerializeField] [Tooltip("Only used if 'overlay' is checked.")] GameObject[] spriteGO_s;

  private byte[] thresholds;

  private SpriteRenderer myRenderer;
  private MapNodeState state;
  private MapNodeThresholdManager threshManager;



  private void Start()
  {
    threshManager = GetComponentInParent<MapNodeThresholdManager>();

    //can't be bothered to assign this for every single element prefab
    myRenderer = GetComponent<SpriteRenderer>();

    thresholds = threshManager.GetThresholdsForLevel(level);
    
    TryUpdateSprite();


  }

  private void Update()
  {
    if (!overlay)
      TryUpdateSprite();
    else
      TryUpdateLayers();
  }

  private void TryUpdateSprite()
  {
    //try to set state, if we can't then don't bother... heh
    if (state == null)
      state = GetComponent<MapNodeState>();
    if(state == null) return;

    for (int i = 0; i < sprites.Length; i++)
    {
      if (state.Health > thresholds[i])
      {
        myRenderer.sprite = sprites[i];
        break;
      }
    }
    //if it's below some kinda threshold then we wanna destroy stuff...
    //that's taken care of the BuildingDestroyer
  }

  private void TryUpdateLayers()
  {
    //try to set state, if we can't then don't bother... heh
    if (state == null)
      state = GetComponent<MapNodeState>();
    if (state == null) return;

    for (int i = 0; i < spriteGO_s.Length; i++)
    {
      if (state.Health < thresholds[i*2])
      {
        spriteGO_s[i].SetActive(true);
      }
      else
        break;
    }
  }


  internal void Instantiate(MapNodeState state)
  {
    if (this.state == null)
      this.state = state;
    else
      Debug.LogError("Already instantiated");
  }
}
