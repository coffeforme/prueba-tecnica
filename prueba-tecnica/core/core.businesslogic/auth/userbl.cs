using core.entities.auth;
using core.globalization;
using core.repos.MSSQL;
using core.unity.Logic;
using core.unity.Types;
using core.util;
using core.util.json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.businesslogic.auth
{
    public class userbl : GenericTransactionBase<user, authContext>
    {
        public jsonresponse<userentity> get(string username = null, Guid? uid = null)
        {
            jsonresponse<userentity> resp = new jsonresponse<userentity>();
            try
            {
                var result = getplain(username, uid);

                resp.done = true;
                resp.data = result.FirstOrDefault();
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

        public jsonresponse<List<userentity>> getall()
        {
            jsonresponse<List<userentity>> resp = new jsonresponse<List<userentity>>();
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

        public jsonresponse<userentity> CredentialValid(userentity data)
        {
            jsonresponse<userentity> resp = new jsonresponse<userentity>();
            try
            {
                var result = getfullplain(data.username);

                resp.done = result.Count > 0;
                if (resp.done)
                {
                    resp.data = result.FirstOrDefault();
                    if (!PBKDF2.PasswordValid(data.pass, resp.data.password, resp.data.salt))
                        goto DENIED;
                    else
                        goto GRANT;
                }
                else
                    goto DENIED;

                DENIED:
                resp.response = commonres.credentials_invalid;
                resp.status = jsonstatus.warning;
                resp.done = false;
                goto END;

            GRANT:
                resp.status = jsonstatus.success;
            }
            catch (Exception ex)
            {
                resp.done = false;
                resp.error = ex;
                resp.response = ex.Message;
            }

        END:
            //Se limpia cualquier dato usado para la operacion
            resp.data = resp.data.MapProperties<userentity>();
            return resp;
        }

        internal List<userentity> getfullplain(string username)
        {
            var resultado = (from _ in Select()
                             where (string.IsNullOrEmpty(username) || (!string.IsNullOrEmpty(username) && _.username == username))
                             select new userentity
                             {
                                 uid = _.uid,
                                 username = _.username,
                                 password = _.password,
                                 email = _.email,
                                 firstname = _.firstname,
                                 lastname = _.lastname,
                                 fullname = _.fullname,
                                 cel = _.cel,
                                 address = _.address,
                                 salt = _.salt,
                                 requirechangepass = _.requirechangepass,
                                 confirmationcode = _.confirmationcode,
                                 sayhi = _.sayhi,
                                 emailconfirmed = _.emailconfirmed,
                                 state = _.state,
                                 date = _.date
                             }).ToList();

            return resultado;
        }
        internal List<userentity> getplain(string username = null, Guid? uid = null)
        {
            var resultado = (from _ in Context.getuser(username, uid)
                             select new userentity
                             {
                                 username = _.username,
                                 email = _.email,
                                 firstname = _.firstname,
                                 lastname = _.lastname,
                                 fullname = _.fullname,
                                 cel = _.cel,
                                 address = _.address,
                                 requirechangepass = _.requirechangepass,
                                 confirmationcode = _.confirmationcode,
                                 sayhi = _.sayhi,
                                 emailconfirmed = _.emailconfirmed,
                                 state = _.state,
                                 date = _.date,
                                 role = _.description,
                                 id_rol = _.id_role
                             }).ToList();

            return resultado;
        }

        public jsonresponse<userentity> save(userentity _data, Transaction trans = Transaction.Insert, roleentity rol = null)
        {
            jsonresponse<userentity> resp = new jsonresponse<userentity>();
            try
            {
                var result = getfullplain(_data.username);

                userentity old = result.FirstOrDefault();

                user ob = new user();
                ob = JsonConvert.DeserializeObject<user>(JsonConvert.SerializeObject(_data));
                switch (trans)
                {
                    case Transaction.Insert:
                        //Si no existe un usuario ya registrado con ese username
                        if (result.Count > 0)
                        {
                            resp.status = jsonstatus.warning;
                            resp.response = commonres.user_exists;
                        }
                        else
                        {
                            var pass = PBKDF2.HashPassword(_data.pass);
                            ob.password = pass.password;
                            ob.salt = pass.salt;
                            ob.date = DateTime.Now;

                            //Si se esta creando y trae un rol lo agrega a la lista de 
                            //userrol para que el EF lo cree automaticamente
                            if (rol != null)
                            {
                                ob.userrole.Add(new userrole
                                {
                                    id_role = rol.uid,
                                    date = DateTime.Now
                                });
                                resp.done = UpdateSaveChanges(ob, trans);

                                HandleResponse(resp, trans, ob);
                            }
                        }
                        break;
                    case Transaction.Update:
                        //Si no existe un usuario ya registrado con ese username distinto al modificando
                        if (result.Count > 0 && old.uid != ob.uid)
                        {
                            resp.status = jsonstatus.warning;
                            resp.response = commonres.user_exists;
                        }
                        else
                        {
                            //conserva su password (Solo deberia cambiarla el propio usuario por una opcion especifica de cambio de contraseña)
                            ob.password = old.password;
                            ob.salt = old.salt;
                            ob.date = DateTime.Now;

                            //Si el rol es distinto al que tiene actual lo elimina y lo agrega a trave de su BL
                            if (rol.uid != old.id_rol)
                            {
                                jsonresponse<userroleentity> respur;

                                using (userrolebl urbl = new userrolebl())
                                {
                                    respur = urbl.get(ob.uid);
                                    if (respur.done)
                                        respur = urbl.save(respur.data, Transaction.Delete);
                                    if (respur.done)
                                        respur = urbl.save(new userroleentity
                                        {
                                            id_role = rol.uid,
                                            id_user = ob.uid,
                                            date = DateTime.Now
                                        }, Transaction.Insert);

                                }

                                resp.done = UpdateSaveChanges(ob, trans);
                                HandleResponse(resp, trans, ob);
                            }


                        }
                        break;
                    case Transaction.Delete:

                        resp.done = UpdateSaveChanges(ob, trans);

                        HandleResponse(resp, trans, ob);
                        break;
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
    }
}
