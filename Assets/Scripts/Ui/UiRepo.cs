using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class UiRepo : MonoBehaviour
{
    // Già inizializzati, necessari per l'inizializzazione dell'enemy panel
    public List<Sprite> Avatars;
    public List<UiEnemyRepo> EnemyUi;

    public GameObject EnemyPanelToMove, MenuPanelToMove;
    public Button MainMenu, QuitGame, Restart, menuMove, enemyMove;


    // si collega all'evento di Marius per far partire l'initializer dell' enemy panel
    private void Awake()
    {
        
    }


    // qua in realtà come parametro ci sarà una lista di classe di tipo nemico contenente 3 interi per elemento e il cui conteggio complessivo
    // rappresenta il numero totale di nemici nella partita
    private void InitializingEnemyPanel(int spriteIndex, int beginTime, int endTime)
    {
        
    }

}
