using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RButtonController : MonoBehaviour
{
    public static GameObject RButton;
    public static Text RText;

    private void Awake()
    {
        RButton = GameObject.Find("RButton");
        RText = RButton.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Message.sonTextNum == 5)
        {
            Message.SetMessage("");
            RButton.gameObject.SetActive(true);
            SetText("村人から自分の住処を守っていた");
        }

        if (Message.dadTextNum == 7)
        {
            Message.SetMessage("");
            RButton.gameObject.SetActive(true);
            SetText("仲間を集めない");
        }

        if (Message.dadTextNum == 3 || Message.dadTextNum == 4 || Message.dadTextNum == 5)
        {
            Message.SetMessage("");
            RButton.gameObject.SetActive(true);
            SetText("仲間にしない");
        }

        if (Message.dadTextNum == 6)
        {
            Message.SetMessage("");
            RButton.gameObject.SetActive(true);
            SetText("鬼に追い詰められている");
        }
    }

    public void OnClick()
    {
        MyCanvas.TimeSpend();

        if (Message.sonTextNum == 5)
        {
            Akaoni.akaoniHp = 100;
            Akaoni.akaoniAttack = 5;
            Message.sonTextNum = 6;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            Message.dadTextNum = 7;
            Message.SetDadMessage();
            StartCoroutine(SetTrue1());
        }

        if (LButtonController.isQ1End && MyCanvas.days == 1)
        {
            Message.sonTextNum = 0;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            Message.dadTextNum = 0;
            Message.SetDadMessage();
            Akaoni.akaoniImg.enabled = true;
            Message.SetMessage("赤鬼があらわれた！");
            ButtonController.isTButtonEnabled = true;
            WazaController.isWButtonEnabled = true;
            LButtonController.LButton.gameObject.SetActive(false);
            RButton.gameObject.SetActive(false);
        }

        if (Message.dadTextNum == 6)
        {
            int i = Random.Range(1, 11);

            if (i >= 9)
            {
                Message.sonTextNum = 8;
                Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            }
            else if (i <= 2)
            {
                Message.sonTextNum = 10;
                Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
                Akaoni.akaoniHp -= 30;
                MyCanvas.myHp += 20;
                MyCanvas.myMp += 20;
            }
            else
            {
                Message.sonTextNum = 9;
                Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
                MyCanvas.myHp -= 10;
            }

            if (Akaoni.akaoniHp <= 0)
            {
                StartCoroutine("ObjectDestroy");
            }

            Message.SetMessage("赤鬼とたたかっている！");

            Message.dadTextNum = 0;
            Message.SetDadMessage();
            
            LButtonController.LButton.SetActive(false);
            RButtonController.RButton.SetActive(false);

            ButtonController.isTButtonEnabled = true;
            WazaController.isWButtonEnabled = true;

        }
    }

    static void SetText(string text)
    {
        RText.text = text;
    }

    IEnumerator SetTrue1()
    {
        yield return new WaitForSeconds(0.1f);

        LButtonController.isQ1End = true;
    }

    IEnumerator ObjectDestroy()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(Akaoni.akaoniImg);
        Message.SetMessage("赤鬼を倒した！");
        MyCanvas.isDestroyed = true;
    }
}