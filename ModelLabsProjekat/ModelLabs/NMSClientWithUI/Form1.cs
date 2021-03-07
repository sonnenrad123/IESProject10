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
        public long GetRelatedValuesInputGid = -1;
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
            this.GetExtentValuesPanel.Visible = false;
            this.GetValuesBtn2.Enabled = false;
            this.GetExtentValBtn2.Enabled = false;
            this.GetRelatedValuesPanel.Visible = false;
        }

        private void GetValueBtn_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.GetValuesPanel.Visible = true;
            this.GetValuesBtn2.Enabled = false;
            this.GetValuesComboBox.Items.Clear();
            Gda gd = new Gda(this.outputTB);
            Dictionary<string, long> ret = gd.GetGids();
            foreach(string name in ret.Keys)
            {
                this.GetValuesComboBox.Items.Add(name + "-" + String.Format("0x{0:x16}", ret[name]));
            }
           
        }

        private void CheckGidBtn_Click(object sender, EventArgs e)
        {
            long gid = -1;
            this.ModelCodesList.Items.Clear();
            this.outputTB.Text = "";
            try
            {

                string strId = "";
                if (this.GetValuesComboBox.SelectedItem != null)
                {
                    strId = this.GetValuesComboBox.SelectedItem.ToString().Split('-')[1];
                }

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

                    if (xmlWriter != null)
                    {
                        xmlWriter.Close();
                        outputTB.Text = File.ReadAllText(Config.Instance.ResultDirecotry + "\\GetValues_Results.xml");
                    }

                }
                catch (Exception ex)
                {
                    message = string.Format("Getting values method for entered id = {0} failed.\n\t{1}",GetValuesInputGid, ex.Message);
                    this.outputTB.Text = message;
                    CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                }
                finally
                {
                    
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

        private void BckToMMBtn_Click(object sender, EventArgs e)
        {
            //this.GiDTb.Text = "";
            this.ModelCodesList.Items.Clear();
            this.outputTB.Text = "";
            this.GetValuesPanel.Visible = false;
            this.MainPanel.Visible = true;
            this.GiDGRVtb.Text = "";
            this.ModelCodesGEVCB.Items.Clear();
            this.ModelCodesListGevClb.Items.Clear();
            this.OutPutTBGev.Text = "";
            this.referencePropertyGrvCB.Items.Clear();
            this.ModelCodeFilterGRVCB.Items.Clear();
        }

        private void ExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetExtentValuesBtn_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.GetExtentValuesPanel.Visible = true;
            this.GetExtentValBtn2.Enabled = false;
            foreach (DMSType dt in modelResourcesDesc.AllDMSTypes)
            {
                if(dt != DMSType.MASK_TYPE)
                {
                    ModelCode temp = modelResourcesDesc.GetModelCodeFromType(dt);
                    this.ModelCodesGEVCB.Items.Add(temp.ToString());
                }
            }
            
        }

        private void ModelCodesListGevClb_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(this.ModelCodesListGevClb.CheckedItems.Count >= 1 || e.NewValue == CheckState.Checked)
            {
                this.GetExtentValBtn2.Enabled = true;
            }

            if(this.ModelCodesListGevClb.CheckedItems.Count == 1 && e.NewValue == CheckState.Unchecked)
            {
                this.GetExtentValBtn2.Enabled = false;
            }
        }

        private void ModelCodesGEVCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.GetExtentValBtn2.Enabled = false;
                this.ModelCodesListGevClb.Items.Clear();
                ModelCode selMC;
                if (Enum.TryParse(this.ModelCodesGEVCB.Items[ModelCodesGEVCB.SelectedIndex].ToString(), out selMC))
                {
                    List<ModelCode> properties = modelResourcesDesc.GetAllPropertyIds(selMC);
                    foreach (ModelCode prop in properties)
                    {
                        this.ModelCodesListGevClb.Items.Add(prop.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "There was a error. Exception messagge: " + ex.Message;
                this.OutPutTBGev.Text = message;
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            }
        }

        private void BckToMMBtn2_Click(object sender, EventArgs e)
        {
            this.ModelCodesGEVCB.Items.Clear();
            this.ModelCodesListGevClb.Items.Clear();
            this.OutPutTBGev.Text = "";
            this.GetExtentValuesPanel.Visible = false;
            this.MainPanel.Visible = true;
        }

        private void GetExtentValBtn2_Click(object sender, EventArgs e)
        {
            Gda gd = new Gda(this.OutPutTBGev);
            List<ModelCode> chckProp = new List<ModelCode>();
            ModelCode selMC;
            if (Enum.TryParse(this.ModelCodesGEVCB.Items[ModelCodesGEVCB.SelectedIndex].ToString(), out selMC))
            {
                try
                {
                    foreach (string mc in this.ModelCodesListGevClb.CheckedItems)
                    {
                        ModelCode mcenum;
                        if (Enum.TryParse(mc, out mcenum))
                        {
                            chckProp.Add(mcenum);
                        }
                    }
                    if (Enum.TryParse(this.ModelCodesGEVCB.Items[ModelCodesGEVCB.SelectedIndex].ToString(), out selMC))
                    {
                        gd.GetExtentValues(selMC, chckProp);

                    }
                    OutPutTBGev.Text = File.ReadAllText(Config.Instance.ResultDirecotry + "\\GetExtentValues_Results.xml");
                }
                catch (Exception ex)
                {
                    string message = string.Format("Getting extent values method failed for {0}.\n\t{1}", selMC, ex.Message);
                    this.OutPutTBGev.Text = message;
                    CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                }
                finally
                {
                    
                }
            }

        }

        private void GetRelatedValuesBtn_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.GetRelatedValuesPanel.Visible = true;
            this.GetRelatedValuesBtn2.Enabled = false;
            foreach (DMSType dt in modelResourcesDesc.AllDMSTypes)
            {
                if (dt != DMSType.MASK_TYPE)
                {
                    ModelCode temp = modelResourcesDesc.GetModelCodeFromType(dt);
                    this.ModelCodeFilterGRVCB.Items.Add(temp.ToString());
                }
            }

            this.GRVCBGid.Items.Clear();
            Gda gd = new Gda(this.outputTB);
            Dictionary<string, long> ret = gd.GetGids();
            foreach (string name in ret.Keys)
            {
                this.GRVCBGid.Items.Add(name + "-" + String.Format("0x{0:x16}", ret[name]));
            }

        }








        private void GetRelatedValuesPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CheckGidRVBtn_Click(object sender, EventArgs e)
        {

            long gid = -1;
            this.referencePropertyGrvCB.Items.Clear();
            this.OutputTBGrv.Text = "";
            try
            {

                string strId = "";
                if (this.GRVCBGid.SelectedItem != null)
                {
                    strId = this.GRVCBGid.SelectedItem.ToString().Split('-')[1];
                }

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
                this.GetRelatedValuesInputGid = gid;
                short type = ModelCodeHelper.ExtractTypeFromGlobalId(gid);
                List<ModelCode> properties = modelResourcesDesc.GetPropertyIds((DMSType)type, PropertyType.Reference);
                List<ModelCode> properties2 = modelResourcesDesc.GetPropertyIds((DMSType)type, PropertyType.ReferenceVector);
                foreach (ModelCode prop in properties)
                {
                    this.referencePropertyGrvCB.Items.Add(prop.ToString());
                }
                foreach (ModelCode prop in properties2)
                {
                    this.referencePropertyGrvCB.Items.Add(prop.ToString());
                }
                this.OutputTBGrv.Text = "Please, from dropdown choose reference property now.";
                //TODO:tek sa selektovanjem filtera mogu razmisliti da dodam druge opcije
                this.ModelCodesListGRV.Items.Add(ModelCode.IDOBJ_ALIAS.ToString());
                this.ModelCodesListGRV.Items.Add(ModelCode.IDOBJ_GID.ToString());
                this.ModelCodesListGRV.Items.Add(ModelCode.IDOBJ_MRID.ToString());
                this.ModelCodesListGRV.Items.Add(ModelCode.IDOBJ_NAME.ToString());
            }
            catch (Exception ex)
            {
                string message = "There was a error. Exception messagge: " + ex.Message;
                this.OutputTBGrv.Text = message;
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            }
        }

        private void referencePropertyGrvCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.ModelCodesListGRV.CheckedItems.Count >= 1) && this.referencePropertyGrvCB.SelectedItem != null)
            {
                this.GetRelatedValuesBtn2.Enabled = true;
            }
            this.OutputTBGrv.Text = "";
        }

        private void GetRelatedValuesBtn2_Click(object sender, EventArgs e)
        {
            Gda gd = new Gda(this.OutputTBGrv);
            Association association = new Association();
            
            try
            {
                ModelCode propertyID;
                ModelCode type;
                if (Enum.TryParse(this.referencePropertyGrvCB.SelectedItem.ToString(), out propertyID))
                {
                    if (this.ModelCodeFilterGRVCB.SelectedItem != null)
                    {
                        if (Enum.TryParse(this.ModelCodeFilterGRVCB.SelectedItem.ToString(), out type))
                        {
                            association.PropertyId = propertyID;
                            association.Type = type;
                            List<ModelCode> chckProp = new List<ModelCode>();
                            foreach (string mc in this.ModelCodesListGRV.CheckedItems)
                            {
                                ModelCode mcenum;
                                if (Enum.TryParse(mc, out mcenum))
                                {
                                    chckProp.Add(mcenum);
                                }
                            }
                            gd.GetRelatedValues(this.GetRelatedValuesInputGid, chckProp, association);
                        }
                    }
                    else
                    {
                        association.PropertyId = propertyID;
                        List<ModelCode> chckProp = new List<ModelCode>();
                        foreach (string mc in this.ModelCodesListGRV.CheckedItems)
                        {
                            ModelCode mcenum;
                            if (Enum.TryParse(mc, out mcenum))
                            {
                                chckProp.Add(mcenum);
                            }
                        }
                        gd.GetRelatedValues(this.GetRelatedValuesInputGid, chckProp, association);
                    }
                }
                this.OutputTBGrv.Text = File.ReadAllText(Config.Instance.ResultDirecotry + "\\GetRelatedValues_Results.xml");
                if(this.OutputTBGrv.Text == "")
                {
                    this.OutputTBGrv.Text = "There is no reference that you are searching for. Please check your input again.";
                }
            }
            catch(Exception ex)
            {
                string message = "There was a error. Exception messagge: " + ex.Message;
                this.OutputTBGrv.Text = message;
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
            finally
            {
                this.GiDGRVtb.Text = "";
                this.referencePropertyGrvCB.Items.Clear();
                this.ModelCodeFilterGRVCB.Items.Clear();
                this.ModelCodesListGRV.Items.Clear();
                this.GetRelatedValuesBtn2.Enabled = false;
                foreach (DMSType dt in modelResourcesDesc.AllDMSTypes)
                {
                    if (dt != DMSType.MASK_TYPE)
                    {
                        ModelCode temp = modelResourcesDesc.GetModelCodeFromType(dt);
                        this.ModelCodeFilterGRVCB.Items.Add(temp.ToString());
                    }
                }
            }
        }


        private void ModelCodeFilterGRVCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.GetRelatedValuesBtn2.Enabled = false;
                this.ModelCodesListGRV.Items.Clear();
                ModelCode selMC;
                if (Enum.TryParse(this.ModelCodeFilterGRVCB.Items[ModelCodeFilterGRVCB.SelectedIndex].ToString(), out selMC))
                {
                    List<ModelCode> properties = modelResourcesDesc.GetAllPropertyIds(selMC);
                    foreach (ModelCode prop in properties)
                    {
                        this.ModelCodesListGRV.Items.Add(prop.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "There was a error. Exception messagge: " + ex.Message;
                this.OutPutTBGev.Text = message;
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            }
        }

        private void ModelCodesListGRV_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if ((this.ModelCodesListGRV.CheckedItems.Count >= 1 || e.NewValue == CheckState.Checked) && this.referencePropertyGrvCB.SelectedItem!=null)
            {
                this.GetRelatedValuesBtn2.Enabled = true;
            }

            if (this.ModelCodesListGRV.CheckedItems.Count == 1 && e.NewValue == CheckState.Unchecked)
            {
                this.GetRelatedValuesBtn2.Enabled = false;
            }
        }

        private void BackTMMBtn3_Click(object sender, EventArgs e)
        {
            //this.GiDTb.Text = "";
            this.ModelCodesList.Items.Clear();
            this.outputTB.Text = "";
            this.OutputTBGrv.Text = "";
            this.GetValuesPanel.Visible = false;
            this.MainPanel.Visible = true;
            this.GiDGRVtb.Text = "";
            this.ModelCodesGEVCB.Items.Clear();
            this.ModelCodesListGevClb.Items.Clear();
            this.OutPutTBGev.Text = "";
            this.referencePropertyGrvCB.Items.Clear();
            this.ModelCodeFilterGRVCB.Items.Clear();
            this.ModelCodesListGRV.Items.Clear();
            this.MainPanel.Visible = true;
            this.GetRelatedValuesPanel.Visible = false;

        }

       
    }
}
