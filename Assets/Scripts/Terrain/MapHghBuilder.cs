using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHghBuilder : MonoBehaviour
{

  [Tooltip("Crs, Hor, Vert, Vert entr")]
  [SerializeField] private GameObject[] mapElementTemplates;
  [SerializeField] bool deactivateTemplates = true;

  private float totalHeight;
  private float totalWidth;

  private void Start()
  {
    mapElementTemplates = new GameObject[transform.childCount];
    for (int i = 0; i < transform.childCount; i++)
    {
      mapElementTemplates[i] = transform.GetChild(i).gameObject;
    }

    SpriteRenderer myRenderer0 = mapElementTemplates[0].GetComponent<SpriteRenderer>();
    SpriteRenderer myRenderer1 = mapElementTemplates[1].GetComponent<SpriteRenderer>();
    SpriteRenderer myRenderer2 = mapElementTemplates[2].GetComponent<SpriteRenderer>();
    SpriteRenderer myRenderer3 = mapElementTemplates[3].GetComponent<SpriteRenderer>();

    totalHeight = myRenderer0.sprite.rect.height / myRenderer0.sprite.pixelsPerUnit +
                        //myRenderer1.sprite.rect.height / myRenderer1.sprite.pixelsPerUnit + //horizontal
                        myRenderer2.sprite.rect.height / myRenderer2.sprite.pixelsPerUnit +
                        myRenderer3.sprite.rect.height / myRenderer3.sprite.pixelsPerUnit;

    totalWidth =  myRenderer0.sprite.rect.width / myRenderer0.sprite.pixelsPerUnit +
                  myRenderer1.sprite.rect.width / myRenderer1.sprite.pixelsPerUnit;


    //set any active templates inactive
    if (deactivateTemplates)
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        transform.GetChild(i).gameObject.SetActive(false);
      }
    }

  }

  internal void BuildNode(int col, int row, float mapElementSideSize, byte state)
  {
    //TODO use state

    //if (guyToInstantiate.TryGetComponent(out SpriteRenderer myRenderer))
    //  Debug.Log("size? " + myRenderer.sprite.rect.height / myRenderer.sprite.pixelsPerUnit);
    //else Debug.Log("couldn't fetch");

    
    float x, y;
    
    //crs
    GameObject newGO = Instantiate(mapElementTemplates[0]);
    x = col * totalWidth;
    y = row * totalHeight;
    newGO.transform.position = new Vector3(x, y);
    newGO.SetActive(true);
    

    //hor
    newGO = Instantiate(mapElementTemplates[1]);
    x = col * totalWidth + 30f;
    y = row * totalHeight;
    newGO.transform.position = new Vector3(x, y);
    newGO.SetActive(true);
    
    //vrt
    newGO = Instantiate(mapElementTemplates[2]);
    x = col * totalWidth;
    y = row * totalHeight + 30f;
    newGO.transform.position = new Vector3(x, y);
    newGO.SetActive(true);
    
    //vrt
    newGO = Instantiate(mapElementTemplates[3]);
    x = col * totalWidth;
    y = row * totalHeight + 50f;
    newGO.transform.position = new Vector3(x, y);
    newGO.SetActive(true);
  }



}
