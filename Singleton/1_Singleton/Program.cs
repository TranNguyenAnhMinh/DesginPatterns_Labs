using System;
using System.Threading;
using System.Threading.Tasks;


/*//Singleton là phương pháp sử dụng khi mình muốn tạo class
//mà nó chỉ có duy nhất 1 thuộc tính
//để những class khác được sử dụng nó
//đại loại giống tạo 1 biến toàn cục cho cả dự án dùng
public class Singleton
{
    private static Singleton instance;

    //private constructor này ngăn lập trình viên tạo mới Singleton
    //ở class khác bằng cách dùng keyword new Singleton() 
    private Singleton()
    {
    }

    //method này cho phép class khác dùng singleton của class này 
    //singleton được tạo 1 lần duy nhất
    //nếu singleton đã được tạo thì sẽ trả về singleton đã có
    //chưa có thì mới được tạo
    public static Singleton GetInstance()
    {
        if (instance == null)
        {
            instance = new Singleton();
        }
        return instance;
    }
}*/

//SINGLETON TRONG BÀI LAB
public class Singleton
{
    private Singleton() //private constructor
    {
        Console.WriteLine("Create Singleton");
    }

    private DateTime DateCreated { get; } = DateTime.Now; //giá trị của Singleton

    public override string ToString() 
    {
        return DateCreated.ToString("O");
    }

    //Lazy<> nghĩa là
    //tạo singleton (tức trong bài này là DateTime) khi giá trị của nó được sử dụng
    //không dùng thì không tạo
    //và tránh singleton được tạo nhiều lần trong môi trường lập trình multi-thread
    private static readonly Lazy<Singleton> lazyInstance = new Lazy<Singleton>(() => new Singleton());

    public static Singleton GetInstance()
    {
        return lazyInstance.Value;
    }
}

public class Program
{
    public static void Main()
    {
        // Thử singleton trong môi trường thread đơn
        var s1 = Singleton.GetInstance();
        var s2 = Singleton.GetInstance();


        Console.WriteLine(s1 == s2
            ? "Singleton works, both variables contain the same instance."
            : "Singleton failed, variables contain different instances.");

        // Thử singleton trong môi trường multi-thread
        //nếu datetime hiện ra giá trị giống nhau chứng tỏ singleton được tạo 1 lần duy nhất
        var tasks = new Task[5];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(() => TestSingleton());
        }
        Task.WaitAll(tasks);
    }

    private static void TestSingleton()
    {
        //tạo hoặc lấy giá trị của singleton
        var singleton = Singleton.GetInstance();
        Console.WriteLine(singleton);

    }
}
