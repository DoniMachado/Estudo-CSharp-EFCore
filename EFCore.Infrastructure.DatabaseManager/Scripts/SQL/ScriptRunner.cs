using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Infrastructure.DatabaseManager.Scripts.SQL;

public class ScriptRunner
{
    private readonly IConfiguration _configuration;

    public ScriptRunner(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
