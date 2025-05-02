using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Bound : MonoBehaviour
{
    public GameObject hint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Get the Player's script component that contains ColorIsObtained
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (player.ColorIsObtained)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (hint != null) hint.SetActive(true);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hint.SetActive(false);

        }
    }
}
