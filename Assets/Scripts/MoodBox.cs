using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBox
{
    List<Sprite> moods = new List<Sprite>();
    Sprite negativeChange;
    Sprite positiveChange;
    GameObject moodBoxObject;
    int upcomingMood=1;
    public int currentCatMood=1;
    int actionTime;
    public bool finished { get; private set; } = true;
    public MoodBox(GameObject newMoodObject, Sprite newPositiveChange, Sprite newNegativeChange, params Sprite[] newMoods)
    {
        moodBoxObject = newMoodObject;
        positiveChange = newPositiveChange;
        negativeChange = newNegativeChange;
        for (int i = 0; i < newMoods.Length; i++)
            moods.Add(newMoods[i]);
    }
    public void StartAdjustMood(int newMood)
    {
        if (newMood != (int)CatMoods.Неизменное)
        {
            if ((currentCatMood > newMood) || (currentCatMood == 0) && (currentCatMood == newMood))
            {
                StartNegativeChange();
            }
            if ((currentCatMood < newMood) || (currentCatMood == 2) && (currentCatMood == newMood))
            {
                StartPositiveChange();
            }
            upcomingMood = newMood;
            finished = false;
        }
    }

    void StartNegativeChange()
    {
        moodBoxObject.GetComponent<Image>().sprite = negativeChange;
        actionTime = 20;
        finished = false;
    }
    void StartPositiveChange()
    {
        moodBoxObject.GetComponent<Image>().sprite = positiveChange;
        actionTime = 20;
        finished = false;
    }

    public void UpdateAction()
    {
        if (actionTime == 0)
        {
            moodBoxObject.GetComponent<Image>().sprite = moods[currentCatMood];
            finished = true;
        }
        else
        actionTime--;
    }
    public void UpdateMood()
    {
        currentCatMood = upcomingMood;
        moodBoxObject.GetComponent<Image>().sprite = moods[currentCatMood];
    }
}
