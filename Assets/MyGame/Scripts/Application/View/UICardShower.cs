using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.LightingExplorerTableColumn;

public class UICardShower : View
{
    #region Component
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI TMP_ATK;
    [SerializeField] private TextMeshProUGUI TMP_ASPD;
    [SerializeField] private TextMeshProUGUI TMP_AtkType;
    #endregion
    public override string Name => Consts.V_CardShower;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        AttentionEvents.Add(Consts.E_CardItemClick);
        AttentionEvents.Add(Consts.E_CardUnSelect);
        AttentionEvents.Add(Consts.E_StartCardDrag);
        AttentionEvents.Add(Consts.E_EndCardDrag);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_CardItemClick:
                gameObject.SetActive(true);
                var card = obj as Card;
                image.sprite = Resources.Load<Sprite>(card.imgPath);
                TMP_ATK.text = "攻击力: " + card.atk;
                TMP_ASPD.text = "攻速: " + card.aspd;
                TMP_AtkType.text = "伤害类型: " + card.atkType;
                break;
            case Consts.E_CardUnSelect:
                gameObject.SetActive(false);
                break;
            case Consts.E_StartCardDrag:
                {
                    gameObject.SetActive(true);
                    GetComponent<Image>().color = new Color(1, 1, 1, .1f);
                    transform.position += new Vector3(-400, 0, 0);
                }
                break;
            case Consts.E_EndCardDrag:
                {
                    gameObject.SetActive(false);
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    transform.position += new Vector3(400, 0, 0);
                }
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
    }

    #region Method

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion
}