using ClosedXML.Excel;
using EmprestimosLivros.Data;
using EmprestimosLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmprestimosLivros.Controllers
{
    public class EmprestimosController : Controller
    {
        readonly private ApplicationDBContext _database;

        public EmprestimosController(ApplicationDBContext database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            
            List<EmprestimosModel> emprestimos = _database.Emprestimos.ToList();

            return View(emprestimos);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int Id )
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            EmprestimosModel emprestimos = _database.Emprestimos.FirstOrDefault(x => x.Id == Id);
            if(emprestimos == null)
            {
                return NotFound();
            }
            return View(emprestimos);
        }

        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimos)
        {
            if (ModelState.IsValid)
            {

                var EmprestimosDb = _database.Emprestimos.Find(emprestimos.Id);
                EmprestimosDb.Recebedor = emprestimos.Recebedor;
                EmprestimosDb.Fornecedor = emprestimos.Fornecedor;
                EmprestimosDb.LivroEmprestado = emprestimos.LivroEmprestado;
                _database.Update(EmprestimosDb);
                _database.SaveChanges();
                TempData["MensagemEdicao"] = "Edição Realizada com Sucesso";
                return RedirectToAction("Index");
            }
           
            return View(emprestimos);

        }
        [HttpGet]
        public IActionResult Excluir(int Id) 
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            EmprestimosModel emprestimos = _database.Emprestimos.FirstOrDefault(x => x.Id == Id);
            if (emprestimos == null)
            {
                return NotFound();
            }
            return View(emprestimos);
        }



        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimos) 
        {
            if (emprestimos == null)
            {
                return NotFound();
            }
            _database.Remove(emprestimos);
            _database.SaveChanges();
            TempData["MensagemErro"] = "Remoção Realizada com Sucesso";
            return RedirectToAction("Index");

        }


        public IActionResult Exportar()
        {
            var Dados = GetDados();
            using (XLWorkbook workbook = new XLWorkbook())
            {

               
                workbook.AddWorksheet(Dados,"Dados Empréstimos");
                using(MemoryStream ms = new MemoryStream())
                {
                  
                    workbook.SaveAs(ms);
                   
                    return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spredsheetml.sheet","Emprestimo.xls");
                }
            }
           
        }


        private DataTable GetDados()
        {
          
            DataTable datatable = new DataTable();
            datatable.TableName = "Dados do Emprestimos";
            datatable.Columns.Add("Recebedor",typeof(string));
            datatable.Columns.Add("Fornecedor", typeof(string));
            datatable.Columns.Add("Livro", typeof(string));
            datatable.Columns.Add("Data do Empréstimo", typeof(DateTime));


           
            var dados = _database.Emprestimos.ToList();
            if(dados.Count > 0)
            {

                dados.ForEach(emprestimos =>
                   datatable.Rows.Add(emprestimos.Recebedor, emprestimos.Fornecedor, emprestimos.LivroEmprestado, emprestimos.DataEmprestimo)
                ); 

            }

           
            return datatable;

        }

        
        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel DadosDaModel)
        {
            if(ModelState.IsValid) 
            {
                DadosDaModel.DataEmprestimo = DateTime.Now;
                _database.Emprestimos.Add(DadosDaModel);
                _database.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro Realizado com Sucesso";
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        
    }
}
