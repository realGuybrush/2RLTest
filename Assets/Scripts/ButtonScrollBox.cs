using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScrollBox : MonoBehaviour
{
    public Scrollbar buttonScrollBar;
    string[] actions = new string[] { "Поиграть", "Накормить", "Подойти", "Погладить", "Дать пинка" };
    public Button[] Buttons;
    private void Start()
    {
        Buttons[0].onClick.AddListener(() => ExecuteHumanAction(0));
        Buttons[1].onClick.AddListener(() => ExecuteHumanAction(1));
        Buttons[2].onClick.AddListener(() => ExecuteHumanAction(2));
        Buttons[3].onClick.AddListener(() => ExecuteHumanAction(3));
        Buttons[4].onClick.AddListener(() => ExecuteHumanAction(4));
    }
    public void SetButtonNames(string[] newActions)
    {
        RemoveButton((uint)actions.Length);
        actions = newActions;
        AddButton((uint)actions.Length);
        UpdateButtonNames();
    }
    public void AddButton(uint amount=1)
    {
        buttonScrollBar.numberOfSteps = actions.Length>5?actions.Length-5:0;
        buttonScrollBar.size = 1f / (buttonScrollBar.numberOfSteps + 1);
        AdjustScrollBarVisibility();
        UpdateButtonNames();
    }
    public void RemoveButton(uint amount = 1)
    {
        buttonScrollBar.numberOfSteps -= (buttonScrollBar.numberOfSteps >= amount ? (int)amount : buttonScrollBar.numberOfSteps);
        buttonScrollBar.size = 1f / (buttonScrollBar.numberOfSteps + 1);
        AdjustScrollBarVisibility();
        UpdateButtonNames();
    }
    public void AdjustScrollBarVisibility()
    {
        if (buttonScrollBar.numberOfSteps > 0)
        {
            buttonScrollBar.gameObject.SetActive(true);
        }
        else
        {
            buttonScrollBar.gameObject.SetActive(false);
        }
    }
    public void UpdateButtonNames()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = actions.Length > i?actions[i + (int)(buttonScrollBar.value/(1.0f/buttonScrollBar.numberOfSteps))]:"";
        }
    }

    public void ExecuteHumanAction(int buttonIndex)
    {
        GameObject.Find("Canvas").GetComponent<RelationshipManager>().HumanAct(buttonIndex + (int)(buttonScrollBar.value / (1.0f / buttonScrollBar.numberOfSteps)));
    }
}
