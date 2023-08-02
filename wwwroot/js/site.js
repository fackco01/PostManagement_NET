
let id = null;

$(document).ready()
{

    id = document.cookie;

    console.log('id: ' + id);

    if (id == "") {
        $("#username").hide();
        $("#logout").hide();
    }
    else {
        $("#login").hide();
        $("#register").hide();

        $("#username").show();
        let user = id.substring(id.indexOf("email=") + 6, id.indexOf("id") - 2)
        $("#username").empty().append(user);
        $("#logout").show();

    }

    //--------------------------------------------------------------------------

    LoadPostData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/SignalRHub").build();
    connection.start();
        
    connection.on('LoadPost', function () {
        LoadPostData();
    })


    //------------------------------------------------------------

    $('#search-btn').on('click', function () {
        console.log("--- Searching ---")
        LoadSearchData();
    });


    //------------------------------------------------------------

    $("#logout").on('click', function () {
        console.log('logout');

        $("#username").hide();
        $("#logout").hide();

        $("#login").show();
        $("#register").show();

        delete_cookie();
    });
}

function LoadPostData() {
    console.log("--- Load Data Post ---");

    $.ajax({
        type: "GET",
        url: "/Index?handler=Post",
        error: function (error) {
            console.log(error)
        },
        success: function (data) {
            $("#post-body").empty();


            for (let i = 0; i < data.length; i++) {
                console.log(data[i]);
                $("#post-body").append("<tr>" +
                    "<td>" + data[i].user.fullName + "</td>" +
                    "<td>" + data[i].createdDate.replace("T", " ") + "</td>" +
                    "<td>" + data[i].updatedDate.replace("T", " ") + "</td>" +
                    "<td>" + data[i].title + "</td>" +
                    "<td>" + data[i].content + "</td>" +
                    "<td>" + data[i].publishStatus + "</td>" +
                    "<td>" + data[i].category.categoryName + "</td>" +
                    "<td>" + data[i].category.categoryDescription + "</td>" +
                    "<td id='function'>" +
                    addFunction(data[i]) +
                    "</td>" +
                    "</tr>");
            }
        }
    });

}

function LoadSearchData() {
    $.ajax({
        type: "GET",
        url: "/Index?handler=Searching",
        data: {
            search: $("#searching").val(),
            type: $('input[name="type"]:checked').val()
        },
        error: function (error) {
            console.log(error)
        },
        success: function (data) {
            console.log(data)
                ;
            $("#post-body").empty();

            for (let i = 0; i < data.length; i++) {
                $("#post-body").append("<tr>" +
                    "<td>" + data[i].user.fullName + "</td>" +
                    "<td>" + data[i].createdDate.replace("T", " ") + "</td>" +
                    "<td>" + data[i].updatedDate.replace("T", " ") + "</td>" +
                    "<td>" + data[i].title + "</td>" +
                    "<td>" + data[i].content + "</td>" +
                    "<td>" + data[i].publishStatus + "</td>" +
                    "<td>" + data[i].category.categoryName + "</td>" +
                    "<td>" + data[i].category.categoryDescription + "</td>" +
                    "<td id='function'>" +
                    addFunction(data[i]) +
                    "</td>" +
                    "</tr>");
            }

        }
    });

}

function addFunction(data) {
    if (id.substring(id.indexOf("id") + 3) == data.userID) {
        return "<a href= '/Edit?id=" + data.postID + "'> Edit  </a> |" +
            "<a href= '/Details?id=" + data.postID + "'> Details </a> |" +
            "<a href= '/Delete?id=" + data.postID + "'> Delete </a> |"
    }
    return "";
}

function delete_cookie() {
    document.cookie = 'account =; Max-Age=-99999999;';
}

