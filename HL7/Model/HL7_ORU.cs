using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model
{
    /// <summary>
    /// HL7_ORU - The following objects will contian the HL7 ORU objects
	 /// HL7Header
	 /// HL7Patient
	 /// {Hl7Results}
	 /// 
    /// </summary>
	public class HL7_ORU
	{
		public HL7_ORU()
		{
		}

		private HL7Header mHeader;
		public HL7Header HL7Header
		{
			get { return mHeader; }
			set { mHeader = value; }
		}

		private HL7Patient mHL7Patient;
		public HL7Patient HL7Patient
		{
			get { return mHL7Patient; }
			set { mHL7Patient = value; }
		}

		private List<HL7Results> mHL7Results;
		public List<HL7Results> HL7Results
		{
			get { return mHL7Results; }
			set { mHL7Results = value; }
		}
	}
}
