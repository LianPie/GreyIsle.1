using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
