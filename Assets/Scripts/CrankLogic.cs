using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankLogic : MonoBehaviour
{
    public Sprite newSprite;
    public GameObject box;
    public GameObject tip;
    private bool isPulled = false;
    private bool inRange = false;
    private SpriteRenderer spriteRenderer;
    Audio AudioManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
            Debug.Log("f");

        if (inRange && Input.GetButtonDown("Interact") && !isPulled)
        {

            AudioManager.SFXplayer(AudioManager.Crank);
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
            tip.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
            tip.SetActive(!true);
        Debug.Log("out of range");
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
