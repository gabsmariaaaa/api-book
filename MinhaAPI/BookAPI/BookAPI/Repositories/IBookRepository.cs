using BookAPI.Model;
using BookAPI.Repositories;

namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get();

        Task<Book> Get(int Id);

        Task<Book> Create(Book book);

        Task<Book> Update(Book book);

        Task Delete(int id);


    }
}
