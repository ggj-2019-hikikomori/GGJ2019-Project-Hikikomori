using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
public class DialogWithPlayer : MonoBehaviour {

    [Range(0.001f, 0.02f)]
    public float displaySpeedInverse;

    [SerializeField]
    public List<DialogStep> dialogSteps = new List<DialogStep>();

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

        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (dialogSteps.Count == 0)
        {
            Debug.LogError("Error in dialog: missing dialog");
        }

        if (transform.childCount != 1)
        {
            Debug.LogError("Error in dialog: missing text child");
        }

        dialogDisplay = transform.GetChild(0).gameObject;
        dialogDisplayText = dialogDisplay.GetComponent<TextMeshPro>();

        if(dialogDisplay.transform.childCount != 1)
        {
            Debug.LogError("Error in dialog: missing bubble");
        }

        bubbleDisplaySprite = dialogDisplay.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        refRectTransform = dialogDisplayText.GetComponent<RectTransform>();
        originalPositionRectTransform = refRectTransform.position;
        
        DisableDisplay();
	}
	

	void Update () {
        flagActionInput = GameManager.ActionButton();
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
        if (currentChar >= dialogSteps[currentStep].text.Length) 
        {
            if (other.CompareTag("Player") && flagActionInput)
            {
                StopCoroutine(CoroutinePartialText());
                currentStep = dialogSteps[currentStep].next;
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
        return dialogSteps[currentStep].text.Substring(0, currentChar);
    }

    private void Formatting()
    {
        if(dialogSteps[currentStep].player && dialogSteps[currentStep].internalThoughts)
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

        switch(dialogSteps[currentStep].feeling)
        {
            case DiaglogFeeling.Neutral:
                dialogDisplayText.outlineColor = Color.black;
                break;
            case DiaglogFeeling.Good:
                dialogDisplayText.outlineColor = Color.green;
                break;
            case DiaglogFeeling.Bad:
                dialogDisplayText.outlineColor = Color.red;
                break;
        }
    }

    private void Placing()
    {
        if(dialogSteps[currentStep].player)
        {
            refRectTransform.position = new Vector3(player.transform.position.x, originalPositionRectTransform.y, player.transform.position.z);
            
        }
        else
        {
            refRectTransform.position = originalPositionRectTransform;
        }
        refRectTransform.rotation = Quaternion.identity;
        refRectTransform.rotation *= Quaternion.FromToRotation(refRectTransform.forward, Vector3.ProjectOnPlane(refRectTransform.position - mainCamera.transform.position, Vector3.up));
    }

    private IEnumerator CoroutinePartialText()
    {
        yield return new WaitForSeconds(dialogSteps[currentStep].delayBeforeDisplay);
        while(currentChar < dialogSteps[currentStep].text.Length)
        {
            ++currentChar;
            dialogDisplayText.text = PartialText();
            yield return new WaitForSeconds(displaySpeedInverse);
        }
    }
}*/