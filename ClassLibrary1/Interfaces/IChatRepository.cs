﻿using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface IChatRepository : IDataRepository<Chat>
{
    Chat GetChatById(int id);
}
