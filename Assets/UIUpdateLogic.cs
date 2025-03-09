using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class UIUpdateLogic : MonoBehaviour
{
    public static UIUpdateLogic Instance;
    private Dictionary<string, Label> labels = new Dictionary<string, Label>();
    void Awake()
    {
        Instance = this;
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        labels.Add("statTextHealth", root.Q<Label>("statTextHealth"));
        labels.Add("statTextBombs", root.Q<Label>("statTextBombs"));
        labels.Add("statTextPapers", root.Q<Label>("statTextPapers"));
        labels.Add("statTextMoney", root.Q<Label>("statTextMoney"));
    }
    public void changeHealth(int health)
    {
        labels["statTextHealth"].text = health.ToString();
    }
    public void changeBombs(int bombs)
    {
        labels["statTextBombs"].text = bombs.ToString();
    }
    public void changePapers(string papers)
    {
        labels["statTextPapers"].text = papers;
    }
    public void changeMoney(int money)
    {
        labels["statTextMoney"].text = money.ToString();
    }
}
