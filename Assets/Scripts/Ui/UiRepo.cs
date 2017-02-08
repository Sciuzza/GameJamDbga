using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class UiRepo : MonoBehaviour
{
    // Già inizializzati, necessari per l'inizializzazione dell'enemy panel
    public List<Sprite> Avatars;
    public List<UiEnemyRepo> EnemyUi;

    public GameObject EnemyPanelToMove, MenuPanelToMove, showUpPos, HiddenPosEnemy, HiddenPosMenu;
    public Button MainMenu, QuitGame, Restart, menuMove, enemyMove;

    public bool MenuMoving, MenuHidden, EnemyMoving, EnemyHidden;

    public Button HourUp, HourDown, Conferma;
    public GameObject ExitPanel;
    public Text HourText;

    public List<Button> HourNote;
}
