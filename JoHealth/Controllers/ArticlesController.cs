using Microsoft.AspNetCore.Mvc;
using JoHealth.Models;
using System;
using System.Collections.Generic;

namespace JoHealth.Controllers
{
    public class ArticlesController : Controller
    {
        public IActionResult Index()
        {
            // Pre-set articles
            var articles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "The 25 Healthiest Fruits You Can Eat",
                    Body = "This article discusses the healthiest fruits you can eat and their benefits...",
                    ImageUrl = "/img/Health.jpg",
                    PublishDate = new DateTime(2023, 6, 10),
                    Author = "John Doe"
                },
                new Article
                {
                    Id = 2,
                    Title = "The Impact of COVID-19 on Healthcare Systems",
                    Body = "This article examines how the COVID-19 pandemic has impacted global healthcare systems...",
                    ImageUrl = "/img/Health.jpg",
                    PublishDate = new DateTime(2023, 7, 10),
                    Author = "Jane Smith"
                }
            };

            return View(articles);
        }

        public IActionResult Details(int id)
        {
            // Same pre-set articles for Details page
            var articles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "The 25 Healthiest Fruits You Can Eat",
                    Body = "This article discusses the healthiest fruits you can eat and their benefits...",
                    ImageUrl = "/img/Health.jpg",
                    PublishDate = new DateTime(2023, 6, 10),
                    Author = "John Doe"
                },
                new Article
                {
                    Id = 2,
                    Title = "The Impact of COVID-19 on Healthcare Systems",
                    Body = "This article examines how the COVID-19 pandemic has impacted global healthcare systems...",
                    ImageUrl = "/img/Health.jpg",
                    PublishDate = new DateTime(2023, 7, 10),
                    Author = "Jane Smith"
                }
            };

            // Find the article by ID
            var article = articles.Find(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
    }
}
