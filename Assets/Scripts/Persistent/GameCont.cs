using UnityEngine;
using System.Collections;
using UnityEngine.Events;

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



    private int tutoCont = 1;
    private TutoRepo tutoBbRef;

    public void MenuInitializer()
    {
        var mrTempLink = GameObject.FindGameObjectWithTag("Menu Panel").GetComponent<MenuRepo>();

        mrTempLink.NewGame.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.SwitchToTutorial);
        mrTempLink.QuitGame.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.ExitGame);
    }

    public void TutorialInitializer()
    {
        this.tutoBbRef = GameObject.FindGameObjectWithTag("TutoPanel").GetComponent<TutoRepo>();

        this.tutoBbRef.skipTuto.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.SwitchToGameplayScene);
        this.tutoBbRef.goAhead.GetComponent<ButWithoutText>().ButtonClicked.AddListener(this.TuToHandler);
    }

    private void TuToHandler()
    {
        this.tutoCont++;

        switch (this.tutoCont)
        {
            case 2:
                this.tutoBbRef.tutoTitle.text = "Tutorial 2/3";
                this.tutoBbRef.tutoText.text = "Stanze Testo";
                break;
            case 3:
                this.tutoBbRef.tutoTitle.text = "Tutorial 3/3";
                this.tutoBbRef.tutoText.text = "Nemici Testo";
                break;
            case 4:
                this.SwitchToGameplayScene();
                break;
        }
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    private void SwitchToGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void SwitchToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

}
