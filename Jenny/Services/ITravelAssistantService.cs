using Jenny.Contracts;

namespace Jenny.Services;

public interface ITravelAssistantService
{
    ChatResponse GetResponse(string message);
}
