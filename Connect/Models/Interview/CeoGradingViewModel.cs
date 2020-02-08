using System;
using System.ComponentModel.DataAnnotations;

namespace Connect.Models.Interview
{
	[Serializable]
	public class CeoGradingViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		[DataType(DataType.MultilineText)]
		public string Comments { get; set; }

		public int Grade { get; set; }
	}

	public class Grade
	{
		public int GradeId { get; set; }
		public string GradeValue { get; set; }
	}
}