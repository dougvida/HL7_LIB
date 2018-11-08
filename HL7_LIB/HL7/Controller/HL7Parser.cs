using PTOX_LIB.HL7.Controller;
using PTOX_LIB.HL7.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PTOX_LIB.HL7.Model
{
	/// <summary>
	/// HL7Parser - Base class for BuildOrders and BuildResults
	/// </summary>
	public class HL7Parser
	{
		readonly List<List<RequiredField>> mReqFields = new List<List<RequiredField>>();

		public enum Segments
		{
			[Description("MSH")] MSH
			 , [Description("PID")] PID
			 , [Description("PV1")] PV1
			 , [Description("PV2")] PV2
			 , [Description("GT1")] GT1
			 , [Description("NTE")] NTE
			 , [Description("IN1")] IN1
			 , [Description("IN2")] IN2
			 , [Description("IN3")] IN3
			 , [Description("ORC")] ORC
			 , [Description("OBR")] OBR
			 , [Description("OBX")] OBX
			 , [Description("DG1")] DG1
			 , [Description("CTI")] CTI
			 , [Description("BLG")] BLG
			 , [Description("RXE")] RXE
			 , [Description("AL1")] AL1

			// Segments enumVar = Segments.PID;
			// string value = GetEnumDescription(enumVar);
		}

		public HL7Parser()
		{
		}

		public List<List<RequiredField>> RequiredFields()
		{
			return mReqFields;
		}

		/// <summary>
		/// GetEnumDescription - Based on the Enum value return the string
		/// </summary>
		/// <param name="value">Enum</param>
		/// <returns>string</returns>
		public static string GetEnumDescription(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}

		/// <summary>
		/// GetComponent - get the field component list for a given field
		/// use the HL7 encoding character for components
		/// split the field string based on the component encoding character and return the value
		/// at the index
		/// if no components are found return orginal string
		/// </summary>
		/// <param name="msg">HL7 message</param>
		/// <returns></returns>
		public string GetComponent(HL7Encoding _encode, string msg, int idx = -1)
		{
			string sStr = "";
			idx--;

			if (string.IsNullOrEmpty(msg) || idx < 0)
			{
				return msg;    // bad
			}

			string[] saStr = msg.Split(_encode.Encoding[(int)encodingCharacters.ComponentSeparator]);
//			if (idx <= saStr.Count())
//			{
				try
				{
					sStr = saStr.GetValue(idx).ToString();
				}
				catch (IndexOutOfRangeException)
				{
					sStr = string.Empty;	// not found
				}
//			}
//			else
//			{
//				return msg;    // not found
//			}
			return sStr;
		}
		/// <summary>
		/// getElement - get the HL7 element for the given message 
		/// </summary>
		/// <param name="msg">HL7 message</param>
		/// <returns></returns>
		public string GetField(HL7Encoding _encode, string msg, int idx = -1)
		{
			string sStr = "";
			if (string.IsNullOrEmpty(msg) || idx == -1)
			{
				return null;    // bad
			}

			string[] saStr = msg.Split(_encode.FieldSeparator);
			if (idx <= saStr.Count())
			{
				try
				{
					sStr = saStr.GetValue(idx).ToString();
				}
				catch(IndexOutOfRangeException)
				{
					sStr = string.Empty;
				}
			}
			else
			{
				return msg;    // not found
			}
			return sStr;
		}

		/// <summary>
		/// getElement - return the element for the message segment 
		/// </summary>
		/// <param name="msg">HL7 message</param>
		/// <param name="idx">Index to the element within the message</param>
		/// <returns>object. can be string, datetime, int</returns>
		public object GetElement(HL7Encoding _encode, string msg, int idx = -1)
		{
			string sStr = "";
			if (string.IsNullOrEmpty(msg) || idx == -1)
			{
				return null;    // bad
			}

			string[] saStr = msg.Split(_encode.FieldSeparator);
			if (idx >= saStr.Length)
			{
				// error
				return null;
			}
			try
			{
				sStr = saStr.GetValue(idx).ToString();
			}
			catch(IndexOutOfRangeException)
			{
				sStr = string.Empty;
			}
			return sStr;
		}


		/// <summary>
		/// getField - Get the HL7 value for the given field based on field value
		/// I.E. field = 3.1 get the value from field index 3 subvalue 1
		/// I.E. field = 5.3 get the value from field 5 subvalue 3 
		/// </summary>
		/// <param name="msg">segment to parse</param>
		/// <param name="encoding">HL7 encoding characters from the MSH segment use to obtain the field separator character</param>
		/// <param name="field">field index Major.Subfield</param>
		/// <returns>string</returns>If null failed to retreive the field
		public string GetField(HL7Encoding _encode, string msg, double element = 0.0)
		{
			if (string.IsNullOrEmpty(msg) || _encode.Encoding[(int)encodingCharacters.ComponentSeparator] <= 32 || element == 0.0)
			{
				return null;
			}

			string sStr = "";

			// lets split the value to field.subField
			// I.E.  5.3 or 5.3.1 or 5.4.3.1
			// first index is Field, other nunbers are subfield
			string[] saField = string.Format("{0}", element).Split('.');
			int[] fldIdx = new int[10];
			for (int xx = 0; xx < saField.Length; xx++)
			{
				fldIdx[xx] = int.Parse(saField[xx]);
			}

			// break string into fields
			string[] saStr = msg.Split(_encode.FieldSeparator);
			if (element <= saStr.Count())
			{
				sStr = saStr.GetValue(fldIdx[0]).ToString();
				if (fldIdx[1] != 0)
				{
					// now get the subfield
					string[] subFld = sStr.Split(_encode.Encoding[(int)encodingCharacters.ComponentSeparator]);
					sStr = subFld[fldIdx[1] - 1];
				}
			}
			else
			{
				return null;    // bad
			}
			return sStr;
		}

		/// <summary>
		/// GetSegment - Return a list of strings that contain all the HL7 segments found based on the key value
		/// </summary>
		/// <param name="key"></param>
		/// <param name="nIdx"></param>
		/// <returns></returns>
		public List<string> GetSegment(List<string> _lmsg, string key)
		{
			List<string> lMsg = new List<string>();
			if ((_lmsg.Count <= 0) || string.IsNullOrEmpty(key))
			{
				return null;
			}

			foreach (var line in _lmsg)
			{
				if (line.Substring(0, 3).Equals(key))
				{
					// ok we have a Message Header line
					// now lets get the decoding informaiton
					lMsg.Add(line);
				}
			}
			return lMsg;
		}
	}
}
