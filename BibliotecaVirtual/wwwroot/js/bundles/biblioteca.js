/* Fonte: js/biblioteca/image-dropzone-input.js */
$(document).ready(function ()
{
    // Declarações de variaveis que poderão ser reutilizadas no decorrer do script.

    let textoImagemTitulo = "";

    // Declarações de métodos de inicialização do componente.

    if ($('.img-output').length)
    {
        $('.img-output').each(function (i, elemento)
        {
            let dropzone = $(this).parent().find('.imagem-dropzone');
            if (dropzone.length)
            {
                let valorOutput = dropzone.parent().find('.img-output').val();

                if (valorOutput.length)
                {
                    $img = $('<img />').attr('src', valorOutput).fadeIn();
                    dropzone.find('.imagem-dropzone-texto').hide();
                    dropzone.find('.imagem-dropzone-src').html($img);
                    dropzone.find(".imagem-dropzone-btn-remover").show();
                    textoImagemTitulo = "Imagem selecionada"
                    dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                } else
                {
                    textoImagemTitulo = "Nenhuma imagem selecionada"
                    dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                }
            }
        });
    };

    // Declarações de métodos acionados por meio de clicks.

    $('.imagem-dropzone .imagem-dropzone-btn-remover').click(function (e)
    {
        let dropzone = $(this).parents('.imagem-dropzone');
        if (dropzone.length)
        {
            smartMessageBox.SimNao(e, 'Remover imagem?', 'Deseja realmente remover a imagem?', function ()
            {
                dropzone.find('.imagem-dropzone-texto').show();
                dropzone.find('.imagem-dropzone-src img').removeAttr("src").attr("src", "");
                dropzone.parent().find('.img-output').attr("value", "");
                dropzone.find('.imagem-dropzone-borda input[type="file"]').val(null);
                dropzone.find(".imagem-dropzone-btn-remover").hide();
                textoImagemTitulo = "Nenhuma imagem selecionada"
                dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
            });
        }

    });

    // Declaração de métodos acionados por meio de drags over/leave e changes.

    $('.imagem-dropzone .imagem-dropzone-borda').on('dragover', function ()
    {
        $(this).addClass('hover');
    });

    $('.imagem-dropzone .imagem-dropzone-borda').on('dragleave', function ()
    {
        $(this).removeClass('hover');
    });


    $('.imagem-dropzone input[type="file"]').on('change', function (e)
    {
        let dropzone = $(this).parents('.imagem-dropzone');

        if (dropzone.length)
        {
            if (this.files.length > 0)
            {
                var file = this.files[0];

                if (this.accept && $.inArray(file.type, this.accept.split(/, ?/)) === -1) 
                {
                    smartBox.Aviso(null, 'Cancelado!', 'Verifique o formato do arquivo e tente novamente.', 6000);
                    dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                    dropzone.find('.imagem-dropzone-borda').removeClass('hover');

                    return;
                }

                dropzone.find('.imagem-dropzone-borda').removeClass('hover');
                dropzone.find('.imagem-dropzone-texto').hide();

                dropzone.find('.imagem-dropzone-borda').addClass('dropped');

                if (dropzone.find('.imagem-dropzone-src img').length)
                {
                    dropzone.find('.imagem-dropzone-src').removeAttr('img');
                }

                if ((/^image\/(jpg|png|jpeg)$/i).test(file.type)) 
                {
                    var reader = new FileReader(file);
                    reader.readAsDataURL(file);

                    reader.onload = function (e)
                    {
                        var data = e.target.result,
                            $img = $('<img />').attr('src', data).fadeIn();

                        dropzone.find('.imagem-dropzone-src').html($img);

                        var dataUrl = data.split(',')[1];
                        dropzone.parent().find('.img-output').attr("value", dataUrl);
                        dropzone.find(".imagem-dropzone-btn-remover").show();
                        textoImagemTitulo = "Imagem selecionada"
                        dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                    };
                }
                else
                {
                    var ext = file.name.split('.').pop();
                    dropzone.find('.imagem-dropzone-src').html(ext);
                }
            }
        }
    });
});
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var site = {};
site.url = location.protocol + "//" + location.host + "/";

/** Congela o objeto para prevenir manipulação. */
Object.freeze(site);
/** Objeto que cria as as tooltips na aplicação.
  * Fonte: js/biblioteca/tooltips.js
 */
function tooltips()
{
    /** Cria as tooltips em todos os componentes com a a classe Jquery informada.
     * @param {string} jquery jQuery para encontrar o componente html. (Exemplo: '.tree')
     */
    tooltips.atualizar = function atualizar(jquery)
    {
        var tooltipOptions = {
            delay:
            {
                show: 500,
                hide: 200
            },
        };
        $(jquery).not('[rel*=popover]').tooltip(tooltipOptions);
    }

    $(document).ready(function ()
    {
        /** Cria as tooltips em todos os componentes com a propriedade 'data-original-title'. */
        tooltips.atualizar('[data-original-title]');
    });
}
tooltips();