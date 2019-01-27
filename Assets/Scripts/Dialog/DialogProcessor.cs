using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogProcessor : MonoBehaviour {

	public Animator animator;
	public List<Dialog> dialogs;

	private Dialog currentDialog;
	private int currentStep;
	private int currentChar;
	private GameObject dialogDisplay;
	private SpriteRenderer bubbleDisplaySprite;
	private TextMeshPro dialogDisplayText;
	private RectTransform refRectTransform;
	private Vector3 originalPositionRectTransform;

	private GameObject player;
	private GameObject mainCamera;

	private bool flagActionInput;

	void Start () {

		currentDialog = GetDialog();

		player = GameObject.FindGameObjectWithTag("Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

		if (currentDialog.dialogSteps.Count == 0)
		{
			Debug.LogError("Error in dialog: missing dialog");
		}

		if (transform.childCount < 1)
		{
			Debug.LogError("Error in dialog: missing text child");
		}

		dialogDisplay = transform.GetChild(0).gameObject;
		dialogDisplayText = dialogDisplay.GetComponent<TextMeshPro>();

		if (dialogDisplay.transform.childCount < 1)
		{
			Debug.LogError("Error in dialog: missing bubble");
		}

		bubbleDisplaySprite = dialogDisplay.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

		refRectTransform = dialogDisplayText.GetComponent<RectTransform>();
		originalPositionRectTransform = refRectTransform.position;

		DisableDisplay();
	}

	private Dialog GetDialog()
	{
		foreach (Dialog dialog in dialogs)
		{
			if (CheckDialogConditions(dialog)) return dialog;
		}

		return GameManager.instance.dialog_empty;
	}

	private bool CheckDialogConditions(Dialog dialog)
	{
		foreach (GameManager.StoryVariable variable in dialog.conditions) {
			if (variable.value != GameManager.GetVariable(variable.name)) {
				return false;
			}
		}
		return true;
	}

	void Update()
	{
		flagActionInput = GameManager.ActionButton();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			EnableDispay();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (currentChar >= GetDisplayText().Length)
		{
			if (other.CompareTag("Player") && flagActionInput)
			{
				StopCoroutine(CoroutinePartialText());

				Dialog.DialogStep dialogStep = currentDialog.dialogSteps[currentStep];
				// NEXT STEP EVENT : update variables
				foreach (GameManager.StoryVariable variable in dialogStep.variableUpdates) {
					GameManager.SetVariable(variable);
				}

				if (animator != null)
				{
					if (dialogStep.animation == Dialog.Animation.GIVE)
					{
						animator.SetTrigger("Give");
					} else if (dialogStep.animation == Dialog.Animation.WAVE) {
						animator.SetTrigger("Wave");
					}
				}

				if (dialogStep.choices.Count == 0)
				{
					currentStep = currentDialog.dialogSteps[currentStep].next;
				}
				else
				{
					// TODO : Implement Choice w/ Coroutine ?
					currentStep = dialogStep.choices[0].target;
				}

				if (currentStep == -1)
				{
					currentStep = 0;
					currentDialog = GetDialog();
				}
				currentChar = 0;
				dialogDisplayText.text = PartialText();
				Formatting();

				StartCoroutine(CoroutinePartialText());

			}
		}
		Placing();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			DisableDisplay();
		}
	}

	private void EnableDispay()
	{
		dialogDisplayText.enabled = true;
		dialogDisplayText.text = "";
		StartCoroutine(CoroutinePartialText());
		Formatting();
		Placing();
	}

	private void DisableDisplay()
	{
		StopCoroutine(CoroutinePartialText());

		currentStep = 0;
		currentChar = 0;
		dialogDisplayText.text = "";
		dialogDisplayText.enabled = false;
		bubbleDisplaySprite.enabled = false;
	}

	private string PartialText()
	{
		return GetDisplayText().Substring(0, currentChar);
	}

	private void Formatting()
	{
		if (currentDialog.dialogSteps[currentStep].player && currentDialog.dialogSteps[currentStep].internalThoughts)
		{
			dialogDisplayText.color = Color.white;
			dialogDisplayText.fontStyle = FontStyles.Bold | FontStyles.Italic;
			dialogDisplayText.outlineWidth = 0.2f;
			bubbleDisplaySprite.enabled = true;
		}
		else
		{
			dialogDisplayText.color = Color.white;
			dialogDisplayText.fontStyle = FontStyles.Bold;
			dialogDisplayText.outlineWidth = 0.2f;
			bubbleDisplaySprite.enabled = false;
		}

		switch (currentDialog.dialogSteps[currentStep].feeling)
		{
			case Dialog.DiaglogFeeling.Neutral:
				dialogDisplayText.outlineColor = Color.black;
				break;
			case Dialog.DiaglogFeeling.Good:
				dialogDisplayText.outlineColor = Color.green;
				break;
			case Dialog.DiaglogFeeling.Bad:
				dialogDisplayText.outlineColor = Color.red;
				break;
		}
	}

	private void Placing()
	{
		if (currentDialog.dialogSteps[currentStep].player)
		{
			refRectTransform.position = new Vector3(player.transform.position.x, originalPositionRectTransform.y, player.transform.position.z);

		}
		else
		{
			refRectTransform.position = originalPositionRectTransform;
		}
		refRectTransform.rotation = Quaternion.identity;
		refRectTransform.rotation *= Quaternion.FromToRotation(refRectTransform.forward, refRectTransform.position - mainCamera.transform.position);
	}

	private IEnumerator CoroutinePartialText()
	{
		yield return new WaitForSeconds(currentDialog.dialogSteps[currentStep].delayBeforeDisplay);
		while (currentChar < GetDisplayText().Length)
		{
			++currentChar;
			dialogDisplayText.text = PartialText();
			yield return new WaitForSeconds(currentDialog.displaySpeedInverse);
		}
	}

	private string GetDisplayText()
	{
		return Localization.Translate("dialog." + currentDialog.unlocalizedName + "." + currentDialog.dialogSteps[currentStep].text);
	}
}
