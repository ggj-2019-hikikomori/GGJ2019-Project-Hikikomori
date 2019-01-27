using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour {

	public int choosedInd;
	public Text choice1;
	public Text choice2;

	public void updateChoiceTexts(Dialog.DialogStep dialogStep)
	{
		choice1.text = dialogStep.choices[0].name;
		choice2.text = dialogStep.choices[1].name;
	}

	public void choose1()
	{
		choosedInd = 0;
	}

	public void choose2()
	{
		choosedInd = 1;
	}
}
