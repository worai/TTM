using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO remove
/// </summary>
[Obsolete("Not needed")]
public class PostCollisionTransformUpdater : MonoBehaviour
{
  //[SerializeField] private Collider2D collider;
  //[SerializeField] private Transform _parentTrans;

  //private Transform _trans;

  private void Start()
  {
    //collider = GetComponent<Collider2D>();
    //_trans = transform;
    //_parentTrans = _trans.parent.transform;
  }

  private void FixedUpdate()
  {
    //UpdatePosition();
  }


  //private void OnCollisionStay2D(Collision2D collision)
  //{
  //  //_parentTrans.position = _trans.position;
  //  UpdatePosition();
  //}

  //private void OnCollisionExit2D(Collision2D collision)
  //{
  //  //_parentTrans.position = _trans.position;
  //  UpdatePosition();
  //}

  //private void UpdatePosition()
  //{
  //  //return; 
  //  _trans.position = _parentTrans.position;
  //  _parentTrans.position = _parentTrans.position - _trans.position;
  //  _trans.localPosition = new Vector3();
  //}
}
