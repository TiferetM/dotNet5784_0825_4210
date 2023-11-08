namespace DalTest;
using DalApi;
using DO;
public static class Initialization
{ 
    private static IEngineer? s_dalEngineer; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IDependency? s_dalDependency; //stage 1

    private static readonly Random s_rand = new();
    public static void createEngineer()
    {
        string[] names = { "Nikola", "Tesla", " Edison", " Alexander", " Graham" };
        int MAX_ID = 400000000;
        int MIN_ID = 200000000;
        foreach (var _name in names)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);
            int cost = s_rand.Next(30, 100);
            EngineerExperience level = (EngineerExperience)s_rand.Next(0, 3);//שגיאה לתקן
            string email = names+"@gmail.com";
            email.Replace(" ", "");
            Engineer newEngineer = new(_id, _name, email, cost);
            s_dalEngineer!.Create(newEngineer);
        }
    }
    public static void createTask()
    {
        string[] tasks = {
    "Write a blog post",
    "Create a presentation",
    "Learn a new skill",
    "Work on a side project",
    "Volunteer your time",
    "Clean and organize your home",
    "Plan a trip",
    "Cook a new meal",
    "Read a book",
    "Exercise",
    "Spend time with loved ones",
    "Write a thank-you note",
    "Donate to a charity",
    "Meditate",
    "Take a walk in nature",
    "Learn a new language",
    "Play a game",
    "Watch a documentary",
    "Take a nap",
    "Relax and do nothing",
   
 };
        string[] descriptions = {
    "Write a detailed blog post about a topic you're passionate about.",
    "Create an engaging presentation on a subject of your choice.",
    "Spend time learning a new skill that interests you.",
    "Dedicate some time to a personal side project you've been wanting to work on.",
    "Find a local charity or organization and volunteer your time to help them.",
    "Spend some time cleaning and organizing your home to create a more pleasant environment.",
    "Plan a trip to a destination you've always wanted to visit and research the best attractions and activities.",
    "Try cooking a new recipe or experimenting with different ingredients to create a delicious meal.",
    "Pick up a book that you've been meaning to read and spend some time diving into its pages.",
    "Engage in physical activity, whether it's going for a run, doing yoga, or any other form of exercise.",
    "Spend quality time with your family or friends, doing activities that you all enjoy.",
    "Write a thank-you note to someone who has made a positive impact in your life.",
    "Find a charity or cause that resonates with you and make a donation to support their work.",
    "Take some time to practice meditation and focus on your breathing and mindfulness.",
    "Go for a walk in a nearby park or nature reserve to enjoy the beauty of the outdoors.",
    "Challenge yourself to learn a new language, whether through online resources or language learning apps.",
    "Play a game that you enjoy, whether it's a board game, video game, or any other form of entertainment.",
    "Watch a documentary on a topic that interests you and expand your knowledge.",
    "Take a short nap to recharge and rejuvenate your mind and body.",
    "Give yourself permission to relax and do nothing for a while, allowing yourself to unwind and destress."
};
        string[] drlivrables = {
"A marketing plan",
"A training manual",
"A website",
"A product design",
"A report" ,
"A presentation",
"A consultation",
"A service"};
        //אם נרצה לעשות מארך רשימות חלקי
        string[] Difficulty = { "  Novice", " AdvancedBeginner", "Competent", "Proficient", " Expert" };
        List<Engineer> list = s_dalEngineer.ReadAll();
        int i = 0;
        
        foreach (var _task in tasks)
        { 
            DateTime createdat = createRandomDate();
            DateTime start = createRandomDate();//תאריך התחלה בפועל
            DateTime schedudalDate = start.AddDays(-3);//תאריך מתוכנן לתחילת ביצוע
            DateTime Complete = start.AddDays(s_rand.Next(0, 35));//תאריך סיום
            DateTime DeadLine = Complete.AddDays(3);//דד-ליין
            string ComplexityLevel = Difficulty[s_rand.Next(0, 5)];//רמת קושי
            string Delivrables = drlivrables[s_rand.Next(0, 7)];
            string Remarks="remark";// למשימה הערות
            int randID = list[s_rand.Next(0, 39)].Id;
            int Engineerid = randID;
            i++;
            Task newtask = new(0, descriptions[i], _task, false, createdat,
                start, schedudalDate, DeadLine, Complete, Delivrables,Remarks,Engineerid, ComplexityLevel);
            s_dalTask!.Create(newtask);
        }
    }
    private static void createDependencies()//initialize the dependency's list with random values
    {
        for(int i = 0; i < 250; i++) 
        {
            Dependency item= new Dependency();
            List<Task> tasks_list = s_dalTask.ReadAll();//get the tasks list in order to get a random task id's that exists 
            int randomIndex1 = s_rand.Next(0, tasks_list.Count-1);
            int randomIndex2 = s_rand.Next(0, tasks_list.Count - 1);
            item.DependenceTask = tasks_list[randomIndex1].Id;
            item.DependenceOnTask = tasks_list[randomIndex2].Id;
            s_dalDependency!.Create(item);
        }

    }
    public static void Do(IDependency? dalDependency, ITask? dalTask, IEngineer? dalEngineer)
    {
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        createEngineer();
        createTask();
        createDependencies();

    }
    public static DateTime createRandomDate()
    {
        int day = s_rand.Next(1, 30);
        int month = s_rand.Next(1, 12);
        int year = 2023;
        int howr = s_rand.Next(1, 24);
        int minute = s_rand.Next(1, 60);
        int second = s_rand.Next(1, 60);
        DateTime newDate = new DateTime(year, month, day, howr, minute, second);
        return newDate;
    }
}
