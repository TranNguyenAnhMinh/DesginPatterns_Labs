//được sử dụng khi bạn muốn kết nối hai thành phần có giao diện không tương thích với nhau.
//Adapter giúp chúng tương tác với nhau
//mà không cần thay đổi mã nguồn của chúng.

//Trong bài này
//RoundHole: lỗ tròn
//RoundPeg: đinh tròn tương ứng.
//Tuy nhiên, SquarePeg là đinh vuông
//để có thể đặt đinh vuông này vừa với lỗ tròn, cần một cách để chuyển đổi (adapt) đinh vuông thành đinh tròn.

public class Program
{
    static void Main()
    {
        var hole = new RoundHole(5);
        var rpeg = new RoundPeg(5);
        if (hole.Fits(rpeg))
        {
            Console.WriteLine("Round peg r5 fits round hole r5.");
        }

        var smallSqPeg = new SquarePeg(2);
        var largeSqPeg = new SquarePeg(20);

        var smallSqPegAdapter = new SquarePegAdapter(smallSqPeg);
        var largeSqPegAdapter = new SquarePegAdapter(largeSqPeg);

        //Kiểm tra có đặt vừa khối vuông và trụ tròn không
        if (hole.Fits(smallSqPegAdapter))
        {
            Console.WriteLine("Square peg w2 fits round hole r5.");
        }

        if (!hole.Fits(largeSqPegAdapter))
        {
            Console.WriteLine("Square peg w20 does not fit into round hole r5.");
        }
    }
}

// RoundHole and RoundPeg classes
public class RoundHole
{
    private readonly double radius;
    public RoundHole(double radius)
    {
        this.radius = radius;
    }
    public double Radius => radius;

    public bool Fits(RoundPeg peg)
    {
        return peg.Radius <= radius;
    }
}

public class RoundPeg
{
    private readonly double radius;

    public RoundPeg(double radius)
    {
        this.radius = radius;
    }

    public double Radius => radius;
}



//ADAPTER=====================
// SquarePeg and SquarePegAdapter classes
public class SquarePeg
{
    private readonly double width;

    public SquarePeg(double width)
    {
        this.width = width;
    }

    public double Width => width;
}

//SquarePegAdapter là con của RoundPeg vì RoundPeg là kết quả của sự thay đổi (SquarePeg => RoundPeg)
public class SquarePegAdapter : RoundPeg
{
    private readonly SquarePeg peg;

    public SquarePegAdapter(SquarePeg peg) : base(GetRadius(peg))
    {
        this.peg = peg;
    }

    //Chuyển khối vuông thành khối tròn bằng cách tính toán dựa trên thông số khối vuông
    private static double GetRadius(SquarePeg peg)
    {
        return Math.Sqrt(Math.Pow(peg.Width / 2, 2) * 2);
    }
}

