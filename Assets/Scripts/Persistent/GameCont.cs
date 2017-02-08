using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerStats
{
    public int Life;
    public int Moves;

}

public class event_int_int_int : UnityEvent<int, int, int>{

}
public class GameCont : MonoBehaviour {

    public event_int_int_int initia;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    public void NgpInitializer()
    {
        MenuRepo mrTempLink = GameObject.FindGameObjectWithTag("Menu Panel").GetComponent<MenuRepo>();

        mrTempLink.NewGame.GetComponent<PointerHandler>().ButtonClicked.AddListener(this.SwitchToGameplayScene);
    }

    private void SwitchToGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }


}
