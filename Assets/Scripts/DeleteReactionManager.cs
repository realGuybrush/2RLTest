using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public partial class WidgetManager : MonoBehaviour
{
    public GameObject deleteReactionPanel;
    public Dropdown actionsRDeletePanel;
    public Dropdown moodsRDeletePanel;
    public Text reactionRDeletePanel;
    public Button deleteRButton;
    void TriggerDeleteReaction()
    {
        mainManager.DeleteReaction((byte)moodsRDeletePanel.value, actionsRDeletePanel.value);
        UpdateReactionDRPanel();
        mainManager.Save();
    }
    public void RemoveReactionPanelCallButton()
    {
        deleteReactionPanel.SetActive(!deleteReactionPanel.activeSelf);
        UpdateDeleteReactionPanel();
    }
    void UpdateReactionDRPanel()
    {
        reactionRDeletePanel.text = mainManager.catReactionList[moodsRDeletePanel.value][actionsRDeletePanel.value]==null?
                         "": mainManager.catReactionList[moodsRDeletePanel.value][actionsRDeletePanel.value].reactionName;
    }
    void UpdateDeleteReactionPanel()
    {
        actionsRDeletePanel.ClearOptions();
        actionsRDeletePanel.AddOptions(mainManager.humanActionList.Select(a => a.actionName).ToList());
        actionsRDeletePanel.value = 0;
        UpdateReactionDRPanel();
    }
}
