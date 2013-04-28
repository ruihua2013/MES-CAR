namespace NpMis.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [ApplicationScopedSetting, SpecialSetting(SpecialSetting.ConnectionString), DefaultSettingValue("Data Source=LOCALHOST;Initial Catalog=TC_VPMIS;Persist Security Info=True;User ID=sa;Password=Tcco"), DebuggerNonUserCode]
        public string TC_VPMISConnectionString
        {
            get
            {
                return (string) this["TC_VPMISConnectionString"];
            }
        }
    }
}

