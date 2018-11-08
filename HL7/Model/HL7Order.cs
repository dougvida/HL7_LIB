using PTOX_LIB.HL7.Model.Segments;
using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model
{
	/// <summary>
	/// HL7Order - Get all the segments that can build an HL7_ORM object
	///	[] = Optional
	///	{} = Repeats
	///	
	///	ORC -------------------| --- Common order segment
	///	[NTE] -----------------| --- Notes
	///		{Order_Details} ----| ---- HL7Details
	///	[CTI] -----------------| --- Clinical trial identification
	///	[BLG] -----------------| --- Billing segment
	/// </summary>
	public class HL7Order
	{

		public HL7Order()
		{
			ORCSegment = new ORC();
			NTESegments = new List<NTE>();
			HL7Details = new List<HL7Details>();
			CTISegments = new List<CTI>();
			BLGSegment = new BLG();
		}

		private ORC mORC;
		public ORC ORCSegment
		{
			get { return mORC; }
			set { mORC = value; }
		}

		private List<NTE> mNTE;
		public List<NTE> NTESegments
		{
			get { return mNTE; }
			set { mNTE = value; }
		}

		private List<HL7Details> mHL7Details;
		public List<HL7Details> HL7Details
		{
			get { return mHL7Details; }
			set { mHL7Details = value; }
		}

		private List<CTI> mCTI;
		public List<CTI> CTISegments
		{
			get { return mCTI; }
			set { mCTI = value; }
		}

		private BLG mBLG;
		public BLG BLGSegment
		{
			get { return mBLG; }
			set { mBLG = value; }
		}
	}
}
