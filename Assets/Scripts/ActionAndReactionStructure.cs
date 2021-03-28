using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CatReactionStructure
{
    public string reactionName;
    public int reactionTime;
    public byte moodChange;
    public string pathToIcon;
    public SerializeTexture reactionIcon;
    public List<string> subReactionActions = new List<string>();
    public List<CatReactionStructure> subReaction = new List<CatReactionStructure>();
    public CatReactionStructure() { }
    public CatReactionStructure(string newReactionName, int newReactionTime, byte newMood, string newPathToIcon, Sprite newReactionIcon)
    {
        reactionName = newReactionName;
        reactionTime = newReactionTime;
        moodChange = newMood;
        pathToIcon = newPathToIcon;
        reactionIcon = SpriteToSerialize.SpriteToSTexture(newReactionIcon);
    }
    public CatReactionStructure(string newReactionName, int newReactionTime, byte newMood, string newPathToIcon, Sprite newReactionIcon, string newSubReactionAction, CatReactionStructure newSubReaction)
    {
        reactionName = newReactionName;
        reactionTime = newReactionTime;
        moodChange = newMood;
        pathToIcon = newPathToIcon;
        reactionIcon = SpriteToSerialize.SpriteToSTexture(newReactionIcon);
        int indexForSub = subReactionActions.FindIndex(s=>s == newSubReactionAction);
        if (indexForSub > -1)
            subReaction[indexForSub] = newSubReaction;
        else
        {
            subReactionActions.Add(newSubReactionAction);
            subReaction.Add(newSubReaction); 
        }
    }
}
[Serializable]
public struct HumanActionStructure
{
    public string actionName;
    public string pathToIcon;
    public SerializeTexture actionIcon;
}
