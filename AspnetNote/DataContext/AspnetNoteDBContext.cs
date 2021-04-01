using AspnetNote.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetNote.DataContext
{
	public class AspnetNoteDBContext : DbContext
	{
		// 테이블을 생성하는 코드 작성 ( 마이그레이션이라는 과정을 넘어가면 DB Table가 자동으로 생성된다 )
		public DbSet<User> Users { get; set; }
		public DbSet<Note> Notes { get; set; }

		// DB와 컨넥션 연결 ( override 를 적고 OnConfiguring을 선택해 준다 )
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=DESKTOP-7BR531T;Database=AspnetNoteDb;User Id=sa;Password=!spree;");
		}
	}
}
