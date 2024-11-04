# AITP3 - Biblioteca - Emprestimo de Livros
# Atenção - Para iniciar corretamente: Executar Update-Package no Console de Gerenciador de Pacotes, Depois ir em Compilação > Limpar Solução, Compilação > Reconstruir Solução e Compilação > Compilar Solução 
Foi desenvolvido um sistema de aluguel de livros, Você pode se cadastrar por externo (Google e Facebook) ou dentro do programa, ir para o acervo de livros e adicionar os que lhe interessar ao carrinho. No Carrinho voce pode ir para a confirmação do emprestimo, onde ele já coloca automaticamente a data do dia + 7 dias para o retorno dos livros, alem da lista de livros que você está alugando. Pode ir então em meus emprestimos para visualizar. 

Existe também a tela de administradores, onde você pode gerenciar todos os livros, seus atributos e categorias.
No model e no banco de dados temos a relação de usuario e endereço, para uma tela de envio dos emprestimos


Contas já criadas com o seed: 
Login:
admin@biblioteca.com
Senha:
Admin123!

Login:
usuario@biblioteca.com
Senha:
Usuario123!

Relações
Usuario (ApplicationUser) 1:N Emprestimos,
Emprestimos: N:N Livros,
Livro N:1 Categoria,
Cart 1:1,
EmprestimoLivro N:1 Emprestimo,
EmprestimoLivro N:1 com Livro,

ConnectionString do banco de dados usando o SQL EXPRESS com um banco criado vazio, AITP3. Foi alterado para LocalDB no github para o uso do professor.

  <connectionStrings>
    <add name="BibliotecaContext" connectionString="Data Source=NITROLUIS\SQLEXPRESS;Initial Catalog=AITP3;Integrated Security=True;Encrypt=False" providerName="System.Data.SqlClient" />
</connectionStrings>
      

