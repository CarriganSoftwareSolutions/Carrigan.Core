namespace Carrigan.Core.Interfaces;


/// <summary>
/// This is a simple interface to define a encryption class.
/// This is part of the Carrigan.SqlTools library,
/// as well as a closed sourced encryption library I use in conjunction with the Carrigan.SqlTools library.
/// To use the encryption feature in Carrigan.SqlTools, you are expected to provide your own encryption class using this interface.
/// </summary>
public interface IEncryption
{
    /// <summary>
    /// This method does the decryption.
    /// </summary>
    /// <param name="cipherText">the encrypted cipher text</param>
    /// <returns>the decrypted plain text</returns>
    string? Decrypt(string? cipherText);

    /// <summary>
    /// This method does the encryption.
    /// </summary>
    /// <param name="plainText">The plain text to be encrypted</param>
    /// <returns>the encrypted cypher text</returns>
    string? Encrypt(string? plainText);

    /// <summary>
    /// This is a getter to determine which version of your encryption key to use.
    /// The idea is that your encryption utility class must account for key rotation, 
    /// and each key you use should have a corresponding version as a <see cref="int?"/>
    /// Leaving this null will cause an exception to be thrown in Carrigan.SqlTools
    /// I used the nullable int to enforce null reference warnings, and enforce proper null reference checking.
    /// </summary>
    int? Version { get; }

    /// <summary>
    /// This is a way to access the current key being used by Encryptor
    /// </summary>
    byte[] KeyBytes { get; }
}
