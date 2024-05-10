using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid
{
    internal class ViolationPrinciple
    {
        // Vi phạm Nguyên tắc trách nhiệm đơn - Single Responsibility Principle (S)
        //chứa hai công việc không liên quan đến nhau
        //=> tách các lớp riêng biệt cho FileLogger và DatabaseLogger 
        public class Logger
        {
            public void LogToFile(string message)
            {
                // Log to a file
            }

            public void LogToDatabase(string message)
            {
                // Log to a database
            }
        }

        // Vi phạm Nguyên tắc mở/đóng - Open/Closed Principle (O)
        //Lớp này tính toán bị phụ thuộc vào loại hình dạng của khối cần tính
        // Mỗi lần tính toán phải đưa ra phương thức riêng cho mỗi loại hình
        //=> Sử dụng đa hình và triển khai giao diện IShape với phương thức Area().
        //Mỗi lớp hình dạng (Rectangle, Circle, v.v.) sẽ triển khai IShape
        //và cung cấp tính toán diện tích cụ thể của nó.
        public class AreaCalculator
        {
            public double CalculateArea(object shape)
            {
                if (shape is Rectangle)
                {
                    var rectangle = (Rectangle)shape;
                    return rectangle.Width * rectangle.Height;
                }

                if (shape is Circle)
                {
                    var circle = (Circle)shape;
                    return Math.PI * Math.Pow(circle.Radius, 2);
                }
                // More shapes to be added in the future will require modifying this class

                throw new ArgumentException("Unsupported shape");
            }
        }
        public class Circle
        {
            public double Radius { get; set; }
        }

        //Vi phạm Nguyên tắc thay thế - Liskov Substitution Principle (L)
        //Square(Hình vuông) thừa kế từ Rectangle(Hình chữ nhật)
        //nhưng ghi đè phương thức SetDimensions để áp dụng chiều rộng và chiều cao bằng nhau
        //như thế là vi phạm nguyên tắc (L) vì đáng lẽ khi thừa kế phương thức
        //thì lớp con phải được xử lý y như phương thức đó lớp cha 
        //=>  tạo một lớp Square chuyên dụng không thừa kế từ Rectangle và có phương thức SetDimensions(double side) riêng
        public class Square : Rectangle
        {
            public void SetDimensions(double side)
            {
                // Violates Liskov Substitution Principle
                // A client might expect setting width and height separately, but it's not the case for Square.
                Width = side;
                Height = side;
            }
        }

        // vi phạm Nguyên tắc phân tách giao diện - Interface Segregation Principle (I) 
        //Giao diện IShape yêu cầu cả Area() và SetDimensions(double width, double height),
        //nhưng một lớp như Circle(hình tròn) lại không cần SetDimensions
        //Tại interface có thể linh hoạt trong hình dạng 
        //=> IShape bên Program.cs
        public interface IShape
        {
            double Area();
            void SetDimensions(double width, double height);
        }
        public class Rectangle : IShape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public double Area()
            {
                return Width * Height;
            }

            public void SetDimensions(double width, double height)
            {
                Width = width;
                Height = height;
            }
        }

        //Vi phạm Nguyên tắc đảo ngược phụ thuộc Dependency Inversion Principle (D) 
        // Lớp AreaCalculatorWithLogger phụ thuộc trực tiếp vào lớp Logger
        //khiến việc kiểm tra và thay đổi hành vi ghi nhật ký trở nên khó khăn
        //=> Tạo interface ILogger và thông qua constructor để ghi log kết quả tính toán.
        public class AreaCalculatorWithLogger
        {
            public AreaCalculatorWithLogger(Logger logger)
            {
                this.logger = logger;
            }

            private readonly Logger logger;

            public double CalculateArea(IShape shape)
            {
                var area = shape.Area();

                // Violates Dependency Inversion Principle
                // The high-level module (AreaCalculatorWithLogger) depends on a low-level module (Logger).
                // Both should depend on abstractions.
                logger.LogToFile($"Calculated area: {area}");

                return area;
            }
        }



        public class BadClient
        {
            public static void Run()
            {
                var rect = new Rectangle { Height = 1, Width = 4 };
                Console.WriteLine(rect.Area());

                var square = new Square { Height = 3, Width = 3 };
                Console.WriteLine(square.Area());

                WrongUsage(square);

                Console.WriteLine(square.Height);
                Console.WriteLine(square.Width);
                Console.WriteLine(square.Area());

                // rect 1x4=4
                // square => side: 3 => 9
                // var square = new Square(4,4)
                // square.SetDimensions(3)
                // rect.SetDimensions(3,4)
            }

            private static void WrongUsage(Rectangle rect)
            {
                rect.SetDimensions(2, 3);
            }
        }

    }
}
