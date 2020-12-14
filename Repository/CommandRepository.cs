using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Repository
{
    public class CommandRepository : ICommandRepository
    {
        private readonly DataContext _context;

        public CommandRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Command> GetAppCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(Guid id)
        {
            return _context.Commands.FirstOrDefault(x => x.Id == id);
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public void UpdateCommand(Command cmd)
        {
            //nothing to do here.
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Commands.Remove(command);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}