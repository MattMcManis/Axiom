using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom
{
    public class ViewModel
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Filters
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // -------------------------
        // Deband
        // -------------------------
        // Items
        public static ObservableCollection<string> _cboFilterVideo_Deband_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public static ObservableCollection<string> cboFilterVideo_Deband_Items
        {
            get { return _cboFilterVideo_Deband_Items; }
            set { _cboFilterVideo_Deband_Items = value; }
        }

        // Selected Item
        public static string cboFilterVideo_Deband_SelectedItem { get; set; }


        // -------------------------
        // Deshake
        // -------------------------
        public static ObservableCollection<string> _cboFilterVideo_Deshake_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public static ObservableCollection<string> cboFilterVideo_Deshake_Items
        {
            get { return _cboFilterVideo_Deshake_Items; }
            set { _cboFilterVideo_Deshake_Items = value; }
        }

        // Selected Item
        public static string cboFilterVideo_Deshake_SelectedItem { get; set; }


        // -------------------------
        // Deflicker
        // -------------------------
        public static ObservableCollection<string> _cboFilterVideo_Deflicker_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public static ObservableCollection<string> cboFilterVideo_Deflicker_Items
        {
            get { return _cboFilterVideo_Deflicker_Items; }
            set { _cboFilterVideo_Deflicker_Items = value; }
        }

        public static string cboFilterVideo_Deflicker_SelectedItem { get; set; }


        // -------------------------
        // Dejudder
        // -------------------------
        public static ObservableCollection<string> _cboFilterVideo_Dejudder_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public static ObservableCollection<string> cboFilterVideo_Dejudder_Items
        {
            get { return _cboFilterVideo_Dejudder_Items; }
            set { _cboFilterVideo_Dejudder_Items = value; }
        }

        public static string cboFilterVideo_Dejudder_SelectedItem { get; set; }

        // -------------------------
        // Denoise
        // -------------------------
        public static ObservableCollection<string> _cboFilterVideo_Denoise_Items = new ObservableCollection<string>()
        {
            "disabled",
            "default",
            "light",
            "medium",
            "heavy",
        };
        public static ObservableCollection<string> cboFilterVideo_Denoise_Items
        {
            get { return _cboFilterVideo_Denoise_Items; }
            set { _cboFilterVideo_Denoise_Items = value; }
        }

        public static string cboFilterVideo_Denoise_SelectedItem { get; set; }


        // -------------------------
        // Selective Color
        // -------------------------
        // -------------------------
        // Correction Method
        // -------------------------
        public static ObservableCollection<string> _cboFilterVideo_SelectiveColor_Correction_Method_Items = new ObservableCollection<string>()
        {
            "relative",
            "absolute"
        };
        public static ObservableCollection<string> cboFilterVideo_SelectiveColor_Correction_Method_Items
        {
            get { return _cboFilterVideo_SelectiveColor_Correction_Method_Items; }
            set { _cboFilterVideo_SelectiveColor_Correction_Method_Items = value; }
        }

        public static string cboFilterVideo_SelectiveColor_Correction_Method_SelectedItem { get; set; }

    }
}
