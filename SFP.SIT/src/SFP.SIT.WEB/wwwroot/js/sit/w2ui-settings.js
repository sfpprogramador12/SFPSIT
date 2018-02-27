w2utils.settings = {
    locale: 'es-MX',
    date_format: 'dd/mm/yyyy',
    date_display: "Mon dd, yyyy",
    time_format: "hh:mi pm",
    currency: "^[\$\â‚¬\Â£\Â¥]?[-]?[0-9]*[\.]?[0-9]+$",
    currencySymbol: "$",
    float: "^[-]?[0-9]*[\.]?[0-9]+$",
    shortmonths: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
    fullmonths: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre","Octubre", "Noviembre", "Diciembre"],
    shortdays: ["L", "M", "Mi", "J", "V", "S", "D"],
    fulldays: ["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"],
    phrases: {
        "yesterday": "ayer",
        "none": "ninguno",
        "of": "de",
        "No items found": "No se encontraron elementos",
        "Attach files by dragging and dropping or Click to Select": "Agregue los archivos arrastrando y soltando o dando click en seleccione",
        "Remove": "Remover",
        "Not a valid date": "No es una fecha válida",
        "Required field": "Campo obligatorio",
        "Saving...": "Guardando...",
        "Not an integer": "No es un entero válido",
        "Not a float": "No es un número flotante válido",
        "Not in money format": "No es un formato de moneda válido",
        "Not a hex number": "No es un número en hexadecimal válido",
        "Not alpha-numeric": "No es un carácter válido",
        "Not a valid email": "No es un correo electrónico válido",
        "Are you sure you want to delete selected records?": "¿Está usted seguro de querer eliminar los registros seleccionados?",
        "Return data is not in JSON format. See console for more information.": "La respuesta no lleva un formato de JSON válido. Para más información consulte la consola.",
        "Refreshing...": "Recargando..",
        "All Fields": "Todos los campos",
        "Multi Fields": "Múltiples campos",
        "Delete Confirmation": "Confirmación de borrado",
        "Ok": "Aceptar",
        "Yes": "Si",
        "No": "No",
        "Reload data in the list": "Recargar los datos en el listado",
        "Show/hide columns": "Mostrar/ocultar las columnas",
        "Select Search Field": "Seleccione el campo de búsqueda",
        "Search...": "Buscando..",
        "Open Search Fields": "Abrir los campos de búsqueda",
        "is": "es",
        "begins with": "empieza con",
        "contains": "contiene",
        "ends with": "termina con",
        "between": "entre",
        "Add new record": "Agregar un nuevo registro",
        "Add New": "Agregar uno nuevo",
        "Delete selected records": "Borrar los registros seleccionados",
        "Delete": "Borrar",
        "Save changed records": "Guardar los registros modificados",
        "Save": "Guardar",
        "Reset": "Limpiar",
        "Search": "Buscar",
        "Confirmation": "Confirmación",
        "Notification": "Mensaje",
        "Show": "Mostrar",
        "Hide": "Ocultar",
        "Toggle Line Numbers": "Mostrar número de línea",
        "Reset Column Size": "Reajusta el tamaño de las columnas",
        "Column": "Columna",
        "Record ID": "ID del Registro",
        "selected": "seleccionado"
    }
};

function lock(idForm, msg) {

    w2utils.lock(idForm, msg, true);

}

function unlock(idForm) {

    w2utils.unlock(idForm);

}
