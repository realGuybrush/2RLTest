using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public partial class WidgetManager : MonoBehaviour
{
    public Button deleteAButton;
    public InputField actionsList;
    public GameObject deleteActionPanel;
    int caretPosition=0;
    public void UpdateDeleteActionPanel()
    {
        StringBuilder sumOfActions=new StringBuilder("");
        foreach (HumanActionStructure action in mainManager.humanActionList)
        {
            sumOfActions.Append(action.actionName);
            sumOfActions.Append("\n");
        }
        actionsList.text = sumOfActions.ToString();
    }
    void TriggerDeleteAction()
    {
        string preCaret = actionsList.text.Substring(0, caretPosition);
        mainManager.DeleteAction(preCaret.Split('\n').Length - 1);
        UpdateDeleteActionPanel();
        mainManager.Save();
    }
    public void DeleteActionPanelCallButton()
    {
        deleteActionPanel.SetActive(!deleteActionPanel.activeSelf);
        UpdateDeleteActionPanel();
        UpdateAddReactionPanel();
    }
}
