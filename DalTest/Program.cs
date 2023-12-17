namespace DalTest;

using System.Numerics;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Dal;
using DalApi;
using DO;
using DalXml;
using System.Diagnostics;

internal class Program
{

    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    //private static IDependency s_dalDependency = new DependencyImplementation(); //stage 1
    private static DateTime createdate;





    //  static readonly IDal s_dal = new DalList(); //stage 2
    //  static readonly IDal s_dal = new DalXml(); //stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4


    static void Main(string[] args)
    {
        try
        {
            //Initialization.Do(s_dalDependency, s_dalTask, s_dalEngineer);
            Initialization.Do(s_dal); //stage 2
            Console.OutputEncoding = new UTF8Encoding();
            Console.InputEncoding = new UTF8Encoding();
            //  Console.WriteLine("Write your input:");
            //  string Input = Console.ReadLine();
            mainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


    static void mainMenu()
    {
        while (true)
        {
            Console.WriteLine("תפריט ראשי - בחר ישות שברצונך לבדוק:");
            Console.WriteLine("0. יציאה מתפריט ראשי");
            Console.WriteLine("1. מהנדס");
            Console.WriteLine("2. משימה");
            Console.WriteLine("3. משימה");

            int choice = 0;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("יצאת מהתוכנית.");
                        return;
                    case 1:
                        engineerMenu();
                        break;
                    case 2:
                        taskMenu();
                        break;
                    case 3:
                        dependencyMenu();
                        break;
                    default:
                        Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
            return;
        }
    }
    static void engineerMenu()
    {
        while (true)
        {
            Console.WriteLine("בחר את המתודה שברצונך לבצע:");
            Console.WriteLine("1. יציאה מתפריט ראשי");
            Console.WriteLine("2. הוספת אובייקט חדש (Create)");
            Console.WriteLine("3. תצוגת אובייקט על פי מזהה (Read)");
            Console.WriteLine("4. תצוגת רשימת כל האובייקטים (ReadAll)");
            Console.WriteLine("5. עדכון נתוני אובייקט קיים (Update)");
            Console.WriteLine("6. מחיקת אובייקט קיים (Delete)");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("יצאת מהתוכנית.");
                            return;
                        case 2:
                            createEngineer();
                            break;
                        case 3:
                            readEngineer();
                            break;
                        case 4:
                            readAllEngineers();
                            break;
                        case 5:
                            updateEngineer();
                            break;
                        case 6:
                            deleteEngineer();
                            break;

                        default:
                            Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                            break;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); };
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
        }
    }//Manages the engineer menu
    static void dependencyMenu()
    {
        while (true)
        {
            Console.WriteLine("בחר את המתודה שברצונך לבצע:");
            Console.WriteLine("1. יציאה מתפריט ראשי");
            Console.WriteLine("2. הוספת אובייקט חדש (Create)");
            Console.WriteLine("3. תצוגת אובייקט על פי מזהה (Read)");
            Console.WriteLine("4. תצוגת רשימת כל האובייקטים (ReadAll)");
            Console.WriteLine("5. עדכון נתוני אובייקט קיים (Update)");
            Console.WriteLine("6. מחיקת אובייקט קיים (Delete)");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("יצאת מהתוכנית.");
                            return;
                        case 2:
                            createDependency();
                            break;
                        case 3:
                            readDependency();
                            break;
                        case 4:
                            readAllDependencies();
                            break;
                        case 5:
                            updateDependency();
                            break;
                        case 6:
                            deleteDependency();
                            break;

                        default:
                            Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                            break;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
        }
    }//Manages the dependency menu

    static void taskMenu()
    {
        while (true)
        {
            Console.WriteLine("בחר את המתודה שברצונך לבצע:");
            Console.WriteLine("1. יציאה מתפריט ראשי");
            Console.WriteLine("2. הוספת אובייקט חדש (Create)");
            Console.WriteLine("3. תצוגת אובייקט על פי מזהה (Read)");
            Console.WriteLine("4. תצוגת רשימת כל האובייקטים (ReadAll)");
            Console.WriteLine("5. עדכון נתוני אובייקט קיים (Update)");
            Console.WriteLine("6. מחיקת אובייקט קיים (Delete)");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("יצאת מהתוכנית.");
                            return;
                        case 2:
                            createTask();
                            break;
                        case 3:
                            readTask();
                            break;
                        case 4:
                            readAllTasks();
                            break;
                        case 5:
                            updateTask();
                            break;
                        case 6:
                            deleteTask();
                            break;

                        default:
                            Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                            break;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
        }
    }
    static void createTask()
    {
        int id;
        DO.Task? new_task = ReadTaskFromUser();
        if (new_task != null)
        {
            id = s_dal.Task.Create(new_task);
            Console.WriteLine($"המשימה נוצרה בהצלחה, המספר המזהה הוא: {id}");
        }
    }//create a new task


    static void readTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        DO.Task? task = s_dal.Task.Read(id);
        if (task != null)
        {
            Console.WriteLine(task);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Task עם ID {id} לא קיים");
        }

    }//prints the task by it's id
    static void readAllTasks()
    {
        IEnumerable<Task> tasks = (IEnumerable<Task>)s_dal.Task.ReadAll();
        //  ( IEnumerable )List<Task> tasks = s_dal.Task.ReadAll();
        if (tasks == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (DO.Task task in tasks)
        {
            Console.WriteLine(task);
        }
    }//prits the all list of tasks
    static void updateTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        DO.Task? task = s_dal.Task.Read(id);
        if (task != null)
        {
            Console.WriteLine(task);
            DO.Task? updatedTask = ReadTaskFromUser();
            FieldInfo[] fields;
            if (updatedTask != null)
            {
                fields = updatedTask.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                // Loop through each field
                foreach (FieldInfo field in fields)
                {

                    object? fieldValue = field.GetValue(updatedTask);
                    if (fieldValue == null || DateTime.Equals(fieldValue, DateTime.MinValue) || (string)fieldValue == "")
                    {
                        field.SetValue(updatedTask, field.GetValue(task));
                    }
                }
                updatedTask.Id = id;//update the new task's id to be the same as the task the user want to update
                s_dal.Task.Update(updatedTask);
                Console.WriteLine(s_dal.Task.Read(id));//prints the task after the update
            }
        }

    }//update a specific task by the user's input
    static void deleteTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_dal.Task.Delete(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");
    }//delete a task by it's id from the user's input
    static DO.Task? ReadTaskFromUser()
    {
        Console.WriteLine("Enter Task Data:");

        // Input fields
        Console.Write("Description: ");
        string? description = Console.ReadLine();

        Console.Write("Alias: ");
        string? alias = Console.ReadLine();

        Console.Write("Milestone (true/false): ");
        bool milestone;
        if (!bool.TryParse(Console.ReadLine(), out milestone))
        {
            Console.WriteLine("Invalid Milestone value. Aborting.");
            return null;
        }

        Console.Write("Start Date (yyyy-MM-dd HH:mm:ss): ");
        DateTime start;
        if (!DateTime.TryParse(Console.ReadLine(), out start))
        {
            Console.WriteLine("Invalid Start Date format. Aborting.");
            return null;
        }

        Console.Write("Schedule Date (yyyy-MM-dd HH:mm:ss): ");
        DateTime scheduleDate;
        if (!DateTime.TryParse(Console.ReadLine(), out scheduleDate))
        {
            Console.WriteLine("Invalid Schedule Date format. Aborting.");
            return null;
        }

        Console.Write("Deadline (yyyy-MM-dd HH:mm:ss): ");
        DateTime deadline;
        if (!DateTime.TryParse(Console.ReadLine(), out deadline))
        {
            Console.WriteLine("Invalid Deadline format. Aborting.");
            return null;
        }

        Console.Write("Complete Date (yyyy-MM-dd HH:mm:ss): ");
        DateTime complete;
        if (!DateTime.TryParse(Console.ReadLine(), out complete))
        {
            Console.WriteLine("Invalid Complete Date format. Aborting.");
            return null;
        }

        Console.Write("Deliverables: ");
        string? deliverables = Console.ReadLine();

        Console.Write("Remarks: ");
        string? remarks = Console.ReadLine();

        Console.Write("Engineer ID: ");
        int engineerId;
        if (!int.TryParse(Console.ReadLine(), out engineerId))
        {
            Console.WriteLine("Invalid Engineer ID. Aborting.");
            return null;
        }
        Console.WriteLine("Choose Engineer Experience Level:");
        Console.WriteLine("1. Beginer");
        Console.WriteLine("2. Compeatative");
        Console.WriteLine("3. Profesional");
        Console.WriteLine("4. Export");
        Console.WriteLine("5. Proficient");

        Console.Write("Enter the corresponding number: ");

        EngineerExperience complexityLevel;
        if (int.TryParse(Console.ReadLine(), out int levelChoice))
        {
            switch (levelChoice)
            {
                case 1:
                    complexityLevel = EngineerExperience.Beginner;
                    break;
                case 2:
                    complexityLevel = EngineerExperience.Competitive;
                    break;
                case 3:
                    complexityLevel = EngineerExperience.Professionals;
                    break;
                case 4:
                    complexityLevel = EngineerExperience.Export;
                    break;
                case 5:
                    complexityLevel = EngineerExperience.Proficient;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Aborting.");
                    return null;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Aborting.");
            return null;
        }


        // Create and return a new Task object
        return new DO.Task
        (0,
            description,
         alias,
         milestone,
         createdate,
         start,
         scheduleDate,
         deadline,
         complete,
         deliverables,
         remarks,
         engineerId,
         complexityLevel
         );
    }//reads the task values from the user

    static void createEngineer()
    {
        DO.Engineer? newEngineer = ReadEngineerFromUser();
        if (newEngineer != null)
            s_dal.Engineer.Create(newEngineer);
    }//create a new engineer

    static void readEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        DO.Engineer? engineer = s_dal.Engineer.Read(id);
        if (engineer != null)
        {
            Console.WriteLine(engineer);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Engineer עם ID {id} לא קיים");
        }
    }//prints the engineer by it's id

    static void readAllEngineers()
    {
        IEnumerable<Engineer> tasks = (IEnumerable<Engineer>)s_dal.Task.ReadAll();
        // List<DO.Engineer> engineers = s_dal.Engineer.ReadAll();
        if (tasks == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (DO.Engineer engineer in tasks)
        {
            Console.WriteLine(engineer);
        }
    }//prits the all list of engineers

    static void updateEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        DO.Engineer? engineer = s_dal.Engineer.Read(id);
        if (engineer != null)
        {
            Console.WriteLine(engineer);
            DO.Engineer? updatedEngineer = ReadEngineerFromUser();
            if (updatedEngineer != null)
            {
                FieldInfo[] fields = updatedEngineer.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                // Loop through each field
                foreach (FieldInfo field in fields)
                {
                    //string fieldName = field.Name;
                    object? fieldValue = field.GetValue(updatedEngineer);
                    if (fieldValue == null || (string)fieldValue == "")
                    {
                        field.SetValue(updatedEngineer, field.GetValue(engineer));
                    }
                }
                s_dal.Engineer.Update(updatedEngineer);
                Console.WriteLine(s_dal.Engineer.Read(id));//prints the updated engineer
            }
        }
    }//update a specific engineer by the user's input

    static void deleteEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_dal.Engineer.Delete(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");
    }//delete a engineer by it's id from the user's input
    static void createDependency()
    {
        int id;
        DO.Dependency? newDependency = ReadDependencyFromUser();
        if (newDependency != null)
        {
            id = s_dal.Dependency.Create(newDependency);
            Console.WriteLine($"התלות נוצרה בהצלחה, המספר המזהה הוא:{id}");

        }
    }//create a new dependency

    static void readDependency()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של התלות שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        DO.Dependency? dependency = s_dal.Dependency.Read(id);
        if (dependency != null)
        {
            Console.WriteLine(dependency);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Dependency עם ID {id} לא קיים");
        }
    }//prints the dependency by it's id

    static void readAllDependencies()
    {
        IEnumerable<Dependency> dependencies = (IEnumerable<Dependency>)s_dal.Task.ReadAll();
        //   List<DO.Dependency> dependencies = s_dal.Dependency.ReadAll();
        if (dependencies == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (DO.Dependency dependency in dependencies)
        {
            Console.WriteLine(dependency);
        }
    }//prits the all list of dependencies
    static void updateDependency()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של התלות שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        DO.Dependency? dependency = s_dal.Dependency.Read(id);
        if (dependency != null)
        {
            Console.WriteLine(dependency);
            DO.Dependency? updatedDependency = ReadDependencyFromUser();
            FieldInfo[] fields = updatedDependency.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Loop through each field
            foreach (FieldInfo field in fields)
            {
                //string fieldName = field.Name;
                object? fieldValue = field.GetValue(updatedDependency);
                if (fieldValue == null || (string)fieldValue == "")
                {
                    field.SetValue(updatedDependency, field.GetValue(dependency));
                }
            }
            updatedDependency.Id = id;//update the new dependency's id to be the same as the task the user want to update
            s_dal.Dependency.Update(updatedDependency);
            Console.WriteLine(s_dal.Dependency.Read(id));//prints the updated value
        }
    }//update a dependency task by the user's input

    static void deleteDependency()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של התלות שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_dal.Dependency.Delete(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");

    }//delete a dependency by it's id from the user's input
    static Engineer? ReadEngineerFromUser()
    {
        Console.WriteLine("Enter Engineer Data:");

        // Input fields
        Console.Write("ID: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid ID. Aborting.");
            return null;
        }

        Console.Write("Name: ");
        string? name = Console.ReadLine();

        Console.Write("Email: ");
        string? email = Console.ReadLine();
        Console.WriteLine("Choose Engineer Experience Level:");
        Console.WriteLine("1. Beginer");
        Console.WriteLine("2. Compeatative");
        Console.WriteLine("3. Profesional");
        Console.WriteLine("4. Export");
        Console.WriteLine("5. Proficient");

        Console.Write("Enter the corresponding number: ");

        EngineerExperience complexityLevel;
        if (int.TryParse(Console.ReadLine(), out int levelChoice))
        {
            switch (levelChoice)
            {
                case 1:
                    complexityLevel = EngineerExperience.Beginner;
                    break;
                case 2:
                    complexityLevel = EngineerExperience.Competitive;
                    break;
                case 3:
                    complexityLevel = EngineerExperience.Professionals;
                    break;
                case 4:
                    complexityLevel = EngineerExperience.Export;
                    break;
                case 5:
                    complexityLevel = EngineerExperience.Proficient;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Aborting.");
                    return null;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Aborting.");
            return null;
        }
        //todo validation input name & email
        // Create and return a new Engineer object
        return new Engineer(id, name, email, complexityLevel);
    }//reads the engineer values from the user
    static Dependency? ReadDependencyFromUser()
    {
        Console.WriteLine("Enter Dependency Data:");

        // Input fields


        Console.Write("Dependent Task ID: ");
        int dependentTask;
        if (!int.TryParse(Console.ReadLine(), out dependentTask))
        {
            Console.WriteLine("Invalid Dependent Task ID. Aborting.");
            return null;
        }

        Console.Write("Depends On Task ID: ");
        int dependsOnTask;
        if (!int.TryParse(Console.ReadLine(), out dependsOnTask))
        {
            Console.WriteLine("Invalid Depends On Task ID. Aborting.");
            return null;
        }

        // Create and return a new Dependency object
        return new Dependency
        {

            DependenceTask = dependentTask,
            DependenceOnTask = dependsOnTask
        };

    }
}//reads the dependency values from the user
