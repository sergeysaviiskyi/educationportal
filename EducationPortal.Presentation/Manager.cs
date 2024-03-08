using Microsoft.Extensions.DependencyInjection;
using EducationPortal.Presentation.Services;
using System.Reflection;
using EducationPortal.Presentation.Interfaces;
using Autofac;
using EducationPortal.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EducationPortal.Infrastructure.Services;
using System.Text;

namespace EducationPortal.Presentation
{
    public class Manager
    {
        public CommandProcessor CommandProcessor;
        public Manager(CommandProcessor commandProcessor)
        {
            CommandProcessor = commandProcessor;
        }
        public async Task StartUpAsync()
        {
            Configuration();
            await DisplayLoginPageAsync();
        }
        
        public void Configuration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetConnectionString("MyConnection");

            var services = new ServiceCollection()
                .AddDbContext<EducationPortalContext>(options => options.UseSqlServer(connectionString))
                .AddAutoMapper(Assembly.Load("EducationPortal.Application"))
                .AddScoped<IUnitOfWork, UnitOfWork>();

            var servicesTypes = typeof(ICourseService).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                .ToList();
            foreach (var s in servicesTypes)
            {
                var parentType = s.GetInterfaces().FirstOrDefault();
                services.AddScoped(parentType, s);
            }
            var commandsTypes = typeof(ICommonCommand).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Command"))
                .ToList();
            foreach (var c in commandsTypes)
            {
                var parentType = c.GetInterfaces().FirstOrDefault();
                services.AddScoped(parentType, c);
            }
            services.AddScoped<DisplayService>();
            var serviceProvider = services.BuildServiceProvider();

            var commands = serviceProvider.GetServices<ICommonCommand>();
            foreach (var instance in commands)
            {
                var commandAttribute = instance.GetType().GetCustomAttribute<CommandNameAttribute>();
                if (commandAttribute != null)
                {
                    CommandProcessor.SetCommand(commandAttribute.Name, instance);
                }
            }
        }
        public async Task DisplayLoginPageAsync()
        {
            while (true)
            {
                Console.WriteLine("LOGIN PAGE");
                Console.WriteLine("===============================================");
                await CommandProcessor.Execute(CommadsList.DisplayUsers.ToString());
                Console.Write("Write user's ID to log in. To register new user press 0: ");
                var inputInfo = Console.ReadLine();
                int selectedId;
                bool isValidId = int.TryParse(inputInfo, out selectedId);
                if (isValidId && selectedId >= 0)
                {
                    switch (selectedId)
                    {
                        case 0:
                            Console.Write("User name: ");
                            CommandProcessor.Context.Name = Console.ReadLine();
                            await CommandProcessor.Execute(CommadsList.CreateUser.ToString());
                            break;
                        default:
                            CommandProcessor.Context.UserId = selectedId;
                            Console.Clear();
                            await DisplayUserPageAsync();//await
                            break;
                    }
                }
                else { Console.WriteLine("Wrong input information. Try again."); }
                Console.ReadKey();
                Console.Clear();
            }
        }
        public async Task DisplayUserPageAsync()
        {
            while (true)
            {
                Console.WriteLine("USER PAGE");
                Console.WriteLine("===============================================");
                await CommandProcessor.Execute(CommadsList.DisplayUserInfo.ToString());
                await CommandProcessor.Execute(CommadsList.DisplayCoursesInProgress.ToString());
                await CommandProcessor.Execute(CommadsList.DisplayFinishedCourses.ToString());
                await CommandProcessor.Execute(CommadsList.DisplayLearnedSkills.ToString());
                Console.WriteLine("Select what you want to interact with:\n1 - Courses.\n2 - Materials.\n3 - Skills\n0 - Log out.");
                int selectedOperation;
                bool isValid = int.TryParse(Console.ReadLine(), out selectedOperation);
                if (isValid && selectedOperation <= 4 && selectedOperation >= 0)
                {
                    switch (selectedOperation)
                    {
                        case 1:
                            Console.Clear();
                            await DisplayCoursesPageAsync();
                            break;
                        case 2:
                            Console.Clear();
                            await DisplayMaterialsPageAsync();
                            break;
                        case 3:
                            Console.Clear();
                            await DisplaySkillsPageAsync();
                            break;
                        case 0:
                            goto exit_loop;
                        default:
                            Console.WriteLine("Wrong input information. Try again.");
                            break;
                    }
                }
                Console.ReadKey();
                Console.Clear();
            }
        exit_loop:;
        }
        public async Task DisplayCoursesPageAsync()
        {
            while (true)
            {
                Console.WriteLine("ACTIONS WITH COURSES");
                Console.WriteLine("===============================================");
                await CommandProcessor.Execute(CommadsList.DisplayCoursesInProgress.ToString());
                await CommandProcessor.Execute(CommadsList.DisplayFinishedCourses.ToString());
                await CommandProcessor.Execute(CommadsList.DisplayCourses.ToString());

                Console.Write("Select operation:\n1 - Learn a lesson.\n2 - Start learning new course." +
                    "\n3 - Create new course.\n4 - Add material to course.\n5 - Add skill to course.\n0 - Exit.\n");
                string inputInfo = Console.ReadLine();
                int selectedOperation;
                bool isValid = int.TryParse(inputInfo, out selectedOperation);
                if (isValid && selectedOperation <= 6 && selectedOperation >= 0)
                {
                    switch (selectedOperation)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("LEARN A LESSON");
                            Console.WriteLine("===============================================");
                            await CommandProcessor.Execute(CommadsList.DisplayCoursesInProgress.ToString());
                            Console.Write("Select course id. Press 0 to exit: ");
                            inputInfo = Console.ReadLine();
                            if (await ValidateInputAsync(inputInfo))
                            {
                                int courseId = int.Parse(inputInfo);
                                if (courseId != 0)
                                {
                                    CommandProcessor.Context.CourseId = courseId;
                                    await CommandProcessor.Execute(CommadsList.LearnLesson.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong input information. Try again.");
                                Console.ReadKey();
                            }
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("START LEARNING NEW COURSE");
                            Console.WriteLine("===============================================");
                            await CommandProcessor.Execute(CommadsList.DisplayCourses.ToString());
                            Console.Write("Select course id. Press 0 to exit: ");
                            inputInfo = Console.ReadLine();
                            if (await ValidateInputAsync(inputInfo))
                            {
                                int courseId = int.Parse(inputInfo);
                                if (courseId != 0)
                                {
                                    CommandProcessor.Context.CourseId = courseId;
                                    await CommandProcessor.Execute(CommadsList.StartLearningNewCourse.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong input information. Try again.");
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("CREATE NEW COURSE");
                                Console.WriteLine("===============================================");
                                await CommandProcessor.Execute(CommadsList.DisplayCourses.ToString());
                                Console.Write("Course name. Press 0 to exit: ");
                                string courseName = Console.ReadLine();
                                if (await ValidateInputAsync(courseName))
                                {
                                    selectedOperation = int.Parse(courseName);
                                    if (selectedOperation == 0)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                else
                                {
                                    if (courseName != string.Empty && courseName != null)
                                    {
                                        CommandProcessor.Context.Name = courseName;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                Console.Write("Course description, press 0 to exit: ");
                                string courseDescription = Console.ReadLine();
                                if (await ValidateInputAsync(courseDescription))
                                {
                                    selectedOperation = int.Parse(courseDescription);
                                    if (selectedOperation == 0)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                else
                                {
                                    if (courseDescription != string.Empty && courseDescription != null)
                                    {
                                        CommandProcessor.Context.Description = courseDescription;
                                        await CommandProcessor.Execute(CommadsList.CreateCourse.ToString());
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 4:
                            await AddMaterialsToCoursePageAsync();
                            break;
                        case 5:
                            await AddSkillsToCoursePageAsync();
                            break;
                        case 0:
                            goto exit_loop;
                    }
                }
                else
                    Console.WriteLine("Wrong input information. Try again.");
                Console.ReadKey();
                Console.Clear();
            }
        exit_loop:;
        }
        public async Task AddMaterialsToCoursePageAsync()
        {
            Console.Clear();
            Console.WriteLine("ADD MATERIAL TO COURSE");
            Console.WriteLine("===============================================");
            await CommandProcessor.Execute(CommadsList.DisplayCoursesWithMaterials.ToString());
            Console.Write("Select course id. Press 0 to exit: ");
            var inputInfo = Console.ReadLine();
            if (await ValidateInputAsync(inputInfo))
            {
                int courseId = int.Parse(inputInfo);
                if (courseId != 0)
                {
                    CommandProcessor.Context.CourseId = courseId;
                    while (true)
                    {
                        Console.Clear();
                        await CommandProcessor.Execute(CommadsList.DisplayCourseWithMaterials.ToString());
                        await CommandProcessor.Execute(CommadsList.DisplayMaterials.ToString());
                        Console.Write("Select material id. Press 0 to exit: ");
                        inputInfo = Console.ReadLine();
                        if (await ValidateInputAsync(inputInfo))
                        {
                            int materialId = int.Parse(inputInfo);
                            if (materialId != 0)
                            {
                                CommandProcessor.Context.MaterialId = materialId;
                                await CommandProcessor.Execute(CommadsList.AddMaterialToCourse.ToString());
                            }
                            else break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input information. Try again.");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }
        public async Task AddSkillsToCoursePageAsync()
        {
            Console.Clear();
            Console.WriteLine("ADD SKILL TO COURSE");
            Console.WriteLine("===============================================");
            await CommandProcessor.Execute(CommadsList.DisplayCoursesWithSkills.ToString());
            Console.Write("Select course id. Press 0 to exit: ");
            var inputInfo = Console.ReadLine();
            if (await ValidateInputAsync(inputInfo))
            {
                int courseId = int.Parse(inputInfo);
                if (courseId != 0)
                {
                    CommandProcessor.Context.CourseId = courseId;
                    while (true)
                    {
                        Console.Clear();
                        await CommandProcessor.Execute(CommadsList.DisplayCourseWithSkills.ToString());
                        await CommandProcessor.Execute(CommadsList.DisplaySkills.ToString());
                        Console.Write("Select material id. Press 0 to exit: ");
                        inputInfo = Console.ReadLine();
                        if (await ValidateInputAsync(inputInfo))
                        {
                            int skillId = int.Parse(inputInfo);
                            if (skillId != 0)
                            {
                                CommandProcessor.Context.SkillId = skillId;
                                await CommandProcessor.Execute(CommadsList.AddSkillToCourse.ToString());
                            }
                            else break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input information. Try again.");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }
        public async Task<bool> ValidateInputAsync(string str)
        {
            if (str != null || str.Length != 0)
            {
                return int.TryParse(str, out _);
            }
            else return false;
        }
        public async Task DisplaySkillsPageAsync()
        {
            while (true)
            {
                Console.WriteLine("ACTIONS WITH SKILLS");
                Console.WriteLine("===============================================");
                await CommandProcessor.Execute(CommadsList.DisplayLearnedSkills.ToString());
                await CommandProcessor.Execute(CommadsList.DisplaySkills.ToString());
                Console.Write("Select operation:\n1 - Create new skill.\n2 - #." +
                        "\n0 - Exit.\n");
                string inputInfo = Console.ReadLine();
                int selectedOperation;
                bool isValid = int.TryParse(inputInfo, out selectedOperation);
                if (isValid && selectedOperation <= 1 && selectedOperation >= 0)
                {
                    switch (selectedOperation)
                    {
                        case 1:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("CREATE NEW COURSE");
                                Console.WriteLine("===============================================");
                                await CommandProcessor.Execute(CommadsList.DisplaySkills.ToString());
                                Console.Write("Skill name. Press 0 to exit: ");
                                string skillName = Console.ReadLine();
                                if (await ValidateInputAsync(skillName))
                                {
                                    selectedOperation = int.Parse(skillName);
                                    if (selectedOperation == 0)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                else
                                {
                                    if (skillName != string.Empty && skillName != null)
                                    {
                                        CommandProcessor.Context.Name = skillName;
                                        await CommandProcessor.Execute(CommadsList.CreateSkill.ToString());
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 0:
                            goto exit_loop;
                    }
                }
                else
                    Console.WriteLine("Wrong input information. Try again.");
                Console.ReadKey();
                Console.Clear();
            }
        exit_loop:;
        }
        public async Task DisplayMaterialsPageAsync()
        {
            while (true)
            {
                Console.WriteLine("ACTIONS WITH MATERIALS");
                Console.WriteLine("===============================================");
                await CommandProcessor.Execute(CommadsList.DisplayMaterials.ToString());
                Console.Write("Select operation:\n1 - Create new materials.\n2 - #." +
                        "\n0 - Exit.\n");
                string inputInfo = Console.ReadLine();
                int selectedOperation;
                bool isValid = int.TryParse(inputInfo, out selectedOperation);
                if (isValid && selectedOperation <= 1 && selectedOperation >= 0)
                {
                    switch (selectedOperation)
                    {
                        case 1:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("CREATE NEW MATERIAL");
                                Console.WriteLine("===============================================");
                                await CommandProcessor.Execute(CommadsList.DisplayMaterials.ToString());
                                Console.Write("Material name. Press 0 to exit: ");
                                string materialName = Console.ReadLine();
                                if (await ValidateInputAsync(materialName))
                                {
                                    selectedOperation = int.Parse(materialName);
                                    if (selectedOperation == 0)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                else
                                {
                                    if (materialName != string.Empty && materialName != null)
                                    {
                                        CommandProcessor.Context.Name = materialName;
                                        await CommandProcessor.Execute(CommadsList.CreateVideo.ToString());
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input information. Try again.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                            }
                            break;
                        case 0:
                            goto exit_loop;
                    }
                }
                else
                    Console.WriteLine("Wrong input information. Try again.");
                Console.ReadKey();
                Console.Clear();
            }
        exit_loop:;
        }
    }
}