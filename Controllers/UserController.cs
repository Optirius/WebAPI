using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DBContext _dbContext;
        public UserController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            //var users = GetUsers();
            //return Ok(users);

            try
            {
                var users = _dbContext.tblUsers.ToList();

                if(users.Count == 0)
                {
                    return StatusCode(404, "No User Found");
                }

                return Ok(users);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            //return Ok();
        }

        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] UserRequest request)
        {
            tblUser user = new tblUser();
            user.ID = request.ID;
            user.UserName = request.UserName;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.City = request.City;
            user.State = request.State;
            user.Country = request.Country;

            try
            {
                _dbContext.tblUsers.Add(user);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            var users = _dbContext.tblUsers.ToList();
            return Ok(users);
            //return Ok();
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update([FromBody] UserRequest request)
        {
            try
            {
                var user = _dbContext.tblUsers.FirstOrDefault(x => x.ID == request.ID);
                if(user == null)
                {
                    return StatusCode(404, "No User Found");
                }

                user.ID = request.ID;
                user.UserName = request.UserName;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.City = request.City;
                user.State = request.State;
                user.Country = request.Country;

                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            var users = _dbContext.tblUsers.ToList();
            return Ok(users);
        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult Delete([FromRoute]string ID)
        {
            try
            {
                var user = _dbContext.tblUsers.FirstOrDefault(x => x.ID == ID);
                if (user == null)
                {
                    return StatusCode(404, "No User Found");
                }

                _dbContext.Entry(user).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            var users = _dbContext.tblUsers.ToList();
            return Ok(users);
        }

        //private List<UserRequest> GetUsers()
        //{
        //    return new List<UserRequest>{
        //        new UserRequest { UserName = "ABC", FirstName = "Tahmid" },
        //        new UserRequest { UserName = "CDF", FirstName = "Shanta" },
        //        new UserRequest { UserName = "XYZ", FirstName = "Jenny" },
        //    };
        //}
    }
}
