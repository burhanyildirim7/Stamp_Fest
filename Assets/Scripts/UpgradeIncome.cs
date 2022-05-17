using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeIncome : MonoBehaviour
{
	public Button upgradeIncomeButton;

	void Start()
	{
		Button btn = upgradeIncomeButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		

		if (PlayerPrefs.GetInt("totalScore") < 50)
		{
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
		}
		else
		{
			GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().dolarMiktarý += 5;
			PlayerPrefs.SetInt("dolarMiktarý", GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().dolarMiktarý);

			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - 50);
		}

	}
}
