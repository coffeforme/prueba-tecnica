using core.businesslogic.auth;
using core.entities.auth;
using core.util.json;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace prueba_api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/auth/role")]
    public class RoleController : ApiController
    {
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            jsonresponse<List<roleentity>> resp = null;
            using (rolebl bl = new rolebl())
            {
                resp = bl.getall();
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get(Guid id)
        {
            jsonresponse<roleentity> resp = null;
            using (rolebl bl = new rolebl())
            {
                resp = bl.get(id);
            }
            return Ok(resp);
        }

    }
}