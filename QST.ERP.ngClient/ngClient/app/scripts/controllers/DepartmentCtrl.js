(function (app) {




    debugger;
    var DepartmentCtrl = function ($scope, $location, projectService, ngToast) {


        function prepareFields() {

            $scope.model.Project.LegalObligationId = "";
            $scope.model.Project.RequestedDeptId = "";
            $scope.model.Project.PriorityId = "";
            $scope.model.Project.ImpactedDeptId = "";
            $scope.model.Project.SponsoredById = "";

            angular.element('#RequstingDept').focus();
        }

        debugger;
        $scope.IsDataLoaded = false;
        angular.element('#topHeader').show();
        angular.element('#mainTitle').show();
        angular.element('#subTitle').show();
        angular.element('#sidebar-wrapper').show();


        projectService.get()
                .success(function (model) {
                    debugger;
                    $scope.model = model;
                    $scope.IsDataLoaded = true;

            prepareFields();
            


        });


        $scope.cancel = function () {
            debugger;
            $scope.IsDataLoaded = false;
            projectService.get().success(function (model) {
                debugger;
                //$scope.model.Project = model;
                $scope.model = model;
                $scope.IsDataLoaded = true;

                prepareFields();
                
                

            });
        };


        $scope.save = function () {
            debugger;

            projectService.create($scope.model)
            .success(function (mod) {
                debugger;
                ngToast.create({ className: 'success', content: '' + mod.Message + '' });
                $scope.model = mod;

                prepareFields();
              
            });
        };

        angular.element('#RequstingDept').focus();

       

       

        // DatePicker
        $scope.open = function ($event) {
            //debugger;
            $event.preventDefault();
            $event.stopPropagation();
            $scope.status.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yyyy',
            startingDay: 1,
            showWeeks: 'false'
        };

        $scope.formats = ['MM/dd/yyyy', 'dd/MM/yyyy', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        $scope.status = {
            opened: false
        };

        //End DatePicker
    };

    DepartmentCtrl.$inject = ["$scope", "$location", "projectService", "ngToast"];
    app.controller("DepartmentCtrl", DepartmentCtrl);

    var ProjectsListCtrl = function ($scope, $location, projectService) {

        

        $scope.IsDataLoaded = false;
        debugger;
        projectService.getAll()
            .success(function (mod) {
                debugger;
                $scope.model = mod;
                $scope.IsDataLoaded = true;
            })
            .error(function (mod) {
            });

        $scope.createProject = function () {
            debugger;
            $location.path('/Project');

        };


        //$scope.model = projectSvc.getAll().query();
        $scope.sort = function (keyname) {
            debugger;
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        }
       
    };

    ProjectsListCtrl.$inject = ["$scope", "$location", "projectService" ];

    app.controller("ProjectsListCtrl", ProjectsListCtrl);
}(angular.module("QST")));
