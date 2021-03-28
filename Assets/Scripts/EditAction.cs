using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class WidgetManager : MonoBehaviour
{
    public Button editAButton;
    public Dropdown editAction;
    public InputField editActionName;
    public InputField editPathToIcon;
    public GameObject editActionPanel;
    public Button browseEditActionIcon;
    void TriggerEditAction()
    {
        mainManager.EditAction(editAction.value, new HumanActionStructure { actionName = editActionName.text, pathToIcon = editPathToIcon.text, actionIcon = SpriteToSerialize.PathToSTexture(editPathToIcon.text) });
        editAction.options[editAction.value].text = editActionName.text;
        UpdateDeleteActionPanel();
        UpdateARPActionList();
        mainManager.Save();
    }

    public void EditActionPanelCallButton()
    {
        editActionPanel.SetActive(!editActionPanel.activeSelf);
        UploadEditActionPanel();
    }

    public void UploadEditActionPanel()
    {
        editAction.ClearOptions();
        editAction.AddOptions(mainManager.humanActionList.Select(a => a.actionName).ToList());
        editAction.value = 0;
        EditEditName();
        editPathToIcon.text = mainManager.humanActionList[0].pathToIcon;
    }
    public void EditEditName()
    {
        editActionName.text = editAction.options[editAction.value].text;
    }

    public void BrowseEditActionIcon()
    {
#if UNITY_EDITOR
        editPathToIcon.text = EditorUtility.OpenFilePanel("Fetch Action Icon", Directory.GetCurrentDirectory(), "png").Replace('/', '\\');
#endif
    }
}
