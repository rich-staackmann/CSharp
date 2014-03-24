using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //our bridge pattern
    //basically, we wanted to be able to format our output any way we want
    //rather than endlessly subclass our existing classes (backwords book, cursive book, etc)
    //we implement bridge pattern instead

    //here is our formatting interface
    public interface IFormatter
    {
        string Format(string key, string value);
    }
    //concrete formatting class (our bridge)
    public class BackwordsFormatter : IFormatter
    {
        public string Format(string key, string value)
        {
           
            return String.Format("{0}: {1}", key, new string(value.Reverse().ToArray()));
        }
    }
    //another bridge class for formatting
    public class FancyFormatter : IFormatter
    {
        public string Format(string key, string value)
        {
            return String.Format("-= {0} ----- =- {1}", key, value);
        }
    }
    //----------------------------------------------------------------------------------------
    //our abstraction
    public abstract class IManuscript
    {
        protected readonly IFormatter formatter;

        public IManuscript(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        abstract public void Print();
    }
    //----------------------------------------------------------------------------------------
    //following three classes are the implementation of our abstraction
    public class Book : IManuscript
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public Book(IFormatter formatter) : base(formatter)
        {
        }

        public override void Print()
        {
            Console.WriteLine(formatter.Format("Title", Title));
            Console.WriteLine(formatter.Format("Author", Author));
            Console.WriteLine(formatter.Format("Text", Text));
            Console.WriteLine();
        }
    }

    public class FAQ : IManuscript
    {
        public string Title { get; set; }
        public Dictionary<string, string> Questions { get; set; }

        public FAQ(IFormatter formatter) : base(formatter)
        {
            Questions = new Dictionary<string, string>();
        }

        public override void Print()
        {
            Console.WriteLine(formatter.Format("Title", Title));
            foreach (var question in Questions)
            {
                Console.WriteLine(formatter.Format("     Question", question.Key));
                Console.WriteLine(formatter.Format("     Answer", question.Value));
            }
            Console.WriteLine();
        }
    }

    public class TermPaper : IManuscript
    {
        public string Class { get; set; }
        public string Student { get; set; }
        public string Text { get; set; }
        public string References { get; set; }

        public TermPaper(IFormatter formatter) : base(formatter)
        {
        }

        public override void Print()
        {
            Console.WriteLine(formatter.Format("Class", Class));
            Console.WriteLine(formatter.Format("Student", Student));
            Console.WriteLine(formatter.Format("Text", Text));
            Console.WriteLine(formatter.Format("References", References));
            Console.WriteLine();
        }
    }
}
