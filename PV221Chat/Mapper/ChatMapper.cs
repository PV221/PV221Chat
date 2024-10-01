using PV221Chat.Core.DataModels;
using PV221Chat.DTO;

namespace PV221Chat.Mapper
{
    public static class ChatMapper
    {
        public static Chat ToModel(CreateChatDTO dto)
        {
            return new Chat
            {
                ChatName = dto.ChatName,
                ChatType = dto.ChatType,
                IsOpen = dto.IsOpen,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static ChatDTO ToDTO(Chat model, int unreadMessagesCount, string? unreadMessagesText)
        {
            return new ChatDTO
            {
                ChatId = model.ChatId,
                ChatName = model.ChatName,
                ChatType = model.ChatType,
                IsOpen = model.IsOpen,
                CreatedAt = model.CreatedAt,
                HasUnreadMessages = unreadMessagesCount > 0,
                CountUnreadMessages = unreadMessagesCount,
                TextUnreadMessages = unreadMessagesText
            };
        }

        public static void UpdateModel(UpdateChatDTO dto, Chat model)
        {
            if (!string.IsNullOrEmpty(dto.ChatName))
                model.ChatName = dto.ChatName;

            if (!string.IsNullOrEmpty(dto.ChatType))
                model.ChatType = dto.ChatType;

            if (dto.IsOpen.HasValue)
                model.IsOpen = dto.IsOpen.Value;
        }

        public static Chat ToModel(ChatDTO dto)
        {
            return new Chat
            {
                ChatId = dto.ChatId,
                ChatName = dto.ChatName,
                ChatType = dto.ChatType,
                IsOpen = dto.IsOpen,
                CreatedAt = dto.CreatedAt
            };
        }
    }
}