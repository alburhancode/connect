using System.Collections.Generic;
using Connect.Classes.DataModels;

namespace Connect.Models.Records
{
	public class RecordsViewModel
	{
		public IEnumerable<Individual> Individuals { get; set; }
		public UserType UserType { get; set; }
	}
}