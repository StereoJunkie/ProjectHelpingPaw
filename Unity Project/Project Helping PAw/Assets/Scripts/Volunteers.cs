using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volunteers : MonoBehaviour
{
    [SerializeField] public int AmountVolunteers;
    [SerializeField] public int VolunteersInUse;

    private void Update()
    {
        VolunteersInUse = Mathf.Clamp(VolunteersInUse, 0, AmountVolunteers);
    }
}
