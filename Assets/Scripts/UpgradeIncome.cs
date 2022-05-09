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
		GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().dolarMiktarý += 5;
	}
}
