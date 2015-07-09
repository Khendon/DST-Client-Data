using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism;

namespace SRO_Management.Models
{
    /// <summary>
    /// Used by viewmodel for selection of files to read depending on certain conditions on view.
    /// Opens OpenFileDialog with multiple file select on/off depending on user requirements.
    /// Directory path property also used for output file.
    /// </summary>
    public class FileSelectModel
    {
        public FileSelectModel()
        {
            this.DefaultDirPath();
        }

        public List<string> MultipleFiles { get; set; }

        public string DirPath { get; set; }

        public string MemFile { get; set; }

        public string TargetDir { get; set; }

        public string FileSaveName { get; set; }        

        public void UserFileTypeSelection(FileTypes userSelectedFileType)
        {
            switch (userSelectedFileType)
            {                
                case FileTypes.Memory:
                    this.MemFileSelection();
                    break;
                case FileTypes.SRO:
                    this.MultiFileSelection();
                    break;
                default:
                    System.Diagnostics.Trace.Assert(false, "User selected file type not implemented");
                    throw new NotImplementedException("User selected file type not implemented");                   
            }
        }

        public void SaveTargetDir(FileTypes selectedFileType, string serialInput, string positionInput)
        {
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = "Data Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveDialog.FileName = string.Format(@"{0}_{1}_{2}_{3:MM-yy_HHmm}.txt", selectedFileType.ToString(), serialInput, positionInput, DateTime.Now);
            saveDialog.InitialDirectory = this.DirPath;

            if (saveDialog.ShowDialog() == true)
            {
                this.FileSaveName = saveDialog.FileName;
            }
        }

        private void DefaultDirPath()
        {
            string defaultPath;
            string userName;
            string trimUser;

            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            trimUser = userName.Substring(userName.IndexOf(@"\") + 1);

            defaultPath = @"C:\Users\" + trimUser + @"\Configuration Workshop\";

            this.DirPath = defaultPath;
        }

        private void MemFileSelection()
        {
            this.MemFile = string.Empty;

            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();

            openDialog.Filter = "Data Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openDialog.Multiselect = false;
            openDialog.InitialDirectory = this.DirPath;

            string fileName;

            try
            {
                if (openDialog.ShowDialog() == true)
                {
                    fileName = System.IO.Path.GetFileName(openDialog.FileName);
                    this.DirPath = System.IO.Path.GetDirectoryName(openDialog.FileName);
                    this.MemFile = fileName;
                }
            }
            catch (Exception memFileEx)
            {                
                System.Diagnostics.Trace.WriteLine(DateTime.Now + "," + memFileEx.ToString(), ",Please select a valid sensor log file");
                this.MemFile = null;
                throw;
            }
        }

        private void MultiFileSelection()
        {
            if (this.MultipleFiles != null)
            {
                this.MultipleFiles.Clear();
            }

            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();

            openDialog.Filter = "Data Files (*.hdf; *.txt)|*.hdf; *.txt|All Files (*.*)|*.*";
            openDialog.Multiselect = true;
            openDialog.InitialDirectory = this.DirPath;

            List<string> files = new List<string>();

            try
            {
                if (openDialog.ShowDialog() == true)
                {
                    foreach (string fileName in openDialog.FileNames)
                    {
                        files.Add(System.IO.Path.GetFileName(fileName));
                    }

                    this.MultipleFiles = files;
                    this.DirPath = System.IO.Path.GetDirectoryName(openDialog.FileName);
                }
            }
            catch (Exception sroFileEx)
            {
                System.Diagnostics.Trace.WriteLine(DateTime.Now + "," + sroFileEx.ToString(), ",Please select valid SRO data files");
                this.MultipleFiles = null;
                throw;
            }
        }        
    }
}