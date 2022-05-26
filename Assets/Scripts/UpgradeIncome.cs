using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeIncome : MonoBehaviour
{
	public Button upgradeIncomeButton;
	public Text priceOfIncomeText;
	int priceOfIncome;

	void Start()
	{
		Button btn = upgradeIncomeButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

        if (priceOfIncome == 0)
        {
			priceOfIncome = 50;
			PlayerPrefs.SetInt("priceOfIncome", priceOfIncome);
		}
		priceOfIncomeText.text = PlayerPrefs.GetInt("priceOfIncome") + "$";
	}

	

	void TaskOnClick()
	{
		

		if (PlayerPrefs.GetInt("totalScore") < PlayerPrefs.GetInt("priceOfIncome"))
		{
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
		}
		else
		{
			GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().dolarMiktar += 5;
			PlayerPrefs.SetInt("dolarMiktar�", GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().dolarMiktar);

			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - priceOfIncome);
			priceOfIncome *= 2;
			PlayerPrefs.SetInt("priceOfIncome", priceOfIncome);
			priceOfIncomeText.text = PlayerPrefs.GetInt("priceOfIncome") + "$";
		}

	}
}
