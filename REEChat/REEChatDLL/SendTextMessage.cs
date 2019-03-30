using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
    public class SendTextMessage : Package
    {
        public string EMail { get; set; }
        public string Text { get; set; }

        public SendTextMessage(string eMail, string text)
        {
            Type = PackageType.TextMessageSend;
            EMail = eMail;
            Text = text;
        }

        public override byte[] UserData()
        {
            byte[] emailByte = Encoding.UTF8.GetBytes(EMail);
            byte[] textByte = Encoding.UTF8.GetBytes(Text);
            byte[] userDataByte = new byte[1 + emailByte.Length + textByte.Length];

            int i = 0;

            Array.Copy(emailByte, 0, userDataByte, i, emailByte.Length);
            i += emailByte.Length;

            userDataByte[i] = PackageControl.UnitSeperator;
            i++;

            Array.Copy(textByte, 0, userDataByte, i, textByte.Length);
            return userDataByte;
        }

        public static bool TryParse(byte[] data, out SendTextMessage sendTextMessage)
        {
            sendTextMessage = null;

			if (!TrySplitByte(data, PackageControl.UnitSeperator, false, out byte[] emailByte, out byte[] textByte))
				return false;

			if (emailByte.Length == 0 || textByte.Length == 0)
                return false;

            string email = Encoding.UTF8.GetString(emailByte);
            string text = Encoding.UTF8.GetString(textByte);

            sendTextMessage = new SendTextMessage(email, text);

            return true;
        }
    }
}
