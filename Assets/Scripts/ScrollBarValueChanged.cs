using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBarValueChanged : MonoBehaviour
{
    public ButtonScrollBox buttonScroll;
    public void OnValueChanged()
    {
        buttonScroll.UpdateButtonNames();
    }
}
