using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    public class BreakerImporter
    {
		private static BreakerImporter bImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;

		#region Properties
		public static BreakerImporter Instance
		{
			get
			{
				if (bImporter == null)
				{
					lock (singletoneLock)
					{
						if (bImporter == null)
						{
							bImporter = new BreakerImporter();
							bImporter.Reset();
						}
					}
				}
				return bImporter;
			}
		}

		public Delta NMSDelta
		{
			get
			{
				return delta;
			}
		}

		#endregion Properties

		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing Breaker Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing Breaker Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			ImportBreakers();
			ImportDayTypes();
			ImportSeasons();
			ImportRegulatingControls();
			ImportSwitchSchedules();
			ImportRegulationSchedules();
			ImportRegularTimePoints();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}


		#region Import

		private void ImportBreakers()
        {
			SortedDictionary<string, object> cimBreakers = concreteModel.GetAllObjectsOfType("FTN.Breaker");
			if(cimBreakers != null)
            {
				foreach(KeyValuePair<string, object> cimBreakerPair in cimBreakers)
                {
					FTN.Breaker cimBreaker = cimBreakerPair.Value as FTN.Breaker;

					ResourceDescription rd = CreateBreakerResourceDescription(cimBreaker);

					if(rd != null)
                    {
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Breaker ID = ").Append(cimBreaker.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
                    else
                    {
						report.Report.Append("Breaker ID = ").Append(cimBreaker.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
            }

		}

		private ResourceDescription CreateBreakerResourceDescription(FTN.Breaker cimBreaker)
        {
			ResourceDescription rd = null;
			if(cimBreaker != null)
            {
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BRKR, importHelper.CheckOutIndexForDMSType(DMSType.BRKR));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBreaker.ID, gid);

				BreakerConverter.PopulateBreakerProperties(cimBreaker, rd);

            }

			return rd;
        }

		private void ImportDayTypes()
        {
			SortedDictionary<string, object> cimDayTypes = concreteModel.GetAllObjectsOfType("FTN.DayType");
			if(cimDayTypes != null)
            {
				foreach (KeyValuePair<string, object> cimDayTypePair in cimDayTypes)
                {
					FTN.DayType cimDayType = cimDayTypePair.Value as FTN.DayType;
					ResourceDescription rd = CreateDayTypeResourceDescription(cimDayType);
					if(rd != null)
                    {
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("DayType ID = ").Append(cimDayType.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
                    else
                    {
						report.Report.Append("DayType ID = ").Append(cimDayType.ID).AppendLine(" FAILED to be converted");
					}

				}

				report.Report.AppendLine();
            }

		}

		private ResourceDescription CreateDayTypeResourceDescription(FTN.DayType cimDayType)
        {
			ResourceDescription rd = null;

			if(cimDayType != null)
            {
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DAYTYPE, importHelper.CheckOutIndexForDMSType(DMSType.DAYTYPE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimDayType.ID, gid);

				BreakerConverter.PopulateDayTypeProperties(cimDayType, rd);
            }

			return rd;
        }

		private void ImportSeasons()
        {
			SortedDictionary<string, object> cimSeasons = concreteModel.GetAllObjectsOfType("FTN.Season");
			if(cimSeasons != null)
            {
				foreach (KeyValuePair<string, object> cimSeasonPair in cimSeasons)
                {
					FTN.Season cimSeason = cimSeasonPair.Value as FTN.Season;
					ResourceDescription rd = CreateSeasonResourceDescription(cimSeason);
					if(rd != null)
                    {
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Season ID = ").Append(cimSeason.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
                    else
                    {
						report.Report.Append("Season ID = ").Append(cimSeason.ID).AppendLine(" FAILED to be converted");
					}
                }
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateSeasonResourceDescription(FTN.Season cimSeason)
        {
			ResourceDescription rd = null;
			if(cimSeason != null)
            {
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SEASON, importHelper.CheckOutIndexForDMSType(DMSType.SEASON));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSeason.ID, gid);

				BreakerConverter.PopulateSeasonProperties(cimSeason, rd);
            }
			return rd;
        }

		private void ImportRegulatingControls()
        {
			SortedDictionary<string, object> cimRegulatingControls = concreteModel.GetAllObjectsOfType("FTN.RegulatingControl");
			if(cimRegulatingControls != null)
            {
				foreach(KeyValuePair<string, object> cimRegulatingControlPair in cimRegulatingControls)
                {
					FTN.RegulatingControl cimRegulatingControl = cimRegulatingControlPair.Value as FTN.RegulatingControl;

					ResourceDescription rd = CreateRegulatingControlResourceDescription(cimRegulatingControl);
					if(rd != null)
                    {
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegulatingControl ID = ").Append(cimRegulatingControl.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
                    else
                    {
						report.Report.Append("RegulatingControl ID = ").Append(cimRegulatingControl.ID).AppendLine(" FAILED to be converted");
					}
                }
				report.Report.AppendLine();
            }

		}

		private ResourceDescription CreateRegulatingControlResourceDescription(FTN.RegulatingControl cimRegulatingControl)
        {
			ResourceDescription rd = null;

			if(cimRegulatingControl != null)
            {
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGCONTROL, importHelper.CheckOutIndexForDMSType(DMSType.REGCONTROL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegulatingControl.ID, gid);

				BreakerConverter.PopulateRegulatingControlProperties(cimRegulatingControl, rd);
            }
			return rd;
        }

		private void ImportSwitchSchedules()
        {
			SortedDictionary<string, object> cimSwitchSchedules = concreteModel.GetAllObjectsOfType("FTN.SwitchSchedule");
			if(cimSwitchSchedules != null)
            {
				foreach (KeyValuePair<string, object> cimSwitchSchedulePair in cimSwitchSchedules)
                {
					FTN.SwitchSchedule cimSwitchSchedule = cimSwitchSchedulePair.Value as FTN.SwitchSchedule;
					ResourceDescription rd = CreateSwitchScheduleResourceDescription(cimSwitchSchedule);
					if(rd!= null)
                    {
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("SwitchSchedule ID = ").Append(cimSwitchSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
                    else
                    {
						report.Report.Append("SwitchSchedule ID = ").Append(cimSwitchSchedule.ID).AppendLine(" FAILED to be converted");
					}
                }
				report.Report.AppendLine();
			}

		}

		private ResourceDescription CreateSwitchScheduleResourceDescription(FTN.SwitchSchedule cimSwitchSchedule)
        {
			ResourceDescription rd = null;
			if(cimSwitchSchedule != null)
            {
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWSCH, importHelper.CheckOutIndexForDMSType(DMSType.SWSCH));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSwitchSchedule.ID, gid);

				BreakerConverter.PopulateSwitchScheduleProperties(cimSwitchSchedule, rd, importHelper, report);
            }
			return rd;
        }

		private void ImportRegulationSchedules()
        {
			SortedDictionary<string, object> cimRegulationSchedules = concreteModel.GetAllObjectsOfType("FTN.RegulationSchedule");
			if(cimRegulationSchedules != null)
            {
				foreach (KeyValuePair<string, object> cimRegulationSchedulePair in cimRegulationSchedules)
                {
					FTN.RegulationSchedule cimRegulationSchedule = cimRegulationSchedulePair.Value as FTN.RegulationSchedule;
					ResourceDescription rd = CreateRegulationScheduleResourceDescription(cimRegulationSchedule);
					if(rd != null)
                    {
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegulationSchedule ID = ").Append(cimRegulationSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
                    else
                    {
						report.Report.Append("RegulationSchedule ID = ").Append(cimRegulationSchedule.ID).AppendLine(" FAILED to be converted");
					}
                }
				report.Report.AppendLine();

			}

		}

		private ResourceDescription CreateRegulationScheduleResourceDescription(FTN.RegulationSchedule cimRegulationSchedule)
        {
			ResourceDescription rd = null;
			if(cimRegulationSchedule != null)
            {
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGSCH, importHelper.CheckOutIndexForDMSType(DMSType.REGSCH));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegulationSchedule.ID, gid);

				BreakerConverter.PopulateRegulationScheduleProperties(cimRegulationSchedule, rd, importHelper, report);
            }
			return rd;
        }

		private void ImportRegularTimePoints()
        {
			SortedDictionary<string, object> cimRegularTimePoints = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
            if (cimRegularTimePoints != null)
            {
				foreach (KeyValuePair<string, object> cimRegularTimePointPair in cimRegularTimePoints)
                {
					FTN.RegularTimePoint cimRegularTimePoint = cimRegularTimePointPair.Value as FTN.RegularTimePoint;

					ResourceDescription rd = CreateRegularTimePointResourceDescription(cimRegularTimePoint);
					if(rd != null)
                    {
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
                    else
                    {
						report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).AppendLine(" FAILED to be converted");
					}
                }
				report.Report.AppendLine();
			}

		}

		private ResourceDescription CreateRegularTimePointResourceDescription(FTN.RegularTimePoint cimRegularTimePoint)
        {
			ResourceDescription rd = null;
            if (cimRegularTimePoint != null)
            {
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.RTP, importHelper.CheckOutIndexForDMSType(DMSType.RTP));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegularTimePoint.ID, gid);

				BreakerConverter.PopulateRegularTimePointProperties(cimRegularTimePoint, rd, importHelper, report);
            }
			return rd;
        }

		#endregion Import

	}
}
