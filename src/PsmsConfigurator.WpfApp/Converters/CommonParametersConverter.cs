using PsmsConfigurator.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsmsConfigurator.WpfApp.Converters
{
    public static class CommonParametersConverter
    {
        public static CommonParameters ConvertToConfig(byte[] rawData)
        {
            CommonParameters commonParameters = new CommonParameters();
            if(rawData!=null)
            {
                // первый байт переводим в строку в двоичную систему счисления и берем биты, которые соответствуют общим параметрам
                string s = Convert.ToString(rawData[0], 2);
                int n = Convert.ToInt32(s);
                s = n.ToString("00000000");

                if (s[0] == '1')
                    commonParameters.LedOnNormalLight = true; 
                else
                    commonParameters.LedOnNormalLight = false;

                if (s[1] == '1')
                    commonParameters.FirePlumeVerification = true;
                else
                    commonParameters.FirePlumeVerification = false;

                if (s[3] == '1')
                    commonParameters.RFIUsed = true;
                else
                    commonParameters.RFIUsed = false;            
            
            }
            return commonParameters;
        }
        public static byte ConvertToByte(CommonParameters commonParameters)
        {
            string s = "00000000";
            if (commonParameters.LedOnNormalLight)
                s = s.Remove(0, 1).Insert(0, "1");
            if(commonParameters.FirePlumeVerification)
                s = s.Remove(1, 1).Insert(1, "1");
            if(commonParameters.RFIUsed)
                s = s.Remove(3, 1).Insert(3, "1");
            byte b = (byte)Convert.ToInt32(s);
            
            return b;
        }
    }
}
