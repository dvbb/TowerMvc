using System;
using System.Collections.Generic;

public class DeckModel : Singleton<DeckModel>
{
    public List<UICardItem> cardItems  = new List<UICardItem>();
}