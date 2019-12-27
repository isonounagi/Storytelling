using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MankinController : MonoBehaviour
{
    public static bool isMankinUsedFlag = false;
    public static bool isSuddenEnd_M = false;

    public void OnClick()
    {
        ButtonController.akaoni = GameObject.Find("Akaoni");
        ButtonController.akaoniImg = ButtonController.akaoni.GetComponent<Image>();

        ButtonController.isEnemyMessage = true;

        ButtonController.isTatakauUsedFlag = false;
        ButtonController.isSuddenEnd_T = false;

        KatonController.isKatonUsedFlag = false;
        KatonController.isSuddenEnd_K = false;

        if (isSuddenEnd_M)
        {
            Message.dadTextNum = 1;
            Message.SetDadMessage();
            Message.sonTextNum = 4;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);

            MyCanvas.SetInteractive("たたかう", false);
            MyCanvas.SetInteractive("わざ", false);

            Invoke("SuddenEnd", 3.0f);
        }
        else
        {

            MyCanvas.myMp -= 3;

            if (Akaoni.akaoniHp > 0)
            {
                MyCanvas.myHp += 8;

                WazaController.mankin.SetActive(false);
                Message.messageText.text = "桃太郎が　" + "8" + "　回復した！";
            }

            MyCanvas.SetInteractive("たたかう", false);
            MyCanvas.SetInteractive("わざ", false);


            Invoke("ButtonActive", 2.0f);


            if (ButtonController.isEnemyMessage == true && Akaoni.akaoniHp > 0)
            {
                WazaController.katon.SetActive(false);
                Invoke("EnemyMessage", 1.5f);
            }

            Message.isOneMessage = false;

            Invoke("SetSonMessage", 2.0f);

            if (isMankinUsedFlag == false)
            {
                Invoke("TrueFlag", 2.01f);
            }
            else if (isMankinUsedFlag == true)
            {
                Invoke("FalseFlag", 2.01f);
            }

            MyCanvas.TimeSpend();
        }
    }

    void EnemyMessage()
    {
        ButtonController.isEnemyMessage = false;
        MyCanvas.onMyDamage = true;
        MyCanvas.myHp -= Akaoni.akaoniAttack;
        Message.SetMessage("赤鬼の攻撃！" + Akaoni.akaoniAttack + "のダメージをくらった");
    }

    void SetSonMessage()
    {

        if (Akaoni.akaoniHp > 0)
        {
            if (isMankinUsedFlag && MyCanvas.isSetTomorrowMessage == false)
            {
                Message.sonTextNum = 3;
                isSuddenEnd_M = true;
            }
            else if (MyCanvas.isSetTomorrowMessage)
            {
                Message.sonTextNum = 7;
                MyCanvas.isSetTomorrowMessage = false;
            }
            else
            {
                Message.sonTextNum = Random.Range(0, 3);
            }

            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);
        }

    }

    void TrueFlag()
    {
        isMankinUsedFlag = true;
    }

    void FalseFlag()
    {
        isMankinUsedFlag = false;
    }

    void ButtonActive()
    {
        MyCanvas.SetInteractive("たたかう", true);
        MyCanvas.SetInteractive("わざ", true);
    }

    void SuddenEnd()
    {
        MyCanvas.NewDayStart();
    }
}
