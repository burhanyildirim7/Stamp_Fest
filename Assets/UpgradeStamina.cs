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
		GameObject.FindGameObjectWithTag("damga").GetComponent<DamgaControl>().damgaHakký += 1;
	}
}
