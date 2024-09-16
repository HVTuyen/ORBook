namespace ORBook.Service.BookGenres
{
    public interface IBookGenreService
    {
        Task<bool> UpdateAsync(int bookId, List<int> genreId);
        Task<bool> CreateAsync(int bookId, List<int> genreId);
        Task<bool> DeleteAsync(int bookId);
        Task<List<Models.Book>> GetSearchPage(int genreId, string searchString, int Page, int PageSize);
    }
}
