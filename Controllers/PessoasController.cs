using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM.Pessoas.Data;
using PIM.Pessoas.Models;

namespace PIM.Pessoas.Controllers
{
    public class PessoasController : Controller
    {
        private readonly ApplicationDbContext db;

        public PessoasController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: Pessoas
        public IActionResult Index()
        {
            return View(db.Pessoas.ToList());
        }

        // GET: Pessoas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoas = db.Pessoas.FirstOrDefault(m => m.Id == id);
            pessoas.Endereco = db.Enderecos.FirstOrDefault(m => m.PessoaREF == pessoas.Id);
            pessoas.Telefones = db.Telefones.Where(m => m.IdPessoa == pessoas.Id).ToList();
            if (pessoas == null)
            {
                return NotFound();
            }

            return View(pessoas);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Nome,CPF")] Pessoa pessoas,
            [Bind("Logradouro,Numero,CEP,Bairro,Cidade,UF")] Endereco endereco,
            [Bind("TipoId,DDD,Numero")] IList<Telefone> telefones
            )
        {
            if (ModelState.IsValid)
            {
                // Save Pessoas
                db.Pessoas.Add(pessoas);
                await db.SaveChangesAsync();

                // Save Enderecos
                endereco.PessoaREF = pessoas.Id;
                db.Enderecos.Add(endereco);

                foreach (Telefone tel in telefones)
                {
                    tel.IdPessoa = pessoas.Id;
                    db.Telefones.Add(tel);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(pessoas);
        }

        // GET: Pessoas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoas = db.Pessoas.Find(id);
            pessoas.Endereco = db.Enderecos.FirstOrDefault(m => m.PessoaREF == pessoas.Id);
            pessoas.Telefones = db.Telefones.Where(m => m.IdPessoa == pessoas.Id).ToList();
            if (pessoas == null)
            {
                return NotFound();
            }
            return View(pessoas);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("Id,Nome,CPF")] Pessoa pessoas,
            [Bind("Id,Logradouro,Numero,CEP,Bairro,Cidade,UF")] Endereco endereco,
            [Bind("Id,TipoId,DDD,Numero")] IList<Telefone> telefones
        )
        {
            if (id != pessoas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Pessoas.Update(pessoas);
                    db.SaveChanges();

                    // Save Enderecos
                    endereco.PessoaREF = pessoas.Id;
                    db.Enderecos.Update(endereco);

                    // Save Telefones (primeiro, remove eles...)
                    var telefonesExistentes = db.Telefones.Where(m => m.IdPessoa == pessoas.Id).ToList();
                    foreach (Telefone tel in telefonesExistentes)
                    {
                        db.Telefones.Remove(tel);
                        db.SaveChanges();
                    }

                    foreach (Telefone tel2 in telefones)
                    {
                        tel2.IdPessoa = pessoas.Id;
                        db.Telefones.Add(tel2);
                        db.SaveChanges();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoas);
        }

        // GET: Pessoas/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoas = db.Pessoas
                .FirstOrDefault(m => m.Id == id);
            if (pessoas == null)
            {
                return NotFound();
            }

            return View(pessoas);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pessoa = db.Pessoas.Find(id);

            var endereco = db.Enderecos.FirstOrDefault(m => m.PessoaREF == pessoa.Id);
            if (endereco != null) db.Enderecos.Remove(endereco);

            var telefonesExistentes = db.Telefones.Where(m => m.IdPessoa == pessoa.Id).ToList();
            foreach (Telefone tel in telefonesExistentes)
            {
                db.Telefones.Remove(tel);
                db.SaveChanges();
            }

            db.Pessoas.Remove(pessoa);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return db.Pessoas.Any(e => e.Id == id);
        }
    }
}
