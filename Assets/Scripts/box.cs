using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject box;
    public GameObject color;
    Audio AudioManager;

    void Start()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && box.activeSelf)
        {
            AudioManager.SFXplayer(AudioManager.Box);
            box.SetActive(false);
        }
    }

}
