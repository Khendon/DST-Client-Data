using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism;
using System.Threading;


namespace SRO_Management.ViewModels
{
    public class MainWindowViewModel : INotifyViewModel
    {

        private Models.HeaderModel Header;
        private Models.FileSelectModel FileSelect;
        


        #region File Header Labels
        public string ClientLabel
        {
            get { return Header.ClientLabel; }
            set { Header.ClientLabel = value; RaisePropertyChanged(); }
        }

        public string WellLabel
        {
            get { return Header.WellLabel; }
            set { Header.WellLabel = value; RaisePropertyChanged(); }
        }

        public string DstLabel
        {
            get { return Header.DstLabel; }
            set { Header.DstLabel = value; RaisePropertyChanged(); }
        }

        public string SerialLabel
        {
            get { return Header.SerialLabel; }
            set { Header.SerialLabel = value; RaisePropertyChanged(); }
        }


        public string DepthLabel 
        {
            get { return Header.DepthLabel; }
            set { Header.DepthLabel = value; RaisePropertyChanged(); }
        }

        public string Position
        {
            get { return Header.PositionLabel; }
            set { Header.PositionLabel = value; RaisePropertyChanged(); }
        }

        public string PresStringFormat 
        {
            get { return Header.PresStringFormat; }
            set { Header.PresStringFormat = value; RaisePropertyChanged();} 
        }

        public string TempStringFormat
        {
            get { return Header.TempStringFormat; }
            set { Header.TempStringFormat = value; RaisePropertyChanged(); } 
        }

        #endregion


        #region File Header User Inputs
        public string ClientInput
        {
            get { return Header.ClientInput; }
            set { Header.ClientInput = value; RaisePropertyChanged(); }
        }

        public string WellInput
        {
            get { return Header.WellInput; }
            set { Header.WellInput = value; RaisePropertyChanged(); }
        }

        public string DstInput
        {
            get { return Header.DstInput; }
            set { Header.DstInput = value; RaisePropertyChanged(); }
        }

        public string SerialInput
        {
            get { return Header.SerialInput; }
            set { Header.SerialInput = value; RaisePropertyChanged(); ExportDataFilesCommand.RaiseCanExecuteChanged(); }
        }

        public string DepthInput
        {
            get { return Header.DepthInput; }
            set { Header.DepthInput = value; RaisePropertyChanged(); }
        }

        public string PositionInput
        {
            get { return Header.PositionInput; }
            set { Header.PositionInput = value; RaisePropertyChanged(); }
        }


        #endregion


        #region Path of currently selected directory
        public string SelectedDirectory
        {
            get { return FileSelect.DirPath; }
            set { FileSelect.DirPath = value; RaisePropertyChanged(); }
        }
        #endregion


        #region Filter groupbox properties

        private bool allDataCb;

        public bool AllDataCb
        {
            get { return allDataCb; }
            set 
            { 
                allDataCb = value;                 
                RaisePropertyChanged();
            }
        }
        

        private Models.FileTypes selectFileType;
        public Models.FileTypes SelectFileType
        {
            get { return selectFileType; }
            set 
            { 
                selectFileType = value;
                FileSelect.MemFile = null;
                FileSelect.MultipleFiles = null;
                RaisePropertyChanged();
                ExportDataFilesCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<Models.FileTypes> FileTypeComboBox
        {
            get 
            {
                return Enum.GetValues(typeof(Models.FileTypes))
                    .Cast<Models.FileTypes>();
            }
        }
        
        
        // Bound to pressure unit combobox
        private Models.PresUnitSelection selectedPresUnit;
        public Models.PresUnitSelection SelectedPresUnit
        {
            get { return selectedPresUnit; }
            set
            {
                selectedPresUnit = value;
                PresStringFormat = selectedPresUnit.ToString();
                RaisePropertyChanged();
            }
        }

        //Populate combobox with pressure unit enum values.
        public IEnumerable<Models.PresUnitSelection> PresUnitValuesComboBox
        {
            get
            {
                return Enum.GetValues(typeof(Models.PresUnitSelection))
                    .Cast<Models.PresUnitSelection>();
            }
        }


        // Bound to temp unit combobox
        private Models.TempUnitSelection selectedTempUnit;
        public Models.TempUnitSelection SelectedTempUnit
        {
            get { return selectedTempUnit; }
            set
            {
                selectedTempUnit = value;
                TempStringFormat = selectedTempUnit.ToString();
                RaisePropertyChanged();
            }
        }

        //Populate combobox with temp unit enum values.
        public IEnumerable<Models.TempUnitSelection> TempUnitValueComboBox
        {
            get 
            {
                return Enum.GetValues(typeof(Models.TempUnitSelection))
                    .Cast<Models.TempUnitSelection>();
            }
        }


        private DateTime? filterStartTime = null;

        public DateTime? FilterStartTime
        {
            get { return filterStartTime; }
            set { filterStartTime = value; RaisePropertyChanged(); }
        }


        private DateTime? filterEndTime = null;

        public DateTime? FilterEndTime
        {
            get { return filterEndTime; }
            set { filterEndTime = value; RaisePropertyChanged(); }
        }
        
        


        #endregion


        #region Command properties
        public DelegateCommand SelectFilesToExportCommand { get; private set; }
        public DelegateCommand ExportDataFilesCommand { get; private set; }
        #endregion


        public MainWindowViewModel()
        {   
            //Instantiate client file header model object
            Header = new Models.HeaderModel();
            // Instantiate file selection and dir path model
            FileSelect = new Models.FileSelectModel();
            //Default Pressure Units Combobox
            SelectedPresUnit = Models.PresUnitSelection.psia;
            //Default Temp Units Combobox
            SelectedTempUnit = Models.TempUnitSelection.degC;
            //All Data checkbox default value
            AllDataCb = true;

            SelectFilesToExportCommand = new DelegateCommand(OnSelectFilesToExport, CanSelectFilesToExport);
            ExportDataFilesCommand = new DelegateCommand(OnExportDataFiles, CanExportDataFiles);
        }

        #region File Select Command Implementation
        private bool CanSelectFilesToExport()
        {
            return true;
        }

        private void OnSelectFilesToExport()
        {
            //Call to method with a switch statement that opens the correct OpenFileDialog and returns the filename/filenames to the properties.
            FileSelect.UserFileTypeSelection(SelectFileType);
            ExportDataFilesCommand.RaiseCanExecuteChanged();
        }
        #endregion


        #region Export command implementation
        private bool CanExportDataFiles()
        {

            if (Header.SerialInput != null && (FileSelect.MultipleFiles != null || FileSelect.MemFile != null))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void OnExportDataFiles()
        {

            FileSelect.SaveTargetDir(SelectFileType, SerialInput, PositionInput);

            IEnumerable<Models.IDataRecord> readInputFiles;

            if (SelectFileType == Models.FileTypes.Memory)
            {
                readInputFiles = new Models.MemoryReader(SelectedDirectory, FileSelect.MemFile, SelectFileType);
            }
            else
            {
                readInputFiles = new Models.SROReader(SelectedDirectory, FileSelect.MultipleFiles, SelectFileType);
            }


            Models.UnitConverter converter = new Models.UnitConverter();
            IEnumerable<Models.IDataRecord> convertedRecords = converter.ConvertUnits(readInputFiles, SelectedPresUnit, SelectedTempUnit);

            Models.SortAndFilterData filter = new Models.SortAndFilterData();
            IEnumerable<Models.IDataRecord> filteredRecords = filter.ChooseFilters(convertedRecords, AllDataCb, FilterStartTime, FilterEndTime);

            Models.CsvWriter writer = new Models.CsvWriter();
            writer.CreateFileWriterStreams(FileSelect.FileSaveName, filteredRecords, Header);

            ExportDataFilesCommand.RaiseCanExecuteChanged();
        }
        #endregion







    }
}

