using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class VictoryUIUpdateLogic : MonoBehaviour
{
    public static VictoryUIUpdateLogic Instance;
    private Dictionary<string, Label> labels = new Dictionary<string, Label>();

    void Awake()
    {
        Instance = this;
    }
    
    public void InitializeLabels(int money, string time, int score)
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        labels.Add("scoreText1", root.Q<Label>("scoreText1"));
        labels.Add("scoreText2", root.Q<Label>("scoreText2"));
        changeScore(score);
        labels.Add("moneyText", root.Q<Label>("moneyText"));
        changeMoney(money);
        labels.Add("timeText", root.Q<Label>("timeText"));
        changeTime(time);
    }
    public void changeScore(int score)
    {
        labels["scoreText1"].text = score.ToString();
        labels["scoreText2"].text = score.ToString();
    }
    public void changeMoney(int money)
    {
        labels["moneyText"].text = money.ToString();
    }
    public void changeTime(string time)
    {
        labels["timeText"].text = time;
    }
}
