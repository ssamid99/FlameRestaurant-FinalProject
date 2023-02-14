using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.BlogPostModule
{
    public class BlogPostCommentPostCommand : IRequest<BlogPostComment>
    {
        public int? commentId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "PostId uygun deyil")]
        public int postId { get; set; }
        [Required]
        public string comment { get; set; }
        public class BlogPostCommentPostCommandHandler : IRequestHandler<BlogPostCommentPostCommand, BlogPostComment>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public BlogPostCommentPostCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<BlogPostComment> Handle(BlogPostCommentPostCommand request, CancellationToken cancellationToken)
            {
                if (!ctx.ActionContext.ModelState.IsValid)
                {
                    throw new Exception(ctx.ActionContext.ModelState.GetErrors().FirstOrDefault().Message);
                }
                var post = await db.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == request.postId);
                if (post == null)
                {
                    throw new Exception("Post movcud deyil");
                }
                var commentModel = new BlogPostComment
                {
                    BlogPostId = request.postId,
                    Text = request.comment,
                    CreatedByUserId = ctx.GetCurrentUserId()
                };
                if (request.commentId.HasValue && await db.BlogPostComments.AnyAsync(bpc => bpc.Id == request.commentId))
                {
                    commentModel.ParentId = request.commentId;
                }
                await db.BlogPostComments.AddAsync(commentModel, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                commentModel = await db.BlogPostComments.Include(bpc => bpc.CreatedByUser)
                    .Include(bpc => bpc.Parent)
                   .FirstOrDefaultAsync(bpc => bpc.Id == commentModel.Id);
                return commentModel;
            }
        }
    }
}
