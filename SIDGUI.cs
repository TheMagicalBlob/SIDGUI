using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static sidgui.SIDGUI;

namespace sidgui
{
    public partial class SIDGUI : Form
    {
        /// <summary>
        /// Iniialize the GUI with a preselected script to be loaded immediately.
        /// </summary>
        /// <param name="path"> The path to the DC Script to be loaded on-boot. </param>
        public SIDGUI(string path = null) => main(path);









        //=================================\\
        //--|   Variable Declarations   |--\\
        //=================================\\
        #region [Variable Declarations]

        private Mode ActiveMode = 0;

        enum Mode : byte
        {
            Decode,
            Encode
        }


        /// <summary>
        /// The SIDBase instance to be used for all SID crap
        /// </summary>
        private SIDBase SIDBases;

        private byte InvalidDecoderAttempts = 0;

        /// <summary> If true, show the string representation of the raw SID's instead of UNKNOWN_SID_64 when an id can not be decoded. </summary>
        public static bool ShowUnresolvedSIDs = true;

#if DEBUG
        /// <summary> If true, show the string representation of the raw SID's instead of INVALID_SID_64 when an invalid sid has been provided. </summary>
        public static bool ShowInvalidSIDs = true;
#endif
        #endregion












        //==================================\\
        //--|   Function Declarations   |---\\
        //==================================\\
        #region [Function Delcarations]
#pragma warning disable IDE1006

        private void main(string SIDBasePath)
        {
            InitializeComponent();
            InitializeAdditionalEventHandlers(this);
            InitializeFormDecorations<Panel>(unnamedPanel);
#if DEBUG
            RunStyleConsistencyCheck(this);
#endif
            ForceStyleConsistency(this);

#if DEBUG
            SetToolMode(Mode.Encode);
#else
            SetToolMode(Mode.Decode);
#endif



            // Set global object refs used in various static functions (maybe change that...)
            Refresh();
            Venat = this;
            Update();



            SIDBases = new SIDBase();

            var workingDirectory = Directory.GetCurrentDirectory();

            var expectedSIDBasePaths = new[]
            {
                workingDirectory,
                workingDirectory + @"\sid",
                workingDirectory + @"\sid1",
                workingDirectory + @"\..",
                workingDirectory + @"\..\sid",
                workingDirectory + @"\..\sid1",
            };

            for(var i = 0; i < expectedSIDBasePaths.Length; i++)
            {
                var path = expectedSIDBasePaths[i] + @"\sidbase.bin";

                if (File.Exists(path))
                {
                    echo($"Loaded sidbase.bin from {path}.\nSize: {File.ReadAllBytes(path).Length}\nSIDBase Type: {"UNIMPLEMENTED"}");
                    SIDBases.LoadSIDBase(path);
                }
            }

            if (SIDBases.LoadedSIDBaseCount < 1)
            {
                DecoderOutputBox.Text = "An sidbase.bin is required to decode strings. Please select one with the Load sidbase button.";
            }
        }
#pragma warning restore IDE1006


        private void SetToolMode(Mode Mode)
        {
            if (Mode == ActiveMode)
            {
                return;
            }


            ActiveMode = Mode;

            if (ActiveMode == Mode.Encode)
            {
                DecoderModeBtn.FlatAppearance.BorderColor = Color.Red;
                EncoderModeBtn.FlatAppearance.BorderColor = Color.Green;

                ProcessEntryBtn.Text = "Encode";
                EncoderOutputBox.Visible = !(DecoderOutputBox.Visible = false);
            }
            else if (ActiveMode == Mode.Decode)
            {
                DecoderModeBtn.FlatAppearance.BorderColor = Color.Green;
                EncoderModeBtn.FlatAppearance.BorderColor = Color.Red;

                ProcessEntryBtn.Text = "Decode";
                DecoderOutputBox.Visible = !(EncoderOutputBox.Visible = false);
            }


        }
#endregion (function declarations)











        //======================================\\
        //--|   Event Handler Declarations   |--\\
        //======================================\\
        #region [Event Handler Declarations]
        
        private void EnterDecoderMode(object _, EventArgs __)
        {
            SetToolMode(Mode.Decode);
        }


        private void EnterEncoderMode(object _, EventArgs __)
        {
            SetToolMode(Mode.Encode);
        }


        private void ProcessEntryBtn_Click(object _, EventArgs __)
        {
            if (ActiveMode == Mode.Encode)
            {
                EncoderOutputBox.AppendLine($"{EntryBox.Text} -> " + new SID(EntryBox.Text).EncodedSID);      
            }
            else if (ActiveMode == Mode.Decode)
            {
                if (SIDBases == null)
                {
                    if (++InvalidDecoderAttempts >= 5)
                    {
                        InvalidDecoderAttempts = 0;
                        MessageBox.Show($"Please load an sidbase.bin to be able to decode string ids.");
                    }
                    return;
                }
                DecoderOutputBox.AppendLine($"{EntryBox.Text} -> " + new SID(SIDBases, EntryBox.Text).DecodedSID);
            }
            else {
                echo($"Unknown mode active ({(byte) ActiveMode:X})");
            }
        }


        private void InfoBtn_Click(object sender, EventArgs e)
        {

        }


        private void EntryBoxEnterButtonPressed(object Sender, PreviewKeyDownEventArgs Event)
        {
            if (Event.KeyCode == Keys.Enter && ((TextBox) Sender).Text.Trim().Length > 0)
            {
                ProcessEntryBtn_Click(null, null);
            }
        }


        private void ClearActiveOutputBtn_Click(object sender, EventArgs e)
        {
            if (ActiveMode == Mode.Encode)
            {
                EncoderOutputBox.Clear();
            }
            else if (ActiveMode == Mode.Decode)
            {
                DecoderOutputBox.Clear();
            }
        }

        private void LoadSIDBaseBtn_Click(object sender, EventArgs e)
        {
            using (var fileDialogue = new OpenFileDialog()
            {
                Title = "Select a 64-bit-sid sidbase.bin.",
                Filter = "NaughtyDog SIDBase|*.bin",
                CheckFileExists = true,
                ValidateNames = true
            })
            {
                DialogResult result;

                if ((result = fileDialogue.ShowDialog()) == DialogResult.OK)
                {
                    if (!SIDBases.LoadSIDBase(fileDialogue.FileName))
                    {
                        MessageBox.Show($"Unable to load sidbase at provided path.\n{fileDialogue.FileName}", "Error");
                    }
                }
            }
        }


        private void DebugClearSIDBasesBtn_Click(object sender, EventArgs e)
        {
            SIDBases?.UnloadSIDBases();
        }
        #endregion
    }














    //==================================\\
    //--|   SID Class Declaration   |---\\
    //==================================\\

    /// <summary>
    /// Small class used for handling string id's in a bit more of a convenient manner.
    /// </summary>
    public class SID
    {
        //#
        //## Instance Initializers
        //#

        /// <summary>
        /// Create a new SID instance from a provided byte array, and attempt to decode the id.
        /// </summary>
        /// <param name="EncodedSID"> The encoded ulong string id, converted to a byte array. </param>
        private SID(SIDBase SIDBase, byte[] EncodedSID)
        {
            DecodedSID = SIDBase.DecodeSIDHash(EncodedSID);
            this.EncodedSID = BitConverter.ToString(EncodedSID).Replace("-", string.Empty);

            RawSID = BitConverter.ToUInt64(EncodedSID, 0);
        }


        /// <summary>
        /// Create a new SID instance from a provided ulong hash, and attempt to decode the id.
        /// </summary>
        /// <param name="EncodedSID"> The encoded ulong string id. </param>
        private SID(SIDBase SIDBase, ulong EncodedSID)
        {
            var EncodedSIDArray = BitConverter.GetBytes(EncodedSID);

            this.DecodedSID = SIDBase.DecodeSIDHash(EncodedSIDArray);
            this.EncodedSID = EncodedSID.ToString("X").PadLeft(16, '0');

            RawSID = EncodedSID;
        }


        /// <summary>
        /// Create a new SID instance from a provided sid string, and attempt to decode the id.
        /// </summary>
        /// <param name="EncodedSID"> The encoded string id as a sting. </param>
        public SID(SIDBase SIDBase, string EncodedSID)
        {
            if (!ulong.TryParse(EncodedSID.StartsWith("0x") ? EncodedSID.Substring(2) : EncodedSID, System.Globalization.NumberStyles.HexNumber, null, out var rawSID))
            {
                echo($"Invalid encoded id provided- unable to parse value. {nameof(RawSID)} will be zero");
                rawSID = 0;
            }


            this.DecodedSID = SIDBase.DecodeSIDHash(BitConverter.GetBytes(rawSID));
            this.EncodedSID = EncodedSID;

            RawSID = rawSID;
        }




        /// <summary>
        /// Create a new SID instance from an uncoded string
        /// </summary>
        /// <param name="StringToEncode"></param>
        public SID(string StringToEncode)
        {
            SID id = SIDBase.EncodeSIDHash(StringToEncode);

            if (id.DecodedSID != StringToEncode)
            {
                throw new InvalidDataException($"ERROR: {nameof(DecodedSID)} in SID intance returned by {nameof(SIDBase.EncodeSIDHash)} did not match the originally-provided string for encoding.\n\n[STRINGS]\nProvided: {StringToEncode}\nReturned: {id.DecodedSID}");
            }

            DecodedSID = id.DecodedSID;
            EncodedSID = id.EncodedSID;

            RawSID = id.RawSID;
        }


        /// <summary>
        /// Create a new SID instance from pre-existing decoded and encoded values
        /// </summary>
        public SID(string DecodedSID, string EncodedSID)
        {
            if (!ulong.TryParse(EncodedSID.StartsWith("0x") ? EncodedSID.Substring(2) : EncodedSID, System.Globalization.NumberStyles.HexNumber, null, out var rawSID))
            {
                echo($"Invalid encoded id provided- unable to parse value. {nameof(RawSID)} will be zero");
                rawSID = 0;
            }


            this.DecodedSID = DecodedSID;
            this.EncodedSID = EncodedSID;

            RawSID = rawSID;
        }



        /// <summary>
        /// Create a new SID instance from pre-existing decoded and encoded values
        /// </summary>
        public SID(string decodedSID, ulong encodedSID)
        {
            DecodedSID = decodedSID;
            EncodedSID = encodedSID.ToString("X");

            RawSID = encodedSID;
        }







        //#
        //## VARIABLE DECLARATIONS
        //#
        #region [Variable Declarations]

        /// <summary>
        /// The decoded string id.
        /// <br/><br/>
        /// If the id cannot be decoded, it will return either: 
        /// <br/> - the encoded ulong sid's string representation
        /// <br/> OR
        /// <br/> - UNKNOWN_SID_64
        /// <br/><br/>
        /// Depending on whether the ShowUnresolvedSIDs option is enabled or disabled respectively
        /// </summary>
        public string DecodedSID
        {
            get {
                if (_decodedID == "UNKNOWN_SID_64" && ShowUnresolvedSIDs)
                {
                    return EncodedSID;
                }
#if DEBUG
                else if (_decodedID == "INVALID_SID_64" && ShowInvalidSIDs)
                {
                    return EncodedSID;
                }
#endif

                return _decodedID;
            }

            set => _decodedID = value;
        }
        private string _decodedID;



        /// <summary>
        /// The string representation of the encoded ulong string id.
        /// </summary>
        public string EncodedSID { get; set; }



        /// <summary>
        /// The raw ulong version of the encoded string id. (used for hardcoded checks in code)
        /// </summary>
        public ulong RawSID { get; set; }



        /// <summary>
        /// Represents an item with an unspecified name.
        /// </summary>
        public static readonly SID Empty = new SID(string.Empty, 0x0000000000000000);
        #endregion
    }





















    //======================================\\
    //--|   SIDBase Class Declaration   |---\\
    //======================================\\



    /// <summary> 
    /// Used for decoding any encoded string id's found.
    /// </summary>
    public class SIDBase
    {
        private struct sidbase
        {
            /// <summary>
            /// Initialize a new instance of the SIDBase class, and load the sidbase.bin at the provided <paramref name="SIDBasePath"/>.
            /// <br/>
            /// Additional sidbases can be loaded via the LoadSIDBase(path) function
            /// <br/>
            ///
            /// </summary>
            /// <param name="SIDBasePath"> The path of the sidbase.bin to be loaded for this instance. </param>
            /// <exception cref="FileNotFoundException"> Thrown in the event that Jupiter aligns wi- what the fuck else would it be for. </exception>
            public sidbase(string SIDBasePath)
            {
                // Verify the provided path before proceeding
                if (!File.Exists(SIDBasePath))
                {
                    throw new FileNotFoundException("The file at the provided path does not exist, please ensure that you're not a complete moron.");
                }


                var rawSIDBase = File.ReadAllBytes(SIDBasePath);
                if (rawSIDBase.Length < 0x18)
                {
    #if DEBUG
                    throw new InvalidDataException($"ERROR: Invalid length for sidbase.bin (0x{rawSIDBase.Length:X} < 0x18; is it corrupted?)");
    #else
                    MessageBox.Show($"ERROR: Invalid length for sidbase.bin (0x{rawSIDBase.Length:X} < 0x18; is it corrupted?)", "The provided sidbase was unable to be loaded.");
    #endif
                }



                // Check the initial value to see whether it's a community-built sidbase, or one from NaughtyDog's own modern games-
                // then, read the table length to get the expected size of the hash table (don't really need it anymore)
                var magic = BitConverter.ToInt32(rawSIDBase, 0);

                if (magic < 0x10)
                {
                    echo("Naughty Dog SIDBase detected");
                    
                    SIDBaseVersionMaybe = magic;
                    IsNDSIDBase = true;

                    HashTableRawLength = BitConverter.ToInt32(rawSIDBase, 8) * 16;
                }
                else {
                    echo("Community SIDBase detected");

                    SIDBaseVersionMaybe = -1;

                    IsNDSIDBase = false;
                    HashTableRawLength = magic * 16;
                }






                // Just-In-Case.
                if (HashTableRawLength >= int.MaxValue)
                {
    #if DEBUG
                    throw new InvalidDataException($"ERROR: Sidbase is too large for 64-bit addresses, blame Microsoft for limiting me to that, then blame me for not bothering to try splitting the sidbases. Do that yourself.");
    #else
                    MessageBox.Show($"ERROR: Sidbase is too large for 64-bit addresses, blame Microsoft for limiting me to that, then blame me for not bothering to try splitting the sidbases.");
    #endif
                }





                // Get a sub-array of the specified length from a larger array of bytes, starting at the specified index.
                byte[] GetSubArray(byte[] array, int index, int length = 8)
                {
                    if (length == 0)
                    {
                        return Array.Empty<byte>();
                    }


                    // Build return array.
                    for (var ret = new byte[length]; ; ret[length - 1] = array[index + (length-- - 1)])
                    {
                        if (length <= 0)
                        {
                            return ret;
                        }
                    }
                }


                SIDHashTable = GetSubArray(rawSIDBase, IsNDSIDBase ? 0x10 : 0x08, HashTableRawLength);
                SIDStringTable = GetSubArray(rawSIDBase, SIDHashTable.Length + (IsNDSIDBase ? 0x10 : 0x08), rawSIDBase.Length - (HashTableRawLength + (IsNDSIDBase ? 0x10 : 0x08)));
            }








            //#
            //## VARIABLE DECLARATIONS
            //#

            /// <summary>
            /// The Lookup table of the sidbase, containing the hashes & their decoded string pointers, the latter of which get adjusted to be used with the string table.
            /// </summary>
            public readonly byte[] SIDHashTable;


            /// <summary>
            /// The raw, null-separated string data of the sidbase.
            /// </summary>
            public readonly byte[] SIDStringTable;


            /// <summary>
            /// The length of the sidbase.bin's lookup table (in bytes)<br/>
            /// </summary>
            public readonly int HashTableRawLength;



            public readonly bool IsNDSIDBase;


            /// <summary>
            /// Value read from first 8 bytes. Not entirely sure what it is, maybe file magic or sidbase version. Lazily going with the latter.
            /// <br/><br/>
            /// 
            /// -1: ND Games Modding Community (no version)
            /// 
            /// Anything Else: Naughty Dog
            /// </summary>
            public long SIDBaseVersionMaybe;
        }








        //#
        //## VARIABLE DECLARATIONS
        //#

        /// <summary>
        /// List of sidbase struct instances for the active sidbase.bin lookup tables.
        /// </summary>
        private sidbase[] SIDBases;


        /// <summary>
        /// The amount of sidbases that have been loaded in to the current instance. (all loaded sidbases are checked in the order they're added until the desired SID has been found)
        /// </summary>
        public int LoadedSIDBaseCount => SIDBases?.Length ?? 0;






        //#
        //## FUNCTION DECLARATIONS
        //#
//#pragma warning disable IDE0011 // aDd BrAcEs

        /// <summary>
        /// Load a new sidbase from the path provided, adding it to the list of SIDBases to search through. <br/>
        ///
        /// </summary>
        /// <param name="SIDBasePath"> The path of the SIDBase to be loaded. </param>
        public bool LoadSIDBase(string SIDBasePath)
        {
            if (File.Exists(SIDBasePath))
            {
                if (SIDBases == null || SIDBases.Length < 1)
                {
                    // Load it and create the lookup table list
                    SIDBases = new[] { new sidbase(SIDBasePath) };
                }
                else {
                    // Load it and add it to the list of previously-loaded lookup tables
                    SIDBases = SIDBases.Concat(new[] { new sidbase(SIDBasePath) }).ToArray();
                }



                // Project-specific lines
                {
                    var browseBtn = sidgui.SIDGUI.Venat.Controls.Find("LoadSIDBaseBtn", true).FirstOrDefault();
                    if ((browseBtn ?? default) != default)
                    {
                        browseBtn.Text = "Load another SIDBase";
                    }

                    var decoderOutputBox = sidgui.SIDGUI.Venat.Controls.Find("DecoderOutputBox", true).FirstOrDefault();
                    if (decoderOutputBox.Text == "An sidbase.bin is required to decode strings. Please select one with the Load sidbase button.")
                    {
                        decoderOutputBox.Text = "";
                    }
                }

                return true;
            }
            else {
                return false;
            }
        }


        public void UnloadSIDBases()
        {
            SIDBases = Array.Empty<sidbase>();

            // Project-specific line
            var browseBtn = sidgui.SIDGUI.Venat.Controls.Find("LoadSIDBaseBtn", true).FirstOrDefault();
            if ((browseBtn ?? default) != default)
            {
                browseBtn.Text = "Load an SIDBase";
            }

            var decoderOutputBox = sidgui.SIDGUI.Venat.Controls.Find("DecoderOutputBox", true).FirstOrDefault();
            decoderOutputBox.Text = "An sidbase.bin is required to decode strings. Please select one with the Load sidbase button.";
        }



        /// <summary>
        /// Attempt to decode a provided 64-bit FNV-1a hash via a provided lookup file (sidbase.bin)
        /// </summary>
        /// <param name="BytesToDecode"> The hash to decode, as an array of bytes </param>
        /// <exception cref="IndexOutOfRangeException"> Thrown in the event of an invalid string pointer read from the sidbase after the provided hash is located. </exception>
        private string LookupSID(sidbase SIDBase, byte[] BytesToDecode)
        {
            if (BytesToDecode.Length == 8)
            {
                ulong
                    currentHash,
                    expectedHash
                ;
                int
                    previousAddress = 0xBADBEEF, // Used for checking whether the hash could not be decoded
                    scanAddress = SIDBase.HashTableRawLength / 2,
                    currentRange = scanAddress
                ;


                expectedHash = BitConverter.ToUInt64(BytesToDecode, 0);


                // check whether or not the chunk can be evenly split; if not, check
                // the odd one out for the expected hash, then exclude it and continue as normal if it isn't a match.
                if (((SIDBase.HashTableRawLength >> 4) & 1) == 1)
                {
                    var checkedHash = BitConverter.ToUInt64(SIDBase.SIDHashTable, SIDBase.HashTableRawLength - 0x10);

                    if (checkedHash == expectedHash)
                    {
                        scanAddress = SIDBase.HashTableRawLength - 0x10;
                        goto readString;
                    }

                    scanAddress = currentRange -= 8;
                }


                while (true)
                {
                    // Adjust the address to maintain alignment
                    if (((scanAddress >> 4) & 1) == 1)
                    {
                        if (BitConverter.ToUInt64(SIDBase.SIDHashTable, scanAddress) == expectedHash)
                        {
                            goto readString;
                        }

                        scanAddress -= 0x10;
                    }
                    if (((currentRange >> 4) & 1) == 1)
                    {
                        currentRange += 0x10;
                    }


                    currentHash = BitConverter.ToUInt64(SIDBase.SIDHashTable, scanAddress);

                    if (expectedHash < currentHash)
                    {
                        scanAddress -= currentRange / 2;
                        currentRange /= 2;
                    }
                    else if (expectedHash > currentHash)
                    {
                        scanAddress += currentRange / 2;
                        currentRange /= 2;
                    }
                    else {
                        break;
                    }



                    // Handle missing sid's.
                    if (scanAddress == previousAddress)
                    {
                        return "UNKNOWN_SID_64";
                    }

                    previousAddress = scanAddress;
                }






                //#
                //## Read the string pointer
                //#
                
            readString:
                // Get the string pointer for the encoded sid, read from the proceeding 8 bytes
                var stringPtr = (int) BitConverter.ToInt64(SIDBase.SIDHashTable, scanAddress + 8);


                // Adjust the string pointer to account for the lookup table being a separate array, and table length being removed (not needed in NaughtyDog's SIDBases)
                if (!SIDBase.IsNDSIDBase)
                {
                    stringPtr -= SIDBase.HashTableRawLength + 8;
                }
                

                if (stringPtr >= SIDBase.SIDStringTable.Length)
                {
                    throw new IndexOutOfRangeException($"ERROR: Invalid Pointer Read for String Data!\n    str* 0x{stringPtr:X} >= len 0x{SIDBase.SIDHashTable.Length + SIDBase.SIDStringTable.Length + 8:X}.");
                }

                
                
                // Parse and add the string to the array
                var stringBuffer = string.Empty;

                while (SIDBase.SIDStringTable[stringPtr] != 0)
                {
                    stringBuffer += Encoding.UTF8.GetString(SIDBase.SIDStringTable, (int)stringPtr++, 1);
                }


                return stringBuffer;
            }
            else {
                echo($"Invalid SID provided; unexpected length of \"{BytesToDecode?.Length ?? 0}\". Must be 8 bytes.");
                return "INVALID_SID_64";
            }
        }



        /// <summary>
        /// Encode an <paramref name="inputString"/> as a 64-bit FNV-1a hash, and return the string representation of said hash in both endians
        /// </summary>
        /// <param name="inputString"> The string to encode. </param>
        /// <returns> An SID instance containing the hashed string in both endians. </returns>
        public static SID EncodeSIDHash(string inputString)
        {
            // Hash input string
            var hash = 14695981039346656037ul;
            var prime = 1099511628211ul;
            var inputLen = inputString?.Length ?? 0;

            for (var i = 0; i < inputLen; ++i)
            {
                hash ^= inputString[i];
                hash *= prime;
            }

            return new SID(inputString, BitConverter.ToUInt64(BitConverter.GetBytes(hash), 0).ToString("X").PadLeft(16, '0').PadRight(16, '0'));
        }




        /// <summary>
        /// Search all loaded sidbases for the provided hash.
        /// </summary>
        /// <param name="EncodedSID"></param>
        /// <returns>The decoded string version of the provided <paramref name="EncodedSID"/> if found. Otherwise, returns UNKNOWN_SID_64 (or INVALID_SID_64 if the format was invalid).</returns>
        public string DecodeSIDHash(byte[] EncodedSID)
        {
            var id = "(No SIDBases Loaded.)";

            foreach (var table in SIDBases ?? Array.Empty<sidbase>())
            {
                id = LookupSID(table, EncodedSID) ?? "(Null SIDBase Instance!!!)";

                if (id != "UNKNOWN_SID_64")
                {
                    break;
                }
            }

            return id;
        }



        private static void echo(object message = null)
        {
#if DEBUG
            string str;

            Console.WriteLine(str = message?.ToString() ?? string.Empty);

            if (!Console.IsInputRedirected)
            {
                Debug.WriteLine(str);
            }
#endif
        }
    }
}
