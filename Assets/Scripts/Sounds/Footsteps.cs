using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Newtonsoft.Json;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _footsteps;
    private FMOD.Studio.EventInstance footsteps;
    public bool isWood;
    public bool isSnow;
    public bool isStone;
    
    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = RuntimeManager.CreateInstance(_footsteps);
        }
    }

    public void WalkSound()
    {
        if (isSnow == true)
        {
            footsteps.setParameterByName("Footsteps", 0);
            footsteps.start();
        }
        
        if (isStone == true)
        {
            footsteps.setParameterByName("Footsteps", 1);
            footsteps.start();
        }
        
        if (isWood == true)
        {
            footsteps.setParameterByName("Footsteps", 2);
            footsteps.start();
        }
        
        
        
        
    }
    
    
}

