using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStamina : MonoBehaviour
{
	public Button upgradeStaminaButton;
	public Text priceOfStaminaText;
	int priceOfStamina;
	GameObject Damga;
	void Start()
	{
		Button btn = upgradeStaminaButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Damga = GameObject.FindGameObjectWithTag("damga");

		if (priceOfStamina == 0)
		{
			priceOfStamina = 50;
			PlayerPrefs.SetInt("priceOfStamina", priceOfStamina);
		}


		priceOfStaminaText.text = PlayerPrefs.GetInt("priceOfStamina") + "$";
	}

	void TaskOnClick()
	{
		GameController.instance.isContinue = false;
	

		if (PlayerPrefs.GetInt("totalScore") < PlayerPrefs.GetInt("priceOfStamina"))
		{
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
		}
		else
		{
			Damga.GetComponent<DamgaControl>().elHakkiLimit++;
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - PlayerPrefs.GetInt("priceOfStamina"));
			priceOfStamina *= 2;
			PlayerPrefs.SetInt("priceOfStamina", priceOfStamina);
			priceOfStaminaText.text = PlayerPrefs.GetInt("priceOfStamina") + "$";

		}

	}
}
