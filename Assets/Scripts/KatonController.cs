using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KatonController : MonoBehaviour
{
    public static bool isKatonUsedFlag = false;
    public static bool isSuddenEnd_K = false;

    public void OnClick()
    {
        ButtonController.akaoni = GameObject.Find("Akaoni");
        ButtonController.akaoniImg = ButtonController.akaoni.GetComponent<Image>();

        ButtonController.isEnemyMessage = true;

        ButtonController.isTatakauUsedFlag = false;
        ButtonController.isSuddenEnd_T = false;

        MankinController.isMankinUsedFlag = false;
        MankinController.isSuddenEnd_M = false;

        if (isSuddenEnd_K)
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
            
            MyCanvas.myMp -= 5;

            if (Akaoni.akaoniHp > 0)
            {
                if (Message.sonTextNum == 1)
                {
                    ButtonController.akaonidamage = 15;
                }
                else if (Message.sonTextNum == 2)
                {
                    ButtonController.akaonidamage = 12;
                }
                else
                {
                    ButtonController.akaonidamage = 10;
                }

                Akaoni.akaoniHp -= ButtonController.akaonidamage;
                WazaController.katon.SetActive(false);
                Message.messageText.text = "桃太郎の攻撃！" + ButtonController.akaonidamage + "のダメージを与えた";

                if (Akaoni.akaoniHp <= 0)
                {
                    Invoke("ObjectDestroy", 0.5f);
                }
            }

            MyCanvas.SetInteractive("たたかう", false);
            MyCanvas.SetInteractive("わざ", false);

           
            Invoke("ButtonActive", 2.0f);
            
            

            OnDamageEffect();

            if (ButtonController.isEnemyMessage == true && Akaoni.akaoniHp > 0)
            {
                WazaController.katon.SetActive(false);
                WazaController.mankin.SetActive(false);
                Invoke("EnemyMessage", 1.5f);
            }
            
            Message.isOneMessage = false;

            Invoke("SetSonMessage", 2.0f);

            if (isKatonUsedFlag == false)
            {
                Invoke("TrueFlag", 2.01f);
            }
            else if (isKatonUsedFlag == true)
            {
                Invoke("FalseFlag", 2.01f);
            }

            MyCanvas.TimeSpend();
        }
    }

    void OnDamageEffect()
    {
        Akaoni.onDamage = true;
        Invoke("WaitForIt", 0.5f);
    }

    void WaitForIt()
    {
        // 0.5秒後ダメージフラグをfalseにして点滅を戻す
        Akaoni.onDamage = false;
        ButtonController.akaoniImg.color = new Color(1f, 1f, 1f, 1f);
    }

    void EnemyMessage()
    {
        ButtonController.isEnemyMessage = false;
        MyCanvas.onMyDamage = true;
        MyCanvas.myHp -= Akaoni.akaoniAttack;
        Message.SetMessage("赤鬼の攻撃！" + Akaoni.akaoniAttack + "のダメージをくらった");
    }
    

    void ObjectDestroy()
    {
        Destroy(ButtonController.akaoniImg);
        Message.SetMessage("赤鬼を倒した！");
        MyCanvas.isDestroyed = true;
    }

    void SetSonMessage()
    {
        
            if (Akaoni.akaoniHp > 0)
            {
                if (isKatonUsedFlag && MyCanvas.isSetTomorrowMessage == false)
                {
                    Message.sonTextNum = 3;
                    isSuddenEnd_K = true;
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
        isKatonUsedFlag = true;
    }

    void FalseFlag()
    {
        isKatonUsedFlag = false;
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
