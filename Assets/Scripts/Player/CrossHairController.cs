using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{

  private PlayerWeaponInput pwi;
  private Transform _trans;

  [SerializeField] private SpriteRenderer _renderer;
  [SerializeField] Vector2 hotspotPosition = new Vector2(8f, 8f);
  [SerializeField] Texture2D texture;

  void Awake()
  {
    _trans = transform;
    pwi = new PlayerWeaponInput();
    //Cursor.visible = false;

    Cursor.SetCursor(texture, hotspotPosition, CursorMode.Auto);
  }


  //void Update()
  //{
  //  //Vector2 mouseCursorPositon = Camera.main.ScreenToWorldPoint(pwi.Player.MousePosition.ReadValue<Vector2>());
  //  //_trans.position = mouseCursorPositon;
  //}
}
