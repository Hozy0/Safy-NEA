﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] tileGraphics;

    public float hoverAmount;

    public LayerMask obstacleLayer;

    public Color highlightedColor;
    public bool isWalkable;
    GameMaster gm;

    public Color creatableColor;
    public bool isCreatable;

    private void Start()
        {

            rend = GetComponent<SpriteRenderer>();
            int randTile = Random.Range(0, tileGraphics.Length);
            rend.sprite = tileGraphics[randTile];

        gm = FindObjectOfType<GameMaster>();

        }


    private void OnMouseEnter()
        {

            transform.localScale += Vector3.one * hoverAmount;
         }

    private void OnMouseExit()
    {

        transform.localScale -= Vector3.one * hoverAmount;
    }

    public bool IsClear()
    {

        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        if (obstacle != null)
        {
            return false;
        }else
        {
            return true;
        }

    }

    public void Highlight() //this will highlight a tile if walkable
    {
        rend.color = highlightedColor;
        isWalkable = true;
    }

    public void Reset() //resets tile to original colour
    {
        rend.color = Color.white;
        isWalkable = false;
        isCreatable = false;
    }

    public void Creatable() //this will highlight a tile if you can create a unit on it
    {
        rend.color = creatableColor;
        isCreatable = true;
    }

    private void OnMouseDown()
    {
        if (isWalkable && gm.selectedUnit != null)
        {
            gm.selectedUnit.Move(this.transform.position);
        }
        else if (isCreatable == true)
        {
            BarrackItem item = Instantiate(gm.purchasedItem, new Vector2(transform.position.x, transform.position.y), Quaternion.identity); //spawns the purchased item on the selected tile
            gm.ResetTiles();
            Units unit = item.GetComponent<Units>();
            if (unit != null)
            {
                unit.hasAttacked = true;
                unit.hasMoved = true;
            }
        }
    }
}
