﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public bool selected;
    GameMaster gm;

    public int movementRange;
    public bool hasMoved;
    
    private void Start()
    {
        //this allows the script to access all public attributes and methode from GameMaster script
        gm = FindObjectOfType<GameMaster>(); 
    }

    private void OnMouseDown()
    {

        if (selected == true)
        {
            selected = false;
            gm.selectedUnit = null; //because there isn't a selected unit anymore
        } else
        {
            if(gm.selectedUnit != null)
            {
                gm.selectedUnit.selected = false; //this de-selects a unit if there's one already selected
            }
            //this now allows me to select the unit the player clicking on
            selected = true;
            gm.selectedUnit = this; //this refers to the this instance of the Units script thats attached to the character the player is clicking
            GetWalkableTiles();
        }

    }

    void GetWalkableTiles()
    {
        if (hasMoved == true)
        {
            return;
        }

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Round(Mathf.Abs(transform.position.x - tile.transform.position.x))+ Mathf.Round(Mathf.Abs(transform.position.y - tile.transform.position.y)) <= movementRange)
            {
                if (tile.IsClear() == true)
                {
                    tile.Highlight();
                }

            }
        }
    }

}
