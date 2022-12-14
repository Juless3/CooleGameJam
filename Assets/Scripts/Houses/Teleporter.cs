using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject currentTeleporter;

    public void EnterHouse()
    {
        player.transform.position = currentTeleporter.GetComponent<Teleportdestination>().GetDestination().position;
    }
}
