﻿using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class All
    {
        public const byte JobID = 0;

        public const uint
            Swiftcast = 7561,
            Resurrection = 173,
            Verraise = 7523,
            Raise = 125,
            Ascend = 3603,
            Egeiro = 24287,
            SolidReason = 232,
            AgelessWords = 215,
            WiseToTheWorldMIN = 26521,
            WiseToTheWorldBTN = 26522,
            LowBlow = 7540,
            Interject = 7538,
            LucidDreaming = 7562,
            Cast = 289,
            Hook = 296,
            CastLight = 2135,
            Snagging = 4100,
            SurfaceSlap = 4595,
            Gig = 7632,
            VeteranTrade = 7906,
            NaturesBounty = 7909,
            Salvage = 7910,
            ElectricCurrent = 26872,
            PrizeCatch = 26806;

        public static class Buffs
        {
            public const ushort
                Swiftcast = 167,
                EurekaMoment = 2765;
        }

        public static class Levels
        {
            public const byte
                Cast = 1,
                Hook = 1,
                Raise = 12,
                Snagging = 36,
                Gig = 61,
                Salvage = 67,
                VeteranTrade = 63,
                NaturesBounty = 69,
                SurfaceSlap = 71,
                PrizeCatch = 81;
        }
    }

    internal class AllSwiftcastFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllSwiftcastFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Raise || actionID == All.Resurrection || actionID == All.Ascend || actionID == All.Verraise || actionID == All.Egeiro)
            {
                if ((IsActionOffCooldown(All.Swiftcast) && !HasEffect(RDM.Buffs.Dualcast))
                    || level <= All.Levels.Raise
                    || (level <= RDM.Levels.Verraise && actionID == All.Verraise))
                    return All.Swiftcast;
            }

            return actionID;
        }
    }

    internal class AllEurekaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllEurekaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.SolidReason || actionID == All.AgelessWords)
            {
                if (HasEffect(All.Buffs.EurekaMoment))
                {
                    if (actionID == All.SolidReason)
                        return All.WiseToTheWorldMIN;
                    return All.WiseToTheWorldBTN;
                }
            }

            return actionID;
        }
    }

    internal class DoLCastHookFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolCastHookFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Cast)
            {
                if (HasCondition(ConditionFlag.Fishing))
                    return All.Hook;
            }

            return actionID;
        }
    }

    internal class DoLCastGigFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolCastGigFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Cast)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.Gig;
            }

            return actionID;
        }
    }

    internal class DoLSurfaceTradeFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolSurfaceTradeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.SurfaceSlap)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.VeteranTrade;
            }

            return actionID;
        }
    }

    internal class DoLPrizeBountyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolPrizeBountyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.PrizeCatch)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.NaturesBounty;
            }

            return actionID;
        }
    }

    internal class DoLSnaggingSalvageFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolSnaggingSalvageFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Snagging)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.Salvage;
            }

            return actionID;
        }
    }

    internal class DoLCastLightElectricCurrentFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolCastLightElectricCurrentFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.CastLight)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.ElectricCurrent;
            }

            return actionID;
        }
    }

    internal class AllTankInterruptFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllTankInterruptFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.LowBlow)
            {
                if (CanInterruptEnemy() && IsActionOffCooldown(All.Interject) && CanUseAction(All.Interject))
                    return All.Interject;
            }

            return actionID;
        }
    }
}