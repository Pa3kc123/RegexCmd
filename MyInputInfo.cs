using System;

namespace regexcmd
{
    class MyInputInfo
    {
        private string action = String.Empty;
        private string inputString = String.Empty;
        private string inputType = MyInputInfoType.Unknown;
        private string pattern = String.Empty;
        private string[] inputValues = null;
        private string[] parameters = null;

        public string Action { get { return this.action; } }
        public string InputString { get { return this.inputString; } }
        public string InputType { get { return this.inputType; } }
        public string Pattern { get { return this.pattern; } }
        public string[] InputValues { get { return this.inputValues; } }
        public string[] Parameters { get { return this.parameters; } }

        public MyInputInfo(params string[] _parameters)
        {
            this.action = 0 < _parameters.Length ? _parameters[0] : "NULL";
            this.pattern = 1 < _parameters.Length ? _parameters[1] : "NULL";
            this.inputString = 2 < _parameters.Length ? _parameters[2] : "NULL";

            this.parameters = 3 < _parameters.Length ? new string[_parameters.Length - 3] : null;
            if (this.parameters != null)
            {
                for (byte i = 3; i < this.parameters.Length + 3; i++)
                {
                    this.parameters[i-3] = _parameters[i];
                }
            }

            if (System.IO.File.Exists(this.inputString) == true)
            {
                this.inputType = MyInputInfoType.File;
                this.inputValues = new string[1] { this.inputString };
            }
            if (System.IO.Directory.Exists(this.inputString) == true)
            {
                this.inputType = MyInputInfoType.Directory;
                this.inputValues = System.IO.Directory.GetFiles(this.inputString);
            }
        }
        ~MyInputInfo()
        {
            this.action = null;
            this.inputString = null;
            this.inputValues = null;
            this.parameters = null;
            this.pattern = null;
        }
    }

    static class MyInputInfoType
    {
        public const string Unknown = "UNKNOWN";
        public const string File = "FILE";
        public const string Directory = "Directory"; 
    }
}