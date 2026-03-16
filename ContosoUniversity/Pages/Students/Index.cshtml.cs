using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages_Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
            
        }
        
        public string NameSort { get; set; } = "";
        public string DateSort { get; set; } = "";
        public string AgeSort { get; set; } = "";

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            AgeSort = sortOrder == "Age" ? "age_desc" : "Age";

            IQueryable<Student> students = _context.Students;

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                case "Age":
                    students = students.OrderBy(s => s.Age);
                    break;
                case "age_desc":
                    students = students.OrderByDescending(s => s.Age);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            Student = await students.ToListAsync();
        }
    }
}