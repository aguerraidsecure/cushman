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