using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model.Segments
{
    public enum ctiElements
    {
        Segment = 0
        ,SeqId
    }

    public class CTI
    {
        /// <summary>
        /// RequiredCTIFields - NEED TO FIX UP
        /// </summary>
        /// <returns></returns>
        private List<RequiredField> ReqFields()
        {
            // can check if the inbound line is equal to one of the required field and verify type and size
            List<RequiredField> reqFields = new List<RequiredField>();

            reqFields.Add(new RequiredField("CTI", ctiElements.SeqId.ToString(), "int", 1.0, 0, true));    // sequence Id
            return (reqFields);
        }

        public CTI()
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

        private List<string> mErrors;
        public List<string> Errors
        {
            get { return mErrors; }
            set { mErrors = value; }
        }
    }
}
