//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;
//using Connect.Models;

//namespace Connect.Classes.Validation
//{
//	public class PaymentValidation : ValidationAttribute
//	{
//		public override bool IsValid(object value)
//		{
//			var model = value as SignupViewModel;
//			if (model != null && model.PaymentMethod == "Jazz" && model.PaidThroughJazz && string.IsNullOrEmpty(model.PaymentCode))
//			{
//				ErrorMessage = "please enter you Jazz Payment Reference";
//				return false;
//			}
//			return true;
//		}
//	}
//}