using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StringBuilder
/// </summary>
public class StringGenerator
{
	public string CreateString(string str)
	{
        try
        {
            str = str.Replace("//", "/");
            string[] KeyCodeArray = str.Split('/');
            string RetString = string.Empty;
            for (int i = 0; i < KeyCodeArray.Length; i++)
            {
                if (KeyCodeArray[i] != string.Empty)
                {
                    ///ي = 1610  , ئ = 1574  , ی = 1740
                    if (KeyCodeArray[i] == "1610")
                        KeyCodeArray[i] = "1740";
                    ///ك = 1603 , ک = 1705
                    if (KeyCodeArray[i] == "1603")
                        KeyCodeArray[i] = "1705";
                    RetString += Convert.ToChar(Convert.ToInt32(KeyCodeArray[i]));
                }
            }
            return RetString;
        }
        catch
        {
            return string.Empty;
        }
	}
}