using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWithPlayer : MonoBehaviour {

    [SerializeField]
    public List<DialogStep> dialogSteps = new List<DialogStep>();

    [Range(0.01f, 0.05f)]
    public float displayWait;

    private int currentStep;
    private GameObject dialogDisplay;
    private TextMeshPro dialogDisplayText;
    private int currentChar;

	void Start () {
        if (transform.childCount != 1)
        {
            Debug.LogError("Error in dialog: missing text child");
        }

        if (dialogSteps.Count == 0)
        {
            Debug.LogError("Error in dialog: missing dialog");
        }

        dialogDisplay = transform.GetChild(0).gameObject;
        dialogDisplayText = dialogDisplay.GetComponent<TextMeshPro>();

        DisableDisplay();
	}
	

	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EnableDispay();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.ActionButton())
        {
            StopCoroutine(CoroutinePartialText());
            currentStep = dialogSteps[currentStep].next;
            currentChar = 0;
            dialogDisplayText.text = PartialText();

            StartCoroutine(CoroutinePartialText());
        }
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
        StartCoroutine(CoroutinePartialText());
    }

    private void DisableDisplay()
    {
        StopCoroutine(CoroutinePartialText());

        currentStep = 0;
        currentChar = 0;
        dialogDisplayText.text = dialogSteps[currentStep].text;
        dialogDisplayText.enabled = false;
    }

    private string PartialText()
    {
        return dialogSteps[currentStep].text.Substring(0, currentChar);
    }

    private IEnumerator CoroutinePartialText()
    {
        while(currentChar < dialogSteps[currentStep].text.Length)
        {
            dialogDisplayText.text = PartialText();
            ++currentChar;
            yield return new WaitForSeconds(displayWait);
        }
    }
}

[System.Serializable]
public struct DialogStep
{
    public string text;
    public int next;
}