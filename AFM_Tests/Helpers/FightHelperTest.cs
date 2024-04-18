using AFM_DLL;
using AFM_DLL.Helpers;
using AFM_DLL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_Tests.Helpers
{
    public class FightHelperTest
    {

        [TestCase(Element.ROCK, Element.SCISSORS, FightResult.WIN)]
        [TestCase(Element.SCISSORS, Element.PAPER, FightResult.WIN)]
        [TestCase(Element.PAPER, Element.ROCK, FightResult.WIN)]
        
        [TestCase(Element.ROCK, Element.PAPER, FightResult.LOSE)]
        [TestCase(Element.PAPER, Element.SCISSORS, FightResult.LOSE)]
        [TestCase(Element.SCISSORS, Element.ROCK, FightResult.LOSE)]

        [TestCase(Element.ROCK, Element.ROCK, FightResult.DRAW)]
        [TestCase(Element.SCISSORS, Element.SCISSORS, FightResult.DRAW)]
        [TestCase(Element.PAPER, Element.PAPER, FightResult.DRAW)]
        public void ElementFightTest(Element element, Element enemy, FightResult res)
        {
            Assert.That(res, Is.EqualTo(FightHelper.ElementFight(element, enemy)));
        }
    }
}
