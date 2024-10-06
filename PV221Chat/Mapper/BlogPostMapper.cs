using PV221Chat.Core.DataModels;
using PV221Chat.DTO;

namespace PV221Chat.Mapper
{
    public static class BlogPostMapper
    {
        public static BlogPost ToModel(BlogPostDTO dto)
        {
            return new BlogPost
            {
                Title = dto.Title,
                Text = dto.Text,
                CreateAt = dto.CreateAt,
                BlogPageId = dto.BlogPageId
            };
        }
        public static BlogPostDTO ToDTO(BlogPost model)
        {
            return new BlogPostDTO
            {
                Title = model.Title,
                Text = model.Text,
                CreateAt = model.CreateAt,
                BlogPageId = model.BlogPageId,
                BlogPostId = model.BlogPostId
            };
        }
        public static void UpdateModel(BlogPostDTO dto, BlogPost model)
        {
            if (!string.IsNullOrEmpty(dto.Title))
                model.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Text))
                model.Text = dto.Text;
        }
    }
}
