using System;
using AutoMapper;
using Commander.DTOModels;
using Commander.Models;
using Commander.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly  IAuthenticateRepository _repository;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //GET api/users
        [HttpGet]
        public ActionResult<UserModel> GetAllUsers()
        {
            var users = _repository.ListUsers();
            return Ok(users);
        }
        
        //POST api/users/authenticate
        [HttpPost("authenticate")]
        public ActionResult<ResponseModelDTO> Authenticate([FromBody] UserCreateDTO user)
        {
            var userModel = _mapper.Map<UserModel>(user);
            var response =_repository.Authenticate(userModel);
            _repository.SaveChanges();
            return Ok(response);
        }
       
    }
}