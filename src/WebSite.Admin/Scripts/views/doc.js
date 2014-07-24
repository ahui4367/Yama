var url, gfile, origin;
var gcount = 0;
function addDoc() {
    $('#dlg').dialog('open').dialog('setTitle', '添加文档');
    $('#fm').form('clear');
    $('#mediarea').html('');
    url = '/media/dsave';
}

function modDoc() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '修改文档信息');
        $('#fm').form('load', row);
        $('#mediarea').html('');
        gfile = row.Media;
        gcount = row.Count;
        render();
        url = "/media/dsave";
    }
}

function delDoc() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $.messager.confirm('警告', '是否删除这个文档？', function (r) {
            $.post('/media/ddelete', { id: row.DocID }, function (ret) {
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

function reload() {
    $('#tab').datagrid('reload');
}

function doSearch(value, name) {
    $('#tab').datagrid({ url: '/media/dsearch?SearchType=' + name + '&SearchText=' + value });
    $('#tab').datagrid('reload');
}


$(function () {
    $('#tab').datagrid({ url: '/media/dsearch' });
    $('#tab').datagrid('reload');

    $(function () {
        $('#uploady-doc').uploadify({
            uploader: '/media/uploady',
            swf: '/Scripts/uploadify.swf',
            width: 100,
            height: 23,
            //buttonImage: '/Images/btn/btnpdf.png',
            buttonCursor: 'hand',
            fileObjName: 'file',
            fileTypeExts: '*.ppt;*.pptx;',
            fileTypeDesc: '文档文件',
            auto: true,
            multi: false,
            queueSizeLimit: 1,
            sizeLimit: 10737418240,
            onUploadSuccess: function (file, data, response) {
                var json = eval('(' + data + ')');
                gfile = String(json.file);
                origin = String(json.origin);
                //setTimeout("doProgress('" + json.file + "')", 200);
            }
        });
    })
})

function save() {
    gcount = 0;
    var docid = 0;
    var row = $('#tab').datagrid('getSelected');
    if (row == null) {
        docid = -1;
    } else {
        docid = isNaN(row.DocID) ? -1 : parseInt(row.DocID);
    }

    $.messager.progress({
        title: '进度',
        msg: '处理中，请稍候...',
        text: '',
        interval: 1000
    });
    $.ajax({
        cache: false,
        data: {
            id: docid,
            file: gfile,
            origin: origin,
            cmt: $('#Comment').val()
        },
        dataType: 'json',
        type: 'get',
        url: '/media/dsave',
        success: function (ret) {
            $.messager.progress('close');
            if (ret != null && ret.error == '') {
                $.messager.show({
                    title: '提示',
                    msg: '保存成功'
                });
                gcount = ret.count;
                render();
                $('#tab').datagrid('reload');
            } else {
                $.messager.show({
                    title: '保存失败',
                    msg: json.error
                });
            }
        },
        error: function (xhr, status, error) {
            $.messager.show({
                title: '保存失败',
                msg: error
            });
            $.messager.progress('close');
            console.log(status + "," + error);
        }
    });
}

function render() {
    var folder;
    if (gcount > 0 && gfile != null) {
        folder = String(gfile).substring(0, gfile.length - String(getExt(gfile)).length);
        for (var i = 1; i <= gcount; i++) {
            $('<img src="/files/doc/' + folder + '/' + i + '.jpeg" alt="img">')
                .appendTo($('#mediarea'));
        }
        $('#mediarea').slidesjs({
            width: 500,
            height: 300
        });
    }
}

function getExt(str) {
    return /\.[^\.]+$/.exec(str);
}