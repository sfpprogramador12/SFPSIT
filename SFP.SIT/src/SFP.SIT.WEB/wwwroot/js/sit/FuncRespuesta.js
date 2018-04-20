

/* VARIABLE GLOBAL _DOCUMENTO */

function obtenerDocumento(iIdx) {
    var iRecorre = 0;
    var iTotal = _Documento.length;

    if (iIdx === '')
        return '';
    else {
        for (iRecorre = 0; iRecorre < iTotal; iRecorre++) {
            if (_Documento[iRecorre].rtpclave === iIdx) {
                return _Documento[iRecorre].rtpformato;
            }
        }
        return '';
    }
}

/*-- ---------------------------------------------
                FUNCIONES DE RESPUESTA
--------------------------------------------- --*/
function EventosRespuesta() {
    $('#radCompetenciaArea').click(function () {
        $('#lstTipoRespuesta').val("");
        $('#divEditorResolucion').show();
        $('#tblArchivo').show();

        //SI
        if ($('#radCompetencia1')[0].checked === true) {
            $('#trRequiereAclaracion').show();
            $('#radAclaracion1')[0].checked = true;
            $('#resolucion').val(obtenerDocumento(_RespInfoAdicional));
        } else {
            $('#resolucion').val(obtenerDocumento(_RespNoCompeteciaUE));
            $('#trRequiereAclaracion').hide();
            $('#trExiteInformacion').hide();
            $('#trAmpliacion').hide();
            $('#trRespuesta').hide();
            $('#divGridResolucion').hide();
            $('#divAnexarArchivos').hide();
        }
    });

    $('#radRequireAclaracion').click(function () {
        $('#lstTipoRespuesta').val("");
        $('#divEditorResolucion').show();
        $('#tblArchivo').show();
        $('#trRespuesta').hide();
        $('#divGridResolucion').hide();
        $('#divAnexarArchivos').hide();

        //SI
        if ($('#radAclaracion1')[0].checked === true) {
            $('#trAmpliacion').hide();
            $('#trExiteInformacion').hide();
            $('#resolucion').val(obtenerDocumento(_RespInfoAdicional));
        } else {
            $('#trExiteInformacion').show();
            $('#radExiste2')[0].checked = true;
        }
    });

    $('#radExisteInfo').click(function () {
        $('#lstTipoRespuesta').val("");
        $('#divEditorResolucion').show();
        $('#tblArchivo').show();
        $('#trAmpliacion').hide();
        $('#trRespuesta').hide();
        $('#divGridResolucion').hide();
        $('#divAnexarArchivos').hide();

        //SI
        if ($('#radExiste1')[0].checked === true) {
            $('#radAmpliacion1')[0].checked = true;
            $('#trAmpliacion').show();
            $('#divGridResolucion').hide();
            $('#trRespuesta').hide();
            $('#resolucion').val(obtenerDocumento(_RespProrroga));
        } else {
            $('#resolucion').val(obtenerDocumento(_RespInexistente));
        }
    });

    $('#radAmpliacion').click(function () {
        $('#lstTipoRespuesta').val("");
        $('#divGridResolucion').hide();
        $('#divAnexarArchivos').hide();

        // SI
        if ($('#radAmpliacion1')[0].checked === true) {
            $('#divEditorResolucion').show();
            $('#tblArchivo').show();
            $('#resolucion').val(obtenerDocumento(_RespProrroga));
            $('#trRespuesta').hide();

        } else {
            $('#divEditorResolucion').hide();
            $('#tblArchivo').hide();
            $('#trRespuesta').show();
        }
    });

    /*-- ---------------------------------------------
                LISTADO DE RESPUESTAS
    --------------------------------------------- --*/
    $( "#lstTipoRespuesta" ).change(function() {
        var iResp = $('#lstTipoRespuesta').val();

        $('#resolucion').val(obtenerDocumento(iResp));
        $('#divEditorResolucion').show();
        $('#tblArchivo').show();

        w2ui['gridResolucion'].clear();
        $('#divGridResolucion').show();
        $('#divGridResolucion').w2render('gridResolucion');
        $('#divAnexarArchivos').show();

        $('#lstTipoInfo').empty();
        if ( iResp  === "5" || iResp  === "3" ){
            w2ui['gridResolucion'].add({ recid: 1, titulo: 'Art. 7', valor: 'NO' });
            w2ui['gridResolucion'].add({ recid: 2, titulo: 'Clasificación', valor: ' ' });
            w2ui['gridResolucion'].add({ recid: 3, titulo: 'Modalidad', valor: '1' });
            w2ui['gridResolucion'].add({ recid: 4, titulo: 'Capacidad', valor: ' ' });
            w2ui['gridResolucion'].add({ recid: 5, titulo: 'Ubicación', valor: ' ' });
            $('#lstTipoInfo').append($('<option>', { value: 1, text: 'Pública sin costo' }));
            SelectAddElemClear('lstModoEntrega', _ModoEntrega, false);

            // NEGATIVA
        }else if ( $('#lstTipoRespuesta').val()  === "7"){
            w2ui['gridResolucion'].add({ recid: 1, titulo: 'Art. 7', valor: 'NO' });
            w2ui['gridResolucion'].add({ recid: 2, titulo: 'Clasificación', valor: ' ' });
            w2ui['gridResolucion'].add({ recid: 3, titulo: 'Modalidad', valor: ' ' });
            w2ui['gridResolucion'].add({ recid: 5, titulo: 'Ubicación', valor: ' ' });
            $('#lstTipoInfo').append($('<option>', { value: 4, text: 'Reservada' }));
            $('#lstTipoInfo').append($('<option>', { value: 5, text: 'Confidencial' }));
            SelectAddElemClear('lstModoEntrega', _ModoEntrega,false);


        }else if ( $('#lstTipoRespuesta').val()  === "2"){
            w2ui['gridResolucion'].add({ recid: 1, titulo: 'Art. 7', valor: 'NO' });
            w2ui['gridResolucion'].add({ recid: 2, titulo: 'Clasificación', valor: ' ' });
            $('#lstTipoInfo').append($('<option>', { value: 1, text: 'Pública sin costo' }));

        }else if ( $('#lstTipoRespuesta').val()  === "11"){
            w2ui['gridResolucion'].add({ recid: 1, titulo: 'Art. 7', valor: 'NO' });
            w2ui['gridResolucion'].add({ recid: 2, titulo: 'Clasificación', valor: ' ' });
            $('#lstTipoInfo').append($('<option>', { value: 6, text: 'Parcialmente reservada' }));
            $('#lstTipoInfo').append($('<option>', { value: 7, text: 'Parcialmente confidencial' }));

        }else if ( $('#lstTipoRespuesta').val()  === "10"){
            w2ui['gridResolucion'].add({ recid: 1, titulo: 'Art. 7', valor: 'NO' });
            w2ui['gridResolucion'].add({ recid: 2, titulo: 'Clasificación', valor: ' ' });
            $('#lstTipoInfo').append($('<option>', { value: 1, text: 'Pública sin costo' }));
            $('#lstTipoInfo').append($('<option>', { value: 2, text: 'Pública con costo' }));
            $('#lstTipoInfo').append($('<option>', { value: 3, text: 'Pública con valor comercial' }));

        }else if ( $('#lstTipoRespuesta').val()  === "6"){
            w2ui['gridResolucion'].add({ recid: 1, titulo: 'Art. 7', valor: 'NO' });
            w2ui['gridResolucion'].add({ recid: 2, titulo: 'Clasificación', valor: ' ' });
            $('#lstTipoInfo').append($('<option>', { value: 2, text: 'Pública con costo' }));
        }
    });
}

function EventoslstTipoRespuestaUT()
{
    $("#lstTipoRespuesta").change(function () {
        var iResp = $('#lstTipoRespuesta').val();


        $('#lstTipoInfo').empty();
        if (iResp === "5" || iResp === "3") {
            $('#lstTipoInfo').append($('<option>', { value: 1, text: 'Pública sin costo' }));

            // NEGATIVA
        } else if ($('#lstTipoRespuesta').val() === "7") {
            $('#lstTipoInfo').append($('<option>', { value: 4, text: 'Reservada' }));
            $('#lstTipoInfo').append($('<option>', { value: 5, text: 'Confidencial' }));

        } else if ($('#lstTipoRespuesta').val() === "2") {
            $('#lstTipoInfo').append($('<option>', { value: 1, text: 'Pública sin costo' }));

        } else if ($('#lstTipoRespuesta').val() === "11") {
            $('#lstTipoInfo').append($('<option>', { value: 6, text: 'Parcialmente reservada' }));
            $('#lstTipoInfo').append($('<option>', { value: 7, text: 'Parcialmente confidencial' }));

        } else if ($('#lstTipoRespuesta').val() === "10") {
            $('#lstTipoInfo').append($('<option>', { value: 1, text: 'Pública sin costo' }));
            $('#lstTipoInfo').append($('<option>', { value: 2, text: 'Pública con costo' }));
            $('#lstTipoInfo').append($('<option>', { value: 3, text: 'Pública con valor comercial' }));

        } else if ($('#lstTipoRespuesta').val() === "6") {
            $('#lstTipoInfo').append($('<option>', { value: 2, text: 'Pública con costo' }));

        } else if ($('#lstTipoRespuesta').val() === "8") {
            $('#lstTipoInfo').append($('<option>', { value: 0, text: 'N/A' }));
        }
    });
}

function ckEditorDefinir(sHeight) {
    /* EDITOR */
    CKEDITOR.config.height = sHeight;
    CKEDITOR.replace('txtCkEditor', {
        toolbar: [
            ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
            ['Undo', 'Redo'],
            ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
            ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
            ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Maximize'],
            ['Table', 'HorizontalRule', 'SpecialChar'],
            ['Styles', 'BGColor']
        ]
    });
}

// FALTA UNA FUNCION PARA LIMPIAR LOS CAMPOS.....
function actualizarDatos() {
    $('#folio').val(_solclave);
    $('#nodo').val(_nodclave);
    ///$('#seguimiento').val(_Seguimiento);

    if ($('#lstTipoInfo').val() === undefined)
        $('#tipoInfo').val(0);
    else
        $('#tipoInfo').val($('#lstTipoInfo').val());

    if ($('#lstModoEntrega').val() == undefined)
        $('#modoEntrega').val(0);
    else
        $('#modoEntrega').val($('#lstModoEntrega').val());


    // dependiendo de como esten los botones yo debo de generar una respuesta.........
    if (document.getElementById("radCompetencia2").checked === true)
        $('#tipoArista').val(_RespNoCompeteciaUE);
    else if (document.getElementById("radAclaracion1").checked === true)
        $('#tipoArista').val(_RespInfoAdicional);
    else if (document.getElementById("radExiste2").checked === true)
        $('#tipoArista').val(_RespInexistente);
    else if (document.getElementById("radAmpliacion1").checked === true)
        $('#tipoArista').val(_RespProrroga);
    else
        $('#tipoArista').val($('#lstTipoRespuesta').val());
}


/* FUNCIONES PARA COLOCARLOS EN OTRO ARCHIVO */
function reemplazarDocumento(folio, fecha, descripcion)
{
    var iRecorre = 0;
    var iTotal = _Documento.length;
    var dHoy = new Date();

    for (iRecorre = 0; iRecorre < iTotal; iRecorre++)
    {
        if ( _Documento[iRecorre].rtpformato !== null)
        {
            _Documento[iRecorre].rtpformato = _Documento[iRecorre].rtpformato.replace("|Folio|", _solclave  );
            _Documento[iRecorre].rtpformato = _Documento[iRecorre].rtpformato.replace("|Solicitud|", "\""+ _Solicitud + "\"");
            _Documento[iRecorre].rtpformato = _Documento[iRecorre].rtpformato.replace("|Fecha|", dHoy.getDate() + '/'+ (dHoy.getMonth()+1) +'/'+ dHoy.getFullYear() );
        }
    }
}

function obtenerDocumento(iIdx)
{
    var iRecorre = 0;
    var iTotal = _Documento.length;

    if (iIdx === '')
        return '';
    else
    {
        for (iRecorre = 0; iRecorre < iTotal; iRecorre++)
        {
            if ( _Documento[iRecorre].rtpclave === iIdx)
            {
                return _Documento[iRecorre].rtpformato;
            }
        }
        return '';
    }
}

function AristaClick(elementos, iIdxValor) {
    for (var iIdx = 0; iIdx < elementos.records.length; iIdx++) {
        if (elementos.records[iIdx].id === iIdxValor) {
            return elementos.records[iIdx].forma;
        }
    }
    return "";
}

