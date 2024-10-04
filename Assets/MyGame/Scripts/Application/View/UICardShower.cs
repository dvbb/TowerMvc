using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.LightingExplorerTableColumn;

public class UICardShower : View
{
    #region Component
    private Image image;
    private TextMeshProUGUI TMP_ATK;
    private TextMeshProUGUI TMP_ASPD;
    private TextMeshProUGUI TMP_AtkType;
    #endregion
    public override string Name => Consts.V_CardShower;

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_CardItemClick:
                var card = obj as Card;
                image.sprite = Resources.Load<Sprite>(card.imgPath);
                TMP_ATK.text = "攻击力: " + card.atk;
                TMP_ASPD.text = "攻速: " + card.aspd;
                TMP_AtkType.text = "伤害类型: " + card.atkType;
                break;
            default:
                break;
        }
    }

    #region Method

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion
}