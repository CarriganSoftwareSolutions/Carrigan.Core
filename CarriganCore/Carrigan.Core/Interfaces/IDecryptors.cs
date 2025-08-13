namespace Carrigan.Core.Interfaces;

public interface IDecryptors
{
    public IEncryption Decryptor(int key);
    public IEnumerable<int> Keys { get; }
}
