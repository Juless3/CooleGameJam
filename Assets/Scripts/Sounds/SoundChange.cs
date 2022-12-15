using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SoundChange : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter Track1;
    [SerializeField] private StudioEventEmitter Track2;

    [SerializeField] private GameObject Track1Object;
    [SerializeField] private GameObject Track2Object;

    private void Awake()
    {
        
    }

    public void Track1Off()
    {
        Track1.SetParameter("Fade", 0);
        Track2.SetParameter("Fade", 1);
        Track1Object.SetActive(false);
        Track2Object.SetActive(true);
    }
    public void Track2Off()
    {
        Track1.SetParameter("Fade", 1);
        Track2.SetParameter("Fade", 0);
        Track1Object.SetActive(true);
        Track2Object.SetActive(false);
    }
   
}
