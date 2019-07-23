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
var intern_component_1 = require("./intern.component");
var intern_home_component_1 = require("./intern-home/intern-home.component");
var intern_navbar_component_1 = require("./intern-navbar/intern-navbar.component");
var intern_footer_component_1 = require("./intern-footer/intern-footer.component");
var routes = [
    {
        path: '', component: intern_component_1.InternComponent,
        children: [
            { path: '', redirectTo: 'home' },
            { path: 'home', component: intern_home_component_1.InternHomeComponent },
            { path: '**', redirectTo: 'home' }
        ]
    },
    { path: '**', redirectTo: '' }
];
var InternRoutingModule = (function () {
    function InternRoutingModule() {
    }
    InternRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(routes)],
            exports: [router_1.RouterModule]
        })
    ], InternRoutingModule);
    return InternRoutingModule;
}());
exports.InternRoutingModule = InternRoutingModule;
exports.routedComponents = [
    intern_component_1.InternComponent,
    intern_navbar_component_1.InternNavbarComponent,
    intern_home_component_1.InternHomeComponent,
    intern_footer_component_1.InternFooterComponent
];
//# sourceMappingURL=intern-routing.module.js.map