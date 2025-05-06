using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float Move;
    private bool shouldMove = false;
    private bool OnGround = false;
    private bool IsDead = false;
    private Animator animator;
    private Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    Audio AudioManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        rb.freezeRotation = true;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsWalking", shouldMove);
        animator.SetBool("isDead", IsDead);
        if (shouldMove && OnGround && target && !IsDead)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            if (moveDirection.x < 0)
                transform.localScale = new Vector3(transform.localScale.x > 0 ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, transform.localScale.z);
            if (moveDirection.x > 0)
                transform.localScale = new Vector3(transform.localScale.x < 0 ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, transform.localScale.z);
            rb.velocity = new Vector2(speed * moveDirection.x, rb.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnGround = true;
        }

        if (other.gameObject.tag == "spike")
        {
            Debug.Log("Hit Spike");
            AudioManager.SFXplayer(AudioManager.EnemyDeath);
            IsDead = true;
            gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnGround = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            shouldMove = true;
        }
        if (other.gameObject.tag == "pit")
        {
            shouldMove = false;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            shouldMove = false;
        }

    }
}
