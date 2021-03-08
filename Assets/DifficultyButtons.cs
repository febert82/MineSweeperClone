using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButtons : MonoBehaviour
{
    Minefield mineField;

    public string currentDifficulty = "easy";

    // Start is called before the first frame update
    void Start()
    {
        this.mineField = GameObject.FindGameObjectWithTag("Minefield").GetComponent<Minefield>();

        this.SetEasy();
    }

    public void SetEasy()
    {
        this.mineField.CreateMineField(10, 10, 10);
        this.currentDifficulty = "easy";
    }

    public void SetMedium()
    {
        this.mineField.CreateMineField(20, 20, 40);
        this.currentDifficulty = "medium";
    }

    public void SetHard()
    {
        this.mineField.CreateMineField(30, 20, 60);
        this.currentDifficulty = "hard";
    }

    public void ResetGame()
    {
        if(this.currentDifficulty == "easy")
        {
            SetEasy();
        }
        else if(this.currentDifficulty == "medium")
        {
            SetMedium();
        }
        else if(this.currentDifficulty == "hard")
        {
            SetHard();
        }
    }
}
