using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Domain.Common.Exceptions;

public class DomainException(string messageError): Exception(messageError)
{
    public static void When(bool condition,string field, string messageError)
    {
        if(condition)
            throw new DomainException($"{field} {messageError}");
    }

    public static void When(bool condition, string messageError)
    {
        if (condition)
            throw new DomainException(messageError);
    }
}
