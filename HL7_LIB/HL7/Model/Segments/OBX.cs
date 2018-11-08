using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum obxElements
	{
		Segment = 0
		, SeqId
		, ValueType
		, ObservationIdentifier
		, ObservationSubId
		, ObservationValue
		, Units
		, ReferenceRange
		, AbnormalFlags
		, Probability
		, AbnormalTest
		, ResultStatus
		, ObservedLastDate
		, AccessChecks
		, ObservationDateTime
		, Producers_ID
	}

	public enum observationIdentifier
	{
		testCode = 1
		, testCodeDescription
		, codingSystem
		, limTestCode
		, limTestCodeDescription
	}

	public class OBX
	{
		private List<RequiredField> ReqFields(string msgType)
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>();

			reqFields.Add(new RequiredField("OBX", obxElements.SeqId.ToString(), "int", (float)obxElements.SeqId, 1, true));
			reqFields.Add(new RequiredField("OBX", obxElements.ValueType.ToString(), "string", (float)obxElements.ValueType, 25, false));
			reqFields.Add(new RequiredField("OBX", obxElements.ObservationIdentifier.ToString(), "string", (float)obxElements.ObservationIdentifier, 255, false));
			reqFields.Add(new RequiredField("OBX", obxElements.ObservationSubId.ToString(), "string", (float)obxElements.ObservationSubId, 25, false));
			reqFields.Add(new RequiredField("OBX", obxElements.ObservationValue.ToString(), "string", (float)obxElements.ObservationValue, 100, true));

			if ("ORU".Equals(msgType))
			{
				reqFields.Add(new RequiredField("OBX", obxElements.Units.ToString(), "string", (float)obxElements.Units, 25, true));
				reqFields.Add(new RequiredField("OBX", obxElements.ReferenceRange.ToString(), "string", (float)obxElements.ReferenceRange, 100, true));
				reqFields.Add(new RequiredField("OBX", obxElements.ResultStatus.ToString(), "string", (float)obxElements.ResultStatus, 100, true));
				reqFields.Add(new RequiredField("OBX", obxElements.ObservedLastDate.ToString(), "string", (float)obxElements.ObservedLastDate, 100, true));
				reqFields.Add(new RequiredField("OBX", obxElements.Producers_ID.ToString(), "string", (float)obxElements.Producers_ID, 100, true));
			}
			return (reqFields);
		}

		public OBX(string messageType)
		{
			RequiredFields = ReqFields(messageType);

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

		private string mValueType;
		public string ValueType
		{
			get { return mValueType; }
			set { mValueType = value; }
		}

		// Observatoin sequence number
		private string observationSubId;
		public string ObservationSubId
		{
			get { return observationSubId; }
			set { observationSubId = value; }
		}

		// Result Value
		private string observationValue;
		public string ObservationValue
		{
			get { return observationValue; }
			set { observationValue = value; }
		}

		// Test Code^Description^CodingSystem^LIM Test code^Description
		private string _testCode;
		public string TestCode
		{
			get { return _testCode; }
			set { _testCode = value; }
		}
		private string _tcDescription;
		public string TestCodeDescription
		{
			get { return _tcDescription; }
			set { _tcDescription = value; }
		}
		private string _codingSystem;
		public string CodingSystem
		{
			get { return _codingSystem; }
			set { _codingSystem = value; }
		}
		private string _limTestCode;
		public string LimTestCode
		{
			get { return _limTestCode; }
			set { _limTestCode = value; }
		}
		private string _limTestCodeDescription;
		public string LimTestCodeDescription
		{
			get { return _limTestCodeDescription; }
			set { _limTestCodeDescription = value; }
		}

		private string _units;
		public string Units
		{
			get { return _units; }
			set { _units = value; }
		}

		private string _referenceRange;
		public string ReferenceRange
		{
			get { return _referenceRange; }
			set { _referenceRange = value; }
		}

		private string _abnormalFlags;
		public string AbnormalFlags
		{
			get { return _abnormalFlags; }
			set { _abnormalFlags = value; }
		}

		private string _probability;
		public string Probability
		{
			get { return _probability; }
			set { _probability = value; }
		}

		private string _abnormalTest;
		public string AbnormalTest
		{
			get { return _abnormalTest; }
			set { _abnormalTest = value; }
		}

		private string _resultStatus;
		public string ResultStatus
		{
			get { return _resultStatus; }
			set { _resultStatus = value; }
		}

		private string _observedLastDate;
		public string ObservedLastDate
		{
			get { return _observedLastDate; }
			set { _observedLastDate = value; }
		}

		private string _accessChecks;
		public string AccessChecks
		{
			get { return _accessChecks; }
			set { _accessChecks = value; }
		}

		private string _observationDateTime;
		public string ObservationDateTime
		{
			get { return _observationDateTime; }
			set { _observationDateTime = value; }
		}

		private string _producers_ID;
		public string Producers_ID
		{
			get { return _producers_ID; }
			set { _producers_ID = value; }
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
