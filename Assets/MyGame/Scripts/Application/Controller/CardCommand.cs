using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CardCommand : Controller
{
    public override void Execute(object obj)
    {
        // obj == null means that cancel select
        if (obj == null)
        {
            foreach (var cardItem in DeckModel.Instance.cardItems)
            {
                if (cardItem.isSelected)
                {
                    cardItem.DisableSelect();
                }
            }
            return;
        }

        var args = obj as CardArgs;
        foreach (var cardItem in DeckModel.Instance.cardItems)
        {
            // 1. Show selected card
            if (cardItem.card.id == args.CardId && !cardItem.isSelected)
            {
                cardItem.EnableSelect();
            }
            // 2. Hide the last selected card
            if (cardItem.card.id != args.CardId && cardItem.isSelected)
            {
                cardItem.DisableSelect();
            }
        }
    }
}