using System;
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Repository
{
    public interface ICommandRepository
    {
        IEnumerable<Command> GetAppCommands();
        Command GetCommandById(Guid id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
        bool SaveChanges();
    }
}