using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryBuilder
{
public class Utils {
	public static string DATE_FORMAT_DB = "yyyy-MM-dd";
	public static string TIME_FORMAT_DB = "HH:mm";
	public static string DATETIME_FORMAT_DB = "yyyy-MM-dd HH:mm:ss";

	public static string dateToString(DateTime date) {
		if (date == null)
			return null;

		try {
			return date.ToString(DATE_FORMAT_DB);
		} catch (Exception) {
			return null;
		}
	}

	public static string dateTimeToString(DateTime date) {
		if (date == null)
			return null;

		try {
			return date.ToString(DATETIME_FORMAT_DB);
		} catch (Exception) {
			return null;
		}
	}

	public static string toString(object value) {
		if (value == null)
			return null;

		if (value is float)
			return new decimal((float) value).ToString();//.stripTrailingZeros().toPlainString();
		else if (value is double)
            return new decimal((double)value).ToString();//.stripTrailingZeros().toPlainString();
		else
			return value.ToString();
	}
	
	
}
}