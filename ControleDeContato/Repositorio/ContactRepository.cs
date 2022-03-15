using ControleDeContato.Data;
using ControleDeContato.Models;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContato.Repositorio
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataBaseContext _databaseContext;

        public ContactRepository(DataBaseContext baseContext)
        {
            _databaseContext = baseContext;
        }

        public Contact ListarPorId(int id)
        {
            return _databaseContext.Contacts.FirstOrDefault(x => x.Id == id);
        }

        public List<Contact> BuscarTodos()
        {
            return _databaseContext.Contacts.ToList();
        }

        public Contact Adicionar(Contact contact)
        {
            _databaseContext.Contacts.Add(contact);
            _databaseContext.SaveChanges();
            return contact;
        }

        public Contact Atualizar(Contact contact)
        {
            Contact contactDB = ListarPorId(contact.Id);

            if (contactDB == null)
            {
                throw new System.Exception("Houve um erro na atualização do contato");
            }

            contactDB.Name = contact.Name;
            contactDB.Email = contact.Email;
            contactDB.Phone = contact.Phone;

            _databaseContext.Contacts.Update(contactDB);
            _databaseContext.SaveChanges();
            return contactDB;
        }

        public bool Apagar(int id)
        {
            Contact contactDB = ListarPorId(id);

            if (contactDB == null)
            {
                throw new System.Exception("Houve um erro na deleção do contato");
            }

            _databaseContext.Contacts.Remove(contactDB);
            _databaseContext.SaveChanges();
            return true;
        }
    }
}
