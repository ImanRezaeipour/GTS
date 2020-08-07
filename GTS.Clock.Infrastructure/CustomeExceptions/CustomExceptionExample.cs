using System;
using System.Text;
using System.Runtime.Serialization;

namespace MyProject.Exceptions
{

	[Serializable]
	public class InvalidUrlException : Exception
	{
	
		#region Member Variables
		const string _className = "InvalidUrlException";
		const int _hResult = -2146232832; // COR_E_APPLICATION
		string _url;
		#endregion	
		
		#region Constructors
		public InvalidUrlException() : base()
		{
			base.HResult = _hResult;
		}
			
		public InvalidUrlException(string message) : base(message)
		{
			base.HResult = _hResult;
		}
			
		public InvalidUrlException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = _hResult;
		}
			
		public InvalidUrlException(string message, string url) : base(message)
		{
			base.HResult = _hResult;
			_url = url;
		}

		public InvalidUrlException(string message, string url, Exception innerException) : base(message, innerException)
		{
			base.HResult = _hResult;
			_url = url;
		}

		protected InvalidUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._url = info.GetString("InvalidUrlException_Url");
			base.HResult = _hResult;
		}
		
		#endregion		
			
		#region Properties & Operators
		public static implicit operator string(InvalidUrlException ex)
		{
			return ex.ToString();
		}
		#endregion
			
		#region Methods
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0}: {1}", _className, this.Message);
			sb.AppendFormat(" The specified url '{0}' is invalid!", _url);
			
			if (this.InnerException != null)
			{
				sb.AppendFormat(" ---> {0} <---", base.InnerException.ToString());
			}
		
			if (this.StackTrace != null)
			{
				sb.Append(Environment.NewLine);
				sb.Append(base.StackTrace);
			}
	
			return sb.ToString();
		}
	
		public override void GetObjectData(SerializationInfo info, StreamingContext context) 
		{ 
			base.GetObjectData (info, context); 
			info.AddValue("InvalidUrlException_Url", this._url, typeof(string));
		} 
	
		#endregion
			
	}
}
