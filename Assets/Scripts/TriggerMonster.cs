using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TriggerMonster : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject monster;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float stayDuration = 5f;

    private enum MovementState { startFlying, stopflying }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Trigger monster!!!");
            StartCoroutine(MoveMonsterRoundTrip(startPoint.position, targetPoint.position, speed, stayDuration));
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
        yield return MoveToPosition(monster.transform, start, moveDuration, MovementState.stopflying);
        //close animator when stop event
        animator.SetBool("startFly", false);
        animator.SetBool("flying", false);
        yield return new WaitForSeconds(3);
        animator.SetBool("stopFly", false);
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
