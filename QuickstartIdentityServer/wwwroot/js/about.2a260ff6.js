(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["about"],{8362:function(e,t,a){},a55b:function(e,t,a){"use strict";a.r(t);var r=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",{attrs:{id:"login_box"}},[a("div",{staticClass:"form_box"},[a("el-form",{ref:"loginForm",staticClass:"login-ruleForm",attrs:{model:e.logindata,"status-icon":"",rules:e.loginrules,"label-width":"66px"}},[a("el-form-item",{attrs:{label:"用户名",prop:"userName"}},[a("el-input",{model:{value:e.logindata.userName,callback:function(t){e.$set(e.logindata,"userName",e._n(t))},expression:"logindata.userName"}})],1),a("el-form-item",{attrs:{label:"密码",prop:"passWord"}},[a("el-input",{attrs:{type:"password",autocomplete:"off"},model:{value:e.logindata.passWord,callback:function(t){e.$set(e.logindata,"passWord",t)},expression:"logindata.passWord"}})],1),a("el-form-item",{attrs:{label:"记住我"}},[a("el-switch",{model:{value:e.logindata.rember,callback:function(t){e.$set(e.logindata,"rember",t)},expression:"logindata.rember"}})],1),a("el-form-item",{staticClass:"submenu_btn_lo"},[a("el-button",{staticClass:"submenu_btn",attrs:{type:"primary"},on:{click:function(t){e.submitForm("loginForm")}}},[e._v("登录")])],1)],1)],1)])},s=[],n=(a("96cf"),a("3b8d")),o={name:"login",data:function(){return{logindata:{userName:"superadmin",passWord:"666666",rember:!1},loginrules:{userName:[{required:!0,message:"用户名不能为空"}],passWord:[{required:!0,message:"密码不能为空"}]}}},methods:{submitForm:function(e){var t=this;this.$refs[e].validate(function(e){if(!e)return console.log("error submit!!"),!1;t.login()})},resetForm:function(e){this.$refs[e].resetFields()},login:function(){var e=Object(n["a"])(regeneratorRuntime.mark(function e(){var t,a,r;return regeneratorRuntime.wrap(function(e){while(1)switch(e.prev=e.next){case 0:return t=new FormData,t.append("grant_type","password"),t.append("client_id","ro.client"),t.append("client_secret","secret"),t.append("username",this.logindata.userName),t.append("password",this.logindata.passWord),a={headers:{"Content-Type":"multipart/form-data"}},e.next=9,this.$http.post("/connect/token",t,a);case 9:r=e.sent,this.$http.setheader(r.access_token),this.$router.push("/home/index");case 12:case"end":return e.stop()}},e,this)}));function t(){return e.apply(this,arguments)}return t}()}},i=o,l=(a("dec7"),a("2877")),u=Object(l["a"])(i,r,s,!1,null,"e2cd49e0",null);u.options.__file="Login.vue";t["default"]=u.exports},dec7:function(e,t,a){"use strict";var r=a("8362"),s=a.n(r);s.a}}]);
//# sourceMappingURL=about.2a260ff6.js.map