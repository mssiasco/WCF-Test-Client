using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Main.Tools.TestClient
{
	// Token: 0x0200001C RID: 28
	internal static class ResourceHelper
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002DE2 File Offset: 0x00000FE2
		internal static Bitmap AboutBoxImage
		{
			get
			{
				if (ResourceHelper.theAboutBoxImage == null)
				{
					ResourceHelper.theAboutBoxImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.AboutBox.bmp"));
				}
				return ResourceHelper.theAboutBoxImage;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002E04 File Offset: 0x00001004
		internal static Icon ApplicationIcon
		{
			get
			{
				if (ResourceHelper.theAppIcon == null)
				{
					ResourceHelper.theAppIcon = new Icon(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.Service.ico"));
				}
				return ResourceHelper.theAppIcon;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002E26 File Offset: 0x00001026
		internal static Bitmap ArrowDownImage
		{
			get
			{
				if (ResourceHelper.theArrowDownImage == null)
				{
					ResourceHelper.theArrowDownImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.ArrowDown.bmp"));
					ResourceHelper.theArrowDownImage.MakeTransparent();
				}
				return ResourceHelper.theArrowDownImage;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002E52 File Offset: 0x00001052
		internal static Bitmap ArrowUpImage
		{
			get
			{
				if (ResourceHelper.theArrowUpImage == null)
				{
					ResourceHelper.theArrowUpImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.ArrowUp.bmp"));
					ResourceHelper.theArrowUpImage.MakeTransparent();
				}
				return ResourceHelper.theArrowUpImage;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00002E7E File Offset: 0x0000107E
		internal static Bitmap ContractImage
		{
			get
			{
				if (ResourceHelper.theContractImage == null)
				{
					ResourceHelper.theContractImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.Contract.bmp"));
					ResourceHelper.theContractImage.MakeTransparent();
				}
				return ResourceHelper.theContractImage;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002EAA File Offset: 0x000010AA
		internal static Bitmap EndpointImage
		{
			get
			{
				if (ResourceHelper.theEndpointImage == null)
				{
					ResourceHelper.theEndpointImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.Endpoint.bmp"));
					ResourceHelper.theEndpointImage.MakeTransparent();
				}
				return ResourceHelper.theEndpointImage;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002ED6 File Offset: 0x000010D6
		internal static Bitmap ErrorImage
		{
			get
			{
				if (ResourceHelper.theErrorImage == null)
				{
					ResourceHelper.theErrorImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.Error.bmp"));
					ResourceHelper.theErrorImage.MakeTransparent();
				}
				return ResourceHelper.theErrorImage;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002F02 File Offset: 0x00001102
		internal static Bitmap FileImage
		{
			get
			{
				if (ResourceHelper.theFileImage == null)
				{
					ResourceHelper.theFileImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.File.bmp"));
					ResourceHelper.theFileImage.MakeTransparent();
				}
				return ResourceHelper.theFileImage;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00002F2E File Offset: 0x0000112E
		internal static Bitmap OperationImage
		{
			get
			{
				if (ResourceHelper.theOperationImage == null)
				{
					ResourceHelper.theOperationImage = new Bitmap(ResourceHelper.LoadResourceStream("WcfTestClientEx.Resources.Operation.bmp"));
					ResourceHelper.theOperationImage.MakeTransparent();
				}
				return ResourceHelper.theOperationImage;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006370 File Offset: 0x00004570
		private static Stream LoadResourceStream(string resourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			return executingAssembly.GetManifestResourceStream(resourceName);
		}

		// Token: 0x04000058 RID: 88
		private static Bitmap theAboutBoxImage;

		// Token: 0x04000059 RID: 89
		private static Icon theAppIcon;

		// Token: 0x0400005A RID: 90
		private static Bitmap theArrowDownImage;

		// Token: 0x0400005B RID: 91
		private static Bitmap theArrowUpImage;

		// Token: 0x0400005C RID: 92
		private static Bitmap theContractImage;

		// Token: 0x0400005D RID: 93
		private static Bitmap theEndpointImage;

		// Token: 0x0400005E RID: 94
		private static Bitmap theErrorImage;

		// Token: 0x0400005F RID: 95
		private static Bitmap theFileImage;

		// Token: 0x04000060 RID: 96
		private static Bitmap theOperationImage;
	}
}
