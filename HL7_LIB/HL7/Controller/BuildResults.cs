using PTOX_LIB.HL7.Model;
using System;
using System.Collections.Generic;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	///
	/// {} = Repeating
	/// [] = optional
	/// <summary>
	///	MSH ---------------| --- Message Header
	///	PID ---------------| --- Patient Identification
	///	[PV1] -------------| --- Patient Visit
	///	ORC ---------------| --- Common Order - Only one per Result
	///	{                  | --- Order Group
	///		OBR-------------| ---- Detail
	///	   {               | ---- Detail Group
	///			{[OBX]}------| ------ Observation
	///		   {[NTE]}------| ------ Notes
	///	   }               |
	///	}                  |
	/// </summary>
	public class BuildResults : HL7Parser
	{
		public BuildResults()
		{
		}

		/// <summary>
		/// ParseORU - parse the List into an HL7_ORU object
		/// <param name="slMsg"></param>
		/// <returns>HL7_ORU</returns>
		/// </summary>
		public HL7_ORU ParseORU(List<string> slMsg)
		{
			HL7_ORU HL7ORU = new HL7_ORU();
			List<ErrorMsg> lErrorMsg = new List<ErrorMsg>();
			string sPrevSegment = GetEnumDescription(Segments.MSH);

			BuildHeader bldMsh = new BuildHeader();
			try
			{
				HL7ORU.HL7Header = new BuildHeader().GetHeader(slMsg);
				HL7ORU.HL7Patient = new BuildPatient().GetPatient(HL7ORU.HL7Header.HL7Encoding, slMsg, HL7ORU.HL7Header.MSHSegment.MessageType);
				HL7ORU.HL7Results = new BuildResults().GetResults(HL7ORU.HL7Header.HL7Encoding, slMsg);
			}
			catch (Exception ex)
			{
				lErrorMsg.Add(new ErrorMsg(1, slMsg.ToString() + " Exception: " + ex));
			}
			//do we have errors ?
			if (lErrorMsg.Count > 0)
			{
				foreach (ErrorMsg err in lErrorMsg)
				{
					Console.WriteLine(string.Format("Error {0}: {1}", err.Code, err.Message));
				}
			}
			if (HL7ORU.HL7Header.MSHSegment.Errors.Count > 0)
			{
				// display errors for the MSH segment
				foreach (var err in HL7ORU.HL7Header.MSHSegment.Errors)
				{
					Console.WriteLine("MSH segment processing errors");
					Console.WriteLine("   Error: " + err);
				}
			}
			if (HL7ORU.HL7Header.NTESegments != null)
			{
				if (HL7ORU.HL7Header.MSHSegment.Errors.Count > 0)
				{
					// display errors for the MSH segment
					foreach (var err in HL7ORU.HL7Header.NTESegments)
					{
						Console.WriteLine("NTE segment processing errors");
						Console.WriteLine("   Error: " + err);
					}
				}
			}
			return HL7ORU;
		}

		/// <summary>
		/// GetResults - get the HL7Result object based on the hl7 message
		/// </summary>
		/// <param name="_encode"></param>
		/// <param name="hl7Msg"></param>
		/// <returns></returns>
		public List<HL7Results> GetResults(HL7Encoding _encode, List<string> hl7Msg)
		{
			HL7Results result = null;
			HL7Details od = null;
			HL7Observation obs = null;
			List<HL7Results> hl7Results = new List<HL7Results>();
			BuildResults bldResult = new BuildResults();
			BuildNTE bldNTE = new BuildNTE();

			var errorMsg = new ErrorMsg();

			List<ErrorMsg> lErrorMsg = new List<ErrorMsg>();
			string line = string.Empty;

			bool bResult = false;         // result
			bool bResultDetails = false;  // result details
			bool bObservation = false;    // observation
			try
			{
				// search for the ORC segment 
				for (int nIdx = 0; nIdx < hl7Msg.Count; nIdx++)
				{
					line = hl7Msg[nIdx].ToString();

					string sTmp = GetField(_encode, line, 0);
					if (string.IsNullOrEmpty(sTmp))
					{
						lErrorMsg.Add(new ErrorMsg(1, line));
					}
					else
					{
						// Enum.TryParse<Segments>(sTmp, out sResult);
						switch (((Segments)Enum.Parse(typeof(Segments), sTmp)))
						{
							case Segments.ORC:   // start HL7 order
								if (result != null)
								{
									if (bResultDetails)
									{
										if (bObservation)
										{
											if (obs != null)
											{
												od.Observations.Add(obs);
												obs = null;
											}
											bObservation = false;
										}
										if (od != null)
										{
											result.HL7Details.Add(od);
											od = null;
										}
										bResultDetails = false;
									}
									// we are working on a current order and a new order was found
									// we need to add the existing order to the array
									// create new order and start again
									hl7Results.Add(result);
									result = null;
								}

								// lets start again
								result = new HL7Results
								{
									ORCSegment = new BuildORC().GetORC(_encode, line)   // build ORC object 
								};

								bResult = true; // working on new order
								bResultDetails = false;
								bObservation = false;
								break;

							case Segments.OBR:      // order details
								if (od != null)
								{
									// already building Order Detail object
									// new message found add existing order detial and start again
									result.HL7Details.Add(od);
									od = null;
								}
								od = new HL7Details("ORU")
								{
									OBRSegment = new BuildOBR().GetOBR(_encode, line)
								};

								bResultDetails = true;
								bObservation = false;
								break;

							// Observations consist of OBX, NTE
							case Segments.OBX:      // Observations
								if (obs != null)
								{
									od.Observations.Add(obs);
									obs = null;
								}
								obs = new HL7Observation("ORU")
								{
									OBXSegment = new BuildOBX().GetOBX(_encode, line, "ORU")
								};
								bObservation = true;
								// if ("ED".Equals(obs.OBXSegment.ValueType))
								// {
								// 	// this is the Embedded PDF segment so lets end this
								// 	bObservation = true;
								// }
								break;

							case Segments.NTE:
								// Note the order matters keep this order
								//
								if (bObservation) // NTE for Observations
								{
									obs.NTESegments.Add(bldNTE.GetNTE(_encode, line));    // build NTE for OBR group
								}
								else if (bResultDetails) // NTE for Order details
								{
									od.NTESegments.Add(bldNTE.GetNTE(_encode, line));     // build NTE for OBR group
								}
								else if (bResult)       // NTE for Order
								{
									result.NTESegments.Add(bldNTE.GetNTE(_encode, line));  // build NTE for ORC group
								}
								break;

							default:
								// nIdx = hl7Msg.Count; // we are done, leave
								break;
						}
					}
				}

				// we need to finish what we stated
				if (bResult)
				{
					if (result != null)
					{
						if (bResultDetails)
						{
							if (bObservation)
							{
								if (obs != null)
								{
									od.Observations.Add(obs);
									obs = null;
								}
								bObservation = false;
							}
							if (od != null)
							{
								result.HL7Details.Add(od);
								od = null;
							}
							bResultDetails = false;
						}
						// we are working on a current order and a new order was found
						// we need to add the existing order to the array
						// create new order and start again
						hl7Results.Add(result);
						result = null;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("BuildResults:GetResults: Exception {0}", ex);
				errorMsg.Message = sErr;
				Console.WriteLine(sErr);
			}
			return hl7Results;
		}
	}
}
