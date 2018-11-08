using PTOX_LIB.HL7.Controller;
using PTOX_LIB.HL7.Model.Segments;
using System.Collections.Generic;
using static PTOX_LIB.HL7.Model.Segments.MSH;

namespace PTOX_LIB.HL7.Model
{
	/// <summary>
	/// HL7Patient - Patient object can contain the following segments
	///	[] =  optional
	///	{} = repeats
	/// 
	///	PID			  | --- Patient
	///	[PD1]         | --- 
	///	[{NTE}]       | --- Notes 
	///   [PV1]         | --- Patient Visit
	///	[PV2]         | --- Patient Visit
	///	[{             | Insurance group
	///		IN1 --------| --- Insurance
	///	   [IN2] ------| --- Insurance
	///	   [IN3] ------| --- Insurance
	///	}]
	///	[GT1] ---------| --- Gauntor
	///	[{AL1}] -------| --- Optional, if present can repeat
	///  
	/// </summary>
	public class HL7Patient
	{
		public HL7Patient(string messageType)
		{
			PIDSegment = new PID(messageType);
			PD1Segment = new PD1();
			NTESegments = new List<NTE>();
			Visit1 = new PV1();
			Visit2 = new PV2();
			Insurance1 = new IN1();
			Insurance2 = new IN1();
			Insurance3 = new IN1();
			GT1Segment = new GT1();
			AL1Segments = new List<AL1>();

			Errors = new List<string>();
		}

		private PID pID;
		public PID PIDSegment
		{
			get { return pID; }
			set { pID = value; }
		}

		private PD1 pD1;
		public PD1 PD1Segment
		{
			get { return pD1; }
			set { pD1 = value; }
		}

		private List<NTE> nTE;
		public List<NTE> NTESegments
		{
			get { return nTE; }
			set { nTE = value; }
		}

		private PV1 pV1;
		public PV1 Visit1
		{
			get { return pV1; }
			set { pV1 = value; }
		}

		private PV2 pV2;
		public PV2 Visit2
		{
			get { return pV2; }
			set { pV2 = value; }
		}

		private IN1 iN1;
		public IN1 Insurance1
		{
			get { return iN1; }
			set { iN1 = value; }
		}
		private IN1 iN2;
		public IN1 Insurance2
		{
			get { return iN2; }
			set { iN2 = value; }
		}

		private IN1 iN3;
		public IN1 Insurance3
		{
			get { return iN3; }
			set { iN3 = value; }
		}

		private GT1 gT1;
		public GT1 GT1Segment
		{
			get { return gT1; }
			set { gT1 = value; }
		}

		private List<AL1> aL1;
		public List<AL1> AL1Segments
		{
			get { return aL1; }
			set { aL1 = value; }
		}

		private List<string> mErrors;
		public List<string> Errors
		{
			get { return mErrors; }
			set { mErrors = value; }
		}
	}
}
