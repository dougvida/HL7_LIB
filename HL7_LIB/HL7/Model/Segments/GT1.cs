using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum Gt1Elements
	{
		Segment = 0
		, SeqId
		, GuarantorNumber
		, GuarantorName
		, GuarantorSpouseName
		, Address
		, PhoneNumber
		, BusinessPhone
		, DOB
		, Gender
		, GuarantorType
		, Relationship
		, SSN
	}

	public enum GuarantorName
	{
		LastName = 1    // start at position 1 in HL7 message
		, FirstName
		, MiddleInitial
	}

	public enum GuarantorAddress
	{
		Address1 = 1    // start at position 1 in HL7 message
		, Address2
		, City
		, State
		, Zip
	}

	/// <summary>
	/// GT1 - Guarantor segment
	/// </summary>
	public class GT1
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>
			{
				new RequiredField("GT1", Gt1Elements.SeqId.ToString(), "int", (float)Gt1Elements.SeqId, 1, false),
				new RequiredField("GT1", Gt1Elements.GuarantorName.ToString(), "string", (float)Gt1Elements.GuarantorName, 180, true),
				new RequiredField("GT1", Gt1Elements.Address.ToString(), "string", (float)Gt1Elements.Address + 0.1, 157, false),
				new RequiredField("GT1", Gt1Elements.PhoneNumber.ToString(), "string", (float)Gt1Elements.PhoneNumber, 10, false),
				new RequiredField("GT1", Gt1Elements.DOB.ToString(), "date", (float)Gt1Elements.DOB, 14, false),
				new RequiredField("GT1", Gt1Elements.Gender.ToString(), "string", (float)Gt1Elements.Gender, 16, false),
				new RequiredField("GT1", Gt1Elements.Relationship.ToString(), "string", (float)Gt1Elements.Relationship, 2, false),
				new RequiredField("GT1", Gt1Elements.SSN.ToString(), "string", (float)Gt1Elements.SSN, 9, false)
			};
			return (reqFields);
		}

		public GT1()
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

		private string mGuarantorNumber;
		public string GuarantorNumber
		{
			get { return mGuarantorNumber; }
			set { mGuarantorNumber = value; }
		}

		// LastName^FirstName^MI
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

		private string mGuarantorSpouseName;
		public string GuarantorSpouseName
		{
			get { return mGuarantorSpouseName; }
			set { mGuarantorSpouseName = value; }
		}

		// Address1^Address2^City^State^Zip
		private string mAddress1;
		public string Address1
		{
			get { return mAddress1; }
			set { mAddress1 = value; }
		}
		private string mAddress2;
		public string Address2
		{
			get { return mAddress2; }
			set { mAddress2 = value; }
		}
		private string mCity;
		public string City
		{
			get { return mCity; }
			set { mCity = value; }
		}
		private string mState;
		public string State
		{
			get { return mState; }
			set { mState = value; }
		}
		private string mZip;
		public string Zip
		{
			get { return mZip; }
			set { mZip = value; }
		}

		// 9999999999
		private string mPhoneNumber;
		public string PhoneNumber
		{
			get { return mPhoneNumber; }
			set { mPhoneNumber = value; }
		}

		private string mBusinessPhone;
		public string BusinessPhone
		{
			get { return mBusinessPhone; }
			set { mBusinessPhone = value; }
		}

		// YYYYMMDD
		private string mDOB;
		public string DOB
		{
			get { return mDOB; }
			set { mDOB = value; }
		}

		private string mGender;
		public string Gender
		{
			get { return mGender; }
			set { mGender = value; }
		}

		private string mGuarantorType;
		public string GuarantorType
		{
			get { return mGuarantorType; }
			set { mGuarantorType = value; }
		}

		private string mRelationship;
		public string Relationship
		{
			get { return mRelationship; }
			set { mRelationship = value; }
		}

		private string mSSN;
		public string SSN
		{
			get { return mSSN; }
			set { mSSN = value; }
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
