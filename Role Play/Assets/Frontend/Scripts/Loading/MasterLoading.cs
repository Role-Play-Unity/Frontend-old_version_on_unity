using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MasterLoading : MonoBehaviour
{
    public TMP_Text UI_Version;
    public TMP_Text UI_All_Loading_Percent;
    public GameObject ModelItemLoadingUI;
    public List<LoadingUIItem> UIItems = new List<LoadingUIItem>();
    public Transform ContentLoadingUI;
    public TitleCut titleCut;
    public Status status;
    private float _allLoadPercent = 0;
    public enum Status
    {
        LOADING, LOADING_GOOD, LOADING_FAILED, //MAIN LOADING

        CONNECTION, CONNECTED, CONNECTED_FAILED, //SERVER STATUS
        LOADIN_DATA, LOADING_DATA_DONE, LOADING_DATA_FAILED,  //SERVER (PHP LARAVEL)
        DONE, DONE_FAILED //LOADING e.t.c
    }
    // Start is called before the first frame update
    void Start()
    {
        UI_Version.text = $"LOADING UI Version => V{DATA.VERSION.MASTER_LOADING_UI}   |   Game Version => V{DATA.VERSION.MASTER_GAME_CLIENT}";
        UI_All_Loading_Percent.text = $"All loading percent => 0%";

        StartCoroutine(LoadingUIload());
    }

    private void SetAllLoadPercent(float oldpercent) {
        float percent = oldpercent;
        if (_allLoadPercent < oldpercent) percent = _allLoadPercent - oldpercent;
        else percent = oldpercent - _allLoadPercent;

        _allLoadPercent += percent;
        Debug.Log($"Percent -> {percent} / All Load Percent -> {_allLoadPercent} / Old Percent -> {oldpercent}");
        string percent_text = "";
        try { percent_text = Convert.ToInt64(_allLoadPercent).ToString(); } catch { }
        UI_All_Loading_Percent.text = $"All loading percent => {percent_text}%";
    }

    private void AddAllLoadPercent(float percent) {
        _allLoadPercent += percent;
        string percent_text = "";
        try { percent_text = Convert.ToInt64(_allLoadPercent).ToString(); } catch { }
        UI_All_Loading_Percent.text = $"All loading percent => {percent_text}%";
    }
    

    public string GateStringStatus(Status status)
    {
        string text="";
        switch(status)
        {
            case Status.LOADING: text = "LOADING"; break;
            case Status.LOADING_GOOD: text = "LOADING GOOD"; break;
            case Status.LOADING_FAILED: text = "LOADING FAILED"; break;

            case Status.CONNECTION: text = "CONNECTION"; break;
            case Status.CONNECTED: text = "CONNECTED"; break;
            case Status.CONNECTED_FAILED: text = "CONNECTED FAILED"; break;

            case Status.LOADIN_DATA: text = "LOADIN DATA"; break;
            case Status.LOADING_DATA_DONE: text = "LOADING DATA DONE"; break;
            case Status.LOADING_DATA_FAILED: text = "LOADING DATA FAILED"; break;

            case Status.DONE: text = "DONE"; break;
            case Status.DONE_FAILED: text = "DONE FAILED"; break;
        }
        return text;
    }


    IEnumerator<Status> LoadingUIload()
    {
        float percent = 0;
        titleCut.Show("LOADING -> LOADING UI", Color.black, Color.white);
        var item = Instantiate(ModelItemLoadingUI, ContentLoadingUI).GetComponent<LoadingUIItem>();
        item.SetText("LOADING UI");
        //item.PercentAdd(100);
        //item.SetStatus(GateStringStatus(Status.DONE));
        UIItems.Add(item);

        while (percent < 100f)
        {
            percent += 16f * Time.deltaTime;
            item.PercentSet(percent);
            item.SetStatus(GateStringStatus(Status.LOADING));
            AddAllLoadPercent(percent / 5000f);
            yield return Status.LOADING;
        }
        if (percent > 99.8f)
        {
            item.PercentSet(100);
            item.SetStatus(GateStringStatus(Status.DONE));
        }
        StartCoroutine(LoadingData());
        yield return Status.DONE;
    }



    IEnumerator<Status> LoadingData()
    {
        float percent = 0;
        bool is_start_loading_data = false; //говно код
        var item = Instantiate(ModelItemLoadingUI, ContentLoadingUI).GetComponent<LoadingUIItem>();
        item.SetText("LOADING DATA");
        UIItems.Add(item);

        while (percent < 100f)
        {
            percent += 10f * Time.deltaTime;
            item.PercentSet(percent);
            item.SetStatus(GateStringStatus(Status.LOADING));
           // if (!is_start_loading_data)

            AddAllLoadPercent(percent / 5000f);
            yield return Status.CONNECTION;
        }


        if (percent > 99.8f)
        {
            item.PercentSet(100);
            item.SetStatus(GateStringStatus(Status.LOADING_DATA_DONE));
            StartCoroutine(LoadingMainServer());
            yield return Status.LOADING_DATA_DONE;
        }
        else
        {
            item.PercentSet(100);
            item.SetStatus(GateStringStatus(Status.LOADING_DATA_FAILED));
            StartCoroutine(LoadingMainServer());
            yield return Status.LOADING_DATA_FAILED;
        }
    }

    IEnumerator<Status> LoadingMainServer()
    {
        float percent = 0;
        bool isStartConnection = false; //говно код
        var item = Instantiate(ModelItemLoadingUI, ContentLoadingUI).GetComponent<LoadingUIItem>();
        item.SetText("MASTER SERVER");
        UIItems.Add(item);
        var mstclient = GameObject.Find("--CONNECTION_TO_MASTER").GetComponent<MasterServerToolkit.MasterServer.ClientToMasterConnector>();

        while (percent < 100f)
        {
            percent += 16f * Time.deltaTime;
            item.PercentSet(percent);
            item.SetStatus(GateStringStatus(Status.CONNECTION));
            if (!isStartConnection) { mstclient.StartConnection(); isStartConnection = true; }

            AddAllLoadPercent(percent / 5000f);
            yield return Status.CONNECTION;
        }


        if (mstclient.Connection.IsConnected)
        {
            item.PercentSet(100);
            item.SetStatus(GateStringStatus(Status.CONNECTED));
            StartCoroutine(LoadingGeneralMenu());
            yield return Status.CONNECTED;
        }
        else
        {
            item.PercentSet(100);
            item.SetStatus(GateStringStatus(Status.CONNECTED_FAILED));
            StartCoroutine(LoadingGeneralMenu());
            yield return Status.CONNECTED_FAILED;
        }
    }
    private AsyncOperation asyncLoadingGeneralMenu;
    IEnumerator<Status> LoadingGeneralMenu()
    { 
        float percent = 0;
        var item = Instantiate(ModelItemLoadingUI, ContentLoadingUI).GetComponent<LoadingUIItem>();
        item.SetText("LOADING GENERAL MENU");
        UIItems.Add(item);

        
        asyncLoadingGeneralMenu = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("General Menu");
        asyncLoadingGeneralMenu.allowSceneActivation = true;

        while (percent < 100f)
        {
            percent += asyncLoadingGeneralMenu.progress * 100;
            item.PercentSet(percent);
            item.SetStatus(GateStringStatus(Status.LOADING));
            // if (!is_start_loading_general_menu)

            AddAllLoadPercent(percent / 5000f);
            yield return Status.LOADING;
        }
        
        if (percent > 99.8f)
        {
            item.PercentSet(100);
            item.SetStatus(GateStringStatus(Status.DONE));
            yield return Status.DONE;
        }
    }
}
