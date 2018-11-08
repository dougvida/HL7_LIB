using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PTOX_LIB.HL7.Extension
{
	public static class RenameFile
	{
		public static bool Rename(this FileInfo fi, string newName)
		{
			bool bSuccess = false;
			string sTmp = string.Empty;
			try
			{
				sTmp = string.Format(@"{0}\{1}",fi.Directory.FullName, newName);
				// lets delete the file is it already exists
				if (File.Exists(sTmp))
				{
					File.Delete(sTmp);
				}
				fi.MoveTo(sTmp);
				if (File.Exists(sTmp))
				{
					bSuccess = true;
				}
			}
			catch(Exception ep)
			{
				Console.WriteLine("Exception : {0}", ep.ToString());
				bSuccess = false;
			}
			return bSuccess;
		}
	}
}
