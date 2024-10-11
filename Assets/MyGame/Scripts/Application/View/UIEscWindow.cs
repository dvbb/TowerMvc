using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEscWindow : View
{
    [Header("Panels")]
    [SerializeField] private GameObject Mask;
    [SerializeField] private GameObject MainWindow;
    [SerializeField] private GameObject SettingWindow;

    [Header("UI_Components")]
    [SerializeField] private TMP_Dropdown ResolutionDropdown;
    [SerializeField] private Slider BgmSlider;
    [SerializeField] private Slider SeSlider;

    public override string Name => Consts.V_EscWindow;
    private bool isOpen;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        AttentionEvents.Add(Consts.V_EscWindow);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.V_EscWindow:
                if (isOpen)
                {
                    isOpen = false;
                    ResetWindowState();
                    return;
                }

                isOpen = true;
                OpenMainWindow();
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        ResetWindowState();
    }

    private void Update()
    {
        Debug.Log(BgmSlider.value);
        Debug.Log(SeSlider.value);
        Debug.Log(ResolutionDropdown.value);
    }

    #region Click Method

    public void OnContinueClicked()
    {
        ResetWindowState();
    }

    public void OnSettingClicked()
    {
        MainWindow.SetActive(false);
        SettingWindow.SetActive(true);
    }

    public void OnBackToTitleClicked()
    {
        Game.Instance.LoadScene(1);
    }

    public void OnBackToMapClicked()
    {
        Game.Instance.LoadScene(2);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnBgmSliderChanged()
    {
        MusicManager.Instance.SetBgmValue(BgmSlider.value);
    }

    public void OnSeSliderChanged()
    {
        MusicManager.Instance.SetSeValue(SeSlider.value);
    }

    public void OnResolutionChanged()
    {
        StaticData.Instance.SetResolution(ResolutionDropdown.value);
    }

    #endregion

    #region Method
    private void ResetWindowState()
    {
        isOpen = false;
        Mask.SetActive(false);
        MainWindow.SetActive(false);
        SettingWindow.SetActive(false);
    }

    private void OpenMainWindow()
    {
        isOpen = true;
        Mask.SetActive(true);
        MainWindow.SetActive(true);
        SettingWindow.SetActive(false);
    }

    private void OpenSettingWindow()
    {
        Mask.SetActive(true);
        MainWindow.SetActive(false);
        SettingWindow.SetActive(true);
    }
    #endregion

    #region Event Callback
    #endregion

    #region Unity Callback
    #endregion
}