using System;

using System.IO;
using System.Text.RegularExpressions;

using static System.Environment;
using static System.Text.RegularExpressions.Regex;
using static System.Text.RegularExpressions.RegexOptions;

namespace regexcmd
{
    static class MyRegex
    {
        private static byte i = 1;
        public static readonly string currentDirectory = Directory.GetCurrentDirectory() + '\\';

        public static void RegexMove(MyInputInfo myInputInfo)
        {
            foreach (string file in myInputInfo.InputValues)
            {
                string filePath = Path.GetDirectoryName(file) + '\\';
                string fileName = Path.GetFileName(file);
                GroupCollection groups = Regex.Match(fileName, myInputInfo.Pattern, Multiline).Groups;

                if (groups.Count <= 1) continue;
                if (myInputInfo.Parameters.Length < (groups.Count - 1))
                {
                    ushort tmp = (ushort)((groups.Count - 1) - myInputInfo.Parameters.Length);
                    Console.Write("Need " + tmp + " more parameter" + (tmp < 1 ? "s" : String.Empty) + NewLine);
                    return;
                }
                
                string result = String.Empty;
                short previousIndex = 0;

                for (byte group = 1; group < groups.Count; group++)
                {
                    result = result + fileName.Substring((result.Length + previousIndex), (groups[group].Index - result.Length - previousIndex));
                    result = result + myInputInfo.Parameters[group - 1];
                    previousIndex = ((short)(groups[group].Length - myInputInfo.Parameters[group - 1].Length));
                }
                previousIndex = (short)(groups[groups.Count - 1].Index + groups[groups.Count - 1].Length);
                result = result + fileName.Substring(previousIndex, (fileName.Length - previousIndex));
                Console.Write("Output[" + i++ + "]: \"" + result + "\"" + NewLine);
                result = filePath + result;
                File.Move(file, result);
            }
        }

        public static void RegexCopy(MyInputInfo myInputInfo)
        {
            foreach (string file in myInputInfo.InputValues)
            {
                string filePath = Path.GetDirectoryName(file) + '\\';
                string fileName = Path.GetFileName(file);
                GroupCollection groups = Regex.Match(fileName, myInputInfo.Pattern, Multiline).Groups;

                if (groups.Count <= 1) continue;
                if (myInputInfo.Parameters.Length < (groups.Count - 1))
                {
                    ushort tmp = (ushort)((groups.Count - 1) - myInputInfo.Parameters.Length);
                    Console.Write("Need " + tmp + " more parameter" + (tmp < 1 ? "s" : String.Empty) + NewLine);
                    return;
                }
                
                string result = String.Empty;
                short previousIndex = 0;

                for (byte group = 1; group < groups.Count; group++)
                {
                    result = result + fileName.Substring((result.Length + previousIndex), (groups[group].Index - result.Length - previousIndex));
                    result = result + myInputInfo.Parameters[group - 1];
                    previousIndex = (short)(groups[group].Length - myInputInfo.Parameters[group - 1].Length);
                }
                previousIndex = (short)(groups[groups.Count - 1].Index + groups[groups.Count - 1].Length);
                result = result + fileName.Substring(previousIndex, (fileName.Length - previousIndex));
                Console.Write("Output[" + i++ + "]: \"" + result + "\"" + NewLine);
                result = filePath + result;
                File.Copy(file, result);
            }
        }

        public static void RegexFind(MyInputInfo myInputInfo)
        {
            foreach (string file in myInputInfo.InputValues)
            {
                if (IsMatch(file, myInputInfo.Pattern, Multiline) == true) Console.Write("Output[" + i++ + "]: " + Path.GetFileName(file) + NewLine);
            }
        }

        public static void RegexDelete(MyInputInfo myInputInfo)
        {
            bool autodelete = false;
            foreach (string file in myInputInfo.InputValues)
            {
                if (IsMatch(file, myInputInfo.Pattern, Multiline) == false) continue;

                Console.Write("Output[" + i++ + "]: " + Path.GetFileName(file) + " OK [Y/n]: ");
                if (autodelete == true) { Console.Write("YA" + NewLine); File.Delete(file); continue; }
                string answer = Console.ReadLine();

                if (answer == "n" || answer == "N") continue;
                if (answer == "na" || answer == "nA" || answer == "Na" || answer == "NA") return;
                if (answer == "y" || answer == "Y" || answer == "") File.Delete(file);
                if (answer == "ya" || answer == "yA" || answer == "Ya" || answer == "YA") autodelete = true;

                /*switch (Console.ReadLine())
                {
                    case "n":
                    case "N": continue;

                    case "na":
                    case "nA":
                    case "Na":
                    case "NA": return;

                    default: File.Delete(file); break;
                }*/
            }
        }
    }
}