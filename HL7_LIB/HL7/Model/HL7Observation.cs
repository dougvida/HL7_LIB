using PTOX_LIB.HL7.Controller;
using PTOX_LIB.HL7.Model.Segments;
using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model
{
	/// <summary>
	/// HL7Observation - Get all the segments to build the Observation object.
	/// Inbound Order this can be AOE/POC mostly NTE segments
	///	[] = Optional
	///	{} = Repeats
	///	[OBX]
	///	{NTE}
	/// </summary>
	public class HL7Observation
	{
		public HL7Observation(string messageType)
		{
			OBXSegment = new OBX(messageType);
			NTESegments = new List<NTE>();
		}

		private OBX mOBX;
		public OBX OBXSegment
		{
			get { return mOBX; }
			set { mOBX = value; }
		}

		private List<NTE> mNTE;
		public List<NTE> NTESegments
		{
			get { return mNTE; }
			set { mNTE = value; }
		}
	}
}
