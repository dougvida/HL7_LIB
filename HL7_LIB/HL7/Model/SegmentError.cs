using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTOX_LIB.HL7.Model
{
	public class SegmentError
	{
		private SegmentError()
		{
		}

		public SegmentError(string seg, string fldName, string sErr, string comment = "")
		{
			segment = seg;
			fieldName = fldName;
			error = sErr;
			note = comment;
		}

		private string error;
		public string Error
		{
			get { return error; }
			//set { error = value; }
		}

		private string segment;
		public string Segment
		{
			get { return segment; }
			//set { segment = value; }
		}

		private string fieldName;
		public string FieldName
		{
			get { return fieldName; }
			set { fieldName = value; }
		}

		private string note;
		public string Note
		{
			get { return note; }
			//set { note = value; }
		}
	}
}
