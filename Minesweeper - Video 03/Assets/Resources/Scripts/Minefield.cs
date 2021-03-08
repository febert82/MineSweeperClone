﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minefield : MonoBehaviour
{

    public int amountMines;
    public int amountTilesUnrevealed;
    public bool hasGameStarted = false;

    public int xTotal;
    public int yTotal;

    public Tile[,] tiles;

    public int minesLeft = 0;

    public void CreateMineField(int xTotal, int yTotal, int amountMines){
        this.xTotal = xTotal;
        this.yTotal = yTotal;
        this.amountMines = amountMines;
        this.amountTilesUnrevealed = xTotal * yTotal;
        this.hasGameStarted = false;

        this.minesLeft = amountMines;



        if(this.tiles != null){
            foreach(Tile tile in this.tiles){
                Destroy(tile.gameObject);
            }
        }

        this.tiles = new Tile[xTotal, yTotal];

        for (int x = 0; x < xTotal; x++){
            for (int y = 0; y < yTotal; y++ ){
                tiles[x, y] = Tile.CreateNewTile(x, y);
            }
        }

    }

    private void Start()
    {
        CreateMineField(10,10,10);
    }



}
