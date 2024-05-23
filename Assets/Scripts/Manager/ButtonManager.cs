using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
   public void EndGame()
    {
        Debug.Log("End");
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }

    public void ResetGame()
    {
        Debug.Log("RE");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
}
