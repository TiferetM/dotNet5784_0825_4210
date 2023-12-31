using BO;
using DalApi;
using DO;
using System.Reflection;

namespace DalTest;
public class Program
{ 
    private static DateTime createdate;
    static readonly IDal s_dal = Factory.Get; //stage 4
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        BO.Engineer engineer = new BO.Engineer()
        {
            //  Level = (int)EngineerExperience.Beginner,
        };
        try
        {
             s_bl.Engineer.AddEngineer(engineer);
             Console.Write("Would you like to create Initial data? (Y/N)");
             //לשאול את המשתמש איך הוא רוצה להכניס את הנתונים עפ קובץ או עפ initialization
             string ? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
             if (ans == "Y")
                DalTest.Initialization.Do(s_dal);
            //Console.OutputEncoding = new UTF8Encoding();
            //Console.InputEncoding = new UTF8Encoding();
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
            Console.WriteLine("3. אבן דרך");
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
                        milestoneMenu();
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
    static void milestoneMenu()
    {
        while (true)
        {
            Console.WriteLine("בחר את המתודה שברצונך לבצע:");
            Console.WriteLine("1. יציאה מתפריט ראשי");
            Console.WriteLine("2. הוספת לוז פרוייקט חדש (Create)");
            Console.WriteLine("3. תצוגת אובייקט על פי מזהה (Read)");
            Console.WriteLine("4. עדכון נתוני אובייקט קיים (Update)");
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
                            createSchedualProject();
                            break;
                        case 3:
                            ReadMilestoneData( );
                            break;
                        case 4:
                            UpdateMilestoneData();
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
    static void createSchedualProject()
    {
        //צריך לבקש מהמשתמש או ממקום אחר..משהו שנראה ככה:
        //BO.Milestone item
        return;
    }
    static void ReadMilestoneData()
    {
        // IDצריל לבקש מהמשתמש
        return;
    }
    static void UpdateMilestoneData()
    {
        //צריך לבקש מהמשתמש או ממקום אחר..משהו שנראה ככה:
        //BO.Milestone item
        return;
    }
    static void createTask()
    {
        int id;
        BO.Task? new_task = ReadTaskFromUser();
        if (new_task != null)
        {
            id = s_bl.Task.AddTask(new_task);
            Console.WriteLine($"המשימה נוצרה בהצלחה, המספר המזהה הוא: {id}");
        }
    }//create a new task*
    static void readTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        BO.Task? task = s_bl.Task.TaskDetailsRequest(id);
        if (task != null)
        {
            Console.WriteLine(task);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Task עם ID {id} לא קיים");
        }

    }//prints the task by it's id*
    static void readAllTasks()
    {
        IEnumerable<BO.Task> tasks = (IEnumerable<BO.Task>)s_bl.Task.GetTasksList();
        if (tasks == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (BO.Task task in tasks)
        {
            Console.WriteLine(task);
        }
    }//printing  the  list of the tasks*
    static void updateTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        BO.Task? task = s_bl.Task.TaskDetailsRequest(id);
        if (task != null)
        {
            Console.WriteLine(task);
            BO.Task? updatedTask = ReadTaskFromUser();
            FieldInfo[] fields;
            if (updatedTask != null)
            {
                fields = updatedTask.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);//????

                // Loop through each field
                foreach (FieldInfo field in fields)
                {
                    object? fieldValue = field.GetValue(updatedTask);
                    if (fieldValue == null || DateTime.Equals(fieldValue, DateTime.MinValue) || (string)fieldValue == "")
                    {
                        field.SetValue(updatedTask, field.GetValue(task));
                    }
                }
                updatedTask.ID = id;//update the new task's id to be the same as the task the user want to update
                s_bl.Task.UpdateTaskData(updatedTask);
                Console.WriteLine(s_bl.Task.TaskDetailsRequest(id));//prints the task after the update
            }
        }

    }//update a specific task by the user's input* ??? we don't really understand what are we doing...
    static void deleteTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_bl.Task.DeleteTask(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");
    }//delete a task by it's id from the user's input*
    static BO.Task? ReadTaskFromUser()
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
        Console.WriteLine("1. Beginner");
        Console.WriteLine("2. Competitive");
        Console.WriteLine("3. Professional");
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
        return new BO.Task()
        {
            ID = engineerId,
            Description= description,
            Alias=   alias,
            Milestone= null,
            BaselineStartDate=null,
            ScheduledStartDate=null,
            ForecastDate =null,
            CreatedAtDate = createdate,
            StartDate = start,
            DeadlineDate= deadline,
            CompleteDate=complete,
            Deliverables= deliverables,
            Remarks=remarks,
            Engineer= null,
            ComplexityLevel=complexityLevel,
            Status = (BO.Status)0
        };
    }//reads the task values from the user*
    static void createEngineer()
    {
        BO.Engineer? newEngineer = ReadEngineerFromUser();
        if (newEngineer != null)
            s_bl.Engineer.AddEngineer(newEngineer);
    }//create a new engineer*
    static void readEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        BO.Engineer? engineer = s_bl.Engineer.EngineerDetailsRequest(id);
        if (engineer != null)
        {
            Console.WriteLine(engineer);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Engineer עם ID {id} לא קיים");
        }
    }//prints the engineer by it's id*

    static void readAllEngineers()
    {
        IEnumerable<BO.Engineer> tasks = (IEnumerable<BO.Engineer>)s_bl.Task.GetTasksList();
        // List<DO.Engineer> engineers = s_bl.Engineer.ReadAll();
        if (tasks == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (BO.Engineer engineer in tasks)
        {
            Console.WriteLine(engineer);
        }
    }//prints the all list of engineers*
    static void updateEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        BO.Engineer? engineer = s_bl.Engineer.EngineerDetailsRequest(id);
        if (engineer != null)
        {
            Console.WriteLine(engineer);
            BO.Engineer? updatedEngineer = ReadEngineerFromUser();
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
                s_bl.Engineer.UpdateEngineer(updatedEngineer);
                Console.WriteLine(s_bl.Engineer.EngineerDetailsRequest(id));//prints the updated engineer
            }
        }
    }//update a specific engineer by the user's input*

    static void deleteEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_bl.Engineer.DeleteEngineer(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");
    }//delete a engineer by it's id from the user's input*
    static BO.Engineer? ReadEngineerFromUser()
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
        Console.WriteLine("1. Beginner");
        Console.WriteLine("2. Competitive");
        Console.WriteLine("3. Professional");
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
        return new BO.Engineer()
        {
            ID = id,
            Name = name,
            Email=email,
            Level=complexityLevel,
            Cost=0,//not the real continent
            Task =null//not the real continent
        };
    }//reads the engineer values from the user
}
