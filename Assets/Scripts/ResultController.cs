using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    GameObject RText;

    // Start is called before the first frame update
    void Start()
    {
        RText = GameObject.Find("ResultText");

        if(MyCanvas.myHp <= 0)
        {
            RText.GetComponent<Text>().text = "GAME OVER";
        }
        else
        {
            RText.GetComponent<Text>().text = MyCanvas.days + " 日目の "
           + MyCanvas.hour + " 時 " + MyCanvas.minutes1 + MyCanvas.minutes2 + " 分 でクリアした！";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
