// Write your Javascript code.

function AjustarPantalla(divNombre, porcentajeAlto, porcentajeAncho) {


    var iAlto = window.screen.availHeight * porcentajeAlto;
    var iAncho = window.screen.availWidth * porcentajeAncho;

    $('#' + divNombre).height(iAlto);
    $('#' + divNombre).width(iAncho);
}



function FormDatosJSON(nombreForma, datos) {
    var oForm = document.getElementById(nombreForma);

    Object.keys(datos).forEach(function (prop) {
        var elemento = document.createElement('INPUT');
        elemento.type = 'HIDDEN';
        elemento.name = prop;
        elemento.value = datos[prop];
        oForm.appendChild(elemento);
    });
}


function FormDataJSON(datos) {
    var formData = new FormData();

    Object.keys(datos).forEach(function (prop) {
        formData.append(prop, datos[prop]);
    });
    return formData;
}




function AjaxPostFile(pUrl, pMetodo, pFormData, pFunc, pMimeType, pUrlInicio, pTipoAsyc) {
    $.ajax({
        url: pUrl,
        type: pMetodo,
        data: pFormData,
        mimeType: pMimeType,
        contentType: false,
        cache: false,
        processData: false,
        async: pTipoAsyc,
        success: function (data) {
            if (data === undefined) {
                w2alert("No existen Información");
            } else {
                pFunc(data);
            }
        },
        fail: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status === 401) {
                w2alert("La sesión de usuario no existe, se redirecciona a la página principal");
                window.open(pUrlInicio, "_self");
            }
            else {
                w2alert("Existe un error en el sistema, favor de avisar al área de la DGTI");
            }            
        }
    });
}


function AjaxPostJson(pUrl, pDataType, pContentType, pFunc, pUrlInicio, pJsonDatos) {
    console.log("AJAX::::::::" + pUrl);

    $.ajax({
        url: pUrl,
        dataType: pDataType,
        type: "POST",
        contentType: pContentType,
        data: pJsonDatos,
        success: function (data) {
            if (data === undefined) {
                w2alert("No existen Información");
            } else {
                pFunc(data);
            }
        },
        fail: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status === 401) {
                w2alert("La sesión de usuario no existe, se redirecciona a la página principal");
                window.open(pUrlInicio, "_self");
            }
            else {
                w2alert("Existe un error en el sistema, favor de avisar al área de la DGTI");
            }
            w2popup.unlock();
        }
    });
}


function AjaxPostForm(pUrl, pData, pFunc, pUrlInicio) {
    console.log("AJAX::::::::" + pUrl  + ":::" + pData);

    $.ajax({
        url: pUrl,
        type: "POST",
        data: pData,
        success: function (data) {
            if (data === undefined) {
                w2alert("No existen Información");
            } else {
                pFunc(data);
            }
        },
        fail: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status === 401) {
                w2alert("La sesión de usuario no existe, se redirecciona a la página principal");
                window.open(pUrlInicio, "_self");
            }
            else {
                w2alert("Existe un error en el sistema, favor de avisar al área de la DGTI");
            }
            w2popup.unlock();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status === 401) {
                w2alert("La sesión de usuario no existe, se redirecciona a la página principal");
                window.open(pUrlInicio, "_self");
            }
            else {
                w2alert("Existe un error en el sistema, favor de avisar al área de la DGTI");
            }
            w2popup.unlock();
        }
    });
}

function AjaxValidarSesion(pUrl, pUrlInicio) {
    $.ajax({
        url: pUrl,
        type: "GET",
        fail: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status === 401) {
                alert("La sesión de usuario no existe, se redirecciona a la página principal");
                window.open(pUrlInicio, "_self");
            }
            else {
                w2alert("Existe un error en el sistema, favor de avisar al área de la DGTI");
            }            
            w2popup.unlock();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.status === 401) {
                alert("La sesión de usuario no existe, se redirecciona a la página principal");
                window.open(pUrlInicio, "_self");
            }
            else {
                w2alert("Existe un error en el sistema, favor de avisar al área de la DGTI");
            }
            w2popup.unlock();
        }
    });
}

function openPopup(sTitulo, iAncho, iLargo, oFuncionOpen, oFuncionClose) {
    w2popup.open({
        title: sTitulo,
        width: iAncho,
        height: iLargo,
        showMax: false,
        model: false,
        body: '<div id="divBodyPopup" style="position: absolute; left: 5px; top: 5px; right: 5px; bottom: 5px;"></div>',
        onOpen: function (event) {
            event.onComplete = function () {
                flag = true;
                oFuncionOpen();
            };
        },
        onClose: function (event) {
            oFuncionClose();
        },
        onMax: function (event) {
            event.onComplete = function () {
                w2ui.layout.resize();
            };
        },
        onMin: function (event) {
            event.onComplete = function () {
                w2ui.layout.resize();
            };
        }
    });
}


////function openPopup( sNombreForma, sTitulo ) {
////    w2popup.open({
////        title: sTitulo,
////        width: 780,
////        height: 580,
////        showMax: false,
////        model: false,
////        ///body: '<div id="popupSeguimiento" style="position: absolute; left: 5px; top: 5px; right: 5px; bottom: 5px;"></div>',
////        body:  $("#" + "sNombreForma").html(),
////        onOpen: function(event) {
////            event.onComplete = function() {
////                flag = true;
////                ///$('#w2ui-popup #popupSeguimiento').w2render(sNombreForma);
////                $("#TipoProceso").val(0);
////            };
////        },
////        onClose: function(event) {
////            /*event. = function() {
////                }*/
////        },
////        onMax: function(event) {
////            event.onComplete = function() {
////                w2ui.layout.resize();
////            };
////        },
////        onMin: function(event) {
////            event.onComplete = function() {
////                w2ui.layout.resize();
////            };
////        }
////    });
////}