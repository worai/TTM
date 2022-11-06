using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BigPicture
{

  [Serializable]
  public class MapData
  {
    //map node rows
    NodeRowData[] rows;
    int level;

    public MapData(int numCols, int numRows, int? currentLevel)
    {
      rows = new NodeRowData[numRows];
      for (int i = 0; i < numRows; i++)
      {
        rows[i] = new NodeRowData(numCols);
      }
      level = currentLevel.HasValue ? currentLevel.Value : 1;
    }

    public int NumCols => rows[0].NumCols;
    public int NumRows => rows.Length;
    public int Level => level;
  }

  [Serializable]
  public class NodeRowData
  {
    //map node row
    byte[] mapNodeStateRow;
    bool[] mapNodeConstructionRow;

    public NodeRowData(int numCols)
    {
      mapNodeStateRow = new byte[numCols];
      mapNodeConstructionRow = new bool[numCols];
      for (int i = 0; i < numCols; i++)
      {
        mapNodeStateRow[i] = byte.MaxValue;
        mapNodeConstructionRow[i] = false;
      }
    }

    public int NumCols { get => mapNodeStateRow.Length; }
  }
}
