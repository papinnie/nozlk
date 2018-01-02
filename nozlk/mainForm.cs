using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace nozlk
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        Encoding enc = Encoding.UTF8;
        

        /// <summary>
        /// URI Schemeのシェルハンドラを登録する
        /// </summary>
        /// <param name="proto">URI Scheme</param>
        /// <param name="path_exe">アプリケーションのパス</param>
        /// <param name="path_icon">アイコンのパス</param>
        public static void RegisterUriSchemeShellHandler(string scheme, string proto, string path_exe = null, string path_icon = null)
        {
            path_exe = path_exe ?? System.Reflection.Assembly.GetExecutingAssembly().Location;
            path_icon = path_icon ?? path_exe;

            var key_proto = Microsoft.Win32.Registry.CurrentUser
                            .CreateSubKey("Software").CreateSubKey("Classes").CreateSubKey(scheme);

            // Protocol Handler
            key_proto.SetValue("", string.Format("URL:{0} Protocol", proto));
            key_proto.SetValue("URL Protocol", "");

            // Icon
            var key_deficon = key_proto.CreateSubKey("DefaultIcon");
            key_deficon.SetValue("", string.Format(@"""{0}""", path_icon));

            // Executable Path
            var key_command = key_proto.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");
            key_command.SetValue("",
                string.Format(@"""{0}"" ""%1"" ""%2"" ""%3"" ""%4"" ""%5"" ""%6"" ""%7"" ""%8"" ""%9""", path_exe)
                );
            
        }

        private void ButtonMakeLink_Click(object sender, EventArgs e)
        {
            //
            if (textBoxOperand.Text != "")
            {
                string cmdName = "";
                string comment = "_";
                string operand = textBoxOperand.Text;
                string base64operand;

                //


                if(Directory.Exists(operand)){
                    //ディレクトリなら自動的にエクスプローラ
                    cmdName = "Explorer";

                }else if(File.Exists(operand))
                {
                    
                    //ファイル拡張子からコマンド自動生成
                    switch (Path.GetExtension(operand))
                    {
                        case ".pptx":
                            cmdName = "PowerPoint";
                            break;

                        case ".xlsx":
                            cmdName = "Excel";
                            break;

                        case ".sln":
                            cmdName = "vcs2017";
                            break;

                        default:
                            cmdName = "Unkwon";
                            break;
                    }
                }
                else if (operand.StartsWith("evernote",true,null) && operand.Length>10)
                {
                    cmdName = "Evernote";
                    operand = operand.Substring(9);
                }
                else
                {
                    //Evernoteとか   
                }

                //コメント反映
                if (textBoxComment.Text != "") comment = textBoxComment.Text;

                base64operand = Convert.ToBase64String(enc.GetBytes(operand));
                base64operand = base64operand.Replace("/", "$");
                for (int i = 10; i < base64operand.Length; i += 10) base64operand = base64operand.Insert(i, "-");


                string uri = string.Format("nozlk://{0}/{1}/{2}",cmdName, comment, base64operand);

                linkLabel1.Text = uri;
                Clipboard.SetDataObject(uri, true);
            }
        }

        class DataFormatsEvernote
        {
            public const string Notes = "EN Notes";
            public const string Notebooks = "EN Notebooks";
        }

        private void fileDragEnter(object sender, DragEventArgs e)
        {

            //DataFormats.
            /*
            MessageBox.Show(
                       DataFormats.FileDrop + "$$" + string.Join(",", e.Data.GetFormats()),//e.Data.GetType().ToString(),
                                    "Nozlk",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            */

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
                Console.WriteLine("move");
            }
            else
            if (e.Data.GetDataPresent(DataFormatsEvernote.Notes) || e.Data.GetDataPresent(DataFormatsEvernote.Notebooks))
            {
                e.Effect = DragDropEffects.Copy;
                Console.WriteLine("link");
            }
            else
            {
                e.Effect = DragDropEffects.None;
                Console.WriteLine("none");
            }
        }
        
        private void fileDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                textBoxOperand.Text = files[0];
                textBoxComment.Text = Path.GetFileName(files[0]);

                ButtonMakeLink_Click(sender, e);//リンク生成呼ぶ

            }
            else if (e.Data.GetDataPresent(DataFormatsEvernote.Notes))
            {
                Console.WriteLine("Notes");
                //
                string[] files = (string[])e.Data.GetData(DataFormatsEvernote.Notes);
                textBoxOperand.Text = files[0];
            }
            else if (e.Data.GetDataPresent(DataFormatsEvernote.Notebooks))
            {
                Console.WriteLine("Notebooks");
                //
                string[] files = (string[])e.Data.GetData(DataFormatsEvernote.Notebooks);
                textBoxOperand.Text = files[0];
            }
            else
            {
                //ファイル以外だったら
                MessageBox.Show(
                       e.Data.GetType().ToString(),
                                    "Nozlk",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { 

                //左クリックなら単純にリンク処理（nozlk前提）
                try
                {
                    System.Diagnostics.Process.Start(linkLabel1.Text);
                }
                catch
                {
                    //
                }
   
            }

            if (e.Button == MouseButtons.Right)
            {
                //右クリックならURLをクリップボードへコピー
                Clipboard.SetDataObject(linkLabel1.Text, true);
            }
        }
    }
}
