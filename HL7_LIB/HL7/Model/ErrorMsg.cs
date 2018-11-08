using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model
{
	/// <summary>
	/// ErrorMsg - Contains error informaiton of validation during HL7 processing
	/// </summary>
	public class ErrorMsg
	{
		public ErrorMsg()
		{
		}

		public ErrorMsg(int code, string msg)
		{
			this.Code = code;
			this.Message = msg;
		}

		private int mCode;
		public int Code { get => mCode; set => mCode = value; }

		private string mMessage;
		public string Message { get => mMessage; set => mMessage = value; }
	}
}
