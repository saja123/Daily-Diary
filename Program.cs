namespace DiaryManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../mydiary.txt";
            DailyDiary diary = new DailyDiary(path);
            diary.Run();
        }
    }
}
