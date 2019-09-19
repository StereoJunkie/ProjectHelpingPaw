using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Dog : Animal
{
    
    private int timesToCheckPerDay;
    private List<int> timeStampsToCheck;
    public List<StatusEffect> dailyStatusEffects;
    [SerializeField] private List<string> currentActiveEffects;
    
    private AnimalStatManager StatManager;
    private StatusManager statusManager;
    private DayAndNightCycle dayNightCycle;
    private RoomManager roomManager;
    private DailyStatusEffectCheck DailyStatusInfo;
    private int lastMinute;
    private NeedZoneEffects needZoneEffectObject;
    private List<int> needEffectIndex;
    private DogActivities dogActivities;
    public GameObject room;
    private Room roomScript;

    [SerializeField] private int adoptionRates;
    [SerializeField] private float adoptionChance;
    [SerializeField] private float adopteeChance;


    public bool poopTimer = false;
    public float poopTimePassed = 0f;
    

    public List<StatusEffect> needBarEffects;
    

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        if(gameManager == null)
            Debug.LogError("Help");
        roomManager = gameManager.GetComponent<RoomManager>();
        StatManager = gameManager.GetComponent<AnimalStatManager>();
        dayNightCycle = gameManager.GetComponent<DayAndNightCycle>();
        DailyStatusInfo = gameManager.GetComponent<DailyStatusEffectCheck>();
        needZoneEffectObject = gameManager.GetComponent<NeedZoneEffects>();
        statusManager = gameManager.GetComponent<StatusManager>();
        needEffectIndex = new List<int>();
        timeStampsToCheck = new List<int>();
        
        if(needZoneEffectObject != null)
            needBarEffects = needZoneEffectObject.NeedZoneEffectList;
        
        Homeless = true;
        Adopted = false;
        Sheltered = false;
        lastMinute = 0;
        
        Health = Random.Range(10f, StatManager.MaxStartHealth);
        Behaviour = Random.Range(10f, StatManager.MaxStartBehaviour);
        Look = Random.Range(10f, StatManager.MaxStartLook);
        Nutrition = Random.Range(10f, StatManager.MaxStartNutrition);
        Hygiene = Random.Range(10f, StatManager.MaxStartHygiene);
        Socialization = Random.Range(10f, StatManager.MaxStartSocialization);

        if (statusManager.statusEffects.Count != 0)
        {
            activeEffects = new List<StatusEffect>(statusManager.statusEffects);
            foreach (StatusEffect needEffect in needBarEffects)
            {
                if (activeEffects.Contains(needEffect))
                {
                    needEffectIndex.Add(activeEffects.IndexOf(needEffect));
                }
                else
                {
                    Debug.LogError("Something went wrong with, the importing of need effects into the active effects list!");
                }
            }
        }

        if (needZoneEffectObject != null)
        {
            needBarEffects = needZoneEffectObject.NeedZoneEffectList;
        }
        else
        {
            Debug.LogError("Need Zone effect object not found, please fix the error");
        }
        
        if (DailyStatusInfo != null)
        {
            timesToCheckPerDay = DailyStatusInfo.timesToCheckPerDay;
            for (int i = 0; i < timeStampsToCheck.Count; i++)
            {
                timeStampsToCheck[i] = dayNightCycle.minutesInADay / timesToCheckPerDay * (i + 1);
            }
        }
        else
        {
            Debug.LogError("StatManager doesn't exist.");
        }

        if (StatManager != null)
        {
            Health = Random.Range(1f, StatManager.MaxStartHealth);
            Behaviour = Random.Range(1f, StatManager.MaxStartBehaviour);
            Look = Random.Range(1f, StatManager.MaxStartLook);
            Nutrition = Random.Range(1f, StatManager.MaxStartNutrition);
            Hygiene = Random.Range(1f, StatManager.MaxStartHygiene);
            Socialization = Random.Range(1f, StatManager.MaxStartSocialization);
        }
        else
        {
            Debug.LogError("StatManager doesn't exist.");
        }
    }

    void Update()
    {
        //adoption process
        if (dayNightCycle.timeActive)
        {
            if ((int)dayNightCycle.DaysSinceStart > dayNightCycle.previousDay)
            {
                adoptionRates += 1;
                
                float randomAdoptionChance = Random.Range(1f, 70f);
                adopteeChance = randomAdoptionChance;
                adoptionChance = CalculateAdoptionChance();

                if (randomAdoptionChance < CalculateAdoptionChance())
                {
                    Adopted = true;
                    Sheltered = false;
                    Debug.Log(name + " Has been adopted!");
                }

                dayNightCycle.previousDay = (int) dayNightCycle.DaysSinceStart;
            }
        }

        //show current active effects
        foreach (StatusEffect effect in activeEffects)
        {
            if (effect.effectName == "Aggressive")
            {
                foreach (StatusEffect deffect in activeEffects)
                {
                    if (deffect.effectName == "Bored")
                        deffect.ActivateEffect = false;
                }
            }
                
            if (effect.ActivateEffect)
            {
                if (!currentActiveEffects.Contains(effect.effectName))
                    currentActiveEffects.Add(effect.effectName);
            }
            else
            {
                if (currentActiveEffects.Contains(effect.effectName))
                    currentActiveEffects.Remove(effect.effectName);
            }
        }
        
        
        
        
        if (Adopted)
        {
            roomManager.DogsAdopted += 1;
            Destroy(this.transform.parent.gameObject);
        }
        if (dogActivities == null && Sheltered)
        {
            dogActivities = gameObject.AddComponent<DogActivities>();
        }
        Death();
        if (activeEffects.Count != 0)
        {
            NeedCheck();
            DailyStatusCheck();
        }

        if (dayNightCycle != null)
        {
            DrainStats();
        }
        else
        {
            Debug.LogError("I think the daynightCycle is nonexistant");
        }

        if (poopTimer)
        {
            poopTimePassed += Time.deltaTime;
            if (poopTimePassed > 60f)
            {
                Hygiene -= 15f;
                poopTimePassed = 0f;
                poopTimer = false;
            }
        }

        Health = CalculateHealth();
        Behaviour = CalculateBehaviour();
        Look = CalculateLook();
        ClampStats();
    }

    
    protected override void Death()
    {
        if (Health <= 0)
        { 
            FindObjectOfType<SoundManager>().Play("Death");
            roomManager.DogsKilled += 1;
            Destroy(this.transform.parent.gameObject);
        }
    }
    protected override void NeedCheck()
    {
        
        if (Nutrition <= 33f)
        {
            activeEffects[needEffectIndex[0]].ActivateEffect = true;
        }
        else if (Nutrition <= 66f)
        {
            activeEffects[needEffectIndex[1]].ActivateEffect = true;
        }
        else
        {
            activeEffects[needEffectIndex[0]].ActivateEffect = false;
            activeEffects[needEffectIndex[1]].ActivateEffect = false;
        }
        
        if (Hygiene <= 33f)
        {
            activeEffects[needEffectIndex[2]].ActivateEffect = true;
        }
        else if (Hygiene <= 66f)
        {
            activeEffects[needEffectIndex[3]].ActivateEffect = true;
        }
        else
        {
            activeEffects[needEffectIndex[2]].ActivateEffect = false;
            activeEffects[needEffectIndex[3]].ActivateEffect = false;
        }
        
        if (Socialization <= 33f)
        {
            activeEffects[needEffectIndex[4]].ActivateEffect = true;
        }
        else if (Socialization <= 66f)
        {
            activeEffects[needEffectIndex[5]].ActivateEffect = true;
        }
        else
        {
            activeEffects[needEffectIndex[4]].ActivateEffect = false;
            activeEffects[needEffectIndex[5]].ActivateEffect = false;
        }
    }

    protected override void DrainStats()
    {
        if (dayNightCycle.timeActive)
        {
            if ((int) dayNightCycle.timePassedMinutes > dayNightCycle.LastMinute)
            {
                ExtraDrainageNutrition = 0;
                ExtraDrainageHygiene = 0;
                ExtraDrainageSocialization = 0;
                ExtraDrainageHealth = 0;
                ExtraDrainageBehaviour = 0;
                ExtraDrainageLook = 0;
                dirtyChance = 0;
                sickChance = 0;
                ungroomedChance = 0;
                MaxHealthDrainage = 0f;
                MaxBehaviourDrainage = 0f;
                MaxLookDrainage = 0f;

                foreach (StatusEffect effect in activeEffects)
                {
                    if (effect.ActivateEffect)
                    {
                        if (effect.chanceEffect)
                        {
                            dirtyChance += effect.chanceDirty;
                            sickChance += effect.chanceSick;
                            ungroomedChance += effect.chanceUngroomed;
                        }
                        if (effect.drainageAdd)
                        {
                            switch (effect.typeDrainage)
                            {
                                default:
                                    Debug.Log("type DRAINAGE had an error");
                                    break;
                                case (1):
                                    ExtraDrainageNutrition += effect.drainageAmount;
                                    break;
                                case (2):
                                    ExtraDrainageHygiene += effect.drainageAmount;
                                    break;
                                case (3):
                                    ExtraDrainageSocialization += effect.drainageAmount;
                                    break;
                                case (4):
                                    ExtraDrainageHealth += effect.drainageAmount;
                                    break;
                                case (5):
                                    ExtraDrainageBehaviour += effect.drainageAmount;
                                    break;
                                case (6):
                                    ExtraDrainageLook += effect.drainageAmount;
                                    break;
                                case (0):
                                    break;
                            }
                        }

                        if (effect.SecondDrainageStat)
                        {
                            switch (effect.typeDrainage2)
                            {
                                default:
                                    Debug.Log("type DRAINAGE2 had an error");
                                    break;
                                case (1):
                                    ExtraDrainageNutrition += effect.drainageAmount2;
                                    break;
                                case (2):
                                    ExtraDrainageHygiene += effect.drainageAmount2;
                                    break;
                                case (3):
                                    ExtraDrainageSocialization += effect.drainageAmount2;
                                    break;
                                case (4):
                                    ExtraDrainageHealth += effect.drainageAmount2;
                                    break;
                                case (5):
                                    ExtraDrainageBehaviour += effect.drainageAmount2;
                                    break;
                                case (6):
                                    ExtraDrainageLook += effect.drainageAmount2;
                                    break;
                                case (0):
                                    break;
                            }
                        }

                        if (effect.straightRemove && !effect.hasRemoved)
                        {
                            switch (effect.typeStraightRemove)
                            {
                                default:
                                    Debug.Log("type STRAIGHT REMOVE had an error");
                                    break;
                                case (1):
                                    Nutrition -= effect.typeStraightRemove;
                                    break;
                                case (2):
                                    Hygiene -= effect.typeStraightRemove;
                                    break;
                                case (3):
                                    Socialization -= effect.typeStraightRemove;
                                    break;
                                case (4):
                                    Health -= effect.typeStraightRemove;
                                    break;
                                case (5):
                                    Behaviour -= effect.typeStraightRemove;
                                    break;
                                case (6):
                                    Look -= effect.typeStraightRemove;
                                    break;
                                case (0):
                                    break;
                            }

                            effect.hasRemoved = true;
                        }

                        /*if (effect.removeFromMax)
                        {
                            switch (effect.typeMaxRemove)
                            {
                                default:
                                    Debug.Log("type MAX REMOVE had an error");
                                    break;
                                case (1):
                                    MaxHealthDrainage -= effect.typeMaxRemove;
                                    Debug.Log("health max " + MaxHealthDrainage);
                                    break;
                                case (2):
                                    MaxBehaviourDrainage -= effect.typeMaxRemove;
                                    Debug.Log("Behaviour max " + MaxBehaviourDrainage);
                                    break;
                                case (3):
                                    MaxLookDrainage -= effect.typeMaxRemove;
                                    Debug.Log("Look max " + MaxLookDrainage);
                                    break;
                                case (0):
                                    break;
                            }
                        }
                        else
                        {
                            MaxHealthDrainage = 100f;
                            MaxBehaviourDrainage = 100f;
                            MaxLookDrainage = 100f;
                        }*/
                    }
                }
                
                Nutrition -= StatManager.drainageNutrition + ExtraDrainageNutrition;
                Hygiene -= StatManager.drainageHygiene + ExtraDrainageHygiene;
                Socialization -= StatManager.drainageSocialization + ExtraDrainageSocialization;
                Health -= ExtraDrainageHealth;
                Behaviour -= ExtraDrainageBehaviour;
                Look -= ExtraDrainageLook;
            }
        }
    }

    protected override void DailyStatusCheck()
    {
        if (dayNightCycle.timeActive)
        {
            foreach (int timeStamp in timeStampsToCheck)
            {
                float random = Random.Range(1f, 100f);
                if (timeStamp == dayNightCycle.minutesInADay)
                {
                    foreach (StatusEffect statusEffect in activeEffects)
                    {
                        if (statusEffect.name == "Sick" && random > sickChance)
                            statusEffect.ActivateEffect = true;
                        if (statusEffect.name == "Dirty" && random > dirtyChance)
                            statusEffect.ActivateEffect = true;
                        if (statusEffect.name == "Ungroomed" && random > ungroomedChance)
                            statusEffect.ActivateEffect = true;
                        
                        
                    }
                }
            }
        }
    }

    protected override void ClampStats()
    {
        Health = Mathf.Clamp(Health, 0f, 100f-MaxHealthDrainage);
        Behaviour = Mathf.Clamp(Behaviour, 0f, 100f-MaxBehaviourDrainage);
        Look = Mathf.Clamp(Look, 0f, 100f-MaxLookDrainage);
        Nutrition = Mathf.Clamp(Nutrition, 0f, 100f);
        Hygiene = Mathf.Clamp(Hygiene, 0f, 100f);
        Socialization = Mathf.Clamp(Socialization, 0f, 100f);
    }

    public override float CalculateAdoptionChance()
    {
        return (Behaviour + Look + Health) / 3f;
    }

    protected override float CalculateHealth()
    {
        return Nutrition*StatManager.NutritionToHygiene + Hygiene*(1f-StatManager.NutritionToHygiene);
    }

    protected override float CalculateBehaviour()
    {
        return Nutrition*StatManager.NutritionToSocialization + Socialization*(1f-StatManager.NutritionToHygiene);
    }

    protected override float CalculateLook()
    {
        return Hygiene*StatManager.HygieneToSocialization + Socialization*(1f-StatManager.HygieneToSocialization);
    }
}
