using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    private int currentLives = 0;
    [SerializeField] private int maxMonkeys = 5;
    private int currentMonkeys = 0;

    [SerializeField] int startBombs = 3;
    [SerializeField] private int currentBombs = 0;

    private int money = 0;
    private int score = 0;

    public void Awake()
    {
        this.currentLives = this.maxLives;
        this.currentMonkeys = 0;
        this.currentBombs = this.startBombs;
        this.score = 0;
    }

    //returns true if player is dead
    public bool modifyLives(int lives) {
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

    public void modifyMonkeys(int monkeys) {
        this.currentMonkeys = Mathf.Clamp(this.currentMonkeys + monkeys, 0, maxMonkeys);
        Debug.Log("this.monkeysCaught: " + this.currentMonkeys);
    }

    public void modifyBombs(int bombs) {
        this.currentBombs = Mathf.Max(0, this.currentBombs + bombs);
        Debug.Log("this.bombs: " + this.currentBombs);
    }

    public void modifyMoney(int money) {
        this.money = Mathf.Max(0, this.money + money);
        Debug.Log("this.money: " + this.money);
    }

    public int getLives() {
        return this.currentLives;
    }

    public int getScore() {
        return this.score;
    }

    public int getMonkeys() {
        return this.currentMonkeys;
    }

    public int getBombs() {
        return this.currentBombs;
    }

    public int getMoney() {
        return this.money;
    }

    private void onDeath() {
        // TODO: add gameOver logic
    }
}
