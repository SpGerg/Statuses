using Exiled.API.Features;
using Mirror;
using Statuses.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Statuses.API.Features.Components
{
    public class StatusObject : NetworkBehaviour
    {
        public Player Owner { get; private set; }

        public IStatus Status { get; private set; }

        public float Rotation { get; private set; }

        public int Count { get; private set; }

        public void Initialization(Player owner, IStatus status, float rotation, int count)
        {
            Owner = owner;
            Status = status;
            Rotation = rotation;
            Count = count;
        }

        public void Update()
        {
            if (!Owner.IsAlive || !API.Contains(Owner, Status))
            {
                NetworkServer.UnSpawn(gameObject);
            }

            transform.Rotate(0, Rotation * Time.deltaTime, 0);
            transform.position = API.GetPosition(Owner, Count);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var player = Player.Get(collision.gameObject);

            if (player == null)
            {
                return;
            }

            if (player == Owner)
            {
                return;
            }

            player.Teleport(Owner.Position);
        }
    }
}
