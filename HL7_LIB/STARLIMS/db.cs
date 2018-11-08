using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTOX_LIB.STARLIMS
{
	static class DB_Constants
	{
		// MSH Segment
		private const int mMSH_FIELD_SEPARATOR = 1;      // MSH.0  Field Separator
		public static int MSH_FIELD_SEPARATOR => mMSH_FIELD_SEPARATOR;

		private const int mMSH_ENCODING_SIZE = 4;        // MSH.2  Encoding Characters
		public static int MSH_ENCODING_SIZE => mMSH_ENCODING_SIZE;

		private const int mMSH_SENDING_APP = 100;        // MSH.3  Sending facility/Client Code
		public static int MSH_SENDING_APP => mMSH_SENDING_APP;

		private const int mMSH_SENDING_FACILITY = 100;   // MSH.4  Sending facility/Client Code
		public static int MSH_SENDING_FACILITY => mMSH_SENDING_FACILITY;

		private const int mMSH_RECEIVING_APP = 100;      // MSH.5  Sending facility/Client Code
		public static int MSH_RECEIVING_APP => mMSH_RECEIVING_APP;

		private const int mMSH_RECEIVING_FACILITY = 100; // MSH.6  Sending facility/Client Code
		public static int MSH_RECEIVING_FACILITY => mMSH_RECEIVING_FACILITY;

		private const int mMSH_DATETIME_RECEIVED = 14;   // MSH.7  Date/Time received message
		public static int MSH_DATETIME_RECEIVED => mMSH_DATETIME_RECEIVED;

		private const int mMSH_SECURITY = 4;             // MSH.8  Security
		public static int MSH_SECURITY => mMSH_SECURITY;

		private const int mMSH_MESSAGE_TYPE = 50;        // MSH.9  Message Type
		public static int MSH_MESSAGE_TYPE => mMSH_MESSAGE_TYPE;

		private const int mMSH_MESSAGE_CONTROL = 20;     // MSH.10 Message Control Id stage table 20, central receiving 30
		public static int MSH_MESSAGE_CONTROL => mMSH_MESSAGE_CONTROL;

		private const int mMSH_PROCESSING_ID = 1;        // MSH.11 Processing ID
		public static int MSH_PROCESSING_ID => mMSH_PROCESSING_ID;

		private const int mMSH_VERSION_ID = 10;          // MSH.12 Version ID
		public static int MSH_VERSION_ID => mMSH_VERSION_ID;

		private const int mMSH_SEQUENCE_NUMBER = 60;          // MSH.13 Sequence Number - Should only be 15 len
		public static int MSH_SEQUENCE_NUMBER => mMSH_SEQUENCE_NUMBER;


		// PID segment
		private const int mPID_SEQUENCE = 1;
		public static int PID_SEQUENCE => mPID_SEQUENCE;

		private const int mPID_EXTERNAL_ID = 15;
		public static int PID_EXTERNAL_ID => mPID_EXTERNAL_ID;

		private const int mPID_PATIENT_ID = 15;
		public static int PID_PATIENT_ID => mPID_PATIENT_ID;

		private const int mPID_PATIENTNAME = 120;
		public static int PID_PATIENTNAME => mPID_PATIENTNAME;

		private const int mPID_DOB = 8;
		public static int PID_DOB => PID_DOB;

		private const int mPID_GENDER = 10;
		public static int PID_GENDER => mPID_GENDER;

		private const int mPID_RACE = 10;
		public static int PID_RACE => mPID_RACE;

		private const int mPID_ADDRESS = 122;
		public static int PID_ADDRESS => mPID_ADDRESS;
		private const int mPID_ADDRESS1 = 40;
		public static int PID_ADDRESS1 => mPID_ADDRESS1;
		private const int mPID_ADDRESS2 = 40;
		public static int PID_ADDRESS2 => mPID_ADDRESS2;
		private const int mPID_CITY = 30;
		public static int PID_CITY => mPID_CITY;
		private const int mPID_STATE = 2;
		public static int PID_STATE => mPID_STATE;
		private const int mPID_ZIP = 10;
		public static int PID_ZIP => mPID_ZIP;

		private const int mPID_PHONE = 25;
		public static int PID_PHONE => mPID_PHONE;

		private const int mPID_BUSINESSPHONE = 25;
		public static int PID_BUSINESSPHONE => mPID_BUSINESSPHONE;

		private const int mPID_MARITALSTATUS = 10;
		public static int PID_MARITALSTATUS => mPID_MARITALSTATUS;

		private const int mPID_SSN = 20;
		public static int PID_SSN => mPID_SSN;

		// Insurance segment
		private const int mIN1_SEQUENCE = 1;
		public static int IN1_SEQUENCE => mIN1_SEQUENCE;

		private const int mIN1_COMPANYCODE = 50;
		public static int IN1_COMPANYCODE => mIN1_COMPANYCODE;

		private const int mIN1_COMPANYNAME = 50;
		public static int IN1_COMPANYNAME => mIN1_COMPANYNAME;

		// Insurance Address
		private const int mIN1_ADDRESS = 122;
		public static int IN1_ADDRESS => mIN1_ADDRESS;

		private const int mIN1_GROUPNUMBER = 50;
		public static int IN1_GROUPNUMBER => mIN1_GROUPNUMBER;

		private const int mIN1_RELATIONSHIP = 100;
		public static int IN1_RELATIONSHIP => mIN1_RELATIONSHIP;

		private const int mIN1_INSURED_DOB = 8;
		public static int IN1_INSURED_DOB => mIN1_INSURED_DOB;

		private const int mIN1_POLICYNUMBER = 50;
		public static int IN1_POLICYNUMBER => mIN1_POLICYNUMBER;

		private const int mIN1_INSURED_SEX = 10;
		public static int IN1_INSURED_SEX => mIN1_INSURED_SEX;

		// ORC   - Common order segment
	}
}
