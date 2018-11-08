using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTOX_LIB.STARLIMS;


namespace PTOX_LIB.HL7.Model.Segments
{
	public enum In1Elements
	{
		Segment = 0
		 , SeqId
		 , ShortName
		 , CompanyCode
		 , CompanyName
		 , CompanyAddress
		 , CompanyContact
		 , CompanyPhone
		 , GroupNumber
		 , GroupName
		 , EmployerId
		 , EmployerName
		 , EffectiveDate
		 , ExpirationDate
		 , Authorization
		 , PlayType
		 , InsuredName
		 , RelationshipTo
		 , InsuredDOB
		 , InsuredAddress
		 , AssignmentBenefits
		 , CoordinationBenefits
		 , BenefitsPriority
		 , AdmissionCode
		 , AdmissionDate
		 , EligibilityCode
		 , EligibilityDate
		 , ReleaseCode
		 , PreAdmin
		 , VerificationDate
		 , VerificationBy
		 , AgreementCode
		 , BillingStatus
		 , ReserveDays
		 , DelayBefore
		 , CompanyPlanCode
		 , PolicyNumber
		 , PolicyDeductible
		 , PolicyAmount
		 , PolicyDays
		 , RoomSemiPrivate
		 , RoomPrivate
		 , EmploymentStatus
		 , InsuredSex
		 , EmployerAddress
		 , VerificationStatus
		 , PriorInsurancePlanId
		 , CoverageType
		 , Handicap
		 , InsuredIdNumber
	}

	public enum CompanyAddress
	{
		Address1 = 1    // start at position 1 in HL7 message
		 , City
		 , State
		 , Zip
	}

	/// <summary>
	/// IN1 - Insurance segment
	/// </summary>
	public class IN1
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>();

			reqFields.Add(new RequiredField("IN1", In1Elements.SeqId.ToString(), "int", (float)In1Elements.SeqId, DB_Constants.IN1_SEQUENCE, true));                 // sequence Id 
			reqFields.Add(new RequiredField("IN1", In1Elements.CompanyCode.ToString(), "int", (float)In1Elements.CompanyCode, DB_Constants.IN1_COMPANYCODE, true));           // Company_ID 
			reqFields.Add(new RequiredField("IN1", In1Elements.CompanyName.ToString(), "string", (float)In1Elements.CompanyName, DB_Constants.IN1_COMPANYNAME, true));       // Company name
			reqFields.Add(new RequiredField("IN1", In1Elements.CompanyAddress.ToString(), "string", (float)In1Elements.CompanyAddress, DB_Constants.IN1_ADDRESS, true));    // insurance company Address
			reqFields.Add(new RequiredField("IN1", In1Elements.RelationshipTo.ToString(), "string", (float)In1Elements.RelationshipTo, DB_Constants.IN1_RELATIONSHIP, true));   // Relationship
			reqFields.Add(new RequiredField("IN1", In1Elements.InsuredDOB.ToString(), "date", (float)In1Elements.InsuredDOB, DB_Constants.IN1_INSURED_DOB, true));               // Insured DOB (Date) 
			reqFields.Add(new RequiredField("IN1", In1Elements.PolicyNumber.ToString(), "string", (float)In1Elements.PolicyNumber, DB_Constants.IN1_POLICYNUMBER, true));        // Policy no
			reqFields.Add(new RequiredField("IN1", In1Elements.InsuredSex.ToString(), "string", (float)In1Elements.InsuredSex, DB_Constants.IN1_INSURED_SEX, false));           // Insured sex
			return (reqFields);
		}

		public IN1()
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

		private string mShortName;
		public string ShortName
		{
			get { return mShortName; }
			set { mShortName = value; }
		}

		private string mCompanyCode;
		public string CompanyCode
		{
			get { return mCompanyCode; }
			set { mCompanyCode = value; }
		}

		private string mCompanyName;
		public string CompanyName
		{
			get { return mCompanyName; }
			set { mCompanyName = value; }
		}

		private string mCompanyAddress;
		public string CompanyAddress
		{
			get { return mCompanyAddress; }
			set { mCompanyAddress = value; }
		}
		private string mCompanyCity;
		public string CompanyCity
		{
			get { return mCompanyCity; }
			set { mCompanyCity = value; }
		}
		private string mCompanyState;
		public string CompanyState
		{
			get { return mCompanyState; }
			set { mCompanyState = value; }
		}
		private string mCompanyZip;
		public string CompanyZip
		{
			get { return mCompanyZip; }
			set { mCompanyZip = value; }
		}

		private string mCompanyContact;
		public string CompanyContact
		{
			get { return mCompanyContact; }
			set { mCompanyContact = value; }
		}

		private string mCompanyPhone;
		public string CompanyPhone
		{
			get { return mCompanyPhone; }
			set { mCompanyPhone = value; }
		}

		private string mGroupNumber;
		public string GroupNumber
		{
			get { return mGroupNumber; }
			set { mGroupNumber = value; }
		}

		private string mGroupName;
		public string GroupName
		{
			get { return mGroupName; }
			set { mGroupName = value; }
		}

		private string mEmployerId;
		public string EmployerId
		{
			get { return mEmployerId; }
			set { mEmployerId = value; }
		}

		private string mEmployerName;
		public string EmployerName
		{
			get { return mEmployerName; }
			set { mEmployerName = value; }
		}

		private string mEffectiveDate;
		public string EffectiveDate
		{
			get { return mEffectiveDate; }
			set { mEffectiveDate = value; }
		}

		private string mExpirationDate;
		public string ExpirationDate
		{
			get { return mExpirationDate; }
			set { mExpirationDate = value; }
		}

		private string mAuthorization;
		public string Authorization
		{
			get { return mAuthorization; }
			set { mAuthorization = value; }
		}

		private string mPlayType;
		public string PlanType
		{
			get { return mPlayType; }
			set { mPlayType = value; }
		}

		// LastName^FirstName^MI
		private string mInsuredName;
		public string InsuredName
		{
			get { return mInsuredName; }
			set { mInsuredName = value; }
		}

		private string mRelationshipTo;
		public string RelationshipTo
		{
			get { return mRelationshipTo; }
			set { mRelationshipTo = value; }
		}

		// YYYYMMDD
		private string mInsuredDOB;
		public string InsuredDOB
		{
			get { return mInsuredDOB; }
			set { mInsuredDOB = value; }
		}

		// Address1^Address2^City^State^zip
		private string mInsuredAddress;
		public string InsuredAddress
		{
			get { return mInsuredAddress; }
			set { mInsuredAddress = value; }
		}

		private string mAssignmentBenefits;
		public string AssignmentBenefits
		{
			get { return mAssignmentBenefits; }
			set { mAssignmentBenefits = value; }
		}

		private string mCoordinationBenefits;

		public string CoordinationBenefits
		{
			get { return mCoordinationBenefits; }
			set { mCoordinationBenefits = value; }
		}

		private string mBenefitsPriority;
		public string BenefitsPriority
		{
			get { return mBenefitsPriority; }
			set { mBenefitsPriority = value; }
		}

		private string mAdmissionCode;
		public string AddmissinCode
		{
			get { return mAdmissionCode; }
			set { mAdmissionCode = value; }
		}

		private string mAdmissionDate;
		public string AdmissionDate
		{
			get { return mAdmissionDate; }
			set { mAdmissionDate = value; }
		}

		private string mEligibilityCode;
		public string EligibilityCode
		{
			get { return mEligibilityCode; }
			set { mEligibilityCode = value; }
		}

		private string mEligibilityDate;
		public string EligibilityDate
		{
			get { return mEligibilityDate; }
			set { mEligibilityDate = value; }
		}

		private string mReleaseCode;
		public string ReleaseCode
		{
			get { return mReleaseCode; }
			set { mReleaseCode = value; }
		}

		private string mPreAdmin;
		public string PreAdmin
		{
			get { return mPreAdmin; }
			set { mPreAdmin = value; }
		}

		private string mVerificationDate;
		public string VerificationDate
		{
			get { return mVerificationDate; }
			set { mVerificationDate = value; }
		}

		private string mVerificationBy;
		public string VerificationBy
		{
			get { return mVerificationBy; }
			set { mVerificationBy = value; }
		}

		private string mAgreementCode;
		public string AgreementCode
		{
			get { return mAgreementCode; }
			set { mAgreementCode = value; }
		}

		private string mBillingStatus;
		public string BillingStatus
		{
			get { return mBillingStatus; }
			set { mBillingStatus = value; }
		}

		private string mReserveDays;
		public string ReserveDays
		{
			get { return mReserveDays; }
			set { mReserveDays = value; }
		}

		private string mDelayBefore;
		public string DelayBefore
		{
			get { return mDelayBefore; }
			set { mDelayBefore = value; }
		}

		private string mCompanyPlanCode;
		public string CompanyPlanCode
		{
			get { return mCompanyPlanCode; }
			set { mCompanyPlanCode = value; }
		}

		private string mPolicyNumber;
		public string PolicyNumber
		{
			get { return mPolicyNumber; }
			set { mPolicyNumber = value; }
		}

		private string mPolicyDeductible;
		public string PolicyDeductible
		{
			get { return mPolicyDeductible; }
			set { mPolicyDeductible = value; }
		}

		private string mPolicyAmount;
		public string PolicyAmount
		{
			get { return mPolicyAmount; }
			set { mPolicyAmount = value; }
		}

		private string mPolicyDays;
		public string PolicyDays
		{
			get { return mPolicyDays; }
			set { mPolicyDays = value; }
		}

		private string mRoomSemiPrivate;
		public string RoomSemiPrivate
		{
			get { return mRoomSemiPrivate; }
			set { mRoomSemiPrivate = value; }
		}

		private string mRoomPrivate;
		public string RoomPrivate
		{
			get { return mRoomPrivate; }
			set { mRoomPrivate = value; }
		}

		private string mEmploymentStatus;
		public string EmploymentStatus
		{
			get { return mEmploymentStatus; }
			set { mEmploymentStatus = value; }
		}

		// M=Male, F=Female, U=Unknown
		private string mInsuredSex;
		public string InsuredSex
		{
			get { return mInsuredSex; }
			set { mInsuredSex = value; }
		}

		private string mEmployerAddress;
		public string EmployerAddress
		{
			get { return mEmployerAddress; }
			set { mEmployerAddress = value; }
		}

		private string mVerificationStatus;
		public string VerificationStatus
		{
			get { return mVerificationStatus; }
			set { mVerificationStatus = value; }
		}

		private string mPriorInsurancePlanId;
		public string PriorInsurancePlanId
		{
			get { return mPriorInsurancePlanId; }
			set { mPriorInsurancePlanId = value; }
		}

		// T=3rd party; P=Patient bill; C=Client bill
		private string mCoverageType;
		public string CoverageType
		{
			get { return mCoverageType; }
			set { mCoverageType = value; }
		}

		private string mHandicap;
		public string Handicap
		{
			get { return mHandicap; }
			set { mHandicap = value; }
		}

		private string mInsuredIdNumber;
		public string InsuredIdNumber
		{
			get { return mInsuredIdNumber; }
			set { mInsuredIdNumber = value; }
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
