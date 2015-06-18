using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace VaultAtlas.FlacAtlas
{
    public class DriveInformation
    {
        // this api uesed to send string messege
        // to media control interface decice (mci)
        // like The cd rom
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        public static extern int mciSendString(string lpstrCommand,
            string lpstrReturnString, int uReturnLength, int hwndCallback);

        // this api used to get information about
        // a drive ex: its name, seial number
        // if this function return zero means that
        // one of the information could not be retrived
        // so if it is a CD ROM drive and we can't
        // obtain its name ---> CD ROM is empty
        [DllImport("kernel32.dll", EntryPoint = "GetVolumeInformationA")]
        public static extern int GetVolumeInformation(string lpRootPathName,
            StringBuilder lpVolumeNameBuffer, int nVolumeNameSize,
            ref int lpVolumeSerialNumber, int lpMaximumComponentLength,
            int lpFileSystemFlags, string lpFileSystemNameBuffer,
            int nFileSystemNameSize);

        //this api get the drive type (0:unknown,1:invalid path,
        //  2:removable(floppy,removabledisk),3:fixed(hard disk),
        //  4:remote(network drive),5:CDROM,6: RAM disk)
        [DllImport("kernel32.dll", EntryPoint = "GetDriveTypeA")]
        public static extern int GetDriveType(string nDrive);

        public string LogDrive { get; private set; }

        public DriveInformation(string logDrive)
        {
            var volumeName = new StringBuilder(256);
            var serialNumber = new int();

            LogDrive = logDrive;
            VolumeType = (VolumeType)GetDriveType(logDrive);

            if (VolumeType == VolumeType.Unknown)
                return;

            var result = GetVolumeInformation(logDrive, volumeName, 256, ref serialNumber, new int(), new int(), "", 256);
            MediumPresent = result != 0;

            if (MediumPresent)
            {
                // a CD
                VolumeName = volumeName.ToString();
                SerialNumber = serialNumber.ToString("x");
            }
        }

        public bool MediumPresent { get; private set; }

        public string VolumeName { get; private set; }

        public string SerialNumber { get; private set; }

        public VolumeType VolumeType { get; private set; }

        public string DisplayName
        {
            get { return string.Format("{0} ({1}) ({2})", LogDrive, VolumeType, VolumeName); }
        }

        public static IEnumerable<DriveInformation> GetDriveInformation()
        {
            var logDrives = System.IO.Directory.GetLogicalDrives();
            return logDrives.Select(logDrive => new DriveInformation(logDrive))
                .Where(di => di.VolumeType == VolumeType.CDROM && di.MediumPresent);
        }

    }


    public enum VolumeType
    {
        Unknown = 0,
        InvalidPath = 1,
        Removable = 2,
        Fixed = 3,
        Remote = 4,
        CDROM = 5,
        RAM = 6
    }

}