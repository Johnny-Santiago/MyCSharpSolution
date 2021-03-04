using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Extensions
{
    public static class StringFormatterExtensions
    {
        private enum MessageType { Success, Error, Notice }

        public static string ToSuccessMessageFormat(this string message)
        {
            return GetFormattedMessage(message, MessageType.Success);
        }

        public static string ToErrorMessageFormat(this string message)
        {
            return GetFormattedMessage(message, MessageType.Error);
        }

        public static string ToNoticeMessageFormat(this string message)
        {
            return GetFormattedMessage(message, MessageType.Notice);
        }

        private static string GetFormattedMessage(string message, MessageType messageType = MessageType.Notice)
        {
            switch (messageType)
            {
                case MessageType.Success: return "<div class='success'>" + message + "</div>";
                case MessageType.Error: return "<div class='error'>" + message + "</div>";
                default: return "<div class='notice'>" + message + "</div>";
            }
        }

        public static string ToEfakturFormatted(this string etaxnumber)
        {

            etaxnumber = string.Format("{0}-{1}-{2}", etaxnumber.Substring(0, 3), 
                etaxnumber.Substring(3, 2), 
                etaxnumber.Substring(5, 8));

            return etaxnumber;
        }
    }
}