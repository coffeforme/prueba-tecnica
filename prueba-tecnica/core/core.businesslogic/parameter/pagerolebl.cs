using core.entities.parameter;
using core.globalization;
using core.repos.MSSQL;
using core.unity.Logic;
using core.unity.Types;
using core.util.json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.businesslogic.parameter
{
    public class pagerolebl : GenericTransactionBase<pagerole, parameterContext>
    {
        public jsonresponse<pageroleentity> get(Guid id)
        {
            jsonresponse<pageroleentity> resp = new jsonresponse<pageroleentity>();
            try
            {
                var result = getplain(id);

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

        public jsonresponse<List<pageroleentity>> getall(Guid uid)
        {
            jsonresponse<List<pageroleentity>> resp = new jsonresponse<List<pageroleentity>>();
            try
            {
                var result = getplain(uid);

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

        public jsonresponse<List<pageroleentity>> getallbyrole(Guid uid)
        {
            jsonresponse<List<pageroleentity>> resp = new jsonresponse<List<pageroleentity>>();
            try
            {
                var result = getplainbyrole(uid);

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

        internal List<pageroleentity> getplain(Guid uid)
        {
            var resultado = (from _ in Context.getpages(uid)
                             select new pageroleentity
                             {
                                 id = _.id,
                                 id_role = _.id_role,
                                 id_page = _.id_page,
                                 title = _.title,
                                 icon = _.icon,
                                 route = _.route,
                                 actions = _.actions
                             }).ToList();
            return resultado;
        }

        internal List<pageroleentity> getplainbyrole(Guid uid)
        {
            var resultado = (from _ in Context.getpagesbyrole(uid)
                             select new pageroleentity
                             {
                                 id = _.id.Value,
                                 id_role = _.id_role,
                                 id_page = _.id_page,
                                 title = _.title,
                                 icon = _.icon,
                                 route = _.route
                             }).ToList();
            return resultado;
        }

        public jsonresponse<pageroleentity> save(pageroleentity _data, Transaction trans = Transaction.Insert)
        {
            jsonresponse<pageroleentity> resp = new jsonresponse<pageroleentity>();
            try
            {
                pagerole ob = new pagerole();
                ob = JsonConvert.DeserializeObject<pagerole>(JsonConvert.SerializeObject(_data));

                ob.date = DateTime.Now;

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
