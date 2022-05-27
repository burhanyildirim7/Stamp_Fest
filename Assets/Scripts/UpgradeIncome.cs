using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeIncome : MonoBehaviour
{
    public Button upgradeIncomeButton;
    public Text priceOfIncomeText;
    public int incomeLevel;
    public Text incomeLevelText;
    int priceOfIncome;

    void Start()
    {
        Button btn = upgradeIncomeButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        if (priceOfIncome == 0 && incomeLevel == 0)
        {
            priceOfIncome = 50;
            PlayerPrefs.SetInt("priceOfIncome", priceOfIncome);
            incomeLevel = 1;
            PlayerPrefs.SetInt("incomeLevel", incomeLevel);
        }
        priceOfIncomeText.text = PlayerPrefs.GetInt("priceOfIncome") + "$";
        incomeLevelText.text = "LVL " + PlayerPrefs.GetInt("incomeLevel");
    }

    void Update()
    {
        incomeLevelText.text = "LVL " + PlayerPrefs.GetInt("incomeLevel");


    }


    void TaskOnClick()
    {

        if (PlayerPrefs.GetInt("totalScore") < PlayerPrefs.GetInt("priceOfIncome"))
        {
            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
        }
        else
        {

            incomeLevel++;
            PlayerPrefs.SetInt("incomeLevel", incomeLevel);
            GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().dolarMiktari += 5;
            PlayerPrefs.SetInt("dolarMiktari", GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().dolarMiktari);

            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - priceOfIncome);
            priceOfIncome += 50;
            PlayerPrefs.SetInt("priceOfIncome", priceOfIncome);
            priceOfIncomeText.text = PlayerPrefs.GetInt("priceOfIncome") + "$";
        }

    }
}
