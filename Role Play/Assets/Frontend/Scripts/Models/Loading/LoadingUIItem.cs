using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadingUIItem : MonoBehaviour
{
    public TMP_Text Text;
    public TMP_Text Percent;
    public TMP_Text Status;


    public int PercentCount = 0;
    public void SetText(string text)
    {
        Text.text = $"{text}";
    }
    public void PercentSet(int value)
    {
        PercentCount = value;
        Percent.text = $"|    {PercentCount}%    |";
    }
    public void PercentSet(float value)
    {
        PercentSet(Convert.ToInt32(value));
    }
    public void PercentAdd(int value)
    {
        PercentSet(PercentCount + value);
    }
    public void SetStatus(string status)
    {
        Status.text = $"Status => {status}";
    }
}
