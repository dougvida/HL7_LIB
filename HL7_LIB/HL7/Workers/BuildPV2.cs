using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildPV2
	///     Validate and Build up the HL7 PID segment object
	/// </summary>
	public class BuildPV2 : HL7Parser
	{
		const string modName = "BuildPV2";

		public BuildPV2()
		{
		}

		/// <summary>
		/// GetPV2
		///     Get the HL7 PV2 segment and parse into the PV2 object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>PV2 object</returns>
		public PV2 GetPV2(HL7Encoding _encode, string line)
		{
			const string fnName = "GetPV2";

			List<SegmentError> segError = null;
			PV2 pv2 = new PV2();
			int nIdx = 0;
			try
			{
				pv2.SegmentMsg = line;
				pv2.Segment = "PV2";
				segError = Validate(pv2, _encode);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(pv2Elements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((Gt1Elements)i).ToString();
						RequiredField rqFld = pv2.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							pv2.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((pv2Elements)i).ToString(), i));
						}
						continue;
					}
					switch ((pv2Elements)i)
					{
						case pv2Elements.Segment:
							pv2.Segment = (string)obj;
							break;
						case pv2Elements.SeqId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								pv2.SeqId = int.Parse((string)obj);
							}
							break;
						case pv2Elements.ConsultingDr:
							pv2.ConsultingDr = (string)obj;
							break;
						case pv2Elements.OrderingProvider:
							pv2.OrderingProvider = (string)obj;
							break;
						case pv2Elements.ReferringDr:
							pv2.ReferringDr = (string)obj;
							break;

						default:
							pv2.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, ((pv2Elements)i).ToString()));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((pv2Elements)nIdx).ToString(), ex);
				pv2.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return pv2;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">PV2 object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(PV2 seg, HL7Encoding _encode)
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
