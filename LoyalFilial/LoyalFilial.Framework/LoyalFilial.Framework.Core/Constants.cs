namespace LoyalFilial.Framework.Core
{
    public class Constants
    {
        #region Config

        public const string Config_ConfigConnection = "ConfigConnection";
        public const string Config_DevFxId = "Id";
        public const string Config_SelectSql = @"IF EXISTS(SELECT config FROM dbo.Workflow_DevFx_Config WHERE DevFxId='{0}' AND State=1)
                                                    BEGIN
	                                                    SELECT config FROM dbo.Workflow_DevFx_Config WHERE DevFxId='{0}' AND State=1
                                                    END
                                                    ELSE
                                                    BEGIN
	                                                    SELECT config FROM dbo.Workflow_DevFx_Config WHERE DevFxId='Default' AND State=1
                                                    END
                                                    ";

        public const string Config_Module = "Module";
        public const string Config_Module_Name = "Name";
        public const string Config_Module_Type = "type";

        public const string Config_Module_Execution_Web = "{0}\\bin\\{1}";
        public const string Config_Module_Execution = "{0}\\{1}";

        public const string Config_Module_Assembly_Web = "{0}\\bin\\{1}.dll";
        public const string Config_Module_Assembly = "{0}\\{1}.dll";

        public const string Config_Database_DataProvider_Name = "DataProvider";
        public const string Config_Database_ConnectionStrings = "ConnectionStrings";
        public const string Config_Database_ConnectionString = "ConnectionString";

        public const string Config_Node_Key = "Key";

        public const string Config_Node_IsDefault = "Default";

        public const string Config_Node_MaxPools = "MaxPools";

        #endregion

        #region Module Name

        public const string Module_Log_Name = "Log";

        #endregion

        #region Log Para

        public const string Log_Directory = "Directory";
        public const string Log_MaxSize = "MaxSize";
        public const string Log_MaxSize_Appendix = "mb";
        public const string Log_Level = "Level";
        public const string Log_Level_Name = "Name";
        public const string Log_Level_Folder = "Folder";

        #endregion

        #region Error Message

        public const string Error_NoConfigForModule = "No config info for module {0}.";

        public const string Error_NoConfigInfo = "There is no devfx config info.";

        public const string Error_NeedConfigMananger = "There is no config manager.";

        public const string Error_Log_InitConfigFailed = "Module Log config failed.";

        public const string Error_Data_InitConfigFailed = "Module Data config failed.";
        
        public const string Error_TypeHelper_CreatObject = "Create object failed in TypeHelper.";
              
        #endregion

        #region DevFx refresh

        public const string DevFx_Refresh_ModulePara = "Module";
        public const string DevFx_Refresh_ModuleAll = "All";

        #endregion

        #region Exception list

        public const string DevFx_ExceptionList_Tip = "Framework init exception list:";

        #endregion
    }
}
