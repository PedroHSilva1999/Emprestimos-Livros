$(document).ready(function(){
    $('#Emprestimos').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No data available in table",
            "info": "Mostrando _START_ to _END_ of _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Carregando...",
            "processing": "",
            "search": "Pesquisar:",
            "zeroRecords": "Sem Resultado",
            "paginate": {
                "first": "Primeiro",
                "last": "Segundo",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    });

    setTimeout(function () {
        $('.alert').fadeOut("slow", function () {
            $('.alert').alert('close')
        })
    },500)

})