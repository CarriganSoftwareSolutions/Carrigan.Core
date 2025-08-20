namespace Carrigan.Core.Interfaces;

//Ignore spelling: Decryptors Decryptor

/// <summary>
/// The correct context in which to think of this interface is likely more like a key chain.
/// In retrospect, IDecryptorsKeyChain might have made a more descriptive name, but that also it a bit to wordy even for me.
/// The ideas Carrigan.SqlTools is to use this with key rotation. Each Entity Framework model that you wish to encrypt should
/// have two data annotations: <see cref="Carrigan.Core.Attributes.EncryptedAttribute"/> and <see cref="Carrigan.Core.Attributes.KeyVersionAttribute"/>
/// <see cref="Carrigan.Core.Attributes.EncryptedAttribute"/> indicates that a property is Encrypted in the database.
/// <see cref="Carrigan.Core.Attributes.KeyVersionAttribute"/> indicates which decryption key to use to decrypt a given record.
/// When your server launches, you build a IDecryptors "key chain" to look up which  <see cref="IEncryption"/> instance to use to decrypt a given record.
/// Of course there are tools in Carrigan.SqlTools to do the decryption for you as it load records from the database, all you have to do is provide the key chain.
/// </summary>
public interface IDecryptors
{
    /// <summary>
    /// get an <see cref="IEncryption"/> "decryptor" by the key number
    /// </summary>
    /// <param name="key">an <see cref="int"/> that represents the lookup for the <see cref="IEncryption"/> "decryptor" to get. </param>
    /// <returns>an <see cref="int"/> that represented by the <paramref name="key"/></returns>
    public IEncryption Decryptor(int key);
    /// <summary>
    /// This is a public getter to get an <see cref="int"/> for all the available keys. Use this to safely check if the key version you want is in the key chain.
    /// </summary>
    public IEnumerable<int> Keys { get; }
}
