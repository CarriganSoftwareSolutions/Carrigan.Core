namespace Carrigan.Core.Interfaces;

public interface INonceGenerator
{
    public byte[] GenerateNonce();
}
