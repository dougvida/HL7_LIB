using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildAL
	///     Validate and Build up the HL7 ORC segment object
	/// </summary>
	public class BuildORC : HL7Parser
	{
		const string modName = "BuildORC";

		public BuildORC()
		{
		}

		/// <summary>
		/// GetPV1
		///     Get the HL7 ORC segment and parse into the orc object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>ORC object</returns>
		public ORC GetORC(HL7Encoding _encode, string line)
		{
			const string fnName = "GetORC";
			List<SegmentError> segError = null;
			ORC orc = new ORC();
			int nIdx = 0;
			try
			{
				orc.SegmentMsg = line;
				orc.Segment = "ORC";
				segError = Validate(orc, _encode);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(orcElements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((Gt1Elements)i).ToString();
						RequiredField rqFld = orc.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							orc.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((orcElements)i).ToString(), i));
						}
						continue;
					}
					switch ((orcElements)i)
					{
						case orcElements.Segment:
							orc.Segment = (string)obj;
							break;
						case orcElements.EnteredBy:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.EnteredBy = (string)obj;
							}
							break;
						case orcElements.FillerOrderNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.FillerOrderNumber = (string)obj;
							}
							break;
						case orcElements.OrderControl:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.OrderControl = (string)obj;
							}
							break;
						case orcElements.OrderingPhysician:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.NPI = GetComponent(_encode, (string)obj, (int)orcProvider.NPI);
								orc.LastName = GetComponent(_encode, (string)obj, (int)orcProvider.LastName);
								orc.FirstName = GetComponent(_encode, (string)obj, (int)orcProvider.FirstName);
							}
							break;
						case orcElements.OrderStatus:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.OrderStatus = (string)obj;
							}
							break;
						case orcElements.Parent:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.Parent = (string)obj;
							}
							break;
						case orcElements.PlacerGroupNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.PlacerGroupNumber = (string)obj;
							}
							break;
						case orcElements.PlacerOrderNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.PlacerOrderNumber = (string)obj;
							}
							break;
						case orcElements.QuantityTiming:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.QuantityTiming = (string)obj;
							}
							break;
						case orcElements.ResponseFlag:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.ResponseFlag = (string)obj;
							}
							break;
						case orcElements.TransactionDateTime:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.TransactionDateTime = (string)obj;
							}
							break;
						case orcElements.VerifiedBy:
							if (!string.IsNullOrEmpty((string)obj))
							{
								orc.VerifiedBy = (string)obj;
							}
							break;

						default:
							orc.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, (orcElements)i).ToString());
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((orcElements)nIdx).ToString(), ex);
				orc.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return orc;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">ORC object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(ORC seg, HL7Encoding _encode)
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
								if (rqFld.FieldName.Equals(orcElements.OrderControl.ToString()))
								{
									// split the string ORM^O01.   Validate ORM is first field
									if (!"NW".Equals(sTmp))
									{
										segErrors.Add(new SegmentError(rqFld.HL7Segment, rqFld.FieldName, string.Format("{0}:{1} - Order Control value '{3}' is incorrect : (" + (string)obj + ")", modName, fnName, sTmp)));
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
