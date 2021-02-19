using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public bool selected;
    GameMaster gm;

    public int movementRange;
    public bool hasMoved;

    public float moveSpeed;

    public int playerNumber;

    public int attackRange;
    List<Units> enemiesInRange = new List<Units>();
    public bool hasAttacked;

    public GameObject weaponIcon;
    private void Start()
    {
        //this allows the script to access all public attributes and methode from GameMaster script
        gm = FindObjectOfType<GameMaster>(); 
    }

    private void OnMouseDown()
    {
        ResetWaponIcon();

        if (selected == true)
        {
            selected = false;
            gm.selectedUnit = null; //because there isn't a selected unit anymore
            gm.ResetTiles();
        } else
        {
            if (playerNumber == gm.playerTurn)
            {
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false; //this de-selects a unit if there's one already selected
                }
                //this now allows me to select the unit the player clicking on
                selected = true;
                gm.selectedUnit = this; //this refers to the this instance of the Units script thats attached to the character the player is clicking

                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
         
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

    void GetEnemies()
    {
        enemiesInRange.Clear();

        foreach (Units unit in FindObjectsOfType<Units>())
        {
            if (Mathf.Round(Mathf.Abs(transform.position.x - unit.transform.position.x)) + Mathf.Round(Mathf.Abs(transform.position.y - unit.transform.position.y)) <= attackRange)
            {
                if (unit.playerNumber != gm.playerTurn && hasAttacked == false)
                {
                    enemiesInRange.Add(unit);
                    unit.weaponIcon.SetActive(true);
                }
            }
        }
    }
     public void ResetWaponIcon()
    {
        foreach (Units unit in FindObjectsOfType<Units>())
        {
            unit.weaponIcon.SetActive(false);
        }
    }


    public void Move(Vector2 tilePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePos));
    }

    IEnumerator StartMovement(Vector2 tilePos)
    {
        while(transform.position.x != tilePos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        hasMoved = true;
        ResetWaponIcon();
        GetEnemies();
    }
}
