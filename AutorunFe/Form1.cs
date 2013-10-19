using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace AutorunFe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            {
                this.listView1 = new ListView();
                this.listView2 = new ListView();
                this.listView3 = new ListView();
                this.listView4 = new ListView();
                this.listView5 = new ListView();
                this.listView6 = new ListView();
                this.listView7 = new ListView();
                this.listView8 = new ListView();
                this.listView9 = new ListView();
                this.listView10 = new ListView();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_Layout(object sender, LayoutEventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Groups.Clear();
            listView1.Columns.Add("Auto Entry",150);
            listView1.Columns.Add("Description",150);
            listView1.Columns.Add("Publisher",150);
            listView1.Columns.Add("Image Path",320);
            //listView1.Columns.Add("Timestamp",150);
            //listView1.Items.Add("abc");
            string entryName, filePath, fileName;
            int colonPos, exePos, dllPos, tailPos;
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey logon1 = hklm.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            ListViewGroup listGroup1 = new ListViewGroup("HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            listView1.Groups.Add(listGroup1);
            for (int i = 0; i < logon1.GetValueNames().Length; i++)
            {
                entryName = logon1.GetValueNames().ElementAt(i);
                filePath = logon1.GetValue(entryName).ToString();
                if (filePath == "") continue;
                colonPos = filePath.IndexOf(":");
                exePos = filePath.ToLower().IndexOf(".exe");
                dllPos = filePath.ToLower().IndexOf(".dll");
                tailPos = -exePos * dllPos;
                if (colonPos == -1) fileName = "C:\\Windows\\system32\\" + filePath.Substring(0, tailPos + 4);
                fileName = filePath.Substring(colonPos-1, tailPos-colonPos+5);
                //listView1.Items.Add(fileName);
                FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(fileName);
                ListViewItem listItem1 = new ListViewItem(entryName.ToString(),0,listGroup1);
                listItem1.SubItems.Add(fileverInfo.FileDescription);
                listItem1.SubItems.Add(fileverInfo.CompanyName);
                listItem1.SubItems.Add(fileName);
                //listItem1.SubItems.Add(filever.ProductName);
                listView1.Items.Add(listItem1);
            }
            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey logon2 = hkcu.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            ListViewGroup listGroup2 = new ListViewGroup("HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            listView1.Groups.Add(listGroup2);
            for (int j = 0; j < logon2.GetValueNames().Length; j++)
            {
                entryName = logon2.GetValueNames().ElementAt(j);
                filePath = logon2.GetValue(entryName).ToString();
                if (filePath == "") continue;
                colonPos = filePath.IndexOf(":");
                exePos = filePath.ToLower().IndexOf(".exe");
                dllPos = filePath.ToLower().IndexOf(".dll");
                tailPos = -exePos * dllPos;
                //listView1.Items.Add(tailPos.ToString());
                if (colonPos == -1) fileName = "C:\\Windows\\system32\\" + filePath.Substring(0, tailPos + 4);
                fileName = filePath.Substring(colonPos - 1, tailPos - colonPos + 5);
                //listView1.Items.Add(fileName);
                FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(fileName);
                ListViewItem listItem2 = new ListViewItem(entryName.ToString(),0,listGroup2);
                listItem2.SubItems.Add(fileverInfo.FileDescription);
                listItem2.SubItems.Add(fileverInfo.CompanyName);
                listItem2.SubItems.Add(filePath);
                listView1.Items.Add(listItem2);
            }
            RegistryKey logon3 = hklm.OpenSubKey("SOFTWARE\\Microsoft\\Active Setup\\Installed Components");
            RegistryKey CLSIDkey = hklm.OpenSubKey("Software\\Classes\\CLSID");
            ListViewGroup listGroup3 = new ListViewGroup("HKLM\\SOFTWARE\\Microsoft\\Active Setup\\Installed Components");
            listView1.Groups.Add(listGroup3);
            foreach (string ieName in logon3.GetSubKeyNames())
            {
                foreach (string key in CLSIDkey.GetSubKeyNames())
                {
                    if (key.Equals(ieName))
                    {
                        RegistryKey subkey = CLSIDkey.OpenSubKey(key).OpenSubKey("InprocServer32");
                        FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(subkey.GetValue("").ToString());
                        ListViewItem listItem3 = new ListViewItem(fileverInfo.InternalName, 0, listGroup3);
                        listItem3.SubItems.Add(fileverInfo.FileDescription);
                        listItem3.SubItems.Add(fileverInfo.CompanyName);
                        listItem3.SubItems.Add(fileverInfo.FileName);
                        listView1.Items.Add(listItem3);
                    }
                }
            }
            RegistryKey logon4 = hklm.OpenSubKey("System\\CurrentControlSet\\Control\\Terminal Server\\Wds\\rdpwd");
            ListViewGroup listGroup4 = new ListViewGroup("HKLM\\System\\CurrentControlSet\\Control\\Terminal Server\\Wds\\rdpwd");
            listView1.Groups.Add(listGroup4);
            fileName = logon4.GetValue("StartupPrograms").ToString();
            filePath = "C:\\Windows\\System32\\" + fileName + ".exe";
            if (filePath != "")
            {
                try
                {
                    FileVersionInfo fileverInfo2 = FileVersionInfo.GetVersionInfo(filePath);
                    ListViewItem listItem4 = new ListViewItem("rdpclip", 0, listGroup4);
                    listItem4.SubItems.Add(fileverInfo2.FileDescription);
                    listItem4.SubItems.Add(fileverInfo2.CompanyName);
                    listItem4.SubItems.Add(filePath);
                    listView1.Items.Add(listItem4);
                }
                catch
                {
                    
                }
            }
        }

        private void listView2_Layout(object sender, LayoutEventArgs e)
        {
            listView2.View = View.Details;
            listView2.Columns.Clear();
            listView2.Items.Clear();
            listView2.Groups.Clear();
            listView2.Columns.Add("Auto Entry", 150);
            listView2.Columns.Add("Description", 150);
            listView2.Columns.Add("Publisher", 150);
            listView2.Columns.Add("Image Path", 320);
            RegistryKey ieKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects");
            RegistryKey CLSIDkey = Registry.LocalMachine.OpenSubKey("Software\\Classes\\CLSID");
            ListViewGroup listGroup1 = new ListViewGroup("HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects");
            listView2.Groups.Add(listGroup1);
            foreach (string ieName in ieKey.GetSubKeyNames())
            {
                foreach (string key in CLSIDkey.GetSubKeyNames())
                {
                    if (key.Equals(ieName))
                    {
                        RegistryKey subkey = CLSIDkey.OpenSubKey(key).OpenSubKey("InprocServer32");
                        FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(subkey.GetValue("").ToString());
                        ListViewItem listItem = new ListViewItem(fileverInfo.ProductName, 0,listGroup1);
                        listItem.SubItems.Add(fileverInfo.FileDescription);
                        listItem.SubItems.Add(fileverInfo.CompanyName);
                        listItem.SubItems.Add(fileverInfo.FileName);
                        listView2.Items.Add(listItem);
                    }
                }
            }
            //RegistryKey ieexKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Extensions");
            //RegistryKey CLSIDexkey = Registry.LocalMachine.OpenSubKey("Software\\Classes\\CLSID");
            //ListViewGroup listGroup2 = new ListViewGroup("HKLM\\Software\\Microsoft\\Internet Explorer\\Extensions");
            //listView2.Groups.Add(listGroup2);
        }

        private void listView3_Layout(object sender, LayoutEventArgs e)
        {
            listView3.View = View.Details;
            listView3.Columns.Clear();
            listView3.Items.Clear();
            listView3.Groups.Clear();
            listView3.Columns.Add("Auto Entry", 150);
            listView3.Columns.Add("Description", 150);
            listView3.Columns.Add("Publisher", 150);
            listView3.Columns.Add("Image Path", 320);
            string filePath, fileName;
            int type, colonPos, exePos, dllPos, tailPos;
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey service = hklm.OpenSubKey("System\\CurrentControlSet\\Services");
            ListViewGroup listGroup1 = new ListViewGroup("HKLM\\System\\CurrentControlSet\\Services");
            listView3.Groups.Add(listGroup1);
            foreach (string serviceName in service.GetSubKeyNames())
            {
                RegistryKey key = service.OpenSubKey(serviceName);
                if (key.GetValue("ImagePath") != null)
                {
                    filePath = key.GetValue("ImagePath").ToString();
                    //listView3.Items.Add(filePath);
                }
                else
                {
                    filePath = "";
                }
                if (key.GetValue("Type") != null)
                {
                    type = (int)key.GetValue("Type");
                }
                else
                {
                    type = 0;
                }
                colonPos = filePath.IndexOf(":");
                exePos = filePath.ToLower().IndexOf(".exe");
                dllPos = filePath.ToLower().IndexOf(".dll");
                tailPos = -exePos * dllPos;
                //listView3.Items.Add(tailPos.ToString());
                if ((tailPos != 1) && ((type == 32) || (type == 16)))
                {
                    if (filePath.ToLower().IndexOf("svchost") != -1)
                    {
                        try
                        {
                            RegistryKey parameter = key.OpenSubKey("Parameters");
                            if (parameter != null)
                            {
                                fileName = parameter.GetValue("ServiceDll").ToString();
                            }
                            else
                            {
                                fileName = filePath.Substring(colonPos - 1, tailPos - colonPos + 5);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        fileName = filePath.Substring(colonPos - 1, tailPos - colonPos + 5);
                    }
                    if (fileName.IndexOf(":") != -1)
                    {
                        try
                        {
                            FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(fileName);
                            ListViewItem listItem = new ListViewItem(serviceName, 0, listGroup1);
                            listItem.SubItems.Add(fileverInfo.FileDescription);
                            listItem.SubItems.Add(fileverInfo.CompanyName);
                            listItem.SubItems.Add(fileName);
                            listView3.Items.Add(listItem);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
        }

        private void listView4_Layout(object sender, LayoutEventArgs e)
        {
            listView4.View = View.Details;
            listView4.Columns.Clear();
            listView4.Items.Clear();
            listView4.Groups.Clear();
            listView4.Columns.Add("Auto Entry", 150);
            listView4.Columns.Add("Description", 150);
            listView4.Columns.Add("Publisher", 150);
            listView4.Columns.Add("Image Path", 320);
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey driver = hklm.OpenSubKey("System\\CurrentControlSet\\Services");
            ListViewGroup listGroup = new ListViewGroup("HKLM\\System\\CurrentControlSet\\Services");
            listView4.Groups.Add(listGroup);
            string filePath, replacedPath, fileName, description, company;
            int colonPos, sysPos;
            foreach (string driverName in driver.GetSubKeyNames())
            {
                RegistryKey key = driver.OpenSubKey(driverName);
                if (key.GetValue("ImagePath") != null)
                {
                    filePath = key.GetValue("ImagePath").ToString();
                }
                else
                {
                    filePath = "";
                }
                //listView4.Items.Add(filePath);
                if ((filePath != "") && (filePath.ToLower().IndexOf(".sys") != -1))
                {
                    if (filePath.ToLower().IndexOf("system32") != -1)
                    {
                        //listView4.Items.Add(filePath.ToLower().IndexOf("system32").ToString());
                        if (filePath.ToLower().IndexOf("system32") != 0)
                        {
                            if (filePath.ToLower().IndexOf("%systemroot%") != -1)
                            {
                                replacedPath = Environment.ExpandEnvironmentVariables(filePath);
                            }
                            else
                            {
                                if (filePath.IndexOf("\\SystemRoot") != -1)
                                {
                                    replacedPath = filePath.Replace(filePath.Substring(0, 11), "%systemroot%");
                                    replacedPath = Environment.ExpandEnvironmentVariables(replacedPath);
                                }
                                else
                                {
                                    replacedPath = filePath;
                                }
                            }
                        }
                        else
                        {
                            replacedPath = "C:\\Windows\\" + filePath;
                        }
                    }
                    else
                    {
                        replacedPath = filePath;
                    }
                    //listView4.Items.Add(replacedPath);
                    colonPos = replacedPath.IndexOf(":");
                    sysPos = replacedPath.ToLower().IndexOf(".sys");
                    fileName = replacedPath.Substring(colonPos - 1, sysPos - colonPos + 5);
                    //listView4.Items.Add(fileName);
                    try
                    {
                        FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(fileName);
                        description = fileverInfo.FileDescription;
                        company = fileverInfo.CompanyName;
                    }
                    catch
                    {
                        continue;
                    }
                    ListViewItem listItem = new ListViewItem(driverName, 0, listGroup);
                    listItem.SubItems.Add(description);
                    listItem.SubItems.Add(company);
                    listItem.SubItems.Add(fileName);
                    listView4.Items.Add(listItem);
                }
            }
        }

        private void listView5_Layout(object sender, LayoutEventArgs e)
        {
            listView5.View = View.Details;
            listView5.Columns.Clear();
            listView5.Items.Clear();
            listView5.Groups.Clear();
            listView5.Columns.Add("Auto Entry", 150);
            listView5.Columns.Add("Description", 150);
            listView5.Columns.Add("Publisher", 150);
            listView5.Columns.Add("Image Path", 320);
            ListViewGroup listGroup = new ListViewGroup("Task Scheduler");
            listView5.Groups.Add(listGroup);
            FileInfo fileName;
            string filePath = "";
            int comPos, endcomPos, colonPos, exePos;
            DirectoryInfo taskdir = new DirectoryInfo("C:\\Windows\\System32\\Tasks");
            for (int i = 0; i < Directory.GetFiles("C:\\Windows\\System32\\Tasks").Length; i++)
            {
                fileName = taskdir.GetFiles().ElementAt(i);
                //FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fileName.FullName);
                FileStream fStream = new FileStream(fileName.FullName, FileMode.Open);
                byte[] content = new byte[256];
                int j=0;
                while (j < fStream.Length)
                {
                    fStream.Read(content, 0, 256);
                    foreach (byte b in content)
                    {
                        if (b != 0)
                        {
                            filePath += (char)b;
                        }
                    }
                    j += 256;
                }
                comPos = filePath.IndexOf("<Command>");
                endcomPos = filePath.IndexOf("</Command>");
                filePath = filePath.Substring(comPos + 9, endcomPos - comPos - 9);
                colonPos = filePath.IndexOf(":");
                exePos = filePath.ToLower().IndexOf(".exe");
                filePath = filePath.Substring(colonPos - 1, exePos - colonPos + 5);
                //listView5.Items.Add(filePath);
                fStream.Close();
                FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(filePath);
                ListViewItem listItem = new ListViewItem(fileName.Name, 0, listGroup);
                listItem.SubItems.Add(fileverInfo.FileDescription);
                listItem.SubItems.Add(fileverInfo.CompanyName);
                listItem.SubItems.Add(filePath);
                listView5.Items.Add(listItem);

            }
        }

        private void listView6_Layout(object sender, LayoutEventArgs e)
        {
            listView6.View = View.Details;
            listView6.Columns.Clear();
            listView6.Items.Clear();
            listView6.Groups.Clear();
            listView6.Columns.Add("Auto Entry", 150);
            listView6.Columns.Add("Description", 150);
            listView6.Columns.Add("Publisher", 150);
            listView6.Columns.Add("Image Path", 320);
            ListViewGroup listGroup = new ListViewGroup("HKLM\\System\\CurrentControlSet\\Control\\Session Manager\\KnownDLLs");
            listView6.Groups.Add(listGroup);
            string dlldir = "", fileName, filePath;
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey dllkey = hklm.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\KnownDLLs");
            foreach (string dll in dllkey.GetValueNames())
            {
                if (dll.Equals("DllDirectory"))
                {
                    dlldir = dllkey.GetValue(dll).ToString();
                    //listView6.Items.Add(dlldir);
                }
            }
            foreach (string value in dllkey.GetValueNames())
            {
                fileName = dllkey.GetValue(value).ToString();
                //listView6.Items.Add(fileName);
                if (fileName.ToLower().IndexOf(".dll") != -1)
                {
                    filePath = dlldir + "\\" + fileName;
                }
                else
                {
                    continue;
                }
                //listView6.Items.Add(fileName);
                FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(filePath);
                ListViewItem listItem = new ListViewItem(value, 0, listGroup);
                listItem.SubItems.Add(fileverInfo.FileDescription);
                listItem.SubItems.Add(fileverInfo.CompanyName);
                listItem.SubItems.Add(filePath);
                listView6.Items.Add(listItem);
            }
        }

        private void listView7_Layout(object sender, LayoutEventArgs e)
        {
            listView7.View = View.Details;
            listView7.Columns.Clear();
            listView7.Items.Clear();
            listView7.Groups.Clear();
            listView7.Columns.Add("Auto Entry", 150);
            listView7.Columns.Add("Description", 150);
            listView7.Columns.Add("Publisher", 150);
            listView7.Columns.Add("Image Path", 320);
            ListViewGroup listGroup = new ListViewGroup("HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
            listView7.Groups.Add(listGroup);
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey winlogon = hklm.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
            string key1 = winlogon.GetValue("Shell").ToString();
            //listView7.Items.Add(key);
            string[] shellArr = key1.Split(',');
            for (int i = 0; i < shellArr.Length; i++)
            {
                if (shellArr[i] == "explorer.exe")
                {
                    shellArr[i] = "C:\\Windows\\" + shellArr[i];
                }
                else
                {
                    shellArr[i] = "C:\\Windows\\System32\\" + shellArr[i];
                }
                //listView7.Items.Add(shellArr[i]);
                FileVersionInfo fileverInfo1 = FileVersionInfo.GetVersionInfo(shellArr[i]);
                ListViewItem listItem1 = new ListViewItem(fileverInfo1.InternalName, 0, listGroup);
                listItem1.SubItems.Add(fileverInfo1.FileDescription);
                listItem1.SubItems.Add(fileverInfo1.CompanyName);
                listItem1.SubItems.Add(shellArr[i]);
                listView7.Items.Add(listItem1);
            }
            string key2 = winlogon.GetValue("Userinit").ToString();
            //listView7.Items.Add(key);
            string[] usrArr = key2.Split(',');
            for (int j = 0; j < usrArr.Length; j++){
                if (usrArr[j] != "")
                {
                    FileVersionInfo fileverInfo2 = FileVersionInfo.GetVersionInfo(usrArr[j]);
                    ListViewItem listItem2 = new ListViewItem(fileverInfo2.InternalName, 0, listGroup);
                    listItem2.SubItems.Add(fileverInfo2.FileDescription);
                    listItem2.SubItems.Add(fileverInfo2.CompanyName);
                    listItem2.SubItems.Add(usrArr[j]);
                    listView7.Items.Add(listItem2);
                }
            }
        }

        private void listView8_Layout(object sender, LayoutEventArgs e)
        {
            listView8.View = View.Details;
            listView8.Columns.Clear();
            listView8.Items.Clear();
            listView8.Groups.Clear();
            listView8.Columns.Add("Auto Entry", 150);
            listView8.Columns.Add("Description", 150);
            listView8.Columns.Add("Publisher", 150);
            listView8.Columns.Add("Image Path", 320);
            ListViewGroup listGroup = new ListViewGroup("HKLM\\System\\CurrentControlSet\\Control\\Session Manager");
            listView8.Groups.Add(listGroup);
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey bootexe = hklm.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager");
            string filePath;
            if (bootexe.GetValue("BootExecute") != null)
            {
                string[] bootName = (string[]) bootexe.GetValue("BootExecute");
                foreach (string boot in bootName)
                {
                    if (boot == "autocheck autochk *")
                    {
                        filePath = "C:\\Windows\\System32\\autochk.exe";
                    }
                    else
                    {
                        filePath = boot;
                    }
                    if (filePath != "")
                    {
                        FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(filePath);
                        ListViewItem listItem = new ListViewItem(fileverInfo.InternalName, 0, listGroup);
                        listItem.SubItems.Add(fileverInfo.FileDescription);
                        listItem.SubItems.Add(fileverInfo.CompanyName);
                        listItem.SubItems.Add(filePath);
                        listView8.Items.Add(listItem);
                    }
                }
            }
        }

        private void listView9_Layout(object sender, LayoutEventArgs e)
        {
            listView9.View = View.Details;
            listView9.Columns.Clear();
            listView9.Items.Clear();
            listView9.Groups.Clear();
            listView9.Columns.Add("Auto Entry", 150);
            listView9.Columns.Add("Description", 150);
            listView9.Columns.Add("Publisher", 150);
            listView9.Columns.Add("Image Path", 320);
            ListViewGroup listGroup = new ListViewGroup("HKLM\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");
            listView9.Groups.Add(listGroup);
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey imagehj = hklm.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options");
            string filePath;
            if (imagehj != null)
            {
                foreach (string imageName in imagehj.GetSubKeyNames())
                {
                    if (imagehj.OpenSubKey(imageName).GetValue("Debugger") != null)
                    {
                        filePath = imagehj.OpenSubKey(imageName).GetValue("Debugger").ToString();
                    }
                    else
                    {
                        filePath = "";
                    }
                    if (filePath != "")
                    {
                        FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(filePath);
                        ListViewItem listItem = new ListViewItem(fileverInfo.InternalName, 0, listGroup);
                        listItem.SubItems.Add(fileverInfo.FileDescription);
                        listItem.SubItems.Add(fileverInfo.CompanyName);
                        listItem.SubItems.Add(filePath);
                        listView9.Items.Add(listItem);
                    }

                }
            }
        }

        private void listView10_Layout(object sender, LayoutEventArgs e)
        {
            listView10.View = View.Details;
            listView10.Columns.Clear();
            listView10.Items.Clear();
            listView10.Groups.Clear();
            listView10.Columns.Add("Auto Entry", 150);
            listView10.Columns.Add("Description", 150);
            listView10.Columns.Add("Publisher", 150);
            listView10.Columns.Add("Image Path", 320);
            ListViewGroup listGroup = new ListViewGroup("HKLM\\SYSTEM\\CurrentControlSet\\Services\\WinSock2\\Parameters\\Protocol_Catalog9\\Catalog_Entries");
            listView10.Groups.Add(listGroup);
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey winsock = hklm.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\WinSock2\\Parameters\\Protocol_Catalog9\\Catalog_Entries");
            foreach (string win in winsock.GetSubKeyNames())
            {
                byte[] content = (byte[])winsock.OpenSubKey(win).GetValue("PackedCatalogItem");
                string filePath = "";
                string fileDescription = "";
                for (int i = 0; i < content.Length; i++)
                {
                    if (content[i] == 0)
                    {
                        break;
                    }
                    else
                    {
                        filePath += (char)content[i];
                    }
                }
                filePath = Environment.ExpandEnvironmentVariables(filePath);
                //listView10.Items.Add(filePath);
                for (int j = 0x0178; j < content.Length; j += 2)
                {
                    if (content[j] == 0)
                    {
                        break;
                    }
                    else
                    {
                        fileDescription += (char)content[j];
                    }
                }
                //listView10.Items.Add(fileDescription);
                FileVersionInfo fileverInfo = FileVersionInfo.GetVersionInfo(filePath);
                ListViewItem listItem = new ListViewItem(fileverInfo.InternalName, 0, listGroup);
                listItem.SubItems.Add(fileDescription);
                listItem.SubItems.Add(fileverInfo.CompanyName);
                listItem.SubItems.Add(filePath);
                listView10.Items.Add(listItem);
            }

        }
    }
}
