using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Extension
{
	public static class CSVData
	{
		public static string FixupCSVString(this string str)
		{
			string sTmp = string.Empty;
			StringBuilder sb = new StringBuilder(str);
			// replace all ',' with '.'
			sb.Replace(',', '.');
			sb.Replace('\n', ' ');
			sb.Replace('\r', ' ');
			sb.Replace('\t', ' ');
			sb.Replace("  ", " ");
			str = sb.ToString();
			return str;
		}
	}
}
