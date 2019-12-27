using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public static GameObject akaoni;
    public static Image akaoniImg;

    public static int akaonidamage;

    public static bool isTatakauUsedFlag = false; //一度使用したコマンドを～Flagとして管理して使ったかどうかを保持する？
    public static bool isSuddenEnd_T = false;

    public static bool isTButtonEnabled = false;

    public static bool isEnemyMessage = false;

    private void Update()
    {
        if (isTButtonEnabled)
        {
            MyCanvas.SetInteractive("たたかう", true);
        }
        else
        {
            MyCanvas.SetInteractive("たたかう", false);
        }
    }

    public void OnClick()
    {
        akaoni = GameObject.Find("Akaoni");
        akaoniImg = akaoni.GetComponent<Image>();

        Message.messageNum++;

        isEnemyMessage = true;

        KatonController.isSuddenEnd_K = false;
        KatonController.isKatonUsedFlag = false;
        MankinController.isMankinUsedFlag = false;
        MankinController.isSuddenEnd_M = false;

        if (isSuddenEnd_T)
        {
            Message.dadTextNum = 1;
            Message.SetDadMessage();
            Message.sonTextNum = 4;
            Message.SetSonMessage(Message.sonText[Message.sonTextNum]);

            MyCanvas.SetInteractive("たたかう", false);
            MyCanvas.SetInteractive("わざ", false);

            StartCoroutine("SuddenEnd");
        }
        else
        {
            StartCoroutine("SetSonMessage");
         
            if (isTatakauUsedFlag == false)
            {
                StartCoroutine("TrueFlag");
            }
            else if (isTatakauUsedFlag == true)
            {
                StartCoroutine("FalseFlag");
            }

            if (Akaoni.akaoniHp > 0)
            {

                if (Message.sonTextNum == 2)
                {
                    akaonidamage = 8;

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == false && LButtonController.is_Kiji_exist == false)
                    {
                        akaonidamage = 9;
                    }

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == false)
                    {
                        akaonidamage = 12;
                    }

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == false && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 10;
                    }

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 13;
                    }

                    if (LButtonController.is_Inu_exist == false && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == false)
                    {
                        akaonidamage = 10;
                    }

                    if (LButtonController.is_Inu_exist == false && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 10;
                    }

                    if (LButtonController.is_Inu_exist == false && LButtonController.is_Saru_exist == false && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 8;
                    }
                }
                else
                {
                    akaonidamage = 5;

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == false && LButtonController.is_Kiji_exist == false)
                    {
                        akaonidamage = 6;
                    }

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == false)
                    {
                        akaonidamage = 9;
                    }

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == false && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 7;
                    }

                    if (LButtonController.is_Inu_exist == true && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 10;
                    }

                    if (LButtonController.is_Inu_exist == false && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == false)
                    {
                        akaonidamage = 7;
                    }

                    if (LButtonController.is_Inu_exist == false && LButtonController.is_Saru_exist == true && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 7;
                    }

                    if (LButtonController.is_Inu_exist == false && LButtonController.is_Saru_exist == false && LButtonController.is_Kiji_exist == true)
                    {
                        akaonidamage = 5;
                    }
                }

                Akaoni.akaoniHp -= akaonidamage;
                Message.messageText.text = "桃太郎の攻撃！　" + akaonidamage + "のダメージを与えた";
                if (Akaoni.akaoniHp <= 0)
                {
                    StartCoroutine("ObjectDestroy");
                }
            }


            OnDamageEffect();

            if (isEnemyMessage == true && Akaoni.akaoniHp > 0)
            {
                // 自分自身を無効化する
                MyCanvas.SetInteractive("たたかう", false);
                MyCanvas.SetInteractive("わざ", false);
                StartCoroutine("EnemyMessage");
                StartCoroutine("ButtonActive");
            }

            if (WazaController.katon == true)
            {
                WazaController.katon.gameObject.SetActive(false);
                WazaController.mankin.gameObject.SetActive(false);
            }

            Message.isOneMessage = false;

            MyCanvas.TimeSpend();
        }
    }


    void OnDamageEffect()
    {
        Akaoni.onDamage = true;
        StartCoroutine("WaitForIt");
    }

    IEnumerator WaitForIt()
    {
        // 0.5秒間処理を止める
        yield return new WaitForSeconds(0.5f);

        // 0.5秒後ダメージフラグをfalseにして点滅を戻す
        Akaoni.onDamage = false;
        akaoniImg.color = new Color(1f, 1f, 1f, 1f);
    }

    IEnumerator EnemyMessage()
    {
        yield return new WaitForSeconds(1.5f);

        isEnemyMessage = false;
        MyCanvas.onMyDamage = true;
        MyCanvas.myHp -= Akaoni.akaoniAttack;
        Message.SetMessage("赤鬼の攻撃！" + Akaoni.akaoniAttack　+ "のダメージをくらった");
    }

    IEnumerator ButtonActive()
    {
        yield return new WaitForSeconds(2.0f);

        MyCanvas.SetInteractive("たたかう", true);
        MyCanvas.SetInteractive("わざ", true);
    }

    IEnumerator ObjectDestroy()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(akaoniImg);
        Message.SetMessage("赤鬼を倒した！");
        MyCanvas.isDestroyed = true;
    }

    IEnumerator SetSonMessage()
    {
            yield return new WaitForSeconds(2.0f);

            if (Akaoni.akaoniHp > 0)
            {
                if (isTatakauUsedFlag && MyCanvas.isSetTomorrowMessage == false)
                {
                    Message.sonTextNum = 3;
                    isSuddenEnd_T = true;
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

    IEnumerator TrueFlag()
    {
        yield return new WaitForSeconds(2.01f);

        isTatakauUsedFlag = true;
    }

    IEnumerator FalseFlag()
    {
        yield return new WaitForSeconds(2.01f);

        isTatakauUsedFlag = false;
    }

    IEnumerator SuddenEnd()
    {
        yield return new WaitForSeconds(3.0f);

        MyCanvas.NewDayStart();
    }
}
