var pstyle = 'background-color: #F5F6F7; border: 1px solid #dfdfdf; padding: 5px;';

////function GetLayoutEstadoActual(leftSize, mainSize) {
////    
////    var layoutEstadoActual = {
////        name: 'layoutEstadoActual',
////        panels: [
////            { type: 'top', size: '34px', resizable: false, style: pstyle, content: 'top' },
////            { type: 'left', size: leftSize, resizable: true, style: pstyle, content: 'main' },
////            { type: 'main', size: mainSize, resizable: false, style: pstyle, content: 'right' },
////            { type: 'preview', size: '93%', resizable: false, style: pstyle }
////        ]
////    };
////    return layoutEstadoActual;
////}

function GetLayoutMain(leftSize, mainSize) {
    var layoutMain = {
        name: 'layoutMain',
        panels: [
            { type: 'top', size: '35px', resizable: false, style: pstyle, content: 'top' },
            { type: 'left', size: leftSize, resizable: true, style: pstyle, content: 'left' },
            { type: 'main', size: mainSize, resizable: true, style: pstyle, content: ' ' }
        ]
    };
    return layoutMain;
}

function GetLayoutLeftMain(left, main) {
    var layoutLeftMain = {
        name: 'layoutLeftMain',
        panels: [
            { type: 'left', size: left, resizable: true, style: pstyle, content: 'left' },
            { type: 'main', size: main, resizable: true, style: pstyle, content: 'main' }
        ]
    };
    return layoutLeftMain;
}
         
function GetLayoutTopMain(top, main) {
    var layoutTopMain = {
        name: 'layoutTopMain',
        panels: [
            { type: 'top', size: top, resizable: false, style: pstyle },
            { type: 'main', size: main, resizable: false, style: pstyle }
        ]
    };
    return layoutTopMain;
}


/* **************************************
        TAB UNICO DE INFORMACION GENERAL
************************************** */

function GetTabInformacion() {    
    var tabInformacion = {
        name: 'tabInformacion',
        items: [
            { type: 'radio', id: 'solicitud', group: '1', caption: 'Solicitud', checked: true },
            { type: 'radio', id: 'solicitante', group: '1', caption: 'Solicitante' },
            { type: 'radio', id: 'aclaracion', group: '1', caption: 'Aclaración' },
            { type: 'radio', id: 'recrevision', group: '1', caption: 'Recurso' }
        ],
        onClick: function (event) {
            switch (event.target) {
                case 'solicitud':
                    $('#divGrid').w2render('gridSolicitud');
                    break;

                case 'solicitante':
                    $('#divGrid').w2render('gridSolicitante');
                    break;

                case 'aclaracion':
                    $('#divGrid').w2render('gridAclaracion');
                    break;

                case 'recrevision':
                    $('#divGrid').w2render('gridRecRevision');
                    break;
            }
        }
    };
    return tabInformacion;
}


/* **************************************
        GRID
************************************** */
function GetGridTurnar() {
    var gridTurnar = {
        name: 'gridTurnar',
        show: {
            lineNumbers: true,
            selectColumn: true,
            multiSelect: true
        },
        columns: [
            { field: 'recid', caption: 'RecID', size: '60px', sortable: true },
            { field: 'Instruccion', caption: 'INSTRUCCIÓN', size: '500px', attr: "align=left" },
            {
                field: 'Areas', caption: 'TURNAR', size: '400px',
                render: function (registro) {

                    var datosHTML = "";

                    var res = registro.Areas;
                    res = res.split(",");

                    for (var idxArea = 0; idxArea < res.length; idxArea++) {
                        datosHTML = datosHTML + '<div style="float:left; font-weight: bold;  font-style: italic;  text-decoration: underline; display: inline;">'
                            + res[idxArea] + "; </div> ";
                    }

                    return datosHTML;
                }
            },
            { field: 'AreasID', caption: 'AREAS ID', size: '1px', hidden: true }
        ]
    };

    return gridTurnar;
}

function GetGridArea() {
    var gridArea = {
        name: 'gridArea',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            lineNumbers: true,
            toolbar: true,
            selectColumn: true,
            multiSelect: true
        },
        columns: [
            { field: 'PERCLAVE', caption: 'PERCLAVE', size: '0px', attr: "align=left" },
            { field: 'ARACLAVE', caption: 'ARACLAVE', size: '0px', attr: "align=left" },
            { field: 'usrclave', caption: 'usrclave', size: '0px', attr: "align=left" },
            { field: 'PERSIGLA', caption: 'PERFIL', size: '60px', attr: "align=left" },
            { field: 'ARHSIGLAS', caption: 'SIGLAS', size: '80px', attr: "align=left" },
            { field: 'NOMBRE', caption: 'NOMBRE', size: '280px' },
            { field: 'usrpuesto', caption: 'PUESTO', size: '280px' },
            { field: 'ARHDESCRIPCION', caption: 'ÁREA', size: '280px', attr: "align=left" }            
        ]
    };

    return gridArea;
}



function GetGridSolicitud() {
    var gridSolicitud = {
        name: 'gridSolicitud',
        method: 'GET', // need this to avoid 412 error on Safari
        show: { columnHeaders: false },
        columns: [
            { field: 'titulo', caption: 'name', size: '40%', style: 'background-color: #efefef; border-bottom: 1px solid white; padding-right: 5px;', attr: "align=left" },
            {
                field: 'valor', caption: 'value', size: '60%',
                render: function (registro) {
                    var datosHTML = '';
                    var iLongitud = 0;
                    var iRen = 0;

                    if (registro.titulo === 'Semáforo') {
                        if (registro.valor === '3') {
                            datosHTML = '<div class="semaforo_rojo" />';
                        } else if (registro.valor === '2') {
                            datosHTML = '<div class="semaforo_anarillo" />';
                        } else if (registro.valor === '1') {
                            datosHTML = '<div class="semaforo_verde" />';
                        }
                    } else if (registro.titulo === 'Otros datos') {
                        iLongitud = registro.valor.length;
                        if (iLongitud > 42) {
                            iRen = parseInt(iLongitud / 42) + 4;
                            datosHTML = '<textarea rows=' + iRen + ' style="width:100%; resize: none; background-color: white; color:black;" readonly  > ' + registro.valor + '</textarea>';
                        } else {
                            datosHTML = registro.valor;
                        }

                        if (_Solicitud.length > 0)
                            _Solicitud = _Solicitud + datosHTML;
                        else
                            _Solicitud = datosHTML;

                    } else if (registro.titulo === 'Descripción') {
                        iLongitud = registro.valor.length;

                        if (iLongitud > 42) {
                            iRen = parseInt(iLongitud / 42) + 4;
                            datosHTML = '<textarea rows=' + iRen + ' style="width:100%; resize: none; background-color: white; color:black;" readonly  > ' + registro.valor + '</textarea>';
                        } else {
                            datosHTML = registro.valor;
                        }

                        if (_Solicitud.length > 0)
                            _Solicitud = _Solicitud + datosHTML;
                        else
                            _Solicitud = datosHTML;
                    } else {
                        datosHTML = registro.valor;
                    }
                    return datosHTML;
                }
            }
        ],
        onClick: function (event) {
            record = this.get(event.recid);
            if (record.titulo === "Anexo") {
                var oDocumento = record.valor.split("_");
                window.open("/sit/Solicitud/Documento?id=" + oDocumento[0]);
            }
        }
    };
    return gridSolicitud;
}

function GetGridSolicitante() {
    var gridSolicitante = {
        name: 'gridSolicitante',
        method: 'GET', // need this to avoid 412 error on Safari
        show: { columnHeaders: false },
        columns: [
            { field: 'titulo', caption: 'name', size: '40%', style: 'background-color: #efefef; border-bottom: 1px solid white; padding-right: 5px;', attr: "align=left" },
            {
                field: 'valor', caption: 'value', size: '65%',
                render: function (record) {
                    if (record.valor === undefined)
                        return '<div> </div>';
                    else
                        return '<div>' + record.valor + '</div>';
                }
            }
        ]
    };
    return gridSolicitante;
}

function GetGridAclaracion() {
    var gridAclaracion = {
        name: 'gridAclaracion',
        method: 'GET', // need this to avoid 412 error on Safari
        show: { columnHeaders: false },
        columns: [
            { field: 'titulo', caption: 'name', size: '30%', style: 'background-color: #efefef; border-bottom: 1px solid white; padding-right: 5px;', attr: "align=left" },
            {
                field: 'valor', caption: 'value', size: '70%',
                render: function (registro) {
                    var datosHTML = '';

                    if (registro.titulo === 'Info. adicional') {
                        var iLongitud = registro.valor.length;
                        if (iLongitud > 20) {
                            var iRen = parseInt(iLongitud / 20) + 8;
                            datosHTML = '<textarea rows=' + iRen + ' style="width:100%; resize: none; background-color: white; color:black;" readonly  > ' + registro.valor + '</textarea>';
                        } else {
                            datosHTML = registro.valor;
                        }
                    } else {
                        datosHTML = registro.valor;
                    }

                    return datosHTML;
                }
            }
        ]
    };
    return gridAclaracion;
}

function GetGridRecRevision() {
    var gridRecRevision = {
        name: 'gridRecRevision',
        method: 'GET', // need this to avoid 412 error on Safari
        show: { columnHeaders: false },
        columns: [
            { field: 'titulo', caption: 'name', size: '40%', style: 'background-color: #efefef; border-bottom: 1px solid white; padding-right: 5px;', attr: "align=left" },
            { field: 'valor', caption: 'value', size: '60%',
                render: function (record) {
                    var sDatoTitulo = record.titulo;
                    var sDatoValor = record.valor;
                    var iLongitud;
                    var iRen;
                    var iIdx;

                    if (sDatoTitulo.indexOf("ocumento") > 0) {
                        iIdx = sDatoValor.indexOf("_");
                        if (iIdx > 0)
                        {
                            datosHTML = '<div class="documento"  style="float:left" onclick="window.open(\'Documento?id=' + sDatoValor.substring(0, iIdx) + '\');"></div> <label style="float:left" >' + sDatoValor.substring(iIdx + 1) + '</label>';
                        }
                    } else if (sDatoTitulo === "Acto") {
                        iLongitud = record.valor.length;

                        if (iLongitud > 42) {
                            iRen = parseInt(iLongitud / 42) + 4;
                            datosHTML = '<textarea rows=' + iRen + ' style="width:100%; resize: none; background-color: white; color:black;" readonly  > ' + sDatoValor + '</textarea>';
                        } else {
                            datosHTML = record.valor;
                        }
                    } else {
                        if (sDatoValor === undefined)
                            datosHTML = '<div> </div>';
                        else
                            datosHTML = '<div>' + sDatoValor + '</div>';
                    }
                    
                    return datosHTML;
                }
            }
        ]
    };
    return gridRecRevision;
}


//////function GetGridResolucion() {
//////    var gridResolucion = {
//////        name: 'gridResolucion',
//////        method: 'GET', // need this to avoid 412 error on Safari
//////        show: { columnHeaders: false },
//////        columns: [
//////            { field: 'titulo', caption: 'name', size: '25%', style: 'background-color: #efefef; border-bottom: 1px solid white; padding-right: 5px;', attr: "align=left" },
//////            {
//////                field: 'valor', caption: 'value', size: '75%',
//////                render: function (record) {
//////                    var datosHTML = '';

//////                    if (record.titulo === 'Art. 7')
//////                        datosHTML = ' <select name=\"lstArt7\" id=\"lstArt7\" style=\"height:20px; margin-bottom: 1%;\"> <option >No</option> <option>Si</option> </select> ';
//////                    else if (record.titulo === 'Clasificación')
//////                        datosHTML = ' <select name=\"lstTipoInfo\" id=\"lstTipoInfo\" style=\"height:20px; margin-bottom: 1%;\"> </select> ';
//////                    else if (record.titulo === 'Modalidad')
//////                        datosHTML = ' <select name=\"lstModoEntrega\" id=\"lstModoEntrega\" style=\"height:20px; margin-bottom: 1%;\"> </select> ';
//////                    else if (record.titulo === 'Capacidad')
//////                        datosHTML = ' <input type=\"text\" id=\"tamCantDir\" name=\"tamCantDir\" style=\"width:100%;\">';
//////                    else if (record.titulo === 'Ubicación')
//////                        datosHTML = ' <input type=\"text\" id=\"ubicacion\" name=\"ubicacion\" style=\"width:100%;\">';
//////                    else
//////                        if (record.valor === undefined)
//////                            datosHTML = '<div> </div>';
//////                        else
//////                            datosHTML = '<div>' + record.valor + '</div>';
//////                    return datosHTML;
//////                }
//////            }
//////        ]
//////    };
//////    return gridResolucion;
//////}

//////function GetGridResolucionCI() {
//////    var gridResolucionCI = {
//////        name: 'gridResolucionCI',
//////        header: 'Respuesta del Cómite de Información',
//////        method: 'GET', // need this to avoid 412 error on Safari
//////        show: { columnHeaders: false, header: true },
//////        columns: [
//////            { field: 'titulo', caption: 'name', size: '25%', style: 'background-color: #efefef; border-bottom: 1px solid white; padding-right: 5px;', attr: "align=left" },
//////            {
//////                field: 'valor', caption: 'value', size: '75%',
//////                render: function (record) {
//////                    var datosHTML = '';
//////                    if (record.titulo === 'No. de acta o minuta')
//////                        datosHTML = ' <input type="text" id=\"txtActa\" > ';
//////                    else if (record.titulo === 'Expediente')
//////                        datosHTML = ' <input type="text" id=\"txtExpediente\" > ';
//////                    else if (record.titulo === 'Fec. Resol')
//////                        datosHTML = ' <input type="date" id=\"txtFecResCI\" > ';
//////                    else if (record.titulo === 'Archivo')
//////                        datosHTML = ' <input type="file" id=\"txtArchivoCI\" > ';
//////                    else if (record.titulo === 'Motivo')
//////                        datosHTML = ' <textarea name="txtMotivo" cols="40" rows="5" ></textarea>';
//////                    else if (record.titulo === 'Rubro tématico')
//////                        datosHTML = ' <select name=\"lstRubroCI\" id=\"lstRubroCI\" style=\"height:20px; margin-bottom: 1%;\"> </select> ';
//////                    else if (record.titulo === 'Sentido respuesta')
//////                        datosHTML = ' <select name=\"lstSentidoCI\" id=\"lstSentidoCI\" style=\"height:20px; margin-bottom: 1%; font-size: 12px;\"></select> ';
//////                    else if (record.titulo === 'Modalidad INFOMEX')
//////                        datosHTML = ' <select name=\"lstInfomexCI\" id=\"lstInfomexCI\" style=\"height:20px; margin-bottom: 1%;\"></select> ';
//////                    else
//////                        datosHTML = '<div></div>';
//////                    return datosHTML;
//////                }
//////            }
//////        ]
//////    };
//////    return gridResolucionCI;
//////}
         
//////function GetGridResolucionArea() {
//////    var gridResolucionArea = {
//////        name: 'gridResolucionArea',
//////        header: 'Respuesta del área turnada',
//////        method: 'GET', // need this to avoid 412 error on Safari
//////        show: { columnHeaders: false, header: true },
//////        columns: [
//////            { field: 'titulo', caption: 'name', size: '85px', style: 'background-color: #efefef; border-bottom: 1px solid white; padding-right: 5px;', attr: "align=left" },
//////            { field: 'valor', caption: 'value', size: '80%' }
//////        ]
//////    };
//////    return gridResolucionArea;
//////}

function GetGridRespAreas()
{
    var gridRespAreas =
    {
        name: 'gridRespAreas',
        method: 'GET', // need this to avoid 412 error on Safari
        header: 'Respuestas Unidades administrativas (UAs) ',
        show: {
            header: true,
            lineNumbers: true,
            toolbar: false
        },
        columns: [
            {
                field: 'RESPUESTA', caption: 'RECIBIDO', size: '75px', resizable: false,
                render: function (registro) {
                    var datosHTML = '';
                    if (registro.RESPUESTA === 1) {
                        datosHTML = '<div class="acierto_verde" style="float:left" />';
                    }
                    else
                        datosHTML = '<label style="float:left">Pendiente</label>';

                    if (registro.docclave !== null) {
                        datosHTML = datosHTML + '<div class="documento"  style="float:left" onclick = "abrirDocumento(' + registro.docclave + ')"/>';
                    }

                    return datosHTML;
                }, attr: 'align=center'
            },
            { field: 'ESTADO', caption: 'ESTADO', size: '100px', sortable: true, resizable: true},
            { field: 'FECHA', caption: 'FECHA', size: '120px' },
            { field: 'KA_SIGLA', caption: 'ÁREA ORIGEN', size: '150px' },
            { field: 'rtpdescripcion', caption: 'RESPUESTA', size: '250px' },
            { field: 'arimensaje', caption: 'RESPUESTA', size: '410px' }
        ],
        onClick: function (event) {
            var record = this.get(event.recid);
            gridRespAreasOnClick(record);
        }
    };
    return gridRespAreas;
}
        
function GetGridRespAreasComite() {
    var gridRespAreasComite =
    {
        name: 'gridRespAreasComite',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            lineNumbers: true,
            toolbar: false
        },
        columns: [
            {
                field: 'RESPUESTA', size: '30px', resizable: false,
                render: function (registro) {
                    var datosHTML = '';
                    if (registro.RESPUESTA === 1) {
                        datosHTML = '<div class="acierto_verde" />';
                    }
                    return datosHTML;
                }, attr: 'align=center'
            },
            { field: 'FECHA', caption: 'FECHA', size: '110px' },
            { field: 'KA_SIGLA', caption: 'ÁREA ORIGEN', size: '100px' },
            { field: 'SIGLASDESTINO', caption: 'ÁREA DESTINO', size: '100px' },
            { field: 'rtpclave', caption: 'Tipo Aris', size: '1px' },
            { field: 'rtpdescripcion', caption: 'RESPUESTA', size: '300px' },
            { field: 'nfodescripcion', caption: 'CLASIFICACIÓN', size: '300px' },
            { field: 'nodclave', caption: 'Dato', size: '1px' }

        ],
        onClick: function (event) {
            var record = this.get(event.recid);

            w2ui['gridResolucionArea'].clear();

            w2ui['gridResolucionArea'].add({ recid: 1, titulo: 'Respuesta', valor: record.rtpdescripcion });
            w2ui['gridResolucionArea'].add({ recid: 2, titulo: 'Art. 7', valor: record.RSL_ART7 });
            w2ui['gridResolucionArea'].add({ recid: 3, titulo: 'Clasificación', valor: record.nfodescripcion });
            w2ui['gridResolucionArea'].add({ recid: 4, titulo: 'Modalidad', valor: record.IDFORMAENTREGA });
            w2ui['gridResolucionArea'].add({ recid: 5, titulo: 'Capacidad', valor: record.RSL_TAM_CANT_DIR });
            w2ui['gridResolucionArea'].add({ recid: 6, titulo: 'Ubicación', valor: record.RSL_UBICACION });
            // Documento
            w2ui['gridResolucionArea'].add({ recid: 7, titulo: 'Doc. Fecha', valor: record.docfecha });
            w2ui['gridResolucionArea'].add({ recid: 8, titulo: 'Doc. Folio', valor: record.docfolio });
            w2ui['gridResolucionArea'].add({ recid: 9, titulo: 'Doc. Nombre', valor: record.docnombre });

            $('#txtNodo').text(record.nodclave);

            // En caso de returnar
            $('#lblAreaReturnar').text(record.KA_SIGLA);
        }
    };
    return gridRespAreasComite;
}

function GetGridRespAcl() {
    var gridRespAcl =
    {
        name: 'gridRespAcl',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            lineNumbers: true,
            toolbar: false
        },
        columns: [
            {
                field: 'RESPUESTA', caption: 'RECIBIDO', size: '75px', resizable: false,
                render: function (registro) {
                    var datosHTML = '';
                    if (registro.RESPUESTA === 1) {
                        datosHTML = '<div class="acierto_verde" style="float:left" />';
                    }

                    if (registro.docclave !== null) {
                        datosHTML = datosHTML + '<div class="documento"  style="float:left" onclick = "abrirDocumento(' + registro.docclave + ')"/>';
                    }

                    return datosHTML;
                }, attr: 'align=center'
            },
            { field: 'FECHA', caption: 'FECHA', size: '120px' },
            { field: 'KA_SIGLA', caption: 'ÁREA ORIGEN', size: '100px' },
            { field: 'rtpdescripcion', caption: 'RESPUESTA', size: '300px' }

        ],
        onClick: function (event) {
            var record = this.get(event.recid);
            gridRespAclOnClick(record);
        }
    };
    return gridRespAcl;
}

function GetGridComiteRespCat() {
    var gridComiteRespCat =
    {
        name: 'gridComiteRespCat',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            lineNumbers: true,
            toolbar: true
        },
        columns: [
            { field: 'RCO_CLACOMITERESP', caption: 'ID', size: '50px', resizable: true },
            { field: 'RCO_DESCRIPCION', caption: 'DESCRIPCIÓN', size: '250px', resizable: true }
        ],
        onClick: function (event) {
            $("#txtCmd").val(2);
            var form = w2ui.formComiteResp.record;
            var record = this.get(event.recid);
            form.RCO_CLACOMITERESP = record.RCO_CLACOMITERESP;
            form.RCO_DESCRIPCION = record.RCO_DESCRIPCION;
            w2ui.formComiteResp.refresh();
        }
    };
    return gridComiteRespCat;
}

function GetFormComiteResp() {
    var formComiteResp =
    {
        header: 'Edición comité respuesta',
        name: 'formComiteResp',
        fields: [
            //{name: 'recid',          type: 'text', required: true, html: {caption: 'ID', attr: 'size="10" readonly'}},
            { name: 'RCO_CLACOMITERESP', type: 'text', html: { caption: 'Respuesta ID', attr: 'readonly' } },
            { name: 'RCO_DESCRIPCION', type: 'text', required: true, html: { caption: 'Descripción', attr: 'size="50"' } }
        ],
        actions: {
            Guardar: function () {
                var errors = this.validate();
                var datos = '';
                if (errors.length > 0)
                    return;

                var form = w2ui.formModulo.record;
                var sCmd;

                var iModuloPadre = 0;
                var iNivelModulo = 0;

                if (typeof form.iidmodulopadre === 'object')
                    iModuloPadre = form.iidmodulopadre.id;
                else
                    iModuloPadre = form.iidmodulopadre;

                if (typeof form.iidnivelmodulo === 'object')
                    iNivelModulo = form.iidnivelmodulo.id;
                else
                    iNivelModulo = form.iidnivelmodulo;


                if (form.recid) {
                    sCmd = 'editar';
                    iNodoActivo = form.recid;
                } else {
                    w2ui.formModulo.recid = 0;
                    sCmd = 'agregar';
                    iNodoActivo = iModuloPadre;
                }

                datos = {
                    'iidsistema': form.iidsistema,
                    'iidmodulopadre': iModuloPadre,
                    'iidnivelmodulo': iNivelModulo,
                    'iindexmodulo': form.iindexmodulo,
                    'cnombremodulo': form.cnombremodulo,
                    'cdescripcion': form.cdescripcion,
                    'cicono': form.cicono,
                    'curl': form.curl,
                    'cdatoadicional': form.cdatoadicional,
                    'recid': form.recid
                };

                $.ajax({
                    type: "POST",
                    url: URL,
                    data: {
                        param: JSON.stringify({ 'entidad': 'MODULO', 'oper': sCmd }),
                        json: JSON.stringify(datos)
                    },
                    success: function (data) {
                        if (sCmd === 'guardar' && data !== null) {
                            msg = 'LOS DATOS SE GUARDARON CORRECTAMENTE';
                        } else {
                            msg = data;
                        }
                        if (msg !== undefined && msg !== '') {
                            w2alert(msg);
                        }
                        gridModuloOnclick();
                    }
                });
            }
        }
    };
    return formComiteResp;
}


function GetFormResponderUT() {
    var formResponderUT = {
        name: 'form',
        header: 'Form with Toolbar',
        url: 'server/post',
        fields: [
            { field: 'first_name', type: 'text', required: true },
            { field: 'last_name', type: 'text', required: true },
            { field: 'comments', type: 'text' }
        ],
        toolbar: {
            items: [
                { id: 'bt1', type: 'button', caption: 'Button 1', img: 'icon-folder' },
                { id: 'bt2', type: 'button', caption: 'Button 2', img: 'icon-folder' },
                { id: 'bt3', type: 'spacer' },
                { id: 'bt4', type: 'button', caption: 'Reset', img: 'icon-page' },
                { id: 'bt5', type: 'button', caption: 'Save', img: 'icon-page' }
            ]
        }
    };
}
