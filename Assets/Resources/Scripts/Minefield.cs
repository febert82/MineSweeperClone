using JetBrains.Annotations;
using System.Collections;
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

    public Timer timer;

    public ResetGameButton resetGameButton;

    public HighScore highscore;

    public int minesLeft = 0;

    public void CreateMineField(int xTotal, int yTotal, int amountMines)
    {
        this.xTotal = xTotal;
        this.yTotal = yTotal;
        this.amountMines = amountMines;
        this.amountTilesUnrevealed = xTotal * yTotal;
        this.hasGameStarted = false;      

        this.minesLeft = amountMines;

        this.timer.ResetTimer();

        this.resetGameButton.SetNeutral();

        if (this.tiles != null)
        {
            foreach (Tile tile in this.tiles)
            {
                Destroy(tile.gameObject);
            }
        }

        this.tiles = new Tile[xTotal, yTotal];

        for (int x = 0; x < xTotal; x++)
        {
            for (int y = 0; y < yTotal; y++)
            {
                tiles[x, y] = Tile.CreateNewTile(x, y);
            }
        }

    }

    public bool IsGameWon()
    {
        if (amountTilesUnrevealed == this.amountMines)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoseGame()
    {
        Debug.Log("Game Lost");

        this.timer.StopTimer();
        this.resetGameButton.SetSad();

        foreach (Tile tile in this.tiles)
        {
            if (!tile.isMine)
            {
                tile.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                tile.spriteController.SetMineSprite();
            }
        }
    }

    public void WinGame()
    {
        Debug.Log("Win Game");

        this.timer.StopTimer();
        this.resetGameButton.SetHappy();

        foreach (Tile tile in this.tiles)
        {
            if (tile.isMine)
            {
                tile.spriteController.SetSecuredMineSprite();
            }
        }

        this.highscore.UpdateHighscore(this.timer.GetCurrentTime());
    }
}
