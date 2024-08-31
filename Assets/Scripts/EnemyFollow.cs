using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    [SerializeField] private float speed = 5f; 
    private Transform target; 
    private Vector3 startPosition; 

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;// Lưu vị trí ban đầu
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            spriteRenderer.flipX = false;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            spriteRenderer.flipX = true;
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if(transform.position == startPosition)
            {
                animator.SetBool("Desapper", true);
                spriteRenderer.enabled = false;
                animator.SetBool("Appear", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") // Giả sử người chơi có tag là "Player"
        {
            animator.SetBool("Appear", true);
            spriteRenderer.enabled = true;
            target = collision.transform; // Đặt người chơi làm mục tiêu
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            target = null; // Ngừng đuổi theo khi người chơi rời khỏi khu vực
        }
    }
}
