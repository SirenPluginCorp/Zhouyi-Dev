﻿using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Memory;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Client.UI;
using System;

namespace ECommons.UIHelpers.AddonMasterImplementations;
public partial class AddonMaster
{
    public unsafe class SelectYesno : AddonMasterBase<AddonSelectYesno>
    {
        public SelectYesno(nint addon) : base(addon)
        {
        }

        public SelectYesno(void* addon) : base(addon) { }

        public SeString SeString => MemoryHelper.ReadSeString(&Addon->PromptText->NodeText);
        public string Text => SeString.ExtractText();

        public void Yes()
        {
            if (Addon->YesButton != null && !Addon->YesButton->IsEnabled)
            {
                Svc.Log.Debug($"{nameof(AddonSelectYesno)}: Force enabling yes button");
                var flagsPtr = (ushort*)&Addon->YesButton->AtkComponentBase.OwnerNode->AtkResNode.NodeFlags;
                *flagsPtr ^= 1 << 5;
            }
            ClickButtonIfEnabled(Addon->YesButton);
        }

        public void No() => ClickButtonIfEnabled(Addon->NoButton);
    }
}

[Obsolete("Please use AddonMaster.SelectYesno")]
public unsafe class SelectYesnoMaster : AddonMaster.SelectYesno
{
    public SelectYesnoMaster(nint addon) : base(addon)
    {
    }

    public SelectYesnoMaster(void* addon) : base(addon)
    {
    }
}
