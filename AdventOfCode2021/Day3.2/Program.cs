using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int oxOnes = 0;
            int oxZeros = 0;
            string oxMost;
            int oxSame;

            int coOnes = 0;
            int coZeros = 0;
            string coMost;
            int coSame;

            List<string> oxList = input.ToList();
            List<string> coList = input.ToList();

            for (int i = 0; i < input[0].Length; i++)
            {
                foreach (string line in oxList)
                    if (line.Substring(i, 1) == "1")
                        oxOnes++;
                    else
                        oxZeros++;

                oxMost = oxOnes > oxZeros ? "1" : "0";
                oxSame = oxOnes == oxZeros ? 1 : 0;

                foreach (string line in coList)
                    if (line.Substring(i, 1) == "1")
                        coOnes++;
                    else
                        coZeros++;

                coMost = coOnes > coZeros ? "1" : "0";
                coSame = coOnes == coZeros ? 1 : 0;

                if (oxList.Count != 1)
                    getOxList(ref oxList, oxSame, i, oxMost);

                if (coList.Count != 1)
                    getCoList(ref coList, coSame, i, coMost);

                oxOnes = 0;
                oxZeros = 0;
                coOnes = 0;
                coZeros = 0;
            }
        }

        static void getOxList(ref List<string> newList, int same, int i, string most)
        {
            for (int k = 0; k < newList.Count; k++)
                if (newList[k].Substring(i, 1) != most && same == 0)
                {
                    newList.RemoveAt(k);
                    k--;
                }

                else if (same == 1)
                    if (newList[k].Substring(i, 1) == "0")
                    {
                        newList.RemoveAt(k);
                        k--;
                    }
        }

        static void getCoList(ref List<string> newList, int same, int i, string most)
        {
            for (int k = 0; k < newList.Count; k++)
                if (newList[k].Substring(i, 1) == most && same == 0)
                {
                    newList.RemoveAt(k);
                    k--;
                }
                else if (same == 1)
                    if (newList[k].Substring(i, 1) == "1")
                    {
                        newList.RemoveAt(k);
                        k--;
                    }
        }
    }
}
