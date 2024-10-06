using PV221Chat.Core.DataModels;
using PV221Chat.DTO;

namespace PV221Chat.Mapper
{
    
    public class GlobalChatMessageMapper
    {
        public static GlobalChatMessageDTO CreateDTO(int userId, string message, string UserName)
        {
            return new GlobalChatMessageDTO
            {
                UserId = userId,
                MessageText = message,
                CreateAt = DateTime.Now,
                SenderName= UserName
            };
        }
        public static GlobalChatMessage ToModel(GlobalChatMessageDTO dto)
        {
            return new GlobalChatMessage
            {
                UserId = dto.UserId,
                MessageText = dto.MessageText,
                CreateAt = dto.CreateAt
            };
        }
        public static GlobalChatMessageDTO ToDTO(GlobalChatMessage model, string UserName)
        {
            return new GlobalChatMessageDTO
            {
                UserId = model.UserId,
                MessageText = model.MessageText,
                CreateAt = model.CreateAt,
                MessageGcId = model.MessageGcId,
                SenderName = UserName
            };
        }
        public static void UpdateModel(GlobalChatMessageDTO dto, GlobalChatMessage model)
        {
            if (!string.IsNullOrEmpty(dto.MessageText))
                model.MessageText = dto.MessageText;
        }
    }
}
