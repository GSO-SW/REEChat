using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public static class DateConverter
	{
		public static int GetAgeFromDate(DateTime birthday)
		{
			int years = DateTime.Now.Year - birthday.Year;
			birthday = birthday.AddYears(years);
			if (DateTime.Now.CompareTo(birthday) < 0) { years--; }
			return years;
		}
	}
}
