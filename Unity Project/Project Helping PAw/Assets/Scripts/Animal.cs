using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : MonoBehaviour
{
    public bool Homeless;
    public bool Sheltered;
    public bool Adopted;
    public List<StatusEffect> activeEffects;

    public float sickChance;
    public float dirtyChance;
    public float ungroomedChance;
    
    [SerializeField] public float Health;
    [SerializeField] protected float Behaviour;
    [SerializeField] protected float Look;
    
    [SerializeField] public float Nutrition;
    [SerializeField] public float Hygiene;
    [SerializeField] public float Socialization;

    protected GameObject gameManager;

    protected float MaxHealthDrainage;
    protected float MaxBehaviourDrainage;
    protected float MaxLookDrainage;

    protected float ExtraDrainageNutrition;
    protected float ExtraDrainageHygiene;
    protected float ExtraDrainageSocialization;
    public float ExtraDrainageHealth;
    protected float ExtraDrainageLook;
    protected float ExtraDrainageBehaviour;

    protected abstract void Death();
    protected abstract void NeedCheck();
    protected abstract void DailyStatusCheck();

    public abstract float CalculateAdoptionChance();
    protected abstract float CalculateHealth();
    protected abstract float CalculateBehaviour();
    protected abstract float CalculateLook();

    protected abstract void DrainStats();

    protected abstract void ClampStats();
}
