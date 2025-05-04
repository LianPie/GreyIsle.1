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
    public bool dead = false;
    public bool isjumping;
    public bool useSpriteRendererFlip = true; // Choose flip method
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // For flipping the sprite
    public Material NewMaterial;
    public string NextLevel;
    public string CurrentScene;

    public GameObject gameOver;
    public float gameOverDuration = 3f;

    Audio AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(speed * Move, rb.velocity.y);

            if (Move < -0.1f) // Moving left
            {
                if (useSpriteRendererFlip)
                    transform.localScale = new Vector3(transform.localScale.x > 0 ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (Move > 0.1f) // Moving right
            {
                if (useSpriteRendererFlip)
                    transform.localScale = new Vector3(transform.localScale.x < 0 ? - transform.localScale.x: transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            if (Input.GetButtonDown("Jump") && !isjumping && jumpCount < 2)
            {
                AudioManager.SFXplayer(AudioManager.Jump);
                rb.AddForce(new Vector2(rb.velocity.x, jump));
                jumpCount++;
                Debug.Log("UP");
            }
        }
        else
        {
            gameOverDuration -= Time.deltaTime;
            if (gameOverDuration <= 0f)
            {
                SceneManager.LoadScene(CurrentScene);
            }
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

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("ENEMY");
            AudioManager.SFXplayer(AudioManager.Gameover);
            gameOver.SetActive(true);
            dead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Color")
        {
            AudioManager.SFXplayer(AudioManager.Obtained);

            other.gameObject.SetActive(false);

            string[] excludedTags = { "Player", "end", "boundry","pit" };

            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer sprite in allSprites)
            {
                if (!(excludedTags.Contains(sprite.tag)))
                    sprite.material = NewMaterial;
            }

            Tilemap[] allTilemaps = FindObjectsOfType<Tilemap>();
            foreach (Tilemap tilemap in allTilemaps)
            {
                ColorIsObtained = true;
                tilemap.GetComponent<Renderer>().material = NewMaterial;
            }
        }
        if (other.gameObject.tag == "spike")
        {
            Debug.Log("SPIKES");
            AudioManager.SFXplayer(AudioManager.Gameover);
            gameOver.SetActive(true);
            dead = true;
        }
        if (other.gameObject.tag == "Water")
        {
            Debug.Log("Water");
            AudioManager.SFXplayer(AudioManager.Splash);
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isjumping = jumpCount >= 2 ? true : false;
            Debug.Log("Flying");
        }
    }

}
