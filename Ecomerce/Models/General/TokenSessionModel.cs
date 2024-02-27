﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Ecomerce.Models.General;
//
//    var tokenSessionModel = TokenSessionModel.FromJson(jsonString);

namespace Ecomerce.Models.General
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TokenSessionModel
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("dateExpired")]
        public string DateExpired { get; set; }
    }

    public partial class TokenSessionModel
    {
        public static TokenSessionModel FromJson(string json) => JsonConvert.DeserializeObject<TokenSessionModel>(json, Ecomerce.Models.General.TokenSessionModelConverter.Settings);
    }

    public static class TokenSessionModelSerialize
    {
        public static string ToJson(this TokenSessionModel self) => JsonConvert.SerializeObject(self, Ecomerce.Models.General.TokenSessionModelConverter.Settings);
    }

    internal static class TokenSessionModelConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}