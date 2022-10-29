using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacMapBuilderDto
{
  public TileMapFac TileMap { get; set; }
  public float TileMapCellSize { get; set; }
  public TileMapFac Path { get; set; }
  public TileMapFac Tooling { get; set; }
  public TileMapFac HorizontalObstructions { get; set; }
  public TileMapFac VerticalObstructions { get; set; }
}
