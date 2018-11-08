using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTOX_LIB.STARLIMS;


namespace PTOX_LIB.HL7.Model.Segments
{

	public enum PidElements
	{
		Segment = 0
		 , SeqId                 // segment sequence
		 , ExternalId            // External System Patient Id (orders only) size(15)
		 , PatientId             // Precision PID        size(15)
		 , AltPID                // Not used
		 , PatientName           // Last^First^MI        size(50,50,20)
		 , MotherMadenName       // Not used
		 , DOB                   // YYYYMMDD             size(8)
		 , Gender                // Male, Female, Other  size(10)
		 , PatAlias              // not used
		 , Race                  // optional             size(255)
		 , Address               // Addr1,Addr2,City,State,Zip  size(255,255,50,3,12)
		 , Phone                 // Number^extension     size(10,?)
		 , BPhone                // number^extension     size(10,?)
		 , Language              // not used
		 , MaritalStatus         //                      size(10)
		 , Religion              // not used
		 , PatientAccountNumber  // not used             size(25)
		 , SSN                   // optional             size(20)
										 //, DriversLicense
										 //, MotherIdentifier
										 //, EthnicGroup
										 //, BirthPlace
										 //, MultipleBirthPlaceIndicator
										 //, BirthOrder
										 //, Citizenship
										 //, VeteransMilitaryStatus
										 //, Nationality
										 //, PatientDeathDateTime
										 //, PatientDeathIndicator
	}

	public enum PatientName
	{
		LastName = 1    // start at position 1 in HL7 message
		, FirstName
		, MiddleInitial
	}

	public enum PatientAddress
	{
		Address1 = 1    // start at position 1 in HL7 message
		, Address2
		, City
		, State
		, Zip
	}


	/// <summary>
	/// PID - Patient Identificaiton segment
	/// </summary>
	public class PID
	{
		private List<RequiredField> ReqFields(string msgType)
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>();

			reqFields.Add(new RequiredField("PID", PidElements.SeqId.ToString(), "int", (float)PidElements.SeqId, DB_Constants.PID_SEQUENCE, true));
			reqFields.Add(new RequiredField("PID", PidElements.PatientName.ToString(), "string", (float)PidElements.PatientName, DB_Constants.PID_PATIENTNAME, true));
			reqFields.Add(new RequiredField("PID", PidElements.Gender.ToString(), "string", (float)PidElements.Gender, DB_Constants.PID_GENDER, true));

			if ("ORM".Equals(msgType))
			{
				reqFields.Add(new RequiredField("PID", PidElements.ExternalId.ToString(), "string", (float)PidElements.ExternalId, DB_Constants.PID_EXTERNAL_ID, true));
				reqFields.Add(new RequiredField("PID", PidElements.PatientId.ToString(), "string", (float)PidElements.PatientId, DB_Constants.PID_PATIENT_ID, false));
				reqFields.Add(new RequiredField("PID", PidElements.Race.ToString(), "string", (float)PidElements.Race, DB_Constants.PID_RACE, false));

				reqFields.Add(new RequiredField("PID", PidElements.Address.ToString(), "string", (float)PidElements.Address, DB_Constants.PID_ADDRESS, true));
				reqFields.Add(new RequiredField("PID", PidElements.Address.ToString(), "string", (float)PidElements.Address, DB_Constants.PID_ADDRESS, true));
				reqFields.Add(new RequiredField("PID", PidElements.Address.ToString(), "string", (float)PidElements.Address, DB_Constants.PID_ADDRESS, true));

				reqFields.Add(new RequiredField("PID", PidElements.Phone.ToString(), "string", (float)PidElements.Phone, DB_Constants.PID_PHONE, false));
				reqFields.Add(new RequiredField("PID", PidElements.BPhone.ToString(), "string", (float)PidElements.BPhone, DB_Constants.PID_BUSINESSPHONE, false));
				reqFields.Add(new RequiredField("PID", PidElements.MaritalStatus.ToString(), "string", (float)PidElements.MaritalStatus, DB_Constants.PID_MARITALSTATUS, false));
				reqFields.Add(new RequiredField("PID", PidElements.SSN.ToString(), "string", (float)PidElements.SSN, DB_Constants.PID_SSN, false));                                                                                                                                                              //reqFields.Add(new RequiredField("PID", pidElements.AltPID.ToString(), "string", (float)pidElements.AltPID, 50, false));                          // Alternate patient Id
			}
			else
			{
				reqFields.Add(new RequiredField("PID", PidElements.PatientId.ToString(), "string", (float)PidElements.PatientId, DB_Constants.PID_PATIENT_ID, true));
			}

			return (reqFields);
		}

		public PID(string msgType)
		{
			RequiredFields = ReqFields(msgType);

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

		private string mExternalId;
		public string ExternalId
		{
			get { return mExternalId; }
			set { mExternalId = value; }
		}

		private string mPatientId;
		public string PatientId
		{
			get { return mPatientId; }
			set { mPatientId = value; }
		}

		private string mAltPID;
		public string AlternatePatientId
		{
			get { return mAltPID; }
			set { mAltPID = value; }
		}

		private string mFName;
		public string FName
		{
			get { return mFName; }
			set { mFName = value; }
		}

		private string mLName;
		public string LName
		{
			get { return mLName; }
			set { mLName = value; }
		}

		private string mMiddleInitial;
		public string MiddleInitial
		{
			get { return mMiddleInitial; }
			set { mMiddleInitial = value; }
		}

		private string mMotherMadenName;
		public string MotherMadenName
		{
			get { return mMotherMadenName; }
			set { mMotherMadenName = value; }
		}

		private string sDOB;
		public string DOB
		{
			get { return sDOB; }
			set { sDOB = value; }
		}
		private DateTime mDOB;
		public DateTime DtDOB
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

		private string mPatAlias;
		public string PatientAlias
		{
			get { return mPatAlias; }
			set { mPatAlias = value; }
		}

		private string mRace;
		public string Race
		{
			get { return mRace; }
			set { mRace = value; }
		}

		private string mAddr1;
		public string Address1
		{
			get { return mAddr1; }
			set { mAddr1 = value; }
		}

		private string mAddr2;
		public string Address2
		{
			get { return mAddr2; }
			set { mAddr2 = value; }
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

		private int mPhone;
		public int Phone
		{
			get { return mPhone; }
			set { mPhone = value; }
		}

		private int mBPhone;
		public int BusinessPhone
		{
			get { return mBPhone; }
			set { mBPhone = value; }
		}

		private string mLanguage;
		public string Language
		{
			get { return mLanguage; }
			set { mLanguage = value; }
		}

		private string mMaritalStatus;
		public string MaritalStatus
		{
			get { return mMaritalStatus; }
			set { mMaritalStatus = value; }
		}

		private string mReligion;
		public string Religion
		{
			get { return mReligion; }
			set { mReligion = value; }
		}

		private string mPatientAccountNumber;
		public string PatientAccountNumber
		{
			get { return mPatientAccountNumber; }
			set { mPatientAccountNumber = value; }
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
