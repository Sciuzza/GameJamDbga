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



    private int tutoCont = 1;
    private TutoRepo tutoBbRef;

    private UiRepo uiRepoRef;


    public void MenuInitializer()
    {
        var mrTempLink = GameObject.FindGameObjectWithTag("Menu Panel").GetComponent<MenuRepo>();

        this.tutoCont = 1;

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

    public void GpInitializer()
    {
        uiRepoRef = GameObject.FindGameObjectWithTag("Gameplay Ui").GetComponent<UiRepo>();

        uiRepoRef.MainMenu.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.SwitchToMenu);
        uiRepoRef.Restart.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.SwitchToGameplayScene);
        uiRepoRef.QuitGame.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.ExitGame);
        uiRepoRef.menuMove.GetComponent<ButWithoutText>().ButtonClicked.AddListener(this.MovingMenuPanel);
        this.uiRepoRef.enemyMove.GetComponent<ButWithoutText>().ButtonClicked.AddListener(this.MovingEnemyPanel);
    }

    private void MovingEnemyPanel()
    {
        if (this.uiRepoRef.MenuMoving)
            StopCoroutine("MovingEnemyP");

        if (this.uiRepoRef.EnemyHidden) this.StartCoroutine(this.MovingEnemyP(this.uiRepoRef.showUpPos));
        else
        {
            this.StartCoroutine(this.MovingEnemyP(this.uiRepoRef.HiddenPosEnemy));
        }
    }

    private void MovingMenuPanel()
    {
        if (this.uiRepoRef.MenuMoving)
            StopCoroutine("MovingMenu");

        if (this.uiRepoRef.MenuHidden) this.StartCoroutine(this.MovingMenu(this.uiRepoRef.showUpPos));
        else
        {
            this.StartCoroutine(this.MovingMenu(this.uiRepoRef.HiddenPosMenu));
        }
    }

    private IEnumerator MovingMenu(GameObject target)
    {
        uiRepoRef.MenuMoving = true;

        var whereToMove = target.GetComponent<RectTransform>();
        var objToMove = this.uiRepoRef.MenuPanelToMove.GetComponent<RectTransform>();

        var timeTaken = 0.5f;

        var oriPos = objToMove.localPosition;

        var posReached = false;

         var timePassed = 0.0f;

        while (!posReached)
        {
            timePassed += Time.deltaTime / timeTaken;

            objToMove.anchoredPosition = Vector3.Lerp(
                oriPos,
                whereToMove.localPosition,
                timePassed);

            if (timePassed >= 1)
            {
                posReached = true;
            }

            yield return null;
        }

        this.uiRepoRef.MenuMoving = false;

        this.uiRepoRef.MenuHidden = !this.uiRepoRef.MenuHidden;
    }

    private IEnumerator MovingEnemyP(GameObject target)
    {
        uiRepoRef.EnemyMoving = true;

        var whereToMove = target.GetComponent<RectTransform>();
        var objToMove = this.uiRepoRef.EnemyPanelToMove.GetComponent<RectTransform>();

        var timeTaken = 0.5f;

        var oriPos = objToMove.localPosition;

        var posReached = false;

        var timePassed = 0.0f;

        while (!posReached)
        {
            timePassed += Time.deltaTime / timeTaken;

            objToMove.anchoredPosition = Vector3.Lerp(
                oriPos,
                whereToMove.localPosition,
                timePassed);

            if (timePassed >= 1)
            {
                posReached = true;
            }

            yield return null;
        }

        this.uiRepoRef.EnemyMoving = false;

        this.uiRepoRef.EnemyHidden = !this.uiRepoRef.EnemyHidden;
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

    private void SwitchToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

}
