using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildAL
	///     Validate and Build up the HL7 OBR segment object
	/// </summary>
	public class BuildOBR : HL7Parser
	{
		const string modName = "BuildOBR";

		public BuildOBR()
		{
		}

		/// <summary>
		/// GetOBR
		///     Get the HL7 OBR segment and parse into the OBR object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>OBR object</returns>
		public OBR GetOBR(HL7Encoding _encode, string line)
		{
			const string fnName = "GetOBR";
			List<SegmentError> segError = null;
			OBR obr = new OBR();
			int nIdx = 0;
			try
			{
				obr.SegmentMsg = line;
				obr.Segment = "OBR";
				segError = Validate(obr, _encode);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(ObrElements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((Gt1Elements)i).ToString();
						RequiredField rqFld = obr.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							obr.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((ObrElements)i).ToString(), i));
						}
						continue;
					}
					switch ((ObrElements)i)
					{
						case ObrElements.Segment:
							obr.Segment = (string)obj;
							break;
						case ObrElements.SeqId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.SeqId = int.Parse((string)obj);
							}
							break;
						case ObrElements.AssistantResultInterpreter:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.AssistantResultInterpereter = (string)obj;
							}
							break;
						case ObrElements.ChargeToPractice:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ChargeToPractice = (string)obj;
							}
							break;
						case ObrElements.CollectionIdentifier:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.CollectionVolume = GetComponent(_encode, (string)obj, 1);
							}
							break;
						case ObrElements.CollectionVolume:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.CollectionVolume = (string)obj;
							}
							break;
						case ObrElements.CollectorsComments:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.CollectorsComments = (string)obj;
							}
							break;
						case ObrElements.DangerCode:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.DangerCode = (string)obj;
							}
							break;
						case ObrElements.EscortRequired:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.EscortRequired = (string)obj;
							}
							break;
						case ObrElements.FillerField1:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.FillerField1 = (string)obj;
							}
							break;
						case ObrElements.FillerField2:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.FillerField2 = (string)obj;
							}
							break;
						case ObrElements.FillerOrderNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.FillerOrderNumber = (string)obj;
							}
							break;
						case ObrElements.LabDept:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.LabDept = (string)obj;
							}
							break;
						case ObrElements.NumberOfSamplesContainers:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.NumberOfSamplesContainers = (string)obj;
							}
							break;
						case ObrElements.ObservationDateTime:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ObservationDateTime = (string)obj;
							}
							break;
						case ObrElements.ObservationEndDateTime:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ObservationEndDateTime = (string)obj;
							}
							break;
						case ObrElements.OrderCallBackPhoneNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.OrderCallBackPhoneNumber = (string)obj;
							}
							break;
						case ObrElements.OrderingProvider:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.NPI = GetComponent(_encode, (string)obj, (int)ProviderName.NPI);
								obr.FirstName = GetComponent(_encode, (string)obj, (int)ProviderName.FirstName);
								obr.LastName = GetComponent(_encode, (string)obj, (int)ProviderName.LastName);
								obr.MiddleName = GetComponent(_encode, (string)obj, (int)ProviderName.MiddleName);
								obr.Suffix = GetComponent(_encode, (string)obj, (int)ProviderName.Suffix);
							}
							break;
						case ObrElements.ParentNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ParentNumber = (string)obj;
							}
							break;
						case ObrElements.ParentResult:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ParentResult = (string)obj;
							}
							break;
						case ObrElements.PlacerField1:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.PlacerField1 = (string)obj;
							}
							break;
						case ObrElements.PlacerField2:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.PlacerField2 = (string)obj;
							}
							break;
						case ObrElements.PlacerOrderNumber:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.PlacerOrderNumber = (string)obj;
							}
							break;
						case ObrElements.PlannedPatientTranspComm:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.PlannedPatientTranspComm = (string)obj;
							}
							break;
						case ObrElements.PrincipalResultInterpreter:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.PrincipalResultInterpreter = (string)obj;
							}
							break;
						case ObrElements.Priority:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.Priority = (string)obj;
							}
							break;
						case ObrElements.QuantityTiming:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.QuantityTiming = (string)obj;
							}
							break;
						case ObrElements.ReasonForStudy:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ReasonForStudy = (string)obj;
							}
							break;
						case ObrElements.RequestedDateTime:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.RequestedDateTime = (string)obj;
							}
							break;
						case ObrElements.ResultCopiesTo:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ResultCopiesTo = (string)obj;
							}
							break;
						case ObrElements.ResultRPT:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ResultRPT = (string)obj;
							}
							break;
						case ObrElements.ResultStatus:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ResultStatus = (string)obj;
							}
							break;
						case ObrElements.ScheduledDateTime:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.SCheduledDateTim = (string)obj;
							}
							break;
						case ObrElements.ServiceIdentifer:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.TestCode = GetComponent(_encode, (string)obj, (int)ServiceIdentifer.testCode);
								obr.TestCodeDescription = GetComponent(_encode, (string)obj, (int)ServiceIdentifer.testCodeDescription);
								obr.CodingSystem = GetComponent(_encode, (string)obj, (int)ServiceIdentifer.codingSystem);
								obr.LimTestCode = GetComponent(_encode, (string)obj, (int)ServiceIdentifer.limTestCode);
								obr.LimTestCodeDescription = GetComponent(_encode, (string)obj, (int)ServiceIdentifer.limTestCodeDescription);
							}
							break;
						case ObrElements.SpecimenActionCode:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.SpecimenActionCode = (string)obj;
							}
							break;
						case ObrElements.SpecimenReceivedDateTime:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.SpecimenReceivedDateTime = (string)obj;
							}
							break;
						case ObrElements.SpecimenSource:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.SpecimenSource = (string)obj;
							}
							break;
						case ObrElements.Technician:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.Technician = (string)obj;
							}
							break;
						case ObrElements.Transcriptionsist:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.Transcriptionist = (string)obj;
							}
							break;
						case ObrElements.TransporationMode:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.TransporationMode = (string)obj;
							}
							break;
						case ObrElements.TransportArranged:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.TransportArranged = (string)obj;
							}
							break;
						case ObrElements.TransportArrangement:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.TransportArrangement = (string)obj;
							}
							break;
						case ObrElements.TransportCallSamp:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.TransportCallSamp = (string)obj;
							}
							break;
						case ObrElements.ClinicalInfo:
							if (!string.IsNullOrEmpty((string)obj))
							{
								obr.ClinicalInfo = (string)obj;
							}
							break;

						default:
							obr.Errors.Add(string.Format("{0}:{1} - Error element ({2}) not found", modName, fnName, ((ObrElements)i).ToString()));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((ObrElements)nIdx).ToString(), ex);
				obr.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return obr;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given object
		///            make this call after the hl7 segment string has been set
		/// </summary>
		/// <param name="seg">OBR object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(OBR seg, HL7Encoding _encode)
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
				segErrors.Add(new SegmentError(seg.SegmentMsg, "N/A", sTmp));
			}
			return segErrors;
		}
	}
}
