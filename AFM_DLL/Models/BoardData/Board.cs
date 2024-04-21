using AFM_DLL.Models.Cards;
using AFM_DLL.Models.PlayerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_DLL.Models.BoardData
{
    public class Board
    {
        public BoardSide BlueSide { get; private set; } = new BoardSide();
        public BoardSide RedSide { get; private set; } = new BoardSide();
        public List<BoardModifiers> Modifiers { get; private set; } = new List<BoardModifiers>();


        public BoardSide GetAllyBoardSide(bool isBlue) => isBlue ? BlueSide : RedSide;
        public BoardSide GetEnemyBoardSide(bool isBlue) => isBlue ? RedSide : BlueSide;

        public List<ElementCard> AllElementsOfBoard => BlueSide.AllElementsOfSide.Concat(RedSide.AllElementsOfSide).ToList();
    }
}
