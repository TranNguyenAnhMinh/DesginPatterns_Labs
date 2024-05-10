using System;
using System.Collections.Generic;



//Observer được sử dụng khi một đối tượng muốn thông báo cho các đối tượng khác về sự thay đổi trạng thái của nó
//Nó cho phép một hoặc nhiều đối tượng (các Observer)
//đăng ký để theo dõi và nhận thông báo khi trạng thái của đối tượng chính (Subject) thay đổi.
namespace Observer.Structural
{
    /// <summary>
    /// Observer Design Pattern
    /// </summary>

    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Observer pattern

            ConcreteSubject s = new ConcreteSubject();

            s.Attach(new ConcreteObserver(s, "X"));
            s.Attach(new ConcreteObserver(s, "Y"));
            s.Attach(new ConcreteObserver(s, "Z"));

            // Change subject and notify observers

            s.SubjectState = "ABC";
            s.Notify();

            // Wait for user

            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>


    //Là lớp trừu tượng đại diện cho đối tượng mà các Observer muốn theo dõi.
    //Có các phương thức để thêm, loại bỏ và thông báo cho các Observer.
    public abstract class Subject
    {
        private List<Observer> observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (Observer o in observers)
            {
                o.Update();
            }
        }
    }

    /// <summary>
    /// The 'ConcreteSubject' class
    /// </summary>

    //Là lớp cụ thể của Subject, chứa thông tin trạng thái mà các Observer quan tâm.
    //Khi trạng thái thay đổi, nó gửi thông báo cho tất cả các Observer đã đăng ký.
    public class ConcreteSubject : Subject
    {
        private string subjectState;

        // Gets or sets subject state

        public string SubjectState
        {
            get { return subjectState; }
            set { subjectState = value; }
        }
    }

    /// <summary>
    /// The 'Observer' abstract class
    /// </summary>

    //Là lớp trừu tượng đại diện cho các đối tượng muốn theo dõi trạng thái của Subject.
    //Định nghĩa phương thức Update() để xử lý thông báo từ Subject.
    public abstract class Observer
    {
        public abstract void Update();
    }

    /// <summary>
    /// The 'ConcreteObserver' class
    /// </summary>

    //Là lớp cụ thể của Observer, đại diện cho các Observer cụ thể.
    //Khi nhận thông báo từ Subject, nó cập nhật trạng thái của mình dựa trên thông tin mới từ Subject.
    public class ConcreteObserver : Observer
    {
        private string name;
        private string observerState;
        private ConcreteSubject subject;

        // Constructor

        public ConcreteObserver(
            ConcreteSubject subject, string name)
        {
            this.subject = subject;
            this.name = name;
        }

        public override void Update()
        {
            observerState = subject.SubjectState;
            Console.WriteLine("Observer {0}'s new state is {1}",
                name, observerState);
        }

        // Gets or sets subject

        public ConcreteSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
}
