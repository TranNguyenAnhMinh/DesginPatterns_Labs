//Decorator được sử dụng để mở rộng hoặc thay đổi hành vi của một đối tượng
//mà không cần phải thay đổi mã nguồn của đối tượng đó
//bằng cách bọc tối tượng này bên trong các lớp decorator

using System.Text;

public class Program
{
    static void Main()
    {
        var ds = new EncrypstionDecorator(new FileDataSource("salary.dat"));
        ds.WriteData("100");
        Console.WriteLine(ds.ReadData());
    }
}

//Đối tượng chính
 public interface IDataSource
{
    void WriteData(string data);
    string ReadData();
}
//Đối tượng ban đầu (sẽ được bao quanh bởi các decorator khác bên dưới)
public class FileDataSource:IDataSource
{
    private string fileName { get; set; }
    public FileDataSource(string fileName)
    {
        this.fileName = fileName;
    }

    public void WriteData(string data)
    {
        using(StreamWriter sw= new StreamWriter(fileName)) 
        { 
            sw.WriteLine(data);
        }
    }

    public string ReadData()
    {
        using (StreamReader sr = new StreamReader(fileName))
        {
            return sr.ReadToEnd();
        }
    }
}

//Lớp này thêm chức năng ghi log vào phương thức WriteData() của IDataSource.
public class LoggingDecorater : IDataSource
{
    private IDataSource Source { get; set; }
    public LoggingDecorater(IDataSource source)
    {
        this.Source = source;
    }
    public string ReadData()
    {
       
       return Source.ReadData();
    }

    public void WriteData(string data)
    {
        Console.Write("...");
        Source.WriteData(data);
    }
}

//Lớp này thêm chức năng mã hóa dữ liệu trước khi ghi vào IDataSource bằng cách sử dụng mã hóa Base64.
//Đồng thời, nó cũng giải mã dữ liệu khi đọc từ IDataSource.
public class EncrypstionDecorator :IDataSource
{
    private IDataSource Source { get; set; }
    public EncrypstionDecorator(IDataSource source)
    {
        this.Source = source;
    }
    public string ReadData()
    {
        string readData = Source.ReadData();
        string decodedString = DecodeBase64(readData);
        return decodedString;
    }
    public static string DecodeBase64(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;
        var valueBytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(valueBytes);
    }

    public void WriteData(string data)
    {
        string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        Source.WriteData(base64String);
    }
}

//Lớp này có thể được triển khai để thêm chức năng nén dữ liệu trước khi ghi vào IDataSource,
//và giải nén dữ liệu khi đọc từ IDataSource.
public class CompressionDecorator : IDataSource
{
    private IDataSource Source { get; set; }
    public CompressionDecorator(IDataSource source)
    {
        this.Source = source;
    }
    public string ReadData()
    {
        throw new NotImplementedException();
    }

    public void WriteData(string data)
    {
        throw new NotImplementedException();
    }
}
