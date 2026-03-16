using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show();
        });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnPausedGame += Instance_OnPausedGame;
        KitchenGameManager.Instance.OnUnPausedGame += Instance_OnUnPausedGame;
        Hide();
    }

    private void Instance_OnUnPausedGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Instance_OnPausedGame(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
