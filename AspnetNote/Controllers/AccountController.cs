using System.Linq;
using AspnetNote.DataContext;
using AspnetNote.Models;
// Login할때 사용할 ViewModel
using AspnetNote.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetNote.Controllers
{
	public class AccountController : Controller
	{
		/// <summary>
		/// 로그인하는 뷰 생성
		/// </summary>
		/// <returns></returns>
		public IActionResult Login()
		{
			return View();
		}

		/// 로그인 전송 메서드 작성
		/// 상단의 Login과 이름이 같으므로 User객체를 Model 이란 이름으로 넘긴다.
		[HttpPost] // 매우 중요(Login에서 post로 넘겼으므로 반드시 써줘야 한다) - 어노테이션을 따로 지정하지 않으면 HttpGet으로 설정됨
		public IActionResult Login(LoginViewModel Model)
		{
			// 필수 입력 요소 :  UserId, UserPassword
			// [1] 벨리데이션 체크 : ModelState.IsValid ( 필수 입력요소로 되어 있는 필드가 입력되었는지 판별 )
   			if (ModelState.IsValid)
			{
				// DB저장할 객체 생성
				using (var db = new AspnetNoteDBContext())
				{
					// Linq이용
					// user 변수에 객체 생성된 db에 (public DbSet<User> Users { get; set; }) user테이블에 FirstOrDefault(제일 첫번째 or 기본 아이 출력됨) 
					// 나는 사용자에게 입력받은 아이나 패스워드로 우리가 원하는 사용자를 찾고 싶다. 
					// 익명 람다식( A Go to B)으로 찾아야 됨 : u(인수를 가져와서 거기에 소속된 아이들이 User테이블에 있는 아이들이 있다)=> 여기서 u는 익명 람다식)

					// 중복 체크 : 사용자가 받은 UserId와 UserPassword가 User테이블에 같은 것이 있느냐 ?
					// 사용자가 받은 Model.UserId와 User테이블에 들어 있는 u.UserId가 같은지 여부 && 사용자가 받은 Model.UserPassword와 User테이블에 들어 있는 u.UserPassword가 같은지 여부
					// ==을 쓰면 새로운 객체가 생성 되므로 같은지 여부를 판단하기 위해서는 Equal을 사용한다.
					// FirstOrDefault => User테이블에서 지금 입력한 아이디,암호가 일치하는 것중 첫번째나 디폴트를 가져와라.
					// [1] var user = db.Users.FirstOrDefault(u=>u.UserId == Model.UserId && u.UserPassword == Model.UserPassword);
					var user = db.Users
						.FirstOrDefault(u=>u.UserId.Equals(Model.UserId) && u.UserPassword.Equals(Model.UserPassword));

					// 성공했을때는 리다이렉트로 처리 : 사용자 & 암호가 맞을 경우 성공
					if (user!=null)
					{
						// [1] 세션 넣기 [2] 로그아웃 구현 [3] 로그아웃 페이지 필요 구현
						// 세션 관련  ( 세션 넣기 )
						HttpContext.Session.SetInt32("User_Login_Key", user.UserNo);
						// 로그인에 성공 했을때 ( Home컨트롤러의 LoginSuccess로 리다이렉트 )
						return RedirectToAction("LoginSuccess", "Home");
					}
				}

				// 로그인에 실패 했을때 : else로 표기 하지 않은 이유는 메시지를 보여주고 바로 뷰로 넘어 가기 위해 Using문장이 조금이라도 빨리 실행되고 코드도 줄일수 있기때문에
				// 실패한 경우는 return View(Model);로 넘어가지만 왜 실패했는지 메시지를 보여주고 싶다. ( ModelState에 담아서 표현 )
				ModelState.AddModelError(string.Empty, "사용자ID 혹은 사용자암호가 올바르지 않습니다");
			}
			//     Login 폼에 asp - validation -for= "UserId" 식으로 입력안되면 에러이므로 Model객체를 넘겨서 체크함
			return View(Model);
		}

		// 로그 아웃 구현
		public IActionResult Logout()
		{
			// 세션 삭제
			HttpContext.Session.Remove("User_Login_Key");
			// 리다이렉트 홈컨트롤러의 인덱스로...
			return RedirectToAction("index", "Home");
		}

		/// <summary>
		/// 회원가입하는 뷰 생성
		/// </summary>
		/// <returns></returns>

		// Get방식으로 페이지 표현 : 전송 받을 Register
		public IActionResult Register()
		{
			return View();
		}

		/// <summary>
		/// 회원 가입 전송 : Register을 전송할 메서드 생성
		/// </summary>
		// 하단과 같이 Register.cshtml(VIEW)에서 Post로 넘겼으니 여기서도 Post로 넘겨야 한다.
		// Account 컨트롤러의  Register로 Post로 넘김 : <form class="form-horizontal" method="post" asp-controller="Account" asp-action="Register">
		/// <param name="Model"></param>
		/// <returns></returns>
		/// 
		/// 나쁜 예 ( Get 전송 : Post로 객체를 넘겨야 URL이 노출되지 않는다)
		/// [HttpGet]
		/// [Route("http://www.example.com/login/honggil/1234567890")]
		/// public IActionResult Register(string UserId, string UserPassword)


		// 회원 가입 전송 : 전송 할 Register : 파라미터를 Get으로 넘길 경우는 URL이 모두 노출되므로 하지 말아야 하고 반드시 Post전송으로 해야 URL이 노출되지 않는다.
		[HttpPost] // 어노테이션을 따로 지정하지 않으면 HttpGet으로 설정됨
		public IActionResult Register(User Model)
		{
			//  IActionResult Register(User model) 상단에 있는 Register과 이름이 같으므로 오류가 발생하므로 
			// 파라미터에 User의 Model(Model은 User 클래스를 쓰고 이름은 그냥 Model이라고 한 것이고 test로 해도 된다)을 넘겨준다 / 또는 이름을 바꿔줘도 되지만 이렇게 하는게 나중에 볼때 편한다

			// Validation Check - 벨리데이션 체크 ( DB 필드가 Nullable일 경우 체크 )
			if (ModelState.IsValid)
			{
				using (var db = new AspnetNoteDBContext())
				{
					// AspnetNote.DataContext의 Users
					db.Users.Add(Model);
					// 실제 저장 Commit
					db.SaveChanges();
				}
				// 저장 후 리다이렉트
				return RedirectToAction("index", "Home");
			}
			return View();
		}
	}
}
