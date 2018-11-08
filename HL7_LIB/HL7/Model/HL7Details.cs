using PTOX_LIB.HL7.Model.Segments;
using System;
using System.Collections.Generic;

namespace PTOX_LIB.HL7.Model
{
	/// <summary>
	/// HL7Details - get the segments to build the Details object
	/// 
	///	[] = Optional
	///	{} = Repeats
	/// 
	///	[OBR] ----------| --- Details
	///	{               | --- Details Group
	///		{NTE} -------| --- Notes
	///		{[DG1]} -----| --- Diagnosis
	///		[
	///			{OBX} ----| --- test  
	///			{NTE} ----| --- Notes
	///		]
	///	}
	/// </summary>
	public class HL7Details
	{
		public HL7Details(string msgType)
		{
			OBRSegment = new OBR();
			DG1Segments = new List<DG1>();
			Observations = new List<HL7Observation>(); // if ORM will only contain NTE.  If ORU will contain OBX and maybe NTE
			NTESegments = new List<NTE>();
		}

		private OBR mOBR;
		public OBR OBRSegment
		{
			get { return mOBR; }
			set { mOBR = value; }
		}

		private List<NTE> mNTE;
		public List<NTE> NTESegments
		{
			get { return mNTE; }
			set { mNTE = value; }
		}

		private List<DG1> mDG1;
		public List<DG1> DG1Segments
		{
			get { return mDG1; }
			set { mDG1 = value; }
		}

		private List<HL7Observation> mObservations;
		public List<HL7Observation> Observations
		{
			get { return mObservations; }
			set { mObservations = value; }
		}

		public static implicit operator HL7Details(HL7Results v)
		{
			throw new NotImplementedException();
		}
	}
}
