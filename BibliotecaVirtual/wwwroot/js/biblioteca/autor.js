/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/autor.js
  */
'use strict';

function autor()
{
    if ($('#selectAuthor').length)
    {
        autor.vueSelect = new Vue({
            el: '#selectAuthor',
            data: {
                selectAuthor: [],
                isEmpty: this.selectPublisher.length == 0,
                selectedValue: -1,
            },
            methods: {
                obterOpcoes: function ()
                {
                    var endereco = site.url + 'biblioteca/author/getauthors';
                    let parametros = {
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.length)
                            {
                                let data = response;
                                autor.vueSelect.atualizarLista(data);
                            }

                            $('#selectAuthor .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.selectAuthor = data;
                    this.$forceUpdate();
                },
                setSelectedValue: function (value)
                {
                    if (value)
                    {
                        this.selectedValue = value;
                    }
                },
                getAuthorId: function (item)
                {
                    return item.authorId;
                },
                getAuthorName: function (item)
                {
                    return item.name;
                },
                newAuthor: function (url)
                {
                    window.open(url, '_blank');
                },
            },
            created: function ()
            {
                this.obterOpcoes();
            }
        });
    }
}
autor();