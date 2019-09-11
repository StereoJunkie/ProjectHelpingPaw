using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NeedZoneEffects : MonoBehaviour
{
    [SerializeField] private StatusEffect nutritionRedZone;
    [SerializeField] private StatusEffect nutritionYellowZone;
    
    [SerializeField] private StatusEffect hygieneRedZone;
    [SerializeField] private StatusEffect hygieneYellowZone;
    
    [SerializeField] private StatusEffect socializationRedZone;
    [SerializeField] private StatusEffect socializationYellowZone;
    public List<StatusEffect> NeedZoneEffectList;

    private void Start()
    {
        NeedZoneEffectList.Add(nutritionRedZone);
        NeedZoneEffectList.Add(nutritionYellowZone);
        NeedZoneEffectList.Add(hygieneRedZone);
        NeedZoneEffectList.Add(hygieneYellowZone);
        NeedZoneEffectList.Add(socializationRedZone);
        NeedZoneEffectList.Add(socializationYellowZone);
    }
}
