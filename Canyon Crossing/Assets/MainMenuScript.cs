using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Unlock cursor and make it visible
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    // Change Scene onClick of Play button
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Quit application onClick of Quit button
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
