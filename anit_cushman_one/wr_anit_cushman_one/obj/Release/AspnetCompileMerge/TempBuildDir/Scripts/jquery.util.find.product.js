function storeCargaProductos(Url) {
    var data = {};
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
                
            } else {
                loadData(StoreData);
            }
        },
        //Mensaje de error en caso de fallo
        error: function (jqXHR, textStatus, errorThrown) {
            $.msgBox({
                title: "ProyCushman",
                content: "Error en el servidor: " + errorThrown,
                type: "alert"
            });
        }
    });
};
function loadData(dataStoreData)
{
    
};

$(document)
    .ready(function () {        
        $("#btnAllProduct")
            .click(function () {
                alert("hola");
         });              
    });
