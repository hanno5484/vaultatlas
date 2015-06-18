using System;
using System.Collections;

using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;

namespace VaultAtlas.FlacAtlas
{
	public class FileTypeImageList
	{
		public FileTypeImageList( )
		{
			this.smalllist.ImageSize = new System.Drawing.Size( 16, 16 );
			this.smalllist.ColorDepth = ColorDepth.Depth32Bit;
			this.largelist.ColorDepth = ColorDepth.Depth32Bit;
			this.largelist.ImageSize = new System.Drawing.Size( 32, 32 );

            this.smalllist.Images.Add(resources.database);
            this.largelist.Images.Add(resources.database_large);
            arr.Add("///DATABASE");
		}

		private ImageList smalllist = new ImageList();
		public ImageList SmallImageList 
		{
			get 
			{
				return this.smalllist;
			}
		}

		private ImageList largelist = new ImageList();
		public ImageList LargeImageList 
		{
			get 
			{
				return this.largelist;
			}
		}
		
		private ArrayList arr = new ArrayList();

		public int GetDirectoryNormalImageIndex( string dir ) 
		{
			int index = arr.IndexOf( "///DIRECTORY"+dir );
			if ( index == -1 )
			{
				this.GetIconFromFilename( dir, conFILE_ATTRIBUTE_DIRECTORY );
				this.arr.Add( "///DIRECTORY"+dir );
				index = this.arr.Count-1;
			}
			return index;

		}

		private const uint conFILE_ATTRIBUTE_DIRECTORY = 0x00000010;  
		public int GetDirectoryOpenImageIndex( string dir  )
		{
			int index = arr.IndexOf( "///DIRECTORYOPEN"+dir );
			if ( index == -1 )
			{
				this.GetIconFromFilename( dir, conFILE_ATTRIBUTE_DIRECTORY );
				this.arr.Add( "///DIRECTORYOPEN"+dir );
				index = this.arr.Count-1;
			}
			return index;

		}

		public int GetFileTypeIndex( string fileName ) 
		{
			string ext = System.IO.Path.GetExtension( fileName ).ToLower();
			int index = ext.Length > 0 ? arr.IndexOf( ext ) : -1;
			if ( index == -1 )
			{
				this.GetIconFromFilename( fileName, 0 );
				this.arr.Add( ext );
				index = this.arr.Count-1;
			}
			return index;
		}

		private void GetIconFromFilename( string fName, uint attr )
		{
			IntPtr hImg;    //the handle to the system image list
			SHFILEINFO shinfo1 = new SHFILEINFO();
			SHFILEINFO shinfo2 = new SHFILEINFO();

				//Use this to get the large Icon
				hImg  = Win32.SHGetFileInfo(fName, attr,
					ref shinfo1, (uint)Marshal.SizeOf(shinfo1),
					Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON | Win32.SHGFI_USEFILEATTRIBUTES );

			System.Drawing.Icon myIcon = (System.Drawing.Icon)(System.Drawing.Icon.FromHandle(shinfo1.hIcon).Clone());
			Win32.DestroyIcon(shinfo1.hIcon);
			this.largelist.Images.Add( myIcon );
		
			hImg  = Win32.SHGetFileInfo(fName, attr, ref shinfo2,
					(uint)Marshal.SizeOf(shinfo2),
					//    Win32.SHGFI_SMALLICON |
					//    Win32.SHGFI_ADDOVERLAYS |
					//    Win32.SHGFI_OVERLAYINDEX |
					Win32.SHGFI_USEFILEATTRIBUTES |
					Win32.SHGFI_SMALLICON |
					Win32.SHGFI_ICON
					//    Win32.SHGFI_TYPENAME |
					//    Win32.SHGFI_DISPLAYNAME |
					// Win32.SHGFI_LINKOVERLAY 
					//    Win32.SHGFI_SYSICONINDEX
					);
			
			System.Drawing.Icon myIcon2 = (System.Drawing.Icon)(System.Drawing.Icon.FromHandle(shinfo2.hIcon).Clone());
			Win32.DestroyIcon(shinfo2.hIcon);
			this.smalllist.Images.Add( myIcon2 );
		}



		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		class Win32
		{
			public const uint SHGFI_ICON          = 0x000000100;
			public const uint SHGFI_DISPLAYNAME       = 0x000000200;
			public const uint SHGFI_TYPENAME      = 0x000000400; 
			public const uint SHGFI_ATTRIBUTES    = 0x000000800;     
			public const uint SHGFI_ICONLOCATION      = 0x000001000;
			public const uint SHGFI_EXETYPE       = 0x000002000; 
			public const uint SHGFI_SYSICONINDEX      = 0x000004000;
			public const uint SHGFI_LINKOVERLAY       = 0x000008000;     
			public const uint SHGFI_SELECTED      = 0x000010000; 
			public const uint SHGFI_ATTR_SPECIFIED    = 0x000020000;
			public const uint SHGFI_LARGEICON     = 0x000000000; 
			public const uint SHGFI_SMALLICON     = 0x000000001;   
			public const uint SHGFI_OPENICON      = 0x000000002;     
			public const uint SHGFI_SHELLICONSIZE     = 0x000000004;
			public const uint SHGFI_PIDL          = 0x000000008; 
			public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
			public const uint SHGFI_ADDOVERLAYS       = 0x000000020;     
			public const uint SHGFI_OVERLAYINDEX      = 0x000000040;
			//  public const uint SHGFI_ICON = 0x100;
			//  public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
			//  public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon
			public const uint ILD_TRANSPARENT = 0x1;

			[DllImport("shell32.dll")]
			public static extern IntPtr SHGetFileInfo(string pszPath,
				uint dwFileAttributes,
				ref SHFILEINFO psfi,
				uint cbSizeFileInfo,
				uint uFlags);

			[DllImport("user32")]
			public static extern int DestroyIcon(IntPtr hIcon);
		}
	}
}
