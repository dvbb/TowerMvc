using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                    RectTransform rectTransform = GetComponent<RectTransform>();
                    rectTransform.position += new Vector3(-200, 0, 0);
                }
                break;
            case Consts.E_EndCardDrag:
                {
                    gameObject.SetActive(false);
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    RectTransform rectTransform = GetComponent<RectTransform>();
                    rectTransform.position += new Vector3(200, 0, 0);
                }
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && gameObject.activeSelf)
        {
            SendEvent(Consts.E_CardUnSelect);
            SendEvent(Consts.E_HideNode);
        }
    }

    #region Method
    public IEnumerator PanLeft()
    {
        yield return new WaitForSeconds(.1f);
    }

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion
}