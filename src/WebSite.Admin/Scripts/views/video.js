var url, gfile, origin;
function addVideo() {
    $('#dlg').dialog('open').dialog('setTitle', '添加视频');
    $('#fm').form('clear');
    $('#mediarea').html('');
    url = '/media/vsave';
}

function modVideo() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '修改视频信息');
        $('#fm').form('load', row);
        $('#mediarea').html('');
        gfile = row.Media;
        render();
        url = "/media/vsave";
    }
}

function delVideo() {
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $.messager.confirm('警告', '是否删除这个视频？', function (r) {
            $.post('/media/vdelete', { id: row.VideoID }, function (ret) {
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
    $('#tab').datagrid({ url: '/media/vsearch?SearchType=' + name + '&SearchText=' + value });
    $('#tab').datagrid('reload');
}


$(function () {
    $('#tab').datagrid({ url: '/media/vsearch' });
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
            fileTypeExts: '*.mp4;',
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
    $.messager.progress({
        title: '进度',
        msg: '处理中，请稍候...',
        text: '',
        interval: 1000
    });
    $.ajax({
        cache: false,
        data: {
            file: gfile,
            origin: origin,
            cmt: $('#Comment').val()
        },
        dataType: 'json',
        type: 'get',
        url: '/media/vsave',
        success: function (ret) {
            $.messager.progress('close');
            if (ret != null && ret.error == '') {
                $.messager.show({
                    title: '提示',
                    msg: '保存成功'
                });
                render();
                $('#tab').datagrid('reload');
            } else {
                $.messager.show({
                    title: '提示',
                    msg: '处理失败，请稍后再试！' + ret.error
                });
            }
        },
        error: function (xhr, status, error) {
            $.messager.show({
                title: '提示',
                msg: '处理失败，请稍后再试！' + error
            });
            $.messager.progress('close');
            console.log(status + "," + error);
        }
    });
}

function render() {
    var folder;
    if (gfile != null) {
        folder = String(gfile).substring(0, gfile.length - String(getExt(gfile)).length);
        var flashVars = {
            video_url: '/files/video/' + folder + '.flv'
        };
        var params = { scale: "exactFit" };
        params.allowfullscreen = "true";
        var attributes = {};

        swfobject.embedSWF("/content/player.swf", "mediarea", "100%", "100%", "9.0.0", null, flashVars, params, attributes);
    }
}

function getExt(str) {
    return /\.[^\.]+$/.exec(str);
}