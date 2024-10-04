using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class UICardItem : View
{
    public Card card { get; set; }
    private bool isSelected;

    #region Components
    private Button button;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    #endregion

    public override string Name => throw new NotImplementedException();

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_CardSelect:
                var selectedCard = obj as Card;
                if (card.id != selectedCard.id)
                {
                    isSelected = false;
                }
                break;
            default:
                break;
        }
    }

    public void Init(Card card)
    {
        this.card = card;
        button = GetComponentInChildren<Button>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();
        foreach (var t in transforms)
        {
            if (t.name == "Button")
                rectTransform = t;
        }
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>(card.imgPath);
        textMeshPro.text = card.cost.ToString();
    }

    #region Method
    public void OnCardItemClicked()
    {
        if (isSelected)
            return;

        isSelected = true;
        SendEvent(Consts.E_CardSelect, card);
        SendEvent(Consts.E_CardItemClick, card);
    }

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion
}