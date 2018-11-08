using PTOX_LIB.HL7.Controller;
using PTOX_LIB.HL7.Model;
using PTOX_LIB.HL7.Model.Segments;
using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model
{
	public class HL7Header : HL7Parser
	{
        public HL7Header(MSH msh, List<NTE> notes, HL7Encoding _encoding)
        {
            MSHSegment = msh;
            NTESegments = notes;
            HL7Encoding = _encoding;
        }

        private HL7Encoding mEncoding;
        public HL7Encoding HL7Encoding
        {
            get { return mEncoding; }
            set { mEncoding = value; }
        }

        private MSH mMSH;
		public MSH MSHSegment
		{
			get { return mMSH; }
			set { mMSH = value; }
		}

		private List<NTE> mNTE;
		public List<NTE> NTESegments
		{
			get { return mNTE; }
			set { mNTE = value; }
		}
	}
}
