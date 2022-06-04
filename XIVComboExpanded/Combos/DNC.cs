using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class DNC
    {
        public const byte JobID = 38;

        public const uint
            // Single Target
            Cascade = 15989,
            Fountain = 15990,
            ReverseCascade = 15991,
            Fountainfall = 15992,
            // AoE
            Windmill = 15993,
            Bladeshower = 15994,
            RisingWindmill = 15995,
            Bloodshower = 15996,
            // Dancing
            StandardStep = 15997,
            TechnicalStep = 15998,
            Tillana = 25790,
            // Fans
            FanDance1 = 16007,
            FanDance2 = 16008,
            FanDance3 = 16009,
            FanDance4 = 25791,
            // Other
            SaberDance = 16005,
            EnAvant = 16010,
            Devilment = 16011,
            Flourish = 16013,
            Improvisation = 16014,
            StarfallDance = 25792;

        public static class Buffs
        {
            public const ushort
                FlourishingSymmetry = 3017,
                SilkenSymmetry = 2693,
                FlourishingFlow = 3018,
                SilkenFlow = 2694,
                FlourishingFinish = 2698,
                FlourishingStarfall = 2700,
                StandardStep = 1818,
                TechnicalStep = 1819,
                ThreefoldFanDance = 1820,
                FourfoldFanDance = 2699,
                TechnicalFinish = 1822,
                Devilment = 1825;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Cascade = 1,
                Fountain = 2,
                Windmill = 15,
                StandardStep = 15,
                ReverseCascade = 20,
                Bladeshower = 25,
                RisingWindmill = 35,
                Fountainfall = 40,
                Bloodshower = 45,
                FanDance3 = 66,
                TechnicalStep = 70,
                SaberDance = 76,
                Tillana = 82,
                FanDance4 = 86,
                StarfallDance = 90;
        }
    }

    internal class DancerDanceComboCompatibility : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerDanceComboCompatibility;

        protected override uint[] ActionIDs => Service.Configuration.DancerDanceCompatActionIDs;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<DNCGauge>();
            if (gauge.IsDancing)
            {
                var actionIDs = Service.Configuration.DancerDanceCompatActionIDs;

                if (actionID == actionIDs[0] || (actionIDs[0] == 0 && actionID == DNC.Cascade))
                    return OriginalHook(DNC.Cascade);

                if (actionID == actionIDs[1] || (actionIDs[1] == 0 && actionID == DNC.Flourish))
                    return OriginalHook(DNC.Fountain);

                if (actionID == actionIDs[2] || (actionIDs[2] == 0 && actionID == DNC.FanDance1))
                    return OriginalHook(DNC.ReverseCascade);

                if (actionID == actionIDs[3] || (actionIDs[3] == 0 && actionID == DNC.FanDance2))
                    return OriginalHook(DNC.Fountainfall);
            }

            return actionID;
        }
    }

    internal class DancerFanDanceCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerFanDanceCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.FanDance1 || actionID == DNC.FanDance2)
            {
                if (level >= DNC.Levels.FanDance4 && HasEffect(DNC.Buffs.FourfoldFanDance) && IsEnabled(CustomComboPreset.DancerFanDance4Combo))
                    return DNC.FanDance4;

                if (level >= DNC.Levels.FanDance3 && HasEffect(DNC.Buffs.ThreefoldFanDance))
                    return DNC.FanDance3;
            }

            return actionID;
        }
    }

    internal class DancerDanceStepCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerDanceStepCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.StandardStep)
            {
                var gauge = GetJobGauge<DNCGauge>();
                if (gauge.IsDancing && level >= DNC.Levels.StandardStep && HasEffect(DNC.Buffs.StandardStep))
                {
                    if (gauge.CompletedSteps < 2)
                        return gauge.NextStep;

                    return OriginalHook(DNC.StandardStep);
                }

                return DNC.StandardStep;
            }

            if (actionID == DNC.TechnicalStep)
            {
                var gauge = GetJobGauge<DNCGauge>();
                if (gauge.IsDancing && level >= DNC.Levels.TechnicalStep && HasEffect(DNC.Buffs.TechnicalStep))
                {
                    if (gauge.CompletedSteps < 4)
                        return gauge.NextStep;
                }

                return OriginalHook(DNC.TechnicalStep);
            }

            return actionID;
        }
    }

    internal class DancerFlourishFanDance3Feature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerFlourishFanDance4Feature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Flourish)
            {
                if (level >= DNC.Levels.FanDance3 && HasEffect(DNC.Buffs.ThreefoldFanDance))
                    return DNC.FanDance3;
            }

            return actionID;
        }
    }

    internal class DancerFlourishFanDance4Feature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerFlourishFanDance4Feature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Flourish)
            {
                if (level >= DNC.Levels.FanDance4 && HasEffect(DNC.Buffs.FourfoldFanDance))
                    return DNC.FanDance4;
            }

            return actionID;
        }
    }

    internal class DancerSingleTargetMultibutton : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerSingleTargetMultibutton;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Cascade)
            {
                // From Fountain
                if (level >= DNC.Levels.Fountainfall && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Fountainfall;

                // From Cascade
                if (level >= DNC.Levels.ReverseCascade && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.ReverseCascade;

                // Cascade Combo
                if (lastComboMove == DNC.Cascade && level >= DNC.Levels.Fountain)
                    return DNC.Fountain;

                return DNC.Cascade;
            }

            return actionID;
        }
    }

    internal class DancerSingleTargetProcs : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerSingleTargetProcs;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Cascade)
                if (level >= DNC.Levels.ReverseCascade && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.ReverseCascade;

            if (actionID == DNC.Fountain)
                if (level >= DNC.Levels.Fountainfall && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Fountainfall;

            return actionID;
        }
    }
    
    internal class DancerSingleTargeAttackEco : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerSingleTargeAttackEco;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Cascade)
            {
                var gauge = GetJobGauge<DNCGauge>();

                // Expiring Starfall Dance.
                if (level >= DNC.Levels.StarfallDance && HasEffectExpiring(DNC.Buffs.FlourishingStarfall))
                    return DNC.StarfallDance;

                // Expiring FountainFall.
                if (level >= DNC.Levels.Fountainfall && (HasEffectExpiring(DNC.Buffs.FlourishingFlow) || HasEffectExpiring(DNC.Buffs.SilkenFlow)))
                    return DNC.Fountainfall;

                // Expiring ReverseCascade.
                if (level >= DNC.Levels.ReverseCascade && (HasEffectExpiring(DNC.Buffs.FlourishingSymmetry) || HasEffectExpiring(DNC.Buffs.SilkenSymmetry)))
                    return DNC.ReverseCascade;

                // Saber Dance if about to overflow.
                if (level >= DNC.Levels.SaberDance && gauge.Esprit >= 85)
                    return DNC.SaberDance;

                // Try to use Fountain, but use FoutainFall if FountainFall is ready.
                if (level >= DNC.Levels.Fountain && lastComboMove == DNC.Cascade)
                {
                    if (level >= DNC.Levels.Fountainfall && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                        return DNC.Fountainfall;
                    return DNC.Fountain;
                }

                // Use ReverseCascade
                if (level >= DNC.Levels.ReverseCascade && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.ReverseCascade;

                return DNC.Cascade;
            }

            return actionID;
        }
    }

    internal class DancerSingleTargeAttackBurst : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerSingleTargeAttackBurst;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.SaberDance)
            {
                var gauge = GetJobGauge<DNCGauge>();

                // Expiring Starfall Dance.
                if (level >= DNC.Levels.StarfallDance && HasEffectExpiring(DNC.Buffs.FlourishingStarfall))
                    return DNC.StarfallDance;

                // Expiring FountainFall.
                if (level >= DNC.Levels.Fountainfall && (HasEffectExpiring(DNC.Buffs.FlourishingFlow) || HasEffectExpiring(DNC.Buffs.SilkenFlow)))
                    return DNC.Fountainfall;

                // Expiring ReverseCascade.
                if (level >= DNC.Levels.ReverseCascade && (HasEffectExpiring(DNC.Buffs.FlourishingSymmetry) || HasEffectExpiring(DNC.Buffs.SilkenSymmetry)))
                    return DNC.ReverseCascade;

                // Saber Dance if available.
                if (level >= DNC.Levels.SaberDance && gauge.Esprit >= 50)
                    return DNC.SaberDance;

                // Fountain fall.
                if (level >= DNC.Levels.Fountainfall && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Fountainfall;

                // Cascade
                if (level >= DNC.Levels.ReverseCascade && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.ReverseCascade;

                // Cascade Combo
                if (level >= DNC.Levels.Fountain && lastComboMove == DNC.Cascade)
                    return DNC.Fountain;

                return DNC.Cascade;
            }

            return actionID;
        }
    }

    internal class DancerAoeMultibutton : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerAoeMultibutton;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Windmill)
            {
                // From Bladeshower
                if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Bloodshower;

                // From Windmill
                if (level >= DNC.Levels.RisingWindmill && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.RisingWindmill;

                // Windmill Combo
                if (lastComboMove == DNC.Windmill && level >= DNC.Levels.Bladeshower)
                    return DNC.Bladeshower;

                return DNC.Windmill;
            }

            return actionID;
        }
    }

    internal class DancerAoEProcs : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerAoeProcs;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Windmill)
                if (level >= DNC.Levels.RisingWindmill && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.RisingWindmill;

            if (actionID == DNC.Bladeshower)
                if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Bloodshower;

            return actionID;
        }
    }
    
    internal class DanceAoeAttackEco : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DanceAoeAttackEco;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Cascade)
            {
                var gauge = GetJobGauge<DNCGauge>();

                // Expiring Starfall Dance.
                if (level >= DNC.Levels.StarfallDance && HasEffectExpiring(DNC.Buffs.FlourishingStarfall))
                    return DNC.StarfallDance;

                // Expiring Bloodshower.
                if (level >= DNC.Levels.Bloodshower && (HasEffectExpiring(DNC.Buffs.FlourishingFlow) || HasEffectExpiring(DNC.Buffs.SilkenFlow)))
                    return DNC.Bloodshower;

                // Expiring RisingWindmill.
                if (level >= DNC.Levels.RisingWindmill && (HasEffectExpiring(DNC.Buffs.FlourishingSymmetry) || HasEffectExpiring(DNC.Buffs.SilkenSymmetry)))
                    return DNC.RisingWindmill;

                // Saber Dance if about to overflow.
                if (level >= DNC.Levels.SaberDance && gauge.Esprit >= 85)
                    return DNC.SaberDance;

                // Try to use Bladeshower, but use Bloodshower if Bloodshower is ready.
                if (lastComboMove == DNC.Windmill && level >= DNC.Levels.Bladeshower)
                {
                    if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                        return DNC.Bloodshower;
                    return DNC.Bladeshower;
                }

                // Use RisingWindmill
                if (level >= DNC.Levels.RisingWindmill && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.RisingWindmill;

                return DNC.Windmill;
            }

            return actionID;
        }
    }

    internal class DancerAoeAttackBurst : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerAoeAttackBurst;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.SaberDance)
            {
                var gauge = GetJobGauge<DNCGauge>();

                // Expiring Starfall Dance.
                if (level >= DNC.Levels.StarfallDance && HasEffectExpiring(DNC.Buffs.FlourishingStarfall))
                    return DNC.StarfallDance;

                // Expiring Bloodshower.
                if (level >= DNC.Levels.Bloodshower && (HasEffectExpiring(DNC.Buffs.FlourishingFlow) || HasEffectExpiring(DNC.Buffs.SilkenFlow)))
                    return DNC.Bloodshower;

                // Expiring RisingWindmill.
                if (level >= DNC.Levels.RisingWindmill && (HasEffectExpiring(DNC.Buffs.FlourishingSymmetry) || HasEffectExpiring(DNC.Buffs.SilkenSymmetry)))
                    return DNC.RisingWindmill;

                // Saber Dance if available.
                if (level >= DNC.Levels.SaberDance && gauge.Esprit >= 50)
                    return DNC.SaberDance;

                // Bloodshower.
                if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Bloodshower;

                // RisingWindmill
                if (level >= DNC.Levels.RisingWindmill && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.RisingWindmill;

                // Windmill Combo
                if (level >= DNC.Levels.Bladeshower && lastComboMove == DNC.Windmill)
                    return DNC.Bladeshower;

                return DNC.Windmill;
            }

            return actionID;
        }
    }

    internal class DancerDevilmentFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerDevilmentFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Devilment)
            {
                if (level >= DNC.Levels.StarfallDance && HasEffect(DNC.Buffs.FlourishingStarfall))
                    return DNC.StarfallDance;

                return DNC.Devilment;
            }

            return actionID;
        }
    }

    internal class DancerTechnicalLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerTechnicalLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == DNC.TechnicalStep && OriginalHook(DNC.TechnicalStep) == DNC.TechnicalStep && IsActionOffCooldown(DNC.TechnicalStep) && HasEffectAny(DNC.Buffs.TechnicalFinish) && FindEffectAny(DNC.Buffs.TechnicalFinish)?.RemainingTime > 8 ? SMN.Physick : actionID;
        }
    }
}
