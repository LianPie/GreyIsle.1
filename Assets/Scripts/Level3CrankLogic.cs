using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3CrankLogic : MonoBehaviour
{
    public Sprite ogSprite;
    public Sprite newSprite;
    public GameObject initSet;
    public GameObject SecondarySet;
    private bool isPulled = false;
    private bool inRange = false;
    private SpriteRenderer spriteRenderer; // For flipping the sprite
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange && Input.GetButtonDown("Interact") && !isPulled)
        {
            Debug.Log("f");
            spriteRenderer.sprite = newSprite;
            initSet.SetActive(false);
            SecondarySet.SetActive(true);
            isPulled = true;
        }
        else if (inRange && Input.GetButtonDown("Interact") && isPulled)
        {
            spriteRenderer.sprite = ogSprite;
            initSet.SetActive(true);
            SecondarySet.SetActive(false);
            isPulled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("within range");
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out of range");
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
