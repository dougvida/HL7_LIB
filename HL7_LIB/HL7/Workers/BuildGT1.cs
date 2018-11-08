﻿using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildGT1
	///     Validate and Build up the HL7 GT1 segment object
	/// </summary>
	public class BuildGT1 : HL7Parser
	{
		const string modName = "BuildGT1";

		public BuildGT1()
		{
		}


		/// <summary>
		/// GetPV1
		///     Get the HL7 GT1 segment and parse into the GT1 object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>GT1 object</returns>
		public GT1 GetGT1(HL7Encoding _encode, string line)
		{
			const string fnName = "GetGT1";
			List<SegmentError> segError = null;
			GT1 gt1 = new GT1();
			int nIdx = 0;
			try
			{
				gt1.SegmentMsg = line;
				gt1.Segment = "GT1";
				segError = Validate(gt1, _encode);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(Gt1Elements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((Gt1Elements)i).ToString();
						RequiredField rqFld = gt1.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							gt1.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((Gt1Elements)i).ToString(), i));
						}
						continue;
					}
					switch ((Gt1Elements)i)
					{
						case Gt1Elements.Segment:
							gt1.Segment = (string)obj;
							break;
						case Gt1Elements.SeqId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.SeqId = int.Parse((string)obj);
							}
							break;
						case Gt1Elements.GuarantorNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.GuarantorNumber = (string)obj;
							}
							break;
						case Gt1Elements.GuarantorName:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.FirstName = GetComponent(_encode, (string)obj, (int)ProviderName.FirstName);
								gt1.LastName = GetComponent(_encode, (string)obj, (int)ProviderName.LastName);
								gt1.MiddleName = GetComponent(_encode, (string)obj, (int)ProviderName.MiddleName);
							}
							break;
						case Gt1Elements.GuarantorSpouseName:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.GuarantorSpouseName = (string)obj;
							}
							break;
						case Gt1Elements.Address:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.Address1 = GetComponent(_encode, (string)obj, (int)GuarantorAddress.Address1);
								gt1.Address2 = GetComponent(_encode, (string)obj, (int)GuarantorAddress.Address2);
								gt1.City = GetComponent(_encode, (string)obj, (int)GuarantorAddress.City);
								gt1.State = GetComponent(_encode, (string)obj, (int)GuarantorAddress.State);
								gt1.Zip = GetComponent(_encode, (string)obj, (int)GuarantorAddress.Zip);
							}
							break;
						case Gt1Elements.PhoneNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.PhoneNumber = (string)obj;
							}
							break;
						case Gt1Elements.BusinessPhone:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.BusinessPhone = (string)obj;
							}
							break;
						case Gt1Elements.DOB:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.DOB = (string)obj;
							}
							break;
						case Gt1Elements.Gender:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.Gender = (string)obj;
							}
							break;
						case Gt1Elements.GuarantorType:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.GuarantorType = (string)obj;
							}
							break;
						case Gt1Elements.Relationship:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.Relationship = (string)obj;
							}
							break;
						case Gt1Elements.SSN:
							if (!string.IsNullOrEmpty((string)obj))
							{
								gt1.SSN = (string)obj;
							}
							break;

						default:
							gt1.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, ((Gt1Elements)i).ToString()));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((Gt1Elements)nIdx).ToString(), ex);
				gt1.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return gt1;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">GT1 object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(GT1 seg, HL7Encoding _encode)
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
