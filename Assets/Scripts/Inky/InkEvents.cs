using System;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Events;

public class InkEvents : MonoBehaviour
{
    #region Inspector

    [Tooltip("List of ink events that can be invoked from inside an ink file.")]
    [SerializeField] private List<InkEvent> inkEvents;

    #endregion

    #region Unity Event Functions

    private void OnEnable()
    {
        DialogueController.InkEvent += TryInvokeEvent;
    }

    private void OnDisable()
    {
        DialogueController.InkEvent -= TryInvokeEvent;
    }

    #endregion

    private void TryInvokeEvent(string eventName)
    {
        foreach (InkEvent inkEvent in inkEvents)
        {
            if (inkEvent.name == eventName)
            {
                inkEvent.onEvent.Invoke();
                return;
            }
        }
    }
}

[Serializable]
public struct InkEvent
{
    [Tooltip("Name of the ink event.")]
    public string name;

    [Tooltip("Invoked when the ink event is invoked.")]
    public UnityEvent onEvent;
}