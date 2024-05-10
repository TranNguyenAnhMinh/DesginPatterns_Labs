//Composite pattern cho phép tạo ra các cấu trúc cây từ các đối tượng đơn lẻ
//và xử lý các cấu trúc đó theo một cách thống nhất.
using System.Text;

// Lớp cơ sở cho tất cả các đối tượng
//Chứa các thuộc tính chung
public abstract class GraphObject
{
    public string Name { get; set; }
    public string Color { get; set; }
    public List<GraphObject> Children { get; set; }

    public GraphObject(string name, string color)
    {
        Name = name;
        Color = color;
        Children = new List<GraphObject>();
    }

    public abstract void Print(StringBuilder sb, int depth);

    public override string ToString()
    {
        var sb = new StringBuilder();
        Print(sb, 0);
        return sb.ToString();
    }
}

// Lớp Square và lớp Circle 
//Là các lớp đối tượng đơn lẻ không có con. Chúng kế thừa từ GraphObject 
public class Square : GraphObject
{
    public Square() : base("Square", "Green") { }

    public override void Print(StringBuilder sb, int depth)
    {
        sb.Append($"{new string(' ', depth * 2)}{Name}: {Color}\n");
    }
}

// Lớp Circle
public class Circle : GraphObject
{
    public Circle() : base("Circle", "Red") { }

    public override void Print(StringBuilder sb, int depth)
    {
        sb.Append($"{new string(' ', depth * 2)}{Name}: {Color}\n");
    }
}

// Lớp Group là một lớp con của GraphObject
//có thể chứa nhiều đối tượng khác
public class Group : GraphObject
{
    public Group() : base("Group", "Black") { }

    public override void Print(StringBuilder sb, int depth)
    {
        sb.Append($"{new string(' ', depth * 2)}{Name}: {Color}\n");

        foreach (var child in Children)
        {
            child.Print(sb, depth + 1);
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        //cách Group chứa nhiều đối tượng
        var group = new Group();
        group.Children.Add(new Square());
        group.Children.Add(new Circle());

        Console.WriteLine(group.ToString());

    }
}
