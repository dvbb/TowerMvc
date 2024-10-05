using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeckModel : Singleton<DeckModel>
{
    public List<UICardItem> cardItems  = new List<UICardItem>();
}