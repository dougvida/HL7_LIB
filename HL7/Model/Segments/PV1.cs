using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum Pv1Elements
	{
		Segment = 0
		, SeqId
		, OrderingProvider
		, ReferringDr
		, ConsultingDr
	}
	/// <summary>
	/// PV1 - Patient visit segment
	/// Not normally used with Precision inbound orders
	/// 
	/// </summary>
	public class PV1
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>();

			reqFields.Add(new RequiredField("PV1", Pv1Elements.SeqId.ToString(), "string", 1.0, 10, true));    // sequence Id
			return (reqFields);
		}

		public PV1()
		{
			RequiredFields = ReqFields();

			this.Errors = new List<string>();
			this.SegmentMsg = string.Empty;
		}

		private List<RequiredField> mReqFields;
		public List<RequiredField> RequiredFields
		{
			get { return mReqFields; }
			set { mReqFields = value; }
		}

		private string mSegment;
		public string Segment
		{
			get { return mSegment; }
			set { mSegment = value; }
		}

		private int mSegId;
		public int SegId
		{
			get { return mSegId; }
			set { mSegId = value; }
		}

		private string mOrderingProvider;
		public string OrderingProvider
		{
			get { return mOrderingProvider; }
			set { mOrderingProvider = value; }
		}

		private string mReferringDr;
		public string ReferringDr
		{
			get { return mReferringDr; }
			set { mReferringDr = value; }
		}

		private string mConsultingDr;
		public string ConsultingDr
		{
			get { return mConsultingDr; }
			set { mConsultingDr = value; }
		}

		private string mSegmentMsg;
		public string SegmentMsg
		{
			get { return mSegmentMsg; }
			set { mSegmentMsg = value; }
		}

		private List<string> mError;
		public List<string> Errors
		{
			get { return mError; }
			set { mError = value; }
		}
	}
}
