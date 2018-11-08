using System;
using PTOX_LIB.HL7.Model;
using System.Collections.Generic;
using PTOX_LIB.HL7.Model.Segments;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	/// BuildIN1
	///     Validate and Build up the HL7 PID segment object
	/// </summary>
	public class BuildIN1 : HL7Parser
	{
		const string modName = "BuildIN1";

		public BuildIN1()
		{
		}

		/// <summary>
		/// GetPV1
		///     Get the HL7 IN1 segment and parse into the IN1 object.
		///     Also perform field validation of required fields and types
		///     Verify no errors have been detected during processing
		/// </summary>
		/// <param name="line">HL7 segment to parse</param>
		/// <returns>IN1 object</returns>
		public IN1 GetIN1(HL7Encoding _encode, string line)
		{
			const string fnName = "GetIN1";

			List<SegmentError> segError = null;
			IN1 in1 = new IN1();
			int nIdx = 0;
			try
			{
				in1.SegmentMsg = line;
				in1.Segment = "IN1";
				segError = Validate(in1, _encode);

				// var enumCnt = Enum.GetNames(typeof(mshElements)).Length;
				foreach (int i in Enum.GetValues(typeof(In1Elements)))
				{
					nIdx = i;
					object obj = GetElement(_encode, line, i);
					if (obj == null)
					{
						// check if this a required field
						string sTmp1 = ((Gt1Elements)i).ToString();
						RequiredField rqFld = in1.RequiredFields.Find(x => x.FieldName.Equals(sTmp1));
						if (rqFld != null && rqFld.IsRequired)
						{
							in1.Errors.Add(string.Format("{0}:{1} - WARNING Element ({2}) not found in segment, at field {3}", modName, fnName, ((In1Elements)i).ToString(), i));
						}
						continue;
					}
					switch ((In1Elements)i)
					{
						case In1Elements.Segment:
							in1.Segment = (string)obj;
							break;
						case In1Elements.SeqId:
							if (!string.IsNullOrEmpty((string)obj))
							{
								in1.SeqId = int.Parse((string)obj);
							}
							break;
						case In1Elements.AdmissionCode:
							in1.AddmissinCode = (string)obj;
							break;
						case In1Elements.AdmissionDate:
							in1.AdmissionDate = (string)obj;
							break;
						case In1Elements.AgreementCode:
							in1.AgreementCode = (string)obj;
							break;
						case In1Elements.AssignmentBenefits:
							in1.AssignmentBenefits = (string)obj;
							break;
						case In1Elements.Authorization:
							in1.Authorization = (string)obj;
							break;
						case In1Elements.BenefitsPriority:
							in1.BenefitsPriority = (string)obj;
							break;
						case In1Elements.BillingStatus:
							in1.BillingStatus = (string)obj;
							break;
						case In1Elements.CompanyAddress:
							in1.CompanyAddress = (string)obj;
							break;
						case In1Elements.CompanyCode:
							in1.CompanyCode = (string)obj;
							break;
						case In1Elements.CompanyContact:
							in1.CompanyContact = (string)obj;
							break;
						case In1Elements.CompanyName:
							in1.CompanyName = (string)obj;
							break;
						case In1Elements.CompanyPhone:
							in1.CompanyPhone = (string)obj;
							break;
						case In1Elements.CompanyPlanCode:
							in1.CompanyPlanCode = (string)obj;
							break;
						case In1Elements.CoordinationBenefits:
							in1.CoordinationBenefits = (string)obj;
							break;
						case In1Elements.CoverageType:
							in1.CoverageType = (string)obj;
							break;
						case In1Elements.DelayBefore:
							in1.DelayBefore = (string)obj;
							break;
						case In1Elements.EffectiveDate:
							in1.EffectiveDate = (string)obj;
							break;
						case In1Elements.EligibilityCode:
							in1.EligibilityCode = (string)obj;
							break;
						case In1Elements.EligibilityDate:
							in1.EligibilityDate = (string)obj;
							break;
						case In1Elements.EmployerAddress:
							in1.EmployerAddress = (string)obj;
							break;
						case In1Elements.EmployerId:
							in1.EmployerId = (string)obj;
							break;
						case In1Elements.EmployerName:
							in1.EmployerName = (string)obj;
							break;
						case In1Elements.EmploymentStatus:
							in1.EmploymentStatus = (string)obj;
							break;
						case In1Elements.ExpirationDate:
							in1.ExpirationDate = (string)obj;
							break;
						case In1Elements.GroupName:
							in1.GroupName = (string)obj;
							break;
						case In1Elements.GroupNumber:
							in1.GroupNumber = (string)obj;
							break;
						case In1Elements.Handicap:
							in1.Handicap = (string)obj;
							break;
						case In1Elements.InsuredAddress:
							in1.InsuredAddress = (string)obj;
							break;
						case In1Elements.InsuredDOB:
							in1.InsuredDOB = (string)obj;
							break;
						case In1Elements.InsuredIdNumber:
							in1.InsuredIdNumber = (string)obj;
							break;
						case In1Elements.InsuredName:
							in1.InsuredName = (string)obj;
							break;
						case In1Elements.InsuredSex:
							in1.InsuredSex = (string)obj;
							break;
						case In1Elements.PlayType:
							in1.PlanType = (string)obj;
							break;
						case In1Elements.PolicyAmount:
							in1.PolicyAmount = (string)obj;
							break;
						case In1Elements.PolicyDays:
							in1.PolicyDays = (string)obj;
							break;
						case In1Elements.PolicyDeductible:
							in1.PolicyDeductible = (string)obj;
							break;
						case In1Elements.PolicyNumber:
							in1.PolicyNumber = (string)obj;
							break;
						case In1Elements.PreAdmin:
							in1.PreAdmin = (string)obj;
							break;
						case In1Elements.PriorInsurancePlanId:
							in1.PriorInsurancePlanId = (string)obj;
							break;
						case In1Elements.RelationshipTo:
							in1.RelationshipTo = (string)obj;
							break;
						case In1Elements.ReleaseCode:
							in1.ReleaseCode = (string)obj;
							break;
						case In1Elements.ReserveDays:
							in1.ReserveDays = (string)obj;
							break;
						case In1Elements.RoomPrivate:
							in1.RoomPrivate = (string)obj;
							break;
						case In1Elements.RoomSemiPrivate:
							in1.RoomSemiPrivate = (string)obj;
							break;
						case In1Elements.ShortName:
							in1.ShortName = (string)obj;
							break;
						case In1Elements.VerificationBy:
							in1.VerificationBy = (string)obj;
							break;
						case In1Elements.VerificationDate:
							in1.VerificationDate = (string)obj;
							break;
						case In1Elements.VerificationStatus:
							in1.VerificationStatus = (string)obj;
							break;

						default:
							in1.Errors.Add(string.Format("{0}:{1} - Error element ({0}) not found", modName, fnName, ((In1Elements)i).ToString()));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("Exception:{0}:{1} - ({2}) {3}", modName, fnName, ((In1Elements)nIdx).ToString(), ex);
				in1.Errors.Add(sErr);
				Console.WriteLine(sErr);
			}
			return in1;
		}

		/// <summary>
		/// Validate - Validate the required fields for the given HL7 segment object
		/// </summary>
		/// <param name="seg">HL7 IN1 segment object</param>
		/// <returns>list<SegmentError></returns>
		private List<SegmentError> Validate(IN1 seg, HL7Encoding _encode)
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
