(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-97451184"],{"49a1":function(e,r,s){"use strict";s.r(r);var t=function(){var e=this,r=e.$createElement,s=e._self._c||r;return s("UserForm",{attrs:{action:e.$resources.es.action.create,createPass:!0},on:{save:e.save}})},a=[],u=s("5bb5"),n={name:"NewUser",components:{UserForm:u["a"]},created:function(){this.$store.commit("auth/cleanUser")},methods:{save:function(e){var r=this;this.$store.commit("setLoading",!0),this.$store.dispatch("auth/register",e).then((function(){r.$store.commit("setLoading",!1),r.$router.push({name:"list"})}),(function(){r.$store.commit("setLoading",!1)}))}}},o=n,i=s("2877"),c=Object(i["a"])(o,t,a,!1,null,null,null);r["default"]=c.exports},"5bb5":function(e,r,s){"use strict";var t=function(){var e=this,r=e.$createElement,s=e._self._c||r;return s("v-container",[s("form",{attrs:{"lazy-validation":!0},on:{submit:function(r){return r.preventDefault(),e.handleSubmit(r)}}},[s("v-row",[s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.name,dense:"","error-messages":e.errorfirstname,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.user.firstname.$touch()},blur:function(r){return e.$v.user.firstname.$touch()}},model:{value:e.user.firstname,callback:function(r){e.$set(e.user,"firstname",r)},expression:"user.firstname"}})],1),s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.lastname,dense:"","error-messages":e.errorlastname,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.user.lastname.$touch()},blur:function(r){return e.$v.user.lastname.$touch()}},model:{value:e.user.lastname,callback:function(r){e.$set(e.user,"lastname",r)},expression:"user.lastname"}})],1),s("v-col",{attrs:{cols:"6"}},[s("v-menu",{attrs:{"close-on-content-click":!1,"nudge-right":40,transition:"scale-transition","offset-y":"","min-width":"290px"},scopedSlots:e._u([{key:"activator",fn:function(r){var t=r.on;return[s("v-text-field",e._g({attrs:{label:e.$resources.es.common.birth,"error-messages":e.errorbirth,disabled:e.$store.state.loading,readonly:""},on:{input:function(r){return e.$v.user.date.$touch()},blur:function(r){return e.$v.user.date.$touch()}},model:{value:e.user.date,callback:function(r){e.$set(e.user,"date",r)},expression:"user.date"}},t))]}}]),model:{value:e.menu2,callback:function(r){e.menu2=r},expression:"menu2"}},[s("v-date-picker",{attrs:{"show-current":"",flat:""},on:{input:function(r){e.menu2=!1}},model:{value:e.user.date,callback:function(r){e.$set(e.user,"date",r)},expression:"user.date"}})],1)],1),s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.cel,dense:"","error-messages":e.errorcel,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.user.cel.$touch()},blur:function(r){return e.$v.user.cel.$touch()}},model:{value:e.user.cel,callback:function(r){e.$set(e.user,"cel",r)},expression:"user.cel"}})],1),s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.address,dense:"","error-messages":e.erroraddress,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.user.address.$touch()},blur:function(r){return e.$v.user.address.$touch()}},model:{value:e.user.address,callback:function(r){e.$set(e.user,"address",r)},expression:"user.address"}})],1),s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.email,dense:"","error-messages":e.erroremail,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.user.email.$touch()},blur:function(r){return e.$v.user.email.$touch()}},model:{value:e.user.email,callback:function(r){e.$set(e.user,"email",r)},expression:"user.email"}})],1),s("v-col",{attrs:{cols:"6"}},[s("v-select",{attrs:{items:e.$store.state.role.rolelist,"item-text":"description","item-value":"uid",label:e.$resources.es.common.rol,"error-messages":e.errorrol,disabled:e.$store.loading,dense:""},on:{input:function(r){return e.$v.user.id_rol.$touch()},blur:function(r){return e.$v.user.id_rol.$touch()}},model:{value:e.user.id_rol,callback:function(r){e.$set(e.user,"id_rol",r)},expression:"user.id_rol"}})],1),s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.username,dense:"","error-messages":e.errorusername,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.user.username.$touch()},blur:function(r){return e.$v.user.username.$touch()}},model:{value:e.user.username,callback:function(r){e.$set(e.user,"username",r)},expression:"user.username"}})],1),e.createPass?s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.password,dense:"",type:"password","error-messages":e.errorpass,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.user.pass.$touch()},blur:function(r){return e.$v.user.pass.$touch()}},model:{value:e.user.pass,callback:function(r){e.$set(e.user,"pass",r)},expression:"user.pass"}})],1):e._e(),e.createPass?s("v-col",{attrs:{cols:"6"}},[s("v-text-field",{attrs:{label:e.$resources.es.common.repeatPassword,dense:"",type:"password","error-messages":e.errorrepeatpass,disabled:e.$store.state.loading},on:{input:function(r){return e.$v.repeatPass.$touch()},blur:function(r){return e.$v.repeatPass.$touch()}},model:{value:e.repeatPass,callback:function(r){e.repeatPass=r},expression:"repeatPass"}})],1):e._e(),s("v-col",{attrs:{cols:"12"}},[s("v-btn",{staticClass:"btn",attrs:{block:"",dense:"",type:"submit",color:"success",elevation:e.$v.$invalid?0:10,disabled:e.$store.state.loading||e.$v.$invalid}},[e._v(e._s(e.action))])],1),s("v-col",{attrs:{cols:"12"}},[s("v-btn",{attrs:{color:"primary",elevation:"10",block:""},on:{click:function(r){return e.$router.go(-1)}}},[e._v("Volver")])],1)],1)],1)])},a=[],u=s("1dce"),n=s("b5ae"),o=s("bbb0"),i={mixins:[u["validationMixin"]],name:"FormUser",props:["data","action","createPass"],data:function(){var e;return{user:null!==(e=this.$store.state.auth.user)&&void 0!==e?e:new o["a"],menu2:!1,repeatPass:null}},created:function(){var e;this.$store.dispatch("role/getAll"),this.user.date=null!==(e=this.user.date)&&void 0!==e?e:this.$resources.es.currentDate},computed:{errorfirstname:function(){var e=[];return this.$v.user.firstname.$dirty?(!this.$v.user.firstname.required&&e.push(this.$resources.es.common.required),e):e},errorlastname:function(){var e=[];return this.$v.user.lastname.$dirty?(!this.$v.user.lastname.required&&e.push(this.$resources.es.common.required),e):e},errorbirth:function(){var e=[];return this.$v.user.date.$dirty?(!this.$v.user.date.required&&e.push(this.$resources.es.common.required),e):e},errorusername:function(){var e=[];return this.$v.user.username.$dirty?(!this.$v.user.username.required&&e.push(this.$resources.es.common.required),e):e},errorrol:function(){var e=[];return this.$v.user.id_rol.$dirty?(!this.$v.user.id_rol.required&&e.push(this.$resources.es.common.required),e):e},errorcel:function(){var e=[];return this.$v.user.cel.$dirty?(!this.$v.user.cel.required&&e.push(this.$resources.es.common.required),e):e},erroremail:function(){var e=[];return this.$v.user.email.$dirty?(!this.$v.user.email.email&&e.push("Email incorrecto"),!this.$v.user.email.required&&e.push(this.$resources.es.common.required),e):e},erroraddress:function(){var e=[];return this.$v.user.address.$dirty?(!this.$v.user.address.required&&e.push(this.$resources.es.common.required),e):e},errorpass:function(){var e=[];return this.$v.user.pass.$dirty?(!this.$v.user.pass.required&&e.push(this.$resources.es.common.required),!this.$v.user.pass.minLength&&e.push("La contraseña debe tener mínimo 6 caracteres"),e):e},errorrepeatpass:function(){var e=[];return this.$v.repeatPass.$dirty?(!this.$v.repeatPass.required&&e.push(this.$resources.es.common.required),!this.$v.repeatPass.sameAsPass&&e.push("Las contraseñas deben ser iguales"),e):e}},methods:{handleSubmit:function(){this.$emit("save",this.user)}},validations:{user:{firstname:{required:n["required"]},lastname:{required:n["required"]},date:{required:n["required"]},cel:{required:n["required"]},username:{required:n["required"]},email:{required:n["required"],email:n["email"]},address:{required:n["required"]},id_rol:{required:n["required"]},pass:{required:Object(n["requiredIf"])((function(){return this.createPass})),minLength:Object(n["minLength"])(6)}},repeatPass:{required:Object(n["requiredIf"])((function(){return this.createPass})),sameAsPass:Object(n["sameAs"])((function(){return this.user.pass}))}}},c=i,l=s("2877"),d=Object(l["a"])(c,t,a,!1,null,null,null);r["a"]=d.exports}}]);
//# sourceMappingURL=chunk-97451184.d0fa9ebc.js.map