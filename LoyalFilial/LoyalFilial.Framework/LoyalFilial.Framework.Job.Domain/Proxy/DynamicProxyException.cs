using System;
using System.Text;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace LoyalFilial.Framework.Job.Domain.Proxy
{


    public class DynamicProxyException : ApplicationException
    {
        private IEnumerable<MetadataConversionError> importErrors = null;
        private IEnumerable<MetadataConversionError> codegenErrors = null;
        private IEnumerable<CompilerError> compilerErrors = null;

        public DynamicProxyException(string message)
            : base(message)
        {
        }

        public DynamicProxyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public IEnumerable<MetadataConversionError> MetadataImportErrors
        {
            get
            {
                return this.importErrors;
            }

            internal set
            {
                this.importErrors = value;
            }
        }

        public IEnumerable<MetadataConversionError> CodeGenerationErrors
        {
            get
            {
                return this.codegenErrors;
            }

            internal set
            {
                this.codegenErrors = value;
            }
        }

        public IEnumerable<CompilerError> CompilationErrors
        {
            get
            {
                return this.compilerErrors;
            }

            internal set
            {
                this.compilerErrors = value;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());

            if (MetadataImportErrors != null)
            {
                builder.AppendLine(Constants.ErrorMessages.Metadata);
                builder.AppendLine(DynamicProxyFactory.ToString(
                            MetadataImportErrors));
            }

            if (CodeGenerationErrors != null)
            {
                builder.AppendLine(Constants.ErrorMessages.CodeGeneration);
                builder.AppendLine(DynamicProxyFactory.ToString(
                            CodeGenerationErrors));
            }

            if (CompilationErrors != null)
            {
                builder.AppendLine(Constants.ErrorMessages.Compilation);
                builder.AppendLine(DynamicProxyFactory.ToString(
                            CompilationErrors));
            }

            return builder.ToString();
        }
    }
}
