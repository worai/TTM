using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileNeighbours))]
public class TileNeightboursEditor : Editor
{

  public string[] options = new string[] { "Yes", "Cancel" };
  public int index = 0;

  public override void OnInspectorGUI()
  {
    //base.OnInspectorGUI();

    
    TileNeighbours tile = target as TileNeighbours;


    if(GUILayout.Button("Reset"))
    {
      //can't make this work with some popup to ensure that the user ain't destroying anything.
      //index = EditorGUILayout.Popup(index, options);
      //if(GUILayout.Button("Yes"))
      //  Debug.Log("Got a click");

      tile.Reset();
    }


    GUILayoutOption horizontalWidth = GUILayout.MaxWidth(150);
    GUILayoutOption labelWidth = GUILayout.MaxWidth(100);

    GUILayout.Label("Alike neighbours");

    GUILayout.BeginHorizontal(horizontalWidth);
    GUILayout.Label("up left", labelWidth);
      tile.neighbours[6] = GUILayout.Toggle(tile.neighbours[6], "");
      tile.neighbours[7] = GUILayout.Toggle(tile.neighbours[7], "");
      tile.neighbours[8] = GUILayout.Toggle(tile.neighbours[8], "");
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal(horizontalWidth);
    GUILayout.Label("left", labelWidth);
      tile.neighbours[3] = GUILayout.Toggle(tile.neighbours[3], "");
      tile.neighbours[4] = GUILayout.Toggle(tile.neighbours[4], "");
      tile.neighbours[5] = GUILayout.Toggle(tile.neighbours[5], "");
    GUILayout.EndHorizontal();


    GUILayout.BeginHorizontal(horizontalWidth);
    GUILayout.Label("down left", labelWidth);
      tile.neighbours[0] = GUILayout.Toggle(tile.neighbours[0], "");
      tile.neighbours[1] = GUILayout.Toggle(tile.neighbours[1], "");
      tile.neighbours[2] = GUILayout.Toggle(tile.neighbours[2], "");
    GUILayout.EndHorizontal();


    EditorGUILayout.Space(width: 25);


    GUILayout.Label("Doors/openings");

    GUILayout.BeginHorizontal(horizontalWidth);
      GUILayout.Label("up left", labelWidth);
      tile.opening[6] = GUILayout.Toggle(tile.opening[6], "");
      tile.opening[7] = GUILayout.Toggle(tile.opening[7], "");
      tile.opening[8] = GUILayout.Toggle(tile.opening[8], "");
    GUILayout.EndHorizontal();
    GUILayout.BeginHorizontal(horizontalWidth);
      GUILayout.Label("left", labelWidth);
      tile.opening[3] = GUILayout.Toggle(tile.opening[3], "");
      tile.opening[4] = GUILayout.Toggle(tile.opening[4], "");
      tile.opening[5] = GUILayout.Toggle(tile.opening[5], "");
    GUILayout.EndHorizontal();
    GUILayout.BeginHorizontal(horizontalWidth);
    GUILayout.Label("down left", labelWidth);
      tile.opening[0] = GUILayout.Toggle(tile.opening[0], "");
      tile.opening[1] = GUILayout.Toggle(tile.opening[1], "");
      tile.opening[2] = GUILayout.Toggle(tile.opening[2], "");
    GUILayout.EndHorizontal();


    //GUILayout.BeginHorizontal(horizontalWidth);
    //  EditorGUILayout.Toggle("sth", false);
    //  EditorGUILayout.Toggle("else", true);
    //  EditorGUILayout.Toggle("ye", false);
    //GUILayout.EndHorizontal();
  }


}
