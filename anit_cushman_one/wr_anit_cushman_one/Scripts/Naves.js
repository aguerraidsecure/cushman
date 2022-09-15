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

    $("#frm-naves")
                .find('select')
                .each(function () {
                    var elemento = this;
                    elemento.disabled = queHago;

                });
};
function set_Movimiento(queMovimiento) {
    //Contacto General
    $("#tsg002_nave_industrial_cd_estado").empty();
    $("#tsg002_nave_industrial_cd_municipio").empty();
    $("#tsg002_nave_industrial_cd_colonia").empty();
    //Propietario
    $("#tsg025_ni_contacto_P_cd_estado").empty();
    $("#tsg025_ni_contacto_P_cd_municipio").empty();
    $("#tsg025_ni_contacto_P_cd_colonia").empty();
    //Administrador
    $("#tsg025_ni_contacto_A_cd_estado").empty();
    $("#tsg025_ni_contacto_A_cd_municipio").empty();
    $("#tsg025_ni_contacto_A_cd_colonia").empty();
    //Contacto
    $("#tsg025_ni_contacto_C_cd_estado").empty();
    $("#tsg025_ni_contacto_C_cd_municipio").empty();
    $("#tsg025_ni_contacto_C_cd_colonia").empty();

    $("#tsg002_nave_industrial_cd_mercado").empty();
    $("#tsg002_nave_industrial_cd_corredor").empty();
    //$("#tsg001_terreno_nu_tamano").empty();
    $("#tsg009_ni_dt_gral_cd_espesor").empty();

    $("#tsg020_ni_servicio_cd_telefonia").empty();

    //Keys
    $("#tsg001_terreno_cd_estado_h").val(0);
    $("#tsg001_terreno_cd_municipio_h").val(0);
    $("#tsg001_terreno_cd_colonia_h").val(0);


    $("#tsg002_nave_industrial_cd_estado_h").empty();
    $("#tsg002_nave_industrial_cd_municipio_h").empty();
    $("#tsg002_nave_industrial_cd_colonia_h").empty();
    $("#tsg025_ni_contacto_P_cd_estado_h").empty();
    $("#tsg025_ni_contacto_P_cd_municipio_h").empty();
    $("#tsg025_ni_contacto_P_cd_colonia_h").empty();
    $("#tsg025_ni_contacto_A_cd_estado_h").empty();
    $("#tsg025_ni_contacto_A_cd_municipio_h").empty();
    $("#tsg025_ni_contacto_A_cd_colonia_h").empty();
    $("#tsg025_ni_contacto_C_cd_estado_h").empty();
    $("#tsg025_ni_contacto_C_cd_municipio_h").empty();
    $("#tsg025_ni_contacto_C_cd_colonia_h").empty();
    $("#tsg002_nave_industrial_cd_mercado_h").empty();
    $("#tsg002_nave_industrial_cd_corredor_h").empty();
    $("#tsg009_ni_dt_gral_cd_espesor_h").empty();

    $("#tsg020_ni_servicio_cd_telefonia_h").empty();
    
    storeCargaCombo("/Utils/GetCdEstado", "tsg002_nave_industrial_cd_estado");
    storeCargaCombo("/Utils/GetCdEstado", "tsg025_ni_contacto_P_cd_estado");
    storeCargaCombo("/Utils/GetCdEstado", "tsg025_ni_contacto_A_cd_estado");
    storeCargaCombo("/Utils/GetCdEstado", "tsg025_ni_contacto_C_cd_estado");
    storeCargaCombo("/Utils/GetMercados", "tsg002_nave_industrial_cd_mercado");    
    //storeCargaCombo("/Utils/GetCdEstado", "tsg002_nave_industrial_cd_mercado");
    storeCargaCombo("/Utils/GetTelefonia", "tsg020_ni_servicio_cd_telefonia");

    $("#tsg002_nave_industrial_st_parque_ind option[value=1]").attr("selected", true);
    $("#tsg020_ni_servicio_nb_tp_gas_natural option[value=1]").attr("selected", true);
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

/////////////////////////////////////////////////////
//27052017 CAmbio AGGH para pasar parametros para
// la búsqueda de disponibilidad
function storeCargaTerrenos(Url) {
    
    var disp_desde = $("#tsg009_ni_dt_gral_nu_disponibilidad").val();
    var disp_hasta = $("#disponible_hasta").val();
    if ($("#disponible_hasta").val().length > 0) {
        data = { disp_desde_p: disp_desde, disp_hasta_p: disp_hasta};      
        $.ajax({
            type: 'POST',
            url: '/Naves/GetNavesDisponible',
            datatype: "json",
            data: data,
            success: function (StoreData) {
                console.log(StroeData);
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
        console.log(Url);
        data = JSON.stringify($('#frm-naves').serializeObject());
        console.log({data});

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
                console.log(StoreData);
                if (StoreData.length === 0) {
                    alert("No hay datos");
                } else {
                    loadData(StoreData);
                }
            },
            //Mensaje de error en caso de fallo
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textSataus);
                $.msgBox({
                    title: "Naves",
                    content: "Error en el servidor: " + errorThrown,
                    type: "alert"
                });
            }
        });
    }
};

function loadData(dataStoreData) {

    console.log(dataStoreData);

    var tab = $('<table class="table"></table>');
    var thead = $('<thead></thead>');
    var trowh = $('<tr class="bg-primary"></tr>');
    trowh.append('<th>Id</th>');
    trowh.append('<th>Nombre Parque</th>');
    trowh.append('<th>Naves</th>');
    trowh.append('<th></th>');
    thead.append(trowh);
    tab.append(thead);
    var tbody = $('<tbody></tbody>');
    $.each(dataStoreData,
        function (i, val) {

            var row = `<tr>
                            <td>E${ val[0].cd_nave }</td>
                            <td>E${ val[0].nb_parque }</td>
                            <td>E${ val[0].nb_nave }</td>
                             <td><button class="btn btn-primary">Editar</button></td>
                       </tr>`;
            var trow = $('<tr></tr>');
            trow.append('<td>E' + val[0].cd_nave + '</td>');
            trow.append('<td>' + val[0].nb_parque + '</td>');
            trow.append('<td>' + val[0].nb_nave + '</td>');
            var th_link = $('<td><t/d>');
            th_link
                .html('<a class="btn btn-primary" role="button"  href="/Naves/Create/' + val[0].cd_nave + '"> editar</a>');
            trow.append(th_link);

            tbody.append(trow);
        });

    tab.append(tbody);
    /*setCP("/Naves/BuscaCP", 1);
    setCP("/Naves/BuscaCP", 2);
    setCP("/Naves/BuscaCP", 3);
    setCP("/Naves/BuscaCP", 4);*/
    $("#tbNaves").html(tab);
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
            //th_link2
            //   .html('<a class="glyphicon glyphicon-download" href="/image/N' + val.cd_nave + '/' + val.nb_archivo + '" target="_blank"> </a>');
            th_link2
               .html('<a class="glyphicon glyphicon-download" onclick="descargararchivo(\'' + val.nb_archivo + '\')"> </a>');
            trow.append(th_link2);

            tbody.append(trow);
        });

    tab.append(tbody);

    $("#Archivos").html(tab);
};


function eliminaarchivo(cd_id, strarchivo) {
    data = { id: +$("#tsg002_nave_industrial_cd_nave").val(), claves: cd_id, 'archivo': strarchivo };
    $.ajax({
        type: 'POST',
        url: '/Naves/EliminarArchivo',
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

function ActualizaArchivo(cd_terreno, cd_reporte) {
    data = { id: cd_terreno, id_reporte: cd_reporte };
    $.ajax({
        type: 'POST',
        url: '/Naves/actualizaArchivo',
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
    data = { id: +$("#tsg002_nave_industrial_cd_nave").val(), 'archivo': strarchivo };
    $.ajax({
        type: 'POST',
        url: '/Naves/downloadFile',
        datatype: "text",
        data: data,
        success: function (StoreData) {
            document.location = StoreData.Name;
        }
    })
};


function descargaarch() {
    data = { id: +$("#tsg002_nave_industrial_cd_nave").val() };
    $.ajax({
        type: 'POST',
        url: '/Naves/getFile',
        datatype: "text",
        data: data,
        success: function (StoreData) {
            loadArchivos(StoreData);
        }
    })
}


function obt_img() {
    var x = document.getElementById("tsg002_nave_industrial_cd_nave");
    if (x.value > 0) {
        document.getElementById('oculto_img').style.display = 'block';
        descargaarch();
    } else {
        document.getElementById('oculto_img').style.display = 'none';
    }
}



function obt_dir() {
    var x = "";
    if (document.getElementById("tsg002_nave_industrial_nu_cp").value != "") {
        x = document.getElementById("tsg002_nave_industrial_nu_cp").value;
    }
    if (document.getElementById("tsg002_nave_industrial_cd_colonia").value != "") {
        x = x + ", " + document.getElementById("tsg002_nave_industrial_cd_colonia").options[document.getElementById('tsg002_nave_industrial_cd_colonia').selectedIndex].text;
    }
    if (document.getElementById("tsg002_nave_industrial_nb_calle").value != "") {
        x = x + ", " + document.getElementById("tsg002_nave_industrial_nb_calle").value;
    }
    if (document.getElementById("tsg002_nave_industrial_nu_direcion").value != "") {
        x = x + ", " + document.getElementById("tsg002_nave_industrial_nu_direcion").value;
    }
    if (document.getElementById("tsg002_nave_industrial_cd_municipio").value != "") {
        x = x + ", " + document.getElementById("tsg002_nave_industrial_cd_municipio").options[document.getElementById('tsg002_nave_industrial_cd_municipio').selectedIndex].text;
    }
    if (document.getElementById("tsg002_nave_industrial_cd_estado").value != "") {
        x = x + ", " + document.getElementById("tsg002_nave_industrial_cd_estado").options[document.getElementById('tsg002_nave_industrial_cd_estado').selectedIndex].text;
    }
    var x1 = document.getElementById("tsg002_nave_industrial_nb_dir_nave");
    x1.value = x;
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

function soloNumeros(e) {
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
        case 5:
            document.getElementById('oculto4').style.display = 'block';
            break;
        case 6:
            document.getElementById('oculto4').style.display = 'none';
            break;
        case 7:
            document.getElementById('oculto5').style.display = 'block';
            break;
        case 8:
            document.getElementById('oculto5').style.display = 'none';
            break;
        case 9:
            document.getElementById('oculto6').style.display = 'block';
            break;
        case 10:
            document.getElementById('oculto6').style.display = 'none';
            break;
        case 11:
            document.getElementById('oculto7').style.display = 'block';
            break;
        case 12:
            document.getElementById('oculto7').style.display = 'none';
            break;
        case 13:
            document.getElementById('oculto8').style.display = 'block';
            break;
        case 14:
            document.getElementById('oculto8').style.display = 'none';
            break;
        case 15:
            document.getElementById('oculto9').style.display = 'block';
            break;
        case 16:
            document.getElementById('oculto9').style.display = 'none';
            break;
        case 17:
            document.getElementById('oculto10').style.display = 'block';
            break;
        case 18:
            document.getElementById('oculto10').style.display = 'none';
            break;
        case 19:
            document.getElementById('oculto11').style.display = 'block';
            break;
        case 20:
            document.getElementById('oculto11').style.display = 'none';
            break;

    }
};


function actu_imp() {
    convertir(1);
    convertir(2);
    convertir(3);
    convertir(4);
    convertir(10);
}


function convertir(id) {
    switch (id) {
        case 1:
            //document.getElementById("nu_disponiblidad_pies") = document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() * 0.3048
            var x = document.getElementById("tsg009_ni_dt_gral_nu_superficie");
            var x1 = document.getElementById("nu_superficie_pies");
            //x1.value = Math.round((x.value / 0.3048) * 100) / 100;
            x1.value = Math.round((x.value *  10.7639)*100)/100;
            break;
        case 2:
            //document.getElementById("nu_tam_min") = document.getElementById("tsg026_te_dt_gral_nu_tam_min").value() * 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_bodega");
            var x1 = document.getElementById("nu_superficie_tot_pies");
            //x1.value = Math.round((x.value / 0.3048) * 100) / 100;
            x1.value = Math.round((x.value *  10.7639) * 100) / 100;
            break;
        case 3:
            //document.getElementById("nu_tam_max") = document.getElementById("tsg026_te_dt_gral_nu_tam_max").value() * 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_disponibilidad");
            var x1 = document.getElementById("nu_disponibilidad_pies");
            //x1.value = Math.round((x.value / 0.3048) * 100) / 100;
            x1.value = Math.round((x.value *  10.7639) * 100) / 100;
            break;
        case 4:
            //document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() = parseFloat(document.getElementById("nu_disponiblidad_pies").value) / 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_min_divisible");
            var x1 = document.getElementById("nu_minimo_pies");
            x1.value = Math.round((x.value *  10.7639) * 100) / 100;
            break;
        case 5:
            //document.getElementById("nu_disponiblidad_pies") = document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() * 0.3048
            var x = document.getElementById("tsg009_ni_dt_gral_nu_superficie");
            var x1 = document.getElementById("nu_superficie_pies");
            //x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            x.value = Math.round((x1.value /  10.7639) * 100) / 100;
            break;
        case 6:
            //document.getElementById("nu_tam_min") = document.getElementById("tsg026_te_dt_gral_nu_tam_min").value() * 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_bodega");
            var x1 = document.getElementById("nu_superficie_tot_pies");
            //x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            x.value = Math.round((x1.value /  10.7639) * 100) / 100;
            break;
        case 7:
            //document.getElementById("nu_tam_max") = document.getElementById("tsg026_te_dt_gral_nu_tam_max").value() * 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_disponibilidad");
            var x1 = document.getElementById("nu_disponibilidad_pies");
            //x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            x.value = Math.round((x1.value / 10.7639) * 100) / 100;
            break;
        case 8:
            //document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() = parseFloat(document.getElementById("nu_disponiblidad_pies").value) / 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_min_divisible");
            var x1 = document.getElementById("nu_minimo_pies");
            //x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            x.value = Math.round((x1.value /  10.7639) * 100) / 100;
            break;
        case 9:
            //document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() = parseFloat(document.getElementById("nu_disponiblidad_pies").value) / 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_altura");
            var x1 = document.getElementById("nu_altura");
            x.value = Math.round((x1.value * 0.3048) * 100) / 100;
            //x.value = Math.round((x1.value /  3.28084) * 100) / 100;
            break;
        case 10:
            //document.getElementById("tsg026_te_dt_gral_nu_disponiblidad").value() = parseFloat(document.getElementById("nu_disponiblidad_pies").value) / 0.3048;
            var x = document.getElementById("tsg009_ni_dt_gral_nu_altura");
            var x1 = document.getElementById("nu_altura");
            x1.value = Math.round((x.value / 0.3048) * 100) / 100;
            //x1.value = Math.round((x.value * 10.7639) * 100) / 100;
            break;
    }
};


function borrar() {
    console.log("Entro a borrar mapa");
    var x = document.getElementById("tsg002_nave_industrial_nb_posicion");
    x.value = "";
    var x1 = document.getElementById("tsg002_nave_industrial_nb_poligono");
    x1.value = "";
    initMap();
    codeAddress();

    //if (!document.getElementById("tsg002_nave_industrial_nb_dir_nave").disabled) {
    //    if (document.getElementById("tsg002_nave_industrial_nb_dir_nave").value != "") {
    //        codeAddress();
    //    }
    //}
};



var lat = null;
var lng = null;
var map = null;
var geocoder = null;
var marker = null;
var pos = 5;

$(document)
    .ready(function () {
              
        if ($("#IdAction").val() === "Editar") {
            $("#btnNuevo").attr("disabled", true);
        } else {
            $("#btnNuevo").attr("disabled", false);
        }

        $('[data-toggle="tooltip"]').tooltip();

        /// Naves
        storeCargaCombo("/Utils/GetCdEstado", "tsg002_nave_industrial_cd_estado");
        storeCargaCombo("/Utils/GetCdEstado", "tsg025_ni_contacto_P_cd_estado");
        storeCargaCombo("/Utils/GetCdEstado", "tsg025_ni_contacto_A_cd_estado");
        storeCargaCombo("/Utils/GetCdEstado", "tsg025_ni_contacto_C_cd_estado");
        
        /*$("#tsg002_nave_industrial_cd_corredor").select2({ placeholder: "Seleccione Corredor", width: "100%" });*/

        storeCargaCombo("/Utils/GetEstatus", "tsg002_nave_industrial_st_parque_ind");
        //$("#tsg002_nave_industrial_st_parque_ind").select2({ placeholder: "Seleccione Estatus", width: "30%" });

        storeCargaCombo("/Utils/GetAreas", "tsg009_ni_dt_gral_cd_area");
        /*$("#tsg009_ni_dt_gral_cd_area").select2({ placeholder: "Seleccione Area", width: "30%" });*/

        storeCargaCombo("/Utils/GetCargas", "tsg009_ni_dt_gral_cd_carga");
        /*$("#tsg009_ni_dt_gral_cd_carga").select2({ placeholder: "Seleccione Carga", width: "30%" });*/

        storeCargaCombo("/Utils/GetSistInc", "tsg009_ni_dt_gral_cd_sist_inc");
       /* $("#tsg009_ni_dt_gral_cd_sist_inc").select2({ placeholder: "Seleccione Sistema Incendio", width: "80%" });*/

        storeCargaCombo("/Utils/GetConstruccion", "tsg009_ni_dt_gral_cd_tp_construccion");
        /*$("#tsg009_ni_dt_gral_cd_tp_construccion").select2({ placeholder: "Seleccione Construcciòn", width: "80%" });*/

        storeCargaCombo("/Utils/GetLamparas", "tsg009_ni_dt_gral_cd_tp_lampara");
        /*$("#tsg009_ni_dt_gral_cd_tp_lampara").select2({ placeholder: "Seleccione Lampara", width: "80%" });*/

        storeCargaCombo("/Utils/GetHVAC", "tsg009_ni_dt_gral_cd_hvac");
       /* $("#tsg009_ni_dt_gral_cd_hvac").select2({ placeholder: "Seleccione HVAC", width: "80%" });*/

        storeCargaCombo("/Utils/GetEspesor", "tsg009_ni_dt_gral_cd_espesor");
        /*$("#tsg009_ni_dt_gral_cd_espesor").select2({ placeholder: "Seleccione Espesor", width: "30%" });*/

        storeCargaCombo("/Utils/GetIluminacion", "tsg009_ni_dt_gral_cd_ilum_nat");
        /*$("#tsg009_ni_dt_gral_cd_ilum_nat").select2({ placeholder: "Seleccione Iluminaciòn", width: "80%" });*/

        storeCargaCombo("/Utils/GetCajones", "tsg009_ni_dt_gral_cd_cajon_est");
        /*$("#tsg009_ni_dt_gral_cd_cajon_est").select2({ placeholder: "Seleccione Cajón", width: "80%" });*/

        storeCargaCombo("/Utils/GetGas", "tsg020_ni_servicio_cd_tp_gas_natural");
        /*$("#tsg020_ni_servicio_cd_tp_gas_natural").select2({ placeholder: "Seleccione Gas", width: "30%" });*/

        storeCargaCombo("/Utils/GetspFerr", "tsg020_ni_servicio_cd_esp_ferr");
        /*$("#tsg020_ni_servicio_cd_esp_ferr").select2({ placeholder: "Seleccione Esp Ferr", width: "30%" });*/

        storeCargaCombo("/Utils/GetCondArrend", "tsg023_ni_precio_cd_cond_arr");
        //$("#tsg023_ni_precio_cd_cond_arr").select2({ placeholder: "Seleccione Arrendamiento", width: "30%" });


        storeCargaCombo("/Utils/GettpTech", "tsg009_ni_dt_gral_cd_tp_tech");
        /*$("#tsg009_ni_dt_gral_cd_tp_tech").select2({ placeholder: "Seleccione Tipo Techumbre", width: "30%" });*/

        storeCargaCombo("/Utils/GetNivelPiso", "tsg009_ni_dt_gral_cd_Nivel_Piso");
        /*$("#tsg009_ni_dt_gral_cd_Nivel_Piso").select2({ placeholder: "Seleccione Nivel Piso", width: "30%" });*/

        storeCargaCombo("/Utils/GetTelefonia", "tsg020_ni_servicio_cd_telefonia");
        /*$("#tsg020_ni_servicio_cd_telefonia").select2({ placeholder: "Seleccione Telefonía", width: "30%" });*/

        storeCargaCombo("/Utils/GetMercados", "tsg002_nave_industrial_cd_mercado");
        //storeCargaCombo("/Utils/GetCdEstado", "tsg002_nave_industrial_cd_mercado");
        /*$("#tsg002_nave_industrial_cd_mercado").select2({ placeholder: "Seleccione Mercado", width: "100%" });*/

        storeCargaCombo("/Utils/GetTipoMoneda", "tsg023_ni_precio_cd_moneda");
        //$("#tsg023_ni_precio_cd_moneda").select2({ placeholder: "Seleccione Tipo Moneda", width: "30%" });
        storeCargaCombo("/Utils/GetTipoReporte", "tsg045_imagenes_naves_cd_reporte");
       /* $("#tsg045_imagenes_naves_cd_reporte").select2({ placeholder: "Seleccione Reporte", width: "30%" });*/
        storeCargaCombo("/Utils/GetTipoMoneda", "tsg023_ni_precio_cd_rep_moneda");
   /*     $("#tsg023_ni_precio_cd_rep_moneda").select2({ placeholder: "Seleccione Tipo Moneda", width: "30%" });*/


        $("#nu_inventario").focus();

        msgBoxImagePath = "/image/";



        $("#tsg009_ni_dt_gral_nu_superficie")
         .change(function () {
             convertir(1);
             return false;
         });

        $("#tsg009_ni_dt_gral_nu_bodega")
          .change(function () {
              convertir(2);
              return false;
          });

        $("#tsg009_ni_dt_gral_nu_disponibilidad")
          .change(function () {
              convertir(3);
              $("#tsg023_ni_precio_im_total").val( $("#tsg009_ni_dt_gral_nu_disponibilidad").val() * $("#tsg023_ni_precio_im_venta").val());
              return false;
          });
        $("#tsg023_ni_precio_im_venta")
          .change(function () {
             
              $("#tsg023_ni_precio_im_total").val($("#tsg009_ni_dt_gral_nu_disponibilidad").val() * $("#tsg023_ni_precio_im_venta").val());
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

        $("#tsg009_ni_dt_gral_nu_min_divisible")
          .change(function () {
              if (parseFloat($("#tsg009_ni_dt_gral_nu_min_divisible").val()) > parseFloat($("#tsg009_ni_dt_gral_nu_disponibilidad").val()) || parseFloat($("#tsg009_ni_dt_gral_nu_min_divisible").val()) > parseFloat($("#tsg009_ni_dt_gral_nu_superficie").val()))
              {
                  //alert('El minino divisible no debe de ser mayor a Disponibilidad Total o Superficie Terreno');
                  $.msgBox({
                      title: "Cushman ONE",
                      content: "El minino divisible no debe de ser mayor a Disponibilidad Total o Superficie Terreno",
                      type: "alert"
                  });
                  return false;
              }
              convertir(4);

              return false;
          });

        $("#tsg009_ni_dt_gral_nu_altura")
          .change(function () {
              convertir(10);
              return false;
          });


        $("#tsg002_nave_industrial_nu_cp")
         .change(function () {
             setCP("/Naves/BuscaCP",1);
         });

        $("#tsg025_ni_contacto_P_nu_cp")
         .change(function () {
             setCP("/Naves/BuscaCP", 2);
         });

        $("#tsg025_ni_contacto_A_nu_cp")
         .change(function () {
             setCP("/Naves/BuscaCP", 3);
         });

        $("#tsg025_ni_contacto_C_nu_cp")
         .change(function () {
             setCP("/Naves/BuscaCP", 4);
         });

        function cargaCP(storedata, estado, municipio, colonia)
        {

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
            if (tp == 1){
                var idRegistro = $("#tsg002_nave_industrial_nu_cp").val();
            }
            if (tp == 2) {
                var idRegistro = $("#tsg025_ni_contacto_P_nu_cp").val();
            }
            if (tp == 3) {
                var idRegistro = $("#tsg025_ni_contacto_A_nu_cp").val();
            }
            if (tp == 4) {
                var idRegistro = $("#tsg025_ni_contacto_C_nu_cp").val();
            }
            var data = { id: idRegistro };
            var datos = $.ajax({
                type: 'POST',
                url: Url,
                data: data,
                success: function (storedata) {
                    //alert("resultado de storedata: " + storedata[0].cd_colonia + " " + storedata[0].cd_municipio + " " + storedata[0].cd_estado);
                    if (tp == 1) {
                        cargaCP(storedata, "tsg002_nave_industrial_cd_estado", "tsg002_nave_industrial_cd_municipio_h", "tsg002_nave_industrial_cd_colonia_h");
                    }
                    if (tp == 2) {
                        cargaCP(storedata, "tsg025_ni_contacto_P_cd_estado", "tsg025_ni_contacto_P_cd_municipio_h", "tsg025_ni_contacto_P_cd_colonia_h");
                    }
                    if (tp == 3) {
                        cargaCP(storedata, "tsg025_ni_contacto_A_cd_estado", "tsg025_ni_contacto_A_cd_municipio_h", "tsg025_ni_contacto_A_cd_colonia_h");
                    }
                    if (tp == 4) {
                        cargaCP(storedata, "tsg025_ni_contacto_C_cd_estado", "tsg025_ni_contacto_C_cd_municipio_h", "tsg025_ni_contacto_C_cd_colonia_h");
                    }
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
                $("#console-log").addClass("alert alert-danger fade in");
                //document.getElementById("console-log").innerHTML = objXMLHttpRequest.responseText;        
                document.getElementById("console-log").innerHTML = "Tipo de Error: " + jqXHR.statusText + "Código de Error:" + jqXHR.readyState;
                $("#console-log").fadeIn(5000);
                $("#console-log").fadeOut(5000);
            });
        };

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
                            setGeneraPdf("/Naves/GeneraPdf",1);
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
                             setGeneraPdf("/Naves/GeneraPpt", 1);
                         }
                     }
                 });
             });

        $("#btnPdfSum")
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
                            setGeneraPdf("/Naves/GeneraPdf", 2);
                        }
                    }
                });
            });
        function setGeneraPdfSum(Url) {
            //var idRegistro = $("#cd_nave").val();
            //var lngRegistro = $("#cd_terreno").val();
            //var lngRegistro = tp;            
            var datos = $.ajax({
                type: 'GET',
                url: Url,
                //data: data,
                success: function (StoreData) {
                    if (StoreData.Name == "") {
                        alert("No se encontraron datos, favor de validar");
                    } else {
                        document.location = StoreData.Name;
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

        function setGeneraPdf(Url, tp) {
            var idRegistro = $("#tsg002_nave_industrial_cd_nave").val();
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
                        document.location = StoreData.Name;
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

        $('#frm-naves').on('keyup keypress', function (e) {
            var keyCode = e.keyCode || e.which;
            if (keyCode === 13) {
                e.preventDefault();
                return false;
            }
        });

        $("#tsg009_ni_dt_gral_nu_superficie").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_superficie_pies").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_bodega").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_superficie_tot_pies").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_disponibilidad").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_disponibilidad_pies").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_min_divisible").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_minimo_pies").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_anio_cons").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_altura").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#nu_altura").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_puertas").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_rampas").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg009_ni_dt_gral_nu_caj_trailer").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#tsg020_ni_servicio_nu_kva").on('keyup keypress', function (e) {
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

        $("#tsg023_ni_precio_nu_ma_ac").on('keyup keypress', function (e) {
            return soloNumeros(e);
        });

        $("#frm-naves")
            .submit(function () {
                var form = $(this);
                var camposObligatorios = "";
                var bandValidacion = false;
                if ($('#tsg002_nave_industrial_nb_parque').val() == "")
                {
                    bandValidacion = true;
                    camposObligatorios = camposObligatorios +  "Nombre parque.\n"
                }
                if ($('#tsg002_nave_industrial_nb_nave').val() == "") {
                    bandValidacion = true;
                    camposObligatorios = camposObligatorios + "Nombre nave.\n"
                }
                if ($('#tsg002_nave_industrial_nb_calle').val() == "") {
                    bandValidacion = true;
                    camposObligatorios = camposObligatorios + "Calle.\n"
                }
                if ($('#tsg002_nave_industrial_nu_direcion').val() == "") {
                    bandValidacion = true;
                    camposObligatorios = camposObligatorios + "Número.\n"
                }
                if ($('#tsg002_nave_industrial_nu_cp').val() == "") {
                    bandValidacion = true;
                    camposObligatorios = camposObligatorios + "Código Postal.\n"
                }

                if (bandValidacion)
                {
                    $.msgBox({
                        title: "Cushman ONE",
                        content: "Los siguientes valores son requeridos: " + camposObligatorios,
                        type: "alert"
                    });
                    return false;
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
                                    content: "registro guardado con el Folio: E" + r.redirect + "\nDesea capturar otro registro?",
                                    type: "confirm",
                                    buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                                    success: function (result) {
                                        if (result == "No") {
                                            //window.location.href = r.redirect;
                                            //set_Controles(true);
                                        } else {                                            
                                            window.location.replace("../../Naves/Create");
                                            $("#btnNuevo").attr("disabled", false);
                                        }
                                    }
                                });
                            } else {
                                // alert("falló");
                                $.msgBox({
                                    title: "Cushman ONE",
                                    content: r.error,
                                    type: "alert"
                                });
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            // alert("error");
                            $.msgBox({
                                title: "Cushman ONE",
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
        
        $("#btnBuscarTodo")
            .on('click',
                function (evt) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    $("#btnNuevo").attr("disabled", true);

                    var $detailDiv = $('#findNaves'),
                        url = '/Naves/Buscar';
                    $.get(url, function (data) {
                        $detailDiv.html(data);
                    });
                    $("#myModal").modal({
                        backdrop: false,
                        keyboard: true
                    });
                    storeCargaTerrenos("/Naves/GetNavesAll");
                });
        $("#btnBuscar")
            .on('click',
                function (evt) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    $("#btnNuevo").attr("disabled", true);
                   
                    var $detailDiv = $('#findNaves'),
                        url = '/Naves/Buscar';
                    $.get(url, function (data) {
                        $detailDiv.html(data);
                    });
                    $("#myModal").modal({
                        backdrop: false,
                        keyboard: true
                    });
                    storeCargaTerrenos("/Naves/GetNaves");
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
                            setBorraRegistro("/Naves/EliminaNaves");
                        }
                    }
                });

            });

        function setBorraRegistro(Url) {
            var idRegistro = $("#tsg002_nave_industrial_cd_nave").val();
            var data = { id: idRegistro };
            var datos = $.ajax({
                type: 'GET',
                url: Url,
                data: data,
                success: function () {
                    window.location.replace("../../Naves/Create");
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


        $("#btnNuevo").click(function () {
            $("#frm-naves").find(':input,select').each(function () {
                var elemento = this;
                //alert(elemento.id);
                if ((elemento.type != 'button') && (elemento.type != 'radio') && (elemento.type != 'submit') && (elemento.id != 'tsg009_ni_dt_gral_fh_desde') && (elemento.id != 'tsg009_ni_dt_gral_fh_hasta')) {
                    //alert(elemento.id);
                    //elemento.disabled = (elemento.disabled ? false : true);
                }
            });
        })

        //OANR 05/08/2015
        $("#btnEditar").click(function () {
            //$("#frm-naves").find(':input,select').each(function () {
            //    var elemento = this;
            //    //alert(elemento.id);
            //    if ((elemento.type != 'button') && (elemento.type != 'radio') && (elemento.type != 'submit')) {
            //        //alert(elemento.id);
            //        elemento.disabled = (elemento.disabled ? false : true);
            //    }
            //});
            set_Controles(false);
            $("input#tsg002_nave_industrial_nb_parque").focus();
        })

        $("#btnBuscarFolio").click(function () {
            var id_nave = $("#folio").val().replace("E", "");
            window.location.replace("../../Naves/Create/" + id_nave);
        })

        $("#btnCopiar").click(function () {
            $.msgBox({
                title: "Cushman ONE",
                content: "¿Está seguro de copiar el registro?",
                type: "confirm",
                buttons: [{ value: "Si" }, { value: "No" }, { value: "Cancel" }],
                success: function (result) {
                    if (result === "Si") {
                        set_Controles(false);
                        $("#tsg002_nave_industrial_nb_nave").val("");
                        $("input#tsg002_nave_industrial_nb_nave").focus();
                        $("#tsg002_nave_industrial_cd_nave").val(0);
                        $.msgBox({
                            title: "Cushman ONE",
                            content: "El registro se encuentra listo para copiar solo necesita escribir el nombre de la nave y dar clic en guardar",
                            type: "alert"
                        });
                    }
                }
            });
            //set_Controles(false);
            //$("input#tsg002_nave_industrial_nb_comercial").focus();
            //$("#cd_nave").val(0);
        })

        $('#fileupload').fileupload({
            dataType: 'json',
            url: '/Naves/UploadFiles/' + $("#tsg002_nave_industrial_cd_nave").val(),
            autoUpload: true,
            success: function (data) {
                ActualizaArchivo(data[0].cd_id, $("#tsg045_imagenes_naves_cd_reporte").val())
                //loadArchivos(data);
                //$('.file_name').html(data.result.name);
                //$('.file_type').html(data.result.type);
                //$('.file_size').html(data.result.size);
            }
        })

        $("#tsg009_ni_dt_gral_cd_area")
                  .change(function () {
                      //Se limpia el contenido del dropdownlist
                      $("#tsg009_ni_dt_gral_cd_area_h").val($("#tsg009_ni_dt_gral_cd_area").val());

                      if (document.getElementById("tsg009_ni_dt_gral_cd_area").options[document.getElementById('tsg009_ni_dt_gral_cd_area').selectedIndex].text == "Otro / Other") {
                          // Funcion para Mostar Div Oculto                                                                                                                               
                          mostrar2(1);
                      }
                      else {
                          // Funcion para Mostar Div Oculto
                          mostrar2(2);
                      }
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

        $("#tsg020_ni_servicio_cd_tp_gas_natural")
           .change(function () {
               $("#tsg020_ni_servicio_cd_tp_gas_natural_h").val($("#tsg020_ni_servicio_cd_tp_gas_natural").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg020_ni_servicio_cd_tp_gas_natural").options[document.getElementById('tsg020_ni_servicio_cd_tp_gas_natural').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(3);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(4);
               }
               return false;
           });

        $("#tsg023_ni_precio_cd_cond_arr")
           .change(function () {
               $("#tsg023_ni_precio_cd_cond_arr_h").val($("#tsg023_ni_precio_cd_cond_arr").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg023_ni_precio_cd_cond_arr").options[document.getElementById('tsg023_ni_precio_cd_cond_arr').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(5);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(6);
               }
               return false;
           });


        $("#tsg009_ni_dt_gral_cd_carga")
           .change(function () {
               $("#tsg009_ni_dt_gral_cd_carga_h").val($("#tsg009_ni_dt_gral_cd_carga").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg009_ni_dt_gral_cd_carga").options[document.getElementById('tsg009_ni_dt_gral_cd_carga').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(7);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(8);
               }
               return false;
           });

        $("#tsg009_ni_dt_gral_cd_sist_inc")
           .change(function () {
               $("#tsg009_ni_dt_gral_cd_sist_inc_h").val($("#tsg009_ni_dt_gral_cd_sist_inc").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg009_ni_dt_gral_cd_sist_inc").options[document.getElementById('tsg009_ni_dt_gral_cd_sist_inc').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(9);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(10);
               }
               return false;
           });

        $("#tsg009_ni_dt_gral_cd_tp_construccion")
           .change(function () {
               $("#tsg009_ni_dt_gral_cd_tp_construccion_h").val($("#tsg009_ni_dt_gral_cd_tp_construccion").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg009_ni_dt_gral_cd_tp_construccion").options[document.getElementById('tsg009_ni_dt_gral_cd_tp_construccion').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(11);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(12);
               }
               return false;
           });


        $("#tsg009_ni_dt_gral_cd_tp_lampara")
           .change(function () {
               $("#tsg009_ni_dt_gral_cd_tp_lampara_h").val($("#tsg009_ni_dt_gral_cd_tp_lampara").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg009_ni_dt_gral_cd_tp_lampara").options[document.getElementById('tsg009_ni_dt_gral_cd_tp_lampara').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(13);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(14);
               }
               return false;
           });

        $("#tsg009_ni_dt_gral_cd_ilum_nat")
           .change(function () {
               $("#tsg009_ni_dt_gral_cd_ilum_nat_h").val($("#tsg009_ni_dt_gral_cd_ilum_nat").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg009_ni_dt_gral_cd_ilum_nat").options[document.getElementById('tsg009_ni_dt_gral_cd_ilum_nat').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(15);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(16);
               }
               return false;
           });

        $("#tsg009_ni_dt_gral_cd_hvac")
           .change(function () {
               $("#tsg009_ni_dt_gral_cd_hvac_h").val($("#tsg009_ni_dt_gral_cd_hvac").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg009_ni_dt_gral_cd_hvac").options[document.getElementById('tsg009_ni_dt_gral_cd_hvac').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(17);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(18);
               }
               return false;
           });

        $("#tsg009_ni_dt_gral_cd_cajon_est")
           .change(function () {
               $("#tsg009_ni_dt_gral_cd_cajon_est_h").val($("#tsg009_ni_dt_gral_cd_cajon_est").val());
               //Se limpia el contenido del dropdownlist
               if (document.getElementById("tsg009_ni_dt_gral_cd_cajon_est").options[document.getElementById('tsg009_ni_dt_gral_cd_cajon_est').selectedIndex].text == "Otro / Other") {
                   // Funcion para Mostar Div Oculto
                   mostrar2(19);
               }
               else {
                   // Funcion para Mostar Div Oculto
                   mostrar2(20);
               }
               return false;
           });

        $("#tsg020_ni_servicio_cd_telefonia")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg020_ni_servicio_cd_telefonia_h").val($("#tsg020_ni_servicio_cd_telefonia").val());
            return false;
        });

        $("#tsg002_nave_industrial_cd_mercado")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg002_nave_industrial_cd_mercado_h").val($("#tsg002_nave_industrial_cd_mercado").val());
            return false;
        });

        $("#tsg002_nave_industrial_cd_corredor")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg002_nave_industrial_cd_corredor_h").val($("#tsg002_nave_industrial_cd_corredor").val());
            return false;
        });

        $("#tsg009_ni_dt_gral_cd_espesor")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg009_ni_dt_gral_cd_espesor_h").val($("#tsg009_ni_dt_gral_cd_espesor").val());
            return false;
        });
        
        $("#tsg002_nave_industrial_st_parque_ind")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg002_nave_industrial_st_parque_ind_h").val($("#tsg002_nave_industrial_st_parque_ind").val());
            return false;
        });

        $("#tsg009_ni_dt_gral_cd_tp_tech")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg009_ni_dt_gral_cd_tp_tech_h").val($("#tsg009_ni_dt_gral_cd_tp_tech").val());
            return false;
        });

        $("#tsg009_ni_dt_gral_cd_Nivel_Piso")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg009_ni_dt_gral_cd_Nivel_Piso_h").val($("#tsg009_ni_dt_gral_cd_Nivel_Piso").val());
            return false;
        });

        $("#tsg020_ni_servicio_cd_esp_ferr")
        .change(function () {
            //Se limpia el contenido del dropdownlist
            $("#tsg020_ni_servicio_cd_esp_ferr_h").val($("#tsg020_ni_servicio_cd_esp_ferr").val());
            return false;
        });


        $("#tsg023_ni_precio_im_renta")
        .change(function () {            
            $("#tsg023_ni_precio_im_renta").val(String.Format("{0:0.00}", $("#tsg023_ni_precio_im_renta").val()));
            return false;
        });

        ///////-*************************parte para naves industriales
        $("#tsg002_nave_industrial_cd_mercado")
            .change(function () {
                //Se limpia el contenido del dropdownlist
                $("#tsg002_nave_industrial_cd_corredor").empty();
                //$("#tsg002_nave_industrial_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetCorredorByCdMercado",
                    "tsg002_nave_industrial_cd_corredor",
                    "tsg002_nave_industrial_cd_mercado",
                    "tsg002_nave_industrial_cd_corredor",
                    undefined,
                    "tsg002_nave_industrial_cd_mercado",
                    undefined,
                    undefined);
                return false;
            });




  $("#tsg002_nave_industrial_cd_estado")
    .change(function () {
        //Se limpia el contenido del dropdownlist
        $("#tsg002_nave_industrial_cd_municipio").empty();
        $("#tsg002_nave_industrial_cd_colonia").empty();
        //$("#cd_tipo_prod").empty();
        storeCargaCombo("/Utils/GetMunicipioByCdEstado",
            "tsg002_nave_industrial_cd_municipio",
            "tsg002_nave_industrial_cd_estado",
            "tsg002_nave_industrial_cd_municipio",
            undefined,
            "tsg002_nave_industrial_cd_estado",
            undefined,
            undefined);
        //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
        //Recargar el plugin para que tenga la funcionalidad del componente
        //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
        //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
        //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
        return false;
    });
        $("#tsg002_nave_industrial_cd_municipio")
            .change(function () {
                //Se limpia el contenido del dropdownlist
                $("#tsg002_nave_industrial_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg002_nave_industrial_cd_colonia",
                    "tsg002_nave_industrial_cd_estado",
                    "tsg002_nave_industrial_cd_municipio",
                    "tsg002_nave_industrial_cd_colonia",
                    "tsg002_nave_industrial_cd_estado",
                    "tsg002_nave_industrial_cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
               /* $("#tsg002_nave_industrial_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "100%" });*/
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });

        //Recargar el plugin para que tenga la funcionalidad del componente
        ////////////$("#tsg002_nave_industrial_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "100%" });
        ////////////$("#tsg002_nave_industrial_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "100%" });
        ////////////$("#tsg002_nave_industrial_cd_estado").select2({ placeholder: "Seleccione Estado", width: "100%" });

        $("#tsg002_nave_industrial_cd_colonia").change(function () {
            $("#tsg002_nave_industrial_cd_colonia_h").val($("#tsg002_nave_industrial_cd_colonia").val());
            var data = { id_estado: $("#tsg002_nave_industrial_cd_estado").val(), id_municipio: $("#tsg002_nave_industrial_cd_municipio").val(), id_colonia: $("#tsg002_nave_industrial_cd_colonia").val() };
            $.ajax({
                type: 'GET',
                url: "/Naves/ObtenerCP",
                data: data,
                success: function (storedata) {
                    $("#tsg002_nave_industrial_nu_cp").val(storedata[0].nu_cp);
                }
            })
        });


        $("#tsg025_ni_contacto_P_cd_estado")
    .change(function () {
        //Se limpia el contenido del dropdownlist
        $("#tsg025_ni_contacto_P_cd_municipio").empty();
        $("#tsg025_ni_contacto_P_cd_colonia").empty();
        //$("#cd_tipo_prod").empty();
        storeCargaCombo("/Utils/GetMunicipioByCdEstado",
            "tsg025_ni_contacto_P_cd_municipio",
            "tsg025_ni_contacto_P_cd_estado",
            "tsg025_ni_contacto_P_cd_municipio",
            undefined,
            "tsg025_ni_contacto_P_cd_estado",
            undefined,
            undefined);
        //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
        //Recargar el plugin para que tenga la funcionalidad del componente
        //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
        //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
        //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
        return false;
    });
        $("#tsg025_ni_contacto_P_cd_municipio")
            .change(function () {
                //Se limpia el contenido del dropdownlist
                $("#tsg025_ni_contacto_P_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg025_ni_contacto_P_cd_colonia",
                    "tsg025_ni_contacto_P_cd_estado",
                    "tsg025_ni_contacto_P_cd_municipio",
                    "tsg025_ni_contacto_P_cd_colonia",
                    "tsg025_ni_contacto_P_cd_estado",
                    "tsg025_ni_contacto_P_cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                //$("#tsg025_ni_contacto_P_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "100%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });

        //Recargar el plugin para que tenga la funcionalidad del componente
        //$("#tsg025_ni_contacto_P_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "100%" });
        //$("#tsg025_ni_contacto_P_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "100%" });
        //$("#tsg025_ni_contacto_P_cd_estado").select2({ placeholder: "Seleccione Estado", width: "100%" });


        $("#tsg025_ni_contacto_P_cd_colonia").change(function () {
            $("#tsg002_nave_industrial_P_cd_colonia_h").val($("#tsg002_nave_industrial_P_cd_colonia").val());
            var data = { id_estado: $("#tsg025_ni_contacto_P_cd_estado").val(), id_municipio: $("#tsg025_ni_contacto_P_cd_municipio").val(), id_colonia: $("#tsg025_ni_contacto_P_cd_colonia").val() };
            $.ajax({
                type: 'GET',
                url: "/Naves/ObtenerCP",
                data: data,
                success: function (storedata) {
                    $("#tsg025_ni_contacto_P_nu_cp").val(storedata[0].nu_cp);
                }
            })
        });

        $("#tsg025_ni_contacto_A_cd_estado")
            .change(function () {
                //Se limpia el contenido del dropdownlist
                $("#tsg025_ni_contacto_A_cd_municipio").empty();
                $("#tsg025_ni_contacto_A_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetMunicipioByCdEstado",
                    "tsg025_ni_contacto_A_cd_municipio",
                    "tsg025_ni_contacto_A_cd_estado",
                    "tsg025_ni_contacto_A_cd_municipio",
                    undefined,
                    "tsg025_ni_contacto_A_cd_estado",
                    undefined,
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
                //Recargar el plugin para que tenga la funcionalidad del componente
                //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
                //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
                //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
                return false;
            });
        $("#tsg025_ni_contacto_A_cd_municipio")
            .change(function () {
                //Se limpia el contenido del dropdownlist
                $("#tsg025_ni_contacto_A_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg025_ni_contacto_A_cd_colonia",
                    "tsg025_ni_contacto_A_cd_estado",
                    "tsg025_ni_contacto_A_cd_municipio",
                    "tsg025_ni_contacto_A_cd_colonia",
                    "tsg025_ni_contacto_A_cd_estado",
                    "tsg025_ni_contacto_A_cd_municipio",
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                //$("#tsg025_ni_contacto_A_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "100%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });

        //Recargar el plugin para que tenga la funcionalidad del componente
        //$("#tsg025_ni_contacto_A_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "100%" });
        //$("#tsg025_ni_contacto_A_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "100%" });
        //$("#tsg025_ni_contacto_A_cd_estado").select2({ placeholder: "Seleccione Estado", width: "100%" });

        $("#tsg025_ni_contacto_A_cd_colonia").change(function () {
            $("#tsg002_nave_industrial_A_cd_colonia_h").val($("#tsg002_nave_industrial_A_cd_colonia").val());
            var data = { id_estado: $("#tsg025_ni_contacto_A_cd_estado").val(), id_municipio: $("#tsg025_ni_contacto_A_cd_municipio").val(), id_colonia: $("#tsg025_ni_contacto_A_cd_colonia").val() };
            $.ajax({
                type: 'GET',
                url: "/Naves/ObtenerCP",
                data: data,
                success: function (storedata) {
                    $("#tsg025_ni_contacto_A_nu_cp").val(storedata[0].nu_cp);
                }
            })
        });

        $("#tsg025_ni_contacto_C_cd_estado")
            .change(function () {
                $("#tsg025_ni_contacto_C_cd_estado_h").val($("#tsg025_ni_contacto_C_cd_estado_cd_estado").val());
                //Se limpia el contenido del dropdownlist
                $("#tsg025_ni_contacto_C_cd_municipio").empty();
                $("#tsg025_ni_contacto_C_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetMunicipioByCdEstado",
                    "tsg025_ni_contacto_C_cd_municipio",
                    "tsg025_ni_contacto_C_cd_estado",
                    "tsg025_ni_contacto_C_cd_municipio",
                    undefined,
                    "tsg025_ni_contacto_C_cd_estado",
                    undefined,
                    undefined);
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion                
                //Recargar el plugin para que tenga la funcionalidad del componente
                //$("#tsg001_terreno_cd_municipio").select2({ placeholder: "Seleccione Municipo", width: "30%" });
                //$("#tsg001_terreno_cd_estado").select2({ placeholder: "Seleccione Estado", width: "30%" });
                //$("#cd_tipo_prod").select2({ placeholder: "Seleccione TpProducto", width: "30%" });
                return false;
            });
        $("#tsg025_ni_contacto_C_cd_municipio")
            .change(function () {
                //Se limpia el contenido del dropdownlist
                $("#tsg025_ni_contacto_C_cd_colonia").empty();
                //$("#cd_tipo_prod").empty();
                storeCargaCombo("/Utils/GetColoniaByMunicipio",
                    "tsg025_ni_contacto_C_cd_colonia",
                    "tsg025_ni_contacto_C_cd_estado",
                    "tsg025_ni_contacto_C_cd_municipio",
                    "tsg025_ni_contacto_C_cd_colonia",
                    "tsg025_ni_contacto_C_cd_estado",
                    "tsg025_ni_contacto_C_cd_municipio",
                    undefined);
                ////Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                //$("#tsg025_ni_contacto_C_cd_colonia").select2({ placeholder: "Seleccione Colonia ", width: "100%" });
                //$("#cd_").select2({ placeholder: "Seleccione SubFamilia", width: "30%" });
                return false;
            });

        //Recargar el plugin para que tenga la funcionalidad del componente
        //$("#tsg025_ni_contacto_C_cd_colonia").select2({ placeholder: "Seleccione Colonia", width: "100%" });
        //$("#tsg025_ni_contacto_C_cd_municipio").select2({ placeholder: "Seleccione Municipio", width: "100%" });
        //$("#tsg025_ni_contacto_C_cd_estado").select2({ placeholder: "Seleccione Estado", width: "100%" });

        $("#tsg025_ni_contacto_C_cd_colonia").change(function () {
            $("#tsg002_nave_industrial_C_cd_colonia_h").val($("#tsg002_nave_industrial_C_cd_colonia").val());
            var data = { id_estado: $("#tsg025_ni_contacto_C_cd_estado").val(), id_municipio: $("#tsg025_ni_contacto_C_cd_municipio").val(), id_colonia: $("#tsg025_ni_contacto_C_cd_colonia").val() };
            $.ajax({
                type: 'GET',
                url: "/Naves/ObtenerCP",
                data: data,
                success: function (storedata) {
                    $("#tsg025_ni_contacto_C_nu_cp").val(storedata[0].nu_cp);
                }
            })
        });

        //*********** Parte de Google map ***************

        jQuery('#pasar').click(function () {
            //if (!document.getElementById("tsg002_nave_industrial_nb_nave").disabled) {
            //    codeAddress();
            //}
            //return false;
            codeAddress();
        });
        //Inicializamos la función de google maps una vez el DOM este cargado
        initMap();


        return false;
    })

function initMap() {

    geocoder = new google.maps.Geocoder();
    pos = 5;
    var posicion = document.getElementById("tsg002_nave_industrial_nb_posicion").value;
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
    if (document.getElementById("tsg002_nave_industrial_nb_poligono").value != "") {
        var poligono = [];
        var posicion = document.getElementById("tsg002_nave_industrial_nb_poligono").value;
        var arregloDeCadenas1 = posicion.split("|");
        for (var i = 0; i < arregloDeCadenas1.length; i++) {
            var cad = arregloDeCadenas1[i];
            //cad = cad.split(",");
            var arregloDeCadenas2 = cad.split(",");        
            lng = parseFloat(arregloDeCadenas2[0]);
            lat = parseFloat(arregloDeCadenas2[1]);
            poligono[i] = new google.maps.LatLng(lng, lat);
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
    if (document.getElementById("tsg002_nave_industrial_nb_poligono").value != "") {
        miPoligono.setMap(map);
    }

    //creamos el marcador en el mapa
    marker = new google.maps.Marker({
        map: map,//el mapa creado en el paso anterior
        position: latLng,//objeto con latitud y longitud
        draggable: false //que el marcador se pueda arrastrar
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
            document.getElementById("tsg002_nave_industrial_nb_poligono").value += xy.lat() + "," + xy.lng() + "|";

        }
        document.getElementById("tsg002_nave_industrial_nb_poligono").value += vertices.getAt(0).lat() + "," + vertices.getAt(0).lng() + "|";
        //alert("Datos del poligono:" + document.getElementById("tsg002_nave_industrial_nb_poligono").value); dasfasd
    });

    map.addListener('click', function (e) {
        alert('posicion : ' + e.latLng);
        document.getElementById("tsg002_nave_industrial_nb_posicion").value = e.latLng;
        marker.setMap(null);
        
        marker = new google.maps.Marker({
            position: e.latLng
        });
        marker.setMap(map);
        
    });
    map.addListener('click', function (e) {
        //alert('posicion : ' + e.latLng);
        var ubicacion = String(e.latLng);

        ubicacion = ubicacion.replace("(", "");
        ubicacion = ubicacion.replace(")", "");

        //document.getElementById("tsg002_nave_industrial_nb_posicion").value = e.latLng;
        document.getElementById("tsg002_nave_industrial_nb_posicion").value = ubicacion;
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
function initMap2() {

    //$.ajax({
    //    type: 'GET',
    //    url: "/Naves/Index",
        
    //    success: function (storedata) {
    //        console.log(storeData);
    //    }
    //});

    $.get("/Naves/getNavesMapas", function (data) {
        
        
        console.log(data);


        try {

            
            pos = 6;

            var latLng = new google.maps.LatLng(24.090303, -102.415217);



            //Definimos algunas opciones del mapa a crear
            var myOptions = {
                center: latLng,//centro del mapa
                zoom: pos,//zoom del mapa
                mapTypeId: google.maps.MapTypeId.ROADMAP //tipo de mapa, carretera, híbrido,etc
            };

            //creamos el mapa con las opciones anteriores y le pasamos el elemento div
            map = new google.maps.Map(document.getElementById("googleMap"), myOptions);
            
            for (let i = 0; i < data.length; i++) {
               

                const coords = data[i].nb_posicion.split(',');

                

                const latLng = new google.maps.LatLng(coords[0], coords[1]);

                const titulo = `<h1>${data[i].nb_nave}</h1>`

                const contentString =
                    "<div class='alert alert-primary' role='alert'>" +
                    "<h1>"+data[i].nb_nave+"</h1>" +
                    "</div>" +
                    
                    '<a class="btn btn-primary" role="button"  href="/Naves/Create/' + data[i].cd_nave + '"> Entrar</a>'
                //const image = "../../image/pinCushman.png";
                const infoWindow = new google.maps.InfoWindow();


                const image = {
                    url: "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png",
                    // This marker is 20 pixels wide by 32 pixels high.
                    size: new google.maps.Size(20, 32),
                    // The origin for this image is (0, 0).
                    origin: new google.maps.Point(0, 0),
                    // The anchor for this image is the base of the flagpole at (0, 32).
                    anchor: new google.maps.Point(0, 32),
                };

                //const svgMarker = {
                //    path: "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png",
                //    fillColor: "blue",
                //    fillOpacity: 0.6,
                //    strokeWeight: 0,
                //    rotation: 0,
                //    scale: 2,
                //    anchor: new google.maps.Point(15, 30),
                //};


                marker = new google.maps.Marker({
                    position: latLng,
                    map: map,
                    title: titulo,                    
                    icon: image,
                    label: `${data[i].cd_nave}`,
                    optimized: false,

                });

                marker.addListener("click", () => {
                    infoWindow.close();
                    infoWindow.setContent(contentString);
                    //infoWindow.setPosition(latLng);
                    console.log(marker.getMap());
                    console.log(marker);
                    //infoWindow.open(marker.getMap(), marker);
                    infoWindow.open({
                        anchor: marker,
                        map,
                        shouldFocus: false,
                    })
                });

            }

        } catch (e) {
            var iError = e.number;
        }
    });

    



};

function navexId(id) {
    console.log("Entro a nave id" + id);

    //$.get("/Naves/Create/" + id, function (data) {
    //    console.log(data);
    //}
}
//funcion que traduce la direccion en coordenadas
function codeAddress() {
    //geocoder = new google.maps.Geocoder();

    //obtengo la direccion del formulario
    var address = document.getElementById("tsg002_nave_industrial_nb_dir_nave").value;
    //hago la llamada al geodecoder ///    
    geocoder.geocode({ 'address': address }, function (results, status) {

        //si el estado de la llamado es OK
        if (status == google.maps.GeocoderStatus.OK) {
            //centro el mapa en las coordenadas obtenidas
            map.setCenter(results[0].geometry.location);

            pos = 17;
            map.setZoom(pos);

            //coloco el marcador en dichas coordenadas

            document.getElementById("tsg002_nave_industrial_nb_posicion").value = results[0].geometry.location;
            var ubicacion = document.getElementById("tsg002_nave_industrial_nb_posicion").value;
            ubicacion = ubicacion.replace("(", "");
            ubicacion = ubicacion.replace(")", "");
            document.getElementById("tsg002_nave_industrial_nb_posicion").value = ubicacion;

            marker.setPosition(results[0].geometry.location);

        } else {
            //si no es OK devuelvo error
            alert("No podemos encontrar la direcci&oacute;n, error: " + status);
        }

    });
       
    


}

window.initMap2 = initMap2;
$.fn.select2.defaults.set('language', 'es');