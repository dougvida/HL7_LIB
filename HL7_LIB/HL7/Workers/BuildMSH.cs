using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildMSH
	///     Validate and Build up the HL7 PID segment object
	/// </summary>
	public class BuildMSH : HL7Parser
	{
		const string modName = "BuildMSH";

		public BuildMSH()
		{
		}

		// public string FldSept { get; }

		/// <summary>
		/// GetMSH
		///     Get the HL7 MSH segment and parse into the MSH object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>MSH object</returns>
		public MSH GetMSH(HL7Encoding hl7Encoding, string line)
		{
			const string fnName = "GetMSH";
			List<SegmentError> segError = null;
			MSH msh = new MSH(hl7Encoding);
			string[] saTmp = new string[20];
			int nIdx = 0;

			try
			{
				msh.SegmentMsg = line;
				msh.Segment = "MSH";
				segError = Validate(msh, hl7Encoding);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(MshElements)))
				{
					nIdx = i;
					object obj = GetElement(hl7Encoding, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((Gt1Elements)i).ToString();
						RequiredField rqFld = msh.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							msh.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((MshElements)i).ToString(), i));
						}
						continue;
					}
					switch ((MshElements)i)
					{
						case MshElements.Segment:
							msh.Segment = (string)obj;
							break;
						case MshElements.Encoding:
							// verify data
							msh.Encoding = (string)obj;
							break;
						case MshElements.MessageType:
							msh.MessageType = (string)obj;
							break;
						case MshElements.MsgCtrlId:
							msh.MessageControlId = (string)obj;
							break;
						case MshElements.ProcessingId:
							msh.ProcessingId = (string)obj;
							break;
						case MshElements.ReceivingApp:
							saTmp = ((string)obj).Split('^');
							msh.ReceivingApp = saTmp[0];
							break;
						case MshElements.ReceivingFacility:
							saTmp = ((string)obj).Split('^');
							msh.ReceivingFacility = saTmp[0];
							break;
						case MshElements.Security:
							msh.Security = (string)obj;
							break;
						case MshElements.SendingApp:
							saTmp = ((string)obj).Split('^');
							msh.SendingApp = saTmp[0];
							break;
						case MshElements.SendingFacility:
							saTmp = ((string)obj).Split('^');
							msh.SendingFacility = saTmp[0];
							break;

						case MshElements.TimeOfMessage:
							// this is a DateTime field
							msh.TimeOfMessage = ((string)obj).Trim();
							string sTformat = "yyyyMMddHHmm";
							switch (msh.TimeOfMessage.Length)
							{
								case 8:
									sTformat = "yyyyMMdd";
									break;
								case 12:
									sTformat = "yyyyMMddHHmm";
									break;
								case 14:
									sTformat = "yyyyMMddHHmmss";
									break;
								default:
									break;
							}
							msh.TimeOfMessageDT = DateTime.ParseExact(msh.TimeOfMessage, sTformat, null);
							break;

						case MshElements.Version:
							msh.Version = (string)obj;
							break;

						default:
							msh.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, ((MshElements)i).ToString()));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((MshElements)nIdx).ToString(), ex);
				msh.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return msh;
		}

		private string mFldSeparator;
		public string FldSeparator
		{
			get { return mFldSeparator; }
			set { mFldSeparator = value; }
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">MSH object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(MSH seg, HL7Encoding hl7Encoding)
		{
			const string fnName = "Validate";
			List<SegmentError> segErrors = new List<SegmentError>();
			try
			{
				foreach (var rqFld in seg.RequiredFields)
				{
					if (rqFld.IsRequired)
					{
						Object obj = GetField(hl7Encoding, seg.SegmentMsg, rqFld.FieldIdx);
						if (string.IsNullOrEmpty((string)obj))
						{
							segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - field {2} Value is required cannot be null", modName, fnName, rqFld.FieldName)));
							break;  // leave
						}
						switch (rqFld.FieldType.ToLower())
						{
							case "int":
								bool bAns = int.TryParse(((string)obj), out int nValue);
								if (!bAns)
								{
									segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - field {2} type is 'int' value is required cannot be null", modName, fnName, rqFld.FieldName)));
								}
								break;

							case "string":
								string sTmp = (string)obj;
								// check if string is greate than fieldLength
								if (sTmp.Length > rqFld.FieldLength)
								{
									segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - field {2} type is 'string' value is greater than max size {3}", modName, fnName, rqFld.FieldName, rqFld.FieldLength)));
								}
								if (rqFld.FieldName.Equals(MshElements.MessageType.ToString()) && seg.SegmentMsg.StartsWith("MSH"))
								{
									// split the string ORM^O01.   Validate ORM is first field
									if (!"ORM^O01".Equals(sTmp) && !"ORU^R01".Equals(sTmp))
									{
										segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - Message type must be ORM^O01 or ORU^R01 : (" + (string)obj + ")", modName, fnName)));
									}
								}
								break;

							case "date":
								// the field is a date field but is a string in the HL7 message
								switch (((string)obj).Length)
								{
									case 8:
									case 12:
									case 14:
										// good
										break;

									default:
										segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - field {2} type is 'date' value out of range YYYYMMDD, YYYYMMDDHHMM, YYYYMMDDHHMMSS", modName, fnName, rqFld.FieldName)));
										break;
								}
								break;

							default:
								segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - FieldType ({2}) is undefined", modName, fnName, rqFld.FieldType.ToLower())));
								break;
						}
					}
				}
			}
			catch (Exception exp)
			{
				string sTmp = string.Format("{0}:{1} - EXCEPTION ({2})", modName, fnName, exp);
				segErrors.Add(new SegmentError(seg.Segment, "N/A", sTmp));
			}
			return segErrors;
		}
	}
}
