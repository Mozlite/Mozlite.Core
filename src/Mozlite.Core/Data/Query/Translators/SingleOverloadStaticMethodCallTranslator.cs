using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mozlite.Data.Query.Expressions;

namespace Mozlite.Data.Query.Translators
{
    /// <summary>
    /// �������ؾ�̬��������ת�������ࡣ
    /// </summary>
    public abstract class SingleOverloadStaticMethodCallTranslator : IMethodCallTranslator
    {
        private readonly Type _declaringType;
        private readonly string _clrMethodName;
        private readonly string _sqlFunctionName;

        /// <summary>
        /// ��ʼ����<see cref="SingleOverloadStaticMethodCallTranslator"/>��
        /// </summary>
        /// <param name="declaringType">�������͡�</param>
        /// <param name="clrMethodName">CLR�������ơ�</param>
        /// <param name="sqlFunctionName">SQL�������ơ�</param>
        public SingleOverloadStaticMethodCallTranslator([NotNull] Type declaringType, [NotNull] string clrMethodName, [NotNull] string sqlFunctionName)
        {
            _declaringType = declaringType;
            _clrMethodName = clrMethodName;
            _sqlFunctionName = sqlFunctionName;
        }

        /// <summary>
        /// ת������ʽ��
        /// </summary>
        /// <param name="methodCallExpression">�������ñ���ʽ��</param>
        /// <returns>����ת����ı���ʽ��</returns>
        public virtual Expression Translate([NotNull] MethodCallExpression methodCallExpression)
        {
            var methodInfo = _declaringType.GetTypeInfo().GetDeclaredMethods(_clrMethodName).SingleOrDefault();
            if (methodInfo == methodCallExpression.Method)
            {
                return new SqlFunctionExpression(_sqlFunctionName, methodCallExpression.Type, methodCallExpression.Arguments);
            }

            return null;
        }
    }
}