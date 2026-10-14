using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using lab_7.Models;

namespace lab_7.Controllers
{
    public class FeedbackController : Controller
    {
        private static readonly List<Feedback> feedbackList =
            new List<Feedback>
            {
                new Feedback
                {
                    FeedbackId = 1,
                    Name = "Rahul Sharma",
                    Email = "rahul@gmail.com",
                    Rating = 5,
                    Category = "Website",
                    Comment = "The website is simple and easy to use.",
                    SubmittedOn = DateTime.Now.AddDays(-2)
                },

                new Feedback
                {
                    FeedbackId = 2,
                    Name = "Priya Patel",
                    Email = "priya@gmail.com",
                    Rating = 4,
                    Category = "Service",
                    Comment = "Good service and quick response.",
                    SubmittedOn = DateTime.Now.AddDays(-1)
                }
            };

        // Display all feedback
        public IActionResult Index()
        {
            var feedbacks = feedbackList
                .OrderByDescending(f => f.FeedbackId)
                .ToList();

            return View(feedbacks);
        }

        // Display feedback form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Submit feedback
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return View(feedback);
            }

            feedback.FeedbackId = feedbackList.Any()
                ? feedbackList.Max(f => f.FeedbackId) + 1
                : 1;

            feedback.SubmittedOn = DateTime.Now;

            feedbackList.Add(feedback);

            return RedirectToAction("Index");
        }
    }
}