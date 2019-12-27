using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Message : MonoBehaviour
{
    // メッセージUI
    public static Text messageText;
    // 表示するメッセージ
    [TextArea(1, 4)]
    public static string allMessage;

    [TextArea(1, 2)]
    public static string sonMessage;

    [TextArea(1, 2)]
    public static string dadMessage;

    public static List<string> dadText = new List<string>();
    public static List<string> sonText = new List<string>();
    public static GameObject dad;
    public static GameObject son;

    public static int dadTextNum;
    public static int sonTextNum;

    //　使用する分割文字列
    public string splitString = "<>";
    //　分割したメッセージ
    public static string[] splitMessage;
    //　分割したメッセージの何番目か
    public static int messageNum;
    //　1回分のメッセージを表示したかどうか
    public static bool isOneMessage = false;
    //　メッセージをすべて表示したかどうか
    public static bool isEndMessage = false;

    public static bool isKatonFlag = false;
   


    // Start is called before the first frame update
    void Start()
    {
        messageText = GetComponentInChildren<Text>();
        messageText.text = allMessage;
        SetMessage(allMessage);

        dadText.Add("次はね・・・");
        dadText.Add("おや？");
        dadText.Add("それはね・・・");
        dadText.Add("犬「仲間にしてください」");
        dadText.Add("猿「仲間にしてくれよ」");
        dadText.Add("キジ「私と仲間になろう」");
        dadText.Add("昨日はそういえば・・・");
        dadText.Add("仲間を集める？");

        sonText.Add("次はどうするの？");
        sonText.Add("次は　かとん　を使って！");
        sonText.Add("どんどんいけー！");
        sonText.Add("・・・");
        sonText.Add("ZZZ...");
        sonText.Add("鬼は何をしてたの？");
        sonText.Add("どうしようかな・・・");
        sonText.Add("どうだったっけ？");
        sonText.Add("えっそうだっけ・・・");
        sonText.Add("うん、そうだったね！");
        sonText.Add("ちがった気がするけど・・・");

        dad = GameObject.Find("Dad");
        dadTextNum = 2;
        dad.GetComponentInChildren<Text>().text = dadText[dadTextNum];

        son = GameObject.Find("Son");
        sonTextNum = 5;
        SetSonMessage(sonText[sonTextNum]);
    }

    // Update is called once per frame
    void Update()
    {



    }

    //　新しいメッセージを設定
    public static void SetMessage(string message)
    {
        messageText.text = message;
    }

    public static void SetSonMessage(string message)
    {
        son.GetComponentInChildren<Text>().text = message;
    }

    public static void SetDadMessage()
    {
        dad.GetComponentInChildren<Text>().text = dadText[dadTextNum];
    }

//　他のスクリプトから新しいメッセージを設定しUIをアクティブにする
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
        transform.GetChild(0).gameObject.SetActive(true);
    }   
}
