using AFM_DLL.Models.Unlockables;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;

namespace AFM_DLL.Converters
{
    /// <summary>
    ///     Permet la désérialisation d'un dos de carte
    /// </summary>
    public class CardBackConverter : CustomCreationConverter<CardBack>
    {
        private CardBackType _type;

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobj = JToken.ReadFrom(reader);
            _type = jobj["BackType"].ToObject<CardBackType>();
            return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
        }

        /// <inheritdoc/>
        public override CardBack Create(Type objectType)
        {
            return CardBack.FromType(_type);
        }
    }
}
