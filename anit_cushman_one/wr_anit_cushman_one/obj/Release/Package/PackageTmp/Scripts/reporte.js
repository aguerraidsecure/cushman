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
    divimagen.innerHTML = "<p> Creando Reporte...<br />";           
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

$(document)
    .ready(function () {
        storeCargaCombo("/Utils/GetCdEstado", "cd_estado");
        $("#cd_estado").select2({ placeholder: "Seleccione Estado", width: "49%" });
        $("#cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "49%" });
        $("#cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "49%" });
        msgBoxImagePath = "/image/";

        $("#btnPdf")
            .click(function () {

                //if ($('input:radio[name=idioma]:checked').val() == null) {
                //    $.msgBox({
                //        title: "Reporte",
                //        content: "Necesita seleccionar un idioma ",
                //        type: "alert"
                //    });
                //    return false;
                //}


                if ($("#cd_titulo").val() == null || $("#cd_titulo").val() == "") {
                    $.msgBox({
                        title: "Reporte",
                        content: "Debe proporcionar un zoom ",
                        type: "alert"
                    });
                    return false;
                }
                $.msgBox({
                    title: "Cushman ONE",
                    content: "Está seguro de generar el pdf?",
                    type: "confirm",
                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                    success: function (result) {
                        if (result === "Si") {
                            //setBorraRegistro("/Producto/Eliminar/" + $("#cd_producto").val());                                                        
                            ShowLoading();
                            setGeneraPdf("/ReportNaves/Reporte");                            
                        }
                    }
                });
            });

        $("#btnPdfS")
            .click(function () {

                $.msgBox({
                    title: "Cushman ONE",
                    content: "Está seguro de generar el pdf?",
                    type: "confirm",
                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                    success: function (result) {
                        if (result === "Si") {
                            //setBorraRegistro("/Producto/Eliminar/" + $("#cd_producto").val());                                                        
                            ShowLoading();
                            setGeneraPdfSum("/Naves/GeneraPdfSum");
                        }
                    }
                });
            });

        $("#btnPP")
            .click(function () {

                $.msgBox({
                    title: "Cushman ONE",
                    content: "Está seguro de generar el Power Point?",
                    type: "confirm",
                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                    success: function (result) {
                        if (result === "Si") {
                            //setBorraRegistro("/Producto/Eliminar/" + $("#cd_producto").val());                                                        
                            ShowLoading();
                            setGeneraPdfSum("/ReportNaves/ReportePP");
                        }
                    }
                });
            });

        $("#btnExcel")
            .click(function () {

                
                $.msgBox({
                    title: "Cushman ONE",
                    content: "Está seguro de generar el reporte en Excel",
                    type: "confirm",
                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                    success: function (result) {
                        if (result === "Si") {
                            //setBorraRegistro("/Producto/Eliminar/" + $("#cd_producto").val());                                                        
                            ShowLoading();
                            setGeneraExcel("/ExportData/GetExcel");
                        }
                    }
                });
            });

        $("#btnBuscar")
            .click(function () {
                
                var id_estado = $("#cd_estado").val();
                if (id_estado == null || id_estado == "")
                {
                    alert('Debe de seleccionar un estado');
                    return false;
                }

                var id_municipio = $("#cd_municipio").val();
                if (id_municipio == null || id_municipio == "")
                {
                    id_municipio = 0;
                }

                var id_colonia = $("#cd_colonia").val();
                if (id_colonia == null || id_colonia == "")
                {
                    id_colonia = 0;
                }

                data = { cd_estado: id_estado, cd_municipio: id_municipio, cd_colonia: id_colonia };
                $.ajax({
                    type: 'POST',
                    url: '/ReportNaves/Buscar',
                    datatype: "text",
                    data: data,
                    success: function(StoreData) {
                        CargaPosiciones(StoreData);
                    },
                    error: function (objXMLHttpRequest, textStatus, errorThrown) {
                        HiedeLoading();
                        $("#console-log").addClass("alert alert-danger fade in");
                        // document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;
                        document.getElementById("console-log").innerHTML = "Tipo de Error: " + objXMLHttpRequest.statusText + "Código de Error:" + objXMLHttpRequest.readyState;
                        $("#console-log").fadeIn(5000);
                        $("#console-log").fadeOut(5000);
                    }
                });
            });               
        $("#cd_estado")
    .change(function () {
        $("#cd_estado_h").val($("#cd_estado").val());
        //Se limpia el contenido del dropdownlist
        $("#cd_municipio").empty();
        $("#cd_colonia").empty();
        //$("#cd_tipo_prod").empty();
        storeCargaCombo("/Utils/GetMunicipioByCdEstado",
            "cd_municipio",
            "cd_estado",
            "cd_municipio",
            undefined,
            "cd_estado",
            undefined,
            undefined);
        //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
        //Recargar el plugin para que tenga la funcionalidad del componente
        $("#cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "49%" });
        $("#cd_estado").select2({ placeholder: "Seleccione Estado", width: "49%" });
        //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
        return false;
    });
        $("#cd_municipio")
            .change(function () {
                $("#cd_municipio_h").val($("#cd_municipio").val());
                //Se limpia el contenido del dropdownlist
                $("#cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "cd_colonia",
                    "cd_estado",
                    "cd_municipio",
                    "cd_colonia",
                    "cd_estado",
                    "cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                $("#cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "49%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });
        initMap();

        return false;
    })

function initMap() {

    try {
        pos = 5;

        var latLng = new google.maps.LatLng(24.090303, -102.415217);

        //Definimos algunas opciones del mapa a crear
        var myOptions = {
            center: latLng,//centro del mapa
            zoom: pos,//zoom del mapa
            mapTypeId: google.maps.MapTypeId.ROADMAP //tipo de mapa, carretera, híbrido,etc
        };

        //creamos el mapa con las opciones anteriores y le pasamos el elemento div
        map = new google.maps.Map(document.getElementById("googleMap"), myOptions);
    } catch (e) {
        var iError = e.number;
    } 
    


};

function CargaPosiciones(StoreData) {
    if (StoreData.length <= 0)
    {
        $.msgBox({
            title: "Reporte",
            content: "No hay registros con el filtro seleccionado",
            type: "info"
        });
    }
    else
    {
        pos = 5;
        var arregloDeCadenas = StoreData[0].nb_posicion.split(",");
        // map.setCenter(posicion);
        lat = parseFloat(arregloDeCadenas[0]);
        lng = parseFloat(arregloDeCadenas[1]);
        var myLatlng = new google.maps.LatLng(lat, lng);
        var mapOptions = {
            zoom: 10,
            center: myLatlng
        }
        var map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);

        for (i = 0; i < StoreData.length; i++) {
            var arregloDeCadenas2 = StoreData[i].nb_posicion.split(",");
            // map.setCenter(posicion);
            lat = parseFloat(arregloDeCadenas2[0]);
            lng = parseFloat(arregloDeCadenas2[1]);
            myLatlng = new google.maps.LatLng(lat, lng);
            var marker = new google.maps.Marker({
                position: myLatlng
            });

            // To add the marker to the map, call setMap();
            marker.setMap(map);
    }
    
    }


};
function setGeneraPdfSum(Url) {
    //var idRegistro = $("#cd_nave").val();
    //var lngRegistro = $("#cd_terreno").val();
    //var lngRegistro = tp;
    //var data = { id: idRegistro, lng: lngRegistro };
    var id_estado = $("#cd_estado").val();
    if (id_estado == null || id_estado == "") {
        alert('Debe de seleccionar un estado');
        return false;
    }

    var id_municipio = $("#cd_municipio").val();
    if (id_municipio == null || id_municipio == "") {
        id_municipio = 0;
    }

    var id_colonia = $("#cd_colonia").val();
    if (id_colonia == null || id_colonia == "") {
        id_colonia = 0;
    }

    data = { cd_estado: id_estado, cd_municipio: id_municipio, cd_colonia: id_colonia };
    var datos = $.ajax({
        type: 'POST',
        url: Url,
        data: data,
        success: function (StoreData) {
            if (StoreData.Name == "") {
                alert("No se encontraron datos, favor de validar");
            } else {
                if (StoreData.type == "1") {
                    alert(StoreData.Name);
                }
                else {
                    document.location = StoreData.Name;
                    HiedeLoading();
                }
            }
        },
        //Mensaje de error en caso de fallo
        error: function (objXMLHttpRequest) {
            HiedeLoading();
            $("#console-log").addClass("alert alert-danger fade in");
            // document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;
            document.getElementById("console-log").innerHTML = "Tipo de Error: " + objXMLHttpRequest.statusText + "Código de Error:" + objXMLHttpRequest.readyState;
            $("#console-log").fadeIn(5000);
            $("#console-log").fadeOut(5000);
        },
        complete: function (objeto, exito) {
        }
    });
    datos.done();
    datos.fail(function (jqXHR, textStatus) {
        HiedeLoading();        
        $("#console-log").addClass("alert alert-danger fade in");
        //document.getElementById("console-log").innerHTML = jqXHR.responseText;
        document.getElementById("console-log").innerHTML = "Tipo de Error: " + jqXHR.statusText + "Código de Error:" + jqXHR.readyState;
        $("#console-log").fadeIn(5000);
        $("#console-log").fadeOut(5000);

    });
};
function setGeneraExcel(Url)
{
    var id_estado = $("#cd_estado").val();
    if (id_estado == null || id_estado == "") {
        alert('Debe de seleccionar un estado');
        return false;
    }

    var id_municipio = $("#cd_municipio").val();
    if (id_municipio == null || id_municipio == "") {
        id_municipio = 0;
    }

    var id_colonia = $("#cd_colonia").val();
    if (id_colonia == null || id_colonia == "") {
        id_colonia = 0;
    }

    data = { estado: id_estado, municipio: id_municipio, colonia: id_colonia };
    var datos = $.ajax({
        type: 'POST',
        url: Url,
        //data:JSON.stringify(data),
        data: data,
        //contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        success: function (data) {
            //document.location = StoreData.Name;
            //$('#result').html(data);
            window.location = '/ExportData/Index/?estado=' + id_estado + '&municipio=' + id_municipio + '&colonia=' + id_colonia;
            HiedeLoading();
        },
        //dataType: 'html',
        //dataType: "json",
        //Mensaje de error en caso de fallo
        error: function (objXMLHttpRequest, textStatus, errorThrown) {
            HiedeLoading();
            $("#console-log").addClass("alert alert-danger fade in");
            // document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;
            document.getElementById("console-log").innerHTML = "Tipo de Error: " + objXMLHttpRequest.statusText + "Código de Error:" + objXMLHttpRequest.readyState;
            $("#console-log").fadeIn(5000);
            $("#console-log").fadeOut(5000);
        },
        complete: function (objeto, exito) {
        }
    });
    datos.done();
    datos.fail(function (jqXHR, textStatus) {
        HiedeLoading();
        $("#console-log").addClass("alert alert-danger fade in");
        //document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;        
        document.getElementById("console-log").innerHTML = "Tipo de Error: " + jqXHR.statusText + "Código de Error:" + jqXHR.readyState;
        $("#console-log").fadeIn(5000);
        $("#console-log").fadeOut(5000);
    });
}
function setGeneraPdf(Url) {
    var id_estado = $("#cd_estado").val();
    if (id_estado == null || id_estado == "") {
        alert('Debe de seleccionar un estado');
        return false;
    }

    var id_municipio = $("#cd_municipio").val();
    if (id_municipio == null || id_municipio == "") {
        id_municipio = 0;
    }

    var id_colonia = $("#cd_colonia").val();
    if (id_colonia == null || id_colonia == "") {
        id_colonia = 0;
    }
   // $('input:radio[name=user_site]:checked').val();
    var id_idioma = $('input:radio[name=idioma]:checked').val();
    //var id_idioma2 = $("#ing").val();
    //var id_lenguaje;

    //if (id_idioma1 == false && id_idioma2 == false)
    //{
    //    alert("Debe de seleccionar un idioma");
    //}

    //if (id_idioma1 == true)
    //{
    //    id_lenguaje = 1;
    //}

    //if (id_idioma2 == true)
    //{
    //    id_lenguaje = 2;
    //}

    //if (id_idioma == null)
    //{
    //    $.msgBox({
    //        title: "Reporte",
    //        content: "Necesita seleccionar un idioma ",
    //        type: "alert"
    //    });
    //}


    if ($("#cd_titulo").val() == null)
    {
        $.msgBox({
            title: "Reporte",
            content: "Debe proporcionar un zoom ",
            type: "alert"
        });
    }
    data = { cd_estado: id_estado, cd_municipio: id_municipio, cd_colonia: id_colonia, cd_titulo: $("#cd_titulo").val(), cd_idioma: id_idioma };
    var datos = $.ajax({
        type: 'POST',
        url: Url,
        data: data,
        success: function (StoreData) {
            document.location = StoreData.Name;
            HiedeLoading();
        },
        //Mensaje de error en caso de fallo
        error: function (objXMLHttpRequest, textStatus, errorThrown) {
            HiedeLoading();
            $("#console-log").addClass("alert alert-danger fade in");
            // document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;
            document.getElementById("console-log").innerHTML = "Tipo de Error: " + objXMLHttpRequest.statusText + "Código de Error:" + objXMLHttpRequest.readyState;
            $("#console-log").fadeIn(5000);
            $("#console-log").fadeOut(5000);
        },
        complete: function (objeto, exito) {
        }
    });
    datos.done();
    datos.fail(function (jqXHR, textStatus) {
        HiedeLoading();
        $("#console-log").addClass("alert alert-danger fade in");
        //document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;        
        document.getElementById("console-log").innerHTML = "Tipo de Error: " + jqXHR.statusText + "Código de Error:" + jqXHR.readyState;        
        $("#console-log").fadeIn(5000);
        $("#console-log").fadeOut(5000);
    });
};
