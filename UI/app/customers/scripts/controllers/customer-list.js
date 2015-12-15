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

      var availableActionsIsActive = [
          { description: 'Edit', action: 'edit' },
          { description: 'Deactivate', action: 'deactivate' }
      ];
      var availableActionsNotIsActive = [
          { description: 'Activate', action: 'activate' }
      ];
      

     
      vm.getAvailableActions = function (item) {
          if (item == null) {
              return [];
          }
          else if (item.IsActive) {
              return availableActionsIsActive;
          } else {
              return availableActionsNotIsActive;
          }
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
              case 'deactivate':
                  vm.deactivateCustomer(item);
                  break;
              case 'activate':
                  vm.activateCustomer(item);
                  break;
              default:
                  alert('unknown action: ' + action);
                  break;
          }
     };

      vm.activateCustomer = function (item) {
          var queryParms = { id: item.Id };
          var query = CustomerResource.activate(queryParms).$promise
            .then(function (result) {
                console.log(result);
                $state.forceReload();
            },
            // on failure...
            function (errorMsg) {
                console.log('Something went wrong: ' + errorMsg);
            });
      };

      vm.deactivateCustomer = function (item) {

          var queryParms = { id: item.Id };
          var query = CustomerResource.deactivate(queryParms).$promise
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