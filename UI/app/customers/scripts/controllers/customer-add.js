'use strict';
/**
 * @ngdoc function
 * @name customersModule.controller:CustomerAddController
 * @description
 * # CustomerAddController
 * Controller of the customersModule
 */
angular.module('customersModule')
  .controller('CustomerAddController', function ($scope) {
      var vm = this;

      var data = {};
      vm.data = data;

      data.name = "input name";
      data.address = {};

      var address = data.address;
      address.address1 = "";
      address.address2 = "";
      address.city = "";
      address.state = "";
      address.zipcode = "";

      var original = angular.copy(vm.data);

      vm.reset = function()
      {
          vm.data = angular.copy(original);
          vm.form1.$setPristine();
      }

      vm.create = function()
      {
          alert(vm.data);
      }
});