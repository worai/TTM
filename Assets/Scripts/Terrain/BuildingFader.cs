using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFader : MonoBehaviour
{
  private SpriteRenderer myRenderer;
  private float buildingY;
  [Range(0f, 10f)]
  [SerializeField] private float transparencyDistance = 10f;
  [SerializeField] private float fadeShift = -5f;
  [SerializeField] private Transform playerTrans;

  private void Start()
  {
    myRenderer = GetComponent<SpriteRenderer>();
    playerTrans = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    buildingY = transform.position.y;
  }

  private void Update()
  {
    float playerY = playerTrans.position.y;
    float transparency = Mathf.Max(0f, playerY - buildingY + fadeShift); // [0, inf]
    float opacity = Mathf.Min(transparencyDistance - transparency, transparencyDistance) / transparencyDistance;
    Color color = myRenderer.color;
    color.a = opacity;
    myRenderer.color = color;
  }

}
