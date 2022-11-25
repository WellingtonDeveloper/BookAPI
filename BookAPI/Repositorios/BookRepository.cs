using BookAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Repositorios
{
    public class BookRepository : IBookRepository
    {
        public readonly BookContext _context; // Apenas realiza leitura, não pode ser implementada
        public BookRepository(BookContext context)
        {
            _context = context;
        }
        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(); // usar o SaveChangesAsync permite fazer varias transações ao mesmo tempo de forma assincrona, simultaneamente, evitando perder informações.

            return book;
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);// Procurando o registro de forma assincrona no banco
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync(); // atualizando a tabela removendo o Id que mandei deletar
        }

        public async Task<IEnumerable<Book>> Get() // Listar o que tenho no banco
        {
            return await _context.Books.ToListAsync();

        }

        public async Task<Book> Get(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
