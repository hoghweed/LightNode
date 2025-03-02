﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace LightNode.Formatter
{
    public class JsonNetContentFormatter : LightNode.Formatter.ContentFormatterBase
    {
        readonly JsonSerializer serializer;

        public JsonNetContentFormatter(string mediaType = "application/json", string ext = "json")
            : this(new JsonSerializer(), mediaType, ext)
        {
        }

        public JsonNetContentFormatter(JsonSerializer serializer, string mediaType = "application/json", string ext = "json")
            : this(serializer, System.Text.Encoding.UTF8, mediaType, ext)
        {
        }

        public JsonNetContentFormatter(Encoding encoding, string mediaType = "application/json", string ext = "json")
            : this(new JsonSerializer(), encoding, mediaType, ext)
        {
        }

        public JsonNetContentFormatter(JsonSerializer serializer, Encoding encoding, string mediaType = "application/json", string ext = "json")
            : base(mediaType, ext, encoding)
        {
            this.serializer = serializer;
        }

        public override void Serialize(System.IO.Stream stream, object obj)
        {
            using (var sw = new StreamWriter(stream, Encoding ?? System.Text.Encoding.UTF8))
            {
                serializer.Serialize(sw, obj);
            }
        }

        public override object Deserialize(Type type, System.IO.Stream stream)
        {
            using (var sr = new StreamReader(stream, Encoding ?? System.Text.Encoding.UTF8))
            {
                return serializer.Deserialize(sr, type);
            }
        }
    }
}