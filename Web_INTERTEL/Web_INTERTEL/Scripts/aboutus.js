$(document).ready(function () {
    $('#tbRates').DataTable({
        /* 'order': [[1, 'asc']],
         'columnDefs': [
         {
             'targets': 1,
             'visible': false,
             'searchable': false
         },
         ],*/
        "lengthMenu": [[10, 10, 20, -1], [10, 10, 20, "Todos"]],
    });
    //$("#tbRates_length").prepend("<button style='margin-right:19px;' type='button' onclick='nwRate()' title='Agregar tarifa' class='btn btn-primary'>+ Agregar tarifa</button>");
});

function EditX(idDistrito, idMovilizador) {

    $('#cont_hIdDistrito').val(idDistrito);
    $('#cont_hIdMovilizador').val(idMovilizador);

    document.getElementById("cphReporte_btnReporte").click();
}

function alertExitDelete() {
    sweetAlert('Éxito', 'Registro eliminado.', 'success');
}
function alertExit() {
    sweetAlert('Éxito', 'Registro guardado.', 'success');
}