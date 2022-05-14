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
		Damga.GetComponent<DamgaControl>().damgaLevel++;
		
	}
}
