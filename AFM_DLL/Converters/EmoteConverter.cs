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
    public class EmoteConverter : CustomCreationConverter<Emote>
    {
        private EmoteType _type;

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobj = JToken.ReadFrom(reader);
            _type = jobj["EmoteType"].ToObject<EmoteType>();
            return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
        }

        /// <inheritdoc/>
        public override Emote Create(Type objectType)
        {
            return Emote.FromType(_type);
        }
    }
}
