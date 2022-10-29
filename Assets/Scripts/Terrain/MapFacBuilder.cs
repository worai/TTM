using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFacBuilder : MonoBehaviour
{
  public GameObject factoryPlacementERROR;

  [Tooltip("This guys must have the empy non-walled tile sorted FIRST!")]
  public TileNeighbours[] tileArray;

  public TileNeighbours[] obstructions;
  [Space(10)]
  [SerializeField] GameObject outsideFloor;
  [SerializeField] GameObject verticalObstruction;
  [SerializeField] GameObject horizontalObstruction;
  [Space(10)]
  [SerializeField] bool deactivateTemplates = true;
  [SerializeField] private Transform mapContainerTrans;

  [SerializeField] private float toolingDensity = 0.1f;
  [SerializeField] private float toolingContinuationProb = 0.9f;

  private void Start()
  {
    //set any active templates inactive
    if (deactivateTemplates)
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        transform.GetChild(i).gameObject.SetActive(false);
      }
    }
  }



  internal TileMapFac GenerateTooling(int numColumns, int numRows)
  {
    TileMapFac resultTooling = new TileMapFac(numColumns, numRows);

    //generate seeds
    int numSeeds = numColumns * numRows;
    int col,row, a,b;
    for (int i = 0; i < numSeeds * toolingDensity; i++)
    {
      col = UnityEngine.Random.Range(0, numColumns);
      row = UnityEngine.Random.Range(0, numRows);
      resultTooling.Set(col, row, true);
      int direction = UnityEngine.Random.Range(0, 4) * 2 + 1;
      a = col; b = row;

      //flip, if too close to either edge
      if ((col < numColumns / 2) && (direction == 3)) 
        direction = 5;
      else if ((col > numColumns / 2) && (direction == 5)) 
        direction = 3;

      while (WithinMap(a,b,resultTooling) && UnityEngine.Random.Range(0f, 1f) < toolingContinuationProb)
      {
        resultTooling.Set(a, b, true);
        GetInDirection(direction, col, row, out a, out b);
        col = a;
        row = b;
      }
    }

    CleanUp(resultTooling);

    return resultTooling;
  }

  /// <summary>
  /// this will prob break everything...
  /// </summary>
  /// <param name="i"></param>
  /// <param name="j"></param>
  /// <param name="toolingMap"></param>
  private void TryFixToolingTooDense(int i, int j, TileMapFac toolingMap)
  {
    //confirming first whether i,j contains any tooling
    if (!toolingMap.Get(i, j)) return;

    int a, b;
    int alpha, beta;
    int x, y;
    for (int direction = 0; direction < 9; direction++)
    {
      GetInDirection(direction, i, j, out a, out b);
      if(toolingMap.Get(a,b))
      {
        //I don't think diagonals can be allowed 
        if (direction % 2 == 0 && a!=i && b!=j) 
          toolingMap.Set(a, b, false);

        int directionAlpha;
        int directionBeta;
        if(direction % 6 == 1)
        {
          directionAlpha = 3;
          directionBeta = 5;
        }
        else
        {
          directionAlpha = 1;
          directionBeta = 7;
        }

        GetInDirection(directionAlpha, i, j, out alpha, out beta);
        GetInDirection(directionBeta, i, j, out x, out y);
        if (toolingMap.Get(alpha, beta) || toolingMap.Get(x, y))
        {
          int alif, ba;
          for (int directionAlif = 0; directionAlif < 9; directionAlif++)
          {
            GetInDirection(directionAlif, i, j, out alif, out ba);
            toolingMap.Set(alif, ba, false);
          }
        }

      }
    }
  }

  internal TileMapFac GenerateDenseTooling(int numColumns, int numRows)
  {
    TileMapFac resultTooling = new TileMapFac(numColumns, numRows);

    //generate seeds
    //int numSeeds = numColumns * numRows;
    int colS, rowS, colE, rowE;

    bool horizontal = UnityEngine.Random.Range(0, 2) > 0;
    horizontal = true;

    for (int rowIndex = 2; rowIndex < numRows;) //index incremented 'twice' at end brackets
    {

      if (horizontal)
      {
        colS = UnityEngine.Random.Range(1, numColumns - 2);
        colE = UnityEngine.Random.Range(1, numColumns - 2);
        SortSwap(ref colS, ref colE);
        colS = Mathf.Min(colS, numColumns / 2);
        colE = Mathf.Max(colE, numColumns / 2);
        for (int colIndex = colS; colIndex < colE; colIndex++)
        {
          resultTooling.Set(colIndex, rowIndex, true);
        }
      }
      else
      {
        rowS = UnityEngine.Random.Range(rowIndex, numRows - 2);
        rowE = UnityEngine.Random.Range(rowIndex, numRows - 2);
        SortSwap(ref rowS, ref rowE);
        int startGenerateRow, endGenerateRow;
        for (int colIndex = 2; colIndex < numColumns;) //index incremented 'twice' at end of brackets
        {
          startGenerateRow = UnityEngine.Random.Range(rowIndex, numRows - 2);
          endGenerateRow = UnityEngine.Random.Range(rowIndex, numRows - 2);
          SortSwap(ref startGenerateRow, ref endGenerateRow);
          for (int innerRowIndex = startGenerateRow; innerRowIndex < endGenerateRow; innerRowIndex++)
          {
            resultTooling.Set(colIndex, innerRowIndex, true);
          }
          colIndex += 2;
        }
      }

      //horizontal or vertical strips
      horizontal = UnityEngine.Random.Range(0, 2) > 0;
      horizontal = true;
      rowIndex += 2;
      rowIndex += UnityEngine.Random.Range(0, 3);
    }

    CleanUp(resultTooling);

    return resultTooling;
  }

  private void CleanUp(TileMapFac resultTooling)
  {
    for (int i = 0; i < resultTooling.Width; i++)
    {
      resultTooling.Set(i, 0, false);
      resultTooling.Set(i, resultTooling.Height - 1, false);
      resultTooling.Set(0, i, false);
      resultTooling.Set(resultTooling.Width - 1, i, false);
    }
    for (int i = 0; i < resultTooling.Width; i++)
    {
      for (int j = 0; j < resultTooling.Height; j++)
      {
        TryFixToolingTooDense(i, j, resultTooling);
      }
    }
  }

  internal TileMapFac GeneratePath(int numColumns, int numRows)
  {
    TileMapFac resultPath = new TileMapFac(numColumns, numRows);

    int col = UnityEngine.Random.Range(0, numColumns); 
    int row = 0;
    resultPath.Set(col, row, true);


    bool endOfPath = false;
    bool up = UnityEngine.Random.Range(0f, 1f) > 0.5f;
    while(!endOfPath)
    {
      if(up)
      {
        up = GeneratePathUp(resultPath, col, numRows, ref row, ref endOfPath);
      }
      else if(!up)
      {
        up = GeneratePathSideways(numColumns, resultPath, ref col, row, ref endOfPath);
      }
    }

    return resultPath;
  }


  private bool GeneratePathUp(TileMapFac resultPath, int col, int numRows, ref int row, ref bool endOfPath)
  {
    bool up;
    int numSteps = UnityEngine.Random.Range(2, 5);
    for (int _ = 0; _ < numSteps; _++)
    {
      row++;
      //if (row > numRows)
      //{
      //  row--;
      //  break;
      //}
      if (!WithinMap(col, row, resultPath))
      {
        endOfPath = true;
        break;
      }
      resultPath.Set(col, row, true);
    }
    up = false;
    return up;
  }



  private bool GeneratePathSideways(int numColumns, TileMapFac resultPath, ref int col, int row, ref bool endOfPath)
  {
    bool up;
    int increment;
    int numSteps;
    if (col < numColumns / 4)
    {
      increment = 1;
      numSteps = UnityEngine.Random.Range(col, numColumns);
    }
    else if (col > 3 * numColumns / 4)
    {
      increment = -1;
      numSteps = UnityEngine.Random.Range(0, col);
    }
    else
    {
      //somewhere middle-ish
      increment = UnityEngine.Random.Range(0f, 1f) > 0.5f ? -1 : 1;
      if (increment > 0)
        numSteps = UnityEngine.Random.Range(1, numColumns-1-col);
      else
        numSteps = UnityEngine.Random.Range(1, col);
    }
    for (int _ = 0; _ < numSteps; _++)
    {
      col += increment;
      if (!WithinMap(col, row, resultPath))
      {
        //up = true;
        break;
      }
      resultPath.Set(col, row, true);
    }
    up = true;
    return up;
  }


  internal TileMapFac GenerateTileMap(TileMapFac toolingTileMap, int numColumns, int numRows)
  {
    TileMapFac resultMap = new TileMapFac(numColumns, numRows);
    int a, b;
    for (int col = 0; col < numColumns; col++)
    {
      for (int row = 0; row < numRows; row++)
      {
        if(toolingTileMap.Get(col,row))
        {
          for (int direction = 0; direction < 9; direction++)
          {
            GetInDirection(direction, col, row, out a, out b);
            resultMap.Set(a, b, true);
          }
          //if (toolingTileMap.Get(col, row + 1)) row++;
        }
      }
    }
    return resultMap;
  }



  internal TileMapFac GenerateHorObstructions(TileMapFac facTileMap, TileMapFac pathTileMap, int numColumns, int numRows)
  {
    TileMapFac resultMap = new TileMapFac(numColumns, numRows);

    for (int col = 0; col < numColumns; col++)
    {
      for (int row = 0; row < numRows; row++)
      {
        if (!facTileMap.Get(col, row)) continue;
        TryAddLeftCandidates(facTileMap, pathTileMap, resultMap, col, row);
        TryAddRightCandidates(facTileMap, pathTileMap, resultMap, col, row, numColumns);
      }
    }
    return resultMap;
  }


  private void TryAddLeftCandidates(TileMapFac facTileMap, TileMapFac pathTileMap, TileMapFac resultMap, int col, int row)
  {
    if (!WithinMap(col - 1, row, facTileMap)) return;
    if (facTileMap.Get(col - 1, row)) return;
    List<Vector2Int> candidatesLeft = new List<Vector2Int>();
    for (int colIndex = col - 1; colIndex >= 0; colIndex--)
    {
      if (pathTileMap.Get(colIndex, row))
      {
        candidatesLeft = new List<Vector2Int>();
        break;
      }
      if (WithinMap(colIndex, row, facTileMap)) candidatesLeft.Add(new Vector2Int(colIndex, row));
    }
    foreach (Vector2Int pts in candidatesLeft)
    {
      resultMap.Set(pts.x, pts.y, true);
    }
  }


  private void TryAddRightCandidates(TileMapFac facTileMap, TileMapFac pathTileMap, TileMapFac resultMap, int col, int row, int numColumns)
  {
    if (!WithinMap(col + 1, row, facTileMap)) return;
    if (facTileMap.Get(col + 1, row)) return;
    List<Vector2Int> candidatesLeft = new List<Vector2Int>();
    for (int colIndex = col + 1; colIndex < numColumns; colIndex++)
    {
      if (pathTileMap.Get(colIndex, row))
      {
        candidatesLeft = new List<Vector2Int>();
        break;
      }
      if (WithinMap(colIndex, row, facTileMap)) candidatesLeft.Add(new Vector2Int(colIndex, row));
    }
    foreach (Vector2Int pts in candidatesLeft)
    {
      resultMap.Set(pts.x, pts.y, true);
    }
  }


  internal TileMapFac GenerateVrtObstructions(TileMapFac facTileMap, TileMapFac pathTileMap, TileMapFac horObstructionsTileMap, int numColumns, int numRows)
  {
    TileMapFac resultMap = new TileMapFac(numColumns, numRows);

    for (int col = 0; col < numColumns; col++)
    {
      for (int row = 0; row < numRows; row++)
      {
        if (!facTileMap.Get(col, row)) continue;
        TryAddBotCandidates(facTileMap, pathTileMap, horObstructionsTileMap, resultMap, col, row);
        //TryAddRightCandidates(facTileMap, pathTileMap, resultMap, col, row, numColumns);
      }
    }
    return resultMap;
  }

  private void TryAddBotCandidates(TileMapFac facTileMap, TileMapFac pathTileMap, TileMapFac horObstructionsTileMap, TileMapFac resultMap, int col, int row)
  {
    if (!WithinMap(col, row - 1, facTileMap)) return;
    if (facTileMap.Get(col, row-1)) return;
    List<Vector2Int> candidatesDown = new List<Vector2Int>();
    for (int rowIndex = row - 1; rowIndex >= 0; rowIndex--)
    {
      if(horObstructionsTileMap.Get(col, rowIndex) || pathTileMap.Get(col, rowIndex))
      {
        candidatesDown = new List<Vector2Int>();
        break;
      }
      if (facTileMap.Get(col, rowIndex)) break;
      if (WithinMap(col, rowIndex, facTileMap)) candidatesDown.Add(new Vector2Int(col, rowIndex));
    }
    foreach (Vector2Int pts in candidatesDown)
    {
      resultMap.Set(pts.x, pts.y);
    }
  }

  public void BuildNode(int i, int j, FacMapBuilderDto dto)
  {
    //build building thang
    GameObject templateToClone = null;
    if (dto.TileMap.Get(i, j))
    {
      foreach (TileNeighbours tile in tileArray)
      {
        if(Verify(i,j, dto.TileMap, tile, dto.Path))
        {
          templateToClone = tile.gameObject;
          break;
        }
      }
      if (templateToClone == null) templateToClone = factoryPlacementERROR;
    }
    else
    {
      templateToClone = outsideFloor;
    }
    if (templateToClone != null)
    {
      GameObject result = Instantiate(templateToClone);
      float size = dto.TileMapCellSize;
      result.transform.position = new Vector3(i * size, j * size);
      result.transform.SetParent(mapContainerTrans);
      result.SetActive(true);
    }

    //build tooling
    GameObject obstructionTemplateToClone = null;
    if(dto.Tooling.Get(i,j))
    {
      foreach (TileNeighbours obstrs in obstructions)
      {
        if(Verify(i,j, dto.Tooling, obstrs))
        {
          obstructionTemplateToClone = obstrs.gameObject;
          break;
        }
      }
    }
    if(obstructionTemplateToClone != null)
    {
      GameObject result = Instantiate(obstructionTemplateToClone);
      float size = dto.TileMapCellSize;
      result.transform.position = new Vector3(i * size, j * size);
      result.transform.SetParent(mapContainerTrans);
      result.SetActive(true);
    }

    //build horizontal outdoor obstructions
    if(dto.HorizontalObstructions.Get(i,j))
    {
      GameObject result = Instantiate(horizontalObstruction);
      float size = dto.TileMapCellSize;
      result.transform.position = new Vector3(i * size, j * size);
      result.transform.SetParent(mapContainerTrans);
      result.SetActive(true);
    }

    //build vertical outdoor obstructions
    if(dto.VerticalObstructions.Get(i,j))
    {
      GameObject result = Instantiate(verticalObstruction);
      float size = dto.TileMapCellSize;
      result.transform.position = new Vector3(i * size, j * size);
      result.transform.SetParent(mapContainerTrans);
      result.SetActive(true);
    }
  }

  /// <summary>
  /// This logic is dependent upon the default tile to be sorted first in a list!
  /// </summary>
  /// <param name="i"></param>
  /// <param name="j"></param>
  /// <param name="tileMap"></param>
  /// <param name="tile"></param>
  /// <param name="path"></param>
  /// <returns></returns>
  private bool Verify(int i, int j, TileMapFac tileMap, TileNeighbours tile, TileMapFac path = null)
  {
    for (int direction = 0; direction < tile.neighbours.Length; direction++) 
    {
      //if (tile.neighbours[direction] != PeekTileMap(direction: direction, i, j, tileMap))
      //{
      //  //bool diagonalDirection = direction % 2 == 0;
      //  //if (tile.IsWall && diagonalDirection)

      //  //if(!tile.AllowedDiagonal(direction))
      //  //  return false; 
      //  if (!tile.AllowedDiagonal(direction))
      //    return false;
      //}

      bool wantANeighbour = tile.neighbours[direction];
      bool thereIsANeighbour = (PeekTileMap(direction: direction, i, j, tileMap)) ;
      bool isDiagonal = IsDiagonal(direction);

      if (wantANeighbour && thereIsANeighbour)
      {
        // we don't care, we carry on
      }
      else if (isDiagonal && !wantANeighbour && thereIsANeighbour)
      {
        // we still don't care, we carry on
        
      }
      else if(!wantANeighbour && !thereIsANeighbour)
      {
        // we still don't care, we carry on
      }
      else
      {
        //if (PeekTileMap(direction: direction, i, j, tileMap))
        return false;
      }
      

      if (path == null) 
        continue;

      bool shouldHaveOpening = direction % 2 == 1 &&
        !PeekTileMap(direction: direction, i, j, tileMap) &&
        PeekTilePath(direction: direction, i, j, path);
      if (!shouldHaveOpening)
      {
        if (tile.opening[direction]) return false;
      }
      else
      {
        if (!tile.opening[direction]) return false;
      }
    }
    return true;
  }

  private bool IsDiagonal(int direction)
  {
    return direction % 2 == 0 && direction != 4;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="direction">0-based</param>
  /// <param name="i"></param>
  /// <param name="j"></param>
  /// <param name="dto"></param>
  /// <returns></returns>
  private bool PeekTileMap(int direction, int i, int j, TileMapFac tileMap)
  {
    GetInDirection(direction, i, j, out int a, out int b);
    return tileMap.Get(a, b);
  }

  private bool PeekTilePath(int direction, int i, int j, TileMapFac path)
  {
    GetInDirection(direction, i, j, out int a, out int b);
    return path.Get(a, b);
  }
  
  private void GetInDirection(int direction, int i, int j, out int a, out int b)
  {
    a = i + (direction) % 3 - 1;
    b = j + (int)((direction) / 3) - 1;
  }

  private bool WithinMap(int a, int b, TileMapFac map)
  {
    return (a >= 0 && a < map.Width && b >= 0 && b < map.Height);
  }
  private static void SortSwap(ref int colS, ref int colE)
  {
    if (colS > colE)
    {
      int colTMP = colS;
      colS = colE;
      colE = colTMP;
    }
  }

}
