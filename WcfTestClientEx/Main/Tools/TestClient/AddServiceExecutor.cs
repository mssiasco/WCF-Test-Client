using System;
using System.ComponentModel;
using System.Globalization;
using WcfTestClientEx;

namespace Main.Tools.TestClient
{
	// Token: 0x02000003 RID: 3
	internal class AddServiceExecutor
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000046C8 File Offset: 0x000028C8
		internal AddServiceOutputs Execute(AddServiceInputs inputs, Workspace workspace, BackgroundWorker addServiceWorker)
		{
			AddServiceOutputs addServiceOutputs = new AddServiceOutputs();
			if (inputs != null && inputs.EndpointsCount > 0)
			{
				float num = 0f;
				float num2 = 100f / (float)inputs.EndpointsCount;
				foreach (string text in inputs.Endpoints)
				{
					Uri uri;
					if (Uri.TryCreate(text, UriKind.Absolute, out uri))
					{
						string text2;
						ServiceProject serviceProject = workspace.AddServiceProject(text, addServiceWorker, num, num2, out text2);
						if (serviceProject != null)
						{
							addServiceOutputs.AddServiceProject(serviceProject);
							ApplicationSettings.GetInstance().RecordUrl(uri.AbsoluteUri);
							addServiceOutputs.IncrementSucceedCount();
						}
						else if (!string.IsNullOrEmpty(text2))
						{
							addServiceOutputs.AddError(text2);
						}
					}
					else
					{
						addServiceOutputs.AddError(string.Format(CultureInfo.CurrentUICulture, StringResources.ErrorInvalidUrl, text));
					}
					num += num2;
				}
			}
			return addServiceOutputs;
		}
	}
}
