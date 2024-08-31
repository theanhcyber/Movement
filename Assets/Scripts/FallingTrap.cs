using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    [SerializeField] private GameObject trapToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Touch the trap!!!");
            ActivateTrap();
        }
    }

    private void ActivateTrap()
    {
        Debug.Log("Trigger event!!!");
        if (trapToActivate != null)
        {
            Rigidbody2D rb = trapToActivate.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = new Vector2(0, -speed);
            }
        }
    }
}
