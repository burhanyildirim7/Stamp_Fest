using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStamp : MonoBehaviour
{
	public Button upgradeStampButton;
	GameObject Damga;
	void Start()
	{
		Button btn = upgradeStampButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Damga = GameObject.FindGameObjectWithTag("damga");
	}

	void TaskOnClick()
	{

        if (PlayerPrefs.GetInt("totalScore")< 50)
        {
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore"));
		}
        else
        {
			Damga.GetComponent<DamgaControl>().damgaLevel++;
			PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - 50);
		}
	
	}
}
