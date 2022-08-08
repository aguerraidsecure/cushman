
function storeCargaCombo(Url, objComboLlenar, objComboPadre, ObjComboHijo, ObjComboNieto, idAbuelo, idPadre, idHijo) {
    if (!(idAbuelo == undefined)) {
        var data = { idAbuelo: $("#" + objComboPadre).val() }
    }
    else {
        var data = {}
    }
    if (!(idPadre == undefined)) {
        var data = { idAbuelo: $("#" + objComboPadre).val(), idPadre: $("#" + ObjComboHijo).val() }
    }
    if (!(idHijo == undefined)) {
        var data = { idAbuelo: $("#" + objComboPadre).val(), idPadre: $("#" + ObjComboHijo).val(), idHijo: $("#" + ObjComboNieto).val() }
    }
    $.ajax({
        type: 'POST',
        //Llamado al metodo GetDepartByGeren en el controlador
        url: Url,
        dataType: 'json',
        //Parametros que se envian al metodo del controlador
        data: data,
        //En caso de resultado exitosos
        success: function (StoreData) {
            if (StoreData.length == 0) {
                $("#" + objComboLlenar).append('<option value=""></option>');
            } else {
                //Se agrega el elemento vacio para poder desplegar que seleccione una opcion
                $("#" + objComboLlenar).append('<option value=""></option>');
                $.each(StoreData,
                    function (i, StoreData) {
                        $("#" + objComboLlenar)
                            .append('<option value="' +
                                StoreData.Value +
                                '">' +
                                StoreData.Text +
                                '</option>');
                    });
                if (($("#" + objComboLlenar + "_h").val() != "") && ($("#" + objComboLlenar + "_h").val() != "0")) {
                    $("#" + objComboLlenar).val($("#" + objComboLlenar + "_h").val());
                    $("#" + objComboLlenar).trigger("change");
                }
            }
        },
        //Mensaje de error en caso de fallo
        error: function (ex) {
            //alert('falla: ' + ex);
        }
    })
};
