using ControleDeContato.Models;
using ControleDeContato.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDeContato.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            List<Contact> contacts = _contactRepository.BuscarTodos();
            return View(contacts);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            Contact contact = _contactRepository.ListarPorId(id);
            return View(contact);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            Contact contact = _contactRepository.ListarPorId(id);
            return View(contact);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool wiped = _contactRepository.Apagar(id);
                if (wiped)
                {
                    TempData["SuccessMessage"] = "Contato apagado com sucesso!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Ops, não conseguimos apagar seu contato!";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos apagar seu contato! Mais detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contactRepository.Adicionar(contact);
                    TempData["SuccessMessage"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contact);
            }
            catch (System.Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente. Detalhe do erro: {error.Message}!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contactRepository.Atualizar(contact);
                    TempData["SuccessMessage"] = "Contato atualizado com sucesso!";
                    return RedirectToAction("Index");

                }

                return View("Editar", contact);
            }
            catch (System.Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos atualizar o seu contato, tente novamente. Detalhe do erro: {error.Message}!";
                return RedirectToAction("Index");
            }
        }
    }
}
