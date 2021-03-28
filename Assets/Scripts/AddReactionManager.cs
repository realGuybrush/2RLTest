using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public partial class WidgetManager : MonoBehaviour
{
    public GameObject addReactionPanel;
    public Dropdown actionsRAddPanel;
    public Dropdown moodsRAddPanel;
    public InputField newReaction;
    public InputField newReactionTime;
    public Dropdown moodChange;
    public InputField pathToNewReactionIcon;
    public Button browseReactionIcon;
    public Dropdown subActionsRAddPanel;
    public InputField newSubReactionName;
    public InputField pathToNewSubReactionIcon;
    public Button subBrowseReactionIcon;
    public Button addRButton;
    void TriggerAddReaction()
    {
        int newTime = 0;
        try
        { newTime=System.Convert.ToInt32(newReactionTime.text)*100; }
        catch { }
        mainManager.AddReaction((byte)moodsRAddPanel.value, actionsRAddPanel.value, new CatReactionStructure (newReaction.text, newTime, (byte)moodChange.value, pathToNewReactionIcon.text, SpriteToSerialize.PathToSprite(pathToNewReactionIcon.text), subActionsRAddPanel.options[subActionsRAddPanel.value].text, new CatReactionStructure(newSubReactionName.text==""?null: newSubReactionName.text, 0, (byte)moodChange.value, pathToNewSubReactionIcon.text, SpriteToSerialize.PathToSprite(pathToNewSubReactionIcon.text))));
        UpdateAddReactionPanel();
    }
    public void AddReactionPanelCallButton()
    {
        addReactionPanel.SetActive(!addReactionPanel.activeSelf);
        UploadAddReactionPanel();
        mainManager.Save();
    }
    void UploadAddReactionPanel()
    {
        UpdateARPActionList();
        moodsRAddPanel.value = 0;
        UpdateAddReactionPanel();
    }
    void UpdateARPActionList()
    {
        actionsRAddPanel.ClearOptions();
        actionsRAddPanel.AddOptions(mainManager.humanActionList.Select(a => a.actionName).ToList());
        actionsRAddPanel.value = 0;
        subActionsRAddPanel.ClearOptions();
        subActionsRAddPanel.AddOptions(mainManager.humanActionList.Select(a => a.actionName).ToList());
        subActionsRAddPanel.value = 0;
    }
    void UpdateAddReactionPanel()
    {
        if ((actionsRAddPanel.value < mainManager.catReactionList[moodsRAddPanel.value].Count) && (mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value] != null))
        {
            newReaction.text = mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value].reactionName;
            newReactionTime.text = (mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value].reactionTime/100).ToString();
            moodChange.value = mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value].moodChange;
            UpdateSubOnARPanel();
            pathToNewReactionIcon.text = mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value].pathToIcon;
        }
        else
        {
            ClearAddReactionPanel();
        }
    }
    void UpdateSubOnARPanel()
    {
        int subActionIndex = -1;
        if (mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value].subReactionActions != null)
            subActionIndex = mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value].subReactionActions.FindIndex(s => s == subActionsRAddPanel.options[subActionsRAddPanel.value].text);
        CatReactionStructure curSubReaction = new CatReactionStructure();
        if (subActionIndex != -1)
        {
            curSubReaction = mainManager.catReactionList[moodsRAddPanel.value][actionsRAddPanel.value].subReaction[subActionIndex];
        }
        if (curSubReaction != null)
        {
            newSubReactionName.text = curSubReaction.reactionName;
            pathToNewSubReactionIcon.text = curSubReaction.pathToIcon;
        }
        else
        {
            newSubReactionName.text = ""; 
            pathToNewSubReactionIcon.text = ""; 
        }
    }
    void ClearAddReactionPanel()
    {
        newReaction.text = "";
        newReactionTime.text = "0";
        moodChange.value = 1;
        pathToNewReactionIcon.text = "";
        pathToNewSubReactionIcon.text = "";
}
    public void BrowseReactionIcon()
    {
#if UNITY_EDITOR
        pathToNewReactionIcon.text = EditorUtility.OpenFilePanel("Fetch Reaction Icon", Directory.GetCurrentDirectory(), "png").Replace('/', '\\');
#endif
    }
    public void BrowseSubReactionIcon()
    {
#if UNITY_EDITOR
        pathToNewSubReactionIcon.text = EditorUtility.OpenFilePanel("Fetch Subreaction Icon", Directory.GetCurrentDirectory(), "png").Replace('/', '\\');
#endif
    }
}
