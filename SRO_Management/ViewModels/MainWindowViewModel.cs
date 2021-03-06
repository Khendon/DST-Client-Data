﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Threading;


namespace SRO_Management.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private Models.HeaderModel Header;
        private Models.FileSelectModel FileSelect;
        


        #region File Header Labels
        public string ClientLabel
        {
            get { return Header.ClientLabel; }
            set { Header.ClientLabel = value; OnPropertyChanged("ClientLabel"); }
        }

        public string WellLabel
        {
            get { return Header.WellLabel; }
            set { Header.WellLabel = value; OnPropertyChanged("WellLabel"); }
        }

        public string DstLabel
        {
            get { return Header.DstLabel; }
            set { Header.DstLabel = value; OnPropertyChanged("DstLabel"); }
        }

        public string SerialLabel
        {
            get { return Header.SerialLabel; }
            set { Header.SerialLabel = value; OnPropertyChanged("SerialLabel"); }
        }


        public string DepthLabel 
        {
            get { return Header.DepthLabel; }
            set { Header.DepthLabel = value; OnPropertyChanged("DepthLabel"); }
        }

        public string Position
        {
            get { return Header.PositionLabel; }
            set { Header.PositionLabel = value; OnPropertyChanged("PositionLabel"); }
        }

        public string PresStringFormat 
        {
            get { return Header.PresStringFormat; }
            set { Header.PresStringFormat = value; OnPropertyChanged("PresStringFormat");} 
        }

        public string TempStringFormat
        {
            get { return Header.TempStringFormat; }
            set { Header.TempStringFormat = value; OnPropertyChanged("TempStringFormat"); } 
        }

        #endregion


        #region File Header User Inputs
        public string ClientInput
        {
            get { return Header.ClientInput; }
            set { Header.ClientInput = value; OnPropertyChanged("ClientInput"); }
        }

        public string WellInput
        {
            get { return Header.WellInput; }
            set { Header.WellInput = value; OnPropertyChanged("WellInput"); }
        }

        public string DstInput
        {
            get { return Header.DstInput; }
            set { Header.DstInput = value; OnPropertyChanged("DstInput"); }
        }

        public string SerialInput
        {
            get { return Header.SerialInput; }
            set { Header.SerialInput = value; OnPropertyChanged("SerialInput"); ExportDataFilesCommand.RaiseCanExecuteChanged(); }
        }

        public string DepthInput
        {
            get { return Header.DepthInput; }
            set { Header.DepthInput = value; OnPropertyChanged("DepthInput"); }
        }

        public string PositionInput
        {
            get { return Header.PositionInput; }
            set { Header.PositionInput = value; OnPropertyChanged("PositionInput"); ExportDataFilesCommand.RaiseCanExecuteChanged(); }
        }


        #endregion


        #region Path of currently selected directory
        public string SelectedDirectory
        {
            get { return FileSelect.DirPath; }
            set { FileSelect.DirPath = value; OnPropertyChanged("DirPath"); }
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
                OnPropertyChanged("AllDataCb");
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
                OnPropertyChanged("SelectFileType");
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
                OnPropertyChanged("SelectedPresUnit");
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
                OnPropertyChanged("SelectedTempUnit");
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
            set { filterStartTime = value; OnPropertyChanged("FilterStartTime"); }
        }


        private DateTime? filterEndTime = null;

        public DateTime? FilterEndTime
        {
            get { return filterEndTime; }
            set { filterEndTime = value; OnPropertyChanged("FilterEndTime"); }
        }

        private int shiftHours;
        public int ShiftHours
        {
            get { return shiftHours; }
            set { shiftHours = value; OnPropertyChanged("shiftHours"); }
        }

        private int shiftMins;
        public int ShiftMins
        {
            get { return shiftMins; }
            set { shiftMins = value; OnPropertyChanged("shiftMins"); }
        }

        private int shiftSecs;
        public int ShiftSecs
        {
            get { return shiftSecs; }
            set { shiftSecs = value; OnPropertyChanged("shiftSecs"); }
        }

        private string[] shiftDirection = {"+", "-"};
        public string[] ShiftDirection
        {
            get { return shiftDirection; }
            set { shiftDirection = value; }
        }

        private string selectedShift;
        public string SelectedShift
        {
            get { return selectedShift; }
            set { selectedShift = value; OnPropertyChanged("selectedShift"); }
        }
        
        
        
        
        
        
        
        


        #endregion


        #region Command properties
        public DelegateCommand SelectFilesToExportCommand { get; private set; }
        public DelegateCommand ExportDataFilesCommand { get; private set; }

        private bool notBusy;
        public bool NotBusy
        {
            get { return notBusy; }
            set { notBusy = value; OnPropertyChanged("notBusy"); }
        }

        private string progressText;
        public string ProgressText
        {
            get { return progressText; }
            set { progressText = value; OnPropertyChanged("progressText"); }
        }

        private int progressValue;
        public int ProgressValue
        {
            get { return progressValue; }
            set { progressValue = value; OnPropertyChanged("progressValue"); }
        }                
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
            //NotBusy set to true, export button enabled.
            NotBusy = true;
            // Progress bar text info
            ProgressText = "Ready";
            // default selected linear time shift direction
            SelectedShift = ShiftDirection[0];

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
            ProgressValue = 0;
            ProgressText = "Ready";
            FileSelect.UserFileTypeSelection(SelectFileType);
            ExportDataFilesCommand.RaiseCanExecuteChanged();
        }
        #endregion


        #region Export command implementation
        private bool CanExportDataFiles()
        {
            if ((Header.SerialInput != "" && Header.PositionInput != "") && (FileSelect.MultipleFiles != null || FileSelect.MemFile != null))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private async void OnExportDataFiles()
        {
            NotBusy = false;
            ProgressText = "Exporting Client Data";

            FileSelect.SaveTargetDir(SelectFileType, SerialInput, PositionInput);

            var progress = new Progress<int>(i => ProgressValue = i);

            await clientDataAsync(progress);

            ExportDataFilesCommand.RaiseCanExecuteChanged();
            NotBusy = true;          
        }


        private Task clientDataAsync(IProgress<int> progress)
        {
            return Task.Run(() =>
                {
                    try
                    {

                        IEnumerable<Models.IDataRecord> readInputFiles;

                        if (SelectFileType == Models.FileTypes.Memory)
                        {
                            readInputFiles = new Models.MemoryReader(SelectedDirectory, FileSelect.MemFile, SelectFileType);
                        }
                        else
                        {
                            readInputFiles = new Models.SROReader(SelectedDirectory, FileSelect.MultipleFiles, SelectFileType);
                        }

                        progress.Report(25);
                        
                        Models.UnitConverter converter = new Models.UnitConverter();
                        IEnumerable<Models.IDataRecord> convertedRecords = converter.ConvertUnits(readInputFiles, SelectedPresUnit, SelectedTempUnit);

                        progress.Report(50);

                        TimeSpan linearShift = new TimeSpan(ShiftHours, ShiftMins, ShiftSecs);
                        Models.SortAndFilterData filter = new Models.SortAndFilterData();
                        IEnumerable<Models.IDataRecord> filteredRecords = filter.ChooseFilters(convertedRecords, AllDataCb, FilterStartTime, FilterEndTime, SelectedShift, linearShift);

                        progress.Report(75);

                        Models.CsvWriter writer = new Models.CsvWriter();
                        writer.CreateFileWriterStreams(FileSelect.FileSaveName, filteredRecords, Header);                 

                        progress.Report(100);
                        ProgressText = "Export Complete! " + DateTime.Now.ToString("T");
                    }
                    catch (FormatException formEx)
                    {
                        System.Windows.MessageBox.Show("Export Failed! Please select valid data file(s) (CW Duplex or later)");
                        System.Diagnostics.Trace.WriteLine(DateTime.Now + formEx.ToString());
                        progress.Report(0);
                        ProgressText = "Ready";
                        FileSelect.MemFile = null;
                        FileSelect.MultipleFiles = null;
                    }
                    catch(UnauthorizedAccessException accEx)
                    {
                        System.Windows.MessageBox.Show("Export Failed! Please choose a valid filename and save location");
                        System.Diagnostics.Trace.WriteLine(DateTime.Now + accEx.ToString());
                        progress.Report(0);
                        ProgressText = "Ready";
                        FileSelect.MemFile = null;
                        FileSelect.MultipleFiles = null;
                    }
                    catch(Exception genEx)
                    {
                        System.Windows.MessageBox.Show("Export Failed! Email: dave.pollock@exprogroup.com for further support");
                        System.Diagnostics.Trace.WriteLine(DateTime.Now + genEx.ToString());
                        progress.Report(0);
                        ProgressText = "Ready";
                        FileSelect.MemFile = null;
                        FileSelect.MultipleFiles = null;
                    }
                    
                });
            
            
        }
        #endregion







    }
}

