using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStamp : MonoBehaviour
{
    public Button upgradeStampButton;
    public Text priceOfStampText;
    public Text stampLevelText;
    public int stampLevel;
    public int priceOfStamp;
    GameObject Damga;


    void Start()
    {
        Button btn = upgradeStampButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        Damga = GameObject.FindGameObjectWithTag("damga");



        if (PlayerPrefs.GetInt("stampLevel") == 0)
        {
            stampLevel = 1;
            PlayerPrefs.SetInt("stampLevel", stampLevel);
            priceOfStamp = 50;
            PlayerPrefs.SetInt("priceOfStamp", priceOfStamp);
        }

        priceOfStampText.text = PlayerPrefs.GetInt("priceOfStamp") + "$";
        stampLevelText.text = "LVL " + PlayerPrefs.GetInt("stampLevel");
    }

    void Update()
    {
        stampLevelText.text = "LVL " + PlayerPrefs.GetInt("stampLevel");
        Debug.Log(stampLevel);
    }

    void TaskOnClick()
    {


        if (PlayerPrefs.GetInt("totalScore") < PlayerPrefs.GetInt("priceOfStamp"))
        {
            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                if (stampLevel == 5 * i)
                {
                    Damga.GetComponent<DamgaControl>().damgaLevel++;
                }
            }
            stampLevel++;
            PlayerPrefs.SetInt("stampLevel", stampLevel);
            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - PlayerPrefs.GetInt("priceOfStamp"));
            priceOfStamp += 50;
            PlayerPrefs.SetInt("priceOfStamp", priceOfStamp);
            priceOfStampText.text = PlayerPrefs.GetInt("priceOfStamp") + "$";
        }

    }
}
