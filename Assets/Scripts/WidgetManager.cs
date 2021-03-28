using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public partial class WidgetManager : MonoBehaviour
{
    public RelationshipManager mainManager;

    public Button addAButton;
    public InputField newAction;
    public InputField newPathToIcon;
    public GameObject addActionPanel;
    public Button browseActionIcon;

    void Start()
    {
        addAButton.onClick.AddListener(TriggerAddAction);
        deleteAButton.onClick.AddListener(TriggerDeleteAction);
        addRButton.onClick.AddListener(TriggerAddReaction);
        deleteRButton.onClick.AddListener(TriggerDeleteReaction);

        actionsList.onEndEdit.AddListener(delegate { caretPosition = actionsList.caretPosition; });
        actionsRAddPanel.onValueChanged.AddListener(delegate { UpdateAddReactionPanel(); });
        moodsRAddPanel.onValueChanged.AddListener(delegate{ UpdateAddReactionPanel(); } );
        subActionsRAddPanel.onValueChanged.AddListener(delegate { UpdateSubOnARPanel(); });
        actionsRDeletePanel.onValueChanged.AddListener(delegate { UpdateReactionDRPanel(); });
        moodsRDeletePanel.onValueChanged.AddListener(delegate { UpdateReactionDRPanel(); });

        browseActionIcon.onClick.AddListener(BrowseActionIcon);
        browseReactionIcon.onClick.AddListener(BrowseReactionIcon);
        subBrowseReactionIcon.onClick.AddListener(BrowseSubReactionIcon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            addActionPanel.gameObject.SetActive(false);
            deleteActionPanel.gameObject.SetActive(false);
            addReactionPanel.gameObject.SetActive(false);
            deleteReactionPanel.gameObject.SetActive(false);
        }
    }
    void TriggerAddAction()
    {
        mainManager.AddAction(new HumanActionStructure { actionName = newAction.text, pathToIcon = newPathToIcon.text, actionIcon = SpriteToSerialize.PathToSTexture(newPathToIcon.text) });
        newAction.text = "";
        newPathToIcon.text = "";
        UpdateDeleteActionPanel();
        UpdateARPActionList();
        mainManager.Save();
    }

    public void AddActionPanelCallButton()
    {
        addActionPanel.SetActive(!addActionPanel.activeSelf);
        newAction.text = "";
        newPathToIcon.text = "";
    }

    public void BrowseActionIcon()
    {
#if UNITY_EDITOR
        newPathToIcon.text = EditorUtility.OpenFilePanel("Fetch Action Icon", Directory.GetCurrentDirectory(), "png").Replace('/', '\\');
#endif
    }

    public void Exit()
    {
        Application.Quit();
    }
}
