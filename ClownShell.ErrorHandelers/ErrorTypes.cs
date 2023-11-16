using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  namespace ErrorHandelers
    {
        public enum ErrorType
        {
            NotValidAction,
            NotValidType,
            NotValidParameter,
            ExecutionError,
            NotImplemented,
            FATAL,
            VariableAlreadySet,
            InvalidOperator
        }

    }
