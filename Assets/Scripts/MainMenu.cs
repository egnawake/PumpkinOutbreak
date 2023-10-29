using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button smashButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private string gameSceneName;

    private void Start()
    {
        smashButton.onClick.AddListener(StartGame);
        closeButton.onClick.AddListener(Quit);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
