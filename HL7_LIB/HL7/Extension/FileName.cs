using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Extension
{
	public static class FileName
	{
		/// <summary>
		/// AppendTimeStampYYYYMMDDHHMMSS - Append the time stamp to the file name
		/// time stamp format is yyyyMMddHHmmss
		/// I.E. filename_yyyyMMddHHmmss.ext
		/// usage:
		/// string result = "filename".AppendTimeStamp();
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>appended timestamp to file name</returns>
		public static string AppendTimeStampYYYYMMDDHHMMSS(this string fileName)
		{
			return string.Concat(
				 Path.GetFileNameWithoutExtension(fileName),
				 "_",
				 DateTime.Now.ToString("yyyyMMddHHmm"),
				 Path.GetExtension(fileName)
				 );
		}

		public static string AppendTimeStampYYYYMMDDHH(this string fileName)
		{
			return string.Concat(
				 Path.GetFileNameWithoutExtension(fileName),
				 "_",
				 DateTime.Now.ToString("yyyyMMddhh"),
				 Path.GetExtension(fileName)
				 );
		}
	}
}
