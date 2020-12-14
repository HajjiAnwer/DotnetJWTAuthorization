using System;
using System.Collections.Generic;
using AutoMapper;
using Commander.DTOModels;
using Commander.Models;
using Commander.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    [Authorize]
    public class CommandsController : ControllerBase
    {
        
        private readonly  ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //Get api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAppCommands();
            return Ok(commandItems);
        }
        
        //Get api/commands/{id}
        [HttpGet("{id}",Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(Guid id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));
            }
            return NotFound();
        }
        
        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO command)
        {
            var commandModel = _mapper.Map<Command>(command);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandRead = _mapper.Map<CommandReadDTO>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandRead.Id}, commandRead);
        }
        
        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult<CommandReadDTO> UpdateCommand(Guid id,CommandCreateDTO command)
        {
            var commandToBeUpdated = _repository.GetCommandById(id);
            if (commandToBeUpdated == null)
            {
                return NotFound();
            }

            _mapper.Map(command,commandToBeUpdated);
            _repository.SaveChanges();
            return NoContent();
        }
        
        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PatchCommand(Guid id,JsonPatchDocument<CommandCreateDTO> patchDocument)
        {
            var commandToBePatched = _repository.GetCommandById(id);
            if (commandToBePatched == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandCreateDTO>(commandToBePatched);
            patchDocument.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch,commandToBePatched);
            _repository.SaveChanges();
            return NoContent();
        }
        
        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult CreateCommand(Guid id)
        {
            var commandToBeDeleted = _repository.GetCommandById(id);
            if (commandToBeDeleted == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandToBeDeleted);
            _repository.SaveChanges();
            return NoContent();
        }
        
    }
}