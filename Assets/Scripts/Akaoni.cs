using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Akaoni : MonoBehaviour
{
    public static Image akaoniImg;
    GameObject button;
    public static int akaoniHp;
    public static int akaoniAttack;
    public float flashTime = 0.2f;
    public float elapsedTime;
    public static bool onDamage = false;
    GameObject sword;


    private void Awake()
    {
        akaoniImg = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        akaoniImg.enabled = false;
        button = GameObject.Find("たたかう");
    }

    // Update is called once per frame
    void Update()
    {

        if (onDamage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            akaoniImg.color = new Color(1f, 1f, 1f, level);
        }



    }
}
