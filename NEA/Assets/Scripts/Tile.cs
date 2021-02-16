using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] tileGraphics;

    public float hoverAmount;

    private void Start()
        {

            rend = GetComponent<SpriteRenderer>();
            int randTile = Random.Range(0, tileGraphics.Length);
            rend.sprite = tileGraphics[randTile];

        }


    private void OnMouseEnter()
        {

            transform.localScale += Vector3.one * hoverAmount;
         }

    private void OnMouseExit()
    {

        transform.localScale -= Vector3.one * hoverAmount;
    }


}
