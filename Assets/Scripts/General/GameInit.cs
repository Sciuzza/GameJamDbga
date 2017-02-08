using UnityEngine;
using System.Collections;

using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "PreLoading")
        {
            SceneManager.LoadScene("Main Menu");
        }
        else if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            GameCont gcTempLink = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCont>();

            gcTempLink.MenuInitializer();
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            GameCont gcTempLink = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCont>();

            gcTempLink.TutorialInitializer();
        }

    }
}
