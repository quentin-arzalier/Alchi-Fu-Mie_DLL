using AFM_DLL;
using AFM_DLL.Helpers;
using AFM_DLL.Models.Enum;

namespace AFM_Tests.Helpers
{
    public class FightHelperTest
    {

        [TestCase(Element.ROCK, Element.SCISSORS, FightResult.BLUE_WIN)]
        [TestCase(Element.SCISSORS, Element.PAPER, FightResult.BLUE_WIN)]
        [TestCase(Element.PAPER, Element.ROCK, FightResult.BLUE_WIN)]

        [TestCase(Element.ROCK, Element.PAPER, FightResult.RED_WIN)]
        [TestCase(Element.PAPER, Element.SCISSORS, FightResult.RED_WIN)]
        [TestCase(Element.SCISSORS, Element.ROCK, FightResult.RED_WIN)]

        [TestCase(Element.ROCK, Element.ROCK, FightResult.DRAW)]
        [TestCase(Element.SCISSORS, Element.SCISSORS, FightResult.DRAW)]
        [TestCase(Element.PAPER, Element.PAPER, FightResult.DRAW)]
        public void ElementFightTest(Element element, Element enemy, FightResult res)
        {
            Assert.That(res, Is.EqualTo(FightHelper.ElementFight(element, enemy)));
        }
    }
}
