using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperDialogue : MonoBehaviour {

    [SerializeField] GameObject playerDialogueObj;
    [SerializeField] GameObject npcDialogueObj;
    [SerializeField] GameObject pressETextObj;
    [SerializeField] Text playerText;
    [SerializeField] Text npcText;

    bool isTalking = false;
    bool isLayer_1 = true;
    bool isLayer_2 = false;
    int layer_1_Decision = 0; // Conversation decision
    int layer_2_Decision = 0;
    float closeDelay = 2f;
	
	// Update is called once per frame
	void Update () {
        if (isTalking)
        {
            UpdateConversation();
        }
	}

    public void StartConversation()
    {

        npcText.text = "Narinkuş: \"Yardım edin!.. Köyümüzü eşkıyalar bastı. Bir çeşit... Bir çeşit taş var..." +
            "Çok, çok korkunçtu. Lütfen, yardım edin!\"";
        playerText.text = "Judy:\n" +
            "1.\t\"Tamam, tamam. Sakin ol. Köyün ne tarafta?\"\n" +
            "2.\t\"Ne işim olur. Ben böyle rahatım.\"\n" +
            "3.\t Bir şey deme.";

        playerDialogueObj.SetActive(true);
        npcDialogueObj.SetActive(true);
        pressETextObj.SetActive(false);
        isTalking = true;
    }

    IEnumerator FinishConversation()
    {
        playerDialogueObj.SetActive(false);
        npcDialogueObj.SetActive(false);
        yield return new WaitForSeconds(3f);
        isTalking = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !(isTalking))
        {
            pressETextObj.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E))
            {
                StartConversation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pressETextObj.SetActive(false);
        }
    }

    void UpdateConversation()
    {
        if (isLayer_1)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                npcText.text = "Narinkuş: \"Şu tarafa bacım\"";
                playerText.text = "Judy:\n" +
                    "1.\t\"Tamam. Sen merak etme.\"\n" +
                    "2.\t\"Güzel. Ben zaten oraya gitmiyordum.\"\n" +
                    "3.\t Bir şey deme.";
                isLayer_1 = false;
                isLayer_2 = true;
                layer_1_Decision = 1;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                npcText.text = "Narinkuş: \"Bu kötü kalbin yüzünden senin belan olacağım.\"";
                playerText.text = "";
                // Enemy narinkuş
                isTalking = false;
                StartCoroutine(waitAndCloseDialogue(closeDelay));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                StartCoroutine(FinishConversation());
                // Enemy narinkuş
            }
        }
        else if (isLayer_2 && layer_1_Decision == 1)
        {
            if(Input.GetKeyUp(KeyCode.Alpha1))
            {
                npcText.text = "Narinkuş: \"Eyvallah bacım. Tanrı seni korusun.\"";
                playerText.text = "";
                StartCoroutine(waitAndCloseDialogue(closeDelay));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                npcText.text = "Narinkuş: \"Bu kötü kalbin yüzünden senin belan olacağım.\"";
                playerText.text = "";
                // Enemy narinkuş
                StartCoroutine(waitAndCloseDialogue(closeDelay));
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                StartCoroutine(FinishConversation());
            }
        }
    }

    IEnumerator waitAndCloseDialogue(float secs)
    {
        yield return new WaitForSeconds(secs);
        StartCoroutine(FinishConversation());
    }
}
