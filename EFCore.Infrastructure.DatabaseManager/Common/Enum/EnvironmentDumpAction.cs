using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Infrastructure.DatabaseManager.Common.Enum;

public enum EnvironmentDumpAction
{
    Update,
    Dump,
    Reset,
    None
}
