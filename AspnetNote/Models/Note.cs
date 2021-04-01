using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetNote.Models
{
	public class Note
	{
		/// <summary>
		/// 게시물 번호
		/// </summary>
		[Key]  // PK 설정
		public int NoteNo { get; set; }
		/// <summary>
		/// 게시물 제목
		/// </summary>
		[Required(ErrorMessage ="제목을 입력하세요")]   // Not Null 설정
		public string NoteTitle { get; set; }
		/// <summary>
		/// 게시물 내용
		/// </summary>
		[Required(ErrorMessage ="내용을 입력하세요")]   // Not Null 설정
		public string NoteContent { get; set; }

		/// <summary>
		/// 작성자 번호
		/// </summary>
		[Required]   // Not Null 설정
		public int UserNo { get; set; }

		/// <summary>
		/// User Class의 UserNo 외래키
		/// </summary>
		[ForeignKey("UserNo")]  // User Class의 외래키
		public virtual User user { get; set; }
	}
}
