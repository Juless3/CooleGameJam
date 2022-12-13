using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SoundChange : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter TrackOutOnEnter;
    [SerializeField] private StudioEventEmitter TrackOnOnEnter; 
 
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            TrackOutOnEnter.SetParameter("Fade", 0);
            TrackOnOnEnter.SetParameter("Fade", 1);
        }
       
    }
    

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            TrackOutOnEnter.SetParameter("Fade", 1);
            TrackOnOnEnter.SetParameter("Fade", 0);
        }
        
    }
}
