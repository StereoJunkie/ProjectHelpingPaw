using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : MonoBehaviour
{
    public bool Homeless;
    public bool Sheltered;
    public bool Adopted;
    public List<StatusEffect> activeEffects;

    protected float sickChance;
    protected float dirtyChance;
    protected float ungroomedChance;
    
    [SerializeField] protected float Health;
    [SerializeField] protected float Behaviour;
    [SerializeField] protected float Look;
    
    [SerializeField] protected float Nutrition;
    [SerializeField] protected float Hygiene;
    [SerializeField] protected float Socialization;

    protected GameObject gameManager;

    protected float MaxHealthDrainage;
    protected float MaxBehaviourDrainage;
    protected float MaxLookDrainage;

    protected float ExtraDrainageNutrition;
    protected float ExtraDrainageHygiene;
    protected float ExtraDrainageSocialization;
    protected float ExtraDrainageHealth;
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
