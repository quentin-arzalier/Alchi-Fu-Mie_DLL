using AFM_DLL.Models.Cards;
using AFM_DLL.Models.PlayerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.BoardData
{
    public class BoardSide
    {
        public PlayerGame Player { get; set; }

        public Dictionary<BoardPosition, ElementCard> ElementCards { get; set; } = new Dictionary<BoardPosition, ElementCard>()
            {
                { BoardPosition.LEFT, null },
                { BoardPosition.MIDDLE, null },
                { BoardPosition.RIGHT, null },
            };

        public SpellCard SpellCard { get; set; }


        public List<ElementCard> AllElementsOfSide => ElementCards.Values.ToList();
    }
}
