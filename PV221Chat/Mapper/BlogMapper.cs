using PV221Chat.Core.DataModels;
using PV221Chat.DTO;

namespace PV221Chat.Mapper
{
    public static class BlogMapper
    {
        public static BlogPage ToModel(CreateBlogPageDTO dto)
        {
            return new BlogPage
            {
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = dto.AuthorId,
                Type = dto.Type,
                CreatedAt = DateTime.UtcNow 
            };
        }

        public static BlogPageDTO ToDTO(BlogPage model)
        {
            return new BlogPageDTO
            {
                BlogId = model.BlogId,
                Title = model.Title,
                Content = model.Content,
                AuthorId = model.AuthorId,
                Type = model.Type,
                CreatedAt = model.CreatedAt
            };
        }

        public static void UpdateModel(UpdateBlogPageDTO dto, BlogPage model)
        {
            if (!string.IsNullOrEmpty(dto.Title))
                model.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Content))
                model.Content = dto.Content;

            if (!string.IsNullOrEmpty(dto.Type))
                model.Type = dto.Type;
        }

        public static BlogPage ToModel(BlogPageDTO dto)
        {
            return new BlogPage
            {
                BlogId = dto.BlogId,
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = dto.AuthorId,
                Type = dto.Type,
                CreatedAt = dto.CreatedAt
            };
        }
    }
}
