using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    private string gameSceneName;
    [SerializeField] GameObject creditsCanvas;
    [SerializeField] GameObject creditsTargetButton;
    [SerializeField] GameObject baseCanvas;
    [SerializeField] GameObject baseTargetButton;

    void Start()
    {
        gameSceneName = "GameScene";
        creditsCanvas.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }

    public void ToggleCredits()
    {
        baseCanvas.SetActive(creditsCanvas.activeSelf);
        creditsCanvas.SetActive(!creditsCanvas.activeSelf);
        if (creditsCanvas.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(creditsTargetButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(baseTargetButton);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
