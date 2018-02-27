var _renderer;
var _stage;


function PixieSitIni(sNombreDiv)
{
    _renderer = PIXI.autoDetectRenderer(1200, 780, { backgroundColor: 0xeeeeee });
    document.getElementById(sNombreDiv).appendChild(_renderer.view);

    //// create the root of the scene graph
    _stage = new PIXI.Container();
    animate();
}


function popupDiagramaFlujo(lFolio) {
    for (var i = _stage.children.length - 1; i >= 0; i--) {
        _stage.removeChild(_stage.children[i]);
    };

    w2popup.open({
        title: 'Diagrama de flujo de la solicitud :' + lFolio,
        width: screen.width - 200,
        height: screen.height - 150,
        body: '<div id="ContenedorFlujo"> </div> ',
        onOpen: function (event) {
            event.onComplete = function () {
                DibujarFlujo(lFolio);
                $("#ContenedorRespuestas").css("width", $("#w2ui-popup").width()-35);
                $("#divFlujoSolicitud").appendTo("#ContenedorFlujo");
                $("#ContenedorRespuestas").appendTo("#ContenedorFlujo");
                $("#divFlujoSolicitud").show();
            };
        },
        onClose: function (event) {
            $("#divFlujoSolicitud").appendTo("body");
            $("#divFlujoSolicitud").hide();
            $("#ContenedorRespuestas").appendTo("body");
            $("#ContenedorRespuestas").hide();
        }
    });
}

function animate() {
    requestAnimationFrame(animate);
    // render the root container
    _renderer.render(_stage);
}

function DibujarFlujo(lFolio) {
    $.ajax({
        url: 'BandejaFlujo?txtFolio=' + lFolio,
        type: "POST",
        success: function (data) {
            if (data === undefined) {
                w2alert("No existen datos para esta solicitud");
            } else {
                _datos = data;
                var w = _datos.Coordenadas[0].y * 100 * 2;
                var h = (_datos.Coordenadas[0].x * 100) + 115;

                _renderer.view.style.width = w + "px";
                _renderer.view.style.height = h + "px";
                //this part adjusts the ratio:
                _renderer.resize(w, h);
                $("#ContenedorFlujo").height(h);
                $("#ContenedorFlujo").width(w);

                AgregarCuadros(_stage, _datos.Areas, _datos.Aristas); 

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status === 401) {
                w2alert("La sesión de usuario no existe, se redirecciona a la página principal").done(function () {
                    w2popup.close();
                });
                
            }
            else {
                w2alert("Existe un error en el sistema, favor de avisar al área de la DGTI");
            }            
        }                
    });
}

function AgregarCuadros(stage, aoAreas, aoAristas) {
    if (aoAreas === undefined)
        return null;

    var mapAreas = {};
    var iPosX = 0;
    var iPosY = 0;
    var iTranslacion = 50;
    var iLargo = 100;
    var iAncho = 100;
    var iEspacio = 210;
    var iEspacioY = 110;
    var iTotal = 0;
    var graphics = new PIXI.Graphics();

    for (iTotal = 0 ; iTotal < aoAreas.length; iTotal++) {
        

        // draw a rounded rectangle
        graphics.lineStyle(2, 0x000066, 1);

        mapAreas[aoAreas[iTotal].id] = aoAreas[iTotal];

        iPosX = (aoAreas[iTotal].col * iEspacio) + iTranslacion;
        iPosY = (aoAreas[iTotal].ren * iEspacioY) + iTranslacion;

        if (aoAreas[iTotal].atendido === 1)
            graphics.beginFill(0xCECEF6, 0.25);
        else
            graphics.beginFill(0x008000, 0.25);

        graphics.interactive = true;
        graphics.drawRoundedRect(iPosX, iPosY, iLargo, 70, 15);        
        graphics.endFill();

        ///nodoEstado
        // AGREGAMOS EL TEXT EN EL AREA
        
        var richText = new PIXI.Text(aoAreas[iTotal].nodoEstado, style);
        richText.x = iPosX + 5;
        richText.y = iPosY + 12;
        stage.addChild(richText);

        var txtSigla = new PIXI.Text(aoAreas[iTotal].sigla, style);
        txtSigla.x = iPosX + 5;
        txtSigla.y = iPosY + 44;
        stage.addChild(txtSigla);

        var fechaText = new PIXI.Text(aoAreas[iTotal].fecha, style);
        fechaText.x = iPosX + 5;
        fechaText.y = iPosY - 17;
        stage.addChild(fechaText);

        //////var NodoID = new PIXI.Text(aoAreas[iTotal].id, styleFont);
        //////NodoID.x = iPosX - 15;
        //////NodoID.y = iPosY + 70;
        //////stage.addChild(NodoID);

        stage.addChild(graphics);
    }

    /*  origen , destino, accion, dias, fecini, fecfin, observacion, responsable    */
    // DIBUJAMOS LAS ARISTAS
    var oOrigen, oDestino;
    graphics.lineStyle(2, 0x000055);
    var oOrigenPosX = 0, oOrigenPosY = 0, oDestinoPosX = 0, oDestinoPosY = 0;

    for (iTotal = 0 ; iTotal < aoAristas.length; iTotal++) {
        //////var graphics = new PIXI.Graphics();
        // draw a rounded rectangle
        graphics.lineStyle(2, 0x000066, 1);
        oOrigen = mapAreas[aoAristas[iTotal].origen];
        oOrigenPosX = (oOrigen.col * iEspacio) + iLargo + iTranslacion;
        oOrigenPosY = (oOrigen.ren * iEspacioY) + 85;

        graphics.moveTo(oOrigenPosX, oOrigenPosY);

        oDestino = mapAreas[aoAristas[iTotal].destino];
        oDestinoPosX = (oDestino.col * iEspacio) + 50;
        oDestinoPosY = (oDestino.ren * iEspacioY) + 85;

        if (oOrigenPosY === oDestinoPosY) {
            if (oOrigenPosX < oDestinoPosX) {
                graphics.lineTo(oDestinoPosX, oDestinoPosY);
                dibujarAccion(stage, aoAristas[iTotal].accion, oDestinoPosX - 103, oDestinoPosY - 27, aoAristas[iTotal].observacion);
                //dibujarDocumento(stage, aoAristas[iTotal].documento, oDestinoPosX - 90, oDestinoPosY - 5);
                dibujarDocumento(stage, "1,2", oDestinoPosX - 90, oDestinoPosY - 5);
            }
            else {
                graphics.lineTo(oOrigenPosX + 30, oOrigenPosY);
                graphics.lineTo(oOrigenPosX + 30, oOrigenPosY - 60);
                graphics.lineTo(oDestinoPosX - 50, oOrigenPosY - 60);
                graphics.lineTo(oDestinoPosX - 50, oDestinoPosY);
                graphics.lineTo(oDestinoPosX, oDestinoPosY);
                dibujarAccion(stage, aoAristas[iTotal].accion, oOrigenPosX + 10, oOrigenPosY + 10, aoAristas[iTotal].observacion);
                ////dibujarDocumento(stage, aoAristas[iTotal].documento, oOrigenPosX + 5, oOrigenPosY + 20);
            }
        }
        else {
            if (oOrigenPosX < oDestinoPosX) {
                // linea recta..
                graphics.lineTo(oDestinoPosX - 50, oOrigenPosY);
                graphics.lineTo(oDestinoPosX - 50, oDestinoPosY);
                graphics.lineTo(oDestinoPosX, oDestinoPosY);
                dibujarAccion(stage, aoAristas[iTotal].accion, oDestinoPosX - 95, oDestinoPosY + 10, aoAristas[iTotal].observacion);
                ////dibujarDocumento(stage, aoAristas[iTotal].documento, oDestinoPosX - 30, oDestinoPosY + 80);
            }
            else {
                graphics.lineTo(oOrigenPosX + 30, oOrigenPosY);
                graphics.lineTo(oOrigenPosX + 30, oOrigenPosY - 60);
                graphics.lineTo(oDestinoPosX - 50, oOrigenPosY - 60);
                graphics.lineTo(oDestinoPosX - 50, oDestinoPosY);
                graphics.lineTo(oDestinoPosX, oDestinoPosY);
                dibujarAccion(stage, aoAristas[iTotal].accion, oOrigenPosX + 10, oOrigenPosY + 10, aoAristas[iTotal].observacion);
                ////dibujarDocumento(stage, aoAristas[iTotal].documento, oOrigenPosX + 10, oOrigenPosY - 20);
            }
        }
        // Dibujamos la flecha
        graphics.beginFill(0x000000); // Negro
        // Draw a polygon to look like a star
        graphics.drawPolygon([oDestinoPosX - 1, oDestinoPosY - 1, oDestinoPosX - 6, oDestinoPosY - 6, oDestinoPosX - 6, oDestinoPosY + 7, oDestinoPosX - 1, oDestinoPosY - 1]);
        graphics.endFill();
        stage.addChild(graphics);
    }
}

function onDownTexto(eventData) {
    this.data = eventData
    w2alert(this.mensaje);
}

function dibujarAccion(stage, texto, PosX, PosY, observacion) {
    var accionText = new PIXI.Text(texto, style);
    accionText.interactive = true;
    accionText.x = PosX;
    accionText.y = PosY;
    accionText.mensaje = observacion;
    accionText.on('mousedown', onDownTexto);
    stage.addChild(accionText);
}

function dibujarDocumento(stage, documentoID, PosX, PosY) {
    if (documentoID !== "") {
        PosX = PosX - 15;
        PosY = PosY - 42;
        var aDoc = documentoID.split(",");

        for (i = 0; i < aDoc.length; i++) {
            
            var documento = new PIXI.Sprite.fromImage(_PathImgDoc);
            //var documento = new PIXI.Sprite.fromImage('/images/documento.png');
            documento.position.set(PosX + (19 * i), PosY);
            documento.interactive = true;
            documento.Idx = aDoc[i];
            documento.on('mousedown', showTableFiles);
            stage.addChild(documento);
        }
    }
}

function showTableFiles(eventData) {
    if (w2ui['gridRespuestasFlujo'] != undefined)
        w2ui['gridRespuestasFlujo'].clear();

    $("#ContenedorRespuestas").show();
    /* Ver documentos de las respuestas */
    var pstyle = 'background-color: #F5F6F7; border: 1px solid #dfdfdf; padding: 5px;';
    $('#ContenedorRespuestas').w2layout({
        name: 'layoutContenedorRespuestas',
        panels: [
            { type: 'main', size: 100, resizable: true, style: pstyle, content: 'main' }
        ]
    }); 


    var objetos = {
        gridRespuestasFlujo: {
            name: 'gridRespuestasFlujo',
            header: 'Archivos de Respuestas',
            show: {
                header: true,
                toolbar: false,
                footer: false,
                lineNumbers: false,
                selectColumn: false,
                expandColumn: true
            },
            columns: [
                { field: 'siglas', caption: 'Documento', size: '20%', attr: 'align=left' },
                { field: 'area', caption: 'Descripción', size: '55%', attr: 'align=left' },
                { field: 'doc', caption: 'Ver', size: '20%', attr: 'align=left' }
            ],
            onExpand: function (event) {
                $('#' + event.box_id).html(oAreaData[event.recid]).animate({ 'height': 100 }, 100);
            }
        }
    };

    $('#layoutContenedorRespuestas').w2grid(objetos.gridRespuestasFlujo);
    w2ui['layoutContenedorRespuestas'].content('main', w2ui['gridRespuestasFlujo']);


    this.data = eventData
    //window.open("Documento?id=" + this.Idx);
    var documentUrl = this.Idx;
    var data = [{}, {}, { records: [{ docnombre: "prueba1", clave: "clave1", docclave: "clavedoc1" }, { docnombre: "prueba1", clave: "clave1", docclave: "clavedoc1" }] }];
    if (data[2].records.length > 0) {
        for (iIndex = 0; iIndex < data[2].records.length; iIndex++) {
            w2ui['gridRespuestasFlujo'].add([{ recid: 756, siglas: data[2].records[iIndex].docnombre, area: data[2].records[iIndex].docclave, doc: '<a href="Documento?id="'+ '1' +'" download="Respuesta.docx">Respuesta.docx</a>'   }]);
        }
    }

}


function onDown(eventData) {
    this.data = eventData
    window.open("Documento?id=" + this.Idx);
}