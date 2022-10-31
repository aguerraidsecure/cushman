var divId = "bgAjaxWait" + new Date().getTime();
var divMsgBoxBackGroundId = divId + "BackGround";

function getDocHeight() {
    var D = document;
    return Math.max(
        Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
        Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
        Math.max(D.body.clientHeight, D.documentElement.clientHeight));
}
function ShowLoading(e) {
    $("body").append("<div id=\"" + divMsgBoxBackGroundId + "\" class=\"msgBoxBackGroundAjax\"></div>");
    var divBackGround = $("#" + divMsgBoxBackGroundId);
    var divimagen = document.createElement('div');
    divimagen.id = "div-dl-im";
    var divMsgBoxAjax = document.createElement('div');
    divMsgBoxAjax.id = "div-dl";
    var img = document.createElement('img');
    img.style.cssText = 'width: 300px; height: 100;';
    img.src = '../../image/cargando.gif';
    divimagen.innerHTML = "<p> Procesando por favor espere...<br />";
    divimagen.appendChild(img);
    divMsgBoxAjax.appendChild(divimagen);
    divBackGround.css({ "width": $(document).width(), "height": getDocHeight() });
    divBackGround.append(divimagen);
    var divMsgBoxAjaxWindow = $("#div-dl-im");
    var width = divMsgBoxAjaxWindow.width();
    var height = divMsgBoxAjaxWindow.height();
    var windowHeight = $(window).height();
    var windowWidth = $(window).width();
    var top = windowHeight / 2 - height / 2;
    var left = windowWidth / 2 - width / 2;
    divMsgBoxAjaxWindow.animate({ opacity: 1, "top": top, "left": left }, 200);
    return true;
}

function HiedeLoading() {
    var divMsgBoxAjaxWindow = $("#div-dl-im");
    var width = divMsgBoxAjaxWindow.width();
    var height = divMsgBoxAjaxWindow.height();
    var windowHeight = $(window).height();
    var windowWidth = $(window).width();
    var top = windowHeight / 2 - height / 2;
    var left = windowWidth / 2 - width / 2;
    divMsgBoxAjaxWindow.animate({ opacity: 0, "top": top - 50, "left": left }, 200);
    $("#" + divMsgBoxBackGroundId).fadeOut(300);
    setTimeout(
        function () {
            $("#div-dl-im").remove();
            $("#" + divMsgBoxBackGroundId).remove();
        }, 1000);
}