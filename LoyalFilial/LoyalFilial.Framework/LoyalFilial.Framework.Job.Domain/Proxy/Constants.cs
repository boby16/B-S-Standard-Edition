namespace LoyalFilial.Framework.Job.Domain.Proxy
{
    internal class Constants
    {
        internal class ErrorMessages
        {
            internal const string ImportError = "����Ԫ����ʱ������һ������.";

            internal const string CodeGenerationError = "���ɴ���������.";

            internal const string CompilationError = "��һ������Ĵ���.";

            internal const string UnknownContract = "û���ҵ��������.";

            internal const string EndpointNotFound = "����Լ��صĶ˵� {1}:{0} û���ҵ�.";

            internal const string ProxyTypeNotFound = "ʵ�ַ�����Լ{0}û���ҵ�����.";

            internal const string ProxyCtorNotFound = "���캯���Ĳ������Ͳ�ƥ��ָ����.";

            internal const string ParameterValueMistmatch = "ÿ��������ֵ����ָ������.";

            internal const string MethodNotFound = "{0}����û���ҵ�.";

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
            internal const string Error = "Error��";

            internal const string DynamicProxyFactoryOptions = "DynamicProxyFactoryOptions[";
            internal const string Language = "Language=";
            internal const string FormatMode = ",FormatMode=";
            internal const string CodeModifier = ",CodeModifier=";
            internal const string EndString = "]";
        }
    }
}
