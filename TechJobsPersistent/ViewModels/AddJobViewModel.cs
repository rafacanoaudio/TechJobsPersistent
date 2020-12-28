using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        //[Required(ErrorMessage = "Job is required")]
        public string Name { get; set; }

        public int EmployerId { get; set; }

        public List<SelectListItem> Employers { get; set; }

        public List<Skill> Skills { get; set; }

        public AddJobViewModel()
        {
        }

        public AddJobViewModel(List<Employer> employers, List<Skill> skills)
        {
            Employers = new List<SelectListItem>();
            Skills = new List<Skill>();

            foreach(Employer thisEmployer in employers)
            {
                Employers.Add(new SelectListItem
                {
                    Value = thisEmployer.Id.ToString(),
                    Text = thisEmployer.Name
                });
            }

            Skills = skills;


            //foreach(Skill thisSkill in skills)
            //{
            //    Skills.Add(new SelectListItem
            //    {
            //        Value = thisSkill.Id.ToString(),
            //        Text = thisSkill.Name
            //    });
            //}
        } 
    }

}
