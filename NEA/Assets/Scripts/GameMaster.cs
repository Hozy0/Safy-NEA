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

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>()) //this allows me to create a function resets the tiles to their original colour
        {
            tile.Reset();
        }
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
    }
}
