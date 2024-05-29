using AFM_DLL;
using AFM_DLL.Models.PlayerInfo;

namespace AFM_Tests.TestData
{
    public static class TestPlayers
    {
        public static PlayerGame GetRockPlayer() => new(TestDecks.GetRockDeck());
        public static PlayerGame GetPaperPlayer() => new(TestDecks.GetPaperDeck());
        public static PlayerGame GetScissorsPlayer() => new(TestDecks.GetScissorsDeck());
        public static PlayerGame FromElement(Element elt) => new(TestDecks.GetElementDeck(elt));

    }
}
