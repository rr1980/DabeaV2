"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var InternNavbarComponent = (function () {
    function InternNavbarComponent(authService) {
        this.authService = authService;
        this.navbarOpen = false;
    }
    InternNavbarComponent.prototype.ngOnInit = function () {
    };
    InternNavbarComponent.prototype.toggleNavbar = function () {
        this.navbarOpen = !this.navbarOpen;
    };
    InternNavbarComponent.prototype.onClickLogout = function () {
        this.authService.logout();
    };
    InternNavbarComponent = __decorate([
        core_1.Component({
            selector: 'cz-intern-navbar',
            templateUrl: './intern-navbar.component.html',
            styleUrls: ['./intern-navbar.component.scss']
        })
    ], InternNavbarComponent);
    return InternNavbarComponent;
}());
exports.InternNavbarComponent = InternNavbarComponent;
//# sourceMappingURL=intern-navbar.component.js.map