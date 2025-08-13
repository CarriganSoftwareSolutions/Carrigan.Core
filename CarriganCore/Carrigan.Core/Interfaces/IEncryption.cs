namespace Carrigan.Core.Interfaces;

public interface IEncryption
{
    string? Decrypt(string? cipherText);
    string? Encrypt(string? plainText);
    int? Version { get; }
    byte[] KeyBytes { get; }
}
