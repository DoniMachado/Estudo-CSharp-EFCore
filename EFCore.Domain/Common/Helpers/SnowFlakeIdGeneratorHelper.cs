using IdGen;

namespace EFCore.Domain.Common.Helpers;

public static class SnowFlakeIdGeneratorHelper
{
    private static readonly IdGenerator _idGenerator;



    static SnowFlakeIdGeneratorHelper()
    {
        var epoch = new DateTime(2024, 1, 1, 0,0,0,DateTimeKind.Utc);
        var structure = new IdStructure(45, 2, 16);
        var options = new IdGeneratorOptions(structure,new DefaultTimeSource(epoch));

        _idGenerator = new IdGenerator(0,options);        
    }

    public static long CreateId()
        => _idGenerator.CreateId();
}
