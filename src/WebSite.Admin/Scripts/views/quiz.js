var url;
function addQuiz() {
    $('#dlg').dialog('open').dialog('setTitle', '添加考题');
    $('#fm').form('clear');
    $('#qtype').combobox('setValue', '0');
    url = '/quiz/save';
}

function modQuiz() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '修改考题信息');
        $('#fm').form('load', row);
        url = "/quiz/save";
    }
}

function delQuiz() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $.messager.confirm('警告', '是否删除这个考题？', function (r) {
            $.post('/quiz/delete', { id: row.QuizID }, function (ret) {
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

function doSearch(value, name) {
    console.log('category:' + name + '=' + value);
    $('#tab').datagrid({ url: '/quiz/search?SearchType=' + name + '&SearchText=' + value });
    $('#tab').datagrid('reload');
}

function formatType(val, row) {
    if (val == "简答") {
        return '<span style="color:red;">' + val + '</span>';
    } else {
        return '<span style="color:blue;">' + val + '</span>';
    }
}

function saveQuiz() {
    $('#fm').form('submit', {
        url: url,
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (data) {
            console.log(data);
            var json = eval('(' + data + ')');
            if (json.error != '') {
                $.messager.show({
                    title: '保存失败',
                    msg: json.error
                });
            } else {
                $.messager.show({
                    title: '提示',
                    msg: '保存成功'
                });
                $('#dlg').dialog('close');
                $('#tab').datagrid({ url: '/quiz/search' });
                $('#tab').datagrid('reload');
            }
        }
    });
}

function reloadQuiz() {
    $('#tab').datagrid('reload');
}

$(function () {
    $('#tab').datagrid({ url: '/quiz/search' });
    $('#tab').datagrid('reload');

    $('#qtype').combobox({
        editable: false,
        required: true,
        onChange: function (nv, ov) {
            if (nv == '1') {
                $('.fnoreq').hide();
            } else {
                $('.fnoreq').show();
            }
        }
    });
});