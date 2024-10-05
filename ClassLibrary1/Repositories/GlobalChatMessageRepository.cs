using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV221Chat.Core.Repositories
{
    public class GlobalChatMessageRepository : EFRepository<GlobalChatMessage>, IGlobalChatMessageRepository
    {
        public GlobalChatMessageRepository(Pv221chatContext context) : base(context)
        {
        }
    }
}
