using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStamp : MonoBehaviour
{
	public Button upgradeStampButton;
	public Text priceOfStampText;
	int priceOfStamp;
	GameObject Damga;
	void Start()
	{
		Button btn = upgradeStampButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Damga = GameObject.FindGameObjectWithTag("damga");

		if (priceOfStamp == 0)
		{
			priceOfStamp = 50;
			PlayerPrefs.SetInt("priceOfStamp", priceOfStamp);
		}
	
		priceOfStampText.text = PlayerPrefs.GetInt("priceOfStamp") + "$";
	}

	void TaskOnClick()
	{

        if (PlayerPrefs.GetInt("totalScore")< PlayerPrefs.GetInt("priceOfStamp"))
        {
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
		}
        else
        {
			Damga.GetComponent<DamgaControl>().damgaLevel++;
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - PlayerPrefs.GetInt("priceOfStamp"));
			priceOfStamp *= 2;
			PlayerPrefs.SetInt("priceOfStamp", priceOfStamp);
			priceOfStampText.text = PlayerPrefs.GetInt("priceOfStamp") + "$";
		}
	
	}
}
