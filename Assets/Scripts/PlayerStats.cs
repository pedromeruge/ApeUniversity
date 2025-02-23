using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    private int currentLives = 0;
    private int score = 0;

    public void Awake()
    {
        this.currentLives = this.maxLives;
    }

    //returns true if player is dead
    public bool modifyLives(int lives) {
        Debug.Log("prevLives: " + this.currentLives + " lives: " + lives);

        this.currentLives = Mathf.Clamp(this.currentLives - lives, 0, maxLives);
        Debug.Log("currLives: " + this.currentLives);

        if (this.currentLives == 0)
        {
            return true;
        }

        return false;
    }

    public void modifyScore(int points) {
        this.score = Mathf.Max(0, this.score + points);
        Debug.Log("this.score: " + this.score);
    }

    public int getLives() {
        return this.currentLives;
    }

    public int getScore() {
        return this.score;
    }

    private void onDeath() {
        // TODO: add gameOver logic
    }
}
