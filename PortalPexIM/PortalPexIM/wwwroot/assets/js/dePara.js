var SiglaEstado = "";
var CodEstado = 0;
var CodPalavra = 0;
var CodCidade = 0;
var tabsExec = 0;
(function () {
    $(document).ready(function () {

        $(".modalSalvar").hide();

        var tabCidade = $("#tab-content1"),
            tabBairro = $("#tab-content2"),
            tabTipo = $("#tab-content3"),
            deparaselected = false,
            adicionarTermo = $(".addTermo"),
            btnsOk = $(".bOk"),
            btnAssociaTermoSelecionado = $("#AssociaTermoSelecionado"),
            btnDesassociaTermoSelecionado = $("#DesassociaTermoSelecionado"),
            btnAssociaTermoSelecionadoMult = $("#AssociaTermoSelecionadoMult"),
            btnDesassociaTermoSelecionadoMult = $("#DesassociaTermoSelecionadoMult"),
            btnSalvar = $("#btnSalvar"),
            btnFechar = $("#btnFechar");

        var msgErro = function (msg) {
            if (!msg) {
                console.log("Preencha o termo antes de adicionar.");
            } else {
                console.log("Selecione ao menos o " + msg + " na esquerda.");
            }
        };

        //Adicionando Termos
        var adicionaTermoCidade = function (termo) {
            //  AjaxLoad(true);
            $.ajax({
                type: 'POST',
                url: '/dePara/adicionaTermoCidade',
                traditional: true,
                data: termo,
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {
                    //  AjaxLoad(false);
                },
                complete: function (data) {
                    //   AjaxLoad(false);
                }
            });

        };
        var adicionaTermoBairro = function (termo) {
            //   AjaxLoad(true);
            $.ajax({
                type: 'POST',
                url: '/dePara/adicionaTermoBairro',
                traditional: true,
                data: termo,
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {
                    //   AjaxLoad(false);
                },
                complete: function (data) {
                    //       AjaxLoad(false);
                }
            });
        };
        var adicionaTermoTipo = function (termo) {
            //   AjaxLoad(true);
            $.ajax({
                type: 'POST',
                url: '/dePara/adicionaTermoTipo',
                traditional: true,
                data: termo,
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {
                    //    AjaxLoad(false);
                },
                complete: function (data) {
                    //    AjaxLoad(false);
                }
            });
        };
        adicionarTermo.on("click", function () {
            switch (this.id) {
                case "addTermoTipo": $("#tab-content3 > input").val() ? adicionaTermoTipo() : msgErro(); break;
                case "addTermoBairro": $("#tab-content2 > input").val() ? adicionaTermoBairro() : msgErro(); break;
                case "addTermoCidade": $("#tab-content1 > input").val() ? adicionaTermoCidade() : msgErro(); break;
                default: break;
            }
        });

        //Redirecionamneto pra pagina de cadastro do depara
        $("#addTermoTipo").on("click", function () { redirecionarCadastrar('tipos') });
        $("#addTermoBairro").on("click", function () { redirecionarCadastrar('bairros') });
        $("#addTermoCidade").on("click", function () { redirecionarCadastrar('cidades') });

        //Redirecionamento pra pagina de editar
        $("#editTermoTipo").on("click", function () { redirecionarEditar('tipos') });
        $("#editTermoBairro").on("click", function () { redirecionarEditar('bairros') });
        $("#editTermoCidade").on("click", function () { redirecionarEditar('cidades') });


        //Selecionando o DE/PARA a partir da Cidade/Bairro/Tipo
        var populaListaDeParaCidades = function (cod) {

            $.ajax({
                type: 'POST',
                url: '/DePara/RetornarCidadesDePara',
                traditional: true,
                //data: JSON.stringify({ 'codEstado': cod[0] }),
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {
                    //aqui remover as já associadas

                    var html = '';
                    $(data.cidades).each(function (indBairro, elem) {

                        html += '<option value="' + elem.nomeCidade + '" data-codcidade="' + 1 + '">' + elem.nomeCidade + '</option>';
                    })

                    $("#termosNaoAssociados").html(html);
                    //    AjaxLoad(false);
                },
                complete: function (data) {
                    //    AjaxLoad(false);
                }
            });//Adicionar trecho abaixo nos outros 3 get e inserir dentro do success quando tiver backend
            $(".config button").show();
            $(".termosNaoAssociados").show();
            $(".termosAssociados").show();
            //$(".assoc").css('display', 'block');
        };


        var populaListaDeParaTipos = function (cod) {
            //AjaxLoad(true);
            $.ajax({
                type: 'POST',
                url: '/DePara/RetornarTipos',
                traditional: true,
                //data: JSON.stringify({ 'codEstado': cod[0] }),
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {

                    var html = '';
                    $(data).each(function (indBairro, elem) {

                        html += '<option value="' + elem.nomeTipo + '" data-codcidade="' + 1 + '">' + elem.nomeTipo + '</option>';
                    })

                    $("#termosNaoAssociados").html(html);
                    // AjaxLoad(false);
                },
                complete: function (data) {
                    //   AjaxLoad(false);
                }
            });//Adicionar trecho abaixo nos outros 3 get e inserir dentro do success quando tiver backend
            $(".config button").show();
            $(".termosNaoAssociados").show();
            $(".termosAssociados").show();
            //$(".assoc").css('display', 'block');
        };

        var populaListaDeParaBairros = function (cod) {
            //debugger;
            //AjaxLoad(true);
            $.ajax({
                type: 'POST',
                url: '/DePara/RetornarBairros',
                traditional: true,
               // data: JSON.stringify({ 'codEstado': cod[0] }),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (data) {
                    debugger;

                    var html = '';
                    $(data).each(function (indBairro, elem) {

                        if (elem.nomeBairro)
                            html += '<option value="' + elem.bairroStr  + '" data-codcidade="' + 1 + '">' + elem.cidade + ' - '+ elem.bairroStr + '</option>';
                    })
                    // split(" ")
                    $("#termosNaoAssociados").html(html);
                    //  AjaxLoad(false);
                },
                complete: function (data) {
                    // AjaxLoad(false);
                }
            });//Adicionar trecho abaixo nos outros 3 get e inserir dentro do success quando tiver backend
            $(".config button").show();
            $(".termosNaoAssociados").show();
            $(".termosAssociados").show();
            //$(".assoc").css('display', 'block');
        };



        //var populaListaDeParaBairros = function (cod) {
        //    AjaxLoad(true);
        //    $.ajax({
        //        type: 'POST',
        //        url: '/dePara/getListaDeParaBairros',
        //        traditional: true,
        //        data: cod,
        //        dataType: "json",
        //        contentType: "application/json",
        //        async: true,
        //        success: function (data) {
        //            AjaxLoad(false);
        //        },
        //        complete: function (data) {
        //            AjaxLoad(false);
        //        }
        //    });
        //};
        //var populaListaDeParaTipos = function (cod) {
        //    AjaxLoad(true);
        //    $.ajax({
        //        type: 'POST',
        //        url: '/dePara/getListaDeParaTipos',
        //        traditional: true,
        //        data: cod,
        //        dataType: "json",
        //        contentType: "application/json",
        //        async: true,
        //        success: function (data) {
        //            AjaxLoad(false);
        //        },
        //        complete: function (data) {
        //            AjaxLoad(false);
        //        }
        //    });
        //};

        var populaListaPalavrasRelacionadasCidades = function (cod) {
            // AjaxLoad(true);

            $.ajax({
                type: 'POST',
                url: '/DePara/PopulaListaPalavrasRelacionadasCidades?cod=' + cod,
                traditional: true,
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {
                    var html = '';
                    $(data).each(function (indBairro, elem) {

                        html += '<option value="' + elem.palavraRelacionada + '" data-codcidade="' + 1 + '">' + elem.palavraRelacionada + '</option>';
                    })

                    $("#termosAssociados").html(html);
                
                },
                complete: function (data) {
             
                }
            });//Adicionar trecho abaixo nos outros 3 get e inserir dentro do success quando tiver backend
            $(".config button").show();
            $(".termosNaoAssociados").show();
            $(".termosAssociados").show();
        }

        var populaListaPalavrasRelacionadasBairros = function (cod) {

            $.ajax({
                type: 'POST',
                url: '/DePara/PopulaListaPalavrasRelacionadasBairros?cod=' + cod,
                traditional: true,
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {
                    var html = ''; debugger;
                    $(data).each(function (indBairro, elem) {

                        html += '<option value="' + elem.palavraRelacionada + '" data-codcidade="' + 1 + '">' + elem.palavraRelacionada + '</option>';
                    })

                    $("#termosAssociados").html(html);
      
                },
                complete: function (data) {
        
                }
            });//Adicionar trecho abaixo nos outros 3 get e inserir dentro do success quando tiver backend
            $(".config button").show();
            $(".termosNaoAssociados").show();
            $(".termosAssociados").show();
        }

        var populaListaPalavrasRelacionadasTipos = function (cod) {
            //AjaxLoad(true);

            $.ajax({
                type: 'POST',

                url: '/DePara/PopulaListaPalavrasRelacionadasTipos?cod=' + cod,
                traditional: true,
                //data: JSON.stringify({ cod: cod }),
                dataType: "json",
                contentType: "application/json",
                async: true,
                success: function (data) {
                    var html = '';
                    $(data).each(function (indBairro, elem) {

                        html += '<option value="' + elem.palavraRelacionada + '" data-codcidade="' + 1 + '">' + elem.palavraRelacionada + '</option>';
                    })

                    $("#termosAssociados").html(html);
                    //   AjaxLoad(false);
                },
                complete: function (data) {
                    //   AjaxLoad(false);
                }
            });//Adicionar trecho abaixo nos outros 3 get e inserir dentro do success quando tiver backend
            $(".config button").show();
            $(".termosNaoAssociados").show();
            $(".termosAssociados").show();
            //$(".assoc").css('display', 'block');
        }

        btnsOk.on("click", function () {
      

            switch (this.id) {
                case "okTipo": $("#paraTipo option:selected").length > 0 ? function () {
                    CodPalavra = $("#paraTipo option:selected").val();
                    populaListaPalavrasRelacionadasTipos($("#paraTipo option:selected").val());

                }() : msgErro('tipo'); break;
                case "okBairro": $("#paraBairro option:selected").length > 0 ? function () {
                    CodPalavra = $("#paraBairro option:selected").val();
                    populaListaPalavrasRelacionadasBairros($("#paraBairro option:selected").val());
                }() : msgErro('bairro'); break;
                case "okCidade": $("#paraCidade option:selected").length > 0 ? function () {

                    CodPalavra = $("#paraCidade option:selected").val();
                    populaListaPalavrasRelacionadasCidades($("#paraCidade option:selected").val());
                }() : msgErro('cidade'); break;
                default: break;
            }
        });

        btnAssociaTermoSelecionado.on("click", function () {

            if (CodPalavra > 0) {
                var html = $("#termosAssociados").html();
                html += '<option value="' + $("#termosNaoAssociados option:selected").val() + '" data-codcidade="' + 1 + '">' + $("#termosNaoAssociados option:selected").val() + '</option>';

                $("#termosAssociados").html(html);

                var naoassociados = $("#termosNaoAssociados option:selected");
                if (naoassociados.length == 1)
                    $("#termosNaoAssociados option:selected").remove();
            }
            else {
                alert('Selecione uma cidade, bairro ou tipo antes de clicar no botão de transferência de termos.');
            }

        });

        btnDesassociaTermoSelecionado.on("click", function () {
            if (CodPalavra > 0) {
                var associados = $("#termosAssociados option:selected");
                if (associados.length == 1)
                    $("#termosAssociados option:selected").remove();

            }

        });

        btnAssociaTermoSelecionadoMult.on("click", function () {

  
            if (CodPalavra > 0) {

                var data = $("#termosNaoAssociados option:selected");
                var html = $("#termosAssociados").html();
                $(data).each(function (indBairro, elem) {

                    html += '<option value="' + elem.value + '" data-codcidade="' + 1 + '">' + elem.value + '</option>';
                })

                $("#termosAssociados").html(html);


                $("#termosNaoAssociados option:selected").remove();
            }
            else {
                alert('Selecione uma cidade, bairro ou tipo antes de clicar no botão de transferência de termos.');
            }
        });

        btnDesassociaTermoSelecionadoMult.on("click", function () {
            if (CodPalavra > 0) {

                $("#termosAssociados option:selected").remove();
            }

        });

        btnSalvar.on("click", function () {
    

            switch (tabsExec) {
                case 1:
                    salvarCidade(CodPalavra)
                    AplicarDePara(CodPalavra, tabsExec);
                    break;

                case 2:
                    salvarBairro(CodPalavra)
                    AplicarDePara(CodPalavra, tabsExec);
                    break;

                case 3:
                    salvarTipo(CodPalavra)
                    AplicarDePara(CodPalavra, tabsExec);
                    break;


                default:
                    break;
            };

  
        });

        btnFechar.on("click", function () {

            $(".modalSalvar").fadeOut(150);
        });
        //Show e hide das tabs
        $("#tab1").on("click", function () {

            tabsExec = 1;
            CodPalavra = 0;
            $("#termosAssociados").html("");
            $("#termosNaoAssociados").html("");

            atualizarTabelaCidade();
            populaListaDeParaCidades();


            deparaselected ? function () { tabBairro.hide(); tabTipo.hide(); tabCidade.show(); }() : 0;
        });
        $("#tab2").on("click", function () {

            tabsExec = 2;
            CodPalavra = 0;
            $("#termosAssociados").html("");
            $("#termosNaoAssociados").html("");

       
            atualizarTabelaCidadeBairro();
            atualizarTabelaBairro([0]);

            populaListaDeParaBairros();

            deparaselected ? function () { tabTipo.hide(); tabCidade.hide(); tabBairro.show(); }() : 0;

        });
        $("#tab3").on("click", function () {
            debugger;
            tabsExec = 3;
            CodPalavra = 0;
            $("#termosAssociados").html("");
            $("#termosNaoAssociados").html("");
 

            atualizarTabelaTipo();

            populaListaDeParaTipos();

            //deparaselected ? function () {
            //    tabBairro.hide();
            //    tabCidade.hide();
            //    tabTipo.show();
            //}() : 0;

        });





        //$("#paraCidade").multipleSelect({
        //    placeholder: "Selecione uma cidade",
        //    single: true,
        //    filter: true
        //});

        //$("#paraBairro").multipleSelect({
        //    placeholder: "Selecione um bairro",
        //    single: true,
        //    filter: true
        //});

        //$("#paraBairroCidade").multipleSelect({
        //    placeholder: "Selecione uma cidade",
        //    single: true,
        //    filter: true,
        //    onClose: function () {
        //        //  SiglaEstado = $('#paraBairroCidade').multipleSelect('getSelects', 'value');
        //        CodCidade = $('#paraBairroCidade').multipleSelect('getSelects', 'value');
        //        //debugger;
        //        atualizarTabelaBairro(CodCidade);


        //    }
        //});

        //$("#paraTipo").multipleSelect({
        //    placeholder: "Selecione um tipo",
        //    single: true,
        //    filter: true
        //});

        //Esconde as tabs e espera pelo click no Secovi
        //tabCidade.hide();
        //tabBairro.hide();
        //tabTipo.hide();



        atualizarTabelaCidade();
        populaListaDeParaCidades();

        $('#paraBairroCidade').on('change', function () {
            alert(this.value);
            atualizarTabelaBairro(parseInt(this.value))
        });

    });
}());


function atualizarTabelaCidade(cod) {

    $.ajax({
        url: "DeParaCidades/RetornarDadosTabela",
        data: JSON.stringify({ numRegistros: parseInt(100000), pagina: parseInt(0), CodCidade: null }),
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        complete: function (data) {
            data.responseJSON.palavras

            var html = '';
            html += '<option value="' + "" + '" data-codcidade="' + 1 + '">' + "" + '</option>';
            $(data.responseJSON.palavras).each(function (indBairro, elem) {
                html += '<option value="' + elem.codPalavra + '" data-codcidade="' + 1 + '">' + elem.nomePalavra + '</option>';
            })

            $("#paraCidade").html(html);
        },
        error: function (data) {
     
        }
    });
}

function atualizarTabelaCidadeBairro(cod) {


    $.ajax({
        url: "DePara/RetornarCidadesBairro",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        complete: function (data) {
            var html = '';
            $(data.responseJSON).each(function (indBairro, elem) {

                html += '<option value="' + elem.codCidade + '" data-codcidade="' + 1 + '">' + elem.nomeCidade + '</option>';
            })

            $("#paraBairroCidade").html(html);

        },
        error: function (data) {

        }
    });
}

function atualizarTabelaBairro(cod) {
    // AjaxLoad(true);

    $("#paraBairro").html("");
    $.ajax({
        url: '/DePara/RetornarPalavrasBairros?cod=' + cod,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        complete: function (data) {
            var html = '';
            $(data.responseJSON).each(function (indBairro, elem) {
                html += '<option value="' + elem.codPalavra + '" data-codcidade="' + 1 + '">' + elem.nomePalavra + '</option>';
            })

            $("#paraBairro").html(html);
        },
        error: function (data) {

        }
    });
}

function atualizarTabelaTipo(cod) {

    $.ajax({
        url: "DeParaTipos/RetornarDadosTabela",
        data: JSON.stringify({ numRegistros: parseInt(100000), pagina: parseInt(0) }),
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        complete: function (data) {
            data.responseJSON.palavras
  

            var html = '';
            $(data.responseJSON.palavras).each(function (indBairro, elem) {

                html += '<option value="' + elem.codPalavra + '" data-codcidade="' + 1 + '">' + elem.nomePalavra + '</option>';
            })

            $("#paraTipo").html(html);

        },
        error: function (data) {
        }
    });
}

function getArraySelecionados(selector) {
    var opcesSelecionadas = [];

    if (selector == "#filtroCidades") {
        $("li.selected [name='selectItemCidades']").each(function () {
            opcesSelecionadas.push($(this).val());
        });
    } else {
        $(selector + ' option:selected').each(function () {
            opcesSelecionadas.push($(this).val());
        });
    }

    return opcesSelecionadas;
}

function AplicarDePara(Cod, dePara) {

    $.ajax({

        url: '/DePara/AplicarDePara?DePara=' + dePara,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        async: true,
        complete: function (data) {
            
            $(".modalSalvar").fadeIn(150);
        },
        error: function (data) {

        }
    });

}

function salvarTipo(cod) {

    var palavrasRelacionadasTipo = new Array();

    var html = '';
    $("#termosAssociados option").each(function (indBairro, elem) {
        palavrasRelacionadasTipo.push(elem.value);

    });


    $.ajax({
        url: "DePara/SalvarTipo",
        data: JSON.stringify({ CodPalavra: parseInt(cod), Palavras: palavrasRelacionadasTipo }),
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        async: false,
        complete: function (data) {
     
        },
        error: function (data) {
      
        }
    });
}


function salvarCidade(cod) {
    var palavrasRelacionadasCidade = new Array();
 
    var html = '';
    $("#termosAssociados option").each(function (indBairro, elem) {
        palavrasRelacionadasCidade.push(elem.value);

    });


    $.ajax({
        contentType: "application/json; charset=utf-8",
        url: "/DePara/SalvarCidade",
        type: "POST",
        async: true,
        dataType: "json",
        data: JSON.stringify({ CodPalavra: parseInt(cod), Palavras: palavrasRelacionadasCidade }),
        success: function (data) {

        }
    });


}

function salvarBairro(cod) {

    var palavrasRelacionadasBairro = new Array();
  
    var html = '';
    $("#termosAssociados option").each(function (indBairro, elem) {
        palavrasRelacionadasBairro.push(elem.value);

    });


    $.ajax({
        url: "DePara/SalvarBairro",
        //data: JSON.stringify({ CodPalavra: cod, Palavras: palavrasRelacionadasBairro }),
        data: JSON.stringify({ CodPalavra: parseInt(cod), Palavras: palavrasRelacionadasBairro }),
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        async: false,
        complete: function (data) {
       
        },
        error: function (data) {
        }
    });
}

function redirecionarCadastrar(tipo) {
    switch (tipo) {
        case "cidades":
            window.open('DeParaCidades/Cadastrar', '_blank');
            break;

        case "bairros":
            window.open('DeParaBairros/Cadastrar', '_blank');
            break;

        case "tipos":
            window.open('DeParaTipos/Cadastrar', '_blank');
            break;

        default:
            break;
    };
}

function redirecionarEditar(tipo) {
    switch (tipo) {
        case "cidades":
            var cidadeSelecionada = $("#paraCidade option:selected").val();
            window.open('DeParaCidades/Editar/' + cidadeSelecionada, '_blank');
            break;

        case "bairros":
            var bairroSelecionado = $("#paraBairro option:selected").val();
            window.open('DeParaBairros/Editar/' + bairroSelecionado, '_blank');
            break;

        case "tipos":
            var tipoSelecionado = $("#paraTipo option:selected").val();
            window.open('DeParaTipos/Editar/' + tipoSelecionado, '_blank');
            break;

        default:
            break;
    };
}


