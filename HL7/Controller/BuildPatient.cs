using PTOX_LIB.HL7.Model;
using PTOX_LIB.HL7.Model.Segments;
using System;
using System.Collections.Generic;

namespace PTOX_LIB.HL7.Controller
{
    public class BuildPatient : HL7Parser
    {
        public BuildPatient()
        {
        }

        /// <summary>
        /// GetPatient - get all the segments that make up a patient
        /// 
        ///	PID ------------| --- Patient
        ///	[PD1] ----------| --- Optinal
        ///	[{NTE}] --------| --- Notes 
        /// [PV1] ----------| --- Patient Visit 
        ///	[PV2] ----------| --- Patient Visit
        ///	IN1 ------------| --- Insurance
        ///	[IN2] ----------| --- Insurance
        ///	[IN3] ----------| --- Insurance
        /// [GT1] ----------| --- Guanator
        /// [{AL1}] --------| --- ???
        /// 
        /// </summary>
        /// <param name="hl7Msg">HL7 ORM message</param>
        /// <returns>HL7Patient</returns>
        public HL7Patient GetPatient(HL7Encoding _encode, List<string> hl7Msg, string msgType)
        {
            HL7Patient pat = new HL7Patient(msgType);
            List<NTE> notes = new List<NTE>();

            string line = string.Empty;

            bool bPIDFound = false;
            try
            {
                // search for the PID segment 
                // any following NTE at the top level we need to keep with the MSH Header object
                for (int nIdx = 0; nIdx < hl7Msg.Count; nIdx++)
                {
                    line = hl7Msg[nIdx].ToString();

                    string sTmp = GetField(_encode, line, 0);
                    if (string.IsNullOrEmpty(sTmp))
                    {
                        pat.Errors.Add(string.Format("BuildPatient:GetPatient: Error Segment not found ({0})", line));
                    }
                    else
                    {
                        // Enum.TryParse<Segments>(sTmp, out sResult);
                        switch (((Segments)Enum.Parse(typeof(Segments), sTmp)))
                        {
                            case Segments.PID:   // we found the start
                                pat.PIDSegment = new BuildPID().GetPID(_encode, line, msgType);
                                bPIDFound = true;
                                break;

                            case Segments.NTE:  // can be more than one
                                if (bPIDFound)  // only keep NTE segments on this level
                                {
                                    pat.NTESegments.Add(new BuildNTE().GetNTE(_encode, line));
                                }
                                break;

                            case Segments.PV1:
                                pat.Visit1 = new BuildPV1().GetPV1(_encode, line);
                                break;
                            case Segments.PV2:
                                pat.Visit2 = new BuildPV2().GetPV2(_encode, line);
                                break;

                            case Segments.IN1:
                                pat.Insurance1 = new BuildIN1().GetIN1(_encode, line);
                                break;
                            case Segments.IN2:
                                pat.Insurance2 = new BuildIN1().GetIN1(_encode, line);
                                break;
                            case Segments.IN3:
                                pat.Insurance3 = new BuildIN1().GetIN1(_encode, line);
                                break;

                            case Segments.GT1:
                                pat.GT1Segment = new BuildGT1().GetGT1(_encode, line);
                                break;

                            case Segments.AL1:
                                pat.AL1Segments.Add(new BuildAL1().GetAL1(_encode, line));
                                break;

                            default:
                                if (bPIDFound)
                                {
                                    nIdx = hl7Msg.Count; // we are done, leave
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string sErr = string.Format("BuildPatient:GetPatient: Exception {0}", ex);
                pat.Errors.Add(sErr);
                Console.WriteLine(sErr);
            }
            return pat;
        }
    }
}
