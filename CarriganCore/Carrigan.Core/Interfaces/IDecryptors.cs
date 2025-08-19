namespace Carrigan.Core.Interfaces;

//Ignore spelling: Decryptors Decryptor

public interface IDecryptors
{
    public IEncryption Decryptor(int key);
    public IEnumerable<int> Keys { get; }
}
