using PTOX_LIB.STARLIMS;
using System;
using System.Collections.Generic;


namespace PTOX_LIB.HL7.Model.Segments
{
	public enum MshElements
	{
		Segment = 0
		 , Encoding
		 , SendingApp
		 , SendingFacility
		 , ReceivingApp
		 , ReceivingFacility
		 , TimeOfMessage
		 , Security
		 , MessageType
		 , MsgCtrlId
		 , ProcessingId
		 , Version
		 , Special
	}

	/// <summary>
	/// MSH - Message Header Segment (Required)
	/// </summary>
	public class MSH
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			// Enum.GetName(typeof(mshElements.Encoding))
			// this is a special segment for field counts. 
			// subtract 1 because the first '|' character is counted as one
			List<RequiredField> reqFields = new List<RequiredField>();
			reqFields.Add(new RequiredField("MSH", MshElements.Encoding.ToString(), "string", (float)MshElements.Encoding, DB_Constants.MSH_ENCODING_SIZE, true));             // MSH.2 Encoding Characters
			reqFields.Add(new RequiredField("MSH", MshElements.SendingFacility.ToString(), "string", (float)MshElements.SendingFacility + 0.1, DB_Constants.MSH_SENDING_FACILITY, true));   // MSH.3 Sending facility/Client Code
			reqFields.Add(new RequiredField("MSH", MshElements.TimeOfMessage.ToString(), "date", (float)MshElements.TimeOfMessage, DB_Constants.MSH_DATETIME_RECEIVED, true));      // MSH.6 Date/Time received message
			reqFields.Add(new RequiredField("MSH", MshElements.MessageType.ToString(), "string", (float)MshElements.MessageType, DB_Constants.MSH_MESSAGE_TYPE, true));           // MSH.8 Message Type
			reqFields.Add(new RequiredField("MSH", MshElements.MsgCtrlId.ToString(), "string", (float)MshElements.MsgCtrlId, DB_Constants.MSH_MESSAGE_CONTROL, true));          // MSH.9 Message Control Id stage table 20, central receiving 30
			reqFields.Add(new RequiredField("MSH", MshElements.Version.ToString(), "string", (float)MshElements.Version, DB_Constants.MSH_VERSION_ID, true));                 // MSH.10 Message Control Id stage table 20, central receiving 30
			reqFields.Add(new RequiredField("MSH", MshElements.Special.ToString(), "string", (float)MshElements.Special, 50, false));
			return (reqFields);
		}

		public MSH(HL7Encoding _encoding)
		{
			mMsgEncoding = _encoding;
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

		private string mEncoding;
		public string Encoding
		{
			get { return mEncoding; }
			set { mEncoding = value; }
		}

		private string mSendingApp;
		public string SendingApp
		{
			get { return mSendingApp; }
			set { mSendingApp = value; }
		}

		// required
		private string mSendingFacility;
		public string SendingFacility
		{
			get { return mSendingFacility; }
			set { mSendingFacility = value; }
		}

		private string mReceivingApp;
		public string ReceivingApp
		{
			get { return mReceivingApp; }
			set { mReceivingApp = value; }
		}

		private string mReceivingFacility;
		public string ReceivingFacility
		{
			get { return mReceivingFacility; }
			set { mReceivingFacility = value; }
		}

		// required
		private string mTimeOfMessage;
		public string TimeOfMessage
		{
			get { return mTimeOfMessage; }
			set { mTimeOfMessage = value; }
		}

		private DateTime mdtTimeOfMessage;
		public DateTime TimeOfMessageDT
		{
			get { return mdtTimeOfMessage; }
			set { mdtTimeOfMessage = value; }
		}

		private string mSecurity;
		public string Security
		{
			get { return mSecurity; }
			set { mSecurity = value; }
		}

		private string mMessageType;
		public string MessageType
		{
			get { return mMessageType; }
			set { mMessageType = value; }
		}

		// required
		private string mMsgCtrlId;
		public string MessageControlId
		{
			get { return mMsgCtrlId; }
			set { mMsgCtrlId = value; }
		}

		private string mProcessingId;
		public string ProcessingId
		{
			get { return mProcessingId; }
			set { mProcessingId = value; }
		}

		private string mVersion;
		public string Version
		{
			get { return mVersion; }
			set { mVersion = value; }
		}

		private string mSpecial;
		public string Special
		{
			get { return mSpecial; }
			set { mSpecial = value; }
		}

		private HL7Encoding mMsgEncoding;
		//        public HL7Encoding MsgEncoding
		//        {
		//            get { return mMsgEncoding; }
		//            set { mMsgEncoding = value; }
		//        }

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
