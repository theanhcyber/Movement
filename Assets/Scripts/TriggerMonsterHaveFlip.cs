using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonsterHaveFlip : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject monster;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float stayDuration = 5f;

    private enum MovementState { startFlying, stopflying }
    private bool checkEvent = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!checkEvent && collision.gameObject.name == "Player")
        {
            Debug.Log("Trigger monster!!!");
            StartCoroutine(MoveMonsterRoundTrip(startPoint.position, targetPoint.position, speed, stayDuration));
            checkEvent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            checkEvent = false;
        }
    }

    private IEnumerator MoveMonsterRoundTrip(Vector3 start, Vector3 destination, float moveDuration, float stayDuration)
    {
        animator.SetBool("startFly", true);
        yield return new WaitForSeconds(3);
        //Move from the strating point back to the destination
        yield return MoveToPosition(monster.transform, destination, moveDuration, MovementState.startFlying);
        //Stop to the destination
        yield return new WaitForSeconds(stayDuration);
        //Move from the destination back to the starting point
        spriteRenderer.flipX = true;
        yield return MoveToPosition(monster.transform, start, moveDuration, MovementState.stopflying);
        //close animator when stop event
        animator.SetBool("startFly", false);
        animator.SetBool("flying", false);
        yield return new WaitForSeconds(3);
        animator.SetBool("stopFly", false);
        spriteRenderer.flipX = false;
    }

    private IEnumerator MoveToPosition(Transform transform, Vector3 position, float duration, MovementState state)
    {
        if (state == MovementState.startFlying)
        {
            animator.SetBool("flying", true);
        }
        Vector3 start = transform.position;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(start, position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = position;
        if (state == MovementState.stopflying)
        {
            animator.SetBool("stopFly", true);
        }
    }
}
