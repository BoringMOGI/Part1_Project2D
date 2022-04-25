using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapEvent : MonoBehaviour
{
    [SerializeField] Trap trap;

    private void Start()
    {
        if (trap == null)
            gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        trap.OnContactEnter(collision.gameObject);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        trap.OnContactStay(collision.gameObject);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        trap.OnContactExit(collision.gameObject);
    }

}
