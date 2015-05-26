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
        private List<string> multipleFiles;

        public List<string> MultipleFiles
        {
            get { return multipleFiles; }
            set { multipleFiles = value; }
        }

        private string dirPath;

        public string DirPath
        {
            get { return dirPath; }
            set { dirPath = value; }
        }

        private string memFile;

        public string MemFile
        {
            get { return memFile; }
            set { memFile = value; }
        }

        private string targetDir;

        public string TargetDir
        {
            get { return targetDir; }
            set { targetDir = value; }
        }

        private string fileSaveName;

        public string FileSaveName
        {
            get { return fileSaveName; }
            set { fileSaveName = value; }
        }

        public FileSelectModel()
        {
            DefaultDirPath();
        }

        public void UserFileTypeSelection(FileTypes userSelectedFileType)
        {
            switch (userSelectedFileType)
            {                
                case FileTypes.Memory:
                    MemFileSelection();
                    break;
                case FileTypes.SRO:
                    MultiFileSelection();
                    break;
                default:
                    System.Diagnostics.Trace.Assert(false, "User selected file type not implemented");
                    throw new NotImplementedException("User selected file type not implemented");                   
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

            DirPath = defaultPath;
        }

        private void MemFileSelection()
        {
            MemFile = "";

            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();

            openDialog.Filter = "Data Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openDialog.Multiselect = false;
            openDialog.InitialDirectory = DirPath;

            string fileName;

            try
            {
                if (openDialog.ShowDialog() == true)
                {

                    fileName = System.IO.Path.GetFileName(openDialog.FileName);
                    DirPath = System.IO.Path.GetDirectoryName(openDialog.FileName);
                    MemFile = fileName;
                }
            }
            catch (Exception memFileEx)
            {                
                System.Diagnostics.Trace.WriteLine(DateTime.Now + "," + memFileEx.ToString(), ",Please select a valid sensor log file");
                MemFile = null;
                throw;
            }
        }

        private void MultiFileSelection()
        {
            if (MultipleFiles != null)
            {
                MultipleFiles.Clear();
            }

            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();

            openDialog.Filter = "Data Files (*.hdf; *.txt)|*.hdf; *.txt|All Files (*.*)|*.*";
            openDialog.Multiselect = true;
            openDialog.InitialDirectory = DirPath;

            List<string> files = new List<string>();

            try
            {
                if (openDialog.ShowDialog() == true)
                {
                    foreach (string fileName in openDialog.FileNames)
                    {
                        files.Add(System.IO.Path.GetFileName(fileName));
                    }

                    MultipleFiles = files;
                    DirPath = System.IO.Path.GetDirectoryName(openDialog.FileName);
                }
            }
            catch (Exception sroFileEx)
            {
                System.Diagnostics.Trace.WriteLine(DateTime.Now + "," + sroFileEx.ToString(), ",Please select valid SRO data files");
                MultipleFiles = null;
                throw;
            }
        }

        public void SaveTargetDir(FileTypes selectedFileType, string serialInput, string positionInput)
        {
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = "Data Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveDialog.FileName = string.Format(@"{0}_{1}_{2}_{3:MM-yy_HHmm}.txt", selectedFileType.ToString(), serialInput, positionInput, DateTime.Now);
            saveDialog.InitialDirectory = DirPath;

            if (saveDialog.ShowDialog() == true)
            {
                FileSaveName = saveDialog.FileName;
            }
        }
    }
}