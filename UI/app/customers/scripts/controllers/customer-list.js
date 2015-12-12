'use strict';
/**
 * @ngdoc function
 * @name customersModule.controller:CustomerAddController
 * @description
 * # CustomerAddController
 * Controller of the customersModule
 */
angular.module('customersModule')
  .controller('CustomerListController', ['$scope', 'CustomerResource', 'rfc4122', '$state', function ($scope, CustomerResource, rfc4122, $state) {
      var vm = this;

      var data = {};
      vm.data = data;

      var availableActions = [
          { description: 'Edit', action: 'edit' },
          { description: 'Delete', action: 'delete' }
      ];
      

     
      vm.getAvailableActions = function (item) {
          return availableActions;
      };

      vm.reload = function () {
          CustomerResource.list(function (list) {
              vm.data = list;
          });
      };

      vm.actionSelected = function (item, actionItem) {

          var action = actionItem.action;

          switch(action)
          {
              case 'edit':
                  vm.editCustomer(item);
                  break;
              case 'delete':
                  vm.deleteCustomer(item);
                  break;
              default:
                  alert('unknown action: ' + action);
                  break;
          }
     };

      vm.deleteCustomer = function (item) {

          var queryParms = { id: item.Id };
          var query = CustomerResource.delete(queryParms).$promise
            .then(function (result) {
                console.log(result);
                $state.forceReload();
            },
            // on failure...
            function (errorMsg) {
                console.log('Something went wrong: ' + errorMsg);
            });
      };

      vm.editCustomer = function (item) {
          $state.go('customers.edit', { id: item.Id });
      };

      vm.reload();

  }]);