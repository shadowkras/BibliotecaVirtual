<p>Está é a <b>Biblioteca Virtual</b>. Um projeto utilizando Asp.NET Core, com EntityFramework e utilizando arquitetura DDD (Domain-Driven Design).</p>
<br />
<p>Ela foi construída por <a href="emailto:lsr.sena@gmail.com">Leonardo Sena</a>, e pode ser encontrado no <a href="https://github.com/shadowkras/BibliotecaVirtual">Github</a> para download.</p>

<p>O projeto foi divido em três camadas, Presentation (views, arquivos estáticos e controllers), Application (serviços, view models e mensagens para o usuário) e por fim, Data (entidades, o mapeamento destas, repositórios, contexto e migrations). Estas camadas seguem os princípios do DDD, com a informação navegando da view, para os serviços, para os repositórios, e finalmente chegando ao banco de dados.</p>

<p>A aplicação é bem simples, e constitui de quatro cadastros com a finalidade do usuário ter uma lista de livros para pesquisar por titulo e autor.</p>

