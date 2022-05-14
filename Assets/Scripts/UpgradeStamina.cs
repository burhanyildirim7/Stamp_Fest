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
		Damga.GetComponent<DamgaControl>().elHakkiLimit++;
		
		
	}
}
