﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            AddJobViewModel addJobViewModel = new AddJobViewModel(context.Employers.ToList() ,context.Skills.ToList());

            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = context.Employers.Find(addJobViewModel.EmployerId),
                   
                };
                foreach (var addedSkill in selectedSkills)
                {
                    JobSkill currentJobSkill = new JobSkill
                    {
                        Job = newJob,
                        JobId = newJob.Id,
                        Skill = context.Skills.Find(int.Parse(addedSkill)),
                        SkillId = int.Parse(addedSkill)
                    };

                    context.JobSkills.Add(currentJobSkill);
                }

                context.Jobs.Add(newJob);
                context.SaveChanges();

                return Redirect("Index");
            }

            List<Employer> employer = context.Employers.ToList();
            List<Skill> skill = context.Skills.ToList();
            addJobViewModel = new AddJobViewModel(employer, skill);


            return View("AddJob", addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
