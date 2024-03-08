
using EducationPortal.Application.Services;
using EducationPortal.Domain.Entities;
using EducationPortal.Domain.Entities.Materials;
using EducationPortal.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Services
{
    public class DisplayService
    {
        public ICourseService CourseService;
        public IUserService UserService;
        public IMaterialService MaterialService;
        public ISkillService SkillService;
        public DisplayService(ICourseService courseService, IUserService userService, IMaterialService materialService, ISkillService skillService)
        {
            CourseService = courseService;
            UserService = userService;
            MaterialService = materialService;
            SkillService = skillService;
        }
        public async Task DisplayCoursesAsync()
        {
            Console.WriteLine("Available for learning courses:");
            var coursesList = await CourseService.GetAllAsync();
            var activeCourses = coursesList.Where(course => course.CourseMaterials.Count > 0 && course.CourseSkills.Count > 0).ToList();
            var draftCourses = coursesList.Where(course => course.CourseMaterials.Count == 0 || course.CourseSkills.Count == 0).ToList();
            if (activeCourses.Count == 0)
                Console.WriteLine("No courses.");
            else
            {
                foreach (var course in activeCourses)
                {
                    Console.WriteLine($"ID - {course.Id}. {course.Name} ({course.Description}). {course.Length} lesson(s).");
                }
            }
            Console.WriteLine("===============================================");
            Console.WriteLine("Courses in draft:");
            if (draftCourses.Count == 0)
                Console.WriteLine("No courses.");
            else
            {
                foreach (var course in draftCourses)
                {
                    Console.Write($"ID - {course.Id}. {course.Name}. ({course.Description}). ");
                    if (course.CourseMaterials.Count == 0 && course.CourseSkills.Count == 0) 
                        Console.WriteLine("No materials and skills added.");
                    else if (course.CourseMaterials.Count == 0)
                        Console.WriteLine("No materials added.");
                    else
                        Console.WriteLine("No skills added.");
                }
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayUsersAsync()
        {
            Console.WriteLine("Users list:");
            var usersList = await UserService.GetAllAsync();
            if (usersList.Count == 0)
                Console.WriteLine("No users.");
            for (int i = 0; i < usersList.Count; i++)
            {
                Console.WriteLine($"ID - {usersList[i].Id}.{usersList[i].Name}.");
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayMaterialsAsync()
        {
            Console.WriteLine("Materials list:");
            var materialsList = await MaterialService.GetAllAsync();
            if (materialsList.Count == 0)
                Console.WriteLine("No materials.");
            foreach (var material in materialsList)
            {
                switch (material)
                {
                    case Video video:
                        Console.WriteLine($"ID - {video.Id}. {video.Name}. Length: {video.Length} min(s). Quality: {video.Quality} | Type: {video.Type}");
                        break;
                    case EBook book:
                        Console.WriteLine($"ID - {book.Id}. {book.Name}. Author: {book.Author}. Format: {book.Format} | Type: {book.Type}");
                        break;
                    case Article article:
                        Console.WriteLine($"ID - {article.Id}. {article.Name}. Publication date: {article.PublicationDate}. Resource: {article.Resource} | Type: {article.Type}");
                        break;
                }
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplaySkillsAsync()
        {
            Console.WriteLine("Skills list: ");
            var skillsList = await SkillService.GetAllAsync();
            if (skillsList.Count == 0)
                Console.WriteLine("No skills.");
            else
                foreach (var skill in skillsList)
                {
                    Console.WriteLine($"ID{skill.Id} - {skill.Name}");
                }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayUserInfoAsync(int id)
        {
            Console.WriteLine("User information:");
            var user = await UserService.GetUserWithNestedInformationAsync(id);
            Console.WriteLine($"Id: {user.Id}");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine("===============================================");
        }
        public async Task DisplayCoursesInProgressAsync(int id)
        {
            Console.WriteLine("Courses in progress:");
            var user = await UserService.GetUserWithNestedInformationAsync(id);
            if (user.CoursesInProgress.Count == 0)
            {
                Console.WriteLine("No courses.");
            }
            else
            {
                foreach (var course in user.CoursesInProgress)
                {
                    Console.WriteLine($"ID - {course.Id}. {course.Name} ({course.Description}). {course.LearningProgress} of {course.Length} lesson(s) learned.");
                }
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayFinishedCoursesAsync(int id)
        {
            Console.WriteLine("Finished courses:");
            var user = await UserService.GetUserWithNestedInformationAsync(id);
            if (user.FinishedCourses.Count == 0)
            {
                Console.WriteLine("No courses.");
            }
            else
            {
                foreach (var course in user.FinishedCourses)
                {
                    Console.WriteLine($"ID - {course.Id}. {course.Name} ({course.Description}). {course.LearningProgress} of {course.Length} lesson(s) learned.");
                }
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayLearnedSkillsAsync(int id)
        {
            Console.WriteLine("Learned skills:");
            var user = await UserService.GetUserWithNestedInformationAsync(id);
            if (user.LearnedSkills.Count == 0)
            {
                Console.WriteLine("No skills.");
            }
            else
            {
                foreach (var skill in user.LearnedSkills)
                {
                    Console.WriteLine($"ID - {skill.Id}. {skill.Name}. Level - {skill.Level}.");
                }
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayCoursesWithMaterialsAsync()
        {
            Console.WriteLine("All courses:");
            var courses = await CourseService.GetAllAsync();
            if (courses.Count == 0)
                Console.WriteLine("No courses.");
            else
            {
                foreach (var course in courses)
                {
                    if (course.CourseMaterials.Count > 0)
                    {
                        Console.WriteLine($"ID - {course.Id}. {course.Name}. Materials({course.CourseMaterials.Count}):");
                        foreach (var material in course.CourseMaterials)
                        {
                            Console.WriteLine($"{material.Name} (ID - {material.Id})");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"ID - {course.Id}. {course.Name}. Materials(0):");
                        Console.WriteLine("No materials added.");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayCourseWithMaterialsAsync(int id)
        {
            Console.WriteLine("Selected course:");
            var course = await CourseService.GetCourseAsync(id);
            if (course.CourseMaterials.Count > 0)
            {
                Console.WriteLine($"ID - {course.Id}. {course.Name}. Materials({course.CourseMaterials.Count}):");
                foreach (var material in course.CourseMaterials)
                {
                    Console.WriteLine($"{material.Name} (ID - {material.Id})");
                }
            }
            else
            {
                Console.WriteLine($"ID - {course.Id}. {course.Name}. Materials(0):");
                Console.WriteLine("No materials added.");
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayCoursesWithSkillsAsync()
        {
            Console.WriteLine("All courses:");
            var courses = await CourseService.GetAllAsync();
            if (courses.Count == 0)
                Console.WriteLine("No courses.");
            else
            {
                foreach (var course in courses)
                {
                    if (course.CourseSkills.Count > 0)
                    {
                        Console.WriteLine($"ID - {course.Id}. {course.Name}. Skills({course.CourseSkills.Count}):");
                        foreach (var skill in course.CourseSkills)
                        {
                            Console.WriteLine($"{skill.Name} (ID - {skill.Id})");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"ID - {course.Id}. {course.Name}. Skills(0):");
                        Console.WriteLine("No skills added.");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("===============================================");
        }
        public async Task DisplayCourseWithSkillsAsync(int id)
        {
            Console.WriteLine("Selected course:");
            var course = await CourseService.GetCourseAsync(id);
            if (course.CourseMaterials.Count > 0)
            {
                Console.WriteLine($"ID - {course.Id}. {course.Name}. Skills({course.CourseSkills.Count}):");
                foreach (var skill in course.CourseSkills)
                {
                    Console.WriteLine($"{skill.Name} (ID - {skill.Id})");
                }
            }
            else
            {
                Console.WriteLine($"ID - {course.Id}. {course.Name}. Skills(0):");
                Console.WriteLine("No skills added.");
            }
            Console.WriteLine("===============================================");
        }
    }
}