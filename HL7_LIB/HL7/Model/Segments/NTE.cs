
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum NteElements
	{
		Segment = 0
		, SeqId
		, Source
		, Comment
	}

	/// <summary>
	/// NTE - Notes and Comments segment
	/// </summary>
	public class NTE
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>
			{
				new RequiredField("NTE", NteElements.SeqId.ToString(), "int", (float)NteElements.SeqId, 0, true),
				new RequiredField("NTE", NteElements.Source.ToString(), "string", (float)NteElements.Source, 8, true),
				new RequiredField("NTE", NteElements.Comment.ToString(), "string", (float)NteElements.Comment, 4000, true)
			};
			return (reqFields);
		}

		public NTE()
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

		private string mSource;
		public string Source
		{
			get { return mSource; }
			set { mSource = value; }
		}

		private string mComment;
		public string Comment
		{
			get { return mComment; }
			set { mComment = value; }
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
