
namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    using FTN.Common;
    public class BreakerConverter
    {
        #region Populate ResourceDescription
        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIAS, cimIdentifiedObject.AliasName));
                }
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }
                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
            }
        }

        public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
        {
            if((cimBasicIntervalSchedule != null) && (rd != null))
            {
                BreakerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);
                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_STIME, cimBasicIntervalSchedule.StartTime));
                }
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_VALUNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
            }
        }

        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
        {
            if((cimPowerSystemResource != null) && (rd != null))
            {
                BreakerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if((cimRegularTimePoint != null) && (rd != null))
            {
                BreakerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);
                if (cimRegularTimePoint.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.RTP_SEQNUM, cimRegularTimePoint.SequenceNumber));
                }
                if (cimRegularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.RTP_VAL1, cimRegularTimePoint.Value1));
                }
                if (cimRegularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.RTP_VAL2, cimRegularTimePoint.Value2));

                }
                if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
                    if(gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID\"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.RTP_INTERVALSCH, gid));
                }
            }
        }

        public static void PopulateDayTypeProperties(FTN.DayType cimDayType,ResourceDescription rd)
        {
            if((cimDayType != null) && (rd != null))
            {
                BreakerConverter.PopulateIdentifiedObjectProperties(cimDayType, rd);
            }
        }

        public static void PopulateSeasonProperties(FTN.Season cimSeason, ResourceDescription rd)
        {
            if((cimSeason!= null) && (rd != null))
            {
                BreakerConverter.PopulateIdentifiedObjectProperties(cimSeason, rd);
                if (cimSeason.StartDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEASON_SDATE, cimSeason.StartDate));
                }
                if (cimSeason.EndDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEASON_EDATE, cimSeason.EndDate));
                }
            }
        }

        public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
        {
            if ((cimRegularIntervalSchedule != null) && (rd != null))
            {
                BreakerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);

            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment,ResourceDescription rd)
        {
            if((cimEquipment != null) && (rd != null))
            {
                BreakerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);
                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIP_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIP_NORMINSERVICE, cimEquipment.NormallyInService));
                }
            }
        }

        public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd)
        {
            if((cimRegulatingControl != null) && (rd != null))
            {
                BreakerConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd);
                if (cimRegulatingControl.DiscreteHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_DISCRETE, cimRegulatingControl.Discrete));
                }
                if (cimRegulatingControl.ModeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_MODE, (short)GetDMSRegulatingControlModeKind(cimRegulatingControl.Mode)));
                }
                if (cimRegulatingControl.MonitoredPhaseHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_MONITORPHASE, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));
                }
                if (cimRegulatingControl.TargetRangeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_TARGETR, cimRegulatingControl.TargetRange));
                }
                if (cimRegulatingControl.TargetValueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGCONTROL_TARGETV, cimRegulatingControl.TargetValue));
                }
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
        {
            if((cimConductingEquipment != null) && (rd != null))
            {
                BreakerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
            }
        }

        public static void PopulateSeasonDayTypeSchedule(FTN.SeasonDayTypeSchedule cimSeasonDayTypeSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if((cimSeasonDayTypeSchedule != null) && (rd != null))
            {
                BreakerConverter.PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeSchedule, rd);
                if (cimSeasonDayTypeSchedule.DayTypeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.DayType.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to DayType: rdfID \"").Append(cimSeasonDayTypeSchedule.DayType.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SDTS_DAYTYPE, gid));
                }
                if (cimSeasonDayTypeSchedule.SeasonHasValue)
                {
                    long gid2 = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.Season.ID);
                    if(gid2 < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Season: rdfID \"").Append(cimSeasonDayTypeSchedule.Season.ID).AppendLine("\" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SDTS_SEASON, gid2));
                }
            }
            
        }

        public static void PopulateRegulationScheduleProperties(FTN.RegulationSchedule cimRegulationSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if((cimRegulationSchedule != null) && (rd != null))
            {
                BreakerConverter.PopulateSeasonDayTypeSchedule(cimRegulationSchedule, rd, importHelper, report);
                if (cimRegulationSchedule.RegulatingControlHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegulationSchedule.RegulatingControl.ID);
                    if(gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegulationSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulationSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimRegulationSchedule.RegulatingControl.ID).AppendLine("\" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGSCH_REGCONTROL, gid));
                }
            }
        }

        public static void PopulateSwitchScheduleProperties(FTN.SwitchSchedule cimSwitchSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if((cimSwitchSchedule != null) && (rd != null))
            {
                BreakerConverter.PopulateSeasonDayTypeSchedule(cimSwitchSchedule, rd, importHelper, report);
                if (cimSwitchSchedule.SwitchHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSwitchSchedule.Switch.ID);
                    if(gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSwitchSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Switch: rdfID \"").Append(cimSwitchSchedule.Switch.ID).AppendLine("\" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SWSCH_SWITCH, gid));
                }
            }
        }

        public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd)
        {
            if((cimSwitch != null) && (rd != null))
            {
                BreakerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd);
                if (cimSwitch.NormalOpenHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SW_NORMOPEN, cimSwitch.NormalOpen));
                }
                if (cimSwitch.RatedCurrentHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SW_RATEDCURR, cimSwitch.RatedCurrent));
                }
                if (cimSwitch.RetainedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SW_RETAINED, cimSwitch.Retained));
                }
                if (cimSwitch.SwitchOnCountHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SW_SWONCOUNT, cimSwitch.SwitchOnCount));
                }
                if (cimSwitch.SwitchOnDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SW_SWONDATE, cimSwitch.SwitchOnDate));
                }
               
            }
        }

        public static void PopulateProtectedSwitchProperties(FTN.ProtectedSwitch cimProtectedSwitch, ResourceDescription rd)
        {
            if ((cimProtectedSwitch != null) && (rd != null))
            {
                BreakerConverter.PopulateSwitchProperties(cimProtectedSwitch, rd);
                if (cimProtectedSwitch.BreakingCapacityHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PROTSW_BRKCAP, cimProtectedSwitch.BreakingCapacity));
                }

            }
        }

        public static void PopulateBreakerProperties(FTN.Breaker cimBreaker, ResourceDescription rd)
        {
            if((cimBreaker != null) && (rd != null))
            {
                BreakerConverter.PopulateProtectedSwitchProperties(cimBreaker, rd);
                if (cimBreaker.InTransitTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BRKR_TRANSTIME, cimBreaker.InTransitTime));
                }
            }
        }
        #endregion Populate ResourceDescription


        #region Enums convert
        public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbols)
        {
            switch (unitSymbols)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;
                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh:
                    return UnitSymbol.VArh;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                default: return UnitSymbol.none;
            }
        }

        public static RegulatingControlModeKind GetDMSRegulatingControlModeKind(FTN.RegulatingControlModeKind regulatingControlModeKinds)
        {
            switch (regulatingControlModeKinds)
            {
                case FTN.RegulatingControlModeKind.activePower:
                    return RegulatingControlModeKind.activePower;
                case FTN.RegulatingControlModeKind.admittance:
                    return RegulatingControlModeKind.admittance;
                case FTN.RegulatingControlModeKind.currentFlow:
                    return RegulatingControlModeKind.currentFlow;
                case FTN.RegulatingControlModeKind.@fixed:
                    return RegulatingControlModeKind.@fixed;
                case FTN.RegulatingControlModeKind.powerFactor:
                    return RegulatingControlModeKind.powerFactor;
                case FTN.RegulatingControlModeKind.reactivePower:
                    return RegulatingControlModeKind.reactivePower;
                case FTN.RegulatingControlModeKind.temperature:
                    return RegulatingControlModeKind.temperature;
                case FTN.RegulatingControlModeKind.timeScheduled:
                    return RegulatingControlModeKind.timeScheduled;
                default: return RegulatingControlModeKind.voltage;
            }
        }

        public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
        {
            switch (phases)
            {
                case FTN.PhaseCode.A:
                    return PhaseCode.A;
                case FTN.PhaseCode.AB:
                    return PhaseCode.AB;
                case FTN.PhaseCode.ABC:
                    return PhaseCode.ABC;
                case FTN.PhaseCode.ABCN:
                    return PhaseCode.ABCN;
                case FTN.PhaseCode.ABN:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.AC:
                    return PhaseCode.AC;
                case FTN.PhaseCode.ACN:
                    return PhaseCode.ACN;
                case FTN.PhaseCode.AN:
                    return PhaseCode.AN;
                case FTN.PhaseCode.B:
                    return PhaseCode.B;
                case FTN.PhaseCode.BC:
                    return PhaseCode.BC;
                case FTN.PhaseCode.BCN:
                    return PhaseCode.BCN;
                case FTN.PhaseCode.BN:
                    return PhaseCode.BN;
                case FTN.PhaseCode.C:
                    return PhaseCode.C;
                case FTN.PhaseCode.CN:
                    return PhaseCode.CN;
                case FTN.PhaseCode.N:
                    return PhaseCode.N;
                case FTN.PhaseCode.s12N:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.s1N:
                    return PhaseCode.AN;
                case FTN.PhaseCode.s2N:
                    return PhaseCode.BN;
                default: return PhaseCode.Unknown;
            }
        }
        #endregion Enums convert
    }
}
