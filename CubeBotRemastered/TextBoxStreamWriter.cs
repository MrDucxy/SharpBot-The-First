using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubeBotRemastered
{
    public class TextBoxStreamWriter : TextWriter
    {
        Guna2TextBox _output = null;

        public TextBoxStreamWriter(Guna2TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.BeginInvoke(new Action(() =>
            {
                _output.AppendText(value.ToString());
            })
            ); // When character data is written, append it to the text box. 
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
