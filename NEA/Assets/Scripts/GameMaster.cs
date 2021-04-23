using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //this allows me to create and modify UI related variables

public class GameMaster : MonoBehaviour
{
    public Units selectedUnit;

    public int playerTurn = 1;

    public Image playerIndicator;
    public Sprite player1Indicator;
    public Sprite player2Indicator;

    public int player1Gold = 100;
    public int player2Gold = 100;

    public Text player1GoldText;
    public Text player2GoldText;

    public BarrackItem purchasedItem;

    public void UpdateGoldText()
    {
        player1GoldText.text = player1Gold.ToString();
        player2GoldText.text = player2Gold.ToString();
    }

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>()) //this allows me to create a function resets the tiles to their original colour
        {
            tile.Reset();
        }
    }

    void GetGoldIncome(int playerTurn)
    {
        foreach (Alchimist alchimist in FindObjectsOfType<Alchimist>())
        {
            if (alchimist.playerNumber == playerTurn)
            {
                if (playerTurn == 1)
                {
                    player1Gold += alchimist.goldPerTurn;
                }
                else
                {
                    player2Gold += alchimist.goldPerTurn;
                }
            }
        }

        UpdateGoldText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    void EndTurn()
    {
        GetGoldIncome(playerTurn);
        if (playerTurn == 1)
        {
            playerTurn = 2;
            playerIndicator.sprite = player2Indicator;
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
            playerIndicator.sprite = player1Indicator;
        }

        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Units unit in FindObjectsOfType<Units>())
        {
            unit.hasMoved = false;
            unit.weaponIcon.SetActive(false);
            unit.hasAttacked = false;
        }

        GetComponent<Barrack>().CloseMenus();
    }
}
