
(function () {
    "use strict";

    angular.module("simpleControls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrick: "E",
            templateUrl: "/views/waitCursor.html"
        };
    }
})();