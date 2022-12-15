using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFootsteps : MonoBehaviour
{
    private Footsteps _steps;
    
    private void Awake()
    {
        _steps = FindObjectOfType<Footsteps>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Feet"))
        {
            _steps.isStone = true;
            _steps.isSnow = false;
            _steps.isWood = false;
        }
    }
}

