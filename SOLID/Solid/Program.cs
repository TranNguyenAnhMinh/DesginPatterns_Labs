//Nguyên tắc SOLID không phải là mẫu thiết kế,
//mà là các hướng dẫn cơ bản để tạo mã hướng đối tượng có cấu trúc tốt, dễ bảo trì và linh hoạt
//Tuy nhiên, nhiều mẫu thiết kế dựa trên nguyên tắc SOLID để đảm bảo hiệu quả của chúng



// Định nghĩa phương thức Area() để tính diện tích
//Không phụ thuộc vào hình cụ thể nào
//Áp dụng Nguyên tắc trách nhiệm đơn (Single Responsibility)
//và nguyên tắc mở/đóng (Open/Closed)
public interface IShape
{
    double Area();
}

// Class hình chữ nhật kế thừa phương thúc tính diện tích Area() từ IShape (Open/Closed)
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public double Area()
    {
        return Width * Height;
    }
}

// Class hình tròn kế thừa phương thúc tính diện tích Area() từ IShape (Open/Closed)
public class Circle : IShape
{
    public double Radius { get; set; }

    public double Area()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }
}

//Có phương thức riêng SetDimensions để thiết lập kích thước cạnh hình vuông,
//tuân theo nguyên tắc thay thế Liskov (Liskov Substitution Principle).
//Không kế thừa từ IShape
public class Square
{
    public double Side { get; set; }

    public double Area()
    {
        return Side * Side;
    }

    public void SetDimensions(double side)
    {
        Side = side;
    }
}

//Định nghĩa các phương thức để ghi nhật ký ra file (LogToFile) và database (LogToDatabase).
//(Nguyên tắc trách nhiệm đơn)
public interface ILogger
{
    void LogToFile(string message);
    void LogToDatabase(string message); 
}

// Triển khai interface ILogger để ghi nhật ký ra file và database tương ứng.
// (Nguyên tắc trách nhiệm đơn)
public class FileLogger : ILogger
{
    public void LogToFile(string message)
    {
        Console.WriteLine($"Logged to file: {message}");
    }

    public void LogToDatabase(string message)
    {
        throw new NotImplementedException("Not implemented in FileLogger");
    }
}
public class DatabaseLogger : ILogger
{
    public void LogToFile(string message)
    {
        throw new NotImplementedException("Not implemented in DatabaseLogger");
    }

    public void LogToDatabase(string message)
    {
        Console.WriteLine($"Logged to database: {message}"); 
    }
}

//Sử dụng nguyên tắc Dependency Injection để nhận đối tượng ILogger trong constructor.
//Điều này cho phép linh hoạt chọn phương thức ghi nhật ký.
//(Nguyên tắc đảo ngược phụ thuộc (Dependency Inversion Principle))
public class AreaCalculator
{
    private readonly ILogger _logger;

    public AreaCalculator(ILogger logger) // Constructor injection
    {
        _logger = logger;
    }

    public double CalculateArea(IShape shape)
    {
        var area = shape.Area();
        _logger.LogToFile($"Calculated area: {area}"); // Can choose logging method
        return area;
    }
}

public class BadClient
{
    public static void Run()
    {
        var rect = new Rectangle { Height = 1, Width = 4 };
        Console.WriteLine(rect.Area());

        var square = new Square { Side = 3 };
        Console.WriteLine(square.Area());

        var calculator = new AreaCalculator(new FileLogger()); // Inject logger
        var circleArea = calculator.CalculateArea(new Circle { Radius = 2 });
        Console.WriteLine(circleArea);

        // Square usage with its own SetDimensions
        square.SetDimensions(5);
        Console.WriteLine(square.Area()); // Now reflects the new side
    }
}