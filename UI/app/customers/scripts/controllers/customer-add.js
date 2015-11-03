'use strict';
/**
 * @ngdoc function
 * @name customersModule.controller:CustomerAddController
 * @description
 * # CustomerAddController
 * Controller of the customersModule
 */
angular.module('customersModule')
  .controller('CustomerAddController', ['$scope', 'CustomerResource', function ($scope, CustomerResource) {
      var vm = this;

      var data = {};
      vm.data = data;

      data.Name = "input name";
      data.BillingAddress = {};

      var address = data.BillingAddress;
      address.Address1 = "";
      address.Address2 = "";
      address.City = "";
      address.State = "";
      address.Zipcode = "";

      var original = angular.copy(vm.data);

      vm.reset = function()
      {
          vm.data = angular.copy(original);
          vm.form1.$setPristine();
      }

      vm.create = function()
      {
          alert(vm.data);
          var query = CustomerResource.query();
      }
}]);