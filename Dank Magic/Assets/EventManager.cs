using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameOverMenu;
    bool isGameOver;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseMenu.active.Equals(false))
                {
                    PauseMenu.SetActive(!PauseMenu.active);
                    Time.timeScale = 0f;
                }
                else
                {
                    continueButton();
                }

            }
        }
	}
    public void continueButton() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void exitButton()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu");
    }
    public void retryButton() {
        Time.timeScale = 1f;

        SceneManager.LoadScene("SampleScene");
    }
    public void pretargetRetry() {
        GameOverMenu.SetActive(true);
        if(!isGameOver)
            GetComponent<EventSystem>().SetSelectedGameObject(GameOverMenu.transform.GetChild(0).gameObject);

        isGameOver = true;
    }
}
