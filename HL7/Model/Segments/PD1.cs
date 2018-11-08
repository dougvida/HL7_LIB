using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model.Segments
{
	public enum Pd1Elements
	{
		Segment = 0
		 , SeqId
		  , PatientID
		  , PID
		  , LastName
		  , FirstName
		  , MiddleName
		  , DOB
		  , Gender
		  , Address1
		  , Address2
		  , City
		  , State
		  , Zip
		  , HomePhone
		  , BusinessPhone
		  , MaritalStatus
		  , SSN
	}

	/// <summary>
	/// PD1 - Patient demographics segment
	/// not used at Precision
	/// </summary>
	public class PD1
	{
		private List<RequiredField> ReqFields()
		{
			// can check if the inbound line is equal to one of the required field and verify type and size
			List<RequiredField> reqFields = new List<RequiredField>();

			reqFields.Add(new RequiredField("PD1", Pd1Elements.SeqId.ToString(), "int", 1.0, 10, true));				// Sequence Id
			reqFields.Add(new RequiredField("PD1", Pd1Elements.PatientID.ToString(), "string", 2.0, 15, true));	// Patient Id
			reqFields.Add(new RequiredField("PD1", Pd1Elements.PID.ToString(), "string", 3.0, 15, true));			// Precision PID
																																					// 4 Alternate Patient ID - optional
			reqFields.Add(new RequiredField("PD1", Pd1Elements.LastName.ToString(), "string", 5.1, 50, true));		// Last Name
			reqFields.Add(new RequiredField("PD1", Pd1Elements.FirstName.ToString(), "string", 5.2, 50, true));	// First Name
			reqFields.Add(new RequiredField("PD1", Pd1Elements.MiddleName.ToString(), "string", 5.3, 20, true));	// Middle Name
																																					// 6 Mothers maiden name - optional
			reqFields.Add(new RequiredField("PD1", Pd1Elements.DOB.ToString(), "datetime", 7.0, 8, true));			// Patient Last Name
			reqFields.Add(new RequiredField("PD1", Pd1Elements.Gender.ToString(), "string", 4.1, 50, true));		// Patient Last Name
																																					// 9 Patient Alias - optional
																																					// 10 Race 255 - optional
			reqFields.Add(new RequiredField("PD1", Pd1Elements.Address1.ToString(), "string", 11.1, 255, true));  // Patient Last Name
			reqFields.Add(new RequiredField("PD1", Pd1Elements.Address2.ToString(), "string", 11.2, 255, true));  // Patient Last Name
			reqFields.Add(new RequiredField("PD1", Pd1Elements.City.ToString(), "string", 11.3, 50, true));			// Patient Last Name
			reqFields.Add(new RequiredField("PD1", Pd1Elements.State.ToString(), "string", 11.4, 2, true));			// Patient Last Name
			reqFields.Add(new RequiredField("PD1", Pd1Elements.Zip.ToString(), "string", 11.5, 12, true));			// Patient Last Name
																																					// 11 Patient address (Addr1=255, Addr2=255, City=50, ST=3, Zip=12)
																																					// 12 County Code - optional
			reqFields.Add(new RequiredField("PD1", Pd1Elements.HomePhone.ToString(), "string", 13.1, 10, true));  // Patient Last Name
																																					// 13 Phone number Home (13.1=phone number, 13.2=extension) 10 ??
			reqFields.Add(new RequiredField("PD1", Pd1Elements.BusinessPhone.ToString(), "string", 14.1, 10, true));	// Patient Last Name
																																						// 14 Phone number Business (14.1=number, 14.2=extension) 10
																																						// 15 Primary Language - optional
			reqFields.Add(new RequiredField("PD1", Pd1Elements.MaritalStatus.ToString(), "string", 16.0, 10, true));	// Patient Last Name
																																						// 16 Marital Status 10
																																						// 17 Religion - optional
																																						// 18 Patient account number - optional 25
			reqFields.Add(new RequiredField("PD1", Pd1Elements.SSN.ToString(), "string", 19.0, 9, true));				// Patient Last Name
																																						// 19 SSN - optional 9
																																						// 20 - 30 (N/A) 
			return (reqFields);
		}

		public PD1()
		{
			RequiredFields = ReqFields();

			this.Errors = new List<string>();
			this.SegmentMsg = string.Empty;
		}

		private List<RequiredField> mReqFields;
		public List<RequiredField> RequiredFields { get => mReqFields; set => mReqFields = value; }

		private string mFieldSeparator;
		public string FieldSeparator { get => mFieldSeparator; set => mFieldSeparator = value; }

		private string mSegment;
		public string Segment { get => mSegment; set => mSegment = value; }

		private int mSeqId;
		public int SeqId { get => mSeqId; set => mSeqId = value; }

		private string mPatientID;
		public string PatientID { get => mPatientID; set => mPatientID = value; }

		private string mPID;
		public string PID { get => mPID; set => mPID = value; }

		private string mLastName;
		public string LastName { get => mLastName; set => mLastName = value; }

		private string mFirstName;
		public string FirstName { get => mFirstName; set => mFirstName = value; }

		private string mMiddleName;
		public string MiddleName { get => mMiddleName; set => mMiddleName = value; }

		private string mDOB;
		public string DOB { get => mDOB; set => mDOB = value; }

		private string mGender;
		public string Gender { get => mGender; set => mGender = value; }

		private string mAddress1;
		public string Address1 { get => mAddress1; set => mAddress1 = value; }

		private string mAddress2;
		public string Address2 { get => mAddress2; set => mAddress2 = value; }

		private string mCity;
		public string City { get => mCity; set => mCity = value; }

		private string mState;
		public string State { get => mState; set => mState = value; }

		private string mZip;
		public string Zip { get => mZip; set => mZip = value; }

		private string mHomePhone;
		public string HomePhone { get => mHomePhone; set => mHomePhone = value; }

		private string mBusinessPhone;
		public string BusinessPhone { get => mBusinessPhone; set => mBusinessPhone = value; }

		private string mMaritalStatus;
		public string MaritalStatus { get => mMaritalStatus; set => mMaritalStatus = value; }

		private string mSSN;
		public string SSN { get => mSSN; set => mSSN = value; }

		private string mSegmentMsg;
		public string SegmentMsg { get => mSegmentMsg; set => mSegmentMsg = value; }

		private List<string> mErrors;
		public List<string> Errors { get => mErrors; set => mErrors = value; }

	}
}
