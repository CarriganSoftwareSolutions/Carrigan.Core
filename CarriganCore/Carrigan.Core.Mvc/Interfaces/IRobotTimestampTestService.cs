namespace Carrigan.Core.Mvc.Interfaces;

public interface IRobotTimestampTestService
{
    string CreateToken();
    bool TryValidateToken(string token, int minSecondsToRespond);
}