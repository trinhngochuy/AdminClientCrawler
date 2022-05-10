
$("#preview").click(function () {
    $(".list-link").empty();
    var link = $("input[name=Url]").val();
    var selector = $("input[name=SelectorSubUrl]").val();
    var data = {
        link: link,
        selector: selector,
    }
    validationGetSubUrl(data)
    $.ajax({
        type: "POST",
        url: "linksDetail",
        data: data,
        success: function (data) {
            data.forEach(element => $(".list-link").append("<li>" + element + "</li>"));
        }
    });

});

$("#next_step").click(function () {
    $("#SelectorContent").removeAttr("disabled");
    $("#SubUrl").removeAttr("disabled");
    $("#SelectorDescrition").removeAttr("disabled");
    $("#SelectorImage").removeAttr("disabled");
    $("#SelectorTitle").removeAttr("disabled");
    $("#back").removeAttr("disabled");
    $("#Url").attr("disabled", "");
    $("#SelectorSubUrl").attr("disabled", "");
    $("#preview").attr("disabled", "");
    $("#category").attr("disabled", "");
    $("#preview_article").removeAttr("disabled");
    var subUrl = $(".list-link li").first().text();
    $("#SubUrl").val(subUrl)
});
$("#back").click(function () {
    $("#SelectorContent").attr("disabled","");
    $("#SubUrl").attr("disabled", "");
    $("#SelectorDescrition").attr("disabled", "");
    $("#SelectorImage").attr("disabled", "");
    $("#SelectorTitle").attr("disabled", "");
    $("#back").attr("disabled", "");
    $("#Url").removeAttr("disabled");
    $("#SelectorSubUrl").removeAttr("disabled");
    $("#preview").removeAttr("disabled");
    $("#category").removeAttr("disabled");
    $("#preview_article").attr("disabled", "");
});

$("#preview_article").click(function () {
    $.ajax({
        type: "POST",
        url: "PreviewArticle",
        data: {
            SelectorContent: $("#SelectorContent").val(),
            SubUrl: $("#SubUrl").val(),
            SelectorDescrition: $("#SelectorDescrition").val(),
            SelectorImage: $("#SelectorImage").val(),
            SelectorTitle: $("#SelectorTitle").val(),
        },
        success: function (data) {
            $("#title").html(data.Title);
            $("#description").html(data.Description);
            $("#img").attr("src",data.Image);
            $("#content").html(data.Content);

        }
    });
});
$("#submit").click(function () {
    var data = {
        SelectorContent: $("#SelectorContent").val(),
        SelectorSubUrl: $("#SelectorSubUrl").val(),
        Url: $("#Url").val(),
        CategoryId: $("#category").val(),
        SelectorDescrition: $("#SelectorDescrition").val(),
        SelectorImage: $("#SelectorImage").val(),
        SelectorTitle: $("#SelectorTitle").val(),
        __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val(),
    };
    $.ajax({
        type: "POST",
        url: "Create",
        data: data,
        success: function (data) {
            if (data == "success") {
                swal("Good job!", "You clicked the button!", "success")
            } else {
                swal("Cancelled", "Your imaginary file is safe :)", "error");
            }
        }, error: function (data) {
            swal("Cancelled", "Your imaginary file is safe :)", "error");
        }
    });
});

function validationGetSubUrl(data) {

}
