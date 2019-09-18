using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimalStatManager : MonoBehaviour
{
    [Range(10f, 50f)] [SerializeField] public float MaxStartHealth;
    [Range(10f, 50f)] [SerializeField] public float MaxStartBehaviour;
    [Range(10f, 50f)] [SerializeField] public float MaxStartLook;
    [Range(10f, 50f)] [SerializeField] public float MaxStartNutrition;
    [Range(10f, 50f)] [SerializeField] public float MaxStartHygiene;
    [Range(10f, 50f)] [SerializeField] public float MaxStartSocialization;

    [Range(0f, 100f)] [SerializeField] public float NutritionToHygiene;
    [Range(0f, 100f)] [SerializeField] public float NutritionToSocialization;
    [Range(0f, 100f)] [SerializeField] public float HygieneToSocialization;

    [Range(1f, 50f)] [SerializeField] public float drainageNutrition;
    [Range(1f, 50f)] [SerializeField] public float drainageHygiene;
    [Range(1f, 50f)] [SerializeField] public float drainageSocialization;

    [Range(10f, 240f)] [SerializeField] public float ActivityTimer;

    private void Start()
    {
        NutritionToHygiene *= 0.01f;
        NutritionToSocialization *= 0.01f;
        HygieneToSocialization *= 0.01f;
    }
}
