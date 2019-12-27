using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MyCanvas : MonoBehaviour
{
    public static int myHp = 40;
    public static int myMp = 25;

    public static int hour = 9;
    public static int minutes1 = 0;
    public static int minutes2 = 0;

    static Canvas canvas;
    GameObject Hp;
    GameObject Mp;
    GameObject myUI;
    public static GameObject KeikaTime;
    public static GameObject Day;
    public static int days = 1;
    Image myUIImg;
    public static bool onMyDamage = false;

    public static bool isSetTomorrowMessage = false;

    public static bool isDestroyed = false;

    void Start()
    {
        // Canvasコンポーネントを保持
        canvas = GetComponent<Canvas>();
        myUI = GameObject.Find("Player");
        myUIImg = myUI.GetComponent<Image>();
        KeikaTime = GameObject.Find("Time");
        Day = GameObject.Find("Day");
    }

    private void Update()
    {
        this.Hp = GameObject.Find("HP_int");
        Hp.GetComponent<Text>().text = myHp.ToString();

        this.Mp = GameObject.Find("MP_int");
        Mp.GetComponent<Text>().text = myMp.ToString();

        TimeControl(hour, minutes1, minutes2);

        Day.GetComponent<Text>().text = days + " 日目";

        if (minutes1 == 5 && minutes2 == 5)
        {
            isSetTomorrowMessage = true;
        }
         

        if (onMyDamage)
        {
            OnDamageEffect();
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            myUIImg.color = new Color(1f, 1f, 1f, level);
        }

        if(isDestroyed)
        {
            StartCoroutine("ToResult");
        }

        if(myHp <= 0)
        {
            SetInteractive("たたかう", false);
            SetInteractive("わざ", false);

            StartCoroutine("ToResult");
        }
        
    }

    void OnDamageEffect()
    {
        onMyDamage = true;
        StartCoroutine("WaitForIt");
    }

    IEnumerator WaitForIt()
    {
        // 0.5秒間処理を止める
        yield return new WaitForSeconds(0.5f);

        // 0.5秒後ダメージフラグをfalseにして点滅を戻す
        onMyDamage = false;
        myUIImg.color = new Color(1f, 1f, 1f, 1f);
    }

    /// ボタンの有効・無効を設定する
    public static void SetInteractive(string name, bool b)
    {
        foreach (Transform child in canvas.transform)
        {
            // 子の要素をたどる
            if (child.name == "Command")
            {
                foreach(Transform child2 in child.transform)
                {
                    if(child2.name == name)
                    {
                        // Buttonコンポーネントを取得する
                        Button btn = child2.GetComponent<Button>();
                        // 有効・無効フラグを設定
                        btn.interactable = b;
                        // おしまい
                        return;
                    }
                }
                
            }
        }
        // 指定したオブジェクト名が見つからなかった
        Debug.LogWarning("Not found objname:" + name);
    }
    
    public static void TimeControl(int hour, int minutes1, int minutes2)
    {
        KeikaTime.GetComponent<Text>().text = "PM " + hour + " : " + minutes1 + minutes2;
    }

    public static void TimeSpend()
    {
        minutes2 += 5;

        if(minutes2 == 10)
        {
            minutes1 += 1;
            minutes2 = 0;

            if (minutes1 == 6)
            {
                hour += 1;
                minutes1 = 0;

                if(hour == 10 && Akaoni.akaoniImg.enabled)
                {
                    NewDayStart();
                }
            }
        }
        
    }

    public static void NewDayStart()
    {
        days += 1;

        Message.dadTextNum = 6;
        Message.SetDadMessage();

        Message.sonTextNum = 7;
        Message.SetSonMessage(Message.sonText[Message.sonTextNum]);

        LButtonController.LButton.SetActive(true);
        RButtonController.RButton.SetActive(true);

        WazaController.katon.gameObject.SetActive(false);
        WazaController.mankin.gameObject.SetActive(false);

        ButtonController.isTButtonEnabled = false;
        WazaController.isWButtonEnabled = false;

        hour = 9;
        minutes1 = 0;
        minutes2 = 0;
        TimeControl(hour, minutes1, minutes2);

        KatonController.isSuddenEnd_K = false;
        KatonController.isKatonUsedFlag = false;
        ButtonController.isTatakauUsedFlag = false;
        ButtonController.isSuddenEnd_T = false;
    }

    IEnumerator ToResult()
    {
        yield return new WaitForSeconds(1.3f);

        SceneManager.LoadScene("Result");
    }
}
