using PV221Chat.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV221Chat.Core.Interfaces
{
    public interface IGlobalChatMessageRepository : IDataRepository<GlobalChatMessage>
    {
        Task<GlobalChatMessage> AddDataReturnedMessageAsync(GlobalChatMessage message);
    }
}
