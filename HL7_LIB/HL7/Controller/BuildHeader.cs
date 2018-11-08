using PTOX_LIB.HL7.Model;
using PTOX_LIB.HL7.Model.Segments;
using System;
using System.Collections.Generic;


namespace PTOX_LIB.HL7.Controller
{
    /// <summary>
    /// BuildHeader
    ///     Build the HL7 message header object
    ///     Required MSH object
    ///     Optional repeating NTE object(s)
    /// </summary>
    public class BuildHeader : HL7Parser
	{
		public BuildHeader()
		{
        }

        /// <summary>
        /// GetHeader
        ///     Build the hl7 message header (MSH)
        ///     Each HL7 object contains a list of errors.
        ///     Verify no errors have been detected during processing
        /// </summary>
        /// <param name="hl7Msg">hl7 message</param>
        /// <returns>HL7Header object</returns>
		public HL7Header GetHeader(List<string> hl7Msg)
		{
            HL7Encoding hl7Encoding = null;
			MSH msh = null;
            NTE nte = null;
            bool bMSHFound = false;
            List<NTE> notes = new List<NTE>();
			List<ErrorMsg> lErrorMsg = new List<ErrorMsg>();
			string line = string.Empty;
            try
            {
                // search for the MSH segment should be the first one
                // any following NTE at the top level we need to keep with the MSH Header object
                for (int nIdx = 0; nIdx < hl7Msg.Count; nIdx++)
                {
                    line = hl7Msg[nIdx].ToString();   // index 0 must be the MSH segment
                    if (line.Substring(0,3).Equals("MSH"))
                    {
                        // ok we have a Message Header line
                        // now lets get the decoding informaiton
                        hl7Encoding = new HL7Encoding((line.Substring(3, 5)).ToCharArray());
                    }
                    else
                    {
                        continue;   // not found get next line
                    }

                    string sTmp = GetField(hl7Encoding, line, 0);    // should be the message segment
                    if (string.IsNullOrEmpty(sTmp))
                    {
                        lErrorMsg.Add(new ErrorMsg(1, line));
                    }
                    else
                    {
                        // Enum.TryParse<Segments>(sTmp, out sResult);
                        switch (((Segments)Enum.Parse(typeof(Segments), sTmp)))
                        {
                            case Segments.MSH:
                                msh = new BuildMSH().GetMSH(hl7Encoding, line);
                                bMSHFound = true;
                                break;

                            case Segments.NTE:   // can be more than one
                                if (bMSHFound) // only keep NTE segments on this level
                                {
                                    nte = new BuildNTE().GetNTE(hl7Encoding, line);
                                    notes.Add(nte);
                                }
                                break;

                            default:
                                nIdx = hl7Msg.Count; // we are done, leave
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string sErr = string.Format("GetHeader:GetHeader: Exception {0}", ex);
                msh.Errors.Add(sErr);
                Console.WriteLine(sErr);
            }
            return new HL7Header(msh, notes, hl7Encoding);
		}
     }
}
