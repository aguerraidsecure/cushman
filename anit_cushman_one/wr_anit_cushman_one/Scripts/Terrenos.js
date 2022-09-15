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
function set_Controles(queHago) {
    $(".form-control")
                    .each(function () {
                        var elemento = this;
                        //var tipo = elemento.Attr().tipo;
                        //if(tipo == 'text')
                        //if (elemento.name == 'cd_mercado') {
                        //    alert.toString('algo');
                        //}
                        elemento.disabled = queHago;
                    });
    $("#frm-terreno")
                .find('select')
                .each(function () {
                    var elemento = this;
                    elemento.disabled = queHago;                    
                });
};

function set_Movimiento(queMovimiento) {
    //Contacto General
    $("#tsg001_terreno_cd_estado").empty();
    $("#tsg001_terreno_cd_municipio").empty();
    $("#tsg001_terreno_cd_colonia").empty();
    //Propietario
    $("#tsg028_te_contacto_Prop_cd_estado").empty();
    $("#tsg028_te_contacto_Prop_cd_municipio").empty();
    $("#tsg028_te_contacto_Prop_cd_colonia").empty();
    //Administrador
    $("#tsg028_te_contacto_Adm_cd_estado").empty();
    $("#tsg028_te_contacto_Adm_cd_municipio").empty();
    $("#tsg028_te_contacto_Adm_cd_colonia").empty();
    //Contacto
    $("#tsg028_te_contacto_Corr_cd_estado").empty();
    $("#tsg028_te_contacto_Corr_cd_municipio").empty();
    $("#tsg028_te_contacto_Corr_cd_colonia").empty();

    $("#tsg001_terreno_cd_mercado").empty();
    $("#tsg001_terreno_cd_corredor").empty();
    $("#tsg001_terreno_nu_tamano").empty();
    $("#tsg027_te_servicio_cd_esp_fer").empty();

    $("#tsg027_te_servicio_cd_telefonia").empty();

    //Keys
    $("#tsg001_terreno_cd_estado_h").val(0);
    $("#tsg001_terreno_cd_municipio_h").val(0);
    $("#tsg001_terreno_cd_colonia_h").val(0);
    
    $("#tsg027_te_servicio_cd_telefonia_h").val(0);

    $("#tsg001_terreno_cd_mercado_h").val(0);
    $("#tsg001_terreno_cd_corredor_h").val(0);
    $("#tsg001_terreno_st_parque_ind_h").val(0);
    $("#tsg027_te_servicio_cd_esp_fer_h").val(0);
    $("#tsg023_ni_precio_cd_cond_arr_h").val(0);
    $("#tsg027_te_servicio_cd_tp_gas_natural_h").val(0);
    $("#tsg028_te_contacto_Prop_cd_estado_h").val(0);
    $("#tsg028_te_contacto_Prop_cd_municipio_h").val(0);
    $("#tsg028_te_contacto_Prop_cd_colonia_h").val(0);
    $("#tsg028_te_contacto_Adm_cd_estado_h").val(0);
    $("#tsg028_te_contacto_Adm_cd_municipio_h").val(0);
    $("#tsg028_te_contacto_Adm_cd_colonia_h").val(0);
    $("#tsg028_te_contacto_Corr_cd_estado_h").val(0);
    $("#tsg028_te_contacto_Corr_cd_municipio_h").val(0);
    $("#tsg028_te_contacto_Corr_cd_colonia_h").val(0);

    storeCargaCombo("/Utils/GetCdEstado", "tsg001_terreno_cd_estado");
    storeCargaCombo("/Utils/GetCdEstado", "tsg028_te_contacto_Prop_cd_estado");
    storeCargaCombo("/Utils/GetCdEstado", "tsg028_te_contacto_Adm_cd_estado");
    storeCargaCombo("/Utils/GetCdEstado", "tsg028_te_contacto_Corr_cd_estado");
    storeCargaCombo("/Utils/GetMercados", "tsg001_terreno_cd_mercado");
    //storeCargaCombo("/Utils/GetCdEstado", "tsg001_terreno_cd_mercado");
   
    $("#tsg001_terreno_st_parque_ind option[value=1]").attr("selected", true);
    $("#tsg027_te_servicio_nb_tp_gas_natural option[value=1]").attr("selected", true);
    $("#tsg023_ni_precio_cd_cond_arr option[value=1]").attr("selected", true);
    



    $('#tab1-tab').trigger('click');
    set_Controles(false);
    $("input#nu_inventario").focus();

};
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};


function storeCargaTerrenos(Url) {


    var disp_desde = $("#tsg026_te_dt_gral_nu_disponibilidad").val();
    var disp_hasta = $("#disponible_hasta").val();
    if ($("#disponible_hasta").val().length > 0) {
        data = { disp_desde_p: disp_desde, disp_hasta_p: disp_hasta };
        $.ajax({
            type: 'POST',
            url: '/Terrenos1/GetTerrenoDisponible',
            datatype: "text",
            data: data,
            success: function (StoreData) {
                if (StoreData.length === 0) {
                    alert("No hay datos");
                } else {
                    loadData(StoreData);
                }
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
    }
    else {
        var data = {};
        data = JSON.stringify($('#frm-terreno').serializeObject());
        $.ajax({
            type: 'POST',
            //Llamado al metodo  en el controlador
            url: Url,
            dataType: 'json',
            contentType: "application/json",
            //Parametros que se envian al metodo del controlador
            data: data,
            //En caso de resultado exitosos
            success: function (StoreData) {
                if (StoreData.length === 0) {
                    alert("No hay datos");
                } else {
                    loadData(StoreData);
                }
            },
            //Mensaje de error en caso de fallo
            error: function (jqXHR, textStatus, errorThrown) {
                $.msgBox({
                    title: "dotProyAlmacen",
                    content: "Error en el servidor: " + errorThrown,
                    type: "alert"
                });
            }
        });
    }
};

function cargaArchivo() {
    //$.ajax({
    //    type:'POST', 
    //    dataType: 'json',
    //    url: '/Terrenos1/UploadFiles',
    //    autoUpload: true,
    //    done: function (e, data) {
    //        $('.file_name').html(data.result.name);
    //        $('.file_type').html(data.result.type);
    //        $('.file_size').html(data.result.size);
    //    }
    //})
};

function loadData(dataStoreData) {


    var tab = $('<table class= "table table-striped table-hover"></table>');
    var thead = $('<thead></thead>');
    var trowh = $('<tr class="bg-primary"></tr>');
    trowh.append('<th>Id</th>');
    trowh.append('<th>Terreno</th>');    
    trowh.append('<th></th>');
    thead.append(trowh);
    tab.append(thead);
    var tbody = $('<tbody></tbody>');
    $.each(dataStoreData,
        function (i, val) {
            var trow = $('<tr></tr>');
            trow.append('<td>T' + val[0].cd_terreno + '</td>');
            trow.append('<td>' + val[0].nb_comercial + '</td>');
            var th_link = $('<td><t/d>');
            th_link
                .html('<a class="glyphicon glyphicon-edit" href="/Terrenos1/Crear/' + val[0].cd_terreno + '"> </a>');
            trow.append(th_link);

            tbody.append(trow);
        });

    tab.append(tbody);

    $("#tbTerrenos").html(tab);
};

function loadArchivos(dataStoreData) {


    var tab = $('<table class= "table table-striped table-hover"></table>');
    var thead = $('<thead></thead>');
    var trowh = $('<tr class="bg-primary"></tr>');

    //trowh.append('<th>Id</th>');
    trowh.append('<th>Clave / Key</th>');
    trowh.append('<th>Nombre Archivo / File name</th>');
    trowh.append('<th>Eliminar / Delete</th>');
    trowh.append('<th>Descargar / Download</th>');
    trowh.append('<th></th>');
    thead.append(trowh);
    tab.append(thead);
    var tbody = $('<tbody></tbody>');
    $.each(dataStoreData,
        function (i, val) {
            var trow = $('<tr></tr>');
            trow.append('<td>' + val.cd_id + '</td>');
            trow.append('<td>' + val.nb_archivo + '</td>');
            var th_link = $('<td><t/d>');
            /* th_link
                 .html('<a class="glyphicon glyphicon-remove-circle" href="/Terrenos1/EliminarArchivo/' + val.nb_archivo + '"> </a>');*/
            th_link.html('<a class="glyphicon glyphicon-remove-circle" onclick="eliminaarchivo(' + val.cd_id + ',\'' + val.nb_archivo + '\')"> </a>');
            trow.append(th_link);
            var th_link2 = $('<td><t/d>');
            th_link2
               .html('<a class="glyphicon glyphicon-download" onclick="descargararchivo(\'' + val.nb_archivo + '\')"> </a>');
            //th_link2
            //   .html('<a class="glyphicon glyphicon-download" href="/image/T'+val.cd_terreno +'/' + val.nb_archivo + '" target="_blank"> </a>');
            trow.append(th_link2);

            tbody.append(trow);
        });

    tab.append(tbody);

    $("#Archivos").html(tab);
};
function eliminaarchivo(cd_id, strarchivo) {
    data = { id: $("#tsg001_terreno_cd_terreno").val(), claves: cd_id, 'archivo': strarchivo };
    $.ajax({
        type: 'POST',
        url: '/Terrenos1/EliminarArchivo',
        datatype: "text",
        data: data,
        success: function (StoreData) {
            //alert('El archivo fue eliminado correctamente');
            loadArchivos(StoreData);
            $.msgBox({
                title: "Cushman ONE",
                content: "El archivo fue eliminado correctamente",
                type: "alert"
            });

        }
    })
};

function ActualizaArchivo(cd_terreno, cd_reporte)
{
    data = { id: cd_terreno, id_reporte: cd_reporte };
    $.ajax({
        type: 'POST',
        url: '/Terrenos1/actualizaArchivo',
        datatype: "text",
        data: data,
        success: function (StoreData) {
            //alert('El archivo fue guardado correctamente');
            loadArchivos(StoreData);
            $.msgBox({
                title: "Cushman ONE",
                content: "El archivo fue guardado correctamente",
                type: "alert"
            });
        }
    })
};

function descargararchivo(strarchivo) {
    data = { id: +$("#tsg001_terreno_cd_terreno").val(), 'archivo': strarchivo };
    $.ajax({
        type: 'POST',
        url: '/Terrenos1/downloadFile',
        datatype: "text",
        data: data,
        success: function (StoreData) {
            document.location = StoreData.Name;
            //alert('El archivo fue descargo correctamente');
            //$.each(StoreData, function (i, val) {
            //    alert(val);
            //});
            //loadArchivos(StoreData);
        }
    })
};


function descargaarch(){    
    data = { id: +$("#tsg001_terreno_cd_terreno").val() };
    $.ajax({
        type: 'POST',
        url: '/Terrenos1/getFile',
        datatype: "text",
        data: data,
        success: function (StoreData) {            
            loadArchivos(StoreData);
        }
    })
}

// Funcion para Mostar Div Oculto
function mostrar() {
    document.getElementById('oculto').style.display = 'block';
    document.getElementById('oculto1').style.display = 'none';
    initMap();
};

// Funcion para Mostar Div Oculto
function mostrar1() {
    document.getElementById('oculto').style.display = 'none';
    document.getElementById('oculto1').style.display = 'block';
    initMap();
    //initAutocomplete();
};

// Funcion para Mostar Div Oculto
function mostrar2() {
    document.getElementById('oculto2').style.display = 'block';
};
// Funcion para Mostar Div Oculto
function mostrar3() {
    document.getElementById('oculto2').style.display = 'none';
};

///select2
function select2Dropdown(hiddenID, valueID, ph, listAction, getAction, isMultiple) {
    var sid = '#' + hiddenID;
    $(sid).select2({
        placeholder: ph,
        minimumInputLength: 2,
        allowClear: true,
        multiple: isMultiple,
        ajax: {
            url: "/api/CommonApi/" + listAction,
            dataType: 'json',
            data: function (term, page) {
                return {
                    id: term // search term
                };
            },
            results: function (data) {
                return { results: data };
            }
        },
        initSelection: function (element, callback) {
            // the input tag has a value attribute preloaded that points to a preselected make's id
            // this function resolves that id attribute to an object that select2 can render
            // using its formatResult renderer - that way the make text is shown preselected
            var id = $('#' + valueID).val();
            if (id !== null && id.length > 0) {
                $.ajax("/api/CommonApi/" + getAction + "/" + id, {
                    dataType: "json"
                }).done(function (data) { callback(data); });
            }
        },
        formatResult: s2FormatResult,
        formatSelection: s2FormatSelection
    });

    $(document.body).on("change", sid, function (ev) {
        var choice;
        var values = ev.val;
        // This is assuming the value will be an array of strings.
        // Convert to a comma-delimited string to set the value.
        if (values !== null && values.length > 0) {
            for (var i = 0; i < values.length; i++) {
                if (typeof choice !== 'undefined') {
                    choice += ",";
                    choice += values[i];
                }
                else {
                    choice = values[i];
                }
            }
        }

        // Set the value so that MVC will load the form values in the postback.
        $('#' + valueID).val(choice);
    });
}

function s2FormatResult(item) {
    return item.text;
}

function s2FormatSelection(item) {
    return item.text;
}

function mostrar2(id) {
    switch (id) {
        case 1:
            document.getElementById('oculto2').style.display = 'block';
            break;
        case 2:
            document.getElementById('oculto2').style.display = 'none';
            break;
        case 3:
            document.getElementById('oculto3').style.display = 'block';
            break;
        case 4:
            document.getElementById('oculto3').style.display = 'none';
            break;
    }
};


function obt_img() {    
    var x = document.getElementById("tsg001_terreno_cd_terreno");
    if (x.value > 0) {
        document.getElementById('oculto_img').style.display = 'block';
        descargaarch();
    }else
    {
        document.getElementById('oculto_img').style.display = 'none';        
    }
}


function obt_dir() {
    var x = "";
    if (document.getElementById("tsg001_terreno_nu_cp").value != "")        
    {
        x = document.getElementById("tsg001_terreno_nu_cp").value;            
    }
    if (document.getElementById("tsg001_terreno_cd_colonia").value != "") {
        x = x + ", " + document.getElementById("tsg001_terreno_cd_colonia").options[document.getElementById('tsg001_terreno_cd_colonia').selectedIndex].text;        
    }
    if (document.getElementById("tsg001_terreno_nb_calle").value != "") {
        x = x + ", " + document.getElementById("tsg001_terreno_nb_calle").value;
    }
    if (document.getElementById("tsg001_terreno_nu_direcion").value != "") {
        x = x + ", " + document.getElementById("tsg001_terreno_nu_direcion").value;
    }
    if (document.getElementById("tsg001_terreno_cd_municipio").value != "") {
        x = x + ", " + document.getElementById("tsg001_terreno_cd_municipio").options[document.getElementById('tsg001_terreno_cd_municipio').selectedIndex].text;
    }
    if (document.getElementById("tsg001_terreno_cd_estado").value != "") {
        x = x + ", " + document.getElementById("tsg001_terreno_cd_estado").options[document.getElementById('tsg001_terreno_cd_estado').selectedIndex].text;
    }    
    var x1 = document.getElementById("tsg001_terreno_nb_dir_terr");
    x1.value = x;
}



function actu_imp() {
    convertir(1);
    convertir(2);
    convertir(3);
}

function convertir(id) {
    switch (id) {
        case 1:
            //document.getElementById("nu_disponiblidad_pies") = document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() * 0.3048
            var x = document.getElementById("tsg026_te_dt_gral_nu_disponiblidad");
            var x1 = document.getElementById("nu_disponiblidad_pies");
            //x1.value = Math.round((x.value / 0.3048) * 100) / 100;
            x1.value = Math.round((x.value * 10.7639) * 100) / 100;
            break;
        case 2:
            //document.getElementById("nu_tam_min") = document.getElementById("tsg026_te_dt_gral_nu_tam_min").value() * 0.3048;
            var x = document.getElementById("tsg026_te_dt_gral_nu_tam_min");
            var x1 = document.getElementById("nu_tam_min");
            //x1.value = Math.round((x.value / 0.3048) * 100) / 100;
            x1.value = Math.round((x.value * 10.7639) * 100) / 100;
            break;
        case 3:
            //document.getElementById("nu_tam_max") = document.getElementById("tsg026_te_dt_gral_nu_tam_max").value() * 0.3048;
            var x = document.getElementById("tsg026_te_dt_gral_nu_tam_max");
            var x1 = document.getElementById("nu_tam_max");
            //x1.value = Math.round((x.value / 0.3048) * 100) / 100;
            x1.value = Math.round((x.value * 10.7639) * 100) / 100;
            break;
        case 4:
            //document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() = parseFloat(document.getElementById("nu_disponiblidad_pies").value) / 0.3048;
            var x = document.getElementById("tsg026_te_dt_gral_nu_disponiblidad");
            var x1 = document.getElementById("nu_disponiblidad_pies");
            //x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            x.value = Math.round((x1.value / 10.7639) * 100) / 100;
            break;
        case 5:
            //document.getElementById("tsg026_te_dt_gral_nu_tam_min").value() = document.getElementById("nu_tam_min").value / 0.3048;
            var x = document.getElementById("tsg026_te_dt_gral_nu_tam_min");
            var x1 = document.getElementById("nu_tam_min");
            //x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            x.value = Math.round((x1.value / 10.7639) * 100) / 100;
            break;
        case 6:
            //document.getElementById("tsg026_te_dt_gral_nu_tam_max").value() = document.getElementById("nu_tam_max").value / 0.3048;
            var x = document.getElementById("tsg026_te_dt_gral_nu_tam_max");
            var x1 = document.getElementById("nu_tam_max");
            //x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            x.value = Math.round((x1.value / 10.7639) * 100) / 100;
            break;
    }
}


function borrar() {
    var x = document.getElementById("tsg001_terreno_nb_posicion");
    x.value = "";
    var x1 = document.getElementById("tsg001_terreno_nb_poligono");
    x1.value = "";
    initMap();

    if (!document.getElementById("tsg001_terreno_nb_dir_terr").disabled) {
        if (document.getElementById("tsg001_terreno_nb_dir_terr").value != ""){
            codeAddress();
        }
    }
};

function soloNumeros(e)
{
    var keyCode = e.keyCode || e.which;
    if (keyCode < 48 || keyCode > 57) {
        if (keyCode != 8) {
            if (keyCode != 46) {
                if (keyCode != 9) {
                    e.preventDefault();
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }

        }
        else {
            return true;
        }
    }
    return true;
}

var lat = null;
var lng = null;
var map = null;
var geocoder = null;
var marker = null;
var pos = 5;

$(document)
    .ready(function () {
        //select2Dropdown('make-hdn', 'make', 'Search for make(s)', 'SearchMake', 'GetMake', true);


        if ($("#IdAction").val() === "Editar") {
            $("#btnNuevo").attr("disabled", true);
        } else {
            $("#btnNuevo").attr("disabled", false);
        }

        $('[data-toggle="tooltip"]').tooltip();

        //$("#tsg001_terreno_cd_terreno").val(0);

        storeCargaCombo("/Utils/GetCdEstado", "tsg001_terreno_cd_estado");
        storeCargaCombo("/Utils/GetCdEstado", "tsg028_te_contacto_Prop_cd_estado");
        storeCargaCombo("/Utils/GetCdEstado", "tsg028_te_contacto_Adm_cd_estado");
        storeCargaCombo("/Utils/GetCdEstado", "tsg028_te_contacto_Corr_cd_estado");
        //storeCargaCombo("/Utils/GetCdEstado", "tsg001_terreno_cd_mercado");
        storeCargaCombo("/Utils/GetMercados", "tsg001_terreno_cd_mercado");
         //$("#tsg001_terreno_cd_mercado").select2({ placeholder: "Seleccione Mercado", width: "30%" });
        storeCargaCombo("/Utils/GetCorredor", "tsg001_terreno_cd_corredor");
        // $("#tsg001_terreno_cd_corredor").select2({ placeholder: "Seleccione Corredor", width: "30%" });
         storeCargaCombo("/Utils/GetEstatus", "tsg026_te_dt_gral_cd_st_entrega");
       //  $("#tsg026_te_dt_gral_cd_st_entrega").select2({ placeholder: "Seleccione Estatus", width: "30%" });
        storeCargaCombo("/Utils/GetEspuelas", "tsg027_te_servicio_cd_esp_fer");
        //$("#tsg027_te_servicio_cd_esp_fer").select2({ placeholder: "Seleccione Esp Ferr", width: "30%" });
        storeCargaCombo("/Utils/GetCondArrend", "tsg023_ni_precio_cd_cond_arr");
        //$("#tsg023_ni_precio_cd_cond_arr").select2({ placeholder: "Seleccione Cond Arrend", width: "80%" });
        storeCargaCombo("/Utils/GetGas", "tsg027_te_servicio_cd_tp_gas_natural");
        //$("#tsg027_te_servicio_cd_tp_gas_natural").select2({ placeholder: "Seleccione Tp Gas", width: "80%" });
        storeCargaCombo("/Utils/GetTelefonia", "tsg027_te_servicio_cd_telefonia");
        //$("#tsg027_te_servicio_cd_telefonia").select2({ placeholder: "Seleccione Telefonía", width: "30%" });

        storeCargaCombo("/Utils/GetTipoMoneda", "tsg023_ni_precio_cd_moneda");
        //$("#tsg023_ni_precio_cd_moneda").select2({ placeholder: "Seleccione Tipo Moneda", width: "30%" });
        storeCargaCombo("/Utils/GetTipoReporte", "tsg040_imagenes_terrenos_cd_reporte");
        //$("#tsg040_imagenes_terrenos_cd_reporte").select2({ placeholder: "Seleccione Reporte", width: "30%" });
        storeCargaCombo("/Utils/GetTipoMoneda", "tsg023_ni_precio_cd_rep_moneda");
        //$("#tsg023_ni_precio_cd_rep_moneda").select2({ placeholder: "Seleccione Tipo Moneda", width: "30%" });

        

        $("#nu_inventario").focus();

        msgBoxImagePath = "/image/";

        //onchange
        //$("#tsg026_te_dt_gral_nu_disponiblidad")
        //  .change(function () {
        //      convertir(1);
        //      return false;
        //  });

        $("#tsg026_te_dt_gral_nu_disponiblidad")
                  .change(function () {
                      convertir(1);
                      $("#tsg023_ni_precio_im_total").val($("#tsg026_te_dt_gral_nu_disponiblidad").val() * $("#tsg023_ni_precio_im_venta").val());
                      return false;
                  });
        $("#tsg023_ni_precio_im_venta")
          .change(function () {
              
              $("#tsg023_ni_precio_im_total").val($("#tsg026_te_dt_gral_nu_disponiblidad").val() * $("#tsg023_ni_precio_im_venta").val());
              return false;
          });

        $("#tsg023_ni_precio_nu_tipo_cambio")
                  .change(function () {
                      $("#nu_total_mxn").val($("#tsg023_ni_precio_im_total").val() * $("#tsg023_ni_precio_nu_tipo_cambio").val());
                      $("#nu_total_usd").val($("#tsg023_ni_precio_im_total").val());
                      if (document.getElementById("tsg023_ni_precio_cd_moneda").options[document.getElementById('tsg023_ni_precio_cd_moneda').selectedIndex].text == "USD") {
                          $("#tsg023_ni_precio_im_total").val($("#nu_total_usd").val())
                      }
                      else {
                          $("#tsg023_ni_precio_im_total").val($("#nu_total_mxn").val())
                      }
                      return false;
                  });

        $("#tsg023_ni_precio_cd_moneda")
                  .change(function () {
                      $("#nu_total_mxn").val($("#tsg023_ni_precio_im_total").val() * $("#tsg023_ni_precio_nu_tipo_cambio").val());
                      $("#nu_total_usd").val($("#tsg023_ni_precio_im_total").val());
                      if (document.getElementById("tsg023_ni_precio_cd_moneda").options[document.getElementById('tsg023_ni_precio_cd_moneda').selectedIndex].text == "USD") {
                          $("#tsg023_ni_precio_im_total").val($("#nu_total_usd").val())
                      }
                      else {
                          $("#tsg023_ni_precio_im_total").val($("#nu_total_mxn").val())
                      }
                      return false;
                  });
        $("#tsg026_te_dt_gral_nu_tam_min")
          .change(function () {
              convertir(2);
              return false;
          });

        $("#tsg026_te_dt_gral_nu_tam_max")
          .change(function () {
              convertir(3);
              return false;
          });

        $('#frm-terreno').on('keyup keypress', function (e) {
            var keyCode = e.keyCode || e.which;
            if (keyCode === 13) {
                e.preventDefault();
                return false;
            }
        });


        $("#tsg026_te_dt_gral_nu_disponiblidad").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_disponiblidad_pies").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg026_te_dt_gral_nu_tam_min").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_tam_min").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg026_te_dt_gral_nu_tam_max").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_tam_max").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg023_ni_precio_im_renta").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg023_ni_precio_im_venta").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg023_ni_precio_im_total").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg023_ni_precio_nu_tipo_cambio").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_total_mxn").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_total_usd").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg023_ni_precio_nu_ma_ac").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg001_terreno_nu_cp")
         .change(function () {
             setCP("/Terrenos1/BuscaCP", 1);
         });

        $("#tsg028_te_contacto_Prop_nu_cp")
         .change(function () {
             setCP("/Terrenos1/BuscaCP", 2);
         });

        $("#tsg028_te_contacto_Adm_nu_cp")
         .change(function () {
             setCP("/Terrenos1/BuscaCP", 3);
         });

        $("#tsg028_te_contacto_Corr_nu_cp")
         .change(function () {
             setCP("/Terrenos1/BuscaCP", 4);
         });


        function cargaCP(storedata, estado, municipio, colonia) {
           /* var Msg = storedata[0].cd_estado;
            $("#" + estado).val(Msg);
            $("#" + estado).trigger("change");

            var Msg2 = storedata[0].cd_municipio;
            $("#" + municipio).val(Msg2);

            var Msg3 = storedata[0].cd_colonia;
            $("#" + colonia).val(Msg3);*/
            var Msg = storedata.Result[0].cd_estado;
            $("#" + estado).val(Msg);
            $("#" + estado + "_h").val(Msg);
            $("#" + estado).trigger("change");

            var Msg2 = storedata.Result[0].cd_municipio;
            $("#" + municipio).val(Msg2);

            var Msg3 = storedata.Result[0].cd_colonia;
            $("#" + colonia).val(Msg3);
        }


        function setCP(Url, tp) {
            if (tp == 1) {
                var idRegistro = $("#tsg001_terreno_nu_cp").val();
            }
            if (tp == 2) {
                var idRegistro = $("#tsg028_te_contacto_Prop_nu_cp").val();
            }
            if (tp == 3) {
                var idRegistro = $("#tsg028_te_contacto_Adm_nu_cp").val();
            }
            if (tp == 4) {
                var idRegistro = $("#tsg028_te_contacto_Corr_nu_cp").val();
            }
            var data = { id: idRegistro };
            var datos = $.ajax({
                type: 'GET',
                url: Url,
                data: data,
                success: function (storedata) {
                    //alert("resultado de storedata: " + storedata[0].cd_colonia + " " + storedata[0].cd_municipio + " " + storedata[0].cd_estado);
                    if (tp == 1) {                      
                        cargaCP(storedata, "tsg001_terreno_cd_estado", "tsg001_terreno_cd_municipio_h", "tsg001_terreno_cd_colonia_h");
                    }
                    if (tp == 2) {
                        cargaCP(storedata, "tsg028_te_contacto_Prop_cd_estado", "tsg028_te_contacto_Prop_cd_municipio_h", "tsg028_te_contacto_Prop_cd_colonia_h");
                    }
                    if (tp == 3) {
                        cargaCP(storedata, "tsg028_te_contacto_Adm_cd_estado", "tsg028_te_contacto_Adm_cd_municipio_h", "tsg028_te_contacto_Adm_cd_colonia_h");
                    }
                    if (tp == 4) {
                        cargaCP(storedata, "tsg028_te_contacto_Corr_cd_estado", "tsg028_te_contacto_Corr_cd_municipio_h", "tsg028_te_contacto_Corr_cd_colonia_h");
                    }
                },
                //Mensaje de error en caso de fallo
                error: function (objXMLHttpRequest) {
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
                $("#console-log").addClass("alert alert-danger fade in");
                //document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;        
                document.getElementById("console-log").innerHTML = "Tipo de Error: " + jqXHR.statusText + "Código de Error:" + jqXHR.readyState;
                $("#console-log").fadeIn(5000);
                $("#console-log").fadeOut(5000);
            });
        };




        $("#frm-terreno")
            .submit(function () {
                var form = $(this);
                if ($("#tsg001_terreno_cd_terreno").val() == "")
                {
                    $("#tsg001_terreno_cd_terreno").val(0);
                }
                //Aquí se pueden hacer las validaciones con JS
                if (form.validate()) {
                    form.ajaxSubmit({
                        dataType: 'JSON',
                        type: 'POST',
                        url: form.attr('action'),
                        success: function (r) {
                            if (r.respuesta) {
                                //  alert("Guarde");
                                $.msgBox({
                                    title: "Cushman ONE",
                                    content: "registro guardado con el Folio: T" + r.redirect + "\nDesea capturar otro registro?",
                                    type: "confirm",
                                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                                    success: function (result) {
                                        if (result == "No") {
                                            //window.location.href = r.redirect;
                                            set_Controles(true);
                                        } else {
                                            //document.getElementById("frm-terreno").reset();
                                            //set_Movimiento(1);
                                            //$(".form-control")
                                            //  .find('select')
                                            //      .each(function () {
                                            //          var elemento = this;
                                            //          elemento.value = "";
                                            //      });
                                            window.location.replace("../../Terrenos1/Crear");
                                            $("#btnNuevo").attr("disabled", false);
                                            
                                        }
                                    }
                                });
                            } else {
                                // alert("falló");
                                $.msgBox({
                                    title: "ProyCushman",
                                    content: r.error,
                                    type: "alert"
                                });
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            // alert("error");
                            $.msgBox({
                                title: "ProyCushman",
                                content: "Error en el servidor: " + errorThrown,
                                type: "alert"
                            });
                        }
                    });
                }

                return false;
            });


        $("#btnCancelar").click(function () {
            window.location.href = "Index";
        });

        $("#btnCerrar").click(function () {
            $("#btnNuevo").attr("disabled", false);
        });

        $("#btnBuscar")
            .on('click',
                function (evt) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    $("#btnNuevo").attr("disabled", true);
                    var $detailDiv = $('#findTerreno'),
                        url = '/Terrenos1/Buscar';
                    $.get(url, function (data) {
                        $detailDiv.html(data);
                    });
                    $("#myModal").modal({
                        backdrop: false,
                        keyboard: true
                    });
                    storeCargaTerrenos("/Terrenos1/GetTerreno");
                });
        $("#btnBuscarTodo")
            .on('click',
                function (evt) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    $("#btnNuevo").attr("disabled", true);
                    var $detailDiv = $('#findTerreno'),
                        url = '/Terrenos1/Buscar';
                    $.get(url, function (data) {
                        $detailDiv.html(data);
                    });
                    $("#myModal").modal({
                        backdrop: false,
                        keyboard: true
                    });
                    storeCargaTerrenos("/Terrenos1/GetTerrenoAll");
                });


        $("#btnEliminar")
            .click(function () {
                $.msgBox({
                    title: "Cushman ONE",
                    content: "Está seguro de eliminar el registro?",
                    type: "confirm",
                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                    success: function (result) {
                        if (result === "Si") {
                            //setBorraRegistro("/Producto/Eliminar/" + $("#cd_producto").val());
                            setBorraRegistro("/Terrenos1/EliminaTerrenos");
                        }
                    }
                });

            });

        
        $("#btnPpt")
            .click(function () {
                $.msgBox({
                    title: "Cushman ONE",
                    content: "Está seguro de generar la presentación?",
                    type: "confirm",
                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                    success: function (result) {
                        if (result === "Si") {
                            //setBorraRegistro("/Producto/Eliminar/" + $("#cd_producto").val());
                            ShowLoading();
                            setGeneraPdf("/Terrenos1/GeneraPpt", 1);
                        }
                    }
                });
            });
        $("#btnPdf")
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
                            setGeneraPdf("/Terrenos1/GeneraPdf",1);
                        }
                    }
                });
            });

        $("#btnPdf1")
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
                            setGeneraPdf("/Terrenos1/GeneraPdf", 2);
                        }
                    }
                });
            });


        function setGeneraPdf(Url, tp) {
            var idRegistro = $("#tsg001_terreno_cd_terreno").val();
            //var lngRegistro = $("#cd_terreno").val();
            var lngRegistro = tp;
            var data = { id: idRegistro, lng: lngRegistro };
            var datos = $.ajax({
                type: 'GET',
                url: Url,
                data: data,
                success: function (StoreData) {
                    if (StoreData.Name == "") {
                        alert("No se encontraron datos, favor de validar");
                    } else {
                        document.location.href = StoreData.Name;                       
                        HiedeLoading();
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
                //document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;        
                document.getElementById("console-log").innerHTML = "Tipo de Error: " + jqXHR.statusText + "Código de Error:" + jqXHR.readyState;
                $("#console-log").fadeIn(5000);
                $("#console-log").fadeOut(5000);
            });
        };




        function setBorraRegistro(Url) {
            var idRegistro = $("#tsg001_terreno_cd_terreno").val();
            var data = { id: idRegistro };
            var datos = $.ajax({
                type: 'GET',
                url: Url,
                data: data,
                success: function () {
                    window.location.replace("../../Terrenos1/Crear");
                },
                //Mensaje de error en caso de fallo
                error: function (objXMLHttpRequest) {
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
                $("#console-log").addClass("alert alert-danger fade in");
                //document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;        
                document.getElementById("console-log").innerHTML = "Tipo de Error: " + jqXHR.statusText + "Código de Error:" + jqXHR.readyState;
                $("#console-log").fadeIn(5000);
                $("#console-log").fadeOut(5000);
            });
        };



        //$("#btnNuevo").click(function () {
        //    $("#frm-terreno").find(':input:text,select').each(function () {
        //        var elemento = this;
        //        elemento.disabled = (elemento.disabled ? false : true);
        //    });
        //})
        $("#btnNuevo").click(function () {
            $("#frm-terreno").find(':input,select').each(function () {
                var elemento = this;
                //alert(elemento.id);
                if ((elemento.type != 'button') && (elemento.type != 'radio') && (elemento.type != 'submit')) {
                    //alert(elemento.id);
                   // elemento.disabled = (elemento.disabled ? false : true);
                }
            });
        })

        //OANR 05/08/2015
        $("#btnEditar").click(function () {
            //$("#frm-terreno").find(':input,select').each(function () {
            //    var elemento = this;
            //    //alert(elemento.id);
            //    if ((elemento.type != 'button') && (elemento.type != 'radio') && (elemento.type != 'submit')) {
            //        //alert(elemento.id);
            //        elemento.disabled = (elemento.disabled ? false : true);
            //    }
            //});
            set_Controles(false);
            $("input#tsg001_terreno_nb_comercial").focus();

        })
        $("#btnBuscarFolio").click(function () {
            var id_nave = $("#folio").val().replace("T", "");
            window.location.replace("../../Terrenos1/Crear/" + id_nave);
        })

        $("#btnCopiar").click(function () {
            set_Controles(false);
            $("input#tsg001_terreno_nb_comercial").focus();
            $("#tsg001_terreno_cd_terreno").val(0);
        })

        $("#archivoKml").fileupload({
            dataType: 'json',
            url: '/Terrenos1/UploadKml/' + $("#tsg001_terreno_cd_terreno").val(),
            autoUpload: true,
            success: function (data) {
                document.getElementById("tsg001_terreno_nb_poligono").value = data.Name;
                document.getElementById("tsg001_terreno_nb_posicion").value = data.Type;
                //$('.file_name').html(data.result.name);
                //$('.file_type').html(data.result.type);
                //$('.file_size').html(data.result.size);
                initMap();
            }            
        })

        //$('#fileupload').change(function(){
        //    alert("cargo un archivo");
        //    cargaArchivo();

        
        //})
        $('#fileupload').fileupload({
            dataType: 'json',
            url: '/Terrenos1/UploadFiles/' + $("#tsg001_terreno_cd_terreno").val(),
            autoUpload: true,
            success: function (data) {
                ActualizaArchivo(data[0].cd_id, $("#tsg040_imagenes_terrenos_cd_reporte").val())
                //loadArchivos(data);
                //$('.file_name').html(data.result.name);
                //$('.file_type').html(data.result.type);
                //$('.file_size').html(data.result.size);
            }
        })

        $("#tsg027_te_servicio_cd_telefonia")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg027_te_servicio_cd_telefonia_h").val($("#tsg027_te_servicio_cd_telefonia").val());
            return false;
        });


        $("#tsg026_te_dt_gral_cd_st_entrega")
       .change(function () {
           //Se limpia el contenido del dropdownlist
           $("#tsg026_te_dt_gral_cd_st_entrega_h").val($("#tsg026_te_dt_gral_cd_st_entrega").val());
           return false;
       });
        $("#tsg001_terreno_cd_mercado")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg001_terreno_cd_mercado_h").val($("#tsg001_terreno_cd_mercado").val());
            return false;
        });

        $("#tsg001_terreno_cd_corredor")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg001_terreno_cd_corredor_h").val($("#tsg001_terreno_cd_corredor").val());
            return false;
        });

        $("#tsg001_terreno_st_parque_ind")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg001_terreno_st_parque_ind_h").val($("#tsg001_terreno_st_parque_ind").val());
            return false;
        });
        
        $("#tsg027_te_servicio_cd_esp_fer")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg027_te_servicio_cd_esp_fer_h").val($("#tsg027_te_servicio_cd_esp_fer").val());
            return false;
        });
        $("#tsg023_ni_precio_cd_moneda")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg023_ni_precio_cd_moneda_h").val($("#tsg023_ni_precio_cd_moneda").val());
            return false;
        });
        $("#tsg023_ni_precio_cd_rep_moneda")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg023_ni_precio_cd_rep_moneda_h").val($("#tsg023_ni_precio_cd_rep_moneda").val());
            return false;
        });

        $("#tsg027_te_servicio_cd_tp_gas_natural")
                  .change(function () {
                      //Se limpia el contenido del dropdownlist
                      $("#tsg027_te_servicio_cd_tp_gas_natural_h").val($("#tsg027_te_servicio_cd_tp_gas_natural").val());

                      if (document.getElementById("tsg027_te_servicio_cd_tp_gas_natural").options[document.getElementById('tsg027_te_servicio_cd_tp_gas_natural').selectedIndex].text == "Otro / Other") {
                          // Funcion para Mostar Div Oculto                                                                                                                               
                          mostrar2(1);
                      }
                      else {
                          // Funcion para Mostar Div Oculto
                          mostrar2(2);
                      }
                      return false;
                  });

        $("#tsg023_ni_precio_cd_cond_arr")
           .change(function () {
               $("#tsg023_ni_precio_cd_cond_arr_h").val($("#tsg023_ni_precio_cd_cond_arr").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg023_ni_precio_cd_cond_arr").options[document.getElementById('tsg023_ni_precio_cd_cond_arr').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(3);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(4);
               }
               return false;
           });


        $("#tsg001_terreno_cd_mercado")
            .change(function () {
                $("#tsg001_terreno_cd_mercado_h").val($("#tsg001_terreno_cd_mercado").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg001_terreno_cd_corredor").empty();
                //$("#tsg001_terreno_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetCorredorByCdMercado",
                    "tsg001_terreno_cd_corredor",
                    "tsg001_terreno_cd_mercado",
                    "tsg001_terreno_cd_corredor",
                    undefined,
                    "tsg001_terreno_cd_mercado",
                    undefined,
                    undefined);
                return false;
            });


        $("#tsg001_terreno_cd_estado")
            .change(function () {
                $("#tsg001_terreno_cd_estado_h").val($("#tsg001_terreno_cd_estado").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg001_terreno_cd_municipio").empty();
                $("#tsg001_terreno_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetMunicipioByCdEstado",
                    "tsg001_terreno_cd_municipio",
                    "tsg001_terreno_cd_estado",
                    "tsg001_terreno_cd_municipio",
                    undefined,
                    "tsg001_terreno_cd_estado",
                    undefined,
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
                //Recargar el plugin para que tenga la funcionalidad del componente
                //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
                //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
                //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
                return false;
            });
        $("#tsg001_terreno_cd_municipio")
            .change(function () {
                $("#tsg001_terreno_cd_municipio_h").val($("#tsg001_terreno_cd_municipio").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg001_terreno_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg001_terreno_cd_colonia",
                    "tsg001_terreno_cd_estado",
                    "tsg001_terreno_cd_municipio",
                    "tsg001_terreno_cd_colonia",
                    "tsg001_terreno_cd_estado",
                    "tsg001_terreno_cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                //$("#tsg001_terreno_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "40%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });
       
        //Recargar el plugin para que tenga la funcionalidad del componente
             //$("#tsg001_terreno_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "40%" });
             //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "40%" });
             //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "100%" });


             $("#tsg001_terreno_cd_colonia").change(function () {
                 $("#tsg001_terreno_cd_colonia_h").val($("#tsg001_terreno_cd_colonia").val());
                 var data = { id_estado: $("#tsg001_terreno_cd_estado").val(), id_municipio: $("#tsg001_terreno_cd_municipio").val(), id_colonia: $("#tsg001_terreno_cd_colonia").val() };
                 $.ajax({
                     type : 'GET',
                     url : "/Terrenos1/ObtenerCP",
                     data: data,
                     success: function(storedata)
                     {
                         $("#tsg001_terreno_nu_cp").val(storedata[0].nu_cp);
                     }
                 })
             });

        $("#tsg028_te_contacto_Prop_cd_estado")
            .change(function () {

                $("#tsg028_te_contacto_Prop_cd_estado_h").val($("#tsg028_te_contacto_Prop_cd_estado").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg028_te_contacto_Prop_cd_municipio").empty();
                $("#tsg028_te_contacto_Prop_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetMunicipioByCdEstado",
                    "tsg028_te_contacto_Prop_cd_municipio",
                    "tsg028_te_contacto_Prop_cd_estado",
                    "tsg028_te_contacto_Prop_cd_municipio",
                    undefined,
                    "tsg028_te_contacto_Prop_cd_estado",
                    undefined,
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
                //Recargar el plugin para que tenga la funcionalidad del componente
                //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
                //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
                //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
                return false;
            });
        $("#tsg028_te_contacto_Prop_cd_municipio")
            .change(function () {
                $("#tsg028_te_contacto_Prop_cd_municipio_h").val($("#tsg028_te_contacto_Prop_cd_municipio").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg028_te_contacto_Prop_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg028_te_contacto_Prop_cd_colonia",
                    "tsg028_te_contacto_Prop_cd_estado",
                    "tsg028_te_contacto_Prop_cd_municipio",
                    "tsg028_te_contacto_Prop_cd_colonia",
                    "tsg028_te_contacto_Prop_cd_estado",
                    "tsg028_te_contacto_Prop_cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                //$("#tsg028_te_contacto_Prop_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "40%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });

        //Recargar el plugin para que tenga la funcionalidad del componente
             //$("#tsg028_te_contacto_Prop_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "40%" });
             //$("#tsg028_te_contacto_Prop_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "40%" });
             //$("#tsg028_te_contacto_Prop_cd_estado").select2({ placeholder: "Seleccione Estado", width: "40%" });

             $("#tsg028_te_contacto_Prop_cd_colonia").change(function () {
                 $("#tsg028_te_contacto_Prop_cd_estado_h").val($("#tsg028_te_contacto_Prop_cd_estado").val());
                 var data = { id_estado: $("#tsg028_te_contacto_Prop_cd_estado").val(), id_municipio: $("#tsg028_te_contacto_Prop_cd_municipio").val(), id_colonia: $("#tsg028_te_contacto_Prop_cd_colonia").val() };
                 $.ajax({
                     type: 'GET',
                     url: "/Terrenos1/ObtenerCP",
                     data: data,
                     success: function (storedata) {
                         $("#tsg028_te_contacto_Prop_nu_cp").val(storedata[0].nu_cp);
                     }
                 })
             });

        $("#tsg028_te_contacto_Adm_cd_estado")
            .change(function () {
                $("#tsg028_te_contacto_Adm_cd_estado_h").val($("#tsg028_te_contacto_Adm_cd_estado").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg028_te_contacto_Adm_cd_municipio").empty();
                $("#tsg028_te_contacto_Adm_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetMunicipioByCdEstado",
                    "tsg028_te_contacto_Adm_cd_municipio",
                    "tsg028_te_contacto_Adm_cd_estado",
                    "tsg028_te_contacto_Adm_cd_municipio",
                    undefined,
                    "tsg028_te_contacto_Adm_cd_estado",
                    undefined,
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
                //Recargar el plugin para que tenga la funcionalidad del componente
                //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
                //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
                //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
                return false;
            });
        $("#tsg028_te_contacto_Adm_cd_municipio")
            .change(function () {
                $("#tsg028_te_contacto_Adm_cd_municipio_h").val($("#tsg028_te_contacto_Adm_cd_municipio").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg028_te_contacto_Adm_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg028_te_contacto_Adm_cd_colonia",
                    "tsg028_te_contacto_Adm_cd_estado",
                    "tsg028_te_contacto_Adm_cd_municipio",
                    "tsg028_te_contacto_Adm_cd_colonia",
                    "tsg028_te_contacto_Adm_cd_estado",
                    "tsg028_te_contacto_Adm_cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                //$("#tsg028_te_contacto_Adm_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "40%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });

        //Recargar el plugin para que tenga la funcionalidad del componente
              //$("#tsg028_te_contacto_Adm_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "40%" });
              //$("#tsg028_te_contacto_Adm_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "40%" });
              //$("#tsg028_te_contacto_Adm_cd_estado").select2({ placeholder: "Seleccione Estado", width: "40%" });

              $("#tsg028_te_contacto_Adm_cd_colonia").change(function () {
                  $("#tsg028_te_contacto_Adm_cd_estado_h").val($("#tsg028_te_contacto_Adm_cd_estado").val());
                  var data = { id_estado: $("#tsg028_te_contacto_Adm_cd_estado").val(), id_municipio: $("#tsg028_te_contacto_Adm_cd_municipio").val(), id_colonia: $("#tsg028_te_contacto_Adm_cd_colonia").val() };
                  $.ajax({
                      type: 'GET',
                      url: "/Terrenos1/ObtenerCP",
                      data: data,
                      success: function (storedata) {
                          $("#tsg028_te_contacto_Adm_nu_cp").val(storedata[0].nu_cp);
                      }
                  })
              });

        $("#tsg028_te_contacto_Corr_cd_estado")
            .change(function () {
                $("#tsg028_te_contacto_Corr_cd_estado_h").val($("#tsg028_te_contacto_Corr_cd_estado").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg028_te_contacto_Corr_cd_municipio").empty();
                $("#tsg028_te_contacto_Corr_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetMunicipioByCdEstado",
                    "tsg028_te_contacto_Corr_cd_municipio",
                    "tsg028_te_contacto_Corr_cd_estado",
                    "tsg028_te_contacto_Corr_cd_municipio",
                    undefined,
                    "tsg028_te_contacto_Corr_cd_estado",
                    undefined,
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
                //Recargar el plugin para que tenga la funcionalidad del componente
                //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
                //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
                //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
                return false;
            });
        $("#tsg028_te_contacto_Corr_cd_municipio")
            .change(function () {
                $("#tsg028_te_contacto_Corr_cd_municipio_h").val($("#tsg028_te_contacto_Corr_cd_municipio").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg028_te_contacto_Corr_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg028_te_contacto_Corr_cd_colonia",
                    "tsg028_te_contacto_Corr_cd_estado",
                    "tsg028_te_contacto_Corr_cd_municipio",
                    "tsg028_te_contacto_Corr_cd_colonia",
                    "tsg028_te_contacto_Corr_cd_estado",
                    "tsg028_te_contacto_Corr_cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                //$("#tsg028_te_contacto_Corr_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "40%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });

        //Recargar el plugin para que tenga la funcionalidad del componente
             //$("#tsg028_te_contacto_Corr_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "40%" });
             //$("#tsg028_te_contacto_Corr_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "40%" });
             //$("#tsg028_te_contacto_Corr_cd_estado").select2({ placeholder: "Seleccione Estado", width: "40%" });

             $("#tsg028_te_contacto_Corr_cd_colonia").change(function () {
                 $("#tsg028_te_contacto_Corr_cd_estado_h").val($("#tsg028_te_contacto_Corr_cd_estado").val());
                 var data = { id_estado: $("#tsg028_te_contacto_Corr_cd_estado").val(), id_municipio: $("#tsg028_te_contacto_Corr_cd_municipio").val(), id_colonia: $("#tsg028_te_contacto_Corr_cd_colonia").val() };
                 $.ajax({
                     type: 'GET',
                     url: "/Terrenos1/ObtenerCP",
                     data: data,
                     success: function (storedata) {
                         $("#tsg028_te_contacto_Corr_nu_cp").val(storedata[0].nu_cp);
                     }
                 })
             });
        //*********** Parte de Google map ***************

        jQuery('#pasar').click(function () {
            if (!document.getElementById("tsg001_terreno_nb_dir_terr").disabled) {
                codeAddress();
            }            
            return false;
        });
        //Inicializamos la función de google maps una vez el DOM este cargado
        initMap();

        return false;
    })

function initMap() {

    geocoder = new google.maps.Geocoder();
    pos = 5;
    var posicion = document.getElementById("tsg001_terreno_nb_posicion").value;
    if (posicion != "") {
        // codeAddress(document.getElementById("tsg001_terreno_nb_posicion").value);
        var arregloDeCadenas = posicion.split(",");
        // map.setCenter(posicion);
        lat = parseFloat(arregloDeCadenas[0]);
        lng = parseFloat(arregloDeCadenas[1]);

        pos = 17;
    }
    //Si hay valores creamos un objeto Latlng
    if (lat != null && lng != null) {
        var latLng = new google.maps.LatLng(lat, lng);
    }
    else {
        var latLng = new google.maps.LatLng(24.090303, -102.415217);
    }
    //Definimos algunas opciones del mapa a crear
    var myOptions = {
        center: latLng,//centro del mapa
        zoom: pos,//zoom del mapa
        mapTypeId: google.maps.MapTypeId.ROADMAP //tipo de mapa, carretera, híbrido,etc
    };

    // Aqui se asignan valores si hay coordenadas de poligono
    if (document.getElementById("tsg001_terreno_nb_poligono").value != ""){
        var poligono = [];
        var posicion = document.getElementById("tsg001_terreno_nb_poligono").value;
        var arregloDeCadenas1 = posicion.split("|");
        for (var i = 0; i < arregloDeCadenas1.length; i++) {
            var cad = arregloDeCadenas1[i];
            //cad = cad.split(",");
            var arregloDeCadenas2 = cad.split(",");        
            lat = parseFloat(arregloDeCadenas2[1]);
            lng = parseFloat(arregloDeCadenas2[0]);
            poligono[i] = new google.maps.LatLng(lng,lat );
        }
        miPoligono = new google.maps.Polygon({
            paths: poligono,
            strokeColor: "#FF0000",
            strokeOpacity: 0.8,
            strokeWeight: 3,
            fillColor: "#FF0000",
            fillOpacity: 0.4
        });        
    }


    //creamos el mapa con las opciones anteriores y le pasamos el elemento div
    map = new google.maps.Map(document.getElementById("googleMap"), myOptions);
    
    //Se setea el poligono
    if (document.getElementById("tsg001_terreno_nb_poligono").value != "") {
        miPoligono.setMap(map);
    }

    //creamos el marcador en el mapa
    marker = new google.maps.Marker({
        map: map,//el mapa creado en el paso anterior
        position: latLng,//objeto con latitud y longitud
        draggable: true //que el marcador se pueda arrastrar
    });


    var drawingManager = new google.maps.drawing.DrawingManager({
        drawingMode: google.maps.drawing.OverlayType.MARKER,
        drawingControl: true,
        drawingControlOptions: {
            position: google.maps.ControlPosition.TOP_CENTER,
            drawingModes: [

              google.maps.drawing.OverlayType.POLYGON

            ]
        },
        markerOptions: { icon: 'images/beachflag.png' },
        circleOptions: {
            fillColor: '#ffff00',
            fillOpacity: 1,
            strokeWeight: 15,
            clickable: false,
            editable: true,
            zIndex: 1
        }

    });
    drawingManager.setMap(map);
    //función que actualiza los input del formulario con las nuevas latitudes
    //Estos campos suelen ser hidden
    //updatePosition(latLng);   
    google.maps.event.addListener(drawingManager, 'polygoncomplete', function (Polygon) {
        var vertices = Polygon.getPath();
        var pocision = "";
        for (var i = 0; i < vertices.length; i++) {
            var xy = vertices.getAt(i);
            //pocision += xy.lng() + " , " + xy.lat();
            document.getElementById("tsg001_terreno_nb_poligono").value += xy.lat() + "," + xy.lng() + "|";
            
        }
        document.getElementById("tsg001_terreno_nb_poligono").value += vertices.getAt(0).lat() + "," + vertices.getAt(0).lng() + "|";
        //alert("Datos del poligono:" + document.getElementById("tsg001_terreno_nb_poligono").value);        
    });
    map.addListener('click', function (e) {
        //alert('posicion : ' + e.latLng);
        var ubicacion = String(e.latLng);

        ubicacion = ubicacion.replace("(", "");
        ubicacion = ubicacion.replace(")", "");

        //document.getElementById("tsg002_nave_industrial_nb_posicion").value = e.latLng;
        document.getElementById("tsg001_terreno_nb_posicion").value = ubicacion;
        marker.setMap(null);
        //clearMarkers();
        marker = new google.maps.Marker({
            position: e.latLng
        });
        marker.setMap(map);
        //initMap();
        // reposicionar(e.latLng, map);
    });

}

function cargaArchivo(rutaArchivo)
{
    var mapOptions = {
        center: new google.maps.LatLng(43.321675, -75.172631),
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);
    var ctaLayer = new google.maps.KmlLayer("https://drive.google.com/file/d/0B3bgbKklbtW1bktHWFp3QU5XdEU/view");
    ctaLayer.setMap(map);
}

//funcion que traduce la direccion en coordenadas
function codeAddress() {
    //geocoder = new google.maps.Geocoder();
    //obtengo la direccion del formulario
    var address = document.getElementById("tsg001_terreno_nb_dir_terr").value;
    //hago la llamada al geodecoder ///    
    geocoder.geocode({ 'address': address }, function (results, status) {

        //si el estado de la llamado es OK
        if (status == google.maps.GeocoderStatus.OK) {
            //centro el mapa en las coordenadas obtenidas
            map.setCenter(results[0].geometry.location);

            pos = 17;
            map.setZoom(pos);

            //coloco el marcador en dichas coordenadas
            
            document.getElementById("tsg001_terreno_nb_posicion").value = results[0].geometry.location;
            var ubicacion = document.getElementById("tsg001_terreno_nb_posicion").value;
            ubicacion = ubicacion.replace("(","");
            ubicacion = ubicacion.replace(")","");
            document.getElementById("tsg001_terreno_nb_posicion").value = ubicacion;

            marker.setPosition(results[0].geometry.location);

        } else {
            //si no es OK devuelvo error
            alert("No podemos encontrar la direcci&oacute;n, error: " + status);
        }
    });
   
}


$.fn.select2.defaults.set('language', 'es');