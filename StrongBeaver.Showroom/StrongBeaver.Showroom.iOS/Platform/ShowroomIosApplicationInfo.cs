using StrongBeaver.Core.Platform;
using StrongBeaver.Showroom.Constants;
using System;

namespace StrongBeaver.Showroom.iOS.Platform
{
    public class ShowroomIosApplicationInfo : BaseIosApplicationInfo
    {
        public override bool IsBackgroundTask => false;

        public override string Name => ShowroomConstants.APPLICATION_NAME;

        public override Guid Identifier => ShowroomConstants.APPLICATION_GUID;

        public override string Secret => ShowroomConstants.APPLICATION_SECRET;
    }
}
