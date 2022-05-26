using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStamina : MonoBehaviour
{
	public Button upgradeStaminaButton;
	public Text priceOfStaminaText;
	public Text staminaLevelText;
	public int staminaLevel;
	int priceOfStamina;
	GameObject Damga;
	void Start()
	{
		Button btn = upgradeStaminaButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Damga = GameObject.FindGameObjectWithTag("damga");

		if (PlayerPrefs.GetInt("staminaLevel") == 0)
		{
			priceOfStamina = 50;
			PlayerPrefs.SetInt("priceOfStamina", priceOfStamina);
			staminaLevel = 1;
			PlayerPrefs.SetInt("staminaLevel", staminaLevel);
		}


		priceOfStaminaText.text = PlayerPrefs.GetInt("priceOfStamina") + "$";
		staminaLevelText.text = "LVL " + PlayerPrefs.GetInt("staminaLevel");
	}

     void Update()
    {
		staminaLevelText.text = "LVL " + PlayerPrefs.GetInt("staminaLevel");
		Debug.Log(staminaLevel);
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
			staminaLevel++;
			PlayerPrefs.SetInt("staminaLevel", staminaLevel);
			Damga.GetComponent<DamgaControl>().elHakkiLimit++;
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - PlayerPrefs.GetInt("priceOfStamina"));
			priceOfStamina *= 2;
			PlayerPrefs.SetInt("priceOfStamina", priceOfStamina);
			priceOfStaminaText.text = PlayerPrefs.GetInt("priceOfStamina") + "$";

		}

	}
}
