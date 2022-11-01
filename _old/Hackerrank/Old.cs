using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank
{
    class Old {
        private static string BeforeAfter(string p1, string p2) {
            var Alph = "abcdefghijklmnopqrstuvwxyz";
            if (Alph.IndexOf(p1.ToLower()) < Alph.IndexOf(p2.ToLower())) {
                return p1;
            }

            return p2;
        }

        static int min(string p) {
            var g = "";
            var r = 0;
            foreach (var ch in p) {
                if (g == ch.ToString()) {
                    r++;
                } else {
                    g = ch.ToString();
                }
            }
            return r;
        }

        static string downloadFTP(string p) {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.byethost16.com/htdocs/" + p);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential("b16_15705594", "get5bigboywhileyoucan");

            FtpWebResponse response;
            try {
                response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                var d = reader.ReadToEnd();

                return d;

            } catch (WebException ex) {
                FtpWebResponse res = (FtpWebResponse)ex.Response;
                if (res.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable) {

                }
            }

            return "error";
        }

        const string DWORD_FOR_ACTIVE_SCRIPTING = "1400";
        const string VALUE_FOR_DISABLED = "3";
        const string VALUE_FOR_ENABLED = "0";

        public static void JavascriptEnable() {
            //get the registry key for Zone 3(Internet Zone)
            Microsoft.Win32.RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3", true);

            if (key != null) {
                key.SetValue(DWORD_FOR_ACTIVE_SCRIPTING, VALUE_FOR_ENABLED, RegistryValueKind.DWord);
                Object value = key.GetValue(DWORD_FOR_ACTIVE_SCRIPTING, VALUE_FOR_ENABLED);
                if (value.ToString().Equals(VALUE_FOR_DISABLED)) {
                    Console.WriteLine("Disabled"); return;
                }
            }
            Console.WriteLine("Enabled");
        }

        private static string[] Marty(string[] ft) {
            var correctPass = ft[0];
            int tries = int.Parse(ft[1]);
            string[] ret = new string[ft.Length - 2];

            for (int i = 2; i < ft.Length; i++) {
                int correct = 0, wrong = 0;
                foreach (var ch in correctPass) {
                    if (ft[i].IndexOf(ch) > -1) {
                        if (ft[i].IndexOf(ch) == correctPass.IndexOf(ch)) correct++; else wrong++;
                    }
                }

                ret[i - 2] = correct + " " + wrong;
            }


            return ret;
        }

        static HashSet<int> kar;
        static int collectMarble(string[] board) {
            kar = new HashSet<int>();
            return Track(0, 0, 0, board);
        }

        private static int Track(int i, int j, int count, string[] board, bool keepCharFunction = true) {
            if (i >= board.Length || j >= board[0].Length || i < 0 || j < 0) return count;
            var s = board[i][j].ToString();
            var c = 0;
            bool ch = false;
            switch (s) {
                case "#": return count;
                case "U": ch = true; if (!keepCharFunction) break; c = Track(i - 1, j, count, board); break;
                case "L": ch = true; if (!keepCharFunction) break; c = Track(i, j - 1, count, board); break;
                default: break;
            }

            var si = ch ? 0 : int.Parse(s);
            int a = count + si, b = count + si;

            if (j + 1 < board[0].Length) {
                if (board[i][j + 1].ToString() != "L") b = Track(i, j + 1, count + si, board); else b = Track(i, j + 1, count + si, board, false);
            }

            if (i + 1 < board.Length) {
                if (board[i + 1][j].ToString() != "U") a = Track(i + 1, j, count + si, board); else a = Track(i + 1, j, count + si, board, false);
            }

            return Math.Max(Math.Max(a, b), c);
        }
    }
}
