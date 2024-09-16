using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORBook.Data;
using ORBook.Models;
using System;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ORBookContext(serviceProvider.GetRequiredService<DbContextOptions<ORBookContext>>()))
            {
                if (!context.Genre.Any())
                {
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[Genre]', RESEED, 0)");
                    var genres = new Genre[]
                     {
                        new Genre{Name="Thiếu nhi"},
                        new Genre{Name="Phiêu lưu"},
                        new Genre{Name="Hiện thực"},
                        new Genre{Name="Văn học hiện đại"},
                        new Genre{Name="Huyền bí"},
                        new Genre{Name="Triết lý"},
                        new Genre{Name="Khoa học"},
                        new Genre{Name="Lịch sửc"},
                        new Genre{Name="Lãng mạn"},
                        new Genre{Name="Chính trị"},
                        new Genre{Name="Gia đình"},
                     };
                    foreach (Genre g in genres)
                    {
                        context.Genre.Add(g);
                    }
                    context.SaveChanges();
                }

                //if (!context.Book.Any())
                //{
                //    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[Book]', RESEED, 0)");
                //    var books = new Book[]
                //    {
                //        new Book{Name="Không Gia Đình",Description="Nội dung cuốn tiểu thuyết kể về cuộc đời phiêu bạt của một cậu bé Remi - không có cha, không có mẹ. Cậu được đem về nuôi trong một gia đình sống ở vùng quê xa xôi, hẻo lánh nhưng may mắn thay, Remi được mẹ nuôi Barberin yêu thương và xem như con ruột của mình.",Author="Hector Malot",CreatedTime=DateTime.Now},
                //        new Book{Name="Ông Già Và Biển Cả",Description="Ông Già Và Biển Cả hay còn có tên tiếng Anh là The Old Man and the Sea, đây là một cuốn tiểu thuyết hay và vô cùng nổi tiếng được viết dưới ngòi bút của nhà văn người Mỹ - Ernest Hemingway. Tác phẩm được nhà văn sáng tác vào năm 1951 tại Cuba, đã mang về giải thưởng giá trị Pulitzer.",Author="Ernest Hemingway",CreatedTime=DateTime.Now},
                //        new Book{Name="Âm Thanh Và Cuồng Nộ",Description="Âm Thanh Và Cuồng Nộ là một trong những cuốn tiểu thuyết hay mà bạn không thể bỏ qua, được viết bởi hào văn William Faulkner. Tiểu thuyết được ấn hành lần đầu tiên vào ngày 7.10.1929, chính là tác phẩm đã giúp William Faulkner đạt đến đỉnh cao của sự thành công.",Author="William Faulkner",CreatedTime=DateTime.Now},
                //        new Book{Name="Thép Đã Tôi Thế Đấy",Description="Đối với những ai đam mê văn học lịch sử thì có lẽ sẽ biết đến Thép Đã Tôi Thế Đấy của nhà văn Nikolai Ostrovsky. Tác phẩm là một cuốn nhật ký ghi chép lại cả quá trình tôi thép, những bước đường gian khổ trưởng thành của thế hệ thanh niên Xô Viết đời đầu.",Author="Nikolai Ostrovsky",CreatedTime=DateTime.Now},
                //        new Book{Name="Nhà Giả Kim",Description="Nhà Giả Kim luôn nằm trong top những cuốn sách bán chạy nhất thế giới, được viết bởi nhà văn Paulo Coelho. Quyển sách sẽ rất phù hợp cho những ai đang cần một sự định hướng đúng đắn cho cuộc đời của mình.",Author="Paulo Coelho",CreatedTime=DateTime.Now},
                //    };
                //    foreach (Book b in books)
                //    {
                //        context.Book.Add(b);
                //    }
                //    context.SaveChanges();
                //}
                //if(!context.BookGenre.Any())
                //{
                    
                //    var bookGenres = new BookGenre[]
                //    {
                //        new BookGenre{BookId=1,GenreId=2},
                //        new BookGenre{BookId=1,GenreId=4},
                //        new BookGenre{BookId=2,GenreId=3},
                //        new BookGenre{BookId=2,GenreId=4},
                //        new BookGenre{BookId=3,GenreId=1},
                //        new BookGenre{BookId=3,GenreId=5},
                //        new BookGenre{BookId=3,GenreId=6},
                //        new BookGenre{BookId=4,GenreId=7},
                //    };
                //    foreach (BookGenre bg in bookGenres)
                //    {
                //        context.BookGenre.Add(bg);
                //    }
                //    context.SaveChanges();
                //}
            }
        }
    }
}