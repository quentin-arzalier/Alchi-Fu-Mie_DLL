using AFM_DLL.Models.Cards;
using AFM_DLL.Models.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;

namespace AFM_DLL.Converters
{
    /// <summary>
    ///     Permet la désérialisation d'une carte sortilège
    /// </summary>
    public class SpellCardConverter : CustomCreationConverter<SpellCard>
    {
        private SpellType _type;

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobj = JToken.ReadFrom(reader);
            _type = jobj["SpellType"].ToObject<SpellType>();
            return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
        }

        /// <inheritdoc/>
        public override SpellCard Create(Type objectType)
        {
            return SpellCard.FromType(_type);
        }
    }
}
