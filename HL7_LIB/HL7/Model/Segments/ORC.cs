using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum orcElements
	{
		Segment = 0
		 , OrderControl
		 , PlacerOrderNumber
		 , FillerOrderNumber
		 , PlacerGroupNumber
		 , OrderStatus
		 , ResponseFlag
		 , QuantityTiming
		 , Parent
		 , TransactionDateTime
		 , EnteredBy
		 , VerifiedBy
		 , OrderingPhysician
		//        , EntererLocation
		//        , CallBackPhoneNumber
		//        , OrderEffectiveDateTime
		//        , OrderControlCodeReason
		//        , EnteringOrganization
		//        , EnteringDevice
		//        , ActionBy
	}

	public enum orcProvider
	{
		NPI = 1
		 , LastName
		 , FirstName
	}

	/// <summary>
	/// ORC - Common order segment
	/// </summary>
	public class ORC
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>();

			reqFields.Add(new RequiredField("ORC", orcElements.OrderControl.ToString(), "string", (float)orcElements.OrderControl, 2, true));
			reqFields.Add(new RequiredField("ORC", orcElements.PlacerOrderNumber.ToString(), "string", (float)orcElements.PlacerOrderNumber, 10, true));
			reqFields.Add(new RequiredField("ORC", orcElements.TransactionDateTime.ToString(), "date", (float)orcElements.TransactionDateTime, 20, true));
			reqFields.Add(new RequiredField("ORC", orcElements.OrderingPhysician.ToString(), "string", (float)orcElements.OrderingPhysician, 100, true));
			return (reqFields);
		}

		public ORC()
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

		private string mOrderControl;
		public string OrderControl
		{
			get { return mOrderControl; }
			set { mOrderControl = value; }
		}

		// Order Number
		private string mPlacerOrderNumber;
		public string PlacerOrderNumber
		{
			get { return mPlacerOrderNumber; }
			set { mPlacerOrderNumber = value; }
		}

		// Specimen ID
		private string mFillerOrderNumber;
		public string FillerOrderNumber
		{
			get { return mFillerOrderNumber; }
			set { mFillerOrderNumber = value; }
		}

		private string mPlacerGroupNumber;
		public string PlacerGroupNumber
		{
			get { return mPlacerGroupNumber; }
			set { mPlacerGroupNumber = value; }
		}

		private string mOrderStatus;
		public string OrderStatus
		{
			get { return mOrderStatus; }
			set { mOrderStatus = value; }
		}

		private string mResponseFlag;
		public string ResponseFlag
		{
			get { return mResponseFlag; }
			set { mResponseFlag = value; }
		}

		private string mQuantityTiming;
		public string QuantityTiming
		{
			get { return mQuantityTiming; }
			set { mQuantityTiming = value; }
		}

		private string mParent;
		public string Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}

		// YYYYMMDDHHMMSS
		private string mTransactionDateTime;
		public string TransactionDateTime
		{
			get { return mTransactionDateTime; }
			set { mTransactionDateTime = value; }
		}

		private string mEnteredBy;
		public string EnteredBy
		{
			get { return mEnteredBy; }
			set { mEnteredBy = value; }
		}

		private string mVerifiedBy;
		public string VerifiedBy
		{
			get { return mVerifiedBy; }
			set { mVerifiedBy = value; }
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
		private string mNPI;
		public string NPI
		{
			get { return mNPI; }
			set { mNPI = value; }
		}

		private string mEntererLocation;
		public string EntererLocation
		{
			get { return mEntererLocation; }
			set { mEntererLocation = value; }
		}

		private string mCallBackPhoneNumber;
		public string CallBackPhoneNumber
		{
			get { return mCallBackPhoneNumber; }
			set { mCallBackPhoneNumber = value; }
		}

		private string mOrderEffectiveDateTime;
		public string OrdereffectiveDateTime
		{
			get { return mOrderEffectiveDateTime; }
			set { mOrderEffectiveDateTime = value; }
		}

		private string mOrderControlCodeReason;
		public string OrderControlCodeReason
		{
			get { return mOrderControlCodeReason; }
			set { mOrderControlCodeReason = value; }
		}

		private string mEnteringOrganization;
		public string EnteringOrganization
		{
			get { return mEnteringOrganization; }
			set { mEnteringOrganization = value; }
		}

		private string mEnteringDevice;
		public string EnteringDevice
		{
			get { return mEnteringDevice; }
			set { mEnteringDevice = value; }
		}

		private string mActionBy;
		public string ActionBy
		{
			get { return mActionBy; }
			set { mActionBy = value; }
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
