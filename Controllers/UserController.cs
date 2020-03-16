using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Handlers;
using Newtonsoft.Json;
using CRUDUserTable.Models;
using System.Net.Http.Headers;

namespace MyServer.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private UserEntities userEntities = new UserEntities();

        [HttpGet]
        [Route("find/{id}")]
        public HttpResponseMessage find(int id)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                result.Content = new StringContent(JsonConvert.SerializeObject
                    (userEntities.Users.Single(p => p.Id == id)));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue
                    ("application/json");
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage delete(int id)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                userEntities.Users.Remove(userEntities.Users.Single(p => p.Id == id));
                userEntities.SaveChanges();
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage create(User user)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                userEntities.Users.Add(user);
                userEntities.SaveChanges();
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public HttpResponseMessage update(User user)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = userEntities.Users.Single(p => p.Id == user.Id);

                newUser.Name = user.Name;
                newUser.Email = user.Email; ;
                newUser.Contact = user.Contact;
                newUser.Address = user.Address;

                userEntities.SaveChanges();
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

    }
}
