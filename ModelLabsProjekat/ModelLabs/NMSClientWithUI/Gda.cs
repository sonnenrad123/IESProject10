using FTN.Common;
using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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

		public List<long> GetExtentValues(ModelCode modelCode,List<ModelCode> propIds)
		{
			string message = "Getting extent values method started.";
			CommonTrace.WriteTrace(CommonTrace.TraceError, message);

			XmlTextWriter xmlWriter = null;
			int iteratorId = 0;
			List<long> ids = new List<long>();

			try
			{
				int numberOfResources = 2;
				int resourcesLeft = 0;

				iteratorId = GdaQueryProxy.GetExtentValues(modelCode, propIds);
				resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);


				xmlWriter = new XmlTextWriter(Config.Instance.ResultDirecotry + "\\GetExtentValues_Results.xml", Encoding.Unicode);
				xmlWriter.Formatting = Formatting.Indented;

				while (resourcesLeft > 0)
				{
					List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

					for (int i = 0; i < rds.Count; i++)
					{
						ids.Add(rds[i].Id);
						rds[i].ExportToXml(xmlWriter);
						xmlWriter.Flush();
					}

					resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
				}

				GdaQueryProxy.IteratorClose(iteratorId);

				message = "Getting extent values method successfully finished.";
				Console.WriteLine(message);
				CommonTrace.WriteTrace(CommonTrace.TraceError, message);

			}
			catch (Exception e)
			{
				message = string.Format("Getting extent values method failed for {0}.\n\t{1}", modelCode, e.Message);
				Console.WriteLine(message);
				CommonTrace.WriteTrace(CommonTrace.TraceError, message);
			}
			finally
			{
				if (xmlWriter != null)
				{
					xmlWriter.Close();
				}
			}

			return ids;
		}

		public List<long> GetRelatedValues(long sourceGlobalId,List<ModelCode> propIds, Association association)
		{
			string message = "Getting related values method started.";
			Console.WriteLine(message);
			CommonTrace.WriteTrace(CommonTrace.TraceError, message);

			List<long> resultIds = new List<long>();


			XmlTextWriter xmlWriter = null;
			int numberOfResources = 2;

			try
			{
				List<ModelCode> properties = propIds;

				int iteratorId = GdaQueryProxy.GetRelatedValues(sourceGlobalId, properties, association);
				int resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);

				xmlWriter = new XmlTextWriter(Config.Instance.ResultDirecotry + "\\GetRelatedValues_Results.xml", Encoding.Unicode);
				xmlWriter.Formatting = Formatting.Indented;

				while (resourcesLeft > 0)
				{
					List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

					for (int i = 0; i < rds.Count; i++)
					{
						resultIds.Add(rds[i].Id);
						rds[i].ExportToXml(xmlWriter);
						xmlWriter.Flush();
					}

					resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
				}

				GdaQueryProxy.IteratorClose(iteratorId);

				message = "Getting related values method successfully finished.";
				Console.WriteLine(message);
				CommonTrace.WriteTrace(CommonTrace.TraceError, message);
			}
			catch (Exception e)
			{
				message = string.Format("Getting related values method  failed for sourceGlobalId = {0} and association (propertyId = {1}, type = {2}). Reason: {3}", sourceGlobalId, association.PropertyId, association.Type, e.Message);
				Console.WriteLine(message);
				CommonTrace.WriteTrace(CommonTrace.TraceError, message);
			}
			finally
			{
				if (xmlWriter != null)
				{
					xmlWriter.Close();
				}
			}

			return resultIds;
		}



		public Dictionary<string,long> GetGids()
		{
			string message = "Getting global Ids method started.";
			CommonTrace.WriteTrace(CommonTrace.TraceError, message);
			Dictionary<string, long> dicIdName = new Dictionary<string, long>();
			
			int iteratorId = 0;
			List<long> ids = new List<long>();

			try
			{
				
				foreach (DMSType dt in modelResourcesDesc.AllDMSTypes)
				{
					if (dt != DMSType.MASK_TYPE)
					{
						ModelCode temp = modelResourcesDesc.GetModelCodeFromType(dt);
						int numberOfResources = 2;
						int resourcesLeft = 0;
						List<ModelCode> properties = modelResourcesDesc.GetAllPropertyIds(temp);
						iteratorId = GdaQueryProxy.GetExtentValues(temp, properties);
						resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);

						while (resourcesLeft > 0)
						{
							List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

							for (int i = 0; i < rds.Count; i++)
							{
								ids.Add(rds[i].Id);
								for(int j = 0; j < rds[i].Properties.Count; j++)
                                {
									if(rds[i].Properties[j].Id.ToString() == "IDOBJ_NAME")
                                    {
										dicIdName.Add(rds[i].Properties[j].AsString(), rds[i].Id);
                                    }
                                }
							}

							resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
						}

						GdaQueryProxy.IteratorClose(iteratorId);
					}
				}


			}
			catch (Exception e)
			{
				throw e;
			}
			

			return dicIdName;
		}














		public void Dispose()
        {
			GC.SuppressFinalize(this);
		}

    }
}
