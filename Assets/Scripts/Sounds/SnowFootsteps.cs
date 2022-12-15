using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFootsteps : MonoBehaviour
{
    private Footsteps _steps;
    
    private void Awake()
    {
        _steps = FindObjectOfType<Footsteps>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _steps.isSnow = true;
        _steps.isStone = false;
        _steps.isWood = false;
    }
}
