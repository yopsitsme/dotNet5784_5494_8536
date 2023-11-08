

namespace DalTest;
using DalApi;
using Dal;
using DO;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Globalization;

internal class Program
{
    private static ITask? s_dalTask = new TaskImplementation();
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();

    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalTask, s_dalDependency, s_dalEngineer);
            DisplayMainMenu();

        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }

    static void DisplayMainMenu()
    {

        bool exitMenu = false;

        while (!exitMenu)
        {
            Console.WriteLine("Main Menu - Select an entity you want to check:");
            Console.WriteLine("0. Exiting the main menu");
            Console.WriteLine("1. Task");
            Console.WriteLine("2. Engineer");
            Console.WriteLine("3. Dependency");

            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    exitMenu = true;
                    break;
                case "1":
                    // Handle Task
                    DisplaySubMenu("Task");
                    
                    break;
                case "2":
                    // Handle Engineer
                    DisplaySubMenu("Engineer");
                    
                    break;
                case "3":
                    // Handle Dependency
                    DisplaySubMenu("Dependency");
                   
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }

    }

    public static void DisplaySubMenu(string entityName)
    {
        try
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine("Select the method you want to execute:");
                Console.WriteLine("1. Exiting the main menu");
                Console.WriteLine("2. Adding a new object to the " + entityName + " list");
                Console.WriteLine("3. Object display by identifier");
                Console.WriteLine("4. The display of the list of all " + entityName);
                Console.WriteLine("5. Updating existing object data");
                Console.WriteLine("6. Deleting an existing object from a list");

                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        exitMenu = true;
                        break;
                    case "2":
                        // Handle Create
                        createGlobal(entityName);
                        break;
                    case "3":
                        // Handle Read
                        readGlobal(entityName);
                        break;
                    case "4":
                        // Handle ReadAll
                        readAllGlobal(entityName);
                        Console.WriteLine("You selected ReadAll");
                        break;
                    //case "5":
                    //    // Handle Update
                    //    updateGlobal(entityName);
                    //    break;
                    case "6":
                        // Handle Delete
                        deleteGlobal(entityName);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }


    public static void createGlobal(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                createTask();
                break;
            case "Engineer":
                createEngineer();
                break;
            case "Dependency":
                createDependency();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }

    static void createTask()
    {
        string description = GetInput("Enter the task description: ");
        string alias = GetInput("Enter the task alias: ");
        bool isMilestone = GetBoolInput("Is the task a milestone? (true/false): ");
        DateTime start = GetDateTimeInput("Enter the task start date and time (YYYY-MM-DD HH:mm:ss): ");
        DateTime deadline = GetDateTimeInput("Enter the task deadline date and time (YYYY-MM-DD HH:mm:ss): ");
        string deliverables = GetInput("Enter the task deliverables: ");
        EngineerExperience complexityLevel = GetComplexityLevelInput("Please enter the engineer's experience level (  Novice, AdvancedBeginner, Competent, Proficient, Expert): ");

        Task task = new Task(description, alias, isMilestone, start, deadline, deliverables, complexityLevel);
        s_dalTask.Create(task);
    }

    static void createEngineer()
    {
        string name = GetInput("Please enter the engineer's name: ");
        string email = GetInput("Please enter the engineer's email: ");

        string levelInput = GetInput("Please enter the engineer's experience level (Novice, AdvancedBeginner, Competent, Proficient, Expert): ");
        EngineerExperience level;
        Enum.TryParse(levelInput, out level);

        double cost = Convert.ToDouble(GetInput("Please enter the engineer's cost per hour: "));
        int id = Convert.ToInt32(GetInput("Please enter the engineer's ID: "));

        Engineer engineer = new Engineer(name, email, level, cost, id);
        try
        {

            s_dalEngineer.Create(engineer);
        }
        catch (Exception e) { Console.WriteLine(e); }
    }

    static void createDependency()
    {
        int dependentTask = int.Parse(GetInput("Enter the DependentTask: "));
        int dependsTask = int.Parse(GetInput("Enter the DependsTask: "));

        Dependency dependency = new Dependency(dependentTask, dependsTask);
        s_dalDependency.Create(dependency);
    }

    public static void readGlobal(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                readTask();
                break;
            case "Engineer":
                readEngineer();
                break;
            case "Dependency":
                readDependency();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }

    static void readTask()
    {
        int idTask; 
        int.TryParse((GetInput("Enter the Tasks id: ")),out idTask);
        Task taskRead = s_dalTask.Read(idTask);
        Console.WriteLine(taskRead);

    }

    static void readEngineer()
    {
        int idEngineer;
         int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
        Engineer engineerRead = s_dalEngineer.Read(idEngineer);
        Console.WriteLine(engineerRead);
    }

    static void readDependency()
    {
        int idDependency;
         int.TryParse(GetInput("Enter the Dependency id: "), out idDependency);
        Dependency dependencyRead = s_dalDependency.Read(idDependency);
        Console.WriteLine(dependencyRead);
    }

    public static void readAllGlobal(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                readAllTask();
                break;
            case "Engineer":
                readAllEngineer();
                break;
            case "Dependency":
                readAllDependency();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }

    static void readAllTask()
    {
        List<Task> taskList = s_dalTask.ReadAll();
        foreach (var task in taskList)
        {
            Console.WriteLine(task);
        }
    }
    static void readAllEngineer()
    {
        List<Engineer> engineerList = s_dalEngineer.ReadAll();
        foreach (var engineer in engineerList)
        {
            Console.WriteLine(engineer);
        }
    }
    static void readAllDependency()
    {
        List<Dependency> dependencyList = s_dalDependency.ReadAll();
        foreach (var dependency in dependencyList)
        {
            Console.WriteLine(dependency);
        }
    }

    public static void deleteGlobal(string entityName)
    {
        switch (entityName)
        {
            case "Task":
                deleteTask();
                break;
            case "Engineer":
                deleteEngineer();
                break;
            case "Dependency":
                deleteDependency();
                break;
            default:
                throw (new Exception("no such entity name"));
        }

    }

    static void deleteTask()
    {
        try
        {
            int idTask;
            int.TryParse(GetInput("Enter the Tasks id: "),out idTask);
            s_dalTask.Delete(idTask);
        }
        catch (Exception e) { Console.WriteLine(e); }

    }
    static void deleteEngineer()
    {
        try
        {
            int idEngineer;
             int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
            s_dalEngineer.Delete(idEngineer);
        }
        catch (Exception e) { Console.WriteLine(e); }

    }

    static void deleteDependency()
    {
        try
        {
            int idDependency;
             int.TryParse(GetInput("Enter the Dependency id: "), out idDependency);
            s_dalDependency.Delete(idDependency);
        }
        catch (Exception e) { Console.WriteLine(e); }
    }

    //public static void updateGlobal(string entityName)
    //{
    //    switch (entityName)
    //    {
    //        case "Task":
    //            updataTask();
    //            break;
    //        case "Engineer":
    //            updataEngineer();
    //            break;
    //        case "Dependency":
    //            updataDependency();
    //            break;
    //        default:
    //            throw (new Exception("no such entity name"));
    //    }

    //}

    //static void updateTask()
    //{
    //    int idTask;
    //     int.TryParse(GetInput("Enter the Tasks id: "),out idTask);
    //    DO.Task task = s_dalTask.Read(idTask);
    //    if (task != null)
    //    {
    //        Console.WriteLine(task);
    //        DO.Task updatedTask = ReadTaskFromUser();
    //        updatedTask.Id = idTask;//update the new task's id to be the same as the task the user want to update
    //        s_dalTask.Update(updatedTask);

    //    }

    //}
    //static void updateEngineer()
    //{
    //    int idEngineer;
    //    int.TryParse(GetInput("Enter the Engineer id: "), out idEngineer);
    //    Engineer engineer = s_dalEngineer.Read(idEngineer);
    //    if (engineer != null)
    //    {
    //        Console.WriteLine(engineer);
    //        Engineer updatedEngineer = ReadEngineerFromUser();
    //        s_dalEngineer.Update(updatedEngineer);
    //    }
    //}
    //static void updateDependency()
    //{
    //    int idDependency;
    //    int.TryParse(GetInput("Enter the Dependency id: "),out idDependency);
    //    Dependency dependency = s_dalDependency.Read(idDependency);
    //    if (dependency != null)
    //    {
    //        Console.WriteLine(dependency);
    //        Dependency updatedDependency = ReadDependencyFromUser();
    //        updatedDependency.Id = idDependency;//update the new dependency's id to be the same as the task the user want to update
    //        s_dalDependency.Update(updatedDependency);
    //    }
    //}

    static string GetInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    static bool GetBoolInput(string message)
    {
        Console.Write(message);
        string input = Console.ReadLine();
        return bool.Parse(input);
    }

    static DateTime GetDateTimeInput(string message)
    {
        Console.Write(message);

        DateTime createdAtDate;
        DateTime.TryParseExact(GetInput("Enter the created at date and time (yyyy-MM-dd HH:mm:ss): "), "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out createdAtDate);
        return createdAtDate;
    }

    static EngineerExperience GetComplexityLevelInput(string message)
    {
        Console.Write(message);
        string level = Console.ReadLine();
        EngineerExperience experienceLevel;
        Enum.TryParse(level, out experienceLevel);
        return experienceLevel;
    }
}






