using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace Import.MT.Tests.Helpers;

public static class IOHelper
{
    public static PipeReader ToPipeReader(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        var sequence = new ReadOnlySequence<byte>(bytes);
        return PipeReader.Create(sequence);
    }
}