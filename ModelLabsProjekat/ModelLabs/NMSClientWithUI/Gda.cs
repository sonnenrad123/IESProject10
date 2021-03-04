using FTN.Common;
using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMSClientWithUI
{
    public class Gda : IDisposable
    {
        private ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();
        private NetworkModelGDAProxy gdaQueryProxy = null;
		private TextBox outputTb;

		private NetworkModelGDAProxy GdaQueryProxy
		{
			get
			{
				if (gdaQueryProxy != null)
				{
					gdaQueryProxy.Abort();
					gdaQueryProxy = null;
				}

				gdaQueryProxy = new NetworkModelGDAProxy("NetworkModelGDAEndpoint");
				gdaQueryProxy.Open();

				return gdaQueryProxy;
			}
		}

        

        public Gda(TextBox tb)
        {
			outputTb = tb;
        }

		public ResourceDescription GetValues(long resourceId, List<ModelCode> propIds)
        {
			string message = "Getting values method started.";
			CommonTrace.WriteTrace(CommonTrace.TraceError, message);
			ResourceDescription rd = null;

            try
            {
				rd = GdaQueryProxy.GetValues(resourceId, propIds);
				message = "Getting values method successfully finished.";
				CommonTrace.WriteTrace(CommonTrace.TraceError, message);
				return rd;
			}
            catch(Exception e)
            {
				message = string.Format("Getting values method for entered id = {0} failed.\n\t{1}", resourceId, e.Message);
				
				CommonTrace.WriteTrace(CommonTrace.TraceError, message);
				outputTb.Text = message;
				return null;
			}
		}


















		public void Dispose()
        {
			GC.SuppressFinalize(this);
		}

    }
}
