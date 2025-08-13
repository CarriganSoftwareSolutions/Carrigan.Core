namespace Carrigan.Core.Extensions;

public static class ByteExtensions
{
    public static string FormattedSize(this byte[] byteArray)
    {
        if (byteArray == null)
        {
            return $"{0:0.##} {"bytes"}";
        }
        else
        {
            long byteCount = byteArray.Length;
            string[] sizeUnits = ["bytes", "KB", "MB", "GB", "TB", "PB", "EB"];
            double size = byteCount;
            int unitIndex = 0;

            while (size >= 1024 && unitIndex < sizeUnits.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }

            return $"{size:0.##} {sizeUnits[unitIndex]}";
        }
    }

    public static byte[] BlockAppend(this byte[]? first, byte[]? second)
    {
        first ??= [];
        second ??= [];

        byte[] result = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, result, 0, first.Length);
        Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
        return result;
    }
}
