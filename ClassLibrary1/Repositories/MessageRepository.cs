using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;
using System;

namespace PV221Chat.Core.Repositories;

public class MessageRepository : EFRepository<Message>, IMessageRepository
{
    public MessageRepository(Pv221chatContext context) : base(context)
    {
    }

    public async Task<Message> AddDataReturnedMessageAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();

        return message;
    }

    public async Task<List<Message>> GetByChatIdAndSortedAsync(int chatId, int limit)
    {
        var messages = await _context.Messages
                         .Where(m => m.ChatId == chatId && (m.IsDeleted == false || m.IsDeleted == null))
                         .OrderByDescending(m => m.SentAt)
                         .Take(limit)
                         .ToListAsync();

        messages.Reverse();

        return messages;
    }
}
