using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public int x;
    public int y;

    public bool isMine = false;
    public bool isReveald = false;
    public bool isSecured = false;



    private void Start()
    {
       
       
    }

    public static Tile CreateNewTile(int x, int y){

        GameObject tile = (GameObject)Instantiate(Resources.Load("Prefabs/Tile"));

        GameObject tiles = GameObject.FindGameObjectWithTag("Tiles");

        Minefield mineField = GameObject.FindGameObjectWithTag("Minefield").GetComponent<Minefield>();

        tile.GetComponent<Tile>().x = x;
        tile.GetComponent<Tile>().y = y;

        tile.transform.parent = tiles.transform;

        tile.transform.position = new Vector2((float)x - ((float)mineField.xTotal - 1f) / 2f, (float)y - ((float)mineField.yTotal - 1f) / 2f);




        return tile.GetComponent<Tile>();
    }



}
