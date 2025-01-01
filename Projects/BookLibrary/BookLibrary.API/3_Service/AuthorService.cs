using BookLibrary.API.Data;
using BookLibrary.API.Repository;

namespace BookLibrary.API.Service;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    public async Task<Author> AddAuthor(Author author)
    {
        await _authorRepository.AddAuthor(author);
        return author;
    }

    public async Task AddBookToAuthorById(int authorId, int bookId)
    {
        await _authorRepository.AddBookToAuthorById(authorId, bookId);
    }

    public async Task DeleteAuthorById(int id)
    {
        await _authorRepository.DeleteAuthorById(id);
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _authorRepository.GetAllAuthors();
    }

    public async Task<Author> GetAuthorById(int id)
    {
        return await _authorRepository.GetAuthorById(id);
    }

}