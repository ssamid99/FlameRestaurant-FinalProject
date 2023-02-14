using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Business.BlogPostModule;
using FlameRestaurant.Domain.Models.DbContexts;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public BlogPostsController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [Authorize("admin.blogposts.index")]
        public async Task<IActionResult> Index(BlogPostGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [Authorize(Policy = "admin.blogposts.details")]
        public async Task<IActionResult> Details(BlogPostGetSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [Authorize("admin.blogposts.create")]
        public IActionResult Create()
        {
            return View();
        }

        //POST: Admin/BlogPosts/Create
       // To protect from overposting attacks, enable the specific properties you want to bind to.
       // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.blogposts.create")]
        public async Task<IActionResult> Create(BlogPostPostCommand command)
        {
            if (command.Image == null)
            {
                ModelState.AddModelError("ImagePath", "Shekil Gonderilmelidir");
            }

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                if (response.Error == false)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(command);
        }

        [Authorize("admin.blogposts.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts
                .FirstOrDefaultAsync(bp => bp.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            var editCommand = new BlogPostPutCommand();
            editCommand.Id = blogPost.Id;
            editCommand.Title = blogPost.Title;
            editCommand.Body = blogPost.Body;
            editCommand.ImagePath = blogPost.ImagePath;

            return View(blogPost);
        }

        // POST: Admin/BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.blogposts.edit")]
        public async Task<IActionResult> Edit(BlogPostPutCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }


        // POST: Admin/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.blogposts.delete")]
        public async Task<IActionResult> DeleteConfirmed(BlogPostRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Publish")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.blogposts.publish")]
        public async Task<IActionResult> PublishConfirmed(BlogPostPublishCommand command)
        {
            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        [Authorize("admin.blogposts.deletedposts")]
        public async Task<IActionResult> DeletedPosts(BlogPostGetAllDeletedQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [HttpPost, ActionName("Back")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.blogposts.back")]
        public async Task<IActionResult> BackToPosts(BlogPostRemoveBackCommand command)
        {
            var response = await mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeletedPostDetails(BlogPostGetSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost, ActionName("Clear")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.blogposts.clear")]
        public async Task<IActionResult> ClearDeletedPosts(BlogPostClearCommand command)
        {

            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("DeleteComment")]
        [Authorize("admin.blogposts.deletecomment")]
        public async Task<IActionResult> DeleteComment(BlogPostCommentRemoveCommand command)
        {
            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

    }
}
