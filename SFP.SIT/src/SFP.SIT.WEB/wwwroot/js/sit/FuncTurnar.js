function InicializarTurnar(pHeight, pWidth, pDivTurnarHeight)
{
    //Cambiar la altura del GRID
    $('#divTurnar').height(pDivTurnarHeight);

    $('#btnBorrarArea').click(function () {
        w2ui['gridTurnar'].remove(w2ui['gridTurnar'].getSelection());
    });

    if (w2ui.gridTurnar === undefined)
    {
        $('#divGridTurnar').w2grid(GetGridTurnar());
    }        
    else
    {
        w2ui['gridTurnar'].destroy();
        $('#divGridTurnar').w2grid(GetGridTurnar());
    }
        
            
    w2ui.gridTurnar.hideColumn('recid', 'PERCLAVE', 'ARACLAVE', 'usrclave');

    EventosTurnar("agregarAreas();");
    $("#divGridTurnar").height(pHeight);
    $("#divGridTurnar").width(pWidth);
}

function EventosTurnar(nombreFuncion) {

    $("#btnPara").click(function () {

        $("#tdParaEjecutar").children().each(function () {
            var element = $(this).detach();
            $('#tdParaAreas').append(element);
            w2alert(element);
        });


        w2popup.open({
            title: "Seleccionar área a turnar",
            showMax: true,
            modal: true,
            height: 650,
            width: 1060,
            body: '<div rel="body">'  +
                '<p><label>Instrucciones :</label></p>' +
                '<div><textarea id="tarInstruccion" name="tarInstruccion" type="text" style="width: 100%; height: 70px; resize: none;"></textarea></div>' +
                '<div id="gridArea" style="width: 100%; height: 440px;"></div>' +
                '</div>',
            buttons: '<button class="btn" onclick="' + nombreFuncion + '">Aceptar</button> ' +
                        '<button class="btn" onclick="w2popup.close();">Cerrar</button> ',
            onOpen: function (event) {
                event.onComplete = function () {

                    //var element = $("#dialog-form").detach();
                    //$('#w2ui-popup .w2ui-msg-body').append(element);
                    
                    var Grid = $().w2grid(GetGridArea());
                    w2ui['gridArea'].hideColumn('recid', 'PERCLAVE', 'ARACLAVE', 'usrclave');
                   
                    for (var iIdx = 0; iIdx < _AreaTurnar.records.length; iIdx++)
                    {
                        var oReg = _AreaTurnar.records[iIdx];
                        w2ui.gridArea.add({ recid: iIdx, PERCLAVE: oReg.PERCLAVE, ARACLAVE: oReg.ARACLAVE, USRCLAVE: oReg.USRCLAVE, ARHSIGLAS: oReg.ARHSIGLAS, NOMBRE: oReg.NOMBRE, USRPUESTO: oReg.USRPUESTO, ARHDESCRIPCION: oReg.ARHDESCRIPCION, PERSIGLA: oReg.PERSIGLA })
                    }

                    Grid.box = $("#w2ui-popup #gridArea");
                    Grid.render();
                    $("#dialog-form").show();
                };
            },

            onClose: function (event) {
                ////var element = $("#dialog-form").detach();
                ////$("#Aux").append(element);
                _instruccion = $("#tarInstruccion").val()
                ///$("#txtInstruccion").val(  );

                w2ui['gridArea'].destroy();
                ////$("#dialog-form").hide();
            }
        });
    });
}


function ExisteArea(claveArea) {
    for (var idxAreaAux = 0; idxAreaAux < w2ui['gridTurnar'].records.length; idxAreaAux++) {
        if (w2ui['gridTurnar'].records[idxAreaAux].AreasID === claveArea)
        {
            return 1;
        }
    }
    return 0;
}

function agregarAreas() {
    var sTexto = $('#tarInstruccion').val();
    var sArea = "";

    if (sTexto.length === 0) {
        w2alert("Es necesario que redactar una instrucción ");
        return;
    }

    if (w2ui['gridArea'].getSelection().length === 0) {
        w2alert("Es necesario seleccionar un área a turnar");
        return;
    }

    var aAreas = w2ui['gridArea'].getSelection();
    var datosHTML = "";
    var datosArea = "";
    for (var idxArea = 0; idxArea < aAreas.length; idxArea++) {

        if ( ExisteArea( w2ui['gridArea'].get(aAreas[idxArea]).araclave ) === 0)
        {
            datosHTML = datosHTML + ',' + w2ui['gridArea'].get(aAreas[idxArea]).NOMBRE;
            datosArea = datosArea + ',' + w2ui['gridArea'].get(aAreas[idxArea]).USRCLAVE + '|' + w2ui['gridArea'].get(aAreas[idxArea]).ARACLAVE;
        }
    }

    var iMax = 0;
    for (var idxAreaAux = 0; idxAreaAux < w2ui['gridTurnar'].records.length; idxAreaAux++) {
        if (w2ui['gridTurnar'].records[idxAreaAux].recid > iMax)
            iMax = w2ui['gridTurnar'].records[idxAreaAux].recid;
    }
    
    w2ui['gridTurnar'].add({ recid: iMax + 1, Instruccion: sTexto, Areas: datosHTML.substring(1), AreasID: datosArea.substring(1) });
    w2popup.close();
}

function agregarAreasRevision() {
    var sArea = "";

    if (w2ui['gridArea'].getSelection().length === 0) {
        w2alert("Es necesario seleccionar un área a turnar");
        return;
    }

    var aAreas = w2ui['gridArea'].getSelection();
    var datosHTML = "";
    var datosArea = "";
    for (var idxArea = 0; idxArea < aAreas.length; idxArea++) {
        datosHTML = datosHTML + ',' + w2ui['gridArea'].get(aAreas[idxArea]).KA_DESCRIPCION;
        datosArea = datosArea + ',' + w2ui['gridArea'].get(aAreas[idxArea]).araclave;
    }

    var iMax = 0;
    for (var idxAreaAux = 0; idxAreaAux < w2ui['gridTurnar'].records.length; idxAreaAux++) {
        if (w2ui['gridTurnar'].records[idxAreaAux].recid > iMax)
            iMax = w2ui['gridTurnar'].records[idxAreaAux].recid;
    }

    w2ui['gridTurnar'].add({ recid: iMax + 1, Instruccion: "", Areas: datosHTML.substring(1), AreasID: datosArea.substring(1) });

    w2popup.close();
}