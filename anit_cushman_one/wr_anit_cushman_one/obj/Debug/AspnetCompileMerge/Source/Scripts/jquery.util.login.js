$(document)
    .ready(function () {
        msgBoxImagePath = "/image/";
    	$("#frmLogin")
            .submit(function () {
            	var form = $(this);
            	//Aquí se pueden hacer las validaciones con JS
            	if (form.validate()) {
            		form.ajaxSubmit({
            			dataType: 'JSON',
            			type: 'POST',
            			url: form.attr('action'),
            			success: function (r) {
            				if (r.response) {
            					window.location.replace(r.href);
            				} else {
            					$.msgBox({
            						title: "dotProyAlmacen",
            						content: r.message,
            						type: "alert"
            					});
            				}
            			},
            			error: function (jqXHR, textStatus, errorThrown) {
            				$.msgBox({
            					title: "dotProyAlmacen",
            					content: "Error : " + errorThrown,
            					type: "alert"
            				});
            			}
            		});
            	}
				return false;
            });
    });