using Microsoft.EntityFrameworkCore;
using EmprestimosLivros.Models;
namespace EmprestimosLivros.Data
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) { }

        //Aqui a string de conexão é passada pro DBContext, que após isso com a classe DBSet cria a tabela
       public  DbSet<EmprestimosModel> Emprestimos {  get; set; }
    }
}
