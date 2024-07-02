using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EFCore.Application.Common.Extensions;

public static class JsonSerializeExtensions
{

    private const int SizeThreshholdBytes = 64 * 1024; //64KB

    public static string SerializeWithCompression<T>(T value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(value, options);

        var bytes = Encoding.UTF8.GetBytes(json);

        if (bytes.Length <= SizeThreshholdBytes)
            return json;


        using (var output = new MemoryStream())
        {
            using (var compression = new GZipStream(output, CompressionMode.Compress))
            {
                compression.Write(bytes,0, bytes.Length);
            }

            var compressedBytes = output.ToArray();

            return Convert.ToBase64String(compressedBytes);
        }
    }
}
