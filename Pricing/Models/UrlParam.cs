﻿namespace Pricing.Models
{
    public class UrlParam
    {
        public UrlParam(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}