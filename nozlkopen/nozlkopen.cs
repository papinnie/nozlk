using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace nozlkopen
{
    class nozlkopen
    {
        /// <summary>
        /// \\HKEY_CLASSES_ROOT\nozlk
        /// に登録することで
        /// nozlk://VS/1yIHLQJNvDw
        /// のように呼ばれることを期待している。
        /// 
        /// そのように呼ばれたら、各種アプリなどを起動する
        /// </summary>
        static void Main(string[] args)
        {
            Encoding enc = Encoding.UTF8;

            if (args.Length == 1 && (args[0] == "/help" || args[0] == "/?")) {
                //コマンドプロンプトから呼ばれようと標準出力には出せないので
                //メッセージウィンドウ出す
                Console.WriteLine("Called help");
                MessageBox.Show(
                    "これは使い方です。",
                    "Nozlk Usage",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (args.Length > 0)
            {
                string[] strs = args[0].Split('/');

                // nozlk://ソフト名(英字3字以上)/コメント/base64
                // 0      12                     3        4

                if (strs.Length == 5)
                {

                    string comment = strs[3];//uri-encoded
                    string cmd = strs[2];
                    string base64string = strs[4];//base64encoded

                    //base64のワードラップ対策ハイフン除去＆でコード
                    base64string = base64string.Replace("-", "");
                    base64string = base64string.Replace("$", "/");
                    string operand = enc.GetString(Convert.FromBase64String(base64string));




                    //オペランドのパスの存在チェック（ツールの場合のみ？形式チェックで？）
                    //自宅、会社の環境チェック文字列入れる？

                    switch (cmd.ToLower())
                    {
                        case "powerpoint":
                        case "excel":
                        case "vcs2017":
                        case "folder":
                        case "explorer":
                            /*
                            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                            {
                                //Shift押しだとファイルの場所を開く（フォルダ開く）
                                System.Diagnostics.Process.Start(Path.GetDirectoryName(operand));
                            }
                            else
                            */
                            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                //Ctrl押しながら起動では表示のみ
                                MessageBox.Show(
                                    cmd + " で " + operand,
                                    "Nozlk",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                            {
                                //Shift押しだとファイルの場所を開く（フォルダ開く）
                                System.Diagnostics.Process.Start(Path.GetDirectoryName(operand));
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(operand);
                            }

                            break;

                            
                        case "evernote":
                            //string evernotescript1 = @"""C:\\Program Files (x86)\\Evernote\\Evernote\\enscript.exe";
                            string evernotescript1 = @"""C:\Program Files (x86)\Evernote\Evernote\enscript.exe""";
                            string evernotescript2 = "showNotes /q \"{0}\"";
                            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                //Ctrl押しながら起動では表示のみ
                                MessageBox.Show(
                                    evernotescript1 + " " +
                                    string.Format(evernotescript2, operand.Replace("\"", "\\\"")),
                                    "Nozlk",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(evernotescript1, string.Format(evernotescript2, operand.Replace("\"","\\\"")));
                                }
                                catch
                                {
                                    MessageBox.Show(
                                    "Evernote起動に失敗しました。",
                                    "Nozlk",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                }
                            }

                            break;

                        default:

                            MessageBox.Show(
                                cmd + " で " + operand,
                                "Nozlk",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            break;
                    }

                }
                else {
                    //不正なデータ量
                    MessageBox.Show(
                        "URLが間違っています。"+ strs.Length.ToString(),
                        "Nozlk",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                
            }
            

            //a
            Console.WriteLine("terminated exe");
            /*
            MessageBox.Show(
                "Opened Changed",
                "Nozlk",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                */
        }
    }
}
