using UnityEngine;

public class UICardTable : View
{
    #region Component
    [SerializeField] private RectTransform contentPosition;
    #endregion

    public override string Name => Consts.V_CardTable;

    public override void HandleEvent(string eventName, object obj)
    {
    }

    private void Awake()
    {
        // TODO: Just for test, will delete late
        for (int i = 1; i < 6; i++)
        {
            GameObject CardItemPrefab = Resources.Load(@"Prefabs\Card\CardItem") as GameObject;
            GameObject obj = Instantiate(CardItemPrefab, contentPosition);
            UICardItem cardItem = obj.GetComponent<UICardItem>();

            //"D:\repo\TowerMvc\Assets\Resources\UI\Card\CardIcon\card_archer.png"
            Card card = new Card()
            {
                //imgPath = Random.Range(0, 100) > 50 ? @"UI\Card\CardIcon\card_archer": @"UI\Card\CardIcon\card_robot",
                id = i,
                imgPath = @"UI\Card\CardIcon\" + i,
                cost = Random.Range(100, 200),
                atk = Random.Range(100, 200),
                aspd = Random.Range(50, 100),
                atkType = Random.Range(0, 100) > 50 ? "物理" : "魔法",
                prefabPath = "Prefabs/Turret/archer",
            };
            cardItem.Init(card);
            DeckModel.Instance.cardItems.Add(cardItem);
        }
    }

    #region Method

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion

}