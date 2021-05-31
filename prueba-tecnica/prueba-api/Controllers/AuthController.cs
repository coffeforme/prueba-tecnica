using core.businesslogic.auth;
using core.entities.auth;
using core.unity.Types;
using core.util.json;
using core.util.Middleware;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace prueba_api.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/v1/auth")]
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("ping")]
        public IHttpActionResult EchoPing()
        {
            return Ok("Pong");
        }


        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            Guid id_user;
            jsonresponse<userentity> respbl = new jsonresponse<userentity>();
            if (!string.IsNullOrEmpty(claim.Value))
            {
                id_user = Guid.Parse(claim.Value);

                using (userbl bl = new userbl())
                    respbl = bl.get(uid: id_user);
            }

            return Ok(respbl);

        }
        [HttpPost]
        [Route("signin")]
        public IHttpActionResult Authenticate(userentity login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            jsonresponse<userentity> respbl = new jsonresponse<userentity>();
            using (userbl bl = new userbl())
                respbl = bl.CredentialValid(login);
            if (respbl.done)
            {
                jsonresponse resp = new jsonresponse(respbl.copyfromtyped());
                resp.data = TokenGenerator.GenerateTokenJwt(login.email, respbl.data.uid.ToString(), respbl.data.role);
                return Ok(resp);
            }
            else
            {
                jsonresponse resp = new jsonresponse(respbl.copyfromtyped());
                return Ok(resp);
            }
        }

        [HttpPost]
        [Route("signup")]
        public IHttpActionResult Register(userentity data)
        {
            if (data == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            jsonresponse<userentity> respbl;
            using (userbl bl = new userbl())
                respbl = bl.save(data, Transaction.Insert, new roleentity { uid = data.id_rol });

            return Ok(respbl);
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(userentity data)
        {
            if (data == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            jsonresponse<userentity> respbl;
            using (userbl bl = new userbl())
                respbl = bl.save(data, Transaction.Update, new roleentity { uid = data.id_rol });


            return Ok(respbl);
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(userentity data)
        {
            if (data == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            jsonresponse<userentity> respbl;
            using (userbl bl = new userbl())
                respbl = bl.save(data, Transaction.Delete);

            return Ok(respbl);
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            jsonresponse<List<userentity>> resp = null;

            using (userbl bl = new userbl())
                resp = bl.getall();

            return Ok(resp);
        }
    }
}