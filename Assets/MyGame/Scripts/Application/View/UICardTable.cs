using UnityEngine;

public class UICardTable : View
{
    public override string Name => Consts.V_CardTable;

    public override void HandleEvent(string eventName, object obj)
    {
    }

    private void Awake()
    {
        for (int i = 3; i < 8; i++)
        {
            //GameObject obj = Instantiate(CardItemPrefab, CardContent);
            //Card card = obj.GetComponent<Card>();
            //card.imgPath = $"Characters/char_{i}";
            //card.cost = Random.Range(100, 200);
            //card.atk = Random.Range(100, 200);
            //card.aspd = Random.Range(50, 100);
            //card.atkType = Random.Range(0, 100) > 50 ? "物理" : "魔法";
            //card.prefabPath = "Turrets/archer_level_1";
        }
    }

    #region Method

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion

}