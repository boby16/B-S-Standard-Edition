namespace LoyalFilial.Framework.Job.Domain.Proxy
{
    internal class Constants
    {
        internal class ErrorMessages
        {
            internal const string ImportError = "导入元数据时发生了一个错误.";

            internal const string CodeGenerationError = "生成代理代码出错.";

            internal const string CompilationError = "有一个错误的代理.";

            internal const string UnknownContract = "没有找到代理组件.";

            internal const string EndpointNotFound = "与契约相关的端点 {1}:{0} 没有找到.";

            internal const string ProxyTypeNotFound = "实现服务契约{0}没有找到代理.";

            internal const string ProxyCtorNotFound = "构造函数的参数类型不匹配指定的.";

            internal const string ParameterValueMistmatch = "每个参数的值必须指定类型.";

            internal const string MethodNotFound = "{0}方法没有找到.";

            internal const string Metadata = "Metadata Import Errors:";

            internal const string CodeGeneration = "Code Generation Errors:";

            internal const string Compilation = "Compilation Errors:";
        }

        internal class SystemConstants
        {
            internal const string Obj = "obj";
            internal const string ObjType = "objType";
            internal const string Close = "Close";
            internal const string WsdlUri = "wsdlUri";
            internal const string Options = "options";
            internal const string Policy = "Policy";
            internal const string Warning = "Warning : ";
            internal const string Error = "Error：";

            internal const string DynamicProxyFactoryOptions = "DynamicProxyFactoryOptions[";
            internal const string Language = "Language=";
            internal const string FormatMode = ",FormatMode=";
            internal const string CodeModifier = ",CodeModifier=";
            internal const string EndString = "]";
        }
    }
}
