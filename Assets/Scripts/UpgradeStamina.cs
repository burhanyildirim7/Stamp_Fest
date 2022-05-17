using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStamina : MonoBehaviour
{
	public Button upgradeStaminaButton;
	GameObject Damga;
	void Start()
	{
		Button btn = upgradeStaminaButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Damga = GameObject.FindGameObjectWithTag("damga");
	}

	void TaskOnClick()
	{
		GameController.instance.isContinue = false;
	

		if (PlayerPrefs.GetInt("totalScore") < 50)
		{
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
		}
		else
		{
			Damga.GetComponent<DamgaControl>().elHakkiLimit++;
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - 50);
		}

	}
}
