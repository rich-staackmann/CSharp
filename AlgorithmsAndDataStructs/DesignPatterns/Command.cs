using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //a class that will use commands
    //initially, we were using a switch statement to parse a list of command line arguments
    public class Command
    {
        public static void Run(string[] args)
        {
            var availableCommands = GetAvailableCommands();

            if (args.Length == 0)
            {
                Console.Write("please supply args");
                return;
            }

            var parser = new CommandParser(availableCommands);
            var command = parser.ParseCommand(args);

            if (command != null)
                command.Execute();
        }

        public static IEnumerable<ICommandFactory> GetAvailableCommands()
        {
            return new ICommandFactory[] { new UpdateQuantityCommand(), new CreateOrderCommand()};
        }
    }
    //----------------------------------------------------------------------------------------
    //our command parser
    public class CommandParser
    {
        readonly IEnumerable<ICommandFactory> availableCommands;

        public CommandParser(IEnumerable<ICommandFactory> availableCommands)
        {
            this.availableCommands = availableCommands;
        }

        internal ICommand ParseCommand(string[] args)
        {
            var requestedCommand = args[0];
            var command = FindRequestedCommand(requestedCommand);
            return command.MakeCommand(args);
        }
        ICommandFactory FindRequestedCommand(string command)
        {
            return availableCommands.FirstOrDefault(c => c.CommandName == command);
        }
    }
    //----------------------------------------------------------------------------------------
    //this is our command interface, it will define what a command will do
    //common examples are Validate(), Execute(), and Undo()
    public interface ICommand
    {
        void Execute();
    }
    //a command factory
    public interface ICommandFactory
    {
        string CommandName { get; }
        string Description { get; }

        ICommand MakeCommand(string[] args);
    }
    //a concrete command object
    public class UpdateQuantityCommand : ICommand , ICommandFactory
    {
        public int NewQuantity { get; set; }

        public void Execute()
        {
            //simulate updating a db
            int oldQuantity = 5;
            Console.WriteLine("DB updated!");
            //simulate logging
            Console.WriteLine("LOG: Updated quantity from {0} to {1}", oldQuantity, NewQuantity);
        }

        public string CommandName { get { return "UpdateQuantity"; } }
        public string Description { get { return "Updates quantity"; } }

        public ICommand MakeCommand(string[] args)
        {
            return new UpdateQuantityCommand { NewQuantity = int.Parse(args[1]) };
        }
    }
    //another command
    public class CreateOrderCommand : ICommand, ICommandFactory
    {
        public int NewQuantity { get; set; }

        public void Execute()
        {
            //simulate updating a db
            int oldQuantity = 5;
            Console.WriteLine("DB updated!");
            //simulate logging
            Console.WriteLine("LOG: Updated quantity from {0} to {1}", oldQuantity, NewQuantity);
        }

        public string CommandName { get { return "CreateOrder"; } }
        public string Description { get { return "Creates new order"; } }

        public ICommand MakeCommand(string[] args)
        {
            return new CreateOrderCommand { NewQuantity = int.Parse(args[1]) };
        }
    }
}
