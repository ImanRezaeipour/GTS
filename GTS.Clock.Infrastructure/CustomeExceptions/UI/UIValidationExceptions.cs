using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{    
    public class UIValidationExceptions : Exception
    {
        /// <summary>
        /// آیا در لاگ آمده است
        /// </summary>
        public bool InsertedLog { get; set; }
       
        public IList<ValidationException> ExceptionList
        {
            get;
            set;
        }

        public string GetLogMessage()
        {
            StringBuilder msg = new StringBuilder();

            foreach (UIBaseException ex in ExceptionList)
            {
                msg.AppendLine(ex.GetLogMessage());
            }
            return msg.ToString();
        }

        public UIValidationExceptions() 
        {
            ExceptionList = new List<ValidationException>();
        }

        public void Add(ValidationException exception)
        {
            ExceptionList.Add(exception);
        }

        public void Add(ExceptionResourceKeys key, string msg, string execptionSrc)
        {
            ValidationException ex = new ValidationException(key, msg, execptionSrc);
            ExceptionList.Add(ex);
        }

        public int Count
        {
            get { return ExceptionList.Count; }
        }

        public bool Exists(ExceptionResourceKeys key) 
        {
           return  ExceptionList.Where(x => x.ResourceKey == key).Count() > 0;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (ValidationException ex in this.ExceptionList) 
            {
                builder.AppendLine(ex.Message + "  " + ex.ResourceKey.ToString("G"));
            }
            return builder.ToString();

        }

        public override string Message
        {
            get
            {
                return this.ToString();
            }
        }

        public override string Source
        {
            get
            {
                if (ExceptionList.Count > 0)
                {
                    string source = "";
                    StringBuilder builder = new StringBuilder();
                    foreach (ValidationException v in ExceptionList.OrderBy(x => x.ExceptionSource).ToList())
                    {
                        if (builder.Length == 0)
                        {
                            builder.AppendLine(v.ExceptionSource);
                            source = v.ExceptionSource;
                        }
                        else if (!v.ExceptionSource.Equals(source))
                        {
                            builder.AppendLine(v.ExceptionSource);
                            source = v.ExceptionSource;
                        }
                    }
                    return builder.ToString();
                }
                else
                {
                    return base.Source;
                }
            }            
        }


    }
}
