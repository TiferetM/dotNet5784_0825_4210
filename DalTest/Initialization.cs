//namespace DalTest;
//using DalApi;
//using DO;
//using System.Data.Common;
//using System.Diagnostics;
//using System.Numerics;
//using System.Threading.Tasks;
//public static class Initialization
//{

//    private static IEngineer? s_dalEngineer; //stage 1
//    private static ITask? s_dalTask; //stage 1
//    private static IDependency? s_dalDependency; //stage 1

//    private static readonly Random s_rand = new();
//    public static void createEngineer()
//    {
//        string[] names = { "Nikola", "Tesla", " Edison", " Alexander", " Graham" };
//        int MAX_ID = 400000000;
//        int MIN_ID = 200000000;
//    foreach(var _name in names)
//    {
//        int _id;
//        do
//            _id = s_rand.Next(MIN_ID, MAX_ID);
//        while (s_dalEngineer!.Read(_id) != null);
//        int cost = s_rand.Next(30, 100);
//            EngineerExperience level = (EngineerExperience)s_rand.Next(0, 3);//שגיאה לתקן
//            string email = names+"@gmail.com";
//            email.Replace(" ", "");
//            Engineer newEngineer = new(_id,_name, email, cost);
//            s_dalEngineer!.Create(newEngineer);
//        }
//    }

//    public static void createTask(object DataSource)
//    {
//        string[] tasks = {
//    "Write a blog post",
//    "Create a presentation",
//    "Learn a new skill",
//    "Work on a side project",
//    "Volunteer your time",
//    "Clean and organize your home",
//    "Plan a trip",
//    "Cook a new meal",
//    "Read a book",
//    "Exercise",
//    "Spend time with loved ones",
//    "Write a thank-you note",
//    "Donate to a charity",
//    "Meditate",
//    "Take a walk in nature",
//    "Learn a new language",
//    "Play a game",
//    "Watch a documentary",
//    "Take a nap",
//    "Relax and do nothing",
//    "Do some journaling",
//    "Organize your photos",
//    "Learn to code",
//    "Write a poem",
//    "Try a new food",
//    "Start a new hobby",
// };
//        foreach (var _task in tasks)
//        {
//            DO.Task newtask =new(0,"",);
//            s_dalTask!.Create(newtask);

//        //foreach (var _task in tasks)
//        //{
//        //    int _id;
//        //    do

//        //    while (s_dalEngineer!.Read(_id) != null);
//        //    int cost = s_rand.Next(30, 100);
//        //    EngineerExperience level = (EngineerExperience)s_rand.Next(0, 3);//שגיאה לתקן
//        //    string email = names + "@gmail.com";
//        //    email.Replace(" ", "");
//        //}
//    }


//}
//    public static void createDependency()
//    {

//    }

//}
