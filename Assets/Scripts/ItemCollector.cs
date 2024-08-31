using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int fruits = 0;

    [SerializeField] private Text scores;
    [SerializeField] private AudioSource collectSourceEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))
        {
            collectSourceEffect.Play();
            Destroy(collision.gameObject);
            fruits++;
            Debug.Log("Fruits: " + fruits);
            scores.text = "Scores: " + fruits;
        }
    }
}
