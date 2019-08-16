/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/categoria.js
  */
'use strict';

function categoria()
{
    if ($('#selectCategory').length)
    {
        categoria.vueSelect = new Vue({
            el: '#selectCategory',
            data: {
                selectCategory: [],
                selectedValues: [],
            },
            methods: {
                obterOpcoes: function ()
                {
                    var endereco = site.url + 'biblioteca/category/getcategories';
                    let parametros = {
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.length)
                            {
                                let data = response;
                                categoria.vueSelect.atualizarLista(data);
                            }

                            $('#selectCategory .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.selectCategory = data;
                    this.$forceUpdate();
                },
                isEmpty: function ()
                {
                    return this.selectCategory.length == 0 || false;
                },
                selectedOptions: function (e)
                {
                    debugger;
                    if (e.target.options.selectedIndex > -1)
                    {
                        let arr = []
                        let data = e.target.options[e.target.options.selectedIndex].value
                        this.selectedItems.push(data)

                        arr = this.selectedItems.filter(value => value !== data)

                        console.log(arr)
                    }
                },
                getCategoryId: function (item)
                {
                    return item.categoryId;
                },
                getCategoryName: function (item)
                {
                    return item.description;
                },
                newCategory: function (url)
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
categoria();