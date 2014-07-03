var url;
function saveOrgan() {
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

function modOrgan() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '修改单位信息');
        $('#fm').form('load', row);
        url = "/organ/save";
    }
}

function addOrgan() {
    $('#dlg').dialog('open').dialog('setTitle', '添加单位');
    $('#fm').form('clear');
    url = "/organ/save";
}

function delOrgan() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $.messager.confirm('警告', '是否删除这个单位？', function (r) {
            $.post('/organ/delete', { oid: row.Oid }, function (ret) {
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

