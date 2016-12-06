//验证表单
function formValid(index) {
    return $(index).valid({
        errorPlacement: function (error, element) {
            element.parents('.formValue').addClass('has-error');
            element.parents('.has-error').find('i.error').remove();
            element.parents('.has-error').append('<i class="form-control-feedback fa fa-exclamation-circle error" data-placement="left" data-toggle="tooltip" title="' + error + '"></i>');
            $(index).find("[data-toggle='tooltip']").tooltip();
            if (element.parents('.input-group').hasClass('input-group')) {
                element.parents('.has-error').find('i.error').css('right', '33px')
            }
            
        },
        success: function (element) {
            element.parents('.has-error').find('i.error').remove();
            element.parent().removeClass('has-error');
        }
    });
}
//新增方法
//table:表格id
//id:编号
//title:标题
//url:地址
//width:宽
//height:高
function btn_open(table, id, title, url, edit, width, height) {
    var keyValue = $("#" + table).jqGridRowValue().ID;
    var submiturl = url;
    if (edit) {
        submiturl = url + '?keyValue=' + keyValue;
    }
    $.modalOpen({
        id: id,
        title: title,
        url: submiturl,
        width: width + "px",
        height: height + "px",
        callBack: function (iframeId,index) {
            if (!formValid($(top.frames[iframeId].document).find('form'))) {
                return false;
            }
            $.submitForm({
                index:index,
                url: url,
                param: $(top.frames[iframeId].document).find("form").formSerialize(),
                success: function () {
                    $.currentWindow().$("#" + table).trigger("reloadGrid");
                }
            })
        }
    });
}
//修改方法
//table:表格id
//id:编号
//title:标题
//url:地址
//formurl:表单加载的地址
//width:宽
//height:高
function btn_edit(table, id, title, url, formurl, width, height) {
    var keyValue = $("#" + table).jqGridRowValue().ID;
    var submiturl = url + '?keyValue=' + keyValue;
    $.modalOpen({
        id: id,
        title: title,
        url: submiturl,
        width: width + "px",
        height: height + "px",
        callBack: function (iframeId, index) {
            if (!formValid($(top.frames[iframeId].document).find('form'))) {
                return false;
            }
            $.submitForm({
                index: index,
                url: submiturl,
                param: $(top.frames[iframeId].document).find("form").formSerialize(),
                success: function () {
                    $.currentWindow().$("#" + table).trigger("reloadGrid");
                }
            })
        }
    });
}
//删除方法
//table:表格id
//url：地址
function btn_delete(table,url) {
    $.deleteForm({
        url: url,
        param: { keyValue: $("#" + table).jqGridRowValue().ID },
        success: function () {
            $.currentWindow().$("#" + table).trigger("reloadGrid");
        }
    })
}
//提交方法
function btn_confirm(table,url,msg) {
    var keyValue = $("#" + table).jqGridRowValue().ID;
    //if (keyValue == null || keyValue == "") {
    //    top.layer.alert("请选中", {
    //        icon: icon,
    //        title: "系统提示",
    //        btn: ['确认'],
    //        btnclass: ['btn btn-primary'],
    //    });
    //}
    $.modalConfirm(msg, function (r) {
        if (r) {
            $.submitForm({
                url: url,
                param: { keyValue: keyValue },
                success: function () {
                    $.currentWindow().$("#" + table).trigger("reloadGrid");
                }
            })
        }
    });
}
function btn_details(table, id, title, url, width, height) {
    var keyValue = $("#" + table).jqGridRowValue().ID;
    var submiturl = url + '?editValue=1&keyValue=' + keyValue;
    $.modalOpen({
        id: id,
        title: title,
        url: submiturl,
        width: width + "px",
        height: height + "px",
        btn: null
    });
}