using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using Statuses.API;
using Statuses.API.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomPlayerEffects.StatusEffectBase;

namespace Statuses.Events.Internal
{
    internal class PlayerHandler
    {
        internal static void OnDying(DyingEventArgs ev)
        {
            API.API.Clear(ev.Player);
        }

        internal static void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (!ev.Player.IsAlive) return; //Cuz when player join to server, for player receiving all effects (it is throwing errors).

            if (ev.Effect.Classification is not EffectClassification.Positive)
            {
                return;
            }
            else
            {
                API.API.Disable<SomePositiveEffectStatus>(ev.Player);
            }

            if (API.API.Contains<SomePositiveEffectStatus>(ev.Player))
            {
                return;
            }

            API.API.Enable<SomePositiveEffectStatus>(ev.Player);
        }
    }
}
