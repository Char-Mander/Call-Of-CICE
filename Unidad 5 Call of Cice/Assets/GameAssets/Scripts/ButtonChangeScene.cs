using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : MonoBehaviour
{
    [SerializeField]
    private bool isExitBtn;
    [SerializeField]
    private string sceneName;

    private void OnMouseDown()
    {
        if (isExitBtn)
        {
            ExitGame();
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }

}
