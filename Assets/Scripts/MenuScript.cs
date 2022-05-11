using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject leaderBoardMenu;

    private void Start()
    {
        ChangeToMainMenu();
    }

    public void ChangeToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        leaderBoardMenu.SetActive(false);
    }

    public void ChangeToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        leaderBoardMenu.SetActive(false);
    }

    public void ChangeToLeaderboard()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        leaderBoardMenu.SetActive(true);
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
