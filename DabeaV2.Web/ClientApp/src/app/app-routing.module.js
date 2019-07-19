"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var login_component_1 = require("./shared/components/login/login.component");
var auth_intern_guard_1 = require("./shared/guards/auth-intern.guard");
var auth_extern_guard_1 = require("./shared/guards/auth-extern.guard");
var routes = [
    { path: 'login', component: login_component_1.LoginComponent },
    { path: 'intern', loadChildren: "./intern/intern.module#InternModule", canLoad: [auth_intern_guard_1.AuthInternGuard], canActivate: [auth_intern_guard_1.AuthInternGuard] },
    { path: 'extern', loadChildren: "./extern/extern.module#ExternModule", canLoad: [auth_extern_guard_1.AuthExternGuard], canActivate: [auth_extern_guard_1.AuthExternGuard] },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
];
var AppRoutingModule = (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forRoot(routes)],
            exports: [router_1.RouterModule]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
exports.routedComponents = [
    login_component_1.LoginComponent
];
//# sourceMappingURL=app-routing.module.js.map