﻿using Abp.Modules;
using Abp.UI;
using Abp.Zero;
using System;
using Abp.Web;

namespace Cinotam.AbpModuleZero.Localization.Helpers
{
    public static class XmlLocations
    {

        private const string XmlLocationModuleZero = "Cinotam.AbpModuleZero.Localization.SourceZero";
        private const string XmlAbp = "Abp.Localization.Sources.AbpXmlSource";
        private const string XmlAbpZero = "Abp.Zero.Zero.Localization.Source";
        private const string XmlAbpWeb = "Abp.Web.Common.Web.Localization.AbpWebXmlSource";
        public static LangLocalization GetXmlLocationBySourceName(string source)
        {
            switch (source)
            {
                case "AbpModuleZero":
                    return new LangLocalization() { Assembly = typeof(AbpModuleZeroCoreModule), LocalizationNameSpace = XmlLocationModuleZero };
                case "Abp":
                    return new LangLocalization() { Assembly = typeof(AbpModule), LocalizationNameSpace = XmlAbp };
                case "AbpZero":
                    return new LangLocalization() { Assembly = typeof(AbpZeroCoreModule), LocalizationNameSpace = XmlAbpZero };
                case "AbpWeb":
                    return new LangLocalization() { Assembly = typeof(AbpWebCommonModule), LocalizationNameSpace = XmlAbpWeb };
                default:
                    throw new ArgumentOutOfRangeException(nameof(source));
            }
        }

        public class LangLocalization
        {
            public string LocalizationNameSpace { get; set; }
            public Type Assembly { get; set; }
        }
    }

}
