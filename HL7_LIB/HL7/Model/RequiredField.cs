using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOX_LIB.HL7.Model
{
	public class RequiredField
	{
		public RequiredField(string segment, string fldName, string type, double fldIdx = -1.0, int length = -1, bool required = false)
		{
			if (string.IsNullOrEmpty(segment) || string.IsNullOrEmpty(fldName) || string.IsNullOrEmpty(type))
			{
				// error condition throw exception
				//throw Exception
				return;
			}
			if (fldIdx == -1)
			{
				//throw exception
				return;
			}

			hl7Segment = segment;
			fieldName = fldName;
			fieldType = type;
			fieldIdx = fldIdx;
			fieldLength = length;
			fieldRequired = required;
		}

		private string hl7Segment;
		public string HL7Segment
		{
			get { return hl7Segment; }
		}

		private string fieldName;
		public string FieldName
		{
			get { return fieldName; }
		}

		private string fieldType;
		public string FieldType
		{
			get { return fieldType; }
		}

		private double fieldIdx;
		public double FieldIdx
		{
			get { return fieldIdx; }
		}

		private int fieldLength;
		public int FieldLength
		{
			get { return fieldLength; }
		}

		private bool fieldRequired;
		public bool IsRequired
		{
			get { return fieldRequired; }
		}
	}
}
