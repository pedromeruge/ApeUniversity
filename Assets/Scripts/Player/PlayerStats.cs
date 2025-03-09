using System;
using System.Collections;
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
        setupUI();
    }

    private void setupUI()
    {
        if (UIUpdateLogic.Instance == null)
        {
            Debug.LogError("UIUpdateLogic instance is null");
            return;
        }
        UIUpdateLogic.Instance.changeHealth(this.currentLives);
        UIUpdateLogic.Instance.changeBombs(this.currentBombs);
        UIUpdateLogic.Instance.changePapers(this.currentMonkeys + "/" + maxMonkeys);
        UIUpdateLogic.Instance.changeMoney(this.money);
    }

    //returns true if player is dead
    public bool modifyLives(int lives) {
        this.currentLives = Mathf.Clamp(this.currentLives - lives, 0, maxLives);
        UIUpdateLogic.Instance.changeHealth(this.currentLives);

        if (this.currentLives == 0)
        {
            StartCoroutine(onDeath());
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
        UIUpdateLogic.Instance.changePapers(this.currentMonkeys + "/" + maxMonkeys);
    }

    public void modifyBombs(int bombs) {
        this.currentBombs = Mathf.Max(0, this.currentBombs + bombs);
        UIUpdateLogic.Instance.changeBombs(this.currentBombs);
    }

    public void modifyMoney(int money) {
        this.money = Mathf.Max(0, this.money + money);
        UIUpdateLogic.Instance.changeMoney(this.money);
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

    IEnumerator onDeath() {
        yield return new WaitForSeconds(1f); // allow for death animation to play before toggling game over screen
        GameStateManager.instance.gameOver();
    }
}
