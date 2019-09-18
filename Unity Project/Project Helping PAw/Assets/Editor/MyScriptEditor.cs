using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
  [CustomEditor(typeof(StatusEffect))]
    public class MyScriptEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            var myScript = target as StatusEffect;
            myScript.timed = GUILayout.Toggle(myScript.timed, "Timed");
            myScript.chanceEffect = GUILayout.Toggle(myScript.chanceEffect, "Chance effect");
            myScript.effectName = GUILayout.TextField(myScript.effectName, "Effect Name:");
            myScript.drainageAdd = GUILayout.Toggle(myScript.drainageAdd, "Add to drainage");
            myScript.SecondDrainageStat = GUILayout.Toggle(myScript.SecondDrainageStat, "Second Stat Drainage");
            myScript.straightRemove = GUILayout.Toggle(myScript.straightRemove, "Remove points");
            myScript.removeFromMax = GUILayout.Toggle(myScript.removeFromMax, "Remove from max amount");

            if (myScript.chanceEffect)
            {
                myScript.chanceDirty = EditorGUILayout.Slider("chanceDirt: ", myScript.chanceDirty, 1f, 100f);
                myScript.chanceSick = EditorGUILayout.Slider("chanceSick: ", myScript.chanceSick, 1f, 100f);
                myScript.chanceUngroomed = EditorGUILayout.Slider("chanceUngroomed: ", myScript.chanceUngroomed, 1f, 100f);
            }
            if (myScript.drainageAdd)
            {
                myScript.drainageAmount = EditorGUILayout.Slider("Drainage amount:", myScript.drainageAmount, 1f, 100f);
                myScript.nutritionDrainage = EditorGUILayout.Toggle("Nutrition drainage:", myScript.nutritionDrainage);
                myScript.hygieneDrainage = EditorGUILayout.Toggle("Hygiene drainage:", myScript.hygieneDrainage);
                myScript.socializationDrainage =
                    EditorGUILayout.Toggle("Socialization drainage:", myScript.socializationDrainage);
                myScript.healthDrainage = EditorGUILayout.Toggle("Health drainage:", myScript.healthDrainage);
                myScript.behaviourDrainage = EditorGUILayout.Toggle("Behaviour drainage:", myScript.behaviourDrainage);
                myScript.lookDrainage = EditorGUILayout.Toggle("Look drainage:", myScript.lookDrainage);
                if (myScript.nutritionDrainage)
                    myScript.typeDrainage = 1;
                else if (myScript.hygieneDrainage)
                    myScript.typeDrainage = 2;
                else if (myScript.socializationDrainage)
                    myScript.typeDrainage = 3;
                else if (myScript.healthDrainage)
                    myScript.typeDrainage = 4;
                else if (myScript.behaviourDrainage)
                    myScript.typeDrainage = 5;
                else if (myScript.lookDrainage)
                    myScript.typeDrainage = 6;



            }
            else myScript.typeDrainage = 0;

            if (myScript.SecondDrainageStat)
            {
                myScript.drainageAmount2 =
                    EditorGUILayout.Slider("Drainage amount:", myScript.drainageAmount2, 1f, 100f);
                myScript.nutritionDrainage2 =
                    EditorGUILayout.Toggle("Nutrition drainage:", myScript.nutritionDrainage2);
                myScript.hygieneDrainage2 = EditorGUILayout.Toggle("Hygiene drainage:", myScript.hygieneDrainage2);
                myScript.socializationDrainage2 =
                    EditorGUILayout.Toggle("Socialization drainage:", myScript.socializationDrainage2);
                myScript.healthDrainage2 = EditorGUILayout.Toggle("Health drainage:", myScript.healthDrainage2);
                myScript.behaviourDrainage2 =
                    EditorGUILayout.Toggle("Behaviour drainage:", myScript.behaviourDrainage2);
                myScript.lookDrainage2 = EditorGUILayout.Toggle("Look drainage:", myScript.lookDrainage2);
                if (myScript.nutritionDrainage2)
                    myScript.typeDrainage2 = 1;
                else if (myScript.hygieneDrainage2)
                    myScript.typeDrainage2 = 2;
                else if (myScript.socializationDrainage2)
                    myScript.typeDrainage2 = 3;
                else if (myScript.healthDrainage2)
                    myScript.typeDrainage2 = 4;
                else if (myScript.behaviourDrainage2)
                    myScript.typeDrainage2 = 5;
                else if (myScript.lookDrainage2)
                    myScript.typeDrainage2 = 6;
            }
            else myScript.typeDrainage2 = 0;

            if (myScript.straightRemove)
            {
                myScript.removeAmount = EditorGUILayout.Slider("Removal amount:", myScript.removeAmount, 1f, 100f);
                myScript.nutritionRemove = EditorGUILayout.Toggle("Nutrition remove:", myScript.nutritionRemove);
                myScript.hygieneRemove = EditorGUILayout.Toggle("Hygiene remove:", myScript.hygieneRemove);
                myScript.socializationRemove =
                    EditorGUILayout.Toggle("Socialization remove:", myScript.socializationRemove);
                myScript.healthRemove = EditorGUILayout.Toggle("Health remove:", myScript.healthRemove);
                myScript.behaviourRemove = EditorGUILayout.Toggle("Behaviour remove:", myScript.behaviourRemove);
                myScript.lookRemove = EditorGUILayout.Toggle("Look remove:", myScript.lookRemove);
                if (myScript.nutritionRemove)
                    myScript.typeStraightRemove = 1;
                else if (myScript.hygieneRemove)
                    myScript.typeStraightRemove = 2;
                else if (myScript.socializationRemove)
                    myScript.typeStraightRemove = 3;
                else if (myScript.healthRemove)
                    myScript.typeStraightRemove = 4;
                else if (myScript.behaviourRemove)
                    myScript.typeStraightRemove = 5;
                else if (myScript.lookRemove)
                    myScript.typeStraightRemove = 6;
            }
            else myScript.typeStraightRemove = 0;

            if (myScript.removeFromMax)
            {
                myScript.removeAmountMax =
                    EditorGUILayout.Slider("Removal from max amount:", myScript.removeAmountMax, 1f, 100f);
                myScript.maxHealthRemove = EditorGUILayout.Toggle("Health remove:", myScript.maxHealthRemove);
                myScript.maxBehaviourRemove = EditorGUILayout.Toggle("Behaviour remove:", myScript.maxBehaviourRemove);
                myScript.maxLookRemove = EditorGUILayout.Toggle("Look remove:", myScript.maxLookRemove);
                if (myScript.healthRemove)
                    myScript.typeMaxRemove = 1;
                else if (myScript.behaviourRemove)
                    myScript.typeMaxRemove = 2;
                else if (myScript.lookRemove)
                    myScript.typeMaxRemove = 3;
            }
            else myScript.typeMaxRemove = 0;
        }
    }