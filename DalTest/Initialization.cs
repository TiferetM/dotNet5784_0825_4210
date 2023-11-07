

//namespace DalTest;
//using DalApi;
//using DO;
//using System.Diagnostics;

//public static class Initialization
//{

//    private static IEngineer? s_dalEngineer; //stage 1
//    private static ITask? s_dalTask; //stage 1
//    private static IDependency? s_dalDependency; //stage 1

//    private static readonly Random s_rand = new();
    

//    public static void createEngineer()
//    {
//        string[] names = { "Nikola", "Tesla", " Edison", " Alexander", " Graham" };
//        int maxId = 400000000;
//        int minId = 200000000;
       

//        for(int i = 0; i<5; i++)
//        {
//            int Id=s_rand.Next( minId,maxId);
//            int cost = s_rand.Next(30, 100);
//            EngineerExperience level = (EngineerExperience)s_rand.Next(0, 3);//שגיאה לתקן
//            string email = names[i]+"@gmail.com";
//            email.Replace(" ", "");
//            Engineer newEngineer = new(Id, names[i], email, cost);
//            Engineers.create(newEngineer);
//            s_dalEngineer++;
//        }
//    }
//    public static void createTask()
//    {


//    }
//    public static void createDependency()
//    {

//    }

//}
