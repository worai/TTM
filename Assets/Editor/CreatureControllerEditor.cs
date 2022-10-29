using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CreatureController))]
public class CreatureControllerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    CreatureController controller = target as CreatureController;

    if (!controller.HasHeavyAttack)
      return;


    //GUILayout.BeginHorizontal(horizontalWidth);
    //GUILayout.Label("up left", labelWidth);

    GUILayoutOption labelWidth = GUILayout.MinWidth(200);


    GUILayout.BeginHorizontal();
    GUILayout.Label("Heavy attack windup", labelWidth);
    controller.heavyAttackWindup = EditorGUILayout.FloatField(controller.heavyAttackWindup);
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal();
    GUILayout.Label("Heavy attack winddown", labelWidth);
    controller.heavyAttackWinddown = EditorGUILayout.FloatField(controller.heavyAttackWinddown);
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal();
    GUILayout.Label("Heavy attack min damage", labelWidth);
    controller.heavyAttackMinDamage = EditorGUILayout.FloatField(controller.heavyAttackMinDamage);
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal();
    GUILayout.Label("Heavy attack max damage", labelWidth);
    controller.heavyAttackMaxDamage = EditorGUILayout.FloatField(controller.heavyAttackMaxDamage);
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal();
    GUILayout.Label("Heavy attack likelyhood", labelWidth);
    controller.heavyAttackLikelyhood = EditorGUILayout.FloatField(controller.heavyAttackLikelyhood);
    GUILayout.EndHorizontal();
    
  }
}
