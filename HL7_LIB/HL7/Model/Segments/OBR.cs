using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum ObrElements
	{
		Segment = 0
		, SeqId
		, PlacerOrderNumber
		, FillerOrderNumber
		, ServiceIdentifer
		, Priority
		, RequestedDateTime
		, ObservationDateTime
		, ObservationEndDateTime
		, CollectionVolume
		, CollectionIdentifier
		, SpecimenActionCode
		, DangerCode
		, ClinicalInfo
		, SpecimenReceivedDateTime
		, SpecimenSource
		, OrderingProvider
		, OrderCallBackPhoneNumber
		, PlacerField1
		, PlacerField2
		, FillerField1
		, FillerField2
		, ResultRPT
		, ChargeToPractice
		, LabDept
		, ResultStatus
		, ParentResult
		, QuantityTiming
		, ResultCopiesTo
		, ParentNumber
		, TransporationMode
		, ReasonForStudy
		, PrincipalResultInterpreter
		, AssistantResultInterpreter
		, Technician
		, Transcriptionsist
		, ScheduledDateTime
		, NumberOfSamplesContainers
		, TransportCallSamp
		, CollectorsComments
		, TransportArrangement
		, TransportArranged
		, EscortRequired
		, PlannedPatientTranspComm
	}

	public enum ProviderName
	{
		NPI = 1
		, LastName
		, FirstName
		, MiddleName
		, Suffix
	}

	public enum ServiceIdentifer
	{
		testCode = 1
		, testCodeDescription
		, codingSystem
		, limTestCode
		, limTestCodeDescription
	}

	public class OBR
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>
			{
				new RequiredField("OBR", ObrElements.SeqId.ToString(), "int", (float)ObrElements.SeqId, 0, true),
				new RequiredField("OBR", ObrElements.PlacerOrderNumber.ToString(), "string", (float)ObrElements.PlacerOrderNumber, 10, true),
				new RequiredField("OBR", ObrElements.FillerOrderNumber.ToString(), "string", (float)ObrElements.FillerOrderNumber, 10, false),
				new RequiredField("OBR", ObrElements.ServiceIdentifer.ToString(), "string", (float)ObrElements.ServiceIdentifer, 255, true),
				new RequiredField("OBR", ObrElements.ObservationDateTime.ToString(), "Date", (float)ObrElements.ObservationDateTime, 14, true),
				new RequiredField("OBR", ObrElements.SpecimenReceivedDateTime.ToString(), "Date", (float)ObrElements.SpecimenReceivedDateTime, 14, true),
				new RequiredField("OBR", ObrElements.OrderingProvider.ToString(), "string", (float)ObrElements.OrderingProvider, 255, true)
			};
			return (reqFields);
		}

		public OBR()
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

		private string mPlacerOrderNumber;
		public string PlacerOrderNumber
		{
			get { return mPlacerOrderNumber; }
			set { mPlacerOrderNumber = value; }
		}

		private string mFillerOrderNumber;
		public string FillerOrderNumber
		{
			get { return mFillerOrderNumber; }
			set { mFillerOrderNumber = value; }
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

		private string mPriority;
		public string Priority
		{
			get { return mPriority; }
			set { mPriority = value; }
		}

		private string mRequestedDateTime;
		public string RequestedDateTime
		{
			get { return mRequestedDateTime; }
			set { mRequestedDateTime = value; }
		}

		// YYYYMMDDHHMMSS
		private string mObservationDateTime;
		public string ObservationDateTime
		{
			get { return mObservationDateTime; }
			set { mObservationDateTime = value; }
		}

		private string mObservationEndDateTime;
		public string ObservationEndDateTime
		{
			get { return mObservationEndDateTime; }
			set { mObservationEndDateTime = value; }
		}

		// Number^Units
		private string mCollectionVolume;
		public string CollectionVolume
		{
			get { return mCollectionVolume; }
			set { mCollectionVolume = value; }
		}

		private string mCollectionIdentifier;
		public string CollectionIdentifier
		{
			get { return mCollectionIdentifier; }
			set { mCollectionIdentifier = value; }
		}

		private string mSpecimenActionCode;
		public string SpecimenActionCode
		{
			get { return mSpecimenActionCode; }
			set { mSpecimenActionCode = value; }
		}

		private string mDangerCode;
		public string DangerCode
		{
			get { return mDangerCode; }
			set { mDangerCode = value; }
		}

		private string mClinicalInfo;
		public string ClinicalInfo
		{
			get { return mClinicalInfo; }
			set { mClinicalInfo = value; }
		}

		//YYYYMMDDHHMMSS
		private string mSpecimenReceivedDateTime;
		public string SpecimenReceivedDateTime
		{
			get { return mSpecimenReceivedDateTime; }
			set { mSpecimenReceivedDateTime = value; }
		}

		private string mSpecimenSource;
		public string SpecimenSource
		{
			get { return mSpecimenSource; }
			set { mSpecimenSource = value; }
		}

		// ProviderID^LastName^FirstName^MI^Suffix^^^^^^^^Type
		private string mNPI;
		public string NPI
		{
			get { return mNPI; }
			set { mNPI = value; }
		}
		private string mLastName;
		public string LastName
		{
			get { return mLastName; }
			set { mLastName = value; }
		}
		private string mFirstName;
		public string FirstName
		{
			get { return mFirstName; }
			set { mFirstName = value; }
		}
		private string mMiddleName;
		public string MiddleName
		{
			get { return mMiddleName; }
			set { mMiddleName = value; }
		}
		private string mSuffix;
		public string Suffix
		{
			get { return mSuffix; }
			set { mSuffix = value; }
		}

		private string mOrderCallBackPhoneNumber;
		public string OrderCallBackPhoneNumber
		{
			get { return mOrderCallBackPhoneNumber; }
			set { mOrderCallBackPhoneNumber = value; }
		}

		private string mPlacerField1;
		public string PlacerField1
		{
			get { return mPlacerField1; }
			set { mPlacerField1 = value; }
		}

		private string mPlacerField2;
		public string PlacerField2
		{
			get { return mPlacerField2; }
			set { mPlacerField2 = value; }
		}

		private string mFillerField1;
		public string FillerField1
		{
			get { return mFillerField1; }
			set { mFillerField1 = value; }
		}

		private string mFillerField2;
		public string FillerField2
		{
			get { return mFillerField2; }
			set { mFillerField2 = value; }
		}

		// YYYYMMDDHHMM
		private string mResultRPT;
		public string ResultRPT
		{
			get { return mResultRPT; }
			set { mResultRPT = value; }
		}

		private string mChargeToPractice;
		public string ChargeToPractice
		{
			get { return mChargeToPractice; }
			set { mChargeToPractice = value; }
		}

		private string mLabDept;
		public string LabDept
		{
			get { return mLabDept; }
			set { mLabDept = value; }
		}

		private string mResultStatus;
		public string ResultStatus
		{
			get { return mResultStatus; }
			set { mResultStatus = value; }
		}

		private string mParentResult;
		public string ParentResult
		{
			get { return mParentResult; }
			set { mParentResult = value; }
		}

		private string mQuantityTiming;
		public string QuantityTiming
		{
			get { return mQuantityTiming; }
			set { mQuantityTiming = value; }
		}

		private string mResultCopiesTo;
		public string ResultCopiesTo
		{
			get { return mResultCopiesTo; }
			set { mResultCopiesTo = value; }
		}

		private string mParentNumber;
		public string ParentNumber
		{
			get { return mParentNumber; }
			set { mParentNumber = value; }
		}

		private string mTransporationMode;
		public string TransporationMode
		{
			get { return mTransporationMode; }
			set { mTransporationMode = value; }
		}

		private string mReasonForStudy;
		public string ReasonForStudy
		{
			get { return mReasonForStudy; }
			set { mReasonForStudy = value; }
		}

		private string mPrincipalResultInterpreter;
		public string PrincipalResultInterpreter
		{
			get { return mPrincipalResultInterpreter; }
			set { mPrincipalResultInterpreter = value; }
		}

		private string mAssistantResultInterpreter;
		public string AssistantResultInterpereter
		{
			get { return mAssistantResultInterpreter; }
			set { mAssistantResultInterpreter = value; }
		}

		private string mTechnician;
		public string Technician
		{
			get { return mTechnician; }
			set { mTechnician = value; }
		}

		private string mTranscriptionsist;
		public string Transcriptionist
		{
			get { return mTranscriptionsist; }
			set { mTranscriptionsist = value; }
		}

		private string mScheduledDateTime;
		public string SCheduledDateTim
		{
			get { return mScheduledDateTime; }
			set { mScheduledDateTime = value; }
		}

		private string mNumberOfSamplesContainers;
		public string NumberOfSamplesContainers
		{
			get { return mNumberOfSamplesContainers; }
			set { mNumberOfSamplesContainers = value; }
		}

		private string mTransportCallSamp;
		public string TransportCallSamp
		{
			get { return mTransportCallSamp; }
			set { mTransportCallSamp = value; }
		}

		private string mCollectorsComments;
		public string CollectorsComments
		{
			get { return mCollectorsComments; }
			set { mCollectorsComments = value; }
		}

		private string mTransportArrangement;
		public string TransportArrangement
		{
			get { return mTransportArrangement; }
			set { mTransportArrangement = value; }
		}

		private string mTransportArranged;
		public string TransportArranged
		{
			get { return mTransportArranged; }
			set { mTransportArranged = value; }
		}

		private string mEscortRequired;
		public string EscortRequired
		{
			get { return mEscortRequired; }
			set { mEscortRequired = value; }
		}

		private string mPlannedPatientTranspComm;
		public string PlannedPatientTranspComm
		{
			get { return mPlannedPatientTranspComm; }
			set { mPlannedPatientTranspComm = value; }
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
