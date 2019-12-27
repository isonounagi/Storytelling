using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WazaController : MonoBehaviour
{
    public static GameObject katon;
    public static GameObject mankin;

    public static bool isWButtonEnabled = false;

    private void Awake()
    {
        katon = GameObject.Find("かとん");
        mankin = GameObject.Find("まんきん");
    }

    private void Start()
    {
        katon.gameObject.SetActive(false);
        mankin.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isWButtonEnabled)
        {
            MyCanvas.SetInteractive("わざ", true);
        }
        else
        {
            MyCanvas.SetInteractive("わざ", false);
        }
    }

    public void OnClick()
    {
        Message.SetMessage("");

        if(MyCanvas.myMp >= 5)
        {
            katon.gameObject.SetActive(true);
        }

        if (MyCanvas.myMp >= 3)
        {
            mankin.gameObject.SetActive(true);
        }

    }
}
