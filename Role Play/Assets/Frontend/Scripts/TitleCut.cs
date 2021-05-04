using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCut : MonoBehaviour
{
    public Image UI_Backgroun;
    public Text UI_Message;
    public Color UI_Backgroun_Color;
    public Color UI_Message_Color;
    public float second;
    public float waitsecond;

    public AnimationCurve curve;

    public void Show(string message, Color backgroundcolor, Color messageColor)
    {
        UI_Message.text = message;

        UI_Message.text = message;

        UI_Backgroun_Color = backgroundcolor;
        UI_Message_Color = messageColor;

        UI_Backgroun.color = new Color(UI_Backgroun_Color.r, UI_Backgroun_Color.g, UI_Backgroun_Color.b, 1);
        //UI_Backgroun.color = new Color(UI_Backgroun_Color.r, UI_Backgroun_Color.g, UI_Backgroun_Color.b, 0.008f);
        UI_Message.color = new Color(UI_Message_Color.r, UI_Message_Color.g, UI_Message_Color.b, 1);
        //UI_Message.color = new Color(UI_Message_Color.r, UI_Message_Color.g, UI_Message_Color.b, 0.008f);

        UI_Backgroun.enabled = true;
        UI_Message.enabled = true;

        StartCoroutine(FadeIn());
    }
    /*public void Show(string message, Color backgroundcolor, Color messageColor, float second)
    {
        _second = second;

        UI_Message.text = message;

        UI_Backgroun_Color = backgroundcolor;
        UI_Message_Color = messageColor;

        UI_Backgroun.color = new Color(UI_Backgroun_Color.r, UI_Backgroun_Color.g, UI_Backgroun_Color.b, 0.1f);
        UI_Message.color = new Color(UI_Message_Color.r, UI_Message_Color.g, UI_Message_Color.b, 0.1f);

        UI_Backgroun.enabled = true;
        UI_Message.enabled = true;

        StartCoroutine(FadeIn());
    }*/


    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(waitsecond); // продолжить примерно через 100ms
        UI_Backgroun_Color = UI_Backgroun.color;
        UI_Message_Color = UI_Message.color;
        if (UI_Backgroun.IsActive())
        {
            while (UI_Backgroun_Color.a != 0f)
            {
                //-= 0.3f * Time.deltaTime;
                UI_Backgroun_Color.a -= second * Time.deltaTime;
                UI_Message_Color.a -= second * Time.deltaTime;
                UI_Backgroun.color = UI_Backgroun_Color;
                UI_Message.color = UI_Message_Color;
                yield return null;
            }
            if (UI_Backgroun_Color.a <= 1f) { UI_Backgroun.enabled = false; UI_Message.enabled = false;  };
            //Destroy(gameObject);
        }
    }
}
