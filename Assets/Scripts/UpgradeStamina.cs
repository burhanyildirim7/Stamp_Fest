using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStamina : MonoBehaviour
{
	public Button upgradeStaminaButton;

	void Start()
	{
		Button btn = upgradeStaminaButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		//Stamina Arttýrma Kodu Buraya Gelecek
	}
}
