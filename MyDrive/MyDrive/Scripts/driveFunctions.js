
$(document).ready(main);
var basePath = "http://localhost:11182/";
function main() {
    $("#newFolder").click(saveFolder);
    loadFolders();
    loadFiles();
    getMetaData();
    $('#myFile').bind('change', function () {
        if (this.files[0].size / (1024 * 1024) > 8) {
            var myFile = $("#myFile").val("");
            alert("Select a file size of less than 8 MB");
        }
        else
            saveFile();
    });

    $(document).on('mousedown', '.folder', function (event) {
        switch (event.which) {
            case 3:             //right click
                deleteFolder(this);
                break;
        }
    });

    $(document).on('mousedown', '.file', function (event) {
        switch (event.which) {
            case 3:             //right click
                deleteFile(this);
                break;
        }
    });

    $("#downloadMeta").click(downloadMeta);
}

function saveFolder() {
    var x;
    var name = prompt("Please enter folder name", "abc");
    if (name != null && name != "") {
        x = "You Entered " + name;
        //alert(x);
        var userId = $("#userId").val();
        var parentFolderId = $("#parentFolderId").val();
        var dataToSend = {
            "name": name,
            "parentFolderId": parentFolderId,
            "createdBy": userId,
        };
        var setting = {
            type: "POST",
            dataType: "JSON",
            url: basePath + "api/folderData/addNewFolder",
            data: dataToSend,
            success: function (result) {
                $('#foldersDiv *').remove();
                loadFolders();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("error while adding new folder");
                console.log(JSON.stringify(jqXHR));
                console.log("AJAX error: " + textStatus + ' : ' + errorThrown);
            }
        }
        $.ajax(setting);
    }
}

function loadFolders() {
    var userId = $("#userId").val();
    var parentFolderId = $("#parentFolderId").val();
    //alert(userId);
    var dataToSend = {
        "createdBy": userId,
        "parentFolderId": parentFolderId
    };
    var setting = {
        type: "POST",
        dataType: "JSON",
        url: basePath + "api/folderData/loadFolders",
        data: dataToSend,
        success: function (result) {
            //alert('folders Loaded');
            //console.log(result);
            var foldersDiv = $("#foldersDiv");
            $(result).each(function () {
                $("div").css("text-decoration", "none");
                $("div").css("color", "black");
                foldersDiv.append("<div id=folder" + $(this).attr("id") + " class='folder serviceBox' ondblclick='openFolder(this);'>" + $(this).attr("name") + "</div>");
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error while loading folders");
            console.log(JSON.stringify(jqXHR));
            console.log("AJAX error: " + textStatus + ' : ' + errorThrown);
        }
    }
    $.ajax(setting);
    return false;
}

function openFolder(divObj) {
    var id = divObj.id;
    id = id.replace("folder", "");
    var parentFolderId = $("#parentFolderId").val(id);
    $('#foldersDiv *').remove();
    $('#filesDiv *').remove();
    var navigation = $("#navigation");
    var name = $(divObj).html();
    if ($(divObj).attr('class') == "navigation") {
        $(divObj).nextAll().remove();
        $(divObj).remove();
    }
    navigation.append("<div id=folder" + id + " class='navigation' onclick='openFolder(this);'>" + name + "</div>");
    loadFolders();
    loadFiles();
    getMetaData();    
}

function deleteFolder(divObj) {
    $(divObj).addClass("selectFolder");
    if (!confirm("Do You want to delete this folder?")) {
        $(divObj).removeClass("selectFolder");
        return false;
    }
    var id = divObj.id;
    id = id.replace("folder", "");
    var dataToSend = {
        "id": id
    }
    var setting = {
        type: "POST",
        dataType: "JSON",
        url: basePath + "api/folderData/deleteFolder",
        data: dataToSend,
        success: function (result) {
            $('#foldersDiv *').remove();
            loadFolders();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error while deleting folder");
            console.log(JSON.stringify(jqXHR));
            console.log("AJAX error: " + textStatus + ' : ' + errorThrown);
        }
    }
    $.ajax(setting);
}

function saveFile() {
    var dataToSend = new FormData();
    var files = $("#myFile").get(0).files;
    //console.log("save file function");
    if (files.length > 0) {
       // console.log("file.length > 0");
        dataToSend.append("uploadedFile", files[0]);
        //console.log("uploaded file" + files[0]);
    }

    //console.log(this.files[0].size);
    dataToSend.append("parentFolderId", $("#parentFolderId").val());
    dataToSend.append("createdBy", $("#userId").val());
    $(".ajaxProgress").show();
    var setting = {
        type: "POST",
        dataType: "JSON",
        url: basePath + "api/fileData/addNewFile",
        data: dataToSend,
        contentType: false,
        processData: false,
        success: function (result) {
            var myFile = $("#myFile").val("");
            $('#filesDiv *').remove();
            $(".ajaxProgress").hide();
            loadFiles();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error while adding new folder");
            console.log(JSON.stringify(jqXHR));
            console.log("AJAX error: " + textStatus + ' : ' + errorThrown);
        }
    }
    $.ajax(setting);
}

function loadFiles() {
    var userId = $("#userId").val();
    var parentFolderId = $("#parentFolderId").val();
    //alert(userId);
    var dataToSend = {
        "createdBy": userId,
        "parentFolderId": parentFolderId
    };
    var setting = {
        type: "POST",
        dataType: "JSON",
        url: basePath + "api/fileData/loadFiles",
        data: dataToSend,
        success: function (result) {
            console.log(result);

            var filesDiv = $("#filesDiv");
            $(result).each(function () {
                var div = $("<div class='thumbnailBox'>");
                filesDiv.append(div);
                var img = $("<img>")
                img.attr('src', basePath + "api/fileData/getThumbnail?uniqueName=" + $(this).attr("uniqueName"));
                img.css("width", "80px");
                img.css("height", "80px");
                //img.addClass('serviceBox');
                div.append(img);
                div.append("<div  uname=" + $(this).attr("uniqueName") + " id=file" + $(this).attr("id") + " class='file' style='cursor:pointer;text-decoration: underline;color:blue' ondblclick='downloadFile(this)'>" + $(this).attr("name") + "</div>");

            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error while loading files");
            console.log(JSON.stringify(jqXHR));
            console.log("AJAX error: " + textStatus + ' : ' + errorThrown);
        }
    }
    $.ajax(setting);
    return false;
}

function deleteFile(divObj) {
    $(divObj).addClass("selectFolder");
    if (!confirm("Do You want to delete this file?")) {
        $(divObj).removeClass("selectFolder");
        return false;
    }
    var id = divObj.id;
    id = id.replace("file", "");
    var dataToSend = {
        "id": id
    }
    var setting = {
        type: "POST",
        dataType: "JSON",
        url: basePath + "api/fileData/deleteFile",
        data: dataToSend,
        success: function (result) {
            $('#filesDiv *').remove();
            loadFiles();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error while deleting file");
            console.log(JSON.stringify(jqXHR));
            console.log("AJAX error: " + textStatus + ' : ' + errorThrown);
        }
    }
    $.ajax(setting);
}

function downloadFile(divObj) {
    var uniqueName = $(divObj).attr("uname");
    var url = basePath + "api/fileData/downloadFile?uniqueName=" + uniqueName;
    window.open(url);
}

function getMetaData() {
    var userId = $("#userId").val();
    var parentFolderId = $("#parentFolderId").val();
    var dataToSend = {
        "createdBy": userId,
        "parentFolderId": parentFolderId
    };
    var setting = {
        type: "POST",
        dataType: "JSON",
        url: basePath + "api/fileFolderData/getMetaData",
        data: dataToSend,
        success: function (result) {
            console.log(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error while downloading Meta Information");
            console.log(JSON.stringify(jqXHR));
            console.log("AJAX error: " + textStatus + ' : ' + errorThrown);
        }
    }
    $.ajax(setting);
    return false;
}

function downloadMeta() {
    var url = basePath + "api/fileFolderData/downloadMeta";
    window.open(url);
}

$("input[type='search']").keyup(function (e) {
    var code = e.keyCode || e.which;
    //alert(code);
    if (code == 13) {     //for "go" key
        //alert("pressed");
        var string = $(this).val();
        //alert(string);
        $("div").css("text-decoration", "none");
        $("div").css("color", "black");
        $("div:contains(" + string + ")").css("text-decoration", "underline");
        $("div:contains(" + string + ")").css("color", "red");
        $(this).trigger("keyup");
    }
    else
        return 1;
});