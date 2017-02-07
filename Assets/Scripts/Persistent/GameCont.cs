using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class PlayerStats
{
    public int Life;
    public int Moves;

}


public class GameCont : MonoBehaviour {


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
