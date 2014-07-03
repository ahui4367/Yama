var url;
$(function () {
    
})

function addUser() {
    $('#dlg').dialog('open').dialog('setTitle', '添加用户');
    $('#fm').form('clear');
    url = '/user/save';
}

function modUser() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '修改用户信息');
        $('#fm').form('load', row);
        url = "/user/save";
    }
}

function delUser() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $.messager.confirm('警告', '是否删除这个用户？', function (r) {
            $.post('/user/delete', { id: row.Uid }, function (ret) {
                if (ret.error != '') {
                    $.messager.show({
                        title: '删除失败',
                        msg: json.error
                    });
                } else {
                    $('#tab').datagrid('reload');
                }
            });
        });
    }
}

function saveUser() {
    $('#fm').form('submit', {
        url: url,
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (data) {
            var json = eval('(' + data + ')');
            if (json.error != '') {
                $.messager.show({
                    title: '保存失败',
                    msg: json.error
                });
            } else {
                $('#dlg').dialog('close');
                $('#tab').datagrid('reload');
            }
        }
    });
}