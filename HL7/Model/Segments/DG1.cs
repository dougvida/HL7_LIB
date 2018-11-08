using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum dg1Elements
	{
		Segment = 0
		, SeqId
        , CodingMethod
        , Code
        , Description
	}

	public class DG1
	{
        private List<RequiredField> ReqFields()
        {
            // can check if the inbound line is equal to one of the required field and verify type and size
            List<RequiredField> reqFields = new List<RequiredField>();

            reqFields.Add(new RequiredField("DG1", dg1Elements.CodingMethod.ToString(), "string", (float)dg1Elements.CodingMethod, 25, true));
            reqFields.Add(new RequiredField("DG1", dg1Elements.Code.ToString(), "string", (float)dg1Elements.Code, 50, true));
            reqFields.Add(new RequiredField("DG1", dg1Elements.Description.ToString(), "string", (float)dg1Elements.Description, 255, false));
            return (reqFields);
        }

        public DG1()
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

		private int mSeqId;
		public int SeqId
		{
			get { return mSeqId; }
			set { mSeqId = value; }
		}

		private string mSegmentMsg;
		public string SegmentMsg
		{
			get { return mSegmentMsg; }
			set { mSegmentMsg = value; }
		}

        private string mCodingMethod;
        public string CodingMethod
        {
            get { return mCodingMethod; }
            set { mCodingMethod = value; }
        }

        private string mCode;
        public string Code
        {
            get { return mCode; }
            set { mCode = value; }
        }

        private string mDescription;
        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        private List<string> mErrors;
		public List<string> Errors
		{
			get { return mErrors; }
			set { mErrors = value; }
		}
	}
}
