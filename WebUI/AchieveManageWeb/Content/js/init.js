$(function () {
    //BindRightAccordion();
    //BindTreeData();  旧版 菜单树 保留不使用
    //CheckIsChangePwd();
})

//退出系统
function loginOut() {
    var para = { "action": "logout" };
    $.ajax({
        url: "/Login/UserLoginOut",
        type: "post",
        data: para,
        dataType: "json",
        success: function (result) {
            if (result.success) {
                window.location.href = "/Login/Index";
            }
            else {
                $.show_alert("提示", result.msg);
            }
        }
    })
};