//mục tiêu của FACTORY là cung cấp một Interface để tạo đối tượng trong một lớp cha,
//nhưng cho phép các lớp con thay đổi các phương thức đã được tạo ở lớp cha (override) 

// Interface IAnimal để định nghĩa các phương thức chung cho các loài động vật
public interface IAnimal
{
    void Action();
    void Speak();
}

// Lớp trừu tượng Animal là lớp cha cho các lớp động vật cụ thể
public abstract class Animal : IAnimal
{
    public abstract void Action();

    public abstract void Speak();
}

// Lớp Tiger kế thừa từ lớp Animal
public class Tiger : Animal
{
    public override void Action()
    {
        Console.WriteLine("Con hổ đang gầm gừ");
    }

    public override void Speak()
    {
        Console.WriteLine("Gừm gừm!");
    }
}

// Lớp Dog kế thừa từ lớp Animal
public class Dog : Animal
{
    public override void Action()
    {
        Console.WriteLine("Con chó đang sủa");
    }

    public override void Speak()
    {
        Console.WriteLine("Gâu gâu!");
    }
}

// Lớp SimpleFactory là lớp factory để tạo ra các đối tượng động vật
public class SimpleFactory
{
    public IAnimal CreateAnimal(string animalType)
    {
        switch (animalType)
        {
            case "tiger":
                return new Tiger();
            case "dog":
                return new Dog();
            default:
                throw new ArgumentException("Loại động vật không hợp lệ: " + animalType);
        }
    }
}

// Lớp Main để sử dụng lớp SimpleFactory
public class AnimalFactoryProgram
{
    public static void Main(string[] args)
    {
        // Tạo ra một lớp SimpleFactory
        SimpleFactory factory = new SimpleFactory();

        // Tạo ra một con hổ
        IAnimal tiger = factory.CreateAnimal("tiger");
        tiger.Action();
        tiger.Speak();

        // Tạo ra một con chó
        IAnimal dog = factory.CreateAnimal("dog");
        dog.Action();
        dog.Speak();
    }
}
