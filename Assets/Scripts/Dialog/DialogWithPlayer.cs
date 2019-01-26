using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWithPlayer : MonoBehaviour {

    [SerializeField]
    public List<DialogStep> dialogSteps = new List<DialogStep>();

    private int currentStep;
    private GameObject dialogDisplay;
    private TextMeshPro dialogDisplayText;


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
            currentStep = dialogSteps[currentStep].next;
            dialogDisplayText.text = dialogSteps[currentStep].text;
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
    }

    private void DisableDisplay()
    {
        currentStep = 0;
        dialogDisplayText.text = dialogSteps[currentStep].text;
        dialogDisplayText.enabled = false;
    }
}

[System.Serializable]
public struct DialogStep
{
    public string text;
    public int next;
}