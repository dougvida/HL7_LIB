using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTOX_LIB.HL7.Controller;
using PTOX_LIB.HL7.Model;
using PTOX_LIB.HL7.Model.Segments;
using System.Collections.Generic;

namespace PTOX_LIB_Test
{
	[TestClass]
	public class BuildORMTest
	{
		public static List<string> InitializeORM
		{
			get {
				List<string> lMsg = new List<string>
					 {
@"MSH|^~\&|CareEvolve|PTOX|LIS|Lab|20180926142110||ORM^O01|20180926142110689198|P|2.3|",
@"PID|1|1234AA|P20180294743||INSTEST^INSURANCE^||19900101|Male||2131-1|1010 TEST WAY^^SAN DIEGO^CA^92121|||||Other|",
@"GT1|1||INSTEST^INSURANCE^||1010 TEST WAY^^SAN DIEGO^CA^92121|||19900101|M||Self|",
@"IN1|1||ACCESSMEDICARE|ACCESS MEDICARE|1564 NORTHEAST EXPRESSWAY, MAIL STOP HQ2361^^ATLANTA^GA^30329|||999999910||||||||INSTEST^INSURANCE^|Self|19900101|1010 TEST WAY^^SAN DIEGO^CA^92121|||||||||||||||||900000010|||||||M|",
@"ORC|NW|CE00090016||||||||JANEDOE^DOE, MD PHD^JANE||1111122224^DOE, MD PHD^JANE^",
@"OBR|1|CE00090016||1760^(Urine) - Alprazolam - NextGen Quant||20180926141300|20180926141300|||JANEDOE^^^|||0||Urine|1111122224^DOE, MD PHD^JANE^|||I|",
@"DG1|1|I10|Z79.891|Long term (current) use of opiate analgesic|",
@"DG1|2|I10|F11.20|Opioid dependence, uncomplicated|",
@"DG1|3|I10|Z79.899|Other long term (current) drug therapy|",
@"OBR|2|CE00090016||1762^(Urine) - Amphetamine - NextGen Quant||20180926141300|20180926141300|||JANEDOE^^^|||0||Urine|1111122224^DOE, MD PHD^JANE^|||I|",
@"DG1|1|I10|Z79.891|Long term (current) use of opiate analgesic|",
@"DG1|2|I10|F11.20|Opioid dependence, uncomplicated|",
@"DG1|3|I10|Z79.899|Other long term (current) drug therapy|",
@"OBR|3|CE00090016||1777^(Urine) - Alcohol Metabolite (EtS/EtG) - NextGen Quant||20180926141300|20180926141300|||JANEDOE^^^||Urine|0|||1111122224^DOE, MD PHD^JANE^|||I|",
@"DG1|1|I10|Z79.891|Long term (current) use of opiate analgesic|",
@"DG1|2|I10|F11.20|Opioid dependence, uncomplicated|",
@"DG1|3|I10|Z79.899|Other long term (current) drug therapy|",
@"NTE|1||changed the group id",
@"NTE|2|L|Medications Oxycontin"
					 };
				return lMsg;
			}
		}

		ProcessHL7Message _processHL7 = null;
		public BuildORMTest()
		{
		}

		[TestMethod]
		public void TestORMParse()
		{
			HL7_ORM _order = null;

			List<string> lMsg = InitializeORM;

			// we need to get the HL7 field separator and encoding characters
			_processHL7 = ProcessHL7Message.Instance;
			// HL7_ORM _order = (HL7_ORM)_processHL7.ParseHL7(slMsg: lMsg);

			object obj = _processHL7.ParseHL7(slMsg: lMsg);
			if (obj is HL7_ORM)
				_order = (HL7_ORM)obj;
			else
				Assert.Fail("HL7 Message is not an ORM message type");

			if (IsHeaderValid(_order.HL7Header))
			{
				Assert.Fail("MHS Header validation failed");
			}

			// validate Patients
			if (IsPatientValid(_order.HL7Patient))
			{
				Assert.Fail("Patient segment failed validation");
			}

			// validate insurance segments
			if (IsPatientInsuranceValid(_order.HL7Patient))
			{
				Assert.Fail("Insurance segment failed validation");
			}

			if (_order.HL7Patient.NTESegments.Count > 0)
			{
				Assert.Fail("NTE segment failed validation");
			}
			if (_order.HL7Orders == null)
			{
				Assert.Fail("Orders object is NULL");
			}
			else
			{
				if (IsOrderValid(_order.HL7Orders))
				{
					Assert.Fail("Order segments failee");
				}
			}
		}

		private Boolean IsHeaderValid(HL7Header _header)
		{
			Boolean bErr = false;

			var data = _header.MSHSegment.Version;
			Assert.AreNotSame("2.3", data);
			data = _header.MSHSegment.SendingFacility;
			Assert.AreNotSame("PTOX", data);

			return bErr;
		}

		private Boolean IsPatientValid(HL7Patient _patient)
		{
			Boolean bErr = false;

			var data = _patient.PIDSegment.ExternalId;
			Assert.AreNotSame("1234AA", data);
			data = _patient.PIDSegment.PatientId;
			Assert.AreNotSame("P20180294743", data);

			data = _patient.PIDSegment.FName;
			Assert.AreNotSame("INSURANCE", data);
			data = _patient.PIDSegment.LName;
			Assert.AreNotSame("INSTEST", data);

			data = _patient.PIDSegment.DOB;
			Assert.AreNotSame("19900101", data);

			data = _patient.PIDSegment.Gender;
			Assert.AreNotSame("Male", data);

			data = _patient.PIDSegment.Address1;
			Assert.AreNotSame("1010 TEST WAY", data);
			data = _patient.PIDSegment.City;
			Assert.AreNotSame("SAN DIEGO", data);
			data = _patient.PIDSegment.State;
			Assert.AreNotSame("CA", data);
			data = _patient.PIDSegment.Zip;
			Assert.AreNotSame("92121", data);

			data = _patient.PIDSegment.MaritalStatus;
			Assert.AreNotSame("Other", data);

			return bErr;
		}

		private Boolean IsPatientInsuranceValid(HL7Patient _patient)
		{
			Boolean bErr = false;
			if (_patient.Insurance1 != null)
			{
				var data = _patient.Insurance1.CompanyCode;
				Assert.AreNotSame("ACCESSMEDICARE", data);
				data = _patient.Insurance1.CompanyName;
				Assert.AreNotSame("ACCESS MEDICARE", data);
				data = _patient.Insurance1.CompanyAddress;
				Assert.AreNotSame("1564 NORTHEAST EXPRESSWAY, MAIL STOP HQ2361", data);
				data = _patient.Insurance1.CompanyCity;
				Assert.AreNotSame("ATLANTA", data);
				data = _patient.Insurance1.CompanyState;
				Assert.AreNotSame("GA", data);
				data = _patient.Insurance1.CompanyZip;
				Assert.AreNotSame("30329", data);
			}
			if (_patient.Insurance2 == null)
			{
				Assert.Fail();
			}
			if (_patient.Insurance3 == null)
			{
				Assert.Fail();
			}
			return bErr;
		}

		private Boolean IsOrderValid(List<HL7Order> _orders)
		{
			Boolean bErr = false;
			foreach (var order in _orders)
			{
				List<HL7Details> details = order.HL7Details;

				// @"ORC|NW|CE00090016||||||||JANEDOE^DOE, MD PHD^JANE||1111122224^DOE, MD PHD^JANE^",
				Assert.AreNotSame("NW", order.ORCSegment.OrderControl);
				Assert.AreNotSame("CE00090016", order.ORCSegment.PlacerOrderNumber);
				Assert.AreNotSame("1111122224", order.ORCSegment.NPI);
				Assert.AreNotSame("DOE, MD PHD", order.ORCSegment.LastName);
				Assert.AreNotSame("JANE", order.ORCSegment.FirstName);

				foreach (var detail in details)
				{
					// OBR there should be one OBR per detail object.
					// can be many DG1 and NTE segments
					if (detail.OBRSegment == null)
					{
						Assert.Fail("OBR Segment is NULL");
					}
					Assert.AreNotSame("CE00090016", detail.OBRSegment.FillerOrderNumber);
					Assert.AreNotSame("1111122224", detail.OBRSegment.NPI);
					Assert.AreNotSame("DOE, MD PHD", detail.OBRSegment.LastName);
					Assert.AreNotSame("Urine", detail.OBRSegment.SpecimenSource);

					if (detail.DG1Segments.Count > 0)
					{
						int nIdx = 0;
						foreach (var dg1 in detail.DG1Segments)
						{
							if (IsDG1Valid(dg1, nIdx++))
							{
								Assert.Fail("DG1 segment validation failed");
							}
						}
					}

					// NTE repeating segments
					if (detail.NTESegments.Count > 0)
					{
						int nIdx = 0;
						foreach (var nte in detail.NTESegments)
						{
							if (IsNTEValid(nte, nIdx++))
							{
								Assert.Fail("NTE segment validation failed");
							}
						}
					}
				}
			}

			return bErr;
		}

		private Boolean IsDG1Valid(DG1 dg1, int nIdx)
		{
			Boolean bErr = false;

			switch (nIdx)
			{
				case 0:
					Assert.AreNotSame(1, dg1.SeqId);
					Assert.AreNotSame("I10", dg1.CodingMethod);
					Assert.AreNotSame("Z79.891", dg1.Code);
					Assert.AreNotSame("Long term (current) use of opiate analgesic", dg1.Description);
					break;
				case 1:
					Assert.AreNotSame(2, dg1.SeqId);
					Assert.AreNotSame("I10", dg1.CodingMethod);
					Assert.AreNotSame("F11.20", dg1.Code);
					Assert.AreNotSame("Opioid dependence, uncomplicated", dg1.Description);
					break;
				case 2:
					Assert.AreNotSame(3, dg1.SeqId);
					Assert.AreNotSame("I10", dg1.CodingMethod);
					Assert.AreNotSame("Z79.899", dg1.Code);
					Assert.AreNotSame("Other long term (current) drug therapy", dg1.Description);
					break;
				default:
					Assert.Fail("DG1 validation failed (default reached)");
					break;
			}
			return bErr;
		}

		private Boolean IsNTEValid(NTE nte, int nIdx)
		{
			Boolean bErr = false;

			switch (nIdx)
			{
				case 0:
					Assert.AreNotSame(1, nte.SeqId);
					Assert.AreNotSame("changed the group id", nte.Comment);
					break;
				case 1:
					Assert.AreNotSame(2, nte.SeqId);
					Assert.AreNotSame("Medications Oxycontin", nte.Comment);
					break;
				default:
					Assert.Fail("NTE validation failed (default reached)");
					break;
			}
			return bErr;
		}

	}
}
