using Lagrange.Core.Common;
using Lagrange.Core.Internal.Event.Protocol;
using Lagrange.Core.Internal.Event.Protocol.Action;
using Lagrange.Core.Internal.Packets.Service.Oidb;
using Lagrange.Core.Internal.Packets.Service.Oidb.Request;
using Lagrange.Core.Internal.Packets.Service.Oidb.Response;
using Lagrange.Core.Utility.Binary;
using Lagrange.Core.Utility.Extension;
using ProtoBuf;

namespace Lagrange.Core.Internal.Service.Action;

[EventSubscribe(typeof(GroupSetAdminEvent))]
[Service("OidbSvcTrpcTcp.0x1096_1")]
internal class GroupSetAdminService : BaseService<GroupSetAdminEvent>
{
    protected override bool Build(GroupSetAdminEvent input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device,
        out BinaryPacket output, out List<BinaryPacket>? extraPackets)
    {
        var packet = new OidbSvcTrpcTcpBase<OidbSvcTrpcTcp0x1096_1>(new OidbSvcTrpcTcp0x1096_1
        {
            GroupUin = input.GroupUin,
            Uid = input.Uid,
            IsAdmin = input.IsAdmin
        });
        
        output = packet.Serialize();
        extraPackets = null;
        return true;
    }

    protected override bool Parse(byte[] input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device, 
        out GroupSetAdminEvent output, out List<ProtocolEvent>? extraEvents)
    {
        var packet = Serializer.Deserialize<OidbSvcTrpcTcpResponse<OidbSvcTrpcTcp0x1096_1Response>>(input.AsSpan());
        
        output = GroupSetAdminEvent.Result((int)packet.ErrorCode);
        extraEvents = null;
        return true;
    }
}