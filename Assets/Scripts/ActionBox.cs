using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBox
{
    int actionFinaleTime = 10;
    int actionTime;
    GameObject actionBoxObject;
    public bool finished { get; private set; } = true;
    Vector2 startPosition;
    Vector2 finishPosition;
    Vector2 moveStep;
    public ActionBox(GameObject actionBox, Vector2 finishPos, int newFinale = 10)
    {
        actionBoxObject = actionBox;
        startPosition = actionBoxObject.transform.position;
        finishPosition = finishPos;
        actionFinaleTime = newFinale;
    }
    public void StartAction(int newActionTime, Sprite newActionSprite)
    {
        actionTime = newActionTime+actionFinaleTime;
        actionBoxObject.GetComponent<Image>().sprite = newActionSprite;
        if(newActionSprite!=null)
            actionBoxObject.SetActive(true);
        finished = false;
        if (newActionTime > 0)
            moveStep = new Vector2(finishPosition.x - startPosition.x, finishPosition.y - startPosition.y) / newActionTime;
        else
            actionBoxObject.transform.position = finishPosition;
    }
    public void UpdateAction()
    {
        if (actionTime <= 0)
        {
            StopAction();
        }
        if(actionTime > actionFinaleTime)
            actionBoxObject.transform.position = new Vector2(actionBoxObject.transform.position.x + moveStep.x, actionBoxObject.transform.position.y + moveStep.y);
        actionTime--;
    }

    public void StopAction()
    {
        actionBoxObject.transform.position = startPosition;
        actionBoxObject.SetActive(false);
        finished = true;
    }
}
