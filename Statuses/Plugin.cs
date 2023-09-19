using Exiled.API.Features;
using Exiled.Events.Handlers;
using MEC;
using Statuses.API;
using Statuses.API.Statuses;
using Statuses.Events.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuses
{
    public class Plugin : Plugin<Config>
    {
        public override void OnEnabled()
        {
            API.API.Register<LowHealthStatus>();
            API.API.Register<SomePositiveEffectStatus>();

            Timing.RunCoroutine(CheckHealthCoroutine());

            Exiled.Events.Handlers.Player.ReceivingEffect += PlayerHandler.OnReceivingEffect;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.ReceivingEffect -= PlayerHandler.OnReceivingEffect;

            base.OnDisabled();
        }


        private IEnumerator<float> CheckHealthCoroutine()
        {
            while (true)
            {
                foreach (var player in Exiled.API.Features.Player.List)
                {
                    if (!player.IsAlive) continue;

                    if (player.Health < 35)
                    {
                        API.API.Enable<LowHealthStatus>(player);
                    }
                    else
                    {
                        API.API.Disable<LowHealthStatus>(player);
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
