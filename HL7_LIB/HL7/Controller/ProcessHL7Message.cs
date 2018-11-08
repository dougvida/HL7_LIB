using PTOX_LIB.HL7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// ProcessHL7Message - singleton object need to get an instance
	/// ProcessHL7Message _processInst = ProcessHL7Message.Instance
	/// Use this instance object to perform all the HL7 operations
	/// </summary>
	public sealed class ProcessHL7Message : HL7Parser
	{
		private static volatile ProcessHL7Message instance;
		private static object syncRoot = new Object();

		private ProcessHL7Message() { }

		public static ProcessHL7Message Instance
		{
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new ProcessHL7Message();
						}
					}
				}
				return instance;
			}
		}

		/// <summary>
		/// ParseHL7 - Parse the list and return HL7_ORM or HL7_ORU object
		/// </summary>
		/// <param name="slMsg"></param>
		/// <returns>HL7_ORM or HL7_ORU</returns>
		public object ParseHL7(List<string> slMsg)
		{
			string msgType = string.Empty;

			// Look in the MSH segment to determine if this is an Order or Results type message
			// MSH.8.1 will contain ORM or ORU
			msgType = GetMessageType(slMsg);
			if (string.IsNullOrEmpty(msgType))
			{
				return null;
			}

			if (msgType.Equals("ORM"))
			{
				// return HL7_ORM
				return (new BuildOrders().ParseORM(slMsg));
			}
			else
			{
				// return HL7_ORU object
				return (new BuildResults().ParseORU(slMsg));
			}
		}

		/// <summary>
		/// GetMessageType - Return the message type ORM or ORU or null if not found
		/// <param name="lsMsg"></param>
		/// <returns>string</returns>
		/// </summary>
		private static string GetMessageType(List<string> lsMsg)
		{
			HL7Encoding _encoding = null;
			foreach (var line in lsMsg)
			{
				if (line.Substring(0, 3).Equals("MSH"))
				{
					// ok we have a Message Header line
					// now lets get the decoding informaiton
					_encoding = new HL7Encoding((line.Substring(3, 5)).ToCharArray());
					// split the string on the field separator
					string[] sTmp = line.Split(_encoding.FieldSeparator);
					return (sTmp[8].Substring(0, 3));
				}
			}
			return null;
		}
	}
}

