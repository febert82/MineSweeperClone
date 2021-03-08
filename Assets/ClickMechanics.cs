using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMechanics : MonoBehaviour
{
    Minefield minefield;
    SpriteController spriteController;
    Tile tile;

    // Start is called before the first frame update
    void Start()
    {
        this.minefield = GameObject.FindGameObjectWithTag("Minefield").GetComponent<Minefield>();
        this.spriteController = this.GetComponent<SpriteController>();
        this.tile = this.GetComponent<Tile>();
    }

    private void OnMouseUpAsButton()
    {
        this.ClickTile();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (this.tile.isSecured)
            {
                this.spriteController.SetDefaultTileSprite();
                this.tile.isSecured = false;
                this.minefield.minesLeft++;
            }
            else
            {
                this.spriteController.SetSecuredTileSprite();
                this.tile.isSecured = true;
                this.minefield.minesLeft--;
            }
        }
    }

    void ClickTile()
    {
        if(!this.minefield.hasGameStarted)
        {
            this.minefield.hasGameStarted = true;

            this.CreateMines();
            this.minefield.timer.StartTimer();
        }

        if(this.tile.isMine)
        {
            this.minefield.LoseGame();
        }
        else
        {
            this.RevealTile();

            if (this.minefield.IsGameWon()) {
                this.minefield.WinGame();
            }
        }
    }

    void CreateMines()
    {
        Debug.Log("Create mines");

        int minesLeft = this.minefield.amountMines;
        int tilesLeft = this.minefield.amountTilesUnrevealed;

        for (int x = 0; x < this.minefield.xTotal; x++)
        {
            for (int y = 0; y < this.minefield.yTotal; y++)
            {
                if (!(x == this.tile.x && y == this.tile.y))
                {
                    Tile aTile = this.minefield.tiles[x, y];

                    float chanceForMine = (float)minesLeft / (float)tilesLeft;

                    if (Random.value <= chanceForMine)
                    {
                        aTile.isMine = true;
                        minesLeft--;
                    }
                }
                tilesLeft--;
            }
        }
    }

    public void RevealTile()
    {
        if (!this.tile.isReveald && !this.tile.isMine)
        {
            this.tile.isReveald = true;
            this.minefield.amountTilesUnrevealed--;

            int amountNeighbourMines = this.GetAmountNeighbourMines();

            this.spriteController.SetEmptyTileSprite(amountNeighbourMines);

            if (amountNeighbourMines == 0)
            {
                this.RevealIfValid(this.tile.x - 1, this.tile.y - 1);
                this.RevealIfValid(this.tile.x - 1, this.tile.y);
                this.RevealIfValid(this.tile.x - 1, this.tile.y + 1);
                this.RevealIfValid(this.tile.x, this.tile.y - 1);
                this.RevealIfValid(this.tile.x, this.tile.y + 1);
                this.RevealIfValid(this.tile.x + 1, this.tile.y - 1);
                this.RevealIfValid(this.tile.x + 1, this.tile.y);
                this.RevealIfValid(this.tile.x + 1, this.tile.y + 1);
            }
        }
    }

    void RevealIfValid(int x, int y)
    {
        if (x >= 0 && x < this.minefield.xTotal && y>= 0 && y < this.minefield.yTotal)
        {
            this.minefield.tiles[x, y].clickMechanics.RevealTile();
        }
    }

    public int GetAmountNeighbourMines()
    {
        int mineCounter = 0;
        if (this.HasMine(this.tile.x - 1, this.tile.y - 1)) mineCounter++;
        if (this.HasMine(this.tile.x - 1, this.tile.y)) mineCounter++;
        if (this.HasMine(this.tile.x - 1, this.tile.y + 1)) mineCounter++;
        if (this.HasMine(this.tile.x, this.tile.y - 1)) mineCounter++;
        if (this.HasMine(this.tile.x, this.tile.y + 1)) mineCounter++;
        if (this.HasMine(this.tile.x + 1, this.tile.y - 1)) mineCounter++;
        if (this.HasMine(this.tile.x + 1, this.tile.y)) mineCounter++;
        if (this.HasMine(this.tile.x + 1, this.tile.y + 1)) mineCounter++;
        return mineCounter;
    }

    bool HasMine(int x, int y)
    {
        bool hasMine = false;

        if (x >= 0 && x < this.minefield.xTotal && y >= 0 && y < this.minefield.yTotal)
        {
            hasMine = this.minefield.tiles[x, y].isMine;
        }

        return hasMine;
    }
}
