/* ********************************************************
            C  A  T  A  L  O  G  O  S 
********************************************************** */

function fechaValidar(nombreObj) {
    var fecha;

    if ($(nombreObj).val() !== "")
        fecha = new Date($(nombreObj).val().substring(0, 4), parseInt($(nombreObj).val().substring(5, 7)) - 1, $(nombreObj).val().substring(8, 10));
    else
        fecha = new Date(1, 1, 1);

    return fecha;
}

function guardarForma( cmd, url, data) {

    var iRes;
    if (cmd === 3) {
        w2confirm('¿Esta usted seguro de borrar este registro?',
            function (btn) {
                if (btn === "Yes") {                    
                    ajaxPost(url, data);

                    w2popup.close();
                } else {
                    w2popup.close();
                }
            });
    } else {
        ajaxPost(url, data);
        w2popup.close();
    }
}

function ajaxPost(url, data) {
    var posting = $.post(url, data, function ( resData ) {
        gridModuloOnclick(_GridCatActual);
        var iRes = parseInt(resData);
        if ( iRes > 0)
            w2alert('La operación se realizó con éxito');
        else
            if (resData != "")
                w2alert(resData);
            else
                w2alert('La operación no pudo ser ejecutada');
    })
    .fail(function () {
        w2alert('La operación no pudo ser ejecutada');
    });

}
 
var oToolBarItems = [
    { id: 'cmdAgregar', type: 'button', caption: 'Agregar', img: 'glyphicon glyphicon-pencil' }, { id: 'break0', type: 'break' },
    { id: 'cmdEditar', type: 'button', caption: 'Editar', img: 'glyphicon glyphicon-edit' }, { id: 'break1', type: 'break' },
    { id: 'cmdBorrar', type: 'button', caption: 'Borrar', img: 'glyphicon glyphicon-remove' }];

var oToolBarItems2 = [
    { id: 'cmdAgregar', type: 'button', caption: 'Agregar', img: 'glyphicon glyphicon-pencil' }, { id: 'break0', type: 'break' },
    { id: 'cmdBorrar', type: 'button', caption: 'Borrar', img: 'glyphicon glyphicon-remove' }];

var oToolBarTreeItems = [
    { id: 'cmdAgregar', type: 'button', caption: 'Agregar', img: 'glyphicon glyphicon-pencil' }, { id: 'break0', type: 'break' },
    { id: 'cmdEditar', type: 'button', caption: 'Editar', img: 'glyphicon glyphicon-edit' }, { id: 'break1', type: 'break' },
    { id: 'cmdBorrar', type: 'button', caption: 'Borrar', img: 'glyphicon glyphicon-remove' }, { id: 'break2', type: 'break' },
    { id: 'cmdArbol', type: 'button', caption: 'Vista árbol', img: 'glyphicon glyphicon-sort-by-attributes' }];


var oToolBarTablaItems = [
    { id: 'cmdAgregar', type: 'button', caption: 'Agregar', img: 'glyphicon glyphicon-pencil' }, { id: 'break0', type: 'break' },
    { id: 'cmdEditar', type: 'button', caption: 'Editar', img: 'glyphicon glyphicon-edit' }, { id: 'break1', type: 'break' },
    { id: 'cmdBorrar', type: 'button', caption: 'Borrar', img: 'glyphicon glyphicon-remove' }, { id: 'break2', type: 'break' },
    { id: 'cmdTabla', type: 'button', caption: 'Vista tabla', img: 'glyphicon glyphicon-list-alt' }, { id: 'break3', type: 'break' } ];

function GetGridCatArea() {
    var gridCatArea =
    {
        name: 'gridCatArea',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
            { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'araclave', caption: 'CLAVE', size: '80px', resizable: true },
            { field: 'KA_SIGLA', caption: 'SIGLA', size: '80px', resizable: true },
            { field: 'KA_ORDEN', caption: 'ORDEN', size: '80px', resizable: true },
            { field: 'KA_DESCRIPCION', caption: 'DESCRIPCIÓN', size: '350px', resizable: true },
            { field: 'KA_REPORTA', caption: 'REPORTA', size: '80px', resizable: true },
            { field: 'KTA_DESCRIPCION', caption: 'TIPO ÁREA', size: '250px', resizable: true },
            { field: 'KP_DESCRIPCION', caption: 'TIPO PERFIL', size: '160px', resizable: true }
        ],
        toolbar: {
            items: oToolBarTreeItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar"  ) {
                    var sTitulo = AsignarCmd(event.target) + " área";
                    w2popup.load({
                        url: 'PVarea', showMax: true, title: sTitulo, width:'700px', height: '300px', model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarArea();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#araclave").val(_RegistroActual.araclave);
                                    $("#ka_sigla").val(_RegistroActual.KA_SIGLA);
                                    $("#ka_orden").val(_RegistroActual.KA_ORDEN);
                                    $("#ka_descripcion").val(_RegistroActual.KA_DESCRIPCION);
                                    $("#ka_reporta").val(_RegistroActual.KA_REPORTA);
                                    $("#kta_clatipo_area").val(_RegistroActual.KTA_CLATIPO_AREA);
                                    $("#PERCLAVE").val(_RegistroActual.PERCLAVE);

                                    if (iOpc === 3) {
                                        $('#araclave').prop('readonly', true);
                                        $('#ka_sigla').prop('readonly', true);
                                        $('#ka_orden').prop('readonly', true);
                                        $('#ka_descripcion').prop('readonly', true);
                                        $('#ka_reporta').prop('readonly', true);
                                        $('#kta_clatipo_area').prop('disabled', true);
                                        $('#PERCLAVE').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                } else if (event.target === "cmdTabla") {                    
                    w2ui.layoutModulo.content('main', w2ui['gridCatArea']);
                    $('#divDatos').show();                    
                    $("#gridSolBuscarPie").show();

                } else if (event.target === "cmdArbol") {
                    w2ui.layoutModulo.load('main', 'PVareaArbol');
                }
            }
        },
        onClick: function (event) {
            _RegistroActual  = this.get(event.recid);
        },
        onDblClick: function(event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatArea_toolbar']; if (el) el.click('cmdEditar', event);
        }  
    };
    return gridCatArea;
}

function GetGridCat() {
    var gridCat =
    {
        name: 'gridCat',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {            
            toolbar: true
        },
        columns: [
            { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'CAT_CLACAT', caption: 'CLAVE', size: '50px', resizable: true },
            { field: 'CAT_CLASE', caption: 'CLASE', size: '450px', resizable: true },
            { field: 'CAT_DESCRIPCION', caption: 'DESCRIPCIÓN', size: '200px', resizable: true },
            { field: 'KP_DESCRIPCION', caption: 'TIPO DE PERFIL', size: '200px', resizable: true }           
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " catálogo";
                    w2popup.load({
                        url: 'PVcatalogo', showMax: true, title: sTitulo, width: 750, height: 250, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarCatalogo();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#cat_clacat").val(_RegistroActual.CAT_CLACAT);
                                    $("#cat_clase").val(_RegistroActual.CAT_CLASE);
                                    $("#cat_descripcion").val(_RegistroActual.CAT_DESCRIPCION);
                                    $("#PERCLAVE").val(_RegistroActual.PERCLAVE);

                                    if (iOpc === 3) {
                                        $('#cat_clacat').prop('readonly', true);
                                        $('#cat_clase').prop('readonly', true);
                                        $('#cat_descripcion').prop('readonly', true);
                                        $('#PERCLAVE').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCat_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCat;
}

function GetGridCatConfig() {
    var gridCatConfig =
    {
        name: 'gridCatConfig',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
            { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'cfgclave', caption: 'CLAVE', size: '50px', resizable: true },
            { field: 'cfgsubclave', caption: 'CLAVE', size: '150px', resizable: true },
            { field: 'cfgvalor', caption: 'VALOR', size: '150px', resizable: true },
            { field: 'cfgfecbaja', caption: 'FECHA BAJA', size: '120px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " configuración";
                    w2popup.load({
                        url: 'PVconfiguracion', showMax: true, title: sTitulo, width: 750, height: 260, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarConfig();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#cfgclave").val(_RegistroActual.cfgclave);
                                    $("#cfgsubclave").val(_RegistroActual.cfgsubclave);
                                    $("#cfgvalor").val(_RegistroActual.cfgvalor);
                                    $("#cfgfecbaja").val(_RegistroActual.cfgfecbaja);
                                    if (iOpc === 3) {
                                        $('#cfgclave').prop('readonly', true);
                                        $('#cfgsubclave').prop('readonly', true);
                                        $('#cfgvalor').prop('readonly', true);
                                        $('#cfgfecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatConfig_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatConfig;
}

function GetGridCatDiaLab() {
    var gridCatDiaLab =
    {
        name: 'gridCatDiaLab',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'diaclave', caption: 'Día no laboral', size: '160px', resizable: true },
            { field: 'diatipo', caption: 'Día de la semana', size: '160px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems2,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " días laborales ";
                    w2popup.load({
                        url: 'PVdiaLab', showMax: true, title: sTitulo, width: 400, height: 190, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarDiaLab();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#diaclave").val(_RegistroActual.diaclave);
                                    $("#diatipo").val(_RegistroActual.diatipo);
                                    if (iOpc === 3) {
                                        $('#diaclave').prop('readonly', true);
                                        $('#diatipo').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatDiaLab_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatDiaLab;
}

function GetGridCatModulo() {
    var gridCatModulo =
    {
        name: 'gridCatModulo',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
            { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'KM_CLAMODULO', caption: 'CLAVE', size: '50px', resizable: true },
            { field: 'KM_DESCRIPCION', caption: 'Descripcion', size: '160px', resizable: true },
            { field: 'KM_CONTROL', caption: 'Control', size: '160px', resizable: true },
            { field: 'KM_CONSECUTIVO', caption: 'Consecutivo', size: '50px', resizable: true },
            { field: 'KM_FECBAJA', caption: 'Fec. Baja', size: '140px', resizable: true },
            { field: 'KM_CLAMODULO_PADRE', caption: 'Módulo Padre', size: '160px', resizable: true },
            { field: 'KM_METODO', caption: 'Método', size: '160px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " módulo";
                    w2popup.load({
                        url: 'PVmodulo', showMax: true, title: sTitulo, width: 650, height: 360, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarModulo();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#km_clamodulo").val(_RegistroActual.KM_CLAMODULO);
                                    $("#km_descripcion").val(_RegistroActual.KM_DESCRIPCION);
                                    $("#km_control").val(_RegistroActual.KM_CONTROL);
                                    $("#km_consecutivo").val(_RegistroActual.KM_CONSECUTIVO);
                                    $("#km_fecbaja").val(_RegistroActual.KM_FECBAJA);
                                    $("#km_clamodulo_padre").val(_RegistroActual.KM_CLAMODULO_PADRE);
                                    $("#km_metodo").val(_RegistroActual.KM_METODO);
                                    if (iOpc === 3) {
                                        $('#km_clamodulo').prop('readonly', true);
                                        $('#km_descripcion').prop('readonly', true);
                                        $('#km_control').prop('readonly', true);
                                        $('#km_consecutivo').prop('readonly', true);
                                        $('#km_fecbaja').prop('readonly', true);
                                        $('#km_clamodulo_padre').prop('disabled', true);
                                        $('#km_metodo').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatModulo_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatModulo;
}

function GetGridCatPerfil() {
    var gridCatPerfil =
    {
        name: 'gridCatPerfil',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'PERCLAVE', caption: 'CLAVE', size: '50px', resizable: true },
            { field: 'KP_FECBAJA', caption: 'Fec. Baja', size: '100px', resizable: true },
            { field: 'KP_DESCRIPCION', caption: 'Descripcion', size: '180px', resizable: true },
            { field: 'KP_SIGLA', caption: 'Sigla', size: '100px', resizable: true },
            { field: 'KP_MULTIPLE', caption: 'Multiple', size: '100px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " perfil";
                    w2popup.load({
                        url: 'PVperfil', showMax: true, title: sTitulo, width: 600, height: 300, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarPerfil();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#PERCLAVE").val(_RegistroActual.PERCLAVE);
                                    $("#kp_fecbaja").val(_RegistroActual.KP_FECBAJA);
                                    $("#kp_descripcion").val(_RegistroActual.KP_DESCRIPCION);
                                    $("#kp_sigla").val(_RegistroActual.KP_SIGLA);
                                    $("#kp_multiple").val(_RegistroActual.KP_MULTIPLE);
                                    if (iOpc === 3) {
                                        $('#PERCLAVE').prop('readonly', true);
                                        $('#kp_fecbaja').prop('readonly', true);
                                        $('#kp_descripcion').prop('readonly', true);
                                        $('#kp_sigla').prop('readonly', true);
                                        $('#kp_multiple').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatPerfil_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatPerfil;
}

function GetGridCatPerfilMod() {
    var gridCatPerfilMod =
    {
        name: 'gridCatPerfilMod',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'KP_DESCRIPCION', caption: 'Perfil', size: '250px', resizable: true },
            { field: 'KM_DESCRIPCION', caption: 'Modulo', size: '250px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems2,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " perfil - módulo ";
                    w2popup.load({
                        url: 'PVperfilModulo', showMax: true, title: sTitulo, width: 600, height: 180, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarPerfilModulo();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#PERCLAVE").val(_RegistroActual.PERCLAVE);
                                    $("#km_clamodulo").val(_RegistroActual.KM_CLAMODULO);
                                    if (iOpc === 3) {
                                        $('#PERCLAVE').prop('disabled', true);
                                        $('#km_clamodulo').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatPerfilMod_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatPerfilMod;
}

function GetGridCatTipoArea() {
    var gridCatTipoArea =
    {
        name: 'gridCatTipoArea',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'KTA_CLATIPO_AREA', caption: 'Clave', size: '50px', resizable: true },
            { field: 'KTA_DESCRIPCION', caption: 'Descripcion', size: '400px', resizable: true },
            { field: 'KTA_SIGLAS', caption: 'Sigla', size: '150px', resizable: true },
            { field: 'KTA_FECBAJA', caption: 'Fec. Baja', size: '100px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " tipo de área";
                    w2popup.load({
                        url: 'PVtipoArea', showMax: true, title: sTitulo, width: 600, height: 255, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarTipoArea();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#kta_clatipo_area").val(_RegistroActual.KTA_CLATIPO_AREA);
                                    $("#kta_descripcion").val(_RegistroActual.KTA_DESCRIPCION);
                                    $("#kta_siglas").val(_RegistroActual.KTA_SIGLAS);
                                    $("#kta_fecbaja").val(_RegistroActual.KTA_FECBAJA);
                                    if (iOpc === 3) {
                                        $('#kta_clatipo_area').prop('readonly', true);
                                        $('#kta_descripcion').prop('readonly', true);
                                        $('#kta_siglas').prop('readonly', true);
                                        $('#kta_fecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatTipoArea_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatTipoArea;
}

function GetGridCatUsuario() {
    var gridCatUsuario =
    {
        name: 'gridCatUsuario',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'usrclave', caption: 'CLAVE', size: '50px', resizable: true },
            { field: 'usrfecbaja', caption: 'FEC BAJA', size: '110px', resizable: true },
            { field: 'usrnombre', caption: 'NOMBRE', size: '250px', resizable: true },
            { field: 'usrpaterno', caption: 'PATERNO', size: '250px', resizable: true },
            { field: 'usrmaterno', caption: 'MATERNO', size: '250px', resizable: true },
            { field: 'usrpuesto', caption: 'PUESTO', size: '250px', resizable: true },
            { field: 'usrcorreo', caption: 'CORREO', size: '150px', resizable: true },
            { field: 'usrextension', caption: 'EXTENSION', size: '50px', resizable: true },
            { field: 'KA_DESCRIPCION', caption: 'ÁREA ADSCRITO', size: '250px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " Usuarios";
                    w2popup.load({
                        url: 'PVusuario', showMax: true, title: sTitulo, width: 1000, height: 500, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarUsuario();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#usrclave").val(_RegistroActual.usrclave);
                                    $("#usrnombre").val(_RegistroActual.usrnombre);
                                    $("#usrpaterno").val(_RegistroActual.usrpaterno);
                                    $("#usrmaterno").val(_RegistroActual.usrmaterno);
                                    $("#usrpuesto").val(_RegistroActual.usrpuesto);
                                    $("#usrcorreo").val(_RegistroActual.usrcorreo);
                                    $("#usrextension").val(_RegistroActual.usrextension);
                                    $("#araclave_origen").val(_RegistroActual.araclave_ORIGEN);
                                    $("#usrcontraseña").val(_RegistroActual.usrcontraseña);
                                    $("#usrfecbaja").val(_RegistroActual.usrfecbaja);
                                    if (iOpc === 3) {
                                        $('#usrclave').prop('readonly', true);
                                        $('#kunombre').prop('readonly', true);
                                        $('#usrpaterno').prop('readonly', true);
                                        $('#usrmaterno').prop('readonly', true);
                                        $('#usrpuesto').prop('readonly', true);
                                        $('#usrcorreo').prop('readonly', true);
                                        $('#usrextension').prop('readonly', true);
                                        $('#araclave_origen').prop('disabled', true);
                                        $('#usrcontraseña').prop('readonly', true);
                                        $('#usrfecbaja').prop('readonly', true);                                        
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatUsuario_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatUsuario;
}

function GetGridCatUsuArea() {
    var gridCatUsuArea =
    {
        name: 'gridCatUsuArea',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
            { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'usrcorreo', caption: 'Usuario', size: '200px', resizable: true },
            { field: 'KA_DESCRIPCION', caption: 'Area', size: '600px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems2,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " usuario - área";
                    w2popup.load({
                        url: 'PVusuArea', showMax: true, title: sTitulo, width: 600, height: 220, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarUsuArea();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#usrclave").val(_RegistroActual.usrcorreo);
                                    $("#araclave").val(_RegistroActual.araclave);
                                    if (iOpc === 3) {
                                        $('#usrclave').prop('disabled', true);
                                        $('#araclave').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatUsuArea_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatUsuArea;
}

function GetGridCatDocArista() {
    var gridCatDocArista =
    {
        name: 'gridCatDocArista',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'ariclave', caption: 'Arista', size: '80px', resizable: true },
            { field: 'solclave', caption: 'Folio', size: '140px', resizable: true },
            { field: 'docnombre', caption: 'Doc', size: '360px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems2,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " documento arista";
                    w2popup.load({
                        url: 'PVdocArista', showMax: true, title: sTitulo, width: 600, height: 220, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarDocArista();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#ariclave").val(_RegistroActual.ariclave);
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#docclave").val(_RegistroActual.docclave);
                                    if (iOpc === 3) {
                                        $('#ariclave').prop('disabled', true);
                                        $('#solclave').prop('disabled', true);
                                        $('#docclave').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatDocArista_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatDocArista;
}

function GetGridCatTipoDoc() {
    var gridCatTipoDoc =
    {
        name: 'gridCatTipoDoc',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'extclave', caption: 'Clave', size: '50px', resizable: true },
            { field: 'extmimetype', caption: 'Mime', size: '510px', resizable: true },
            { field: 'extterminacion', caption: 'Extensión', size: '100px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " tipo de documento";
                    w2popup.load({
                        url: 'PVtipoDoc', showMax: true, title: sTitulo, width: 700, height: 220, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarTipoDoc();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#extclave").val(_RegistroActual.extclave);
                                    $("#extmimetype").val(_RegistroActual.extmimetype);
                                    $("#extterminacion").val(_RegistroActual.extterminacion);
                                    if (iOpc === 3) {
                                        $('#extclave').prop('readonly', true);
                                        $('#extmimetype').prop('readonly', true);
                                        $('#extterminacion').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatTipoDoc_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatTipoDoc;
}

function GetGridCatDoc() {
    var gridCatDoc =
    {
        name: 'gridCatDoc',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'docclave', caption: 'Doc ID', size: '50px', resizable: true },
            { field: 'docfecha', caption: 'Fecha', size: '80px', resizable: true },
            { field: 'doctamaño', caption: 'Tamaño', size: '80px', resizable: true },
            { field: 'docruta', caption: 'Ruta', size: '400px', resizable: true },
            { field: 'docnombre', caption: 'Nombre', size: '400px', resizable: true },
            { field: 'docfolio', caption: 'Folio', size: '80px', resizable: true },
            { field: 'extclave', caption: 'Ext', size: '80px', resizable: true },
            { field: 'docfilesystem', caption: 'FileSystem', size: '80px', resizable: true },
            { field: 'docurl', caption: 'Url', size: '400px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " documento";
                    w2popup.load({
                        url: 'PVdoc', showMax: true, title: sTitulo, width:700, height:430, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarDoc();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#docclave").val(_RegistroActual.docclave);
                                    $("#docfecha").val(_RegistroActual.docfecha);
                                    $("#doctamaño").val(_RegistroActual.doctamaño);
                                    $("#docruta").val(_RegistroActual.docruta);
                                    $("#docnombre").val(_RegistroActual.docnombre);
                                    $("#docfolio").val(_RegistroActual.docfolio);
                                    $("#extclave").val(_RegistroActual.extclave);
                                    $("#docfilesystem").val(_RegistroActual.docfilesystem);
                                    $("#docurl").val(_RegistroActual.docurl);
                                    if (iOpc === 3) {
                                        $('#docclave').prop('readonly', true);
                                        $('#docfecha').prop('readonly', true);
                                        $('#doctamaño').prop('readonly', true);
                                        $('#docruta').prop('readonly', true);
                                        $('#docnombre').prop('readonly', true);
                                        $('#docfolio').prop('readonly', true);
                                        $('#extclave').prop('disabled', true);
                                        $('#docfilesystem').prop('readonly', true);
                                        $('#docurl').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatDoc_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatDoc;
}

function GetGridCatAfd() {
    var gridCatAfd =
    {
        name: 'gridCatAfd',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'AFDCLAVE', caption: 'AFD ID', size: '50px', resizable: true },
            { field: 'AFDDESCRIPCION', caption: 'Descripción', size: '350px', resizable: true },           
            { field: 'AFDPREFIJO', caption: 'Prefijo', size: '140px', resizable: true },
            { field: 'AFDFECBAJA', caption: 'Fecha baja', size: '140px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " afd";
                    w2popup.load({
                        url: 'PVafd', showMax: true, title: sTitulo, width: 600, height: 320, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarAfd();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    console.log(_RegistroActual);
                                    $("#AFDCLAVE").val(_RegistroActual.AFDCLAVE);
                                    $("#afddescripcion").val(_RegistroActual.AFDDESCRIPCION);
                                    $("#afdprefijo").val(_RegistroActual.AFDPREFIJO);
                                    $("#afdfecbaja").val(_RegistroActual.AFDFECBAJA);
                                    if (iOpc === 3) {
                                        $('#AFDCLAVE').prop('readonly', true);
                                        $('#afddescripcion').prop('readonly', true);
                                        $('#afdprefijo').prop('readonly', true);
                                        $('#afdfecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatAfd_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatAfd;
}

function GetGridCatAfdFlujo() {
    var gridCatAfdFlujo =
    {
        name: 'gridCatAfdFlujo',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
            { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'AFDCLAVE', caption: 'AFD ID', size: '50px', resizable: true },
            { field: 'ORIGENAREA', caption: 'Origen', size: '300px', resizable: true },
            { field: 'ORIGENACCION', caption: 'Accion', size: '300px', resizable: true },
            { field: 'rtpdescripcion', caption: 'Tipo arista', size: '250px', resizable: true },
            { field: 'DESTAREA', caption: 'Destino', size: '300px', resizable: true },
            { field: 'DESTACCION', caption: 'Accion', size: '300px', resizable: true },            
            { field: 'affplazo', caption: 'Plazo', size: '50px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems2,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " afd flujo";
                    w2popup.load({
                        url: 'PVafdFlujo', showMax: true, title: sTitulo, width: 800, height: 280, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarAfdFlujo();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#AFDCLAVE").val(_RegistroActual.AFDCLAVE);
                                    $("#afforigen").val(_RegistroActual.afforigen);
                                    $("#rtpclave").val(_RegistroActual.rtpclave);
                                    $("#affdestino").val(_RegistroActual.affdestino);
                                    $("#affplazo").val(_RegistroActual.affplazo);
                                    if (iOpc === 3) {
                                        $('#AFDCLAVE').prop('readonly', true);
                                        $('#afforigen').prop('disabled', true);
                                        $('#rtpclave').prop('disabled', true);
                                        $('#affdestino').prop('disabled', true);
                                        $('#affplazo').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatAfdFlujo_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatAfdFlujo;
}

function GetGridCatArista() {
    var gridCatArista =
    {
        name: 'gridCatArista',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio', size: '90px', resizable: true },
            { field: 'ariclave', caption: 'Arista', size: '70px', resizable: true },
            { field: 'KU_USUARIO', caption: 'Usuario', size: '70px', resizable: true },
            { field: 'ORIGEN', caption: 'Origen', size: '160px', resizable: true },
            { field: 'DESTINO', caption: 'Destino', size: '160px', resizable: true },
            { field: 'NRE_FECLECUSU', caption: 'Fec lec usUario', size: '50px', resizable: true },
            { field: 'NRE_FECINI', caption: 'Fecha inicio', size: '80px', resizable: true },
            { field: 'NRE_FECFIN', caption: 'Fecha fin', size: '80px', resizable: true },
            { field: 'aridiaslab', caption: 'Días Lab.', size: '70px', resizable: true },
            { field: 'aridiasnat', caption: 'Días Nat', size: '70px', resizable: true },
            { field: 'rtpdescripcion', caption: 'Tipo arista', size: '140px', resizable: true },
            { field: 'NRE_FECLECTURA', caption: 'Fec lectura', size: '80px', resizable: true },
            { field: 'arihito', caption: 'Hito', size: '40px', resizable: true },
            { field: 'megclave', caption: 'Modo entrega', size: '200px', resizable: true },
            { field: 'arimensaje', caption: 'Observación', size: '200px', resizable: true }                        
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " arista ";
                    w2popup.load({
                        url: 'PVarista', showMax: true, title: sTitulo, width: 1000, height: 660, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarArista();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#ariclave").val(_RegistroActual.ariclave);
                                    $("#nodorigen").val(_RegistroActual.nodorigen);
                                    $("#noddestino").val(_RegistroActual.noddestino);
                                    $("#usrclave").val(_RegistroActual.usrclave);
                                    $("#nre_feclecusu").val(_RegistroActual.NRE_FECLECUSU);
                                    $("#nre_fecini").val(_RegistroActual.NRE_FECINI);
                                    $("#nre_fecfin").val(_RegistroActual.NRE_FECFIN);
                                    $("#aridiaslab").val(_RegistroActual.aridiaslab);
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#rtpclave").val(_RegistroActual.rtpclave);
                                    $("#nre_feclectura").val(_RegistroActual.NRE_FECLECTURA);
                                    $("#aridiasnat").val(_RegistroActual.aridiasnat);
                                    $("#arimensaje").val(_RegistroActual.arimensaje);
                                    $("#arihito").val(_RegistroActual.arihito);
                                    $("#megclave").val(_RegistroActual.megclave);
                                    if (iOpc === 3) {
                                        $('#ariclave').prop('readonly', true);
                                        $('#nodorigen').prop('readonly', true);
                                        $('#noddestino').prop('readonly', true);
                                        $('#usrclave').prop('disabled', true);
                                        $('#nre_feclecusu').prop('readonly', true);
                                        $('#nre_fecini').prop('readonly', true);
                                        $('#nre_fecfin').prop('disabled', true);
                                        $('#aridiaslab').prop('readonly', true);
                                        $('#solclave').prop('readonly', true);
                                        $('#rtpclave').prop('readonly', true);
                                        $('#nre_feclectura').prop('readonly', true);
                                        $('#aridiasnat').prop('readonly', true);
                                        $('#arimensaje').prop('disabled', true);
                                        $('#arihito').prop('readonly', true);
                                        $('#megclave').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatArista_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatArista;
}

function GetGridCatAristaComite() {
    var gridCatAristaComite =
    {
        name: 'gridCatAristaComite',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio', size: '80px', resizable: true },
            { field: 'ariclave', caption: 'Arista', size: '80px', resizable: true },
            { field: 'cordescripcion', caption: 'Rubro comite', size: '300px', resizable: true },
            { field: 'COM_MOTIVO', caption: 'Motivo', size: '500px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                w2popup.lock("", true);
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " arista comite";
                    w2popup.load({
                        url: 'PVaristaComite', showMax: true, title: sTitulo, width: 1000, height: 300, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarAristaComite();">Ejecutar</button>',
                        onOpen: function (event) {                            
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#ariclave").val(_RegistroActual.ariclave);
                                    $("#corclave").val(_RegistroActual.corclave);
                                    $("#com_motivo").val(_RegistroActual.COM_MOTIVO);
                                    if (iOpc === 3) {
                                        $('#solclave').prop('readonly', true);
                                        $('#ariclave').prop('readonly', true);
                                        $('#corclave').prop('disabled', true);
                                        $('#COM_MOTIVO').prop('disabled', true);
                                    }
                                }
                                w2popup.unlock();
                            };
                        }
                    });
                                        
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatAristaComite_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatAristaComite;
}

function GetGridCatAristaRes() {
    var gridCatAristaRes =
    {
        name: 'gridCatAristaRes',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio', size: '80px', resizable: true },
            { field: 'ariclave', caption: 'Arista', size: '80px', resizable: true },
            { field: 'nfoclave', caption: 'Tipo informacion', size: '200px', resizable: true },
            { field: 'RSL_UBICACION', caption: 'Ubicación', size: '200px', resizable: true },
            { field: 'RSL_TAM_CANT_DIR', caption: 'Tamaño-Cantidad', size: '200px', resizable: true },
            { field: 'RSL_ART7', caption: 'Art. 7', size: '50px', resizable: true }
        ],        
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " arista resolucion";
                    w2popup.load({
                        url: 'PVaristaRes', showMax: true, title: sTitulo, width: 600, height: 330, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarAristaRes();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#ariclave").val(_RegistroActual.ariclave);
                                    $("#nfoclave").val(_RegistroActual.nfoclave);
                                    $("#rsl_ubicacion").val(_RegistroActual.RSL_UBICACION);
                                    $("#rsl_tam_cant_dir").val(_RegistroActual.RSL_TAM_CANT_DIR);
                                    $("#rsl_art7").val(_RegistroActual.RSL_ART7);
                                    if (iOpc === 3) {
                                        $('#solclave').prop('readonly', true);
                                        $('#ariclave').prop('readonly', true);
                                        $('#nfoclave').prop('disabled', true);
                                        $('#rsl_ubicacion').prop('readonly', true);
                                        $('#rsl_tam_cant_dir').prop('readonly', true);
                                        $('#rsl_art7').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatAristaRes_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatAristaRes;
}

function GetGridCatAristaRev() {
    var gridCatAristaRev =
    {
        name: 'gridCatAristaRev',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio', size: '120px', resizable: true },
            { field: 'ariclave', caption: 'Arista', size: '100px', resizable: true },
            { field: 'REV_EXPEDIENTE', caption: 'Expediente', size: '120px', resizable: true },
            { field: 'REV_RESPONSABLE', caption: 'Responsable', size: '140px', resizable: true },
            { field: 'REV_CORREO', caption: 'Correo', size: '140px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " arista revisión";
                    w2popup.load({
                        url: 'PVaristaRev', showMax: true, title: sTitulo, width: 550, height: 290, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarAristaRev();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#ariclave").val(_RegistroActual.ariclave);
                                    $("#rev_expediente").val(_RegistroActual.REV_EXPEDIENTE);
                                    $("#rev_responsable").val(_RegistroActual.REV_RESPONSABLE);
                                    $("#rev_correo").val(_RegistroActual.REV_CORREO);
                                    if (iOpc === 3) {
                                        $('#solclave').prop('readonly', true);
                                        $('#ariclave').prop('readonly', true);
                                        $('#rev_expediente').prop('readonly', true);
                                        $('#rev_responsable').prop('readonly', true);
                                        $('#rev_correo').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatAristaRev_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatAristaRev;
}

function GetGridCatAristaSubCla() {
    var gridCatAristaSubCla =
    {
        name: 'gridCatAristaSubCla',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio', size: '150px', resizable: true },
            { field: 'ariclave', caption: 'Arista', size: '100px', resizable: true },
            { field: 'rtpclave', caption: 'Tipo arista', size: '300px', resizable: true }
        ],        
        toolbar: {
            items: oToolBarItems2,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " arista subclasificación";
                    w2popup.load({
                        url: 'PVaristaSubCla', showMax: true, title: sTitulo, width: 900, height: 220, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarAristaSubCla();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#ariclave").val(_RegistroActual.ariclave);
                                    $("#rtpclave").val(_RegistroActual.rtpclave);
                                    if (iOpc === 3) {
                                        $('#solclave').prop('readonly', true);
                                        $('#ariclave').prop('readonly', true);
                                        $('#rtpclave').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatAristaSubCla_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatAristaSubCla;
}

function GetGridCatComiteRubro() {
    var gridCatComiteRubro =
    {
        name: 'gridCatComiteRubro',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'corclave', caption: 'Clave', size: '80px', resizable: true },
            { field: 'cordescripcion', caption: 'Descripción', size: '400px', resizable: true },
            { field: 'corfecbaja', caption: 'Fecha baja', size: '150px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " rubro del comité";
                    w2popup.load({
                        url: 'PVcomiteRubro', showMax: true, title: sTitulo, width: 600, height: 260, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarComiteRubro();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#corclave").val(_RegistroActual.corclave);
                                    $("#cordescripcion").val(_RegistroActual.cordescripcion);
                                    $("#corfecbaja").val(_RegistroActual.corfecbaja);
                                    if (iOpc === 3) {
                                        $('#corclave').prop('readonly', true);
                                        $('#cordescripcion').prop('readonly', true);
                                        $('#corfecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatComiteRubro_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatComiteRubro;
}

function GetGridCatNodoEstado() {
    var gridCatNodoEstado =
    {
        name: 'gridCatNodoEstado',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'nodclave', caption: 'Nodo estado', size: '100px', resizable: true },
            { field: 'nodDescripcion', caption: 'Descripción', size: '300px', resizable: true },
            { field: 'nedurl', caption: 'URL', size: '300px', resizable: true },
            { field: 'nedtipo', caption: 'Tipo', size: '200px', resizable: true },
            { field: 'KP_DESCRIPCION', caption: 'Perfil', size: '250px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + "estado del nodo";
                    w2popup.load({
                        url: 'PVnodoEstado', showMax: true, title: sTitulo, width: 450, height: 290, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarNodoEstado();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#nodclave").val(_RegistroActual.nodclave);
                                    $("#nodDescripcion").val(_RegistroActual.nodDescripcion);
                                    $("#nedurl").val(_RegistroActual.nedurl);
                                    $("#nedtipo").val(_RegistroActual.nedtipo);
                                    $("#PERCLAVE").val(_RegistroActual.PERCLAVE);
                                    if (iOpc === 3) {
                                        $('#nodclave').prop('readonly', true);
                                        $('#nodDescripcion').prop('readonly', true);
                                        $('#nedurl').prop('readonly', true);
                                        $('#nedtipo').prop('readonly', true);
                                        $('#PERCLAVE').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatNodoEstado_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatNodoEstado;
}

function GetGridCatNodo() {
    var gridCatNodo =
    {
        name: 'gridCatNodo',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio', size: '110px', resizable: true },
            { field: 'prcdescripcion', caption: 'Proceso', size: '110px', resizable: true },
            { field: 'nodclave', caption: 'Nodo', size: '90px', resizable: true },
            { field: 'KA_DESCRIPCION', caption: 'Area', size: '160px', resizable: true },
            { field: 'nodfeccreacion', caption: 'Creación', size: '110px', resizable: true },
            { field: 'nodDescripcion', caption: 'Tipo arista', size: '160px', resizable: true },
            { field: 'nodatendido', caption: 'Atendido', size: '80px', resizable: true },
            { field: 'nodcapa', caption: 'CAPA', size: '80px', resizable: true },
            { field: 'NOD_HOLGURA', caption: 'Holgura', size: '80px', resizable: true },
            { field: 'NOD_TIP', caption: 'TIP', size: '80px', resizable: true },
            { field: 'NOD_TTP', caption: 'TTP', size: '80px', resizable: true },
            { field: 'NOD_TIL', caption: 'TIL', size: '80px', resizable: true },
            { field: 'NOD_TTL', caption: 'TTL', size: '80px', resizable: true }           
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + "estado del nodo";
                    w2popup.load({
                        url: 'PVnodo', showMax: true, title: sTitulo, width: 1100, height: 570, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarNodo();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#prcclave").val(_RegistroActual.prcclave);
                                    $("#nodclave").val(_RegistroActual.nodclave);
                                    $("#araclave").val(_RegistroActual.araclave);
                                    $("#nodfeccreacion").val(_RegistroActual.nodfeccreacion);
                                    $("#nodclave").val(_RegistroActual.nodclave);
                                    $("#nod_holgura").val(_RegistroActual.NOD_HOLGURA);                                    
                                    $("#nodatendido").val(_RegistroActual.nodatendido);                                    
                                    $("#nodcapa").val(_RegistroActual.nodcapa);
                                    $("#nod_tip").val(_RegistroActual.NOD_TIP);
                                    $("#nod_ttp").val(_RegistroActual.NOD_TTP);
                                    $("#nod_til").val(_RegistroActual.NOD_TIL);
                                    $("#nod_ttl").val(_RegistroActual.NOD_TTL);
                                    if (iOpc === 3) {
                                        $('#solclave').prop('readonly', true);
                                        $('#prcclave').prop('disabled', true);
                                        $('#nodclave').prop('readonly', true);
                                        $('#araclave').prop('disabled', true);
                                        $('#nodfeccreacion').prop('readonly', true);
                                        $('#nodclave').prop('disabled', true);
                                        $('#nod_holgura').prop('readonly', true);
                                        $('#nodatendido').prop('readonly', true);
                                        $('#nodcapa').prop('readonly', true);
                                        $('#nod_tip').prop('readonly', true);
                                        $('#nod_ttp').prop('readonly', true);
                                        $('#nod_til').prop('readonly', true);
                                        $('#nod_ttl').prop('readonly', true);                                        
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatNodo_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatNodo;
}

function GetGridCatTipoArista() {
    var gridCatTipoArista =
    {
        name: 'gridCatTipoArista',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'rtpclave', caption: 'Tipo arista', size: '90px', resizable: true },
            { field: 'rtpdescripcion', caption: 'Descripción', size: '160px', resizable: true },
            { field: 'KAR_APLICATIPOINFO', caption: 'Tipo info', size: '90px', resizable: true },
            { field: 'rtpformato', caption: 'Formato', size: '200px', resizable: true },
            { field: 'KAR_RESPUESTA', caption: 'Respuesta', size: '90px', resizable: true },
            { field: 'KAR_TIPO', caption: 'Tipo', size: '90px', resizable: true },
            { field: 'KAR_SIGLA', caption: 'Sigla', size: '90px', resizable: true },
            { field: 'KAR_SUBCLASIFICAR', caption: 'Cubclasificar', size: '90px', resizable: true },
            { field: 'KAR_MOSTRARSUB', caption: 'MostrarSub', size: '90px', resizable: true },
            { field: 'rtpforma', caption: 'Forma', size: '90px', resizable: true },
            { field: 'KAR_NIVEL', caption: 'Nivel', size: '90px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " tipo de arista";
                    w2popup.load({
                        url: 'PVtipoArista', showMax: true, title: sTitulo, width: 980, height: 610, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarTipoArista();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#rtpclave").val(_RegistroActual.rtpclave);
                                    $("#rtpdescripcion").val(_RegistroActual.rtpdescripcion);
                                    $("#kar_aplicatipoinfo").val(_RegistroActual.KAR_APLICATIPOINFO);
                                    $("#rtpformato").val(_RegistroActual.rtpformato);
                                    $("#kar_respuesta").val(_RegistroActual.KAR_RESPUESTA);
                                    $("#kar_tipo").val(_RegistroActual.KAR_TIPO);
                                    $("#kar_sigla").val(_RegistroActual.KAR_SIGLA);
                                    $("#kar_subclasificar").val(_RegistroActual.KAR_SUBCLASIFICAR);
                                    $("#kar_mostrarsub").val(_RegistroActual.KAR_MOSTRARSUB);
                                    $("#rtpforma").val(_RegistroActual.rtpforma);
                                    $("#kar_nivel").val(_RegistroActual.KAR_NIVEL);
                                    if (iOpc === 3) {
                                        $('#rtpclave').prop('readonly', true);
                                        $('#rtpdescripcion').prop('readonly', true);
                                        $('#kar_aplicatipoinfo').prop('readonly', true);
                                        $('#rtpformato').prop('readonly', true);
                                        $('#kar_respuesta').prop('readonly', true);
                                        $('#kar_tipo').prop('readonly', true);
                                        $('#kar_sigla').prop('readonly', true);
                                        $('#kar_subclasificar').prop('readonly', true);
                                        $('#kar_mostrarsub').prop('readonly', true);
                                        $('#rtpforma').prop('readonly', true);
                                        $('#kar_nivel').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatTipoArista_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatTipoArista;
}

function GetGridCatTipoInfo() {
    var gridCatTipoInfo =
    {
        name: 'gridCatTipoInfo',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'nfoclave', caption: 'Tipo info ID', size: '120px', resizable: true },
            { field: 'nfodescripcion', caption: 'Descripción', size: '400px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " tipo de información";
                    w2popup.load({
                        url: 'PVtipoInfo', showMax: true, title: sTitulo, width: 600, height: 180, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarTipoInfo();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#nfoclave").val(_RegistroActual.nfoclave);
                                    $("#nfodescripcion").val(_RegistroActual.nfodescripcion);
                                    if (iOpc === 3) {
                                        $('#nfoclave').prop('readonly', true);
                                        $('#nfodescripcion').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatTipoInfo_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };

    return gridCatTipoInfo;
}

function
    GetGridCatEntFed() {
    var gridCatEntFed =
    {
        name: 'gridCatEntFed',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },        
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'edoclave', caption: 'Entidad ID', size: '110px', resizable: true },
            { field: 'edodescripcion', caption: 'Descripción', size: '300px', resizable: true },
            { field: 'edofecbaja', caption: 'Fecha baja', size: '140px', resizable: true },
            { field: 'paidescripcion', caption: 'Pais', size: '300px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " entidad federativa";
                    w2popup.load({
                        url: 'PVentFed', showMax: true, title: sTitulo, width: 600, height: 320, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarEntFed();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#edoclave").val(_RegistroActual.edoclave);
                                    $("#edodescripcion").val(_RegistroActual.edodescripcion);
                                    $("#edofecbaja").val(_RegistroActual.edofecbaja);
                                    $("#paiclave").val(_RegistroActual.paiclave);
                                    if (iOpc === 3) {
                                        $('#edoclave').prop('readonly', true);
                                        $('#edodescripcion').prop('readonly', true);
                                        $('#edofecbaja').prop('readonly', true);
                                        $('#paiclave').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatEntFed_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatEntFed;
}

function GetGridCatMunicipio() {
    var gridCatMunicipio =
    {
        name: 'gridCatMunicipio',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'paidescripcion', caption: 'Pais', size: '200px', resizable: true },
            { field: 'edodescripcion', caption: 'Estado', size: '200px', resizable: true },
            { field: 'munclave', caption: 'Municipio ID', size: '120px', resizable: true },
            { field: 'mundescripcion', caption: 'Descripción', size: '300px', resizable: true },
            { field: 'munfecbaja', caption: 'Fecha baja', size: '100px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " municipio";
                    w2popup.load({
                        url: 'PVmunicipio', showMax: true, title: sTitulo, width: 600, height: 320, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarMunicipio();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#munclave").val(_RegistroActual.munclave);
                                    $("#mundescripcion").val(_RegistroActual.mundescripcion);
                                    $("#munfecbaja").val(_RegistroActual.munfecbaja);
                                    $("#edoclave").val(_RegistroActual.edoclave);
                                    $("#paiclave").val(_RegistroActual.paiclave);
                                    if (iOpc === 3) {
                                        $('#munclave').prop('readonly', true);
                                        $('#mundescripcion').prop('readonly', true);
                                        $('#munfecbaja').prop('readonly', true);
                                        $('#edoclave').prop('disabled', true);
                                        $('#paiclave').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatMunicipio_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatMunicipio;
}

function GetGridCatOcupacion() {
    var gridCatOcupacion =
    {
        name: 'gridCatOcupacion',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'ocuclave', caption: 'Ocupación', size: '120px', resizable: true },
            { field: 'ocudescripcion', caption: 'Descripción', size: '700px', resizable: true },
            { field: 'ocufecbaja', caption: 'Fecha de baja', size: '120px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " ocupación";
                    w2popup.load({
                        url: 'PVocupacion', showMax: true, title: sTitulo, width: 600, height: 240, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarOcupacion();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#ocuclave").val(_RegistroActual.ocuclave);
                                    $("#ocudescripcion").val(_RegistroActual.ocudescripcion);
                                    $("#ocufecbaja").val(_RegistroActual.ocufecbaja);
                                    if (iOpc === 3) {
                                        $('#ocuclave').prop('readonly', true);
                                        $('#ocudescripcion').prop('readonly', true);
                                        $('#ocufecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatOcupacion_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatOcupacion;
}

function GetGridCatPais() {
    var gridCatPais =
    {
        name: 'gridCatPais',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'paiclave', caption: 'Pais ID', size: '100px', resizable: true },
            { field: 'paidescripcion', caption: 'Descipción', size: '500px', resizable: true },
            { field: 'paifecbaja', caption: 'Fecha baja', size: '140px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " país";
                    w2popup.load({
                        url: 'PVpais', showMax: true, title: sTitulo, width: 600, height: 260, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarPais();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#paiclave").val(_RegistroActual.paiclave);
                                    $("#paidescripcion").val(_RegistroActual.paidescripcion);
                                    $("#paifecbaja").val(_RegistroActual.paifecbaja);
                                    if (iOpc === 3) {
                                        $('#paiclave').prop('readonly', true);
                                        $('#paidescripcion').prop('readonly', true);
                                        $('#paifecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatPais_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatPais;
}

function GetGridCatSolicitante() {
    var gridCatSolicitante =
    {
        name: 'gridCatSolicitante',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'sntclave', caption: 'Usuario', size: '120px', resizable: true },
            { field: 'sntrfc', caption: 'RFC', size: '120px', resizable: true },
            { field: 'sntcurp', caption: 'CURP', size: '200px', resizable: true },
            { field: 'sntapepat', caption: 'Paterno', size: '160px', resizable: true },
            { field: 'sntapemat', caption: 'Materno', size: '160px', resizable: true },
            { field: 'sntnombre', caption: 'Nombre', size: '160px', resizable: true },
            { field: 'sntcalle', caption: 'Calle', size: '120px', resizable: true },
            { field: 'sntnumext', caption: 'Num Ext', size: '120px', resizable: true },
            { field: 'sntnumint', caption: 'Num Int', size: '120px', resizable: true },
            { field: 'sntcol', caption: 'Colonia', size: '120px', resizable: true },
            { field: 'sntcodpos', caption: 'Código', size: '80px', resizable: true },
            { field: 'snttel', caption: 'Telefono', size: '120px', resizable: true },
            { field: 'sntcorele', caption: 'Correo', size: '120px', resizable: true },
            { field: 'sntsexo', caption: 'Sexo', size: '120px', resizable: true },
            { field: 'sntfecnac', caption: 'Fec nacimiento', size: '180px', resizable: true },
            { field: 'sntusuario', caption: 'Usuario', size: '180px', resizable: true },
            { field: 'paidescripcion', caption: 'Pais', size: '200px', resizable: true },
            { field: 'sntedoext', caption: 'Estado Ext.', size: '200px', resizable: true },
            { field: 'edodescripcion', caption: 'Estado ', size: '200px', resizable: true },
            { field: 'mundescripcion', caption: 'Municipio', size: '200px', resizable: true },
            { field: 'sntciudadext', caption: 'Ciudad', size: '200px', resizable: true },
            { field: 'sntrepleg', caption: 'Reprsentante legal', size: '200px', resizable: true },
            { field: 'ocudescripcion', caption: 'Ocupacin', size: '200px', resizable: true },
            { field: 'sntotraocup', caption: 'Otra ocupación', size: '200px', resizable: true },
            { field: 'sntniveduc', caption: 'Niv. Educativo', size: '200px', resizable: true },
            { field: 'sntotroniveledu', caption: 'Otro nivel edu', size: '200px', resizable: true },
            { field: 'tsldescripcion', caption: 'Tipo solicitud', size: '200px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " solicitante";
                    w2popup.load({
                        url: 'PVsolicitante', showMax: true, title: sTitulo, width: 700, height: 750, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarSolicitante();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#sntclave").val(_RegistroActual.sntclave);
                                    $("#sntrfc").val(_RegistroActual.sntrfc);
                                    $("#sntcurp").val(_RegistroActual.sntcurp);
                                    $("#sntapepat").val(_RegistroActual.sntapepat);
                                    $("#sntapemat").val(_RegistroActual.sntapemat);
                                    $("#sntnombre").val(_RegistroActual.sntnombre);
                                    $("#sntcalle").val(_RegistroActual.sntcalle);
                                    $("#sntnumext").val(_RegistroActual.sntnumext);
                                    $("#sntnumint").val(_RegistroActual.sntnumint);
                                    $("#sntcol").val(_RegistroActual.sntcol);
                                    $("#sntcodpos").val(_RegistroActual.sntcodpos);
                                    $("#snttel").val(_RegistroActual.snttel);
                                    $("#sntcorele").val(_RegistroActual.sntcorele);
                                    $("#sntsexo").val(_RegistroActual.sntsexo);
                                    $("#sntfecnac").val(_RegistroActual.sntfecnac);
                                    $("#sntusuario").val(_RegistroActual.sntusuario);
                                    $("#paiclave").val(_RegistroActual.paiclave);
                                    $("#sntedoext").val(_RegistroActual.sntedoext);
                                    $("#edoclave").val(_RegistroActual.edoclave);
                                    $("#kmu_clamu").val(_RegistroActual.munclave);
                                    $("#sntciudadext").val(_RegistroActual.sntciudadext);
                                    $("#sntrepleg").val(_RegistroActual.sntrepleg);
                                    $("#ocuclave").val(_RegistroActual.ocuclave);
                                    $("#sntotraocup").val(_RegistroActual.sntotraocup);
                                    $("#sntniveduc").val(_RegistroActual.sntniveduc);
                                    $("#us_otroniveledu").val(_RegistroActual.sntotroniveledu);
                                    $("#tslclave").val(_RegistroActual.tslclave);
                                    if (iOpc === 3) {
                                        $('#sntclave').prop('readonly', true);
                                        $('#sntrfc').prop('readonly', true);
                                        $('#sntcurp').prop('readonly', true);
                                        $('#sntapepat').prop('readonly', true);
                                        $('#sntapemat').prop('readonly', true);
                                        $('#sntnombre').prop('readonly', true);                                        
                                        $('#sntcalle').prop('readonly', true);
                                        $('#sntnumext').prop('readonly', true);
                                        $('#sntnumint').prop('readonly', true);
                                        $('#sntcol').prop('readonly', true);
                                        $('#sntcodpos').prop('readonly', true);
                                        $('#snttel').prop('readonly', true);
                                        $('#sntcorele').prop('readonly', true);
                                        $('#sntsexo').prop('readonly', true);
                                        $('#sntfecnac').prop('readonly', true);
                                        $('#sntusuario').prop('readonly', true);
                                        $('#paiclave').prop('disabled', true);
                                        $('#sntedoext').prop('readonly', true);
                                        $('#edoclave').prop('disabled', true);
                                        $('#kmu_clamu').prop('disabled', true);
                                        $('#sntciudadext').prop('readonly', true);                                        
                                        $('#sntrepleg').prop('readonly', true);
                                        $('#ocuclave').prop('disabled', true);
                                        $('#sntotraocup').prop('disabled', true);
                                        $('#sntniveduc').prop('readonly', true);
                                        $('#us_otroniveledu').prop('readonly', true);
                                        $('#tslclave').prop('disabled', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatSolicitante_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatSolicitante;
}

function GetGridCatTipoSnt() {
    var gridCatTipoSnt =
    {
        name: 'gridCatTipoSnt',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'tslclave', caption: 'Tipo solicitante', size: '150px', resizable: true },
            { field: 'tsldescripcion', caption: 'Descripción', size: '300px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " tipo solicitante";
                    w2popup.load({
                        url: 'PVtipoSnt', showMax: true, title: sTitulo, width: 650, height: 260, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarTipoSnt();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#tslclave").val(_RegistroActual.tslclave);
                                    $("#tsldescripcion").val(_RegistroActual.tsldescripcion);
                                    if (iOpc === 3) {
                                        $('#tslclave').prop('readonly', true);
                                        $('#tsldescripcion').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatTipoSnt_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatTipoSnt;
}

function GetGridCatProceso() {
    var gridCatProceso =
    {
        name: 'gridCatProceso',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'prcclave', caption: 'Proceso', size: '120px', resizable: true },
            { field: 'prcdescripcion',caption: 'Descripción', size: '300px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " proceso ";
                    w2popup.load({
                        url: 'PVproceso ', showMax: true, title: sTitulo, width: 600, height: 180, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarProceso();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#prcclave").val(_RegistroActual.prcclave);
                                    $("#prcdescripcion").val(_RegistroActual.prcdescripcion);
                                    if (iOpc === 3) {
                                        $('#prcclave').prop('readonly', true);
                                        $('#prcdescripcion').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatProceso_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatProceso;
}

function GetGridCatProcesoPlazos() {
    var gridCatProcesoPlazos =
    {
        name: 'gridCatProcesoPlazos',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'prcdescripcion', caption: 'Proceso', size: '160px', resizable: true },
            { field: 'pczplazo', caption: 'Plazo', size: '90px', resizable: true },
            { field: 'pczverde', caption: 'Verde', size: '90px', resizable: true },
            { field: 'pczamarillo', caption: 'Amarillo', size: '90px', resizable: true },
            { field: 'sotdescripcion', caption: 'Tipo Solicitud', size: '240px', resizable: true },
            { field: 'pczclave', caption: 'Tipo Plazo', size: '90px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " plazos del proceso";
                    w2popup.load({
                        url: 'PVprocesoPlazo', showMax: true, title: sTitulo, width: 600, height: 320, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarProcesoPlazo();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#prcclave").val(_RegistroActual.prcclave);
                                    $("#pczplazo").val(_RegistroActual.pczplazo);
                                    $("#pczverde").val(_RegistroActual.pczverde);
                                    $("#pczamarillo").val(_RegistroActual.pczamarillo);
                                    $("#sotclave").val(_RegistroActual.sotclave);
                                    $("#pczclave").val(_RegistroActual.pczclave);
                                    if (iOpc === 3) {
                                        $('#prcclave').prop('disabled', true);
                                        $('#pczplazo').prop('readonly', true);
                                        $('#pczverde').prop('readonly', true);
                                        $('#pczamarillo').prop('readonly', true);
                                        $('#sotclave').prop('disabled', true);
                                        $('#pczclave').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatProcesoPlazos_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatProcesoPlazos;
}

function GetGridCatMedioEntrada() {
    var gridCatMedioEntrada =
    {
        name: 'gridCatMedioEntrada',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'metclave', caption: 'Medio Entrada', size: '110px', resizable: true },
            { field: 'metdescripcion', caption: 'Descripcion', size: '400px', resizable: true },
            { field: 'MET_FECBAJA', caption: 'Fecha de baja', size: '110px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " medio de entrada";
                    w2popup.load({
                        url: 'PVmedioEntrada', showMax: true, title: sTitulo, width: 600, height: 220, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarMedioEntrada();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#metclave").val(_RegistroActual.metclave);
                                    $("#metdescripcion").val(_RegistroActual.metdescripcion);
                                    $("#met_fecbaja").val(_RegistroActual.MET_FECBAJA);
                                    if (iOpc === 3) {
                                        $('#metclave').prop('readonly', true);
                                        $('#metdescripcion').prop('readonly', true);
                                        $('#met_fecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatMedioEntrada_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatMedioEntrada;
}

function GetGridCatModoEntrega() {
    var gridCatModoEntrega =
    {
        name: 'gridCatModoEntrega',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'megclave', caption: 'Modo entrega', size: '110px', resizable: true },
            { field: 'megdescripcion', caption: 'Descripción', size: '300px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " modo de entrega";
                    w2popup.load({
                        url: 'PVtipoModoEntrega', showMax: true, title: sTitulo, width: 600, height: 180, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarModoEntrega();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#megclave").val(_RegistroActual.megclave);
                                    $("#megdescripcion").val(_RegistroActual.megdescripcion);
                                    if (iOpc === 3) {
                                        $('#megclave').prop('readonly', true);
                                        $('#megdescripcion').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatModoEntrega_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatModoEntrega;
}

function GetGridCatRubroTematico() {
    var gridCatRubroTematico =
    {
        name: 'gridCatRubroTematico',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'KRT_CLATEMA', caption: 'Tema', size: '110px', resizable: true },
            { field: 'KRT_RUBRO', caption: 'Rubro', size: '200px', resizable: true },
            { field: 'KRT_PLAZO_RESERVA', caption: 'Plazo reserva', size: '300px', resizable: true },
            { field: 'KRT_FUNDAMENTO_LEGAL', caption: 'Fundamento', size: '300px', resizable: true },
            { field: 'KRT_FECBAJA', caption: 'Fecha baja', size: '140px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " rubro temático";
                    w2popup.load({
                        url: 'PVtipoRubroTematico', showMax: true, title: sTitulo, width: 600, height: 300, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarTipoRubroTematico();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#krt_clatema").val(_RegistroActual.KRT_CLATEMA);
                                    $("#krt_rubro").val(_RegistroActual.KRT_RUBRO);
                                    $("#krt_plazo_reserva").val(_RegistroActual.KRT_PLAZO_RESERVA);
                                    $("#krt_fundamento_legal").val(_RegistroActual.KRT_FUNDAMENTO_LEGAL);
                                    $("#krt_fecbaja").val(_RegistroActual.KRT_FECBAJA);
                                    if (iOpc === 3) {
                                        $('#krt_clatema').prop('readonly', true);
                                        $('#krt_rubro').prop('readonly', true);
                                        $('#krt_plazo_reserva').prop('readonly', true);
                                        $('#krt_fundamento_legal').prop('readonly', true);
                                        $('#krt_fecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatRubroTematico_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatRubroTematico;
}

function GetGridCatTipoSol() {
    var gridCatTipoSol =
    {
        name: 'gridCatTipoSol',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'sotclave', caption: 'Tipo solicitud', size: '150px', resizable: true },
            { field: 'sotdescripcion', caption: 'Descripcion', size: '300px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " tipo de soliciutd";
                    w2popup.load({
                        url: 'PVtipoSol', showMax: true, title: sTitulo, width: 600, height: 190, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarTipoSol();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#sotclave").val(_RegistroActual.sotclave);
                                    $("#sotdescripcion").val(_RegistroActual.sotdescripcion);
                                    if (iOpc === 3) {
                                        $('#sotclave').prop('readonly', true);
                                        $('#sotdescripcion').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatTipoSol_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatTipoSol;
}

function GetGridCatUnidadTran() {
    var gridCatUnidadTran =
    {
        name: 'gridCatUnidadTran',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'US_UNIENL', caption: 'Unidad Transp.', size: '110px', resizable: true },
            { field: 'ENL_DESCRIPCION', caption: 'Descripción', size: '400px', resizable: true },
            { field: 'ENL_FECBAJA', caption: 'Fecha baja', size: '140px', resizable: true }            
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " unidad transparencia ";
                    w2popup.load({
                        url: 'PVunidadTran', showMax: true, title: sTitulo, width: 600, height: 220, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarUnidadTran();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#us_unienl").val(_RegistroActual.US_UNIENL);
                                    $("#enl_descripcion").val(_RegistroActual.ENL_DESCRIPCION);
                                    $("#enl_fecbaja").val(_RegistroActual.ENL_FECBAJA);                                    
                                    if (iOpc === 3) {
                                        $('#us_unienl').prop('readonly', true);
                                        $('#enl_descripcion').prop('readonly', true);
                                        $('#enl_fecbaja').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatUnidadTran_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatUnidadTran;
}

function GetGridCatSeguimiento() {
    var gridCatSeguimiento =
    {
        name: 'gridCatSeguimiento',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio', size: '110px', resizable: true },
            { field: 'segfecini', caption: 'Fecha inicio', size: '120px', resizable: true },
            { field: 'segfecestimada', caption: 'Fec. estimada', size: '120px', resizable: true },
            { field: 'segfecfin', caption: 'Fecha fin', size: '120px', resizable: true },
            { field: 'segfecamp', caption: 'Fecha Ampl.', size: '120px', resizable: true },
            { field: 'segfeccalculo', caption: 'Fecha calculo', size: '120px', resizable: true },
            { field: 'segdiassemaforo', caption: 'Días Semaforo', size: '130px', resizable: true },
            { field: 'segdiasnolab', caption: 'Días no lab', size: '130px', resizable: true },
            { field: 'segsemaforocolor', caption: 'Semaforo', size: '90px', resizable: true },            
            { field: 'segmultiple', caption: 'Multiple', size: '90px', resizable: true },                        
            { field: 'rtpclave', caption: 'Respuesta Interna', size: '250px', resizable: true },
            { field: 'SEG_RESP_EXTERIOR', caption: 'Respuesta externa', size: '250px', resizable: true },
            { field: 'prcclave', caption: 'Proceso', size: '150px', resizable: true },
            { field: 'segedoproceso', caption: 'Estado Proceso', size: '150px', resizable: true },
            { field: 'AFDCLAVE', caption: 'Tipo AFD', size: '90px', resizable: true },
            { field: 'ariclave', caption: 'Arista ID', size: '90px', resizable: true },
            { field: 'segultimonodo', caption: 'Nodo ID', size: '90px', resizable: true }           
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " seguimiento ";
                    w2popup.load({
                        url: 'PVseguimiento', showMax: true, title: sTitulo, width: 600, height: 720, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarSeguimiento();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#segfecini").val(_RegistroActual.segfecini);
                                    $("#segdiassemaforo").val(_RegistroActual.segdiassemaforo);
                                    $("#segsemaforocolor").val(_RegistroActual.segsemaforocolor);
                                    $("#segfecamp").val(_RegistroActual.segfecamp);
                                    $("#segfecfin").val(_RegistroActual.segfecfin);
                                    $("#segmultiple").val(_RegistroActual.segmultiple);
                                    $("#segdiasnolab").val(_RegistroActual.segdiasnolab);
                                    $("#segfeccalculo").val(_RegistroActual.segfeccalculo);                                    
                                    $("#rtpclave").val(_RegistroActual.rtpclave);
                                    $("#seg_resp_exterior").val(_RegistroActual.SEG_RESP_EXTERIOR);
                                    $("#prcclave").val(_RegistroActual.prcclave);
                                    $("#segedoproceso").val(_RegistroActual.segedoproceso);
                                    $("#AFDCLAVE").val(_RegistroActual.AFDCLAVE);
                                    $("#ariclave").val(_RegistroActual.ariclave);
                                    $("#segfeccalculo").val(_RegistroActual.segfeccalculo);
                                    $("#segfecestimada").val(_RegistroActual.segfecestimada);
                                    $("#segultimonodo").val(_RegistroActual.segultimonodo);
                                    if (iOpc === 3) {
                                        $('#solclave').prop('readonly', true);
                                        $('#segfecini').prop('readonly', true);
                                        $('#segdiassemaforo').prop('readonly', true);
                                        $('#segsemaforocolor').prop('disabled', true);
                                        $('#segfecamp').prop('readonly', true);
                                        $('#segfecfin').prop('readonly', true);
                                        $('#segmultiple').prop('readonly', true);
                                        $('#segdiasnolab').prop('readonly', true);
                                        $('#segfeccalculo').prop('readonly', true);
                                        $("#rtpclave").prop('disabled', true);
                                        $("#seg_resp_exterior").prop('disabled', true);
                                        $("#prcclave").prop('disabled', true);
                                        $("#segedoproceso").prop('readonly', true);
                                        $("#AFDCLAVE").prop('disabled', true);
                                        $("#ariclave").prop('readonly', true);
                                        $("#segfeccalculo").prop('readonly', true);
                                        $("#segfecestimada").prop('readonly', true);
                                        $("#segultimonodo").prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatSeguimiento_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatSeguimiento;
}

function GetGridCatSolicitud() {
    var gridCatSolicitud =
    {
        name: 'gridCatSolicitud',
        method: 'GET', // need this to avoid 412 error on Safari
        show: {
            toolbar: true
        },       
        columns: [
             { field: 'recid', caption: '#', size: '50px', resizable: true, style: 'background-color: #eaeaea;' },
            { field: 'solclave', caption: 'Folio (Num)', size: '110px', resizable: true },
            { field: 'solclave', caption: 'Folio', size: '110px', resizable: true },
            { field: 'solfecsol', caption: 'Fec. solicitud', size: '110px', resizable: true },
            { field: 'US_UNIENL', caption: 'Unid. Tranp.', size: '110px', resizable: true },
            { field: 'solotromod', caption: 'Otro modo entrega', size: '110px', resizable: true },
            { field: 'soldes', caption: 'Descripción', size: '300px', resizable: true },
            { field: 'soldat', caption: 'Otros Datos', size: '300px', resizable: true },
            { field: 'solarcdes', caption: 'Archivo descripcion', size: '200px', resizable: true },
            { field: 'solfecrec', caption: 'Fecha recepción', size: '140px', resizable: true },
            { field: 'metclave', caption: 'Medio entrda', size: '110px', resizable: true },
            { field: 'solfecent', caption: 'Fecha entrada', size: '110px', resizable: true },
            { field: 'solfecenv', caption: 'Fecha envio', size: '110px', resizable: true },
            { field: 'solfecresp', caption: 'Fecha respuesta', size: '100px', resizable: true },
            { field: 'solrespclave', caption: 'Id respuesta', size: '200px', resizable: true },
            { field: 'sotclave', caption: 'Tipo solicitud', size: '200px', resizable: true },
            { field: 'solotroderacc', caption: 'Otro derecho', size: '200px', resizable: true },
            { field: 'solmotdesecha', caption: 'Motivo desechar', size: '200px', resizable: true },
            { field: 'solnotificado', caption: 'solnotificado', size: '110px', resizable: true },
            { field: 'sntclave', caption: 'solicitante ', size: '1100px', resizable: true },
            { field: 'IDFORMAENTREGA', caption: 'Forma entrega', size: '1100px', resizable: true },
            { field: 'megclave', caption: 'Modo entrega', size: '110px', resizable: true },
            { field: 'KRT_CLATEMA', caption: 'Tema', size: '100px', resizable: true },
            { field: 'solfecacl', caption: 'Fec aclaracion', size: '100px', resizable: true },
            { field: 'prcclave', caption: 'Proceso', size: '200px', resizable: true },
            { field: 'pczclave', caption: 'Tipo de plazo', size: '200px', resizable: true },
            { field: 'solfecrecrev', caption: 'Recurso revisión', size: '100px', resizable: true }
        ],
        toolbar: {
            items: oToolBarItems,
            onClick: function (event) {
                if (event.target === "cmdAgregar" || event.target === "cmdEditar" || event.target === "cmdBorrar") {
                    var sTitulo = AsignarCmd(event.target) + " solicitud";
                    w2popup.load({
                        url: 'PVsolicitud', showMax: true, title: sTitulo, width: 1000, height: 690, model: true,
                        buttons: '<button class="btn" onclick="w2popup.close();">Cerrar</button> <button class="btn" onclick="GuardarSolicitud();">Ejecutar</button>',
                        onOpen: function (event) {
                            event.onComplete = function () {
                                var iOpc = parseInt($('#txtCmd').val());
                                if (iOpc > 1) {
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#solclave").val(_RegistroActual.solclave);
                                    $("#solfecsol").val(_RegistroActual.solfecsol);
                                    $("#us_unienl").val(_RegistroActual.US_UNIENL);
                                    $("#solotromod").val(_RegistroActual.solotromod);
                                    $("#soldes").val(_RegistroActual.soldes);
                                    $("#soldat").val(_RegistroActual.soldat);
                                    $("#solarcdes").val(_RegistroActual.solarcdes);
                                    $("#solfecrec").val(_RegistroActual.solfecrec);
                                    $("#metclave").val(_RegistroActual.metclave);
                                    $("#solfecent").val(_RegistroActual.solfecent);
                                    $("#solfecenv").val(_RegistroActual.solfecenv);
                                    $("#solfecresp").val(_RegistroActual.solfecresp);
                                    $("#solrespclave").val(_RegistroActual.solrespclave);
                                    $("#sotclave").val(_RegistroActual.sotclave);
                                    $("#solotroderacc'").val(_RegistroActual.solotroderacc);
                                    $("#solmotdesecha").val(_RegistroActual.solmotdesecha);
                                    $("#solnotificado").val(_RegistroActual.solnotificado);
                                    $("#sntclave").val(_RegistroActual.sntclave);
                                    $("#idformaentrega").val(_RegistroActual.IDFORMAENTREGA);
                                    $("#megclave").val(_RegistroActual.megclave);
                                    $("#krt_clatema").val(_RegistroActual.KRT_CLATEMA);
                                    $("#solfecacl").val(_RegistroActual.solfecacl);
                                    $("#prcclave").val(_RegistroActual.prcclave);
                                    $("#pczclave").val(_RegistroActual.pczclave);
                                    $("#solfecrecrev").val(_RegistroActual.solfecrecrev);
                                    if (iOpc === 3) {
                                        $('#solclave').prop('readonly', true);
                                        $('#solclave').prop('readonly', true);
                                        $('#solfecsol').prop('readonly', true);
                                        $('#us_unienl').prop('readonly', true);
                                        $('#solotromod').prop('readonly', true);
                                        $('#soldes').prop('readonly', true);
                                        $('#soldat').prop('readonly', true);
                                        $('#solarcdes').prop('readonly', true);
                                        $('#solfecrec').prop('readonly', true);
                                        $('#metclave').prop('readonly', true);
                                        $('#solfecent').prop('readonly', true);
                                        $('#solfecenv').prop('readonly', true);
                                        $('#solfecresp').prop('readonly', true);
                                        $('#solrespclave').prop('readonly', true);
                                        $('#sotclave').prop('readonly', true);
                                        $('#solotroderacc').prop('readonly', true);
                                        $('#solmotdesecha').prop('readonly', true);
                                        $('#solnotificado').prop('readonly', true);
                                        $('#sntclave').prop('readonly', true);
                                        $('#idformaentrega').prop('readonly', true);
                                        $('#megclave').prop('readonly', true);
                                        $('#krt_clatema').prop('readonly', true);
                                        $('#solfecacl').prop('readonly', true);
                                        $('#prcclave').prop('readonly', true);
                                        $('#pczclave').prop('readonly', true);
                                        $('#solfecrecrev').prop('readonly', true);
                                    }
                                }
                            };
                        }
                    });
                }
            }
        },
        onClick: function (event) {
            _RegistroActual = this.get(event.recid);
        },
        onDblClick: function (event) {
            _RegistroActual = this.get(event.recid);
            var el = w2ui['gridCatSolicitud_toolbar']; if (el) el.click('cmdEditar', event);
        }
    };
    return gridCatSolicitud;
}