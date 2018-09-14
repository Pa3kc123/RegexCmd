using System;
using System.Text.RegularExpressions;
using System.IO;

using static regexcmd.MyRegex;
using static System.Environment;

namespace regexcmd
{
    class Program
    {
        static MyInputInfo myInputInfo;
        
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Write("Usage: Regex.exe <type of action> <pattern> <input directory> <parameters>" + NewLine);
                Exit(ExitCode);
            }

            myInputInfo = new MyInputInfo(args);
            
            Console.Write("--------------------------------------------------" + NewLine);
            Console.Write("Action: " + myInputInfo.Action + NewLine);
            Console.Write("Pattern: \"" + myInputInfo.Pattern + "\"" + NewLine);
            Console.Write("Input: \"" + myInputInfo.InputString + "\" (" + (myInputInfo.InputType == MyInputInfoType.File ? "File" : "Directory") + ")" + NewLine);

            if (myInputInfo.Parameters == null)
                Console.Write("No parameters added" + NewLine);
            else
                for (Byte i = 0; i < myInputInfo.Parameters.Length; i++)
                    Console.Write("Parameter[" + (i + 1) + "]: \"" + myInputInfo.Parameters[i] + "\"" + NewLine);

            switch (myInputInfo.Action)
            {
                case "replace":
                case "rename":
                case "move":
                case "mv":
                    RegexMove(myInputInfo);
                break;

                case "copy":
                case "scp":
                    RegexCopy(myInputInfo);
                break;

                case "ls":
                case "dir":
                case "find":
                    RegexFind(myInputInfo);
                break;

                case "delete":
                case "del":
                case "rm":
                case "remove":
                    RegexDelete(myInputInfo);
                break;
            }
            
            Console.Write("--------------------------------------------------" + NewLine);
        }
    }
}