namespace Carrigan.Core.Base64;

public readonly struct Base64Data
{
    private readonly string _encoded;

    public Base64Data(string base64)
    {
        // Validate that the input is a valid Base64 string.
        try
        {
            Convert.FromBase64String(base64);
        }
        catch (FormatException ex)
        {
            throw new ArgumentException("Invalid Base64 string.", nameof(base64), ex);
        }
        _encoded = base64;
    }

    public Base64Data(byte[] data)
    {
        _encoded = Convert.ToBase64String(data);
    }

    public byte[] ToByteArray() => Convert.FromBase64String(_encoded);

    public override string ToString() => _encoded;

    // Implicit conversion from string to Base64 (validates the input)
    public static implicit operator Base64Data(string base64) => new(base64);

    // Implicit conversion from Base64 to string
    public static implicit operator string(Base64Data base64) => base64._encoded;


    public static bool IsBase64String(string base64)
    {// Check if the length is a multiple of 4
        if (base64.Length % 4 != 0)
            return false;

        try
        {
            // Attempt to convert the string from Base64
            Convert.FromBase64String(base64);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
