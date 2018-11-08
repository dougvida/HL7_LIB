using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildPID
	///     Validate and Build up the HL7 PID segment object
	/// </summary>
	public class BuildPID : HL7Parser
	{
		const string modName = "BuildPID";

		public BuildPID()
		{
		}

		/// <summary>
		/// GetPID
		///     Get the HL7 PID segment and parse into the PID object.
		///     Also perform field validation of required fields and types
		///     PID object contains a list of errors.
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>PID object</returns>
		public PID GetPID(HL7Encoding _encode, string line, string msgType)
		{
			const string fnName = "GetPID";
			List<SegmentError> segError = null;
			PID pid = new PID(msgType);
			int nIdx = 0;
			try
			{
				pid.SegmentMsg = line;
				pid.Segment = "PID";
				segError = Validate(pid, _encode);

				foreach (int i in Enum.GetValues(typeof(PidElements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((al1Elements)i).ToString();
						RequiredField rqFld = pid.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							pid.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((PidElements)i).ToString(), i));
						}
						continue;
					}
					switch ((PidElements)i)
					{
						case PidElements.Segment:
							pid.Segment = (string)obj;
							break;
						case PidElements.SeqId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.SeqId = int.Parse((string)obj);
							}
							break;
						case PidElements.Address:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.Address1 = GetComponent(_encode, (string)obj, (int)PatientAddress.Address1);
								pid.Address2 = GetComponent(_encode, (string)obj, (int)PatientAddress.Address2);
								pid.City = GetComponent(_encode, (string)obj, (int)PatientAddress.City);
								pid.State = GetComponent(_encode, (string)obj, (int)PatientAddress.State);
								pid.Zip = GetComponent(_encode, (string)obj, (int)PatientAddress.Zip);
							}
							break;
						case PidElements.AltPID:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.AlternatePatientId = (string)obj;
							}
							break;
						case PidElements.BPhone:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.BusinessPhone = int.Parse((string)obj);
							}
							break;
						case PidElements.DOB:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.DOB = (string)obj;
							}
							break;
						case PidElements.ExternalId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.ExternalId = (string)obj;
							}
							break;
						case PidElements.PatientName:
							if (!string.IsNullOrEmpty((string)obj))
							{
								try
								{
									pid.FName = (string)GetComponent(_encode, (string)obj, (int)PatientName.FirstName);
									pid.LName = (string)GetComponent(_encode, (string)obj, (int)PatientName.LastName);
									pid.MiddleInitial = (string)GetComponent(_encode, (string)obj, (int)PatientName.MiddleInitial);
								}
								catch (IndexOutOfRangeException idx)
								{
									// Eat this exception
									pid.Errors.Add(string.Format("{0}:{1} - Exception caught ({2}), at field {3}-{4}", modName, fnName, ((PidElements)i).ToString(), i, idx.ToString()));
									continue;
								}
							}
							break;
						case PidElements.Gender:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.Gender = (string)obj;
							}
							break;
						case PidElements.Language:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.Language = (string)obj;
							}
							break;
						case PidElements.MaritalStatus:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.MaritalStatus = (string)obj;
							}
							break;
						case PidElements.MotherMadenName:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.MotherMadenName = (string)obj;
							}
							break;
						case PidElements.PatAlias:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.PatientAlias = (string)obj;
							}
							break;
						case PidElements.PatientId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.PatientId = (string)obj;
							}
							break;
						case PidElements.Phone:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.Phone = int.Parse((string)obj);
							}
							break;
						case PidElements.Race:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.Race = (string)obj;
							}
							break;
						case PidElements.Religion:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.Religion = (string)obj;
							}
							break;

						case PidElements.PatientAccountNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pid.PatientAccountNumber = (string)obj;
							}
							break;

						default:
							pid.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, ((PidElements)i).ToString()));
							break;
					}
					//Console.WriteLine(i);
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({0}) {1}", modName, fnName, ((PidElements)nIdx).ToString(), ex);
				pid.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return pid;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">PID object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(PID seg, HL7Encoding _encode)
		{
			const string fnName = "Validate";
			List<SegmentError> segErrors = new List<SegmentError>();
			try
			{
				foreach (var rqFld in seg.RequiredFields)
				{
					if (rqFld.IsRequired)
					{
						Object obj = GetField(_encode, seg.SegmentMsg, rqFld.FieldIdx);
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
								if (rqFld.FieldName.Equals(MshElements.MessageType.ToString()) && "MSH".Equals(seg.SegmentMsg))
								{
									// split the string ORM^O01.   Validate ORM is first field
									if (!"ORM^O01".Equals(sTmp))
									{
										segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - Message type must be ORM^O01 : (" + (string)obj + ")", modName, fnName)));
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
