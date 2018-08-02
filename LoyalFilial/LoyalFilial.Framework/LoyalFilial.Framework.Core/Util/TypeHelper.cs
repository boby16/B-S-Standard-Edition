using System;
using System.Reflection;

namespace LoyalFilial.Framework.Core.Util
{
    public abstract class TypeHelper
    {
        #region Fields

        internal static Func<MethodInfo, object, object[], object> MethodInvoker = new Func<MethodInfo, object, object[], object>(TypeHelper.MethodInvoke);
        internal static object MethodInvokerLockObject = new object();
        internal static object ConstructorInvokerLockObject = new object();

        #endregion

        internal TypeHelper()
        {
        }

        //public static string GetExecutionPath()
        //{
        //    return System.Web.HttpContext.Current != null ? Constants.Config_Module_Execution_Path : Constants.Config_Module_Execution;
        //}

        //public static string GetExecutionAssemblyConfig()
        //{
        //    return System.Web.HttpContext.Current != null ? Constants.Config_Module_Assembly_Web : Constants.Config_Module_Assembly;
        //}

        public static Assembly AssemblyLoad(string assemblyName, bool throwOnError)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(assemblyName);
            }
            catch (Exception e)
            {
                if ((e is ArgumentNullException) || throwOnError)
                {
                    throw;
                }
                return assembly;
            }
            return assembly;
        }

        public static Type FindType(Assembly assembly, string typeName)
        {
            Type type = null;
            if (assembly == null) return type;
            type = assembly.GetType(typeName, false);
            if (type != null)
            {
                return type;
            }

            return type;
        }

        public static object CreateObject(Type type, Type[] parameterTypes, object[] parameterValues, bool throwOnError)
        {
            if (type == null) return null;
            object createdObject = null;
            if (parameterTypes == null)
            {
                parameterTypes = new Type[] { };
            }
            try
            {
                var constructor = type.GetConstructor(parameterTypes);
                if (constructor == null)
                {
                    createdObject = Activator.CreateInstance(type, BindingFlags.CreateInstance | (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance)), null, parameterValues, null);
                }
                else
                {
                    lock (ConstructorInvokerLockObject)
                    {
                        createdObject = ConstructorInvoke(constructor, parameterValues);
                    }
                }
                return createdObject;
            }
            catch (Exception ex)
            {
                if (throwOnError)
                    throw new Exception(Constants.Error_TypeHelper_CreatObject, ex);
                else
                    return null;
            }
        }

        /// <summary>
        /// 创建空构造函数的对象实例
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeName"></param>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public static object CreateObject(string assembly, string typeName, bool throwOnError)
        {
            return CreateObject(FindType(AssemblyLoad(assembly, throwOnError), typeName), null, null, throwOnError);
        }

        static object ConstructorInvoke(ConstructorInfo ctor, object[] parameterValues)
        {
            return ctor.Invoke(parameterValues);
        }

        #region Invoke method

        private static object MethodInvoke(MethodInfo method, object instance, object[] parameterValues)
        {
            return method.Invoke(instance, parameterValues);
        }

        public static bool TryInvoke(object obj, string methodName, out object returnValue, bool throwOnError, params object[] parameters)
        {
            returnValue = null;
            Type type;
            MethodInfo method;
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            if (obj is Type)
            {
                type = (Type)obj;
                flags |= BindingFlags.Static;
            }
            else
            {
                type = obj.GetType();
            }

            method = type.GetMethod(methodName, flags);

            if (method != null)
            {
                try
                {
                    lock (MethodInvokerLockObject)
                    {
                        returnValue = MethodInvoker(method, obj, parameters);
                    }
                    return true;
                }
                catch
                {
                    if (throwOnError)
                    {
                        throw;
                    }
                }
            }
            return false;
        }

        public static object Invoke(Type type, string typeNameWithNamespace, string methodName, params object[] parameters)
        {
            object returnValue;
            TryInvoke(type, methodName, out returnValue, true, parameters);
            return returnValue;
        }

        #endregion
    }
}
