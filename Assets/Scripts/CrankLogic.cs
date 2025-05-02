using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankLogic : MonoBehaviour
{
    public Sprite newSprite;
    public GameObject box;
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
        if (Input.GetButtonDown("Interact"))
            Debug.Log("f");

        if (inRange && Input.GetButtonDown("Interact") && !isPulled)
        {
            spriteRenderer.sprite = newSprite;
            box.SetActive(true);
            isPulled = true;
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
