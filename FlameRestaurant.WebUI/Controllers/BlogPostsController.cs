using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using FlameRestaurant.Application.AppCode.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using FlameRestaurant.Domain.Business.BlogPostModule;
using Microsoft.AspNetCore.Authorization;

namespace FlameRestaurant.WebUI.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public BlogPostsController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        [AllowAnonymous]
        // GET: BlogPosts
        public async Task<IActionResult> Index(BlogPostGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PostBody", response);
            }
            return View(response);
        }
        [AllowAnonymous]
        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(BlogPostGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost]
        [ActionName("Add")]
        [Authorize]
        public async Task<IActionResult> Add(BlogPostCommentPostCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return PartialView("_Comment", response);
        }

    }
}
