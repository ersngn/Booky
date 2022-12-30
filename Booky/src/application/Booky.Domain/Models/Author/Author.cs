using Booky.Domain.Models.Base;

namespace Booky.Domain.Models;

public class Author:IEntity<int>
{
    public int Id { get; set; }
    public string FullName { get; set; }

    //Navigations Properties
    public List<BookAuthor> BookAuthors { get; set; }
}