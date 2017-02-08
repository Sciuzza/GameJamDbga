using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats
{
    public int Life;
    public int Moves;

}

public class event_int_int_int : UnityEvent<int, int, int>
{

}

public class GameCont : MonoBehaviour
{

    public event_int_int_int initia;



    private int tutoCont = 1;
    private TutoRepo tutoBbRef;

    private UiRepo uiRepoRef;


    private List<Button> selectedButNote;
    private Color notSelected;
    private Color selected;

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
                this.tutoBbRef.tutoTitle.text = "Tutorial 2/4";
                this.tutoBbRef.tutoText.text = "Stanze Testo";
                break;
            case 3:
                this.tutoBbRef.tutoTitle.text = "Tutorial 3/4";
                this.tutoBbRef.tutoText.text = "Nemici Testo";
                break;
            case 4:
                this.tutoBbRef.tutoTitle.text = "Tutorial 4/4";
                this.tutoBbRef.tutoText.text = "Porta Uscita Testo";
                break;
            case 5:
                this.SwitchToGameplayScene();
                break;
        }

    }

    #region Gamplay Methods
    public void GpInitializer()
    {
        this.uiRepoRef = GameObject.FindGameObjectWithTag("Gameplay Ui").GetComponent<UiRepo>();

        this.uiRepoRef.MainMenu.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.SwitchToMenu);
        this.uiRepoRef.Restart.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.SwitchToGameplayScene);
        this.uiRepoRef.QuitGame.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.ExitGame);
        this.uiRepoRef.menuMove.GetComponent<ButWithoutText>().ButtonClicked.AddListener(this.MovingMenuPanel);
        this.uiRepoRef.enemyMove.GetComponent<ButWithoutText>().ButtonClicked.AddListener(this.MovingEnemyPanel);

        this.uiRepoRef.HourUp.GetComponent<ButWithoutText>().ButtonClicked.AddListener(this.SetHourUp);
        this.uiRepoRef.HourDown.GetComponent<ButWithoutText>().ButtonClicked.AddListener(this.SetHourDown);
        this.uiRepoRef.Conferma.GetComponent<ButtonWithTextH>().ButtonClicked.AddListener(this.ConfirmSelection);


        this.selectedButNote = new List<Button>();

        foreach (var hourBut in this.uiRepoRef.HourNote)
        {
            hourBut.gameObject.GetComponent<specialNoteBut>().NoteButClicked.AddListener(this.ButtonNoteHandler);
        }

        this.notSelected = new Color(Mathf.InverseLerp(0, 255, 129), Mathf.InverseLerp(0, 255, 41), Mathf.InverseLerp(0, 255, 123), Mathf.InverseLerp(0, 255, 100));
        this.selected = new Color(Mathf.InverseLerp(0, 255, 255), Mathf.InverseLerp(0, 255, 0), Mathf.InverseLerp(0, 255, 0), Mathf.InverseLerp(0, 255, 100));

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


    private void SetHourUp()
    {
        int currentHour = int.Parse(this.uiRepoRef.HourText.text);
        currentHour++;

        if (currentHour == 24)
        {
            currentHour = 0;
            this.uiRepoRef.HourText.text = "0" + currentHour;
        }
        else if (currentHour < 10)
            this.uiRepoRef.HourText.text = "0" + currentHour;
        else
        {
            this.uiRepoRef.HourText.text = currentHour.ToString();
        }
    }

    private void SetHourDown()
    {
        int currentHour = int.Parse(this.uiRepoRef.HourText.text);
        currentHour--;

        if (currentHour == -1)
        {
            currentHour = 23;
            this.uiRepoRef.HourText.text = currentHour.ToString();
        }
        else if (currentHour < 10)
            this.uiRepoRef.HourText.text = "0" + currentHour;
        else
        {
            this.uiRepoRef.HourText.text = currentHour.ToString();
        }
    }

    private void ConfirmSelection()
    {
        if (int.Parse(this.uiRepoRef.HourText.text) == Sync.getHour() + 1)
        {
            Debug.Log("Hai vinto Stronzo");
            this.SwitchToMenu();
        }
        else
        {
           Debug.Log("Hai perso AHAHAHAHAH");
            this.SwitchToMenu();
        }
    }

    private void ButtonNoteHandler(Button butClicked)
    {
        if (this.selectedButNote.Contains(butClicked))
        {
            this.selectedButNote.Remove(butClicked);
            butClicked.gameObject.GetComponent<Image>().color = this.notSelected;
        }
        else
        {
            this.selectedButNote.Add(butClicked);
            butClicked.gameObject.GetComponent<Image>().color = this.selected;
        }
    }

    public void ActiveExitPanel()
    {
        this.uiRepoRef.ExitPanel.SetActive(true);

        this.uiRepoRef.menuMove.GetComponent<ButWithoutText>().ButtonClicked.RemoveAllListeners();
        this.uiRepoRef.enemyMove.GetComponent<ButWithoutText>().ButtonClicked.RemoveAllListeners();
        // variabile booleana da settare su false per bloccare input giocatore
        //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCont>().ActiveExitPanel();
    }


    public void DisablingNote()
    {
        foreach (var hourBut in this.uiRepoRef.HourNote)
        {
            hourBut.interactable = false;
        }
    }

    public void EnablingNote()
    {
        foreach (var hourBut in this.uiRepoRef.HourNote)
        {
            hourBut.interactable = true;
        }
    }

    public void CalculatingNewNotes(int hourCost)
    {
        List<int> hoursSelected = new List<int>();

        for (int i = 0; i < this.selectedButNote.Count; i++)
        {
            hoursSelected.Add(this.uiRepoRef.HourNote.IndexOf(this.selectedButNote[i]) + hourCost);

            if (hoursSelected[hoursSelected.Count - 1] > 24) hoursSelected[hoursSelected.Count - 1] %= 24;
        }

        foreach (var previousSelNote in this.selectedButNote)
        {
            previousSelNote.gameObject.GetComponent<Image>().color = this.notSelected;
        }

        this.selectedButNote.Clear();
        this.selectedButNote.TrimExcess();

        foreach (var noteIndex in hoursSelected)
        {
            this.selectedButNote.Add(this.uiRepoRef.HourNote[noteIndex - 1]);
            this.selectedButNote[this.selectedButNote.Count - 1].gameObject.GetComponent<Image>().color = this.selected;
        }
        
    }
    #endregion

    #region Do not Destroy Behaviour
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    #endregion

    #region Scene Handler Methods
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
    #endregion

}
