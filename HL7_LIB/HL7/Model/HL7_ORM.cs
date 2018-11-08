using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model
{
	/// <summary>
	/// HL7_ORM - The following objects will contian the HL7 ORM objects
	/// HL7Header
	/// HL7Patient
	/// {Hl7Orders}
	/// </summary>
	public class HL7_ORM
	{
		public HL7_ORM()
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

		private List<HL7Order> mHL7Orders;
		public List<HL7Order> HL7Orders
		{
			get { return mHL7Orders; }
			set { mHL7Orders = value; }
		}
	}
}
