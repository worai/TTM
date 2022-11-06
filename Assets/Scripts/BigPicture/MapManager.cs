using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.BigPicture;

public class MapManager : MonoBehaviour
{
  private MapData mapData;
  private float mapElementSideSize;

  //int selected = 0;
  //string[] options = new string[]
  //{
  //   "Option1", "Option2", "Option3",
  //};
  //selected = EditorGUILayout.Popup("Label", selected, options); 

  public enum LevelEnum // your custom enumeration
  {
    Accomodations
    , Factory
    , HighWay
  };
  public LevelEnum GenerateLevel = LevelEnum.Accomodations;


  [Tooltip("Acc, Hor, Vrt, Crs, Bld")] private GameObject[] mapElementTemplates;


  [Tooltip("output will be from e.g. 220 to 230")] [Range(0, 10f)]  private float randomStateRange = 10f;

  [Tooltip("5")]
  [SerializeField] private int mapCols = 5;
  [Tooltip("10")]
  [SerializeField] private int mapRows = 10;
  [Tooltip("Amount of decay per cell in y direction")]
  [SerializeField] private float decayGradient = 10f;


  [Tooltip("Can be used for debugging map elements and such")]
  [SerializeField] private bool deactivateTemplates = true;

  [SerializeField] private MapFacBuilder facBuilder;
  [SerializeField] private MapAccBuilder accBuilder;
  [SerializeField] private MapHghBuilder hghBuilder;

  [SerializeField] private MapContainerController mapContainer;

  [SerializeField] private int randomSeed = 2;


  private void Start()
  {
    //SpriteRenderer myRenderer = mapElementTemplates[0].GetComponent<SpriteRenderer>();
    //mapElementSideSize = myRenderer.size.x;
    //if (showMessages) Debug.Log("renderer size: " + myRenderer.size.x);
    //mapElementSideSize = 10f; //F it!

    mapData = new MapData(numCols: mapCols, numRows: mapRows, currentLevel: LevelInfos.Level);

    if(LevelInfos.Level.HasValue)
    {
      if (LevelInfos.Level.Value == 1)
        GenerateAcc();
      else if (LevelInfos.Level.Value == 2)
        GenerateFac();
      else if (LevelInfos.Level.Value == 3)
        GenerateHgh();
      //else if (LevelInfos.Level.Value == 4)
      //  GenerateDes(); //TODO map for desolation, w lotsa rubbles and such.
    }

    if (GenerateLevel == LevelEnum.Accomodations)
      GenerateAcc();
    else if (GenerateLevel == LevelEnum.Factory)
      GenerateFac();
    else if (GenerateLevel == LevelEnum.HighWay)
      GenerateHgh();
  }

  public void ResetMapContainer()
  {
    mapContainer.RemoveMapContents();
  }

  private void GenerateAcc()
  {
    SeedRandomGenerator(randomSeed);

    for (int i = 0; i < mapCols; i++)
    {
      for (int j = 0; j < mapRows; j++)
      {
        int cellState = (int) (byte.MaxValue - j * decayGradient % byte.MaxValue);
        int randValue =  (int) UnityEngine.Random.Range(-randomStateRange / 2f, randomStateRange / 2f);
        cellState = cellState + randValue > byte.MaxValue ? byte.MaxValue : cellState + randValue;

        accBuilder.BuildNode(col: i, row: j, mapElementSideSize: 10f, state: (byte)cellState);

      }
    }

    //simple construction, don't care about state/health of map-node/bigish place
    //wait, wth is this doing here? 20221001
    for (int i = 0; i < mapData.NumCols; i++)
    {
      //mapElements
    }
  }


  private void GenerateFac()
  {
    SeedRandomGenerator(randomSeed);

    int facCols = mapCols * 4;
    int facRows = mapRows * 4;



    //TileMapFac toolingTileMap = facBuilder.GenerateTooling(numColumns: facCols, numRows: facRows); //new TileMapFac(numColumns: facCols, numRows: facRows);
    TileMapFac toolingTileMap = facBuilder.GenerateDenseTooling(numColumns: facCols, numRows: facRows);
    TileMapFac pathTileMap = facBuilder.GeneratePath(numColumns: facCols, numRows: facRows); // new TileMapFac(numColumns: facCols, numRows: facRows);
    TileMapFac facTileMap = facBuilder.GenerateTileMap(toolingTileMap, numColumns: facCols, numRows: facRows); //new TileMapFac(numColumns: facCols, numRows: facRows);
    
    //for (int i = 0; i < pathTileMap.Height; i++)
    //{
    //  //pathTileMap.Set(pathTileMap.Width / 2, i, true);
    //  pathTileMap.Set(0, i, true);
    //}

    TileMapFac horObstructionsTileMap = facBuilder.GenerateHorObstructions(facTileMap, pathTileMap, numColumns: facCols, numRows: facRows);
    TileMapFac vrtObstructionsTileMap = facBuilder.GenerateVrtObstructions(facTileMap, pathTileMap, horObstructionsTileMap, numColumns: facCols, numRows: facRows);



    FacMapBuilderDto dto = new FacMapBuilderDto()
    {
      TileMap = facTileMap,
      Path = pathTileMap,
      Tooling = toolingTileMap,
      HorizontalObstructions = horObstructionsTileMap,
      VerticalObstructions = vrtObstructionsTileMap,
      TileMapCellSize = 5f
    };
    for (int i = 0; i < facTileMap.Width; i++)
    {
      for (int j = 0; j < facTileMap.Height; j++)
      {
        facBuilder.BuildNode(i, j, dto);
      }
    }

  }


  private void GenerateHgh()
  {
    
    for (int col = 0; col < mapCols; col++)
    {
      for (int row = 0; row < mapRows; row++)
      {
        hghBuilder.BuildNode(col, row, 20, byte.MaxValue);
      }
    }

  }



  ///// <summary>
  ///// 
  ///// </summary>
  ///// <param name="col"></param>
  ///// <param name="row"></param>
  ///// <param name="state">the more the healthier</param>
  //private void MakeOneCell(int col, int row, byte state)
  //{
  //  List<GameObject> mapElementList = new List<GameObject>();
  //  foreach (var mapElem in mapElementTemplates)
  //  {
  //    GameObject newGO = GameObject.Instantiate(mapElem);

  //    mapElementList.Add(newGO);
  //    newGO.SetActive(true);
  //    if (!newGO.TryGetComponent<MapNodeState>(out var _))
  //    {
  //      MapNodeState mapNodeState = newGO.AddComponent<MapNodeState>();
  //      //TODO providing level here is probably not correct?
  //      if (mapNodeState.Instantiate(state: state, level: 0))
  //      {
  //        //GetComponent<StateSpriteController>().Instantiate(mapNodeState);
  //        //try doing nothing
  //        //the state sprite controller will attempt setting the MapNodeState in itself by itself
  //      }
  //      else
  //        throw new NotImplementedException("Don't know yet how to handle a MapNodeState that has already been instantiated!");
  //    }
  //    //mapElem.transform.parent = this.gameObject.transform; //Incorrect way of setting parent, seemingly
  //    newGO.transform.SetParent(this.transform);
  //  }
  //  mapElementList[0].transform.position = new Vector3(
  //    mapElementSideSize * 2 * col, 
  //    mapElementSideSize * 2 * row
  //    );
  //  mapElementList[1].transform.position = new Vector3(
  //    mapElementSideSize * 2 * col, 
  //    mapElementSideSize * 2 * row - mapElementSideSize
  //    );
  //  mapElementList[2].transform.position = new Vector3(
  //    mapElementSideSize * 2 * col - mapElementSideSize, 
  //    mapElementSideSize * 2 * row
  //    );
  //  mapElementList[3].transform.position = new Vector3(
  //    mapElementSideSize * 2 * col - mapElementSideSize, 
  //    mapElementSideSize * 2 * row - mapElementSideSize
  //    );
  //  mapElementList[4].transform.position = new Vector3(
  //    mapElementSideSize * 2 * col,
  //    mapElementSideSize * 2 * row + +mapElementSideSize / 2
  //    );
  //}

  private void SeedRandomGenerator(int n)
  {
    UnityEngine.Random.InitState(n);
  }

}
