using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit?.Invoke();
    }
}


