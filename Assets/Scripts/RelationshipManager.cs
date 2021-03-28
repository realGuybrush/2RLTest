using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public enum CatMoods { Плохое, Хорошее, Отличное, Неизменное };
public class RelationshipManager : MonoBehaviour
{
    readonly SaveLoad inputData = new SaveLoad();
    const int humanActionTime = 50;

    public List<HumanActionStructure> humanActionList = new List<HumanActionStructure>();
    public List<List<CatReactionStructure>> catReactionList = new List<List<CatReactionStructure>>();
    int previousHumanActionIndex = -1;
    int humanActionIndex = -1;

    ActionBox humanActionBox, catReactionBox, catSubReactionBox;
    MoodBox catMoodBox;

    public GameObject humanActionPic, catReactionPic, catSubReactionPic, catFacePic;
    public List<Sprite> moodSprites;
    public Text catReactionText;
    public ButtonScrollBox scrollBox;
    public WidgetManager widgets;
    void Start()
    {
        inputData.Load(out humanActionList, out catReactionList);
        if(humanActionList.Count>0)
            scrollBox.SetButtonNames(humanActionList.Select(t=> t.actionName).ToArray());
        humanActionBox = new ActionBox(humanActionPic, catReactionPic.transform.position);
        catReactionBox = new ActionBox(catReactionPic, humanActionPic.transform.position,100);
        catSubReactionBox = new ActionBox(catSubReactionPic, humanActionPic.transform.position);
        catMoodBox = new MoodBox(catFacePic, moodSprites[0], moodSprites[1], moodSprites[2], moodSprites[3], moodSprites[4]);
    }

    void Update()
    {
        if (!humanActionBox.finished)
        {
            humanActionBox.UpdateAction();
            if (humanActionBox.finished && humanActionIndex <= catReactionList[catMoodBox.currentCatMood].Count)
            {
                if (catReactionList[catMoodBox.currentCatMood][humanActionIndex].reactionName != null)
                {
                    if (catReactionBox.finished || (catReactionList[catMoodBox.currentCatMood][humanActionIndex].moodChange == (byte)CatMoods.Плохое))
                    {
                        catReactionBox.StartAction(catReactionList[catMoodBox.currentCatMood][humanActionIndex].reactionTime, SpriteToSerialize.STextureToSprite(catReactionList[catMoodBox.currentCatMood][humanActionIndex].reactionIcon));
                        catMoodBox.StartAdjustMood(catReactionList[catMoodBox.currentCatMood][humanActionIndex].moodChange);
                        catReactionText.text = catReactionList[catMoodBox.currentCatMood][humanActionIndex].reactionName;
                    }
                    else
                    {
                        int subIndex = catReactionList[catMoodBox.currentCatMood][previousHumanActionIndex].subReactionActions.FindIndex(s => s == humanActionList[humanActionIndex].actionName);
                        if (subIndex != -1)
                        {
                            catSubReactionBox.StartAction(0, SpriteToSerialize.STextureToSprite(catReactionList[catMoodBox.currentCatMood][previousHumanActionIndex].subReaction[subIndex].reactionIcon));
                            catReactionText.text = catReactionList[catMoodBox.currentCatMood][previousHumanActionIndex].subReaction[subIndex].reactionName;
                        }
                    }
                }
            }
        }
        if (!catReactionBox.finished)
        {
            catReactionBox.UpdateAction();
            if (catReactionBox.finished)
            { 
                catMoodBox.UpdateMood();
                catReactionText.text = "";
                previousHumanActionIndex = humanActionIndex;
            }
        }
        if (!catSubReactionBox.finished)
        {
            catSubReactionBox.UpdateAction();
            if (catSubReactionBox.finished)
            {
                if (catReactionBox.finished)
                    catReactionText.text = "";
                else
                    catReactionText.text = catReactionList[catMoodBox.currentCatMood][previousHumanActionIndex].reactionName;
            }
        }
        if (!catMoodBox.finished)
            catMoodBox.UpdateAction();
    }
    public void HumanAct(int actionIndex)
    {
        if (humanActionBox.finished)
        {
            previousHumanActionIndex = humanActionIndex;
            humanActionIndex = actionIndex;
            if(humanActionIndex<humanActionList.Count)
                humanActionBox.StartAction(humanActionTime, SpriteToSerialize.STextureToSprite(humanActionList[humanActionIndex].actionIcon));
        }
    }
    public void AddAction(HumanActionStructure newAction)
    {
        humanActionList.Add(newAction);
        foreach (List<CatReactionStructure> list in catReactionList)
            list.Add(new CatReactionStructure());
        scrollBox.SetButtonNames(humanActionList.Select(a => a.actionName).ToArray());
        scrollBox.AddButton();
        widgets.UpdateDeleteActionPanel();
    }
    public void EditAction(int actionIndex, HumanActionStructure newAction)
    {
        humanActionList[actionIndex] = newAction;
        foreach (List<CatReactionStructure> list in catReactionList)
            foreach (CatReactionStructure item in list)
                if ((item.subReactionActions!=null)&&(item.subReactionActions.Contains(humanActionList[actionIndex].actionName)))
                {
                    item.subReactionActions[item.subReactionActions.IndexOf(humanActionList[actionIndex].actionName)] = humanActionList[actionIndex].actionName;
                }
        scrollBox.SetButtonNames(humanActionList.Select(a => a.actionName).ToArray());
    }

    public void DeleteAction(int index)
    {
        if (index < humanActionList.Count)
        {
            humanActionList.RemoveAt(index);
            foreach (List<CatReactionStructure> list in catReactionList)
                list.RemoveAt(index);
            scrollBox.SetButtonNames(humanActionList.Select(a=>a.actionName).ToArray());
            scrollBox.RemoveButton();
        }
    }
    public void AddReaction(byte catMood, int triggerAction, CatReactionStructure newReaction)
    {
        if (triggerAction < catReactionList[catMood].Count)
        {
            if (catReactionList[catMood][triggerAction].reactionName != null)
            {
                if(catReactionList[catMood][triggerAction].reactionIcon!=null)
                    newReaction.reactionIcon = catReactionList[catMood][triggerAction].reactionIcon;
            }
            catReactionList[catMood][triggerAction] = newReaction;
        }
    }

    public void DeleteReaction(byte catMood, int triggerAction)
    {
        if (triggerAction < catReactionList[catMood].Count)
        {
            catReactionList[catMood][triggerAction]=null;
        }
    }
    public void Save()
    {
        inputData.Save(humanActionList, catReactionList);
    }
}
