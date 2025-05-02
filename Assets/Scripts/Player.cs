using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
    public float speed;
    public float Move;
    public float jump;
    public int jumpCount;
    public bool ColorIsObtained;
    public bool isjumping;
    public bool useSpriteRendererFlip = true; // Choose flip method
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // For flipping the sprite
    public Material NewMaterial;
    public string NextLevel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 

        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if (Move < -0.1f) // Moving left
        {
            if (useSpriteRendererFlip && spriteRenderer != null)
                spriteRenderer.flipX = true;
            else
                transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (Move > 0.1f) // Moving right
        {
            if (useSpriteRendererFlip && spriteRenderer != null)
                spriteRenderer.flipX = false;
            else
                transform.localScale = Vector3.one;
        }

        if (Input.GetButtonDown("Jump") && !isjumping && jumpCount < 2)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            jumpCount++;
            Debug.Log("UP");
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isjumping = false;
            jumpCount = 0;
            Debug.Log("On the ground");
        }
        if (other.gameObject.tag == "end")
        {
            if (ColorIsObtained)
            {
                SceneManager.LoadScene(NextLevel);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Color")
        {

            other.gameObject.SetActive(false);

            string[] excludedTags = { "Player", "end", "boundry" };

            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer sprite in allSprites)
            {
                if(!(excludedTags.Contains(sprite.tag)))
                    sprite.material = NewMaterial;
            }

            Tilemap[] allTilemaps = FindObjectsOfType<Tilemap>();
            foreach (Tilemap tilemap in allTilemaps)
            {
                ColorIsObtained = true;
                tilemap.color = Color.blue;
                tilemap.GetComponent<Renderer>().material = NewMaterial;
            }

        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isjumping = jumpCount >= 2 ? true : false;
            Debug.Log("Flying bro");
        }
    }
}
