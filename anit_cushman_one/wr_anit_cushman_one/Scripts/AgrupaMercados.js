/*
$(document).ajaxStart(function () {
    $(".log").text("Triggered ajaxStart handler.");
});
$(document).ajaxComplete(function (event, request, settings) {
    $("#msg").append("<li>Request Complete.</li>");
});
$(document).ajaxStop(function () {
    alert("All AJAX requests completed");
});
$.ajaxSetup({
    url: "/xmlhttp/",
    global: false,
    type: "POST"
});
$.ajax({ data: myData });
*/


//import { myLanguage } from "../Scripts/config.js"
var languageSPANISH = {
    "processing": "Procesando...",
    "lengthMenu": "Mostrar _MENU_ registros",
    "zeroRecords": "No se encontraron resultados",
    "emptyTable": "Ningún dato disponible en esta tabla",
    "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "infoFiltered": "(filtrado de un total de _MAX_ registros)",
    "search": "Buscar:",
    "infoThousands": ",",
    "loadingRecords": "Cargando...",
    "paginate": {
        "first": "Primero",
        "last": "Último",
        "next": "Siguiente",
        "previous": "Anterior"
    },
    "aria": {
        "sortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sortDescending": ": Activar para ordenar la columna de manera descendente"
    },
    "buttons": {
        "copy": "Copiar",
        "colvis": "Visibilidad",
        "collection": "Colección",
        "colvisRestore": "Restaurar visibilidad",
        "copyKeys": "Presione ctrl o u2318 + C para copiar los datos de la tabla al portapapeles del sistema. <br \/> <br \/> Para cancelar, haga clic en este mensaje o presione escape.",
        "copySuccess": {
            "1": "Copiada 1 fila al portapapeles",
            "_": "Copiadas %ds fila al portapapeles"
        },
        "copyTitle": "Copiar al portapapeles",
        "csv": "CSV",
        "excel": "Excel",
        "pageLength": {
            "-1": "Mostrar todas las filas",
            "_": "Mostrar %d filas"
        },
        "pdf": "PDF",
        "print": "Imprimir",
        "renameState": "Cambiar nombre",
        "updateState": "Actualizar",
        "createState": "Crear Estado",
        "removeAllStates": "Remover Estados",
        "removeState": "Remover",
        "savedStates": "Estados Guardados",
        "stateRestore": "Estado %d"
    },
    "autoFill": {
        "cancel": "Cancelar",
        "fill": "Rellene todas las celdas con <i>%d<\/i>",
        "fillHorizontal": "Rellenar celdas horizontalmente",
        "fillVertical": "Rellenar celdas verticalmentemente"
    },
    "decimal": ",",
    "searchBuilder": {
        "add": "Añadir condición",
        "button": {
            "0": "Constructor de búsqueda",
            "_": "Constructor de búsqueda (%d)"
        },
        "clearAll": "Borrar todo",
        "condition": "Condición",
        "conditions": {
            "date": {
                "after": "Despues",
                "before": "Antes",
                "between": "Entre",
                "empty": "Vacío",
                "equals": "Igual a",
                "notBetween": "No entre",
                "notEmpty": "No Vacio",
                "not": "Diferente de"
            },
            "number": {
                "between": "Entre",
                "empty": "Vacio",
                "equals": "Igual a",
                "gt": "Mayor a",
                "gte": "Mayor o igual a",
                "lt": "Menor que",
                "lte": "Menor o igual que",
                "notBetween": "No entre",
                "notEmpty": "No vacío",
                "not": "Diferente de"
            },
            "string": {
                "contains": "Contiene",
                "empty": "Vacío",
                "endsWith": "Termina en",
                "equals": "Igual a",
                "notEmpty": "No Vacio",
                "startsWith": "Empieza con",
                "not": "Diferente de",
                "notContains": "No Contiene",
                "notStarts": "No empieza con",
                "notEnds": "No termina con"
            },
            "array": {
                "not": "Diferente de",
                "equals": "Igual",
                "empty": "Vacío",
                "contains": "Contiene",
                "notEmpty": "No Vacío",
                "without": "Sin"
            }
        },
        "data": "Data",
        "deleteTitle": "Eliminar regla de filtrado",
        "leftTitle": "Criterios anulados",
        "logicAnd": "Y",
        "logicOr": "O",
        "rightTitle": "Criterios de sangría",
        "title": {
            "0": "Constructor de búsqueda",
            "_": "Constructor de búsqueda (%d)"
        },
        "value": "Valor"
    },
    "searchPanes": {
        "clearMessage": "Borrar todo",
        "collapse": {
            "0": "Paneles de búsqueda",
            "_": "Paneles de búsqueda (%d)"
        },
        "count": "{total}",
        "countFiltered": "{shown} ({total})",
        "emptyPanes": "Sin paneles de búsqueda",
        "loadMessage": "Cargando paneles de búsqueda",
        "title": "Filtros Activos - %d",
        "showMessage": "Mostrar Todo",
        "collapseMessage": "Colapsar Todo"
    },
    "select": {
        "cells": {
            "1": "1 celda seleccionada",
            "_": "%d celdas seleccionadas"
        },
        "columns": {
            "1": "1 columna seleccionada",
            "_": "%d columnas seleccionadas"
        },
        "rows": {
            "1": "1 fila seleccionada",
            "_": "%d filas seleccionadas"
        }
    },
    "thousands": ".",
    "datetime": {
        "previous": "Anterior",
        "next": "Proximo",
        "hours": "Horas",
        "minutes": "Minutos",
        "seconds": "Segundos",
        "unknown": "-",
        "amPm": [
            "AM",
            "PM"
        ],
        "months": {
            "0": "Enero",
            "1": "Febrero",
            "10": "Noviembre",
            "11": "Diciembre",
            "2": "Marzo",
            "3": "Abril",
            "4": "Mayo",
            "5": "Junio",
            "6": "Julio",
            "7": "Agosto",
            "8": "Septiembre",
            "9": "Octubre"
        },
        "weekdays": [
            "Dom",
            "Lun",
            "Mar",
            "Mie",
            "Jue",
            "Vie",
            "Sab"
        ]
    },
    "editor": {
        "close": "Cerrar",
        "create": {
            "button": "Nuevo",
            "title": "Crear Nuevo Registro",
            "submit": "Crear"
        },
        "edit": {
            "button": "Editar",
            "title": "Editar Registro",
            "submit": "Actualizar"
        },
        "remove": {
            "button": "Eliminar",
            "title": "Eliminar Registro",
            "submit": "Eliminar",
            "confirm": {
                "_": "¿Está seguro que desea eliminar %d filas?",
                "1": "¿Está seguro que desea eliminar 1 fila?"
            }
        },
        "error": {
            "system": "Ha ocurrido un error en el sistema (<a target=\"\\\" rel=\"\\ nofollow\" href=\"\\\">Más información&lt;\\\/a&gt;).<\/a>"
        },
        "multi": {
            "title": "Múltiples Valores",
            "info": "Los elementos seleccionados contienen diferentes valores para este registro. Para editar y establecer todos los elementos de este registro con el mismo valor, hacer click o tap aquí, de lo contrario conservarán sus valores individuales.",
            "restore": "Deshacer Cambios",
            "noMulti": "Este registro puede ser editado individualmente, pero no como parte de un grupo."
        }
    },
    "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
    "stateRestore": {
        "creationModal": {
            "button": "Crear",
            "name": "Nombre:",
            "order": "Clasificación",
            "paging": "Paginación",
            "search": "Busqueda",
            "select": "Seleccionar",
            "columns": {
                "search": "Búsqueda de Columna",
                "visible": "Visibilidad de Columna"
            },
            "title": "Crear Nuevo Estado",
            "toggleLabel": "Incluir:"
        },
        "emptyError": "El nombre no puede estar vacio",
        "removeConfirm": "¿Seguro que quiere eliminar este %s?",
        "removeError": "Error al eliminar el registro",
        "removeJoiner": "y",
        "removeSubmit": "Eliminar",
        "renameButton": "Cambiar Nombre",
        "renameLabel": "Nuevo nombre para %s",
        "duplicateError": "Ya existe un Estado con este nombre.",
        "emptyStates": "No hay Estados guardados",
        "removeTitle": "Remover Estado",
        "renameTitle": "Cambiar Nombre Estado"
    }
}

$(document).ready(function () {

    msgBoxImagePath = "/image/";

    var head_mb = "<th>#</th>";
    head_mb += "<th>Mercado</th>";
    head_mb += "<th>Estado</th>";
    head_mb += "<th>Municipio</th>";
    head_mb += "<th></th>";
    //head_mb += "<th></th>";
    $("#row_head_grdDatos").append(head_mb);
    /*  Botón agreagar */
    $('#create-new-reg').click(function () {
        if ($("#cd_mercado_combo").val() == "") {
            $.msgBox({
                title: "Cushman ONE",
                content: "Seleccione un Mercado",
                type: "error"
            });
        } else {
            $('#cd_estado option[value=0]').prop('selected', 'selected');
            //$('#cd_municipio option[value=0]').prop('selected', 'selected').change();
            $('#cd_municipio').empty();
            $('#rowCrudModal').html("Agregar registro");
            $('#ajax-crud-modal').modal('show');
            $('#cd_mercado').val($('#cd_mercado_combo').val());
        }
    });

    /*  Carga combos*/
    //$("#cd_estado").empty();
    storeCargaCombo("/Utils/GetCdEstado", "cd_estado");
    storeCargaCombo("/Utils/GetMercados", "cd_mercado_combo");
    SetMercados();
    $('#grdDatos_processing').hide();
    $("#cd_mercado_combo")
        .change(function (event) {
            if (this.value != '') {
                let laTabla = $('#grdDatos').DataTable();
                laTabla.ajax.url("/GruposMercados/GetGruposMercadosAll/" + this.value).load();
            }
            return false;
        });

    $("#cd_estado")
        .change(function () {
            $("#cd_municipio").empty();
            if (this.value != '') {
                storeCargaCombo("/Utils/GetMunicipioByCdEstado",
                    "cd_municipio",
                    "cd_estado",
                    "cd_municipio",
                    undefined,
                    "cd_estado",
                    undefined,
                    undefined);
            }
            return false;
        });
    $(".GurdarMercado").on('click', function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        GuardaDetalleMercado();

    });
});

function SetMercados(idEstado) {
    let URLMaster = "/GruposMercados/GetGruposMercadosAll/" + idEstado;
    //let RECURSOS = "../Scripts/datatables/Spanish.json";
    let ColumnsDataTable = [];
    let ColumnEdit = '<div style="margin: 0 auto;text-align: center;">';
    ColumnEdit += '<button title="Click para editar" type="button" class="btn btn-sm btn-primary btneditar">';
    ColumnEdit += '<i class="fa fa-pencil" aria-hidden="true"></i> Editar </button></div >';

    let ColumnDetails = '<div style="margin: 0 auto;text-align: center;">';
    ColumnDetails += '<button title="Click para editar" type="button" class="btn btn-sm btn-primary btdetalle">';
    ColumnDetails += '<i class="fa fa-list-alt" aria-hidden="true"></i> Detalles </button></div >';

    let ColumnDelete = '<div style="margin: 0 auto;text-align: center;">';
    ColumnDelete += '<button title="Click para eliminar" type="button" class="btn btn-outline-light btn-sm btdelete">';
    ColumnDelete += '<i class="fa fa-trash-o fs-3 text-danger" aria-hidden="true"></i></button></div >';

    ColumnsDataTable = [
        { data: 'cd_grupo', orderable: false, searchable: false, visible: false },
        { data: 'nb_mercado', orderable: false, searchable: true, visible: false },
        { data: 'nb_estado', orderable: false, searchable: true, visible: false },
        { data: 'nb_municipio', searchable: true },
        { defaultContent: ColumnDelete, searchable: false },
        
    ],
        //{ defaultContent: ColumnEdit, searchable: false },
        //{ defaultContent: ColumnDetails, searchable: false }

    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    
    /* Grid con datos */
    $('#grdDatos').DataTable({
        "serverSide": true,
        //dom: 'Bfrtip',
        //"iDisplayLength": 25,
        // Searching Setups
        searching: { regex: true },
        "ajax": {
            "url": "",
            "type": "POST",
            "dataSrc": 'aaData',
            "beforeSend": function () {
            },
            data: function (d) {
                return JSON.stringify(d);
            },
            "error": function (xhr, status, error) {
                //console.log(xhr.responseText);
            },
            //"success": function (data, settings) {
            //    //console.log(data);
            //    //var table = $('#grdDatos').DataTable();
            //    //table.clear();
            //    //table.rows.add(data.aaData).draw();
            //    ////table.rows.add(data).draw();
            //    //$('#grdDatos_processing').hide();
            //    //alert('Cargado..');
            //},
        },
        "info": true,
        "stateSave": false,
        "cache": false,
        "dataType": 'json',
        "contentType": 'application/json; charset=utf-8',
        "orderCellsTop": true,
        "fixedHeader": true,
        "processing": true,
        "bPaginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bSort": true,
        "bInfo": true,
        //"bAutoWidth": false,
        //"autoWidth": false,
        "pageLength": 10,
        "scrollY": "550px",
        "scrollCollapse": true,
        "paging": true,
        "lengthMenu": [[10, 30, 40, -1], [10, 30, 40, "All"]],
        "searching": true,
        "select": true,
        "columns": ColumnsDataTable,
        "columnDefs": [
            { width: 20, targets: 0 },
            { width: 20, targets: 1 },
            {
                width: 20,
                //render: function (data, type, full, meta) {
                //    return ('<div class = "col-sm-12 text-wrap">' + data + '</div>');
                //},
                targets: 2
            },
            { width: 20, targets: 3 },
            { width: 2, targets: 4 },            
        ],
        //"fixedColumns": {
        //    heightMatch: 'none'
        //},
        "oSearch": { "bSmart": false, "caseInsensitive": false },
        "language": languageSPANISH,
        "initComplete": function (settings, data) {
            //alert("Termino e cargar");
        },
        "createdRow": function (row, data, dataIndex) {
            //if (data.K0044 == '1') {
            //    poner de color la fila
            //}
            //$(row).css({"background-color":"red","color":"#ffffff"});
        }
    });
    

    $('#grdDatos_processing').hide();

    $('#grdDatos')
        .on('processing.dt', function (e, settings, processing) {
            var dtable = $('#grdDatos').dataTable();
            // Grab the datatables input box and alter how it is bound to events
            $(".dataTables_filter input")
                .unbind() // Unbind previous default bindings
                .bind('keypress keyup', function (e) { // Bind our desired behavior
                    // If the length is 3 or more characters, or the user pressed ENTER, search
                    if (this.value.length >= 3 || e.keyCode == 13) {
                        // Call the API search function

                        //1.{ string }: String to filter the table on
                        //2.{ int | null }: Column to limit filtering to
                        //3.{ bool } [default=false]: Treat as regular expression or not
                        //4.{ bool } [default=true]: Perform smart filtering or not
                        //5.{ bool } [default=true]: Show the input global filter in it's input box(es)
                        //6.{ bool } [default=true]: Do case -insensitive matching(true) or not(false)


                        //dtable.fnFilter("^" + $(this).val() + "$", 4, true);
                        dtable.fnFilter($(this).val());
                    }
                    // Ensure we clear the search if they backspace far enough
                    return;
                });
        });
   
    /*  Botón Detalle */
    $('#grdDatos tbody').on('click', 'button.btdetalle', function () {
        let tabla = $('#grdDatos').DataTable();
        let data_form = tabla.row($(this).parents("tr")).data();
        console.log(data_form);
        $('#rowForm').trigger("reset");
        $('#rowCrudModal').html("Agregar registro");
        $('#ajax-crud-modal').modal('show');
    });
    /*  Botón Editar */
    $('#grdDatos tbody').on('click', 'button.btneditar', function () {
        let tabla = $('#grdDatos').DataTable();
        let data_form = tabla.row($(this).parents("tr")).data();
        console.log(data_form);
        $('#rowForm').trigger("reset");
        $('#rowCrudModal').html("Agregar registro");
        $('#ajax-crud-modal').modal('show');
    });
    /*  Botón Eliminar */
    $('#grdDatos tbody').on('click', 'button.btdelete', function () {
        let tabla = $('#grdDatos').DataTable();
        let data_form = tabla.row($(this).parents("tr")).data();
        //console.log(data_form);
        DeleteGrupo(data_form.cd_grupo);
    });
    
}

function GuardaDetalleMercado() {        
    let valToken = $('input:hidden[name="__RequestVerificationToken"]').val();
    $.ajaxSetup({
        headers: {
            RequestVerificationToken: valToken,
        }
    });
    let data = $('#rowForm').serialize(); 
    console.log(data);
    $.ajax({
        data: data,
        url: "/GruposMercados/CreaMercado",
        type: "POST",
        dataType: 'json',
        contentType: "application/x-www-form-urlencoded",
        //headers: {
        //    RequestVerificationToken: valToken,
        //},
        success: function (data) {
            if (data.cd_grupo) {
                $('#ajax-crud-modal').modal('hide');
                $.msgBox({
                    title: "Cushman ONE",
                    content: "Municipio agregado",
                    type: "info"
                });
                let laTabla = $('#grdDatos').DataTable();
                let MercadoSeleccionado = $("#cd_mercado_combo").val();
                laTabla.ajax.url("/GruposMercados/GetGruposMercadosAll/" + MercadoSeleccionado).load();
            }
        },
        error: function (request, message, error) {
            if (request.status == 409) {
                $('#ajax-crud-modal').modal('hide');
                $.msgBox({
                    title: "Cushman ONE",
                    content: error,
                    type: "alert"
                });
            } else {
                console.log('Error:', error);
                console.log(request);            
            }
            
        }
    });
}

function DeleteGrupo(Id) {
    let valToken = $('input:hidden[name="__RequestVerificationToken"]').val();
    $.ajaxSetup({
        headers: {
            RequestVerificationToken: valToken,
        }
    });
    //let data = $('#rowForm').serialize();
    $.ajax({
        data: "__RequestVerificationToken=" + valToken + "&id=" + Id,//JSON.stringify({ id: Id }),
        url: "/GruposMercados/Delete",
        type: "POST",
        dataType: 'json',
        contentType: "application/x-www-form-urlencoded",
        success: function (data) {
            if (data.success) {
                $('#ajax-crud-modal').modal('hide');
                $.msgBox({
                    title: "Cushman ONE",
                    content: "Municipio Eliminado",
                    type: "info"
                });
                let laTabla = $('#grdDatos').DataTable();
                let MercadoSeleccionado = $("#cd_mercado_combo").val();
                laTabla.ajax.url("/GruposMercados/GetGruposMercadosAll/" + MercadoSeleccionado).load();
            }
        },
        error: function (request, message, error) {
            if (request.status == 409) {
                $('#ajax-crud-modal').modal('hide');
                $.msgBox({
                    title: "Cushman ONE",
                    content: error,
                    type: "alert"
                });
            } else {
                console.log('Error:', error);
                console.log(request.responseText);
            }

        }
    });


}