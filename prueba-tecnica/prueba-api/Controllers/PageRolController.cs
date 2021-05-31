using core.businesslogic.parameter;
using core.entities.parameter;
using core.unity.Types;
using core.util.json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;


namespace prueba_api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/parameter/pagerole")]
    public class PageRoleController : ApiController
    {
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save([FromBody] pageroleentity data)
        {
            jsonresponse<pageroleentity> resp = null;
            using (pagerolebl bl = new pagerolebl())
            {
                resp = bl.save(data, data.id > 0 ? Transaction.Delete : Transaction.Insert);
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            Guid id_user;
            jsonresponse<List<pageroleentity>> resp = null;

            if (!string.IsNullOrEmpty(claim.Value))
            {
                id_user = Guid.Parse(claim.Value);

                using (pagerolebl bl = new pagerolebl())
                {
                    resp = bl.getall(id_user);
                }
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("getallbyrole")]
        public IHttpActionResult GetAllByRole(Guid uid_role)
        {
            jsonresponse<List<pageroleentity>> resp = null;
            using (pagerolebl bl = new pagerolebl())
            {
                resp = bl.getallbyrole(uid_role);
            }
            return Ok(resp);
        }

        [HttpDelete]
        [Route("drop")]
        public IHttpActionResult Drop([FromBody] pageroleentity data)
        {
            jsonresponse<pageroleentity> resp = null;
            using (pagerolebl bl = new pagerolebl())
            {
                resp = bl.save(data, Transaction.Delete);
            }
            return Ok(resp);
        }
    }
}