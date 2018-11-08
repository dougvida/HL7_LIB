using PTOX_LIB.HL7.Model;
using System;
using System.Collections.Generic;

namespace PTOX_LIB.HL7.Controller
{
	/// <summary>
	///
	///	BuildOrders - Get all the segments that can make up an order
	///	{} = Repeating
	///	[] = optional
	/// 
	///	MSH -----------------| --- Message header
	///		{NTE} ------------| ---- Message Notes
	///	PID -----------------| --- Patient
	///		{[NTE]} ----------| --- Patients Notes
	///	[PV1] ---------------| --- Patient Visit
	///	[GT1] ---------------| --- Guarantor
	///	{                    | ---- Insurance Group
	///		[IN1] ------------| ---- Insurance
	///		[IN2] ------------| ---- Insurance
	///		[IN3] ------------| ---- Insurance
	///	}                    |
	///	ORC -----------------| --- Order
	///	{                    | ---- Order Group
	///		{[NTE]} ----------| ---- Notes
	///		{                 |
	///			OBR -----------| ----- Detail
	///			{              | ------ Details Group
	///				{[NTE]} ----| ------- Notes
	///				{[DG1]} ----| ------- Diagnosis
	///				{           | -------- Diagnosis Group
	///					[OBX] ---| -------- Observation
	///					{[NTE]} -| -------- Notes
	///				}           |
	///			}              | 
	///		}                 |
	///	}                    |
	/// </summary>
	public class BuildOrders : HL7Parser
	{
		public BuildOrders()
		{
		}

		/// <summary>
		/// parseORM - parse the list into an HL7_ORM object
		/// <param name="slMsg">List<string>-List of HL7 Message</param>
		/// <returns>HL7_ORM</returns>
		/// </summary>
		public HL7_ORM ParseORM(List<string> slMsg)
		{

			HL7_ORM HL7ORM = new HL7_ORM();
			List<ErrorMsg> lErrorMsg = new List<ErrorMsg>();
			string sPrevSegment = GetEnumDescription(Segments.MSH);

			BuildHeader bldMsh = new BuildHeader();
			try
			{
				HL7ORM.HL7Header = new BuildHeader().GetHeader(slMsg);
				HL7ORM.HL7Patient = new BuildPatient().GetPatient(HL7ORM.HL7Header.HL7Encoding, slMsg, HL7ORM.HL7Header.MSHSegment.MessageType);
				HL7ORM.HL7Orders = new BuildOrders().GetOrders(HL7ORM.HL7Header.HL7Encoding, slMsg);
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
			if (HL7ORM.HL7Header.MSHSegment.Errors.Count > 0)
			{
				// display errors for the MSH segment
				foreach (var err in HL7ORM.HL7Header.MSHSegment.Errors)
				{
					Console.WriteLine("MSH segment processing errors");
					Console.WriteLine("   Error: " + err);
				}
			}
			if (HL7ORM.HL7Header.NTESegments != null)
			{
				if (HL7ORM.HL7Header.MSHSegment.Errors.Count > 0)
				{
					// display errors for the MSH segment
					foreach (var err in HL7ORM.HL7Header.NTESegments)
					{
						Console.WriteLine("NTE segment processing errors");
						Console.WriteLine("   Error: " + err);
					}
				}
			}
			return HL7ORM;
		}


		/// <summary>
		/// GetOrders - get all the segments that make up an order
		///   {
		///     ORC           - Required (Order)
		///     [
		///      OBR          - Required
		///      [{NTE}]      - Optional, if present can repeat
		///      [{DG1}]      - Optional, if present can repeat
		///      [{
		///        OBX        - Required	
		///        [{NTE}]    - Optional, if present can repeat
		///      }]
		///     ]
		///     [{CTI}]       - Optional, if present can repeat
		///     [BLG]         - Optional
		///  }
		///  
		/// </summary>
		/// <param name="hl7Msg">HL7 ORM message</param>
		/// <returns>List<HL7Orders> object</returns>
		public List<HL7Order> GetOrders(HL7Encoding _encode, List<string> hl7Msg)
		{
			HL7Order order = null;
			HL7Details od = null;
			HL7Observation obs = null;
			List<HL7Order> hl7Orders = new List<HL7Order>();
			BuildOrders bldOrders = new BuildOrders();
			BuildNTE bldNTE = new BuildNTE();

			ErrorMsg errorMsg = new ErrorMsg();

			List<ErrorMsg> lErrorMsg = new List<ErrorMsg>();
			string line = string.Empty;

			bool bOrder = false; // order
			bool bOrderDetails = false; // order details
			bool bObservation = false; // observation
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
								if (order != null)
								{
									if (bOrderDetails)
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
											order.HL7Details.Add(od);
											od = null;
										}
										bOrderDetails = false;
									}
									// we are working on a current order and a new order was found
									// we need to add the existing order to the array
									// create new order and start again
									hl7Orders.Add(order);
									order = null;
								}

								// lets start again
								order = new HL7Order
								{
									ORCSegment = new BuildORC().GetORC(_encode, line)   // build ORC object 
								};

								bOrder = true; // working on new order
								bOrderDetails = false;
								bObservation = false;
								break;

							case Segments.OBR:      // order details
								if (od != null)
								{
									// already building Order Detail object
									// new message found add existing order detial and start again
									order.HL7Details.Add(od);
									od = null;
								}
								od = new HL7Details("ORM")
								{
									OBRSegment = new BuildOBR().GetOBR(_encode, line)
								};

								bOrderDetails = true;
								bObservation = false;
								break;

							case Segments.DG1:      // order details
								od.DG1Segments.Add(new BuildDG1().GetDG1(_encode, line));
								bObservation = false;
								break;

							// Observations consist of OBX, NTE
							case Segments.OBX:      // Observations
								if (obs != null)
								{
									od.Observations.Add(obs);
									obs = null;
								}
								obs = new HL7Observation("ORM")
								{
									OBXSegment = new BuildOBX().GetOBX(_encode, line, "ORM")
								};
								bObservation = true;
								break;

							case Segments.NTE:
								// note the order of the if statements matter.
								// Observations than orderDetails than order
								if (bObservation) // NTE for Observations
								{
									obs.NTESegments.Add(bldNTE.GetNTE(_encode, line));    // build NTE for OBR group
								}
								else if (bOrderDetails) // NTE for Order details
								{
									od.NTESegments.Add(bldNTE.GetNTE(_encode, line));       // build NTE for OBR group
								}
								else if (bOrder)       // NTE for Order
								{
									order.NTESegments.Add(bldNTE.GetNTE(_encode, line));  // build NTE for ORC group
								}
								break;

							case Segments.BLG:
								order.BLGSegment = new BuildBLG().GetBLG(_encode, line);      // build BLG
								break;

							case Segments.CTI:
								order.CTISegments.Add(new BuildCTI().GetCTI(_encode, line));  // build CT1
								break;

							default:
								// nIdx = hl7Msg.Count; // we are done, leave
								break;
						}
					}
				}

				// we need to finish what we stated
				if (bOrder)
				{
					if (order != null)
					{
						if (bOrderDetails)
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
								order.HL7Details.Add(od);
								od = null;
							}
							bOrderDetails = false;
						}
						// we are working on a current order and a new order was found
						// we need to add the existing order to the array
						// create new order and start again
						hl7Orders.Add(order);
						order = null;
					}
				}
			}
			catch (Exception ex)
			{
				string sErr = string.Format("BuildOrders:GetOrders: Exception {0}", ex);
				errorMsg.Message = sErr;
				Console.WriteLine(sErr);
			}
			return hl7Orders;
		}
	}
}
