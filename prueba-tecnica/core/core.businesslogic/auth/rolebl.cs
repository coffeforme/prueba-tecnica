using core.entities.auth;
using core.globalization;
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
    public class rolebl : GenericTransactionBase<role, authContext>
    {
        public jsonresponse<roleentity> get(Guid uid)
        {
            jsonresponse<roleentity> resp = new jsonresponse<roleentity>();
            try
            {
                var result = getplain(uid);

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

        public jsonresponse<roleentity> get(roleentity data)
        {
            jsonresponse<roleentity> resp = new jsonresponse<roleentity>();
            try
            {
                var result = getplain(data.uid, data.name);
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

        public jsonresponse<List<roleentity>> getall()
        {
            jsonresponse<List<roleentity>> resp = new jsonresponse<List<roleentity>>();
            try
            {
                var result = getplain();

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

        internal List<roleentity> getplain(Guid? uid = null, string name = null)
        {
            if (uid.Equals(Guid.Empty)) uid = null;
            var resultado = (from _ in Select()
                                 //Busca por id o por uid
                             where (!uid.HasValue || (uid.HasValue && _.uid == uid.Value))
                             && (string.IsNullOrEmpty(name) || (!string.IsNullOrEmpty(name) && _.name == name))
                             select new roleentity
                             {
                                 uid = _.uid,
                                 name = _.name,
                                 description = _.description,
                             }).ToList();

            return resultado;
        }

        public jsonresponse<roleentity> save(roleentity _data, Transaction trans = Transaction.Insert)
        {
            jsonresponse<roleentity> resp = new jsonresponse<roleentity>();
            try
            {
                role ob = new role();
                ob = JsonConvert.DeserializeObject<role>(JsonConvert.SerializeObject(_data));

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
