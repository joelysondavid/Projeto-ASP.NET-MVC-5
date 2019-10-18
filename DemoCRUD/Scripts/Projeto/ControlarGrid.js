// função para configuração da listagem
function configurarControles() {
    // quando o botao com id 'btnPesquisar' for clicado irá executar listar livros
    // $("#btnPesquisar").click(listarLivros);
    // listarLivros();
    var traducao = {
        all: "Todos",
        infos: "Exibindo {{ctx.start}} itens até {{ctx.end}} itens de {{ctx.total}} registros total",
        loading: "Carregando, pode demorar alguns segundos...",
        noResults: "Nenhum resultado Encontrado!",
        refresh: "Recarregar",
        search: "Procurar"
    }

    var grid = $("#gridDados").bootgrid({
        ajax: true,
        url: urlListar,/* "@Url.Action("Listar")",*/
        labels: traducao,
        searchSettings: {
            delay: 600
        },
        // formatador para as opcoes de editar, ver detalhes e excluir
        formatters: {
            "acoes": function (column, row) {
                return "<a href='#' class='btn-sm btn-primary' data-acao='Details' data-row-id='" + row.Id + "'><span class='glyphicon glyphicon-list'></span></a> " +
                    "<a href='#' class='btn-sm btn-warning' data-acao='Edit' data-row-id='" + row.Id + "'><span class='glyphicon glyphicon-edit'></span></a> " +
                    "<a href='#' class='btn-sm btn-danger' data-acao='Delete' data-row-id='" + row.Id + "'><span class='glyphicon glyphicon-trash'></span></a>";
            }
        }
    });

    // apos os intes serem carregados, ele chama o bootgrid para que ative os botões de opções, permitindo a eles executarem tarefas
    grid.on("loaded.rs.jquery.bootgrid", function () {
        // através do grid find é possivel obter os elementos e assim para cada elemento do tipo procurado atribuir tarefas
        grid.find("a.btn-sm").each(function (index, elemento) {
            // pega o elemento do parametro
            var botaoDeAcao = $(elemento);
            // através de 'propriedades personalizadas' (data) podemos encontrar a o valor que estamos procurando
            var acao = botaoDeAcao.data("acao"); // valor acao
            var idEntidade = botaoDeAcao.data("row-id"); // valor row-id
            // para que o botao possa executar a tarefa é necessário atribuir a ação 'click' dele
            botaoDeAcao.on("click", function () {
                abrirModal(acao, idEntidade); // onde ele irá executar a acao e o id passado nos parametros
            });
        });
    });

    // todas as tag a da class btn realizarao esta funcao quando forem clicada
    $("a.btn").click(function () {
        var acao = $(this).data("action");

        abrirModal(acao);
    });
}

// chamada para abertura do modal juntamente com o metodo create da controller
function abrirModal(acao, param) {
    var url = "/{ctrl}/{acao}/{param}";
    url = url.replace("{ctrl}", controller);
    url = url.replace("{acao}", acao);

    if (param != null) {
        url = url.replace("{param}", param);
    } else {
        url = url.replace("{param}", "");
    }

    $("#conteudoModal").load(url, function () {
        $("#minhaModal").modal('show');
    });
}
/* com uso do bootgrid isso torna - se desnecessário
// função da paginação
function paginar(valor) {

    // obtem o elemento com name 'pagina'
    var elementoPag = $("input[name='pag']");
    // elementopag.val retorna o valor que está no elemento
    var pagAtual = parseInt(elementoPag.val());
    // atribue a soma entre a pagina atual e o valor passado no param para o elemento input
    elementoPag.val(pagAtual + valor);

    listarLivros();
}

function listarLivros() {
    // pega o item com id 'listagemLivros' e carrega a action 'Listar'
    $("#listagemLivros").load("@Url.Action("Listar")", $("form").serialize());
}*/