﻿using System.ComponentModel.DataAnnotations;

namespace AspnetNote.ViewModel
{
	// 로그인할때만 사용할 ViewModel
	public class LoginViewModel
	{
		[Required(ErrorMessage ="사용자 ID를 입력하세요")]
		public string UserId{ get; set; }
		[Required(ErrorMessage ="사용자 비밀번호를 입력하세요")]
		public string UserPassword { get; set; }
	}

}
