var gfile = null;
var gcount = 0;
var url;
var courseid = -1;
$(function () {
    $('#uploady-doc').uploadify({
        uploader: '/course/uploady',
        swf: '/Scripts/uploadify.swf',
        width: 100,
        height: 23,
        buttonImage: '/Images/btn/btnpdf.png',
        buttonCursor: 'hand',
        fileObjName: 'file',
        fileTypeExts: '*.ppt;*.pptx;*.doc;*.docx;*.pdf;*.mp4;*.flv;',
        fileTypeDesc: '文档文件',
        auto: true,
        multi: false,
        queueSizeLimit: 1,
        sizeLimit: 10737418240,
        onUploadSuccess: function (file, data, response) {
            var json = eval('(' + data + ')');
            gfile = String(json.file);
            setTimeout("doProgress('" + json.file + "')", 200);
        }
    });
    var t = 0;
    var v = $('#hdnType').val();
    if (v == 'mp4视频') {
        t = 1;
        $('#selType').attr('value', 'mp4视频');
    }

    var id = parseInt($('#hdnCid').val());
    if (!isNaN(id) && id > 0) {
        courseid = id;
        if (t == 0) {
            gfile = $('#hdnMedia').val();
            gcount = parseInt($('#hdnRank').val());
        } else {
            gfile = $('#hdnMedia').val();
            gcount = 1;
        }
        render(t);

        $('#tab').attr('url', '/quiz/list/' + id);
        $('#tab').datagrid('load');
    }
})

function doProgress(name) {
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
            file: name
        },
        dataType: 'json',
        type: 'get',
        url: '/course/converty',
        success: function (ret) {
            $.messager.progress('close');
            if (ret != null && ret.error == '') {
                $.messager.alert('提示', '处理成功!', 'info');
                gcount = isNaN(ret.count) ? 0 : parseInt(ret.count);
                render(ret.type);
            } else {
                $.messager.alert('警告', '处理失败，请稍后再试！' + ret.error, 'error');
            }
        },
        error: function (xhr, status, error) {
            $.messager.alert('警告', '处理失败，请稍后再试！', 'error');
            $.messager.progress('close');
            console.log(status + "," + error);
        }
    });
}

function render(t) {
    $('#mediarea').html('');
    if (t == 1) {
        renderVideo();
    } else {
        renderDoc();
    }
}

function renderVideo() {
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

function renderDoc() {
    var folder;
    if (gcount > 0 && gfile != null) {
        folder = String(gfile).substring(0, gfile.length - String(getExt(gfile)).length);
        for (var i = 1; i <= gcount; i++) {
            $('<img src="/files/doc/' + folder + '/' + i + '.jpeg" alt="img">')
                .appendTo($('#mediarea'));
        }
        $('#mediarea').slidesjs({
            width: 700,
            height: 600
        });
    }
}

function saveCourse() {
    if (gfile == null || gfile == '' || isNaN(gcount)) {
        $.messager.show({
            title: '警告',
            msg: '请上传课件文档'
        });
        return;
    }
    $('#hdnRank').val(gcount);

    var cname = $('#cname').val();
    if (cname == null || cname == '') {
        $.messager.show({
            title: '警告',
            msg: '请填写课件名称'
        });
        return;
    }

    var limit = $('#climit').val();
    if (isNaN(limit) || parseInt(limit) < 0) {
        $.messager.show({
            title: '警告',
            msg: '限时时间无效，请设置(0~xxx)'
        });
        return;
    }
    $('#hdnMedia').val(gfile);
    var ext = getExt(gfile);
    if (ext == '.mp4') {
        $('#selType').val('mp4视频');
    } else {
        $('#selType').val('ppt文档');
    }

    $('#fmMain').form('submit', {
        url: '/course/save',
        success: function (data) {
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
                courseid = json.courseid;
            }
        }
    });
}

function getExt(str) {
    return /\.[^\.]+$/.exec(str);
}

function addQuiz() {
    if (courseid < 1) {
        $.messager.show({
            title: '警告',
            msg: '请先创建课件！'
        });
        return;
    }
    $('#pno').combobox('clear');
    $('#hdnCourseID').val(courseid);
    if (!isNaN(gcount) && gcount > 0) {
        
        for (var i = 0; i < gcount; i++) {
            $('#pno').append('<option value="' + i + '">第' + i + '页</option>');
        }
        //$('#pno').combobox('loadData');
    }
    $('#dlg').dialog('open').dialog('setTitle', '添加答题');
    $('#fm').form('clear');
    url = '/quiz/save';
}

function modQuiz() {
    if (courseid < 1) {
        $.messager.show({
            title: '警告',
            msg: '请先创建课件！'
        });
        return;
    }
    $('#pno').combobox('clear');
    $('#hdnCourseID').val(courseid);
    if (!isNaN(gcount) && gcount > 0) {
        
        for (var i = 0; i < gcount; i++) {
            $('#pno').append('<option value="' + i + '">第' + i + '页</option>');
        }
        $('#pno').combobox('loadData');
    }
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', '修改答题');
        $('#fm').form('load', row);
        url = "/quiz/save";
    }
}

function delQuiz() {
    if (courseid < 1) {
        $.messager.show({
            title: '警告',
            msg: '请先创建课件！'
        });
        return;
    }
    var row = $('#tab').datagrid('getSelected');
    if (row) {
        $.messager.confirm('警告', '是否删除这个答题？', function (r) {
            $.post('/quiz/delete/' + row.QuizID, { id: row.QuizID }, function (ret) {
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
                $('#tab').datagrid({url:'/quiz/list/' + courseid});
                $('#tab').datagrid('reload');
            }
        }
    });
}