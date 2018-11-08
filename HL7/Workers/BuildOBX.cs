using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildAL
	///     Validate and Build up the HL7 OBX segment object
	/// </summary>
	public class BuildOBX : HL7Parser
	{
		const string modName = "BuildOBX";

		public BuildOBX()
		{
		}

		/// <summary>
		/// GetOBR
		///     Get the HL7 OBR segment and parse into the OBR object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>OBX object</returns>
		public OBX GetOBX(HL7Encoding _encode, string line, string messageType)
		{
			const string fnName = "GetOBX";
			List<SegmentError> segError = null;
			OBX obx = new OBX(messageType);
			int nIdx = 0;
			try
			{
				obx.SegmentMsg = line;
				obx.Segment = "OBR";
				segError = Validate(obx, _encode, messageType);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(obxElements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((Gt1Elements)i).ToString();
						RequiredField rqFld = obx.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							obx.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((obxElements)i).ToString(), i));
						}
						continue;
					}
					switch ((obxElements)i)
					{
						case obxElements.Segment:
							obx.Segment = (string)obj;
							break;
						case obxElements.SeqId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.SeqId = int.Parse((string)obj);
							}
							break;
						case obxElements.ObservationIdentifier:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.TestCode = GetComponent(_encode, (string)obj, (int)observationIdentifier.testCode);
								obx.TestCodeDescription = GetComponent(_encode, (string)obj, (int)observationIdentifier.testCodeDescription);
								obx.LimTestCode = GetComponent(_encode, (string)obj, (int)observationIdentifier.limTestCode);
								obx.LimTestCodeDescription = GetComponent(_encode, (string)obj, (int)observationIdentifier.limTestCodeDescription);
								obx.CodingSystem = GetComponent(_encode, (string)obj, (int)observationIdentifier.codingSystem);
							}
							break;
						case obxElements.ObservationSubId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.ObservationSubId = (string)obj;
							}
							break;
						case obxElements.ObservationValue:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.ObservationValue = (string)obj;
							}
							break;
						case obxElements.ValueType:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.ValueType = (string)obj;
							}
							break;

						case obxElements.Units:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.Units = (string)obj;
							}
							break;

						case obxElements.ReferenceRange:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.ReferenceRange = (string)obj;
							}
							break;

						case obxElements.AbnormalFlags:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.AbnormalFlags = (string)obj;
							}
							break;

						case obxElements.Probability:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.Probability = (string)obj;
							}
							break;

						case obxElements.AbnormalTest:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.AbnormalTest = (string)obj;
							}
							break;

						case obxElements.ResultStatus:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.ResultStatus = (string)obj;
							}
							break;

						case obxElements.ObservedLastDate:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.ObservedLastDate = (string)obj;
							}
							break;

						case obxElements.AccessChecks:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.AccessChecks = (string)obj;
							}
							break;

						case obxElements.ObservationDateTime:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.ObservationDateTime = (string)obj;
							}
							break;

						case obxElements.Producers_ID:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obx.Producers_ID = (string)obj;
							}
							break;

						default:
							obx.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, ((obxElements)i).ToString()));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((obxElements)nIdx).ToString(), ex);
				obx.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return obx;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">OBX object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(OBX seg, HL7Encoding _encode, string messageType)
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
