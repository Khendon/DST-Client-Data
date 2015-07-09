using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Diagnostics;

namespace SRO_Management.ViewModels
{
    /// <summary>
    /// View model for application main window. 
    /// Inherits from BinadableBase purely for INotifyPropertyChanged implementation.
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        #region privates Fields
        private Models.HeaderModel header;

        private Models.FileSelectModel fileSelect;

        private CancellationTokenSource cancelTokenSource;

        private Models.TempUnitSelection selectedTempUnit;

        private bool allDataCb;

        private Models.FileTypes selectFileType;

        private DateTime? filterStartTime = null;

        private Models.PresUnitSelection selectedPresUnit;

        private DateTime? filterEndTime = null;

        private int shiftHours;

        private int shiftMins;

        private int shiftSecs;

        private string[] shiftDirection = { "+", "-" };

        private string selectedShift;

        private bool notBusy;

        private string progressText;

        private int progressValue;

        // Error Traces
        private TextWriterTraceListener traceListener;
        private System.IO.FileStream traceLogFile;
        #endregion

        public MainWindowViewModel()
        {
            // Instantiate client file header model object
            this.header = new Models.HeaderModel();

            // Instantiate file selection and dir path model
            this.fileSelect = new Models.FileSelectModel();

            // Default Pressure Units Combobox
            this.SelectedPresUnit = Models.PresUnitSelection.psia;

            // Default Temp Units Combobox
            this.SelectedTempUnit = Models.TempUnitSelection.degC;

            // All Data checkbox default value
            this.AllDataCb = true;

            // NotBusy set to true, export button enabled.
            this.NotBusy = true;

            // Progress bar text info
            this.ProgressText = "Ready";

            // Default selected linear time shift direction
            this.SelectedShift = this.ShiftDirection[0];

            this.CancelCommand = new DelegateCommand(this.OnCancel, this.CanCancel);

            this.ExportDataFilesCommand = new DelegateCommand(this.OnExportDataFiles, this.CanExportDataFiles);

            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var directoryCreate = System.IO.Directory.CreateDirectory(appDataFolder + "/Expro/DST Client Data");

            var logFilePath = System.IO.Path.Combine(appDataFolder, "Expro/DST Client Data/DST_Exporter_ErrorLog.txt");
            this.traceLogFile = new System.IO.FileStream(logFilePath, System.IO.FileMode.Append);
            this.traceListener = new TextWriterTraceListener(this.traceLogFile);

            Trace.Listeners.Add(this.traceListener);
        }

        #region File Header Labels (Properties)
        public string ClientLabel
        {
            get 
            { 
                return this.header.ClientLabel; 
            }

            set 
            {
                this.header.ClientLabel = value; 
                this.OnPropertyChanged("ClientLabel"); 
            }
        }

        public string WellLabel
        {
            get 
            { 
                return this.header.WellLabel; 
            }

            set 
            {
                this.header.WellLabel = value;
                this.OnPropertyChanged("WellLabel"); 
            }
        }

        public string DstLabel
        {
            get 
            { 
                return this.header.DstLabel; 
            }

            set 
            {
                this.header.DstLabel = value; 
                this.OnPropertyChanged("DstLabel"); 
            }
        }

        public string SerialLabel
        {
            get 
            { 
                return this.header.SerialLabel; 
            }

            set 
            {
                this.header.SerialLabel = value; 
                this.OnPropertyChanged("SerialLabel"); 
            }
        }

        public string DepthLabel 
        {
            get 
            { 
                return this.header.DepthLabel; 
            }

            set 
            {
                this.header.DepthLabel = value; 
                this.OnPropertyChanged("DepthLabel"); 
            }
        }

        public string Position
        {
            get 
            {
                return this.header.PositionLabel;
            }

            set 
            {
                this.header.PositionLabel = value;
                this.OnPropertyChanged("PositionLabel"); 
            }
        }

        public string PresStringFormat 
        {
            get 
            {
                return this.header.PresStringFormat; 
            }

            set 
            {
                this.header.PresStringFormat = value; 
                this.OnPropertyChanged("PresStringFormat"); 
            } 
        }

        public string TempStringFormat
        {
            get 
            { 
                return this.header.TempStringFormat;
            }

            set 
            {
                this.header.TempStringFormat = value; 
                this.OnPropertyChanged("TempStringFormat"); 
            } 
        }
        #endregion

        #region File Header User Inputs (Properties)
        public string ClientInput
        {
            get 
            { 
                return this.header.ClientInput; 
            }

            set 
            {
                this.header.ClientInput = value; 
                this.OnPropertyChanged("ClientInput"); 
            }
        }

        public string WellInput
        {
            get 
            { 
                return this.header.WellInput; 
            }

            set 
            {
                this.header.WellInput = value; 
                this.OnPropertyChanged("WellInput"); 
            }
        }

        public string DstInput
        {
            get 
            {
                return this.header.DstInput; 
            }

            set 
            {
                this.header.DstInput = value; 
                this.OnPropertyChanged("DstInput"); 
            }
        }

        public string SerialInput
        {
            get 
            { 
                return this.header.SerialInput; 
            }

            set 
            {
                this.header.SerialInput = value; 
                this.OnPropertyChanged("SerialInput");
                this.ExportDataFilesCommand.RaiseCanExecuteChanged(); 
            }
        }

        public string DepthInput
        {
            get 
            { 
                return this.header.DepthInput; 
            }

            set 
            {
                this.header.DepthInput = value; 
                this.OnPropertyChanged("DepthInput"); 
            }
        }

        public string PositionInput
        {
            get 
            { 
                return this.header.PositionInput; 
            }

            set 
            {
                this.header.PositionInput = value; 
                this.OnPropertyChanged("PositionInput");
                this.ExportDataFilesCommand.RaiseCanExecuteChanged(); 
            }
        }
        #endregion

        #region Path of currently selected directory (Property)
        public string SelectedDirectory
        {
            get 
            { 
                return this.fileSelect.DirPath; 
            }

            set 
            {
                this.fileSelect.DirPath = value; 
                this.OnPropertyChanged("DirPath"); 
            }
        }
        #endregion

        #region Filter groupbox (Properties)
        public bool AllDataCb
        {
            get 
            {
                return this.allDataCb;
            }

            set 
            {
                this.allDataCb = value;                 
                this.OnPropertyChanged("AllDataCb");
            }
        }

        public Models.FileTypes SelectFileType
        {
            get 
            {
                return this.selectFileType; 
            }

            set 
            {
                this.selectFileType = value;
                this.fileSelect.MemFile = null;
                this.fileSelect.MultipleFiles = null;
                this.OnPropertyChanged("SelectFileType");
                this.ExportDataFilesCommand.RaiseCanExecuteChanged();
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
        public Models.PresUnitSelection SelectedPresUnit
        {
            get 
            {
                return this.selectedPresUnit; 
            }

            set
            {
                this.selectedPresUnit = value;
                this.PresStringFormat = this.selectedPresUnit.ToString();
                this.OnPropertyChanged("SelectedPresUnit");
            }
        }

        // Populate combobox with pressure unit enum values.
        public IEnumerable<Models.PresUnitSelection> PresUnitValuesComboBox
        {
            get
            {
                return Enum.GetValues(typeof(Models.PresUnitSelection))
                    .Cast<Models.PresUnitSelection>();
            }
        }

        // Bound to temp unit combobox
        public Models.TempUnitSelection SelectedTempUnit
        {
            get 
            {
                return this.selectedTempUnit; 
            }

            set
            {
                this.selectedTempUnit = value;
                this.TempStringFormat = this.selectedTempUnit.ToString();
                this.OnPropertyChanged("SelectedTempUnit");
            }
        }

        // Populate combobox with temp unit enum values.
        public IEnumerable<Models.TempUnitSelection> TempUnitValueComboBox
        {
            get 
            {
                return Enum.GetValues(typeof(Models.TempUnitSelection))
                    .Cast<Models.TempUnitSelection>();
            }
        }

        public DateTime? FilterStartTime
        {
            get 
            { 
                return this.filterStartTime; 
            }

            set 
            {
                this.filterStartTime = value; 
                this.OnPropertyChanged("FilterStartTime"); 
            }
        }

        public DateTime? FilterEndTime
        {
            get 
            { 
                return this.filterEndTime; 
            }

            set 
            {
                this.filterEndTime = value; 
                this.OnPropertyChanged("FilterEndTime"); 
            }
        }

        public int ShiftHours
        {
            get 
            { 
                return this.shiftHours; 
            }

            set 
            {
                this.shiftHours = value; 
                this.OnPropertyChanged("shiftHours"); 
            }
        }

        public int ShiftMins
        {
            get 
            { 
                return this.shiftMins;
            }

            set 
            {
                this.shiftMins = value; 
                this.OnPropertyChanged("shiftMins"); 
            }
        }

        public int ShiftSecs
        {
            get 
            { 
                return this.shiftSecs; 
            }

            set 
            {
                this.shiftSecs = value; 
                this.OnPropertyChanged("shiftSecs"); 
            }
        }

        public string[] ShiftDirection
        {
            get { return this.shiftDirection; }

            set { this.shiftDirection = value; }
        }

        public string SelectedShift
        {
            get 
            { 
                return this.selectedShift; 
            }

            set 
            {
                this.selectedShift = value; 
                this.OnPropertyChanged("selectedShift"); 
            }
        }
        #endregion

        #region Commands (Properties)
        public DelegateCommand CancelCommand { get; private set; }

        public DelegateCommand ExportDataFilesCommand { get; private set; }

        public bool NotBusy
        {
            get
            {
                return this.notBusy;
            }

            set 
            {
                this.notBusy = value; 
                this.OnPropertyChanged("notBusy"); 
            }
        }

        public string ProgressText
        {
            get 
            { 
                return this.progressText; 
            }

            set 
            {
                this.progressText = value; 
                this.OnPropertyChanged("progressText"); 
            }
        }

        public int ProgressValue
        {
            get 
            { 
                return this.progressValue;
            }

            set 
            {
                this.progressValue = value; 
                this.OnPropertyChanged("progressValue"); 
            }
        }                
        #endregion       

        #region Cancel Command Implementation
        private bool CanCancel()
        {
            return true;
        }

        private void OnCancel()
        {
            if (this.cancelTokenSource != null)
            {
                this.cancelTokenSource.Cancel();
            }

            this.ProgressValue = 0;

            this.ProgressText = "Export Cancelled";            
        }
        #endregion

        #region Export command implementation
        private bool CanExportDataFiles()
        {
            return true;
        }

        private async void OnExportDataFiles()
        {
            this.cancelTokenSource = new CancellationTokenSource();

            var progress = new Progress<int>(i => this.ProgressValue = i);

            this.fileSelect.UserFileTypeSelection(this.SelectFileType);

            this.NotBusy = false;

            this.fileSelect.SaveTargetDir(this.SelectFileType, this.SerialInput, this.PositionInput);

            if ((this.SelectedDirectory != null) && (this.fileSelect.FileSaveName != null))
            {
                this.ProgressText = "Exporting Client Data";
                await this.ClientDataAsync(progress, this.cancelTokenSource.Token); 
            }

            this.ExportDataFilesCommand.RaiseCanExecuteChanged();
            this.NotBusy = true;          
        }

        private Task ClientDataAsync(IProgress<int> progress, CancellationToken cancelToken)
        {
            return Task.Run(() =>
                {
                    try
                    {
                        IEnumerable<Models.IDataRecord> readInputFiles;

                        IEnumerable<Models.IDataRecord> convertedRecords;

                        IEnumerable<Models.IDataRecord> filteredRecords;

                        if (SelectFileType == Models.FileTypes.Memory)
                        {
                            readInputFiles = new Models.MemReader(SelectedDirectory, fileSelect.MemFile);
                        }
                        else
                        {
                            readInputFiles = new Models.SROReader(SelectedDirectory, fileSelect.MultipleFiles);
                        }

                        if (cancelToken.IsCancellationRequested)
                        {
                            throw new TaskCanceledException();
                        }

                        progress.Report(25);

                        if ((SelectedPresUnit != Models.PresUnitSelection.psia) || (SelectedTempUnit != Models.TempUnitSelection.degC))
                        {
                            Models.UnitConverter converter = new Models.UnitConverter();
                            convertedRecords = converter.ConvertUnits(readInputFiles, SelectedPresUnit, SelectedTempUnit);

                            progress.Report(50);

                            TimeSpan linearShift = new TimeSpan(ShiftHours, ShiftMins, ShiftSecs);
                            Models.SortAndFilterData filter = new Models.SortAndFilterData();
                            filteredRecords = filter.ChooseFilters(convertedRecords, AllDataCb, FilterStartTime, FilterEndTime, SelectedShift, linearShift);
                        }
                        else
                        {
                            progress.Report(50);
                            TimeSpan linearShift = new TimeSpan(ShiftHours, ShiftMins, ShiftSecs);
                            Models.SortAndFilterData filter = new Models.SortAndFilterData();
                            filteredRecords = filter.ChooseFilters(readInputFiles, AllDataCb, FilterStartTime, FilterEndTime, SelectedShift, linearShift);
                        }

                        progress.Report(75);

                        if (cancelToken.IsCancellationRequested)
                        {
                            throw new TaskCanceledException();
                        }

                        Models.CsvWriter writer = new Models.CsvWriter();
                        writer.CreateFileWriterStreams(fileSelect.FileSaveName, filteredRecords, header);

                        progress.Report(100);
                        ProgressText = "Export Complete! " + DateTime.Now.ToString("T");
                    }
                    catch (FormatException formEx)
                    {
                        System.Windows.MessageBox.Show("Export Failed! Please select valid data file(s) (DST v3.1 or later)");
                        System.Diagnostics.Trace.WriteLine(DateTime.Now + "," + formEx.ToString() + "\n");
                        System.Diagnostics.Trace.Flush();
                        progress.Report(0);
                        ProgressText = "Ready";
                        fileSelect.MemFile = null;
                        fileSelect.MultipleFiles = null;
                    }
                    catch (UnauthorizedAccessException accEx)
                    {
                        System.Windows.MessageBox.Show("Export Failed! Please choose a valid file and/or save location");
                        System.Diagnostics.Trace.WriteLine(DateTime.Now + "," +  accEx.ToString() + "\n");
                        System.Diagnostics.Trace.Flush();
                        progress.Report(0);
                        ProgressText = "Ready";
                        fileSelect.MemFile = null;
                        fileSelect.MultipleFiles = null;
                    }
                    catch (TaskCanceledException canEx)
                    {
                        System.Diagnostics.Trace.WriteLine(DateTime.Now + "," + canEx.ToString() + "\n");
                        System.Diagnostics.Trace.Flush();
                        progress.Report(0);
                        fileSelect.MemFile = null;
                        fileSelect.MultipleFiles = null;
                    }
                    catch (Exception genEx)
                    {
                        System.Windows.MessageBox.Show("Export Failed! Email: dave.pollock@exprogroup.com for further support");
                        System.Diagnostics.Trace.WriteLine(DateTime.Now + "," + genEx.ToString() + "\n");
                        System.Diagnostics.Trace.Flush();
                        progress.Report(0);
                        ProgressText = "Ready";
                        fileSelect.MemFile = null;
                        fileSelect.MultipleFiles = null;
                    }                    
                });           
        }
        #endregion
                
    }
}