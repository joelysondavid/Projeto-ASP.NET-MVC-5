
var btnAcao = $("input[type='button']");
var form = $("#formCRUD")

btnAcao.on("click", submeter);

function submeter() {
    var url = form.prop("action"); // através deste comando armazenará a URL da actipon que será executada na variavel url
    var metodo = form.prop("method"); // com este comando o metodo do formulario será armaznado na variavel metodo

    
    // verifica se o formulario é valido
    if (form.valid()) {

        var dadosForm = form.serialize();

        $.ajax({
            url: url,
            type: metodo,
            data: dadosForm,
            success: tratarRetorno
        });
    }
}

function tratarRetorno(resultServ) {
    if (resultServ.resultado) {

        toastr['success'](resultServ.mensagem);

        $("#minhaModal").modal("hide");

        $("#gridDados").bootgrid("reload");
        return;
    } // else
    toastr['error'](resultServ.mensagem);
}

function fecharModal(modal) {

    var modal = $("#minhaModal");
    if (modal.modal("show") == true) {
        modal.modal("hide");
    }
}