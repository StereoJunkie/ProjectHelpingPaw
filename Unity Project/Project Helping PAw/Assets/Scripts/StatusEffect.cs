using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{

    public string effectName;
    public bool chanceEffect;
    public bool timed;

    public bool ActivateEffect = false;

    public bool drainageAdd;
    public float drainageAmount;
    public bool nutritionDrainage;
    public bool hygieneDrainage;
    public bool socializationDrainage;
    public bool healthDrainage;
    public bool behaviourDrainage;
    public bool lookDrainage;
    public int typeDrainage;
    
    public bool SecondDrainageStat;
    public float drainageAmount2;
    public bool nutritionDrainage2;
    public bool hygieneDrainage2;
    public bool socializationDrainage2;
    public bool healthDrainage2;
    public bool behaviourDrainage2;
    public bool lookDrainage2;
    public int typeDrainage2;


    public bool straightRemove;
    public bool hasRemoved = false;
    public float removeAmount;
    public bool nutritionRemove;
    public bool hygieneRemove;
    public bool socializationRemove;
    public bool healthRemove;
    public bool behaviourRemove;
    public bool lookRemove;
    public int typeStraightRemove;


    public bool removeFromMax;
    public float removeAmountMax;
    public bool maxHealthRemove;
    public bool maxBehaviourRemove;
    public bool maxLookRemove;
    public int typeMaxRemove;


    
  
}
