using PV221Chat.Core.DataModels;

namespace PV221Chat.DAL.Interfaces
{
    public interface IGlobalChatMessageRepository : IDataRepository<GlobalChatMessage>
    {
        Task<GlobalChatMessage> AddDataReturnedMessageAsync(GlobalChatMessage message);
    }
}
