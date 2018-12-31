using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameOverMenu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PauseMenu.active.Equals(false))
            {
                PauseMenu.SetActive(!PauseMenu.active);
                Time.timeScale = 0f;
            }
            else {
                continueButton();
            }
            
        }
	}
    public void continueButton() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void exitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void retryButton() {
        SceneManager.LoadScene("SampleScene");
    }
    public void pretargetRetry() {
        GetComponent<EventSystem>().firstSelectedGameObject = GameOverMenu.transform.GetChild(0).gameObject;
    }
}
