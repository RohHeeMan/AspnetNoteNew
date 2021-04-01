using AspnetNote.DataContext;
using AspnetNote.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetNote.Controllers
{
	public class NoteController : Controller
	{
		/// <summary>
		/// 게시판 리스트
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			// 로그인 상태 판단해야 함.
			if(HttpContext.Session.GetInt32("User_Login_Key") == null)
			{
				// 로그인이 안된 상태
				return RedirectToAction("Login", "Account");
			}

			// List 객체 생성
			var list = new List<Note>();

			// DB 연결
			using (var db = new AspnetNoteDBContext())
			{
				// list객체에  ( db.테이블명.ToList ToList -> list를 제네릭으로 만들어줌 )
				list = db.Notes.ToList();

				// 뷰에 모델로 리스트를 넘겨줌.
				return View(list);
			}
		}

		/// <summary>
		/// 게시물 추가
		/// </summary>
		/// <returns></returns>
		public IActionResult Add()
		{
			// 로그인 상태 판단해야 함.
			if (HttpContext.Session.GetInt32("User_Login_Key") == null)
			{
				// 로그인이 안된 상태
				return RedirectToAction("Login", "Account");
			}

			return View();
		}

		[HttpPost]
		public IActionResult	Add(Note Model)
		{
			// 로그인 상태 판단해야 함.
			if (HttpContext.Session.GetInt32("User_Login_Key") == null)
			{
				// 로그인이 안된 상태
				return RedirectToAction("Login", "Account");
			}

			// UserNo값을 세션값으로 넣어 준다. 그래야 DB에 UserNo값이 들어간다.
			Model.UserNo = int.Parse(HttpContext.Session.GetInt32("User_Login_Key").ToString());

			if (ModelState.IsValid)
			{
				using ( var db = new AspnetNoteDBContext())
				{
					// db.테이블.Add(Model)
					db.Notes.Add(Model);
					// 저장한 것이 성공하면 0보다 큰 숫자로 넘어오므로 if로 비교하면됨.
					if (db.SaveChanges() > 0)
					{
						// 동일한 컨트롤에서 갈때는 Redirect를 사용하면 된다.
						return Redirect("Index");
						// 상단과 동일 ( 어차피 NoteController이므로 동일함 )
						//return RedirectToAction("Index", 'Note"');
					}
				}
				// 저장시 오류가 발생한 경우는 전역적인 오류 메시지 발생시켜주면 됨.
				ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
			}
			return View(Model);
		}

		/// <summary>
		/// 게시물 수정
		/// </summary>
		/// <returns></returns>
		public IActionResult Edit()
		{
			// 로그인 상태 판단해야 함.
			if (HttpContext.Session.GetInt32("User_Login_Key") == null)
			{
				// 로그인이 안된 상태
				return RedirectToAction("Login", "Account");
			}

			return View();
		}

		/// <summary>
		/// 게시물 삭제
		/// </summary>
		/// <returns></returns>
		// 삭제
		public IActionResult Delete()
		{
			// 로그인 상태 판단해야 함.
			if (HttpContext.Session.GetInt32("User_Login_Key") == null)
			{
				// 로그인이 안된 상태
				return RedirectToAction("Login", "Account");
			}

			return View();
		}
	}
}
