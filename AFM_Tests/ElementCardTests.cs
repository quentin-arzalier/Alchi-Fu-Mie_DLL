﻿using AFM_DLL;
using AFM_DLL.Models.Cards;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFM_Tests
{
    public class ElementCardTests
    {
        [TestCase(Element.ROCK)]
        [TestCase(Element.PAPER)]
        [TestCase(Element.SCISSORS)]
        public void SerializeDeserializeTest(Element elt)
        {
            var card = new ElementCard(elt);
            var json = JsonConvert.SerializeObject(card);
            var jsonCard = JsonConvert.DeserializeObject<ElementCard>(json);
            Assert.That(jsonCard, Is.Not.Null);
            Assert.That(jsonCard.ActiveElement, Is.EqualTo(card.ActiveElement));
        }
    }
}