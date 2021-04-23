using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrack : MonoBehaviour
{

    public Button player1ToggleButton;
    public Button player2ToggleButton;

    public GameObject player1Menu;
    public GameObject player2Menu;

    GameMaster gm;

    private void Start()
    {
        gm = GetComponent<GameMaster>();
    }

    private void Update()
    {// I'm making sure you can only press on your button during your turn
        if (gm.playerTurn == 1)
        {
            player1ToggleButton.interactable = true;
            player2ToggleButton.interactable = false;
        }
        else
        {
            player1ToggleButton.interactable = false;
            player2ToggleButton.interactable = true;
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf); //menu.activeSelf returns weather the menu is active or not
    }

    public void CloseMenus()
    {
        player1Menu.SetActive(false);
        player2Menu.SetActive(false);
    }

    public void BuyItem(BarrackItem item)
    {
        if (gm.playerTurn == 1 && item.cost <= gm.player1Gold )
        {
            gm.player1Gold -= item.cost;
        } else if (gm.playerTurn == 2 && item.cost <= gm.player2Gold)
        {
            gm.player2Gold -= item.cost;
        } else
        {
            print("NOT ENOUGH GOLD!");
            return;
        }

        gm.UpdateGoldText();

        gm.purchasedItem = item; //stores the currently purchased unit

        if (gm.selectedUnit != null)
        {
            gm.selectedUnit.selected = false;
            gm.selectedUnit = null;
        }

        GetCreatableTiles();
    }
    void GetCreatableTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (tile.IsClear())
            {
                tile.Creatable();
            }
        }
    }
}
