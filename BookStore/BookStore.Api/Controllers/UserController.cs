using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using System;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        UserRepository _repository = new UserRepository();
       

        [HttpGet]
        [Route("GetUsers")]

        public IActionResult GetUsers()
        {
            return Ok(_repository.GetUsers());
            
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                User user = _repository.Login(model);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                User user = _repository.Register(model);
                if (user == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Bad request");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    
    [Route("~/api/User/Roles")]
        [HttpGet]
        public IActionResult Roles()
        {
            var roles = _repository.Roles();
            ListResponse<RoleModel> listResponse = new()
            {
                Results = roles.Results.Select(c => new RoleModel(c)),
                TotalRecords = roles.TotalRecords,
            };
            return Ok(listResponse);
        }


    }
}
