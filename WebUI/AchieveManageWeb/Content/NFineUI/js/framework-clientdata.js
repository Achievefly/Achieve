var clients = [];
$(function () {
    clients = $.clientsInit();
    GetLoadNav();
})
$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        organize: [],
        role: [],
        duty: [],
        user: [],
        authorizeMenu: [],
        authorizeButton: []
    };
    var init = function () {
        $.ajax({
            url: "/ClientsData/GetClientsDataJson",
            data:{id:"0"},
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.organize = data.organize;
                dataJson.role = data.role;
                dataJson.duty = data.duty;
                dataJson.authorizeMenu = data.authorizeMenu;
                dataJson.authorizeButton = data.authorizeButton;
            }
        });
    }
    init();
    return dataJson;
}
function GetLoadNav() {
    var data = top.clients.authorizeMenu;
    var _html = "";
    $.each(data, function (i) {
        var row = data[i];
        if (row.F_ParentId == "0") {
            _html += '<li class="treeview">';
            _html += '<a data-id="' + row.F_Id + '" href="#" class="dropdown-toggle"><i class="' + row.F_Icon + '"></i><span>' + row.F_FullName + '</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a>';
            var childNodes = row.ChildNodes;
            if (childNodes.length > 0) {
                _html += '<ul class="treeview-menu">';
                $.each(childNodes, function (i) {
                    var subrow = childNodes[i];
                    _html += '<li>';
                    _html += '<a class="J_menuItem" iframe="true" data-content="content-info" data-id="' + subrow.F_Id + '" href="' + subrow.F_UrlAddress + '" data-index="' + subrow.F_SortCode + '">' + subrow.F_FullName + '</a>';
                    _html += '</li>';
                });
                _html += '</ul>';
            }
            _html += '</li>';
        }
    });
    $(".sidebar-menu").prepend(_html);
}