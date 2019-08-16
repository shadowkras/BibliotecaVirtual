/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/categoria.js
  */
'use strict';

function categoria()
{
    if ($('#selectCategory').length)
    {
        $('#selectPublisher').change(function ()
        {
            debugger;
            if (e.target.options.selectedIndex > -1)
            {
                let arr = []
                let data = e.target.options[e.target.options.selectedIndex].value;
                categoria.vueSelect.selectedValues.push(data)

                arr = categoria.vueSelect.selectedValues.filter(value => value !== data)

                console.log(arr)
            }
        });

        categoria.vueSelect = new Vue({
            el: '#selectCategory',
            data: {
                selectCategory: [],
                selectedValues: [],
                loaded: false,
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
                    this.loaded = true;
                    this.$forceUpdate();
                },
                isEmpty: function ()
                {
                    return this.selectCategory.length == 0 || false;
                },
                setSelectedValues: function (value)
                {
                    if (loaded == false && typeof value === 'string' || value instanceof String)
                    {
                        if (value.search(',') !== -1)
                        {
                            var values = value.split(',');
                            this.selectedValues = values;
                        }
                        else
                        {
                            var values = [value];
                            this.selectedValues = values;
                        }
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