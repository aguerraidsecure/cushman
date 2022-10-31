$(document).ajaxStart(function () {
    ShowLoading();    
});
$(document).ajaxComplete(function (event, request, settings) {
    HiedeLoading();
});



var BrokersList = $('select[name="duallistbox_naves[]"]').bootstrapDualListbox({
    nonSelectedListLabel: 'Broker(s) disponibles',
    selectedListLabel: 'Broker(s) seleccionado(s)',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: false,
    moveAllLabel: '',
    removeAllLabel: ' ',
    filterPlaceHolder: 'Filtro',
    infoText: 'Encontrados {0}',
    infoTextEmpty: 'Lista vacía',
    infoTextFiltered: '<span class="label label-warning">Filtrados</span> {0} de {1}'
});
var MunicipioList = $('select[name="duallistbox_municipios[]"]').bootstrapDualListbox({
    nonSelectedListLabel: 'Municipio(s) disponible(s)',
    selectedListLabel: 'Municipio(s) seleccionado(s)',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: false,
    moveAllLabel: '',
    removeAllLabel: ' ',
    filterPlaceHolder: 'Filtro',
    infoText: 'Encontrados {0}',
    infoTextEmpty: 'Lista vacía',
    infoTextFiltered: '<span class="label label-warning">Filtrados</span> {0} de {1}'
});
$(document).ready(function () {
    msgBoxImagePath = "/image/";    
    //ShowLoading();
    $("#cd_mercado").empty();    
    storeCargaCombo("/Utils/GetMercados", "cd_mercado");
    $.ajax({
        type: 'GET',
        url: '/Brokers/GetBrokersbyName',
        datatype: "json/application",
        success: function (StoreData) {
            $.each(StoreData, function (i, item) {
                let o = "<option value='" + item.nb_broker + "'>" + item.nb_broker + " | " + item.nb_puesto + "</option>"
                BrokersList.append(o);
            });
            BrokersList.bootstrapDualListbox('refresh');
            $("#cd_mercado").change(function () {
                if ($("#cd_mercado").val() != null) {
                    storeGetDualList("/GruposMercados/GetGruposListById", MunicipioList, $("#cd_mercado").val());
                }
                return false;
            });
        }
    });
    $('input[type=range]').on('input', function () {
        $(this).trigger('change');
    });
    $("#minRange").change(function () {
        var newval = $(this).val();
        $("#num_min").val(newval);
    });
    $("#num_min").keyup(function (event) {
        let valorMin = $(this).val();
        if (valorMin == "") {
            $("#minRange").val(0);
        } else {
            $("#minRange").val(valorMin);
        }
    });
    $("#maxRange").change(function () {
        var newval = $(this).val();
        $("#num_max").val(newval);
    });
    $("#num_max").keyup(function (event) {
        let valorMin = $(this).val();
        if (valorMin == "") {
            $("#maxRange").val(0);
        } else {
            $("#maxRange").val(valorMin);
        }
    });
    /*  Botón agreagar */
    $('#addBroker').click(function () {
        $("#nb_broker").val('');
        $("#nb_puesto").val('');
        $("#nu_telefono").val('');
        $('#rowCrudModal').html("Agregar Broker");
        $('#ajaxcrudmodal').modal('show');
    });

    $(".Gurdar").on('click', function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        GuardaBroker();
    });
});
function GuardaBroker() {
    let valToken = $('input:hidden[name="__RequestVerificationToken"]').val();
    $.ajaxSetup({
        headers: {
            RequestVerificationToken: valToken,
        }
    });
    let data = $('#rowFormBroker').serialize();
    $.ajax({
        data: data,
        url: "/Brokers/AddBroker",
        type: "POST",
        dataType: 'json',
        contentType: "application/x-www-form-urlencoded",
        success: function (data) {
            if (data.cd_broker) {
                $.ajax({
                    type: 'GET',
                    url: '/Brokers/GetBrokersbyName',
                    datatype: "json/application",
                    success: function (StoreData) {
                        $.each(StoreData, function (i, item) {
                            let o = "<option value='" + item.nb_broker + "'>" + item.nb_broker + " | " + item.nb_puesto + "</option>"
                            BrokersList.append(o);
                        });
                        BrokersList.bootstrapDualListbox('refresh');
                        $.msgBox({
                            title: "Cushman ONE",
                            content: "Broker agregado",
                            type: "info"
                        });
                    }
                });
                $('#ajaxcrudmodal').modal('hide');
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
