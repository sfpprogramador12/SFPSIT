/* Esta funcion permite actualziar una lista de elentos a partir de uatos JSON */

function htmlSelect(nombre, funcion, elementos)
{
    var sHTML = '<select id="' + nombre + '" " ' + funcion  + '>';
    var reg = elementos.records;
        
    for (var idx = 0; idx < reg.length; idx++)
    {
        sHTML += '<option value="' + reg[idx].id + '">' + reg[idx].text + '</option>';
    }
    
    sHTML += '<select>';

    return sHTML;
}

function BuscarAreaDescripcion(Areas, AreaIdx)
{
    var iTotal = Areas.length;
    for (var iRecorre = 0; iRecorre < iTotal; iRecorre++)
    {
        if (Areas[iRecorre].araclave === AreaIdx)
            return Areas[iRecorre].KA_DESCRIPCION;
    }
    return "Error";
}

function BuscarIndiceGrid(iIdx, Registros)
{
    alert("borrar esta funcion");
    var iTotal = Registros.length;
    for (var iRecorre = 0; iRecorre < iTotal; iRecorre++) {
        if (Registros[iRecorre].recid === iIdx)
            return iRecorre;
    }
    return 0;
}

function SelectAddElemClear(nombre, elementos, mensaje) {
    // Primero borramos contenido
    $('#' + nombre)
        .find('option')
        .remove()
        .end();

    if (mensaje === true) {
        $('#' + nombre).append($('<option>', {
            value: 0,
            text: " -- seleccionar --",
            disabled: "disabled",
            selected: "selected"
        }));
    }


    $.each(elementos, function (i, item) {
        $('#' + nombre).append($('<option>', {
            value: item.id,
            text: item.DESCRIP
        }));
    });
}

function SelectAddElemClearText(nombre, elementos, mensaje) {
    // Primero borramos contenido
    $('#' + nombre)
        .find('option')
        .remove()
        .end();

    if (mensaje === true) {
        $('#' + nombre).append($('<option>', {
            value: 0,
            text: " -- seleccionar --",
            disabled: "disabled",
            selected: "selected"
        }));
    }


    $.each(elementos, function (i, item) {
        $('#' + nombre).append($('<option>', {
            value: item.id,
            text: item.text
        }));
    });
}



function SelectAddElemClearLimite(nombre, elementos, fecIni, fecAct) {
    // Primero borramos contenido
    $('#' + nombre)
        .find('option')
        .remove()
        .end();


    $('#' + nombre).append($('<option>', {
        value: 0,
        text: " -- Seleccionar --",
        disabled: "disabled",
        selected: "selected"
    }));

    for (var iIdx = 0; iIdx < elementos.records.length; iIdx++) {        
        if (elementos.records[iIdx].plazo > 0) {
            var fecLimite = eval("new Date('" + elementos.records[iIdx].fecLimite + "')");

            if (fecAct <= fecLimite) {
                $('#' + nombre).append($('<option>', {
                    value: elementos.records[iIdx].id,
                    text: elementos.records[iIdx].text + " (Habilitado hasta el día:" + fecLimite.toLocaleDateString() + ")",
                }));
            }
            else {
                $('#' + nombre).append($('<option>', {
                    value: elementos.records[iIdx].id,
                    text: elementos.records[iIdx].text + " (El dia límite fue: " + fecLimite.toLocaleDateString() + ")",
                    disabled: "disabled"
                }));
            }
        }
        else {
            $('#' + nombre).append($('<option>', {
                value: elementos.records[iIdx].id,
                text: elementos.records[iIdx].text
            }));
        }
        
    }
}

function SelectAddElemClearLimiteNivel(nombre, elementos, fecIni, fecAct, nivel) {
    // Primero borramos contenido
    $('#' + nombre)
        .find('option')
        .remove()
        .end();


    $('#' + nombre).append($('<option>', {
        value: 0,
        text: " -- Seleccionar --",
        disabled: "disabled",
        selected: "selected"
    }));

    for (var iIdx = 0; iIdx < elementos.records.length; iIdx++)
    {
        if (nivel === elementos.records[iIdx].nivel)
        {
            if (elementos.records[iIdx].plazo > 0)
            {
                var fecLimite = eval("new Date('" + elementos.records[iIdx].fecLimite + "')");

                if (fecAct <= fecLimite) {
                    $('#' + nombre).append($('<option>', {
                        value: elementos.records[iIdx].id,
                        text: elementos.records[iIdx].text + " (Habilitado hasta el día:" + fecLimite.toLocaleDateString() + ")",
                    }));
                }
                else {
                    $('#' + nombre).append($('<option>', {
                        value: elementos.records[iIdx].id,
                        text: elementos.records[iIdx].text + " (El dia límite fue: " + fecLimite.toLocaleDateString() + ")",
                        disabled: "disabled"
                    }));
                }
            }
            else
            {
                $('#' + nombre).append($('<option>', {
                    value: elementos.records[iIdx].id,
                    text: elementos.records[iIdx].text
                }));
            }
        }
    }
}

function SelectAddElemClearNivel(nombre, elementos,  nivel) {
    // Primero borramos contenido
    $('#' + nombre)
        .find('option')
        .remove()
        .end();


    $('#' + nombre).append($('<option>', {
        value: 0,
        text: " -- Seleccionar --",
        disabled: "disabled",
        selected: "selected"
    }));

    for (var iIdx = 0; iIdx < elementos.records.length; iIdx++) {
        if (nivel === elementos.records[iIdx].nivel) 
        {
            $('#' + nombre).append($('<option>', {
                value: elementos.records[iIdx].id,
                text: elementos.records[iIdx].text
            }));        
        }
    }
}

function HabilitarTabAclaracionRecurso()
{
    if (_FecAclaracion !== undefined)
    {
        if (_FecAclaracion === 0) {
            w2ui.tabInformacion.remove('aclaracion');
        }
        else {
            $().w2grid(GetGridAclaracion());
            w2ui.gridAclaracion.records = _Aclaracion.records;
            w2ui.gridAclaracion.reload();
        }
    }

    if (_RecRevision !== undefined)
    {
        if (_RecRevision.records.length === 0) {
            w2ui.tabInformacion.remove('recrevision');
        } else {
            $().w2grid(GetGridRecRevision());
            w2ui.gridRecRevision.records = _RecRevision.records;
            w2ui.gridRecRevision.reload();
        }
    }
}


// FUNCIONES PARA LA PAGINACION 

var PAGINA_INICIO = 1;
var PAGINA_ANTERIOR = 2;
var PAGINA_POSTERIOR = 3;
var PAGINA_FINAL = 4;

var _PaginaRegistro = 100;
var _PaginaActual = 1;
var _PaginaMax = 0;


function Paginacion(opcion) {
    if (opcion === PAGINA_INICIO) {
        _PaginaActual = 1;
    }
    else if (opcion === PAGINA_ANTERIOR) {
        if (_PaginaActual > 1)
            _PaginaActual--;
    }
    else if (opcion === PAGINA_POSTERIOR) {
        if (_PaginaActual < _PaginaMax)
            _PaginaActual++;
    }
    else if (opcion === PAGINA_FINAL)
        _PaginaActual = _PaginaMax;

    $("#txtPagAct").val(_PaginaActual);

    // falta deshabilitar en la parte de valor maximo
    Buscar();
}


function PaginacionIni() {
    _PaginaRegistro = 100;
    _PaginaActual = 1;
    _PaginaMax = 0;
    $("#txtPagAct").val(_PaginaActual);
    $("#pagActIni").text(" ");
    $("#pagActFin").text(" ");
    $("#pagTotal").text(" ");
}

function PaginacionAct(Registros, RegTotal) {

    $("#pagActIni").text(_PaginaActual);
    
    if (Registros > 0) {
        _PaginaMax = parseInt(RegTotal / _PaginaRegistro);
        _PaginaAdi = parseInt(RegTotal % _PaginaRegistro);
        if (_PaginaAdi > 0)
            _PaginaMax++;

        $("#pagActFin").text(" de " + _PaginaMax);
    }
    else {
        $("#pagActFin").text(" de 1");
    }

    if (RegTotal > 0) {
        var iTotalAct = (_PaginaActual * _PaginaRegistro)

        var iValIni = (_PaginaActual - 1) * _PaginaRegistro;

        if (iValIni === 0 )
            iValIni = 1;
        
        var iValFin =0;
        if (iTotalAct < RegTotal)
            iValFin = iTotalAct;
        else
            iValFin = RegTotal;

        $("#pagTotal").text(iValIni + " - " + iValFin + " de " + RegTotal);

    } else
        $("#pagTotal").text(" ");
}


function FormatoSugerido() {
    var sFormato;
    for (var i = 0; i < _ListaAccion.records.length; i++) {
        if (_ListaAccion.records[i].id === $('#lstAccion').val()) {
            sFormato = _ListaAccion.records[i].formato;

            sFormato = sFormato.replace("|Fecha|", "Ciudad de México");

            for (var y = 0; y < w2ui.gridSolicitud.records.length; y++) {
                if (w2ui.gridSolicitud.records[y].titulo === "Descripción") {
                    sFormato = sFormato.replace("|Solicitud|", w2ui.gridSolicitud.records[y].valor);
                    break;
                }

            }
            $("#txtCkEditor").val(sFormato);
        }
    }
}


function RemoverOnclick(repclave, detclave, docclave) {
    var formData = new FormData();
    formData.append("repclave", repclave);
    formData.append("detclave", detclave);
    formData.append("docclave", docclave);
    
    AjaxPostFile(_UrlRespuesta + "DocRemoverRespuesta", "POST", formData, DocRemoverResp, "multipart/form-data", _UrlInicio, false);

    return false;
}
