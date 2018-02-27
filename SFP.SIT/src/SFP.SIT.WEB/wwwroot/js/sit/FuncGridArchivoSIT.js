/*


            <div id="divAnexarArchivos"> 
                <label style="margin:1%;">Anexar archivos :&nbsp;</label>
                <button id="btnAgregar" name="btnAgregar" style="width:120px"> Agregar</button>
                <button id="btnEliminar" name="btnEliminar"  style="width:120px">Eliminar</button>
                <button id="btnPdf" name="btnPdf" style="width:120px; float:right">PDF Texto</button>

                <div id="divGridArchivo" style="margin-left:1%; width: 99%; height: 12vh;"> </div>                                
            </div>

@* --------------------------------------------
        FORMA PARA SELECCIONAR ARCHIVOS 
-------------------------------------------- *@
<div id="dialog-form-Archivo" style="display: none; width: 100%; height: 100%; overflow: hidden;">
    <form id="formImportar" enctype="multipart/form-data">
        <div rel="body">
            <table style="padding-top: 10px; width:100% ">
                <tr>
                    <td style="width: 60px;"> Fecha :</td>
<td>
    <input type="date" id="txtFechaArchivo" name="txtFechaArchivo">
</td>
</tr>
<tr>
<td style="width: 60px;"> No. de oficio :</td>
<td>
    <input type="text" id="txtOficio" name="txtOficio">
</td>
</tr>
<tr>
<td style="width: 60px;"> Seleccionar archivo :</td>
<td>                                
    <input id="ArchivoResolucion" type="file" name="ArchivoResolucion" 
accept="text/richtext, application/zip, image/jpeg, application/pdf, application/msword, application/vnd.ms-excel, 
application/vnd.ms-excel.sheet.macroEnabled.12, application/vnd.ms-powerpoint" style="width: 70%;" />
</td>
</tr>
</table>
</div>
</form>
</div>


            gridArchivo: {
                name: 'gridArchivo',
                method: 'GET', // need this to avoid 412 error on Safari
                columns: [
                    { field: 'Fecha', caption: 'Fecha', size: '110px' },
                    { field: 'Oficio', caption: 'No. Oficio', size: '150px' },
                    { field: 'Nombre', caption: 'Nombre del archivo', size: '500px' }
                ],
                onClick: function (event) {
                    var record = this.get(event.recid);
                    if (record !== null){
                        var myWindow = window.open("/Consulta/DocumentoObtener?Nombre=" + record.Nombre);
                    }
                }
            },


            $().w2grid(objetos.gridArchivo);
            w2ui['gridArchivo'].clear();
            $('#divGridArchivo').show();
            $("#divGridArchivo").w2render('gridArchivo');

            $('#btnEliminar').click(function() {
                w2ui.gridArchivo.delete(true);
            });

            $('#btnAgregar').click(function() {
                w2popup.open({
                    title: "Anexar un archivo ",
                    showMax: true,
                    modal: true,
                    height:500,
                    width:800,
                    buttons : '<button class="btn" onclick="agregarDocumento();">Aceptar</button> ' +
                                '<button class="btn" onclick="w2popup.close();">Cerrar</button> ',
                    onOpen: function (event) {
                        event.onComplete = function () {
                            var element = $("#dialog-form-Archivo").detach();
                            $('#w2ui-popup .w2ui-msg-body').append(element);
                            $("#dialog-form-Archivo").show();
                        };
                    },
                    onClose: function(event){
                        var element = $("#dialog-form-Archivo").detach();
                        $("#Aux").append(element);
                        $("#dialog-form-Archivo").hide();
                    }
                });
            });

*/


function agregarDocumento()
{
    var sampleFile = document.getElementById("ArchivoResolucion").files[0];
    var formData = new FormData();
    formData.append("ArchivoResolucion", sampleFile);
    formData.append("Folio", _solclave);

    $.ajax({
        url: "@Html.Raw(@Url.Action('UploadArchivo', 'Solicitud', null))",
        type: 'POST',
        data: formData,
        mimeType: "multipart/form-data",
        contentType: false,
        cache: false,
        processData: false,
        success: function (data) {
            if (data !== undefined)
            {
                var sNombre = $("#ArchivoResolucion").val();
                var n = sNombre.lastIndexOf("\\");
                sNombre = sNombre.substring(n+1);

                w2ui['gridArchivo'].add({ recid: 1,
                    Fecha: $("#txtFechaArchivo").val(),
                    Oficio:$("#txtOficio").val(),
                    Nombre: sNombre });
                w2popup.close();
            } else {
                w2alert("El archivo no tiene solicitudes a importar");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

