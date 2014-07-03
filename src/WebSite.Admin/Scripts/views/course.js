function addCourse() {
    $('#dlg').dialog({
        title: '新建课件',
        width: 800,
        height: 600,
        content: '<iframe scrolling="yes" frameborder="0"  src="/course/edit" style="width:100%;height:100%;"></iframe>',
        closed: false,
        cache: false,
        modal: true
    });
}

function modCourse() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog({
            title: '修改课件信息',
            width: 800,
            height: 600,
            content: '<iframe scrolling="yes" frameborder="0"  src="/course/edit/' + row.Cid + '" style="width:100%;height:100%;"></iframe>',
            closed: false,
            cache: false,
            modal: true
        });
    }
}