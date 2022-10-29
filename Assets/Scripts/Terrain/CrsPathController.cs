using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrsPathController : MonoBehaviour
{

  [Tooltip("Mainly for when the vehicle leaves the place")] [SerializeField] private float collisionMargin = 1f;

  private Vector3[] turningCorners;


  private void Start()
  {
    SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
    float sideSize = myRenderer.sprite.rect.width / myRenderer.sprite.pixelsPerUnit;
    turningCorners = new Vector3[4];
    Vector3 pos = transform.position;
    turningCorners[0] = new Vector3(pos.x + sideSize / 2, pos.y + sideSize / 2);
    turningCorners[1] = new Vector3(pos.x + sideSize / 2, pos.y - sideSize / 2);
    turningCorners[2] = new Vector3(pos.x - sideSize / 2, pos.y + sideSize / 2);
    turningCorners[3] = new Vector3(pos.x - sideSize / 2, pos.y - sideSize / 2);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {

    if(collision.gameObject.transform.tag == "Truck" && collision.TryGetComponent(out TruckController controller))
    {
      //going upwards, turning right
      Debug.Log("A truck enters!"); 
      controller.TryUpdateState(TruckController.TruckState.TurningRight);
      controller.TurningRadiusForRadVelocity = Mathf.Abs(controller.gameObject.transform.position.x - turningCorners[1].x);
      controller.OldCoords = turningCorners[1] + new Vector3(-controller.gameObject.transform.position.x, 0f);
      controller.TurningPoint = turningCorners[1];
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.gameObject.transform.tag == "Truck" && collision.TryGetComponent(out TruckController controller))
    {
      if (controller.CurrentTruckState == TruckController.TruckState.TurningRight)
      {
        Debug.Log("A truck leaves!");
        if (controller.TryUpdateState(TruckController.TruckState.Right))
        {
          controller.transform.position = new Vector3(
            turningCorners[1].x + collisionMargin,
            turningCorners[1].y + controller.TurningRadiusForRadVelocity
            );
          controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        Debug.Log("What should I do if the state has already been set?");
        return;
      }
      //TODO other directions.
    }
  }
}
