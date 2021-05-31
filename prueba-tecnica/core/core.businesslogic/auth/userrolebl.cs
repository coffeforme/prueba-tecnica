using core.entities.auth;
using core.globalization;
using core.repos;
using core.repos.MSSQL;
using core.unity.Logic;
using core.unity.Types;
using core.util.json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.businesslogic.auth
{
    public class userrolebl : GenericTransactionBase<userrole, authContext>
    {
        public jsonresponse<userroleentity> get(Guid id_user)
        {
            jsonresponse<userroleentity> resp = new jsonresponse<userroleentity>();
            try
            {
                var result = getplain(id_user);

                if (result.Count > 0)
                {
                    resp.done = true;
                    resp.status = jsonstatus.success;
                    resp.data = result.FirstOrDefault();
                }
                else
                {
                    resp.done = false;
                    resp.status = jsonstatus.info;
                    resp.response = commonres.register_not_found;
                }


            }
            catch (Exception ex)
            {
                resp.done = false;
                resp.error = ex;
                resp.response = ex.Message;
            }
            return resp;
        }

        public jsonresponse<userroleentity> get(userroleentity data)
        {
            jsonresponse<userroleentity> resp = new jsonresponse<userroleentity>();
            try
            {
                var result = getplain(data.uid);
                if (result.Count > 0)
                {
                    resp.done = true;
                    resp.status = jsonstatus.success;
                    resp.data = result.FirstOrDefault();
                }
                else
                {
                    resp.done = false;
                    resp.status = jsonstatus.info;
                    resp.response = commonres.register_not_found;
                }

            }
            catch (Exception ex)
            {
                resp.done = false;
                resp.error = ex;
                resp.response = ex.Message;
            }
            return resp;
        }

        public jsonresponse<List<userroleentity>> getall(Guid id_user)
        {
            jsonresponse<List<userroleentity>> resp = new jsonresponse<List<userroleentity>>();
            try
            {
                var result = getplain(id_user);

                resp.done = true;
                resp.data = result;
                resp.status = jsonstatus.success;

            }
            catch (Exception ex)
            {
                resp.done = false;
                resp.error = ex;
                resp.response = ex.Message;
            }
            return resp;
        }

        internal List<userroleentity> getplain(Guid id_user)
        {
            var resultado = (from _ in Select()
                             where _.id_user == id_user
                             select new userroleentity
                             {
                                 uid = _.uid,
                                 date = _.date,
                                 id_user = _.id_user,
                                 id_role = _.id_role,

                             }).ToList();

            return resultado;
        }

        public jsonresponse<userroleentity> save(userroleentity _data, Transaction trans = Transaction.Insert)
        {
            jsonresponse<userroleentity> resp = new jsonresponse<userroleentity>();
            try
            {
                userrole ob = new userrole();
                ob = JsonConvert.DeserializeObject<userrole>(JsonConvert.SerializeObject(_data));

                resp.done = UpdateSaveChanges(ob, trans);

                HandleResponse(resp, trans, ob);
            }
            catch (Exception ex)
            {
                resp.done = false;
                resp.error = ex;
                resp.response = ex.Message;
            }

            return resp;
        }
    }
}
