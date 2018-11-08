using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildAL
	///     Validate and Build up the HL7 DG1 segment object
	/// </summary>
	public class BuildDG1 : HL7Parser
	{
		const string modName = "BuildDG1";

		public BuildDG1()
		{
		}

		/// <summary>
		/// GetOBR
		///     Get the HL7 DG1 segment and parse into the DG1 object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>DG1 object</returns>
		public DG1 GetDG1(HL7Encoding _encode, string line)
		{
			const string fnName = "GetDG1";
			List<SegmentError> segError = null;
			DG1 dg1 = new DG1();
			int nIdx = 0;
			try
			{
				dg1.SegmentMsg = line;
				dg1.Segment = "DG1";
				segError = Validate(dg1, _encode);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(dg1Elements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((dg1Elements)i).ToString();
						RequiredField rqFld = dg1.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							dg1.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((dg1Elements)i).ToString(), i));
						}
						continue;
					}
					switch ((dg1Elements)i)
					{
						case dg1Elements.Segment:
							dg1.Segment = (string)obj;
							break;
						case dg1Elements.SeqId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								dg1.SeqId = int.Parse((string)obj);
							}
							break;
						case dg1Elements.CodingMethod:
							if (!string.IsNullOrEmpty((string)obj))
							{
								dg1.CodingMethod = (string)obj;
							}
							break;
						case dg1Elements.Code:
							if (!string.IsNullOrEmpty((string)obj))
							{
								dg1.Code = (string)obj;
							}
							break;
						case dg1Elements.Description:
							if (!string.IsNullOrEmpty((string)obj))
							{
								dg1.Description = (string)obj;
							}
							break;

						default:
							dg1.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, ((dg1Elements)i).ToString()));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((dg1Elements)nIdx).ToString(), ex);
				dg1.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return dg1;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">DG1 object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(DG1 seg, HL7Encoding _encode)
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
