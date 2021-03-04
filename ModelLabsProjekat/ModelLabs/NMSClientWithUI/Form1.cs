using FTN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NMSClientWithUI
{
    public partial class NMSClientProject10 : Form
    {
        public List<ModelCode> modelCodes = new List<ModelCode>();
        private ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();
        public long GetValuesInputGid = -1;

        public NMSClientProject10()
        {
            this.BackgroundImage = Properties.Resources.wp3246614;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MainPanel.Visible = true;
            this.GetValuesPanel.Visible = false;
            this.GetValuesBtn2.Enabled = false;
        }

        private void GetValueBtn_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.GetValuesPanel.Visible = true;
        }

        private void CheckGidBtn_Click(object sender, EventArgs e)
        {
            long gid = -1;
            this.ModelCodesList.Items.Clear();
            this.outputTB.Text = "";
            try
            {

                string strId = this.GiDTb.Text;

                if (strId.StartsWith("0x", StringComparison.Ordinal))
                {
                    strId = strId.Remove(0, 2);
                    CommonTrace.WriteTrace(CommonTrace.TraceVerbose, "Entering globalId successfully ended.");

                    gid = Convert.ToInt64(Int64.Parse(strId, System.Globalization.NumberStyles.HexNumber));
                }
                else
                {
                    CommonTrace.WriteTrace(CommonTrace.TraceVerbose, "Entering globalId successfully ended.");
                    gid = Convert.ToInt64(strId);
                }
                this.GetValuesInputGid = gid;
                short type = ModelCodeHelper.ExtractTypeFromGlobalId(gid);
                List<ModelCode> properties = modelResourcesDesc.GetAllPropertyIds((DMSType)type);
                foreach (ModelCode prop in properties)
                {
                    this.ModelCodesList.Items.Add(prop.ToString());
                }
            }
            catch (Exception ex)
            {
                string message = "There was a error. Exception messagge: " +ex.Message;
                this.outputTB.Text = message;
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
               
            }

        }

        private void GetValuesBtn2_Click(object sender, EventArgs e)
        {
            Gda gd = new Gda(this.outputTB);
            List<ModelCode> chckProp = new List<ModelCode>();
            //List<ModelCode> chckProp = this.ModelCodesList.CheckedItems.OfType<ModelCode>().ToList();
            foreach(string mc in ModelCodesList.CheckedItems)
            {
                ModelCode mcenum;
                if(Enum.TryParse(mc,out mcenum))
                {
                    chckProp.Add(mcenum);
                }
            }
            ResourceDescription rd = gd.GetValues(GetValuesInputGid, chckProp);
            XmlTextWriter xmlWriter = null;
            string message = "";
            if (rd != null)
            {
                try
                {
                    
                    xmlWriter = new XmlTextWriter(Config.Instance.ResultDirecotry + "\\GetValues_Results.xml", Encoding.Unicode);
                    xmlWriter.Formatting = Formatting.Indented;


                    rd.ExportToXml(xmlWriter);
                    xmlWriter.Flush();

                    message = "Getting values method successfully finished.";
                    CommonTrace.WriteTrace(CommonTrace.TraceError, message);

                    

                }
                catch (Exception ex)
                {
                    message = string.Format("Getting values method for entered id = {0} failed.\n\t{1}",GetValuesInputGid, ex.Message);
                    this.outputTB.Text = message;
                    CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                }
                finally
                {
                    if (xmlWriter != null)
                    {
                        xmlWriter.Close();
                        outputTB.Text = File.ReadAllText(Config.Instance.ResultDirecotry + "\\GetValues_Results.xml");
                    }
                }
            }
        }

        
        private void ModelCodesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.ModelCodesList.CheckedItems.Count >= 1 || e.NewValue == CheckState.Checked)
            {
                this.GetValuesBtn2.Enabled = true;
            }

            if(this.ModelCodesList.CheckedItems.Count == 1 && e.NewValue == CheckState.Unchecked)
            {
                this.GetValuesBtn2.Enabled = false;
            }
            
        }

        
    }
}
