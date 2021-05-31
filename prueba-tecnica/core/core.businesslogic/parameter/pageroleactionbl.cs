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
    public class pageroleactionbl : GenericTransactionBase<pageroleaction, parameterContext>
    {
         public jsonresponse<pageroleactionentity> get(int id)
        {
            jsonresponse<pageroleactionentity> resp = new jsonresponse<pageroleactionentity>();
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

        public jsonresponse<pageroleactionentity> get(pageroleactionentity data)
        {
            jsonresponse<pageroleactionentity> resp = new jsonresponse<pageroleactionentity>();
            try
            {
                var result = getplain(data.id);
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

        public jsonresponse<List<pageroleactionentity>> getall()
        {
            jsonresponse<List<pageroleactionentity>> resp = new jsonresponse<List<pageroleactionentity>>();
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

        internal List<pageroleactionentity> getplain(int? id = null)
        {
            var resultado = (from _ in Select()
                                 //Busca por id o por uid
                             where (!id.HasValue || (id.HasValue && _.id == id.Value))
                             select new pageroleactionentity
                             {
                                 id = _.id,
                                 id_pagerole = _.id_pagerole,
                                 id_action = _.id_action
                             }).ToList();

            return resultado;
        }

        public jsonresponse<pageroleactionentity> save(pageroleactionentity _data, Transaction trans = Transaction.Insert)
        {
            jsonresponse<pageroleactionentity> resp = new jsonresponse<pageroleactionentity>();
            try
            {
                pageroleaction ob = new pageroleaction();
                ob = JsonConvert.DeserializeObject<pageroleaction>(JsonConvert.SerializeObject(_data));

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
