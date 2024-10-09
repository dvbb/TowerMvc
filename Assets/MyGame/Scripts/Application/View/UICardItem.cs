using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICardItem : View, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Card card { get; set; }
    public bool isSelected;

    #region Components
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private RectTransform rectTransform;
    #endregion

    public override string Name => Consts.V_CardItem;

    /// <summary>
    /// CardItem not include in Views, so RegisterEvents() does not work
    /// </summary>
    public override void RegisterEvents()
    {
        base.RegisterEvents();
    }

    public override void HandleEvent(string eventName, object obj)
    {
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
            UnSelectCard();
            return;
        }

        // 2. Selected card is not the same as current one
        SelectCard();
    }

    private void UnSelectCard()
    {
        SendEvent(Consts.E_CardUnSelect);
        SendEvent(Consts.E_HideNode);
        return;
    }

    private void SelectCard()
    {
        CardArgs cardArgs = new CardArgs()
        {
            CardId = card.id,
        };
        SendEvent(Consts.E_ShowNode);
        SendEvent(Consts.E_CardSelect, cardArgs);
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        SelectCard();
        SendEvent(Consts.E_StartCardDrag, card);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UnSelectCard();
        SendEvent(Consts.E_EndCardDrag);
    }



    #endregion

    #region Event Callback

    #endregion
}