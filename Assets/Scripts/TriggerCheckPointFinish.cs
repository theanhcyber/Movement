using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckPointFinish : MonoBehaviour
{
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject checkPointFinish;
    [SerializeField] private Animator endPointAnimator;
    [SerializeField] private AudioSource finishSourceEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            cup.GetComponent<SpriteRenderer>().enabled = true;
            checkPointFinish.GetComponent<SpriteRenderer>().enabled = false;
            checkPointFinish.GetComponent<BoxCollider2D>().enabled = false;
            finishSourceEffect.Play(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            endPointAnimator.SetBool("viewPoint", true);
            endPointAnimator.SetBool("notViewPoint", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            endPointAnimator.SetBool("notViewPoint", true);
            endPointAnimator.SetBool("viewPoint", false);
        }
    }
}
