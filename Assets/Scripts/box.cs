using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject? box;
    public GameObject color;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            box?.SetActive(false);
        }
    }

}
