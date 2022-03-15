using ControleDeContato.Models;
using System.Collections.Generic;

namespace ControleDeContato.Repositorio
{
    public interface IContactRepository
    {
        Contact ListarPorId(int id);
        List<Contact> BuscarTodos();
        Contact Adicionar(Contact contact);
        Contact Atualizar(Contact contact);
        bool Apagar(int id);
    }
}
