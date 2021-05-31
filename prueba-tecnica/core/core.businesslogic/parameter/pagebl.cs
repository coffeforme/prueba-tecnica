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
    public class pagebl : GenericTransactionBase<page, parameterContext>
    {
        public jsonresponse<pageentity> get(int id)
        {
            jsonresponse<pageentity> resp = new jsonresponse<pageentity>();
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

        public jsonresponse<pageentity> get(pageentity data)
        {
            jsonresponse<pageentity> resp = new jsonresponse<pageentity>();
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

        public jsonresponse<List<pageentity>> getall()
        {
            jsonresponse<List<pageentity>> resp = new jsonresponse<List<pageentity>>();
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

        internal List<pageentity> getplain(int? id = null)
        {
            var resultado = (from _ in Select()
                                 //Busca por id o por uid
                             where (!id.HasValue || (id.HasValue && _.id == id.Value))
                             select new pageentity
                             {
                                 id = _.id,
                                 title = _.title,
                                 icon = _.icon,
                                 route = _.route
                             }).ToList();

            return resultado;
        }

        public jsonresponse<pageentity> save(pageentity _data, Transaction trans = Transaction.Insert)
        {
            jsonresponse<pageentity> resp = new jsonresponse<pageentity>();
            try
            {
                page ob = new page();
                ob = JsonConvert.DeserializeObject<page>(JsonConvert.SerializeObject(_data));

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
