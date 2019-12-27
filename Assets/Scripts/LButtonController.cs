using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LButtonController : MonoBehaviour
{
    public static GameObject LButton;
    public static Text LText;

    public static bool is_H_Akaoni = false;
    public static bool is_Inu_exist = false;
    public static bool is_Saru_exist = false;
    public static bool is_Kiji_exist = false;

    public static bool isQ1End = false;
    public static bool isQ2End = false;
    public static bool isQ3End = false;
    public static bool isQ4End = false;


    private void Awake()
    {
        LButton = GameObject.Find("LButton");
        LText = LButton.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Message.sonTextNum == 5)
        {
            Message.SetMessage("");
            LButton.gameObject.SetActive(true);
            SetText("村人の財産を奪っていた");
        }

        if(Message.dadTextNum == 7)
        {
            Message.SetMessage("");
            LButton.gameObject.SetActive(true);
            SetText("仲間を集める");
        }

        if(Message.dadTextNum == 3 || Message.dadTextNum == 4 || Message.dadTextNum == 5)
        {
            Message.SetMessage("");
            LButton.gameObject.SetActive(true);
            SetText("仲間にする");
        }

        if(Message.dadTextNum == 6)
        {
            Message.SetMessage("");
            LButton.gameObject.SetActive(true);
            SetText("鬼を追い詰めた");
        }
    }

    public void OnClick()
    {
        MyCanvas.TimeSpend();

        if(Message.sonTextNum == 5)
        {
            Akaoni.akaoniHp = 150;
            Akaoni.akaoniAttack = 3;
            Message.sonTextNum = 6;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            Message.dadTextNum = 7;
            Message.SetDadMessage();
            StartCoroutine(SetTrue1());
        }

        if(isQ1End && MyCanvas.days == 1)
        {
            Message.sonTextNum = 6;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            Message.dadTextNum = 3;
            Message.SetDadMessage();
            StartCoroutine(SetTrue2());
        }

        if(isQ2End && MyCanvas.days == 1)
        {
            is_Inu_exist = true;
            Message.sonTextNum = 6;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            Message.dadTextNum = 4;
            Message.SetDadMessage();
            StartCoroutine(SetTrue3());
        }

        if (isQ3End && MyCanvas.days == 1)
        {
            is_Saru_exist = true;
            Message.sonTextNum = 6;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            Message.dadTextNum = 5;
            Message.SetDadMessage();
            StartCoroutine(SetTrue4());
        }

        if (isQ4End && MyCanvas.days == 1)
        {
            is_Kiji_exist = true;
            Message.sonTextNum = 0;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
            Message.dadTextNum = 0;
            Message.SetDadMessage();
            Akaoni.akaoniImg.enabled = true;
            Message.SetMessage("赤鬼があらわれた！");
            ButtonController.isTButtonEnabled = true;
            WazaController.isWButtonEnabled = true;
            LButton.gameObject.SetActive(false);
            RButtonController.RButton.gameObject.SetActive(false);
        }

        if(Message.dadTextNum == 6)
        {
            int i = Random.Range(1, 11);

            if(i >= 9)
            {
                Message.sonTextNum = 8;
                Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
                MyCanvas.myHp += 5;
                MyCanvas.myMp += 5;
            }
            else if(i <= 2)
            {
                Message.sonTextNum = 10;
                Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
                Akaoni.akaoniHp += 30;
            }
            else
            {
                Message.sonTextNum = 9;
                Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
                Akaoni.akaoniHp -= 10;
                MyCanvas.myHp += 10;
                MyCanvas.myMp += 10;
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
        LText.text = text;
    }

    IEnumerator SetTrue1()
    {
        yield return new WaitForSeconds(0.1f);

        isQ1End = true;
    }

    IEnumerator SetTrue2()
    {
        yield return new WaitForSeconds(0.1f);

        isQ2End = true;
    }

    IEnumerator SetTrue3()
    {
        yield return new WaitForSeconds(0.1f);

        isQ3End = true;
    }

    IEnumerator SetTrue4()
    {
        yield return new WaitForSeconds(0.1f);

        isQ4End = true;
    }

    IEnumerator ObjectDestroy()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(Akaoni.akaoniImg);
        Message.SetMessage("赤鬼を倒した！");
        MyCanvas.isDestroyed = true;
    }
}
