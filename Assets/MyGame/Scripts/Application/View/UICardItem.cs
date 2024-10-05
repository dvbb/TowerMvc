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
    public bool isSelected;

    #region Components
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private RectTransform rectTransform;
    #endregion

    public override string Name => Consts.V_CardItem;

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_Card:
                var selectedCard = obj as Card;
                Debug.Log("ecard" + card.id);
                if (card.id != selectedCard.id && isSelected == true)
                {
                    rectTransform.transform.position -= new Vector3(0, 20);
                    isSelected = false;
                }
                break;
            default:
                break;
        }
    }

    private void Start()
    {
    }

    public void Init(Card card)
    {
        this.card = card;
        isSelected = false;

        // init component value
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>(card.imgPath);
        textMeshPro.text = card.cost.ToString();
    }

    #region Method
    public void OnCardItemClicked()
    {
        // 1. Selected card is the same as current one
        if (isSelected == true)
        {
            SendEvent(Consts.E_CardUnSelect);
            DisableSelect();
            return;
        }

        // 2. Selected card is not the same as current one
        CardArgs cardArgs = new CardArgs()
        {
            CardId = card.id,
        };
        SendEvent(Consts.E_Card, cardArgs);
        SendEvent(Consts.E_CardItemClick, card);
    }

    public void EnableSelect()
    {
        isSelected = true;
        rectTransform.transform.position += new Vector3(0, 20);
    }

    public void DisableSelect()
    {
        isSelected = false;
        rectTransform.transform.position -= new Vector3(0, 20);
    }

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion
}