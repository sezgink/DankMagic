using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KidDialogue : MonoBehaviour {

    [SerializeField] GameObject playerDialogueTextObj;
    [SerializeField] GameObject npcDialogueTextObj;
    [SerializeField] GameObject pressETextObj;
    [SerializeField] Text playerText;
    [SerializeField] Text npcText;
    [SerializeField] ItemObtained itemObtained;
    [SerializeField] GameObject hudManager;
    public GameObject topcuk;


    public GameObject manaTasiObj;
    public CharacterControl judyCon;

    bool isTalking = false;
    bool isLayer_1 = true;
    bool isLayer_2 = false;
    bool isLayer_3 = false;
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
        /*
        npcText.text = "Çocuk: \"Ühü ühü... Herkesin ruhunu emdiler. Ühü ühü...\"";
        playerText.text = "Judy:\n" +
            "1.\t\"Nasıl emdiler?\"\n" +
            "2.\t\"Kim emdi?\"";
            */
        npcText.text = "The Kid: \"I...I can't believe. I have no one. They made something...\"";
        playerText.text = "Judy:\n" +
            "1.\t\"What happened?\"\n" +
            "2.\t\"Who are they?\"";

        playerDialogueTextObj.SetActive(true);
        npcDialogueTextObj.SetActive(true);
        pressETextObj.SetActive(false);
        isTalking = true;
    }

    IEnumerator FinishConversation()
    {
        playerDialogueTextObj.SetActive(false);
        npcDialogueTextObj.SetActive(false);
        yield return new WaitForSeconds(3);
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
        if(other.tag == "Player")
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
                /*
                npcText.text = "Çocuk: \"Mor ışıklar... Emin değilim. Ühü ühü...\"";
                playerText.text = "Judy:\n" +
                    "1.\t\"Kimlerdi peki? Görebildin mi?\"\n" +
                    "2.\t\"Tamam. Sen burada dur.\"\n";
                    */
                npcText.text = "The Kid: \"I saw purple lights... Not sure...\"";
                playerText.text = "Judy:\n" +
                    "1.\t\"Who were they? Could you see?\"\n" +
                    "2.\t\"Okay wait here.\"\n";
                isLayer_1 = false;
                isLayer_2 = true;
                layer_1_Decision = 1;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                DialogueDuplicate_2();
                isLayer_1 = false;
                isLayer_2 = true;
                layer_1_Decision = 2;
            }
        }
        else if (isLayer_2)
        {
            if (layer_1_Decision == 1)
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    DialogueDuplicate_2();
                    isLayer_2 = false;
                    isLayer_3 = true;
                    layer_2_Decision = 1;

                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    StartCoroutine(FinishConversation());
                }
            }
            else if(layer_1_Decision == 2)
            {
                DialogueDuplicate_3();
            }
        }
        else if(isLayer_3)
        {
            DialogueDuplicate_3();
        }
    }

    private void DialogueDuplicate_2()
    {
        /*
        npcText.text = "Çocuk: \"Kötü kalpli, hepsi kötü kalpliydi. Onlardan bu taşı çaldım.\"";
        playerText.text = "Judy:\n" +
            "1.\t\"Hmm... Kara büyü. Tahmin etmeliydim. Sen burada bekle küçüğüm. Ben icabına bakacağım.\"\n" +
            "2.\t\"Goblin büyücü taşına benziyor. Muhtemelen bunların hepsi bir halüsinasyon.\"\n" +
            "3.\t\"Sen burada dur çocuğum, ben geleceğim.";
            */
        npcText.text = "Çocuk: \"They are b... Bad people, mercyless... I stealed that stone from them. I hop you can do something\"";
        playerText.text = "Judy:\n" +
            "1.\t\"Dark Magic stone, i am sory kid, wait here until i come back.\"\n" +
            "2.\t\"It is not an important thing you have been hallucinated.\"\n" +
            "3.\t\"Just wait here, OK?";
    }

    private void DialogueDuplicate_3()
    {
        manaTasiObj.SetActive(true);
        judyCon.ruhtasiAlindi = true;
        topcuk.SetActive(false);
        hudManager.GetComponent<HUDManager>().ActivateSpiritBar();
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            StartCoroutine(FinishConversation());
            itemObtained.ShowItem();
            layer_2_Decision = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            StartCoroutine(FinishConversation());
            itemObtained.ShowItem();
            layer_2_Decision = 2;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            StartCoroutine(FinishConversation());
            itemObtained.ShowItem();
            layer_2_Decision = 3;
        }
    }

    IEnumerator waitAndCloseDialogue(float secs)
    {
        yield return new WaitForSeconds(secs);
        FinishConversation();
    }
}
