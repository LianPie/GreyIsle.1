using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3CrankLogic : MonoBehaviour
{
    public Sprite ogSprite;
    public Sprite newSprite;
    public GameObject tip;
    public GameObject initSet;
    public GameObject SecondarySet;
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

        if (inRange && Input.GetButtonDown("Interact") && !isPulled)
        {
            Debug.Log("f");
            AudioManager.SFXplayer(AudioManager.Crank);
            spriteRenderer.sprite = newSprite;
            initSet.SetActive(false);
            SecondarySet.SetActive(true);
            isPulled = true;
        }
        else if (inRange && Input.GetButtonDown("Interact") && isPulled)
        {
            AudioManager.SFXplayer(AudioManager.Crank);
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
            tip.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out of range");
        if (other.gameObject.tag == "Player")
        {
            tip.SetActive(!true);
            inRange = false;
        }
    }
}
