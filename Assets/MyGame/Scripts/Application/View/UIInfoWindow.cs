﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIInfoWindow : View
{
    private bool isStop;
    private int currentSpeed;

    // components
    [SerializeField] private TextMeshProUGUI text_Speed;
    [SerializeField] private TextMeshProUGUI text_Goal;
    [SerializeField] private TextMeshProUGUI text_Health;
    [SerializeField] private TextMeshProUGUI text_Coin;
    [SerializeField] private Image image_StartButton;

    // stored image
    private Sprite icon_start;
    private Sprite icon_stop;

    private string Goal => LevelModel.Instance.DestroyedEnemies.ToString() + "/" + LevelModel.Instance.TotalEnemies.ToString();

    public override string Name => Consts.V_InfoWindow;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        AttentionEvents.Add(Consts.E_SubtractHealth);
        AttentionEvents.Add(Consts.E_EnemyDestroyed);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_SubtractHealth:
                text_Goal.text = Goal;
                text_Health.text = GameModel.Instance.Health.ToString();
                break;
            case Consts.E_EnemyDestroyed:
                text_Goal.text = Goal;
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        // Get icons
        icon_start = Resources.Load<Sprite>("UI/Scenes/Icons/GUI_33");
        icon_stop = Resources.Load<Sprite>("UI/Scenes/Icons/GUI_34");

        // Get Components
        //image_StartButton = gameObject.GetComponentInChildren<Image>();
        //text_SpeedButton = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        // Init
        currentSpeed = 1;
        text_Coin.text = LevelModel.Instance.Cost.ToString();
        text_Health.text = GameModel.Instance.Health.ToString();
        text_Goal.text = Goal;
    }

    #region Unity Button Click Method

    public void OnStartClicked()
    {
        if (isStop) // stop => start
        {
            Time.timeScale = currentSpeed;
            image_StartButton.sprite = icon_stop;
            isStop = false;
        }
        else
        {
            Time.timeScale = 0;
            image_StartButton.sprite = icon_start;
            isStop = true;
        }
    }

    public void OnSpeedClicked()
    {
        switch (currentSpeed)
        {
            case 1:
                currentSpeed = 2;
                text_Speed.text = "X2";
                break;
            case 2:
                currentSpeed = 1;
                text_Speed.text = "X1";
                break;
            default:
                break;
        }

        if (!isStop) // not stop then change time scale
            Time.timeScale = currentSpeed;
    }

    #endregion

    #region Method

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion
}