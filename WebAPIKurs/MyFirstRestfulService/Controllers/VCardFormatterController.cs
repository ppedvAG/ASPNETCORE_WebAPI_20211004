using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstRestfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VCardFormatterController : ControllerBase
    {
        [HttpGet]
        public Contact GetContact() //Out Formatter 
        {
            Contact contact = new Contact();
            contact.Id = 1;
            contact.Firstname = "Otto";
            contact.Lastname = "Walkes";

            return contact;
        }


        [HttpPost]
        public IActionResult Insert(Contact contact) //Input Formatter 
        {
            return Ok();
        }
    }
}
