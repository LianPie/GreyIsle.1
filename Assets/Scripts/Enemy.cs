using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float Move;
    private bool shouldMove = false;
    private bool OnGround = false;
    public AnimatorController Walking;
    public AnimatorController Idle;
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMove && OnGround)
            rb.velocity = new Vector2(speed * Move, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnGround = true;
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
            AnimatorController controller = Walking; // Match your base state name
            animator.runtimeAnimatorController = controller;
            shouldMove = true;
        }
        if (other.gameObject.tag == "pit")
        {
            AnimatorController controller = Idle; // Match your base state name
            animator.runtimeAnimatorController = controller;
            shouldMove = false;
        }
        if (other.gameObject.tag == "spike")
        {
            gameObject.SetActive(false);
        }

    }
}
