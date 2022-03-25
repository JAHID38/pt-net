using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pt_net.Manager.PtNet
{
    public class PtNetManager : IPtNetService
    {
        public int GenerateNumeric()
        {           
            Random _rdm = new Random();

            return _rdm.Next();
        }

        public string GenerateAlphanumeric()
        {

            String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            String beforSpace = "";
            String afterSpace = "";

            int length = 12;
            int minSpace = 1;
            int maxSpace = 10;

            String alphanumeric = "";


            Random ran = new Random();


            int spaceLength = ran.Next(minSpace, maxSpace);
            for (int i = 0; i < spaceLength; i++)
            {
                beforSpace += " ";
                afterSpace += " ";
            }

            alphanumeric = new string(Enumerable.Repeat(alphabet, length)
             .Select(s => s[ran.Next(s.Length)]).ToArray());

            return beforSpace + alphanumeric + afterSpace;
        }

        public double GenerateFloat()
        {
            int min = 1;
            int max = 1000000000;

            Random rand = new Random();

            double sample = rand.NextDouble();
            double floatNumber = (sample + rand.Next(min, max));

            return floatNumber;
        }
    }
}
