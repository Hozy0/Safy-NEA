using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Units selectedUnit;

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>()) //this allows me to create a function resets the tiles to their original colour
        {
            tile.Reset();
        }
    }
}
